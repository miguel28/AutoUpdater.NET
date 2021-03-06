﻿using System;
using System.ComponentModel;
using System.Net.Cache;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using RSAFileSignatures;

namespace AutoUpdaterDotNET
{
    internal partial class DownloadUpdateDialog : Form
    {
        private readonly string _downloadURL;

        private string _tempPath;

        private WebClient _webClient;

        private string _signature;

        public DownloadUpdateDialog(string downloadURL, string expected_signature)
        {
            InitializeComponent();

            _downloadURL = downloadURL;
            _signature = expected_signature;
        }

        private void DownloadUpdateDialogLoad(object sender, EventArgs e)
        {
            _webClient = new WebClient();

            var uri = new Uri(_downloadURL);

            _tempPath = Path.Combine(Path.GetTempPath(), GetFileName(_downloadURL));

            _webClient.DownloadProgressChanged += OnDownloadProgressChanged;

            _webClient.DownloadFileCompleted += OnDownloadComplete;

            _webClient.DownloadFileAsync(uri, _tempPath);
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (!File.Exists("Certificates\\UpdatesSignaturePubK.pem"))
                    return;

                try
                {
                    byte[] zipdata = File.ReadAllBytes(_tempPath);
                    RSACryptoServiceProvider csp = RSASignatures.ReadRSAPublicKeyPem("Certificates\\UpdatesSignaturePubK.pem");
                    bool hashValid = RSASignatures.Verify(zipdata, _signature, csp);

                    if (!hashValid)
                    {
                        Close();
                        var resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
                        string title = resources.GetString("SignatureError", CultureInfo.CurrentCulture);
                        string msg = resources.GetString("SignatureErrorDesc", CultureInfo.CurrentCulture);
                        MessageBox.Show(msg, title, 
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }
                }
                catch (Exception)
                {
                    return;
                }

                var processStartInfo = new ProcessStartInfo {FileName = _tempPath, UseShellExecute = true};
                var extension = Path.GetExtension(_tempPath);
                if (extension != null && extension.ToLower().Equals(".zip"))
                {
                    string installerPath = Path.Combine(Path.GetTempPath(), "ZipExtractor.exe");
                    File.WriteAllBytes(installerPath, Properties.Resources.ZipExtractor);
                    processStartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = installerPath,
                        Arguments = string.Format("\"{0}\" \"{1}\"", _tempPath, Assembly.GetEntryAssembly().Location)
                    };
                }
                try
                {
                    Process.Start(processStartInfo);
                }
                catch (Win32Exception exception)
                {
                    if (exception.NativeErrorCode != 1223)
                        throw;
                }

                var currentProcess = Process.GetCurrentProcess();
                foreach (var process in Process.GetProcessesByName(currentProcess.ProcessName))
                {
                    if (process.Id != currentProcess.Id)
                    {
                        process.Kill();
                    }
                }

                if (AutoUpdater.IsWinFormsApplication)
                {
                    Application.Exit();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private static string GetFileName(string url, string httpWebRequestMethod = "HEAD")
        {
            try
            {
                var fileName = string.Empty;
                var uri = new Uri(url);
                if (uri.Scheme.Equals(Uri.UriSchemeHttp) || uri.Scheme.Equals(Uri.UriSchemeHttps))
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                    httpWebRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    httpWebRequest.Method = httpWebRequestMethod;
                    httpWebRequest.AllowAutoRedirect = false;
                    var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (httpWebResponse.StatusCode.Equals(HttpStatusCode.Redirect) ||
                        httpWebResponse.StatusCode.Equals(HttpStatusCode.Moved) ||
                        httpWebResponse.StatusCode.Equals(HttpStatusCode.MovedPermanently))
                    {
                        if (httpWebResponse.Headers["Location"] != null)
                        {
                            var location = httpWebResponse.Headers["Location"];
                            fileName = GetFileName(location);
                            return fileName;
                        }
                    }
                    var contentDisposition = httpWebResponse.Headers["content-disposition"];
                    if (!string.IsNullOrEmpty(contentDisposition))
                    {
                        const string lookForFileName = "filename=";
                        var index = contentDisposition.IndexOf(lookForFileName, StringComparison.CurrentCultureIgnoreCase);
                        if (index >= 0)
                            fileName = contentDisposition.Substring(index + lookForFileName.Length);
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Substring(1, fileName.Length - 2);
                        }
                    }
                }
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(uri.LocalPath);
                }
                return fileName;
            }
            catch (WebException)
            {
                return GetFileName(url, "GET");
            }
        }

        private void DownloadUpdateDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _webClient.CancelAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Security.Cryptography;

using AutoUpdaterDotNET;
using RSAFileSignatures;
using System.IO.Compression;

namespace SignUpdate
{
    public partial class frmSignRelease : Form
    {
        private const string DefaultPrivateKey = "Certificates\\private.pem";

        public frmSignRelease()
        {
            InitializeComponent();

            if (File.Exists(DefaultPrivateKey))
            {
                txtPrivateKey.Text = DefaultPrivateKey;
            }
        }

        private void btnBrowsePrivate_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (DialogResult.OK == result)
            {
                txtPrivateKey.Text = dialog.FileName;
            }
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (DialogResult.OK == result)
            {
                txtFolderToSign.Text = dialog.SelectedPath;
            }
        }


        private void GetFolderRecursive(string folder, ref List<string> outList)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(folder);
            IEnumerable<string> directories = Directory.EnumerateDirectories(folder);

            outList.AddRange(files);

            foreach (string dir in directories)
            {
                GetFolderRecursive(dir, ref outList);
            }
        }

        private string FilterInZip(string rootFolder, string path)
        {
            return path.Replace(rootFolder, "");
        }

        private bool CreateZip(string zipFile, string source)
        {
            bool ret = false;
            ZipStorer storer = ZipStorer.Create(zipFile, "");
            List<string> rawFiles = new List<string>();
            GetFolderRecursive(source, ref rawFiles);

            try
            {
                rawFiles.ForEach(
                    x => storer.AddFile(ZipStorer.Compression.Deflate, x, FilterInZip(txtFolderToSign.Text, x), "")
                );
                storer.Close();
                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create ZIP File: " + ex.ToString(), "Zip Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        private string SignZip(string zipName, RSACryptoServiceProvider csp)
        {
            string signature = null;
            byte[] ZipRawData = null;
            try
            {
                ZipRawData = File.ReadAllBytes(zipName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read ZIP File: " + ex.ToString(), "Read Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return signature;
            }

            try
            {
                signature = RSASignatures.Sign(ZipRawData, csp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not sign ZIP File:  " + ex.ToString(), "Signature Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return signature;
            }

            return signature;
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrivateKey.Text))
            {
                MessageBox.Show("Please Select a Valid Private Key", "Private Key Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (    string.IsNullOrEmpty(txtFolderToSign.Text) 
                 || !Directory.Exists(txtFolderToSign.Text) )
            {
                MessageBox.Show("Please Select a Directory", "Directory Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!txtFolderToSign.Text.EndsWith("\\"))
            {
                txtFolderToSign.Text += "\\";
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Zip Files *.zip|*.zip";
            DialogResult result = dialog.ShowDialog();

            if (DialogResult.OK == result)
            {
                RSACryptoServiceProvider csp = null;
                try
                {
                    csp = RSASignatures.ReadRSAPrivateKeyPem(txtPrivateKey.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read RSA Private Key: " + ex.ToString(), "Private Key Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (CreateZip(dialog.FileName, txtFolderToSign.Text))
                {
                    string signature = SignZip(dialog.FileName, csp);

                    if (signature != null)
                    {
                        XMLUpdateManifest manifest = new XMLUpdateManifest();
                        manifest.url = "TBD";
                        manifest.version = "TBD";
                        manifest.changelog = "TBD";
                        manifest.signature = signature;

                        manifest.Serialize(Path.GetDirectoryName(dialog.FileName) + "\\UpdateManifest.xml");
                    }
                }

                

            }
        }
    }
}

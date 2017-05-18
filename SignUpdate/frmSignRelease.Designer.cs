namespace SignUpdate
{
    partial class frmSignRelease
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPrivateKey = new System.Windows.Forms.Label();
            this.lblFolderoSign = new System.Windows.Forms.Label();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.txtFolderToSign = new System.Windows.Forms.TextBox();
            this.btnSign = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnBrowsePrivate = new System.Windows.Forms.Button();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPrivateKey
            // 
            this.lblPrivateKey.AutoSize = true;
            this.lblPrivateKey.Location = new System.Drawing.Point(16, 28);
            this.lblPrivateKey.Name = "lblPrivateKey";
            this.lblPrivateKey.Size = new System.Drawing.Size(80, 17);
            this.lblPrivateKey.TabIndex = 0;
            this.lblPrivateKey.Text = "Private Key";
            // 
            // lblFolderoSign
            // 
            this.lblFolderoSign.AutoSize = true;
            this.lblFolderoSign.Location = new System.Drawing.Point(16, 69);
            this.lblFolderoSign.Name = "lblFolderoSign";
            this.lblFolderoSign.Size = new System.Drawing.Size(101, 17);
            this.lblFolderoSign.TabIndex = 1;
            this.lblFolderoSign.Text = "Folder To Sign";
            // 
            // txtPrivateKey
            // 
            this.txtPrivateKey.Location = new System.Drawing.Point(123, 25);
            this.txtPrivateKey.Name = "txtPrivateKey";
            this.txtPrivateKey.ReadOnly = true;
            this.txtPrivateKey.Size = new System.Drawing.Size(292, 22);
            this.txtPrivateKey.TabIndex = 2;
            // 
            // txtFolderToSign
            // 
            this.txtFolderToSign.Location = new System.Drawing.Point(123, 66);
            this.txtFolderToSign.Name = "txtFolderToSign";
            this.txtFolderToSign.ReadOnly = true;
            this.txtFolderToSign.Size = new System.Drawing.Size(292, 22);
            this.txtFolderToSign.TabIndex = 3;
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(123, 150);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(292, 44);
            this.btnSign.TabIndex = 4;
            this.btnSign.Text = "Sign";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(123, 108);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(292, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(16, 114);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(65, 17);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "Progress";
            // 
            // btnBrowsePrivate
            // 
            this.btnBrowsePrivate.Location = new System.Drawing.Point(433, 25);
            this.btnBrowsePrivate.Name = "btnBrowsePrivate";
            this.btnBrowsePrivate.Size = new System.Drawing.Size(58, 23);
            this.btnBrowsePrivate.TabIndex = 7;
            this.btnBrowsePrivate.Text = "...";
            this.btnBrowsePrivate.UseVisualStyleBackColor = true;
            this.btnBrowsePrivate.Click += new System.EventHandler(this.btnBrowsePrivate_Click);
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(433, 66);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(58, 23);
            this.btnBrowseFolder.TabIndex = 8;
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // frmSignRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 215);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.btnBrowsePrivate);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.txtFolderToSign);
            this.Controls.Add(this.txtPrivateKey);
            this.Controls.Add(this.lblFolderoSign);
            this.Controls.Add(this.lblPrivateKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSignRelease";
            this.Text = "Sign Update Release";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrivateKey;
        private System.Windows.Forms.Label lblFolderoSign;
        private System.Windows.Forms.TextBox txtPrivateKey;
        private System.Windows.Forms.TextBox txtFolderToSign;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnBrowsePrivate;
        private System.Windows.Forms.Button btnBrowseFolder;
    }
}


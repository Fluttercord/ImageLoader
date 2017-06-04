namespace WFImageLoader
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbAddressToAdd = new System.Windows.Forms.TextBox();
            this.btnSetpFolder = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.lbQueue = new System.Windows.Forms.ListBox();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbCurrent = new System.Windows.Forms.ProgressBar();
            this.pbTotal = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(384, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // tbAddressToAdd
            // 
            this.tbAddressToAdd.Location = new System.Drawing.Point(12, 26);
            this.tbAddressToAdd.Name = "tbAddressToAdd";
            this.tbAddressToAdd.Size = new System.Drawing.Size(366, 20);
            this.tbAddressToAdd.TabIndex = 1;
            // 
            // btnSetpFolder
            // 
            this.btnSetpFolder.Location = new System.Drawing.Point(384, 421);
            this.btnSetpFolder.Name = "btnSetpFolder";
            this.btnSetpFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetpFolder.TabIndex = 2;
            this.btnSetpFolder.Text = "setup folder";
            this.btnSetpFolder.UseVisualStyleBackColor = true;
            this.btnSetpFolder.Click += new System.EventHandler(this.BtnSetupFolderClick);
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(27, 424);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(351, 20);
            this.tbFolder.TabIndex = 3;
            this.tbFolder.Text = "C:\\Temp\\Images";
            // 
            // lbQueue
            // 
            this.lbQueue.FormattingEnabled = true;
            this.lbQueue.Location = new System.Drawing.Point(12, 52);
            this.lbQueue.Name = "lbQueue";
            this.lbQueue.Size = new System.Drawing.Size(366, 160);
            this.lbQueue.TabIndex = 4;
            // 
            // btnSuspend
            // 
            this.btnSuspend.Location = new System.Drawing.Point(12, 218);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(75, 23);
            this.btnSuspend.TabIndex = 5;
            this.btnSuspend.Text = "suspend";
            this.btnSuspend.UseVisualStyleBackColor = true;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(303, 218);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(93, 218);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(75, 23);
            this.btnResume.TabIndex = 7;
            this.btnResume.Text = "resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(23, 258);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 13);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Idle";
            // 
            // pbCurrent
            // 
            this.pbCurrent.Location = new System.Drawing.Point(12, 288);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(355, 23);
            this.pbCurrent.TabIndex = 9;
            // 
            // pbTotal
            // 
            this.pbTotal.Location = new System.Drawing.Point(12, 317);
            this.pbTotal.Name = "pbTotal";
            this.pbTotal.Size = new System.Drawing.Size(355, 23);
            this.pbTotal.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 468);
            this.Controls.Add(this.pbTotal);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSuspend);
            this.Controls.Add(this.lbQueue);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.btnSetpFolder);
            this.Controls.Add(this.tbAddressToAdd);
            this.Controls.Add(this.btnAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbAddressToAdd;
        private System.Windows.Forms.Button btnSetpFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.ListBox lbQueue;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pbCurrent;
        private System.Windows.Forms.ProgressBar pbTotal;
    }
}


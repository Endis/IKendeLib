namespace Package.Client
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.labName = new System.Windows.Forms.Label();
            this.labEMail = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.txtREMail = new System.Windows.Forms.TextBox();
            this.txtRName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdRegister = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtIPAddress = new System.Windows.Forms.ToolStripTextBox();
            this.cmdConnetc = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtResponseTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(11, 57);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(47, 12);
            this.labName.TabIndex = 0;
            this.labName.Text = "用户名:";
            // 
            // labEMail
            // 
            this.labEMail.AutoSize = true;
            this.labEMail.Location = new System.Drawing.Point(11, 85);
            this.labEMail.Name = "labEMail";
            this.labEMail.Size = new System.Drawing.Size(59, 12);
            this.labEMail.TabIndex = 1;
            this.labEMail.Text = "邮件地址:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(80, 51);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 21);
            this.txtName.TabIndex = 2;
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(80, 82);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(160, 21);
            this.txtEMail.TabIndex = 3;
            // 
            // txtREMail
            // 
            this.txtREMail.Location = new System.Drawing.Point(80, 183);
            this.txtREMail.Name = "txtREMail";
            this.txtREMail.Size = new System.Drawing.Size(160, 21);
            this.txtREMail.TabIndex = 7;
            // 
            // txtRName
            // 
            this.txtRName.Location = new System.Drawing.Point(80, 152);
            this.txtRName.Name = "txtRName";
            this.txtRName.Size = new System.Drawing.Size(160, 21);
            this.txtRName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "邮件地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户名:";
            // 
            // cmdRegister
            // 
            this.cmdRegister.Enabled = false;
            this.cmdRegister.Location = new System.Drawing.Point(165, 116);
            this.cmdRegister.Name = "cmdRegister";
            this.cmdRegister.Size = new System.Drawing.Size(75, 23);
            this.cmdRegister.TabIndex = 8;
            this.cmdRegister.Text = "注册";
            this.cmdRegister.UseVisualStyleBackColor = true;
            this.cmdRegister.Click += new System.EventHandler(this.cmdRegister_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtIPAddress,
            this.cmdConnetc});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(264, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(100, 25);
            this.txtIPAddress.Text = "127.0.0.1";
            // 
            // cmdConnetc
            // 
            this.cmdConnetc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdConnetc.Image = ((System.Drawing.Image)(resources.GetObject("cmdConnetc.Image")));
            this.cmdConnetc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdConnetc.Name = "cmdConnetc";
            this.cmdConnetc.Size = new System.Drawing.Size(36, 22);
            this.cmdConnetc.Text = "连接";
            this.cmdConnetc.Click += new System.EventHandler(this.cmdConnetc_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 272);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(264, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // txtResponseTime
            // 
            this.txtResponseTime.Location = new System.Drawing.Point(80, 210);
            this.txtResponseTime.Name = "txtResponseTime";
            this.txtResponseTime.Size = new System.Drawing.Size(160, 21);
            this.txtResponseTime.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "注册时间:";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 294);
            this.Controls.Add(this.txtResponseTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmdRegister);
            this.Controls.Add(this.txtREMail);
            this.Controls.Add(this.txtRName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEMail);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labEMail);
            this.Controls.Add(this.labName);
            this.Name = "FrmMain";
            this.Text = "Pacakge Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labEMail;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.TextBox txtREMail;
        private System.Windows.Forms.TextBox txtRName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdRegister;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtIPAddress;
        private System.Windows.Forms.ToolStripButton cmdConnetc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.TextBox txtResponseTime;
        private System.Windows.Forms.Label label3;
    }
}


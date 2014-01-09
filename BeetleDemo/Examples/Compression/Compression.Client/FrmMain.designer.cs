namespace Compression.Client
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtIPAddress = new System.Windows.Forms.ToolStripTextBox();
            this.cmdConnetc = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmdRegister = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtIPAddress,
            this.cmdConnetc});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(692, 25);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(692, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // cmdRegister
            // 
            this.cmdRegister.Enabled = false;
            this.cmdRegister.Location = new System.Drawing.Point(605, 290);
            this.cmdRegister.Name = "cmdRegister";
            this.cmdRegister.Size = new System.Drawing.Size(75, 23);
            this.cmdRegister.TabIndex = 8;
            this.cmdRegister.Text = "发送";
            this.cmdRegister.UseVisualStyleBackColor = true;
            this.cmdRegister.Click += new System.EventHandler(this.cmdRegister_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(668, 256);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 347);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmdRegister);
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

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtIPAddress;
        private System.Windows.Forms.ToolStripButton cmdConnetc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.Button cmdRegister;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}


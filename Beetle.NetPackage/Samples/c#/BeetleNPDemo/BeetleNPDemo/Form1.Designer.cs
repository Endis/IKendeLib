namespace BeetleNPDemo
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdRegister = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtError = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 28);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(153, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(101, 55);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(153, 21);
            this.txtEMail.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "EMail:";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(101, 82);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(153, 21);
            this.txtCity.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "City:";
            // 
            // txtCountry
            // 
            this.txtCountry.Location = new System.Drawing.Point(101, 109);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(153, 21);
            this.txtCountry.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Country:";
            // 
            // txtRegTime
            // 
            this.txtRegTime.Location = new System.Drawing.Point(101, 136);
            this.txtRegTime.Name = "txtRegTime";
            this.txtRegTime.ReadOnly = true;
            this.txtRegTime.Size = new System.Drawing.Size(153, 21);
            this.txtRegTime.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "RegTime:";
            // 
            // cmdRegister
            // 
            this.cmdRegister.Location = new System.Drawing.Point(179, 164);
            this.cmdRegister.Name = "cmdRegister";
            this.cmdRegister.Size = new System.Drawing.Size(75, 23);
            this.cmdRegister.TabIndex = 10;
            this.cmdRegister.Text = "Register";
            this.cmdRegister.UseVisualStyleBackColor = true;
            this.cmdRegister.Click += new System.EventHandler(this.cmdRegister_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtConnectionStatus,
            this.txtStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 219);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(301, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtConnectionStatus
            // 
            this.txtConnectionStatus.Name = "txtConnectionStatus";
            this.txtConnectionStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // txtError
            // 
            this.txtError.AutoSize = true;
            this.txtError.Location = new System.Drawing.Point(41, 204);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(0, 12);
            this.txtError.TabIndex = 12;
            // 
            // txtStatus
            // 
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 241);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdRegister);
            this.Controls.Add(this.txtRegTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCountry);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BeetleNPDemo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRegTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdRegister;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtConnectionStatus;
        private System.Windows.Forms.Label txtError;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
    }
}


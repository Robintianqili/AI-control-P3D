
namespace AI_Control
{
    partial class AI_Main
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.p3d_message_richbox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.load_sca_btn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.send_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // p3d_message_richbox
            // 
            this.p3d_message_richbox.Location = new System.Drawing.Point(46, 97);
            this.p3d_message_richbox.Name = "p3d_message_richbox";
            this.p3d_message_richbox.Size = new System.Drawing.Size(274, 189);
            this.p3d_message_richbox.TabIndex = 2;
            this.p3d_message_richbox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "P3D Message";
            // 
            // load_sca_btn
            // 
            this.load_sca_btn.Location = new System.Drawing.Point(407, 97);
            this.load_sca_btn.Name = "load_sca_btn";
            this.load_sca_btn.Size = new System.Drawing.Size(129, 23);
            this.load_sca_btn.TabIndex = 4;
            this.load_sca_btn.Text = "load scenario";
            this.load_sca_btn.UseVisualStyleBackColor = true;
            this.load_sca_btn.Click += new System.EventHandler(this.load_sca_btn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(407, 189);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(129, 23);
            this.send_btn.TabIndex = 5;
            this.send_btn.Text = "send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // AI_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.load_sca_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.p3d_message_richbox);
            this.Name = "AI_Main";
            this.Text = "AI aircraft  （2023.9.11）";
            this.Load += new System.EventHandler(this.AI_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox p3d_message_richbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button load_sca_btn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button send_btn;
    }
}


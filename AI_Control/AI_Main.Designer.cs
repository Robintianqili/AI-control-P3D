﻿
namespace AI_Control
{
    partial class AI_Main
    {
        /// <summary>
        /// The essential components variables.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clear all resources currently used
        /// </summary>
        /// <param name="disposing">if 
        /// you want to release managed resources, set it as true
        /// otherwise false.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows codes generated by Windows designer

        /// <summary>
        /// Do not edit, all methods supports designer.
        /// Using code editor to edit the content
        /// </summary>
        private void InitializeComponent()
        {
            this.p3d_message_richbox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // AI_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.p3d_message_richbox);
            this.Name = "AI_Main";
            this.Text = "AI aircraft  （2023.9.5）";
            this.Load += new System.EventHandler(this.AI_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox p3d_message_richbox;
        private System.Windows.Forms.Label label1;
    }
}

﻿namespace OverTest
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.windowBar1 = new AntdUI.WindowBar();
            this.SuspendLayout();
            // 
            // windowBar1
            // 
            this.windowBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowBar1.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowBar1.Location = new System.Drawing.Point(0, 0);
            this.windowBar1.Name = "windowBar1";
            this.windowBar1.Size = new System.Drawing.Size(1247, 43);
            this.windowBar1.TabIndex = 0;
            this.windowBar1.Text = "windowBar1";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1247, 772);
            this.Controls.Add(this.windowBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.WindowBar windowBar1;
    }
}


namespace TestMain.UserControls
{
    partial class SerialPortForm
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.uiPanel1.SuspendLayout();
            this.uiTitlePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiTitlePanel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(1669, 786);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Controls.Add(this.comboBox1);
            this.uiTitlePanel1.Controls.Add(this.uiLabel1);
            this.uiTitlePanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel1.Location = new System.Drawing.Point(0, 0);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.ShowText = false;
            this.uiTitlePanel1.Size = new System.Drawing.Size(243, 274);
            this.uiTitlePanel1.TabIndex = 0;
            this.uiTitlePanel1.Text = "通信设置";
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(3, 41);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(96, 29);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "串口号";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(105, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(135, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // SerialPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiPanel1);
            this.Name = "SerialPortForm";
            this.Size = new System.Drawing.Size(1669, 786);
            this.uiPanel1.ResumeLayout(false);
            this.uiTitlePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UITitlePanel uiTitlePanel1;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

namespace TestMain.UserControls
{
    partial class IOControls
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IOControls));
            this.uiTitlePanel3 = new Sunny.UI.UITitlePanel();
            this.uiCheckBoxGroup1 = new Sunny.UI.UICheckBoxGroup();
            this.uiButton6 = new Sunny.UI.UIButton();
            this.uiButton7 = new Sunny.UI.UIButton();
            this.uiButton5 = new Sunny.UI.UIButton();
            this.uiButton4 = new Sunny.UI.UIButton();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.outIO7 = new Sunny.UI.UILight();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.outIO1 = new Sunny.UI.UILight();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiLabel14 = new Sunny.UI.UILabel();
            this.outIO6 = new Sunny.UI.UILight();
            this.outIO4 = new Sunny.UI.UILight();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.outIO3 = new Sunny.UI.UILight();
            this.outIO2 = new Sunny.UI.UILight();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.outIO5 = new Sunny.UI.UILight();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiTitlePanel3.SuspendLayout();
            this.uiCheckBoxGroup1.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTitlePanel3
            // 
            this.uiTitlePanel3.Controls.Add(this.uiCheckBoxGroup1);
            this.uiTitlePanel3.Controls.Add(this.uiGroupBox1);
            this.uiTitlePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTitlePanel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel3.Location = new System.Drawing.Point(0, 0);
            this.uiTitlePanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel3.Name = "uiTitlePanel3";
            this.uiTitlePanel3.ShowText = false;
            this.uiTitlePanel3.Size = new System.Drawing.Size(1669, 196);
            this.uiTitlePanel3.TabIndex = 3;
            this.uiTitlePanel3.Text = "真空吸";
            this.uiTitlePanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiCheckBoxGroup1
            // 
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton6);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton7);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton5);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton4);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton3);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton2);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton1);
            this.uiCheckBoxGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiCheckBoxGroup1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiCheckBoxGroup1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.uiCheckBoxGroup1.Location = new System.Drawing.Point(0, 91);
            this.uiCheckBoxGroup1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiCheckBoxGroup1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBoxGroup1.Name = "uiCheckBoxGroup1";
            this.uiCheckBoxGroup1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiCheckBoxGroup1.SelectedIndexes = ((System.Collections.Generic.List<int>)(resources.GetObject("uiCheckBoxGroup1.SelectedIndexes")));
            this.uiCheckBoxGroup1.Size = new System.Drawing.Size(1669, 105);
            this.uiCheckBoxGroup1.TabIndex = 13;
            this.uiCheckBoxGroup1.Text = null;
            this.uiCheckBoxGroup1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiButton6
            // 
            this.uiButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton6.Location = new System.Drawing.Point(1183, 47);
            this.uiButton6.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton6.Name = "uiButton6";
            this.uiButton6.Size = new System.Drawing.Size(147, 42);
            this.uiButton6.TabIndex = 5;
            this.uiButton6.Text = "on";
            this.uiButton6.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton6.Click += new System.EventHandler(this.uiButton6_Click);
            // 
            // uiButton7
            // 
            this.uiButton7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton7.Location = new System.Drawing.Point(1427, 47);
            this.uiButton7.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton7.Name = "uiButton7";
            this.uiButton7.Size = new System.Drawing.Size(147, 42);
            this.uiButton7.TabIndex = 5;
            this.uiButton7.Text = "on";
            this.uiButton7.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton7.Click += new System.EventHandler(this.uiButton7_Click);
            // 
            // uiButton5
            // 
            this.uiButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton5.Location = new System.Drawing.Point(947, 47);
            this.uiButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.Size = new System.Drawing.Size(147, 42);
            this.uiButton5.TabIndex = 4;
            this.uiButton5.Text = "on";
            this.uiButton5.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton5.Click += new System.EventHandler(this.uiButton5_Click);
            // 
            // uiButton4
            // 
            this.uiButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton4.Location = new System.Drawing.Point(729, 47);
            this.uiButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton4.Name = "uiButton4";
            this.uiButton4.Size = new System.Drawing.Size(147, 42);
            this.uiButton4.TabIndex = 3;
            this.uiButton4.Text = "on";
            this.uiButton4.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton4.Click += new System.EventHandler(this.uiButton4_Click);
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton3.Location = new System.Drawing.Point(497, 47);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.Size = new System.Drawing.Size(147, 42);
            this.uiButton3.TabIndex = 2;
            this.uiButton3.Text = "on";
            this.uiButton3.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton3.Click += new System.EventHandler(this.uiButton3_Click);
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Location = new System.Drawing.Point(277, 47);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(147, 42);
            this.uiButton2.TabIndex = 1;
            this.uiButton2.Text = "on";
            this.uiButton2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Location = new System.Drawing.Point(65, 47);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(147, 42);
            this.uiButton1.TabIndex = 0;
            this.uiButton1.Text = "on";
            this.uiButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiLabel1);
            this.uiGroupBox1.Controls.Add(this.outIO7);
            this.uiGroupBox1.Controls.Add(this.uiLabel10);
            this.uiGroupBox1.Controls.Add(this.outIO1);
            this.uiGroupBox1.Controls.Add(this.uiLabel9);
            this.uiGroupBox1.Controls.Add(this.uiLabel14);
            this.uiGroupBox1.Controls.Add(this.outIO6);
            this.uiGroupBox1.Controls.Add(this.outIO4);
            this.uiGroupBox1.Controls.Add(this.uiLabel13);
            this.uiGroupBox1.Controls.Add(this.outIO3);
            this.uiGroupBox1.Controls.Add(this.outIO2);
            this.uiGroupBox1.Controls.Add(this.uiLabel11);
            this.uiGroupBox1.Controls.Add(this.uiLabel12);
            this.uiGroupBox1.Controls.Add(this.outIO5);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(1669, 196);
            this.uiGroupBox1.TabIndex = 12;
            this.uiGroupBox1.Text = "输出";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(1412, 35);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(111, 54);
            this.uiLabel1.TabIndex = 13;
            this.uiLabel1.Text = "真空7";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outIO7
            // 
            this.outIO7.BackColor = System.Drawing.Color.Transparent;
            this.outIO7.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO7.Location = new System.Drawing.Point(1529, 35);
            this.outIO7.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO7.Name = "outIO7";
            this.outIO7.OffColor = System.Drawing.Color.Red;
            this.outIO7.Radius = 54;
            this.outIO7.Size = new System.Drawing.Size(111, 54);
            this.outIO7.TabIndex = 12;
            this.outIO7.Text = "uiLight7";
            // 
            // uiLabel10
            // 
            this.uiLabel10.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel10.Location = new System.Drawing.Point(489, 35);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(111, 54);
            this.uiLabel10.TabIndex = 9;
            this.uiLabel10.Text = "真空3";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outIO1
            // 
            this.outIO1.BackColor = System.Drawing.Color.Transparent;
            this.outIO1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO1.Location = new System.Drawing.Point(169, 35);
            this.outIO1.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO1.Name = "outIO1";
            this.outIO1.OffColor = System.Drawing.Color.Red;
            this.outIO1.Radius = 53;
            this.outIO1.Size = new System.Drawing.Size(53, 54);
            this.outIO1.TabIndex = 0;
            this.outIO1.Text = "uiLight12";
            // 
            // uiLabel9
            // 
            this.uiLabel9.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel9.Location = new System.Drawing.Point(1171, 35);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(111, 54);
            this.uiLabel9.TabIndex = 11;
            this.uiLabel9.Text = "真空6";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel14
            // 
            this.uiLabel14.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel14.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel14.Location = new System.Drawing.Point(52, 35);
            this.uiLabel14.Name = "uiLabel14";
            this.uiLabel14.Size = new System.Drawing.Size(111, 54);
            this.uiLabel14.TabIndex = 1;
            this.uiLabel14.Text = "真空1";
            this.uiLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outIO6
            // 
            this.outIO6.BackColor = System.Drawing.Color.Transparent;
            this.outIO6.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO6.Location = new System.Drawing.Point(1288, 35);
            this.outIO6.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO6.Name = "outIO6";
            this.outIO6.OffColor = System.Drawing.Color.Red;
            this.outIO6.Radius = 54;
            this.outIO6.Size = new System.Drawing.Size(111, 54);
            this.outIO6.TabIndex = 10;
            this.outIO6.Text = "uiLight7";
            // 
            // outIO4
            // 
            this.outIO4.BackColor = System.Drawing.Color.Transparent;
            this.outIO4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO4.Location = new System.Drawing.Point(832, 35);
            this.outIO4.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO4.Name = "outIO4";
            this.outIO4.OffColor = System.Drawing.Color.Red;
            this.outIO4.Radius = 54;
            this.outIO4.Size = new System.Drawing.Size(111, 54);
            this.outIO4.TabIndex = 2;
            this.outIO4.Text = "uiLight11";
            // 
            // uiLabel13
            // 
            this.uiLabel13.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel13.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel13.Location = new System.Drawing.Point(715, 35);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(111, 54);
            this.uiLabel13.TabIndex = 3;
            this.uiLabel13.Text = "真空4";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outIO3
            // 
            this.outIO3.BackColor = System.Drawing.Color.Transparent;
            this.outIO3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO3.Location = new System.Drawing.Point(606, 35);
            this.outIO3.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO3.Name = "outIO3";
            this.outIO3.OffColor = System.Drawing.Color.Red;
            this.outIO3.Radius = 54;
            this.outIO3.Size = new System.Drawing.Size(111, 54);
            this.outIO3.TabIndex = 8;
            this.outIO3.Text = "uiLight8";
            // 
            // outIO2
            // 
            this.outIO2.BackColor = System.Drawing.Color.Transparent;
            this.outIO2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO2.Location = new System.Drawing.Point(379, 35);
            this.outIO2.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO2.Name = "outIO2";
            this.outIO2.OffColor = System.Drawing.Color.Red;
            this.outIO2.Radius = 54;
            this.outIO2.Size = new System.Drawing.Size(55, 54);
            this.outIO2.TabIndex = 4;
            this.outIO2.Text = "uiLight10";
            // 
            // uiLabel11
            // 
            this.uiLabel11.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel11.Location = new System.Drawing.Point(940, 35);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(111, 54);
            this.uiLabel11.TabIndex = 7;
            this.uiLabel11.Text = "真空5";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel12
            // 
            this.uiLabel12.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel12.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel12.Location = new System.Drawing.Point(262, 35);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(111, 54);
            this.uiLabel12.TabIndex = 5;
            this.uiLabel12.Text = "真空2";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outIO5
            // 
            this.outIO5.BackColor = System.Drawing.Color.Transparent;
            this.outIO5.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outIO5.Location = new System.Drawing.Point(1057, 35);
            this.outIO5.MinimumSize = new System.Drawing.Size(1, 1);
            this.outIO5.Name = "outIO5";
            this.outIO5.OffColor = System.Drawing.Color.Red;
            this.outIO5.Radius = 54;
            this.outIO5.Size = new System.Drawing.Size(111, 54);
            this.outIO5.TabIndex = 6;
            this.outIO5.Text = "uiLight9";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // IOControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiTitlePanel3);
            this.Name = "IOControls";
            this.Size = new System.Drawing.Size(1669, 196);
            this.uiTitlePanel3.ResumeLayout(false);
            this.uiCheckBoxGroup1.ResumeLayout(false);
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITitlePanel uiTitlePanel3;
        private Sunny.UI.UILabel uiLabel9;
        private Sunny.UI.UILight outIO6;
        private Sunny.UI.UILabel uiLabel10;
        private Sunny.UI.UILight outIO3;
        private Sunny.UI.UILabel uiLabel11;
        private Sunny.UI.UILight outIO5;
        private Sunny.UI.UILabel uiLabel12;
        private Sunny.UI.UILight outIO2;
        private Sunny.UI.UILabel uiLabel13;
        private Sunny.UI.UILight outIO4;
        private Sunny.UI.UILabel uiLabel14;
        private Sunny.UI.UILight outIO1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UICheckBoxGroup uiCheckBoxGroup1;
        private Sunny.UI.UIButton uiButton7;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton1;
        private System.Windows.Forms.Timer timer1;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILight outIO7;
    }
}

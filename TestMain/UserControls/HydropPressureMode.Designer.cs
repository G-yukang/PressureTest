namespace TestMain.UserControls
{
    partial class HydropPressureMode
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HydropPressureMode));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.uiCheckBoxGroup1 = new Sunny.UI.UICheckBoxGroup();
            this.uiLabel15 = new Sunny.UI.UILabel();
            this.Speel1 = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel16 = new Sunny.UI.UILabel();
            this.uiLabel14 = new Sunny.UI.UILabel();
            this.Speel2 = new Sunny.UI.UITextBox();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.Vacuometertext = new Sunny.UI.UITextBox();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.CurrenTimeText = new Sunny.UI.UITextBox();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.GoSpeelText = new Sunny.UI.UITextBox();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.PressTime = new Sunny.UI.UITextBox();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.LocationText = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.PressureText = new Sunny.UI.UITextBox();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiButton9 = new Sunny.UI.UIButton();
            this.uiButton8 = new Sunny.UI.UIButton();
            this.uiButton7 = new Sunny.UI.UIButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiPanel1.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.uiCheckBoxGroup1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiPanel3);
            this.uiPanel1.Controls.Add(this.uiPanel2);
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(1669, 786);
            this.uiPanel1.TabIndex = 1;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.chart1);
            this.uiPanel3.Controls.Add(this.uiCheckBoxGroup1);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(0, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(1669, 706);
            this.uiPanel3.TabIndex = 41;
            this.uiPanel3.Text = "uiPanel3";
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1145, 706);
            this.chart1.TabIndex = 40;
            this.chart1.Text = "chart1";
            // 
            // uiCheckBoxGroup1
            // 
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel15);
            this.uiCheckBoxGroup1.Controls.Add(this.Speel1);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel2);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel16);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel14);
            this.uiCheckBoxGroup1.Controls.Add(this.Speel2);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel13);
            this.uiCheckBoxGroup1.Controls.Add(this.Vacuometertext);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel10);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel12);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel3);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel11);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel9);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel5);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel6);
            this.uiCheckBoxGroup1.Controls.Add(this.CurrenTimeText);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel7);
            this.uiCheckBoxGroup1.Controls.Add(this.GoSpeelText);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel8);
            this.uiCheckBoxGroup1.Controls.Add(this.PressTime);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton2);
            this.uiCheckBoxGroup1.Controls.Add(this.uiButton1);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel4);
            this.uiCheckBoxGroup1.Controls.Add(this.LocationText);
            this.uiCheckBoxGroup1.Controls.Add(this.uiLabel1);
            this.uiCheckBoxGroup1.Controls.Add(this.PressureText);
            this.uiCheckBoxGroup1.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiCheckBoxGroup1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiCheckBoxGroup1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.uiCheckBoxGroup1.Location = new System.Drawing.Point(1145, 0);
            this.uiCheckBoxGroup1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiCheckBoxGroup1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBoxGroup1.Name = "uiCheckBoxGroup1";
            this.uiCheckBoxGroup1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiCheckBoxGroup1.SelectedIndexes = ((System.Collections.Generic.List<int>)(resources.GetObject("uiCheckBoxGroup1.SelectedIndexes")));
            this.uiCheckBoxGroup1.Size = new System.Drawing.Size(524, 706);
            this.uiCheckBoxGroup1.TabIndex = 38;
            this.uiCheckBoxGroup1.Text = "参数设置";
            this.uiCheckBoxGroup1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel15
            // 
            this.uiLabel15.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel15.Location = new System.Drawing.Point(406, 69);
            this.uiLabel15.Name = "uiLabel15";
            this.uiLabel15.Size = new System.Drawing.Size(115, 49);
            this.uiLabel15.TabIndex = 62;
            this.uiLabel15.Text = "pa";
            this.uiLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Speel1
            // 
            this.Speel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Speel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Speel1.Location = new System.Drawing.Point(149, 128);
            this.Speel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Speel1.MinimumSize = new System.Drawing.Size(1, 16);
            this.Speel1.Name = "Speel1";
            this.Speel1.Padding = new System.Windows.Forms.Padding(5);
            this.Speel1.ShowText = false;
            this.Speel1.Size = new System.Drawing.Size(250, 49);
            this.Speel1.TabIndex = 39;
            this.Speel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Speel1.Watermark = "";
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(24, 128);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(112, 49);
            this.uiLabel2.TabIndex = 40;
            this.uiLabel2.Text = "速度1:";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabel16
            // 
            this.uiLabel16.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel16.Location = new System.Drawing.Point(21, 69);
            this.uiLabel16.Name = "uiLabel16";
            this.uiLabel16.Size = new System.Drawing.Size(112, 49);
            this.uiLabel16.TabIndex = 61;
            this.uiLabel16.Text = "真空计:";
            this.uiLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabel14
            // 
            this.uiLabel14.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel14.Location = new System.Drawing.Point(406, 482);
            this.uiLabel14.Name = "uiLabel14";
            this.uiLabel14.Size = new System.Drawing.Size(115, 49);
            this.uiLabel14.TabIndex = 59;
            this.uiLabel14.Text = "S";
            this.uiLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Speel2
            // 
            this.Speel2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Speel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Speel2.Location = new System.Drawing.Point(149, 187);
            this.Speel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Speel2.MinimumSize = new System.Drawing.Size(1, 16);
            this.Speel2.Name = "Speel2";
            this.Speel2.Padding = new System.Windows.Forms.Padding(5);
            this.Speel2.ShowText = false;
            this.Speel2.Size = new System.Drawing.Size(250, 49);
            this.Speel2.TabIndex = 41;
            this.Speel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Speel2.Watermark = "";
            // 
            // uiLabel13
            // 
            this.uiLabel13.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel13.Location = new System.Drawing.Point(409, 423);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(115, 49);
            this.uiLabel13.TabIndex = 58;
            this.uiLabel13.Text = "mm/s";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Vacuometertext
            // 
            this.Vacuometertext.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Vacuometertext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Vacuometertext.Location = new System.Drawing.Point(149, 69);
            this.Vacuometertext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Vacuometertext.MinimumSize = new System.Drawing.Size(1, 16);
            this.Vacuometertext.Name = "Vacuometertext";
            this.Vacuometertext.Padding = new System.Windows.Forms.Padding(5);
            this.Vacuometertext.ShowText = false;
            this.Vacuometertext.Size = new System.Drawing.Size(250, 49);
            this.Vacuometertext.TabIndex = 60;
            this.Vacuometertext.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Vacuometertext.Watermark = "";
            // 
            // uiLabel10
            // 
            this.uiLabel10.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel10.Location = new System.Drawing.Point(406, 187);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(115, 49);
            this.uiLabel10.TabIndex = 55;
            this.uiLabel10.Text = "mm/s";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel12
            // 
            this.uiLabel12.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel12.Location = new System.Drawing.Point(409, 364);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(115, 49);
            this.uiLabel12.TabIndex = 57;
            this.uiLabel12.Text = "S";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(21, 187);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(112, 49);
            this.uiLabel3.TabIndex = 42;
            this.uiLabel3.Text = "速度2:";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabel11
            // 
            this.uiLabel11.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel11.Location = new System.Drawing.Point(409, 305);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(115, 49);
            this.uiLabel11.TabIndex = 56;
            this.uiLabel11.Text = "mm";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel9
            // 
            this.uiLabel9.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel9.Location = new System.Drawing.Point(406, 128);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(115, 49);
            this.uiLabel9.TabIndex = 54;
            this.uiLabel9.Text = "mm/s";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            this.uiLabel5.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel5.Location = new System.Drawing.Point(406, 246);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(115, 49);
            this.uiLabel5.TabIndex = 53;
            this.uiLabel5.Text = "kg";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel6
            // 
            this.uiLabel6.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel6.Location = new System.Drawing.Point(21, 482);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(112, 49);
            this.uiLabel6.TabIndex = 52;
            this.uiLabel6.Text = "当前保压时间:";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CurrenTimeText
            // 
            this.CurrenTimeText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrenTimeText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrenTimeText.Location = new System.Drawing.Point(149, 482);
            this.CurrenTimeText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrenTimeText.MinimumSize = new System.Drawing.Size(1, 16);
            this.CurrenTimeText.Name = "CurrenTimeText";
            this.CurrenTimeText.Padding = new System.Windows.Forms.Padding(5);
            this.CurrenTimeText.ShowText = false;
            this.CurrenTimeText.Size = new System.Drawing.Size(250, 49);
            this.CurrenTimeText.TabIndex = 51;
            this.CurrenTimeText.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.CurrenTimeText.Watermark = "";
            // 
            // uiLabel7
            // 
            this.uiLabel7.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel7.Location = new System.Drawing.Point(24, 423);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(112, 49);
            this.uiLabel7.TabIndex = 50;
            this.uiLabel7.Text = "回程速度:";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GoSpeelText
            // 
            this.GoSpeelText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GoSpeelText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GoSpeelText.Location = new System.Drawing.Point(149, 423);
            this.GoSpeelText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GoSpeelText.MinimumSize = new System.Drawing.Size(1, 16);
            this.GoSpeelText.Name = "GoSpeelText";
            this.GoSpeelText.Padding = new System.Windows.Forms.Padding(5);
            this.GoSpeelText.ShowText = false;
            this.GoSpeelText.Size = new System.Drawing.Size(250, 49);
            this.GoSpeelText.TabIndex = 49;
            this.GoSpeelText.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.GoSpeelText.Watermark = "";
            // 
            // uiLabel8
            // 
            this.uiLabel8.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel8.Location = new System.Drawing.Point(24, 364);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(112, 49);
            this.uiLabel8.TabIndex = 48;
            this.uiLabel8.Text = "保压时间:";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PressTime
            // 
            this.PressTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PressTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PressTime.Location = new System.Drawing.Point(149, 364);
            this.PressTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PressTime.MinimumSize = new System.Drawing.Size(1, 16);
            this.PressTime.Name = "PressTime";
            this.PressTime.Padding = new System.Windows.Forms.Padding(5);
            this.PressTime.ShowText = false;
            this.PressTime.Size = new System.Drawing.Size(250, 49);
            this.PressTime.TabIndex = 47;
            this.PressTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.PressTime.Watermark = "";
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Location = new System.Drawing.Point(305, 562);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(170, 68);
            this.uiButton2.TabIndex = 46;
            this.uiButton2.Text = "停止";
            this.uiButton2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Location = new System.Drawing.Point(66, 562);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(170, 68);
            this.uiButton1.TabIndex = 45;
            this.uiButton1.Text = "启动";
            this.uiButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // uiLabel4
            // 
            this.uiLabel4.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(24, 305);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(112, 49);
            this.uiLabel4.TabIndex = 44;
            this.uiLabel4.Text = "变速位置:";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LocationText
            // 
            this.LocationText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LocationText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LocationText.Location = new System.Drawing.Point(149, 305);
            this.LocationText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LocationText.MinimumSize = new System.Drawing.Size(1, 16);
            this.LocationText.Name = "LocationText";
            this.LocationText.Padding = new System.Windows.Forms.Padding(5);
            this.LocationText.ShowText = false;
            this.LocationText.Size = new System.Drawing.Size(250, 49);
            this.LocationText.TabIndex = 43;
            this.LocationText.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.LocationText.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(21, 246);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(112, 49);
            this.uiLabel1.TabIndex = 38;
            this.uiLabel1.Text = "压力设定:";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PressureText
            // 
            this.PressureText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PressureText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PressureText.Location = new System.Drawing.Point(149, 246);
            this.PressureText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PressureText.MinimumSize = new System.Drawing.Size(1, 16);
            this.PressureText.Name = "PressureText";
            this.PressureText.Padding = new System.Windows.Forms.Padding(5);
            this.PressureText.ShowText = false;
            this.PressureText.Size = new System.Drawing.Size(250, 49);
            this.PressureText.TabIndex = 37;
            this.PressureText.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.PressureText.Watermark = "";
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiButton9);
            this.uiPanel2.Controls.Add(this.uiButton8);
            this.uiPanel2.Controls.Add(this.uiButton7);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(0, 706);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(1669, 80);
            this.uiPanel2.TabIndex = 40;
            this.uiPanel2.Text = "uiPanel2";
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiButton9
            // 
            this.uiButton9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton9.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton9.Location = new System.Drawing.Point(778, 15);
            this.uiButton9.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.uiButton9.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton9.Name = "uiButton9";
            this.uiButton9.Size = new System.Drawing.Size(178, 53);
            this.uiButton9.TabIndex = 6;
            this.uiButton9.Text = "回零点";
            this.uiButton9.TipsFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton9.Click += new System.EventHandler(this.uiButton9_Click);
            // 
            // uiButton8
            // 
            this.uiButton8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton8.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton8.Location = new System.Drawing.Point(44, 15);
            this.uiButton8.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.uiButton8.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton8.Name = "uiButton8";
            this.uiButton8.Size = new System.Drawing.Size(178, 53);
            this.uiButton8.TabIndex = 5;
            this.uiButton8.Text = "复位报警";
            this.uiButton8.TipsFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton8.Click += new System.EventHandler(this.uiButton8_Click);
            // 
            // uiButton7
            // 
            this.uiButton7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton7.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton7.Location = new System.Drawing.Point(412, 15);
            this.uiButton7.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.uiButton7.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton7.Name = "uiButton7";
            this.uiButton7.Size = new System.Drawing.Size(178, 53);
            this.uiButton7.TabIndex = 4;
            this.uiButton7.Text = "校正置零";
            this.uiButton7.TipsFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton7.Click += new System.EventHandler(this.uiButton7_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HydropPressureMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiPanel1);
            this.Name = "HydropPressureMode";
            this.Size = new System.Drawing.Size(1669, 786);
            this.Load += new System.EventHandler(this.HydropPressureMode_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.uiCheckBoxGroup1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UICheckBoxGroup uiCheckBoxGroup1;
        private Sunny.UI.UILabel uiLabel14;
        private Sunny.UI.UILabel uiLabel13;
        private Sunny.UI.UILabel uiLabel12;
        private Sunny.UI.UILabel uiLabel11;
        private Sunny.UI.UILabel uiLabel10;
        private Sunny.UI.UILabel uiLabel9;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UITextBox CurrenTimeText;
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UITextBox GoSpeelText;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UITextBox PressTime;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox LocationText;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UITextBox Speel2;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox Speel1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox PressureText;
        private System.Windows.Forms.Timer timer1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIButton uiButton9;
        private Sunny.UI.UIButton uiButton8;
        private Sunny.UI.UIButton uiButton7;
        private Sunny.UI.UIPanel uiPanel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Sunny.UI.UILabel uiLabel15;
        private Sunny.UI.UILabel uiLabel16;
        private Sunny.UI.UITextBox Vacuometertext;
    }
}

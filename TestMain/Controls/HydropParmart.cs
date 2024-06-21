using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestMain.Model;

namespace TestMain.Controls
{
    public partial class HydropParmart : Form
    {
        XmlHandler<HydropressModel> xmlFileManager;
        public HydropParmart()
        {
            InitializeComponent();
        }
        public void XmlWriteDateAnalysis()
        {
            try
            {
                // 写入数据到 XML 文件
               // bool res = xmlFileManager.WriteToFile(DataAnalysisDefault());
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void XmlReadyDateAnalysis()
        {
            HydropressModel dataFromFile = xmlFileManager.ReadFromFile();
            foreach (var item in dataFromFile.Pressurestart)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    qssd.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    qswz.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    qsyl.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureOperation)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    yxsd.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    yxwz.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    yxyl.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureClose)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    bhsd.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    bhwz.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    bhyl.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureMoldingFront)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    cxqsd.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    cxqwz.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    cxqyl.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureMolding)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    cxsd.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    cxwz.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    cxyl.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureOpenmould1)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    kmsd1.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    kmwz1.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    kmyl1.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureOpenmould2)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    kmsd2.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    kmwz2.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    kmyl2.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureOpenmould3)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    kmsd3.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    kmwz3.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    kmyl3.Value = Press;
                }
            }
            foreach (var item in dataFromFile.PressureOpenmould4)
            {
                if (int.TryParse(item.Spleed, out int Splee))
                {
                    kmsd4.Value = Splee;
                }
                if (int.TryParse(item.Location, out int Loca))
                {
                    kmwz4.Value = Loca;
                }
                if (int.TryParse(item.Pressure, out int Press))
                {
                    kmyl4.Value = Press;
                }
            }
            if (int.TryParse(dataFromFile.TimeAcc, out int Tacc))   //加速时间
            {
                nud_Tacc.Value = Tacc;
            }
            if (int.TryParse(dataFromFile.TimeDec, out int Tdec))  //减速时间
            {
                nud_Tdec.Value = Tdec;
            }
        }
        //修改
        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (xmlFileManager.UpdateFile(DataAnalysis()))
            {
                this.ShowSuccessNotifier("保存成功");
                Close();
            }
            else
            {
                this.ShowErrorNotifier("保存失败");
            }
        }
        public HydropressModel DataAnalysis()
        {
            var model = new HydropressModel
            {
                Id = 1,
                TimeAcc = nud_Tacc.Value.ToString(),
                TimeDec = nud_Tdec.Value.ToString(),
                Pressurestart = new List<Start>
                {
                    new Start { Location = qswz.Value.ToString(), Spleed = qssd.Value.ToString(), Pressure = qsyl.Value.ToString()}
                },
                PressureOperation = new List<Operation>
                {
                    new Operation { Location = yxwz.Value.ToString(), Spleed = yxsd.Value.ToString(), Pressure = yxyl.Value.ToString()}
                },
                PressureClose = new List<Close>
                {
                    new Close { Location = bhwz.Value.ToString(), Spleed = bhsd.Value.ToString(), Pressure = bhyl.Value.ToString()}
                },
                PressureMoldingFront = new List<MoldingFront>
                {
                    new MoldingFront { Location = cxqwz.Value.ToString(), Spleed = cxqsd.Value.ToString(), Pressure = cxqyl.Value.ToString()}
                },
                PressureMolding = new List<Molding>
                {
                    new Molding { Location = cxwz.Value.ToString(), Spleed = cxsd.Value.ToString(), Pressure = cxyl.Value.ToString()}
                },
                PressureOpenmould1 = new List<Openmould1>
                {
                    new Openmould1 { Location = kmwz1.Value.ToString(), Spleed = kmsd1.Value.ToString(), Pressure = kmyl1.Value.ToString()}
                },
                PressureOpenmould2 = new List<Openmould2>
                {
                    new Openmould2 { Location = kmwz2.Value.ToString(), Spleed = kmyl2.Value.ToString(), Pressure = kmyl2.Value.ToString()}
                },
                PressureOpenmould3 = new List<Openmould3>
                {
                    new Openmould3 { Location = kmwz3.Value.ToString(), Spleed = kmyl3.Value.ToString(), Pressure = kmyl3.Value.ToString()}
                },
                PressureOpenmould4 = new List<Openmould4>
                {
                    new Openmould4 { Location = kmwz4.Value.ToString(), Spleed = kmyl3.Value.ToString(), Pressure = kmyl4.Value.ToString()}
                }
            };
            return model;
        }
        public HydropressModel DataAnalysisDefault()
        {
            var model = new HydropressModel
            {
                Id = 1,
                TimeAcc = "2000",
                TimeDec = "1",
                Pressurestart = new List<Start>
                {
                    new Start { Location = "1", Spleed = "1", Pressure = "1"}
                },
                PressureOperation = new List<Operation>
                {
                    new Operation  { Location = "2", Spleed = "2", Pressure = "2"}
                },
                PressureClose = new List<Close>
                {
                    new Close  { Location = "3", Spleed = "3", Pressure = "3"}
                },
                PressureMoldingFront = new List<MoldingFront>
                {
                    new MoldingFront  { Location = "4", Spleed = "4", Pressure = "4"}
                },
                PressureMolding = new List<Molding>
                {
                    new Molding  { Location = "5", Spleed = "5", Pressure = "5"}
                },
                PressureOpenmould1 = new List<Openmould1>
                {
                    new Openmould1 { Location = "6", Spleed = "6", Pressure = "6"}
                },
                PressureOpenmould2 = new List<Openmould2>
                {
                    new Openmould2  { Location = "7", Spleed = "7", Pressure = "7"}
                },
                PressureOpenmould3 = new List<Openmould3>
                {
                    new Openmould3  { Location = "8", Spleed = "8", Pressure = "8"}
                },
                PressureOpenmould4 = new List<Openmould4>
                {
                    new Openmould4  { Location = "9", Spleed = "9", Pressure = "9"}
                }
            };
            return model;
        }
        private void uiButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void HydropParmart_Load_1(object sender, EventArgs e)
        {
            xmlFileManager = new XmlHandler<HydropressModel>();
            XmlReadyDateAnalysis();
        }
    }
}

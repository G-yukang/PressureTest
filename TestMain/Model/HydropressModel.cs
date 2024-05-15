using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMain.Model
{
    public class HydropressModel
    {
        public int Id { get; set; }
        //加速时间
        public string TimeAcc { get; set; }
        //减速时间
        public string TimeDec { get; set; }
        //开始
        public List<Start> Pressurestart { get; set; } = new List<Start>();

        // 运行
        public List<Operation> PressureOperation { get; set; } = new List<Operation>();

        // 闭合
        public List<Close> PressureClose { get; set; } = new List<Close>();

        // 成型前
        public List<MoldingFront> PressureMoldingFront { get; set; } = new List<MoldingFront>();

        // 成型
        public List<Molding> PressureMolding { get; set; } = new List<Molding>();

        // 开模1
        public List<Openmould1> PressureOpenmould1 { get; set; } = new List<Openmould1>();

        // 开模2
        public List<Openmould2> PressureOpenmould2 { get; set; } = new List<Openmould2>();

        // 开模3
        public List<Openmould3> PressureOpenmould3 { get; set; } = new List<Openmould3>();

        // 开模4
        public List<Openmould4> PressureOpenmould4 { get; set; } = new List<Openmould4>();

    }
    public class Start
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class Operation
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class Close
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class MoldingFront
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class Molding
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }  
    public class Openmould1
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class Openmould2
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class Openmould3
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
    public class Openmould4
    {
        public string Location { get; set; }
        public string Spleed { get; set; }
        public string Pressure { get; set; }
    }
}

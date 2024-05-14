using System;
using TestMain.Interfaces;
using TestMain.Model;

namespace TestMain.BLL
{
    internal class HydropressBLL
    {
        HydropressDAL HydropressDAL = new HydropressDAL();
        public void HydropressBLLUpdate(HydropressModel hydropressModel)
        {
            var result = HydropressDAL.HydropressUpdate(hydropressModel);
        }
        public void HydropressBLLInsert(HydropressModel hydropressModel)
        {
            var result = HydropressDAL.HydropressInsert(hydropressModel);
        }
        public bool HydropressBLLSelect()
        {
            var result = HydropressDAL.HydropressSelect();
            if (result != null) { return true; }
            else { return false; }
        }
    }
}

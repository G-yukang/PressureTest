using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestMain.Controls;
using TestMain.Model;

namespace TestMain.Common
{
    public class INIHandler
    {
        private string filePath;

        public INIHandler(string path)
        {
            filePath = path;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern long WritePrivateProfileString(
            string section, string key, string value, string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue, StringBuilder returnValue, int size, string filePath);

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
        }

        public string Read(string section, string key, string defaultValue = "")
        {
            var returnValue = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, returnValue, returnValue.Capacity, filePath);
            return returnValue.ToString();
        }
    }
}

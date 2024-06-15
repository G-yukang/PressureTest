using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public string ReadValue(string section, string key)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        List<string> lines = new List<string>(File.ReadAllLines(filePath));
                        string searchKey = "[" + section + "]";
                        int sectionIndex = lines.FindIndex(line => line.Contains(searchKey));

                        if (sectionIndex != -1)
                        {
                            string keySearch = key + "=";
                            int keyIndex = lines.FindIndex(sectionIndex + 1, line => line.Trim().StartsWith(keySearch));

                            if (keyIndex != -1)
                            {
                                return lines[keyIndex].Substring(keySearch.Length).Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
        public void WriteValue(string section, string key, string value)
        {
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            string searchKey = "[" + section + "]";
            int sectionIndex = lines.FindIndex(line => line.Contains(searchKey));

            if (sectionIndex == -1)
            {
                lines.Add("[" + section + "]");
                sectionIndex = lines.Count - 1;
            }

            string keySearch = key + "=";
            int keyIndex = lines.FindIndex(sectionIndex + 1, line => line.Trim().StartsWith(keySearch));

            if (keyIndex != -1)
            {
                lines[keyIndex] = key + "=" + value;
            }
            else
            {
                lines.Insert(sectionIndex + 1, key + "=" + value);
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}

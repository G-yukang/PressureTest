using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMain.Common
{
    public class AlarmLogger
    {
        private List<string> logs;
        private string logFilePath;
        public AlarmLogger()
        {
            logs = new List<string>();
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Alarm_logs.txt");

            // 如果文件不存在，则创建文件
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Dispose();
            }
        }
        public void LogAlarm(string alarmMessage)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"{timestamp}: {alarmMessage}";
            logs.Add(logEntry);
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }

        public void ClearLogs()
        {
            logs.Clear();
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
                File.Create(logFilePath).Dispose();
            }
        }

        public List<string> GetLogs()
        {
            if (File.Exists(logFilePath))
            {
                logs = new List<string>(File.ReadAllLines(logFilePath));
            }
            return new List<string>(logs);
        }
    }
    public class AlarmRecord
    {
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        public AlarmRecord(DateTime timestamp, string message)
        {
            Timestamp = timestamp;
            Message = message;
        }
    }
}

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

        private string logDirectory;
        public AlarmLogger()
        {
            logs = new List<string>();
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Alarm_logs.txt");
            logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\Alarm");
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

        public void SystemLogAlarm(string alarmMessage)
        {
            // 获取当前时间戳
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 创建日志条目
            string logEntry = $"{timestamp}: {alarmMessage}";
            // 将日志条目添加到列表
            logs.Add(logEntry);

            // 创建一个以今天日期命名的子目录
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string todayDirectory = Path.Combine(logDirectory, today);

            // 如果子目录不存在，则创建它
            if (!Directory.Exists(todayDirectory))
            {
                Directory.CreateDirectory(todayDirectory);
            }

            // 在今天的目录中创建或追加日志文件
            string logFilePath = Path.Combine(todayDirectory, today + "Alarm_logs.txt");
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
    }
}

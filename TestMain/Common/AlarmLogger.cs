using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMain.Common
{
    public class AlarmLogger
    {
        private List<AlarmRecord> _records;

        public AlarmLogger()
        {
            _records = new List<AlarmRecord>();
        }

        public void LogAlarm(string message)
        {
            var record = new AlarmRecord(DateTime.Now, message);
            _records.Add(record);
        }

        public void DisplayRecords(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear(); // 清空数据表格

            foreach (var record in _records)
            {
                dataGridView.Rows.Add(record.Timestamp, record.Message);
            }
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

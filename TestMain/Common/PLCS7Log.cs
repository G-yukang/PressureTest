using S7.Net;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMain.Common
{
    public class PLCS7Logic
    {
        #region 全局变量
        log4net.ILog log = log4net.LogManager.GetLogger("PlcMessageLogger");
        //(注意收发不能同时进行，要相隔50ms)
        public event Action<string> LogMessageEvent;

        public short rack = 0;
        public short slot = 1;
        private Plc m_Plc;

        //发送和读取都要获取这个锁以保证每条指令之间间隔50ms
        object sendReadLocker = new object();
        //每隔500ms执行一次读取操作的Timer
        private System.Timers.Timer readTimer = new System.Timers.Timer(500);
        private Task readTask;
        //用于重连的Timer,默认10秒调用一次
        private System.Timers.Timer reConnectTimer = new System.Timers.Timer(10000);

        Task plcConnectTask;

        private int reConnectCount = 0;
        private object reConnectLocker = new object();
        //读取时对Connected进行判断，发现Connected == false的计数
        private volatile int connectFalseCount = 0;

        object plcReCollectMethodLocker = new object();

        public event Action<bool[]> StateReceiveEvent;
        public event Action<float[]> PosReceiveEvent;

        public event Action StartEvent;
        public event Action FinishEvent;
        public event Action<bool> AutoKeySelectChanged;

        bool startPressed = false;
        bool finishPressed = false;
        //手动自动的选择（电柜上的手动和自动钥匙旋钮，默认是选中）
        bool autoKeySelected = true;

        private string PLCType = "1200";
        private string IPAddress = "127.0.0.1";
        #endregion

        public PLCS7Logic()
        {
            readTimer.Elapsed += ReadTimer_Elapsed;
            reConnectTimer.Elapsed += ReConnectTimer_Elapsed;
        }
        public bool Connected
        {
            get
            {
                if (m_Plc != null && m_Plc.IsConnected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region 连接与断线重连
        public bool Connect(string plcType, string ipAddress)
        {
            if (Connected) return true;

            reConnectPlc(plcType, ipAddress);
            return true;
        }
        /// <summary>
        /// PLC重连方法，初始化所有变量
        /// </summary>
        private void reConnectPlc(string plcType, string ipAddress)
        {
            if (!Connected)
            {
                lock (reConnectLocker)
                {
                    if (!Connected)
                    {
                        ClosePlc();
                        reConnectCount = 0;
                        ReConnectTimer_Elapsed(null, null);
                        reConnectTimer.Start();
                        System.Threading.Thread.Sleep(5000);
                    }
                }
            }
        }
        /// <summary>
        /// reConnectTimer的间隔稍微长一点，
        /// 避免上一个链接方法还没有执行完导致多次调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReConnectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            reConnectCount++;
            if (plcConnectTask != null && !plcConnectTask.IsCompleted) return;

            if (Connected) return;
            plcConnectTask = Task.Run(() => plcReCollectMethod());
        }
        /// <summary>
        /// 重连的具体方法
        /// </summary>
        private void plcReCollectMethod()
        {
            if (!Connected)
            {
                lock (plcReCollectMethodLocker)
                {
                    if (!Connected)
                    {
                        try
                        {
                            m_Plc = CreatePlcInstance(PLCType, IPAddress);
                            if (m_Plc == null) return;
                            m_Plc.Open();
                            if (m_Plc.IsConnected)
                            {
                                StartReadThread();
                                reConnectTimer.Stop();
                                reConnectCount = 0;
                                LogMessageEvent?.Invoke(DateTime.Now.ToString("HH:mm:ss") + "PLC Connect Success");
                            }
                            else
                            {
                                LogMessageEvent?.Invoke(DateTime.Now.ToString("HH:mm:ss") + "PLC Connect False");
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Info("plcReCollectMethod连接异常", ex);
                            LogMessageEvent?.Invoke(DateTime.Now.ToString("HH:mm:ss") + "PLC Connect False");
                        }
                    }
                }
            }
        }
        private Plc CreatePlcInstance(string plcType, string ipAddress)
        {
            switch (plcType)
            {
                case "1500":
                    return new Plc(CpuType.S71500, ipAddress, rack, slot);
                case "1200":
                    return new Plc(CpuType.S71200, ipAddress, rack, slot);
                case "200":
                    return new Plc(CpuType.S7200, ipAddress, rack, slot);
                default:
                    return null;
            }
        }
        /// <summary>
        /// 连接状态
        /// </summary>
        /// <returns></returns>
        public bool ConnectStatus()
        {
            return Connected;
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        public void Close()
        {
            try
            {
                if (m_Plc != null)
                {
                    m_Plc.Close();
                    m_Plc = null;
                }
                string msg_Log = DateTime.Now.ToString("HH:mm:ss") + " Close Success";
                LogMessageEvent?.Invoke(msg_Log);
            }
            catch (Exception)
            {
                string msg_Log = DateTime.Now.ToString("HH:mm:ss") + " Close Error";
                LogMessageEvent?.Invoke(msg_Log);
                Console.WriteLine("close error");
            }
        }
        private void ClosePlc()
        {
            if (m_Plc != null)
            {
                m_Plc.Close();
                m_Plc = null;
            }
        }
        #endregion

        #region 接口实现
        /// <summary>
        /// 新版本中要读取的Bool类型的点位
        /// 根据表格中的点位填写，注意添加顺序！
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<int[]> GetReadBoolAddress()
        {
            //27个bool点位,要根据实际读取到的点位确定列表初始化的个数，且顺序不能出错
            List<int[]> readBoolList = new List<int[]>();
            for (int i = 0; i < 27; i++)
                readBoolList.Add(new int[3]);

            readBoolList[0] = new int[3] { 201, 1, 2 };    //左后X轴上限位
            readBoolList[1] = new int[3] { 201, 1, 3 };    //左后X轴下限位
            readBoolList[2] = new int[3] { 201, 2, 0 };    //右后X轴上限位
            readBoolList[3] = new int[3] { 201, 2, 1 };    //右后X轴下限位

            readBoolList[4] = new int[3] { 201, 2, 5 };    //前左到位对射传感器
            readBoolList[5] = new int[3] { 201, 2, 6 };    //前右到位对射传感器
            readBoolList[6] = new int[3] { 201, 2, 7 };    //前左对中传感器
            readBoolList[7] = new int[3] { 201, 3, 0 };    //前右对中传感器
            readBoolList[8] = new int[3] { 201, 3, 1 };    //前对中缩回信号
            readBoolList[9] = new int[3] { 201, 3, 2 };    //后对中缩回信号

            readBoolList[10] = new int[3] { 201, 4, 1 };   //启动按钮
            readBoolList[11] = new int[3] { 201, 4, 2 };   //返回按钮

            readBoolList[12] = new int[3] { 201, 5, 0 };   //急停
            readBoolList[13] = new int[3] { 201, 5, 1 };   //手动
            readBoolList[14] = new int[3] { 201, 5, 2 };   //自动

            readBoolList[15] = new int[3] { 201, 5, 5 };   //开始测试按钮
            readBoolList[16] = new int[3] { 201, 5, 6 };   //结束测试按钮

            readBoolList[17] = new int[3] { 201, 8, 1 };   //右后X轴绝对移动完成位
            readBoolList[18] = new int[3] { 201, 9, 0 };   //左后X轴绝对移动完成位

            readBoolList[19] = new int[3] { 201, 10, 0 };   //左后X轴故障位
            readBoolList[20] = new int[3] { 201, 10, 1 };   //右后X轴故障位

            readBoolList[21] = new int[3] { 201, 11, 0 };   //伺服上电
            readBoolList[22] = new int[3] { 201, 11, 1 };   //伺服使能
            readBoolList[23] = new int[3] { 201, 11, 2 };   //故障复位按钮
            readBoolList[24] = new int[3] { 201, 11, 3 };   //回原点按钮
            readBoolList[25] = new int[3] { 201, 11, 4 };   //进气压力开关
            readBoolList[26] = new int[3] { 201, 11, 5 };   //原点位指示灯

            return readBoolList;
        }
        /// <summary>
        /// 新版本中要读取的Real类型的点位,
        /// 只有两个，一个是左后的X轴位置，一个是右后轴X的位置
        /// 根据表格中的点位填写，注意添加顺序！
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<int[]> GetReadRealAddress()
        {
            List<int[]> readRealList = new List<int[]>();
            for (int i = 0; i < 2; i++)
                readRealList.Add(new int[2]);

            readRealList[0] = new int[2] { 201, 70 };    //左后X轴绝对位置
            readRealList[1] = new int[2] { 201, 82 };    //右后X轴绝对位置

            return readRealList;
        }
        #endregion

        #region 定时读取
        //读取时发生错误或者异常的计数
        private volatile int ReadFalseCount = 0;
        /// <summary>
        /// 读取数据线程
        /// </summary>
        private void StartReadThread()
        {
            //注意防止StartReadThread方法被多次被调用，
            //保证每次只有一个readTask和readTimer
            if (readTask != null)
            {
                readTask = null;
            }
            if (readTimer != null)
            {
                readTimer.Stop();
            }
            readTimer.Start();
        }
        /// <summary>
        /// 启动读取计时器，并调用方法读取数据
        /// 在connectFalseCount大于5次后调用重连方法并关闭读取计数器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //500毫秒一次，大于等于5次认为网络断开
            if (ReadFalseCount >= 5)
            {
                log.Info("ReadTimer_Elapsed尝试重新连接plc：connectFalseCount：" + connectFalseCount);

                ReadFalseCount = 0;
                readTimer.Stop();
                return;
            }

            if (readTask != null && readTask.IsCompleted == false)
            {
                log.Info("ReadTimer_Elapsed上次动作未完成,跳过操作");

                return;
            }
            else
            {
                //正常读取
                readTask = new Task(new Action(readMethod));
                readTask.Start();
            }
        }
        private void readMethod()
        {
            if (!Connected)
            {
                log.Info("PLC未连接，无法获取数据！");
                string msg_Log = DateTime.Now.ToString("HH:mm:ss") + " PLC未连接，无法获取数据！ ";
                LogMessageEvent?.Invoke(msg_Log);

                ReadFalseCount++;
                return;
            }
            //一次性从PLC中读取需要的点位值
            List<int[]> bitToRead = GetReadBoolAddress();
            List<S7.Net.Types.DataItem> dataItems = new List<S7.Net.Types.DataItem>();
            for (int i = 0; i < bitToRead.Count; i++)
            {
                S7.Net.Types.DataItem dtItem = new S7.Net.Types.DataItem();
                dtItem.DataType = DataType.DataBlock;
                dtItem.DB = bitToRead[i][0];
                dtItem.StartByteAdr = bitToRead[i][1];
                dtItem.BitAdr = (byte)bitToRead[i][2];
                dtItem.Count = 1;
                dtItem.VarType = VarType.Bit;
                dataItems.Add(dtItem);
            }
            List<int[]> realToRead = GetReadRealAddress();
            for (int i = 0; i < realToRead.Count; i++)
            {
                S7.Net.Types.DataItem dtItemReal = new S7.Net.Types.DataItem();
                dtItemReal.DataType = DataType.DataBlock;
                dtItemReal.DB = realToRead[i][0];
                dtItemReal.StartByteAdr = realToRead[i][1];
                dtItemReal.Count = 1;
                dtItemReal.VarType = VarType.Real;
                dataItems.Add(dtItemReal);
            }

            bool[] resultArray = new bool[bitToRead.Count];
            float[] posResultArray = new float[realToRead.Count];

            lock (sendReadLocker)
            {
                try
                {
                    //每次收发间隔50ms，每次只能读取15个点位
                    List<S7.Net.Types.DataItem> dataItemsTemp = new List<S7.Net.Types.DataItem>();
                    for (int i = 0; i < (dataItems.Count / 15) + 1; i++)
                    {
                        for (int j = 0; j < 15 && j < dataItems.Count - 15 * i; j++)
                        {
                            dataItemsTemp.Add(dataItems[j + 15 * i]);
                        }
                        m_Plc.ReadMultipleVars(dataItemsTemp);
                        dataItemsTemp.Clear();
                        Thread.Sleep(50);
                    }

                    //取出bool读取返回结果
                    for (int i = 0; i < bitToRead.Count; i++)
                    {
                        resultArray[i] = dataItems[i].Value == null ? false : (bool)dataItems[i].Value;
                    }

                    //取出pos读取返回结果，注意核对各个参数取出的值是否正确
                    for (int i = bitToRead.Count; i < dataItems.Count; i++)
                    {
                        posResultArray[i - bitToRead.Count] = dataItems[i].Value == null ? -9999 : (float)dataItems[i].Value;
                    }
                }
                catch (Exception ex)
                {
                    string msg_Log = DateTime.Now.ToString("HH:mm:ss") + "PLC Read False";
                    LogMessageEvent?.Invoke(msg_Log);
                    log.Info("readMethod_ReadMultipleVars读取发生异常", ex);
                    //ReadFalseCount++;
                    return;
                }
            }
            StateReceiveEvent?.Invoke(resultArray);
            PosReceiveEvent?.Invoke(posResultArray);
            //记录读取的点位
            string reslog = DateTime.Now.ToString("HH:mm:ss") + " Rx " + string.Join(" ", resultArray);
            LogMessageEvent?.Invoke(reslog);
            log.Info("readMethod读取到返回值： " + reslog);

            //注意后面的方法要切换线程才能显示在主界面上！
            AnalyseReceiveInfo(resultArray);
        }
        #endregion

        #region 触发事件
        /// <summary>
        /// 分析读取结果,
        /// 根据读取的结果触发对应的事件
        /// </summary>
        /// <param name="datas"></param>
        private void AnalyseReceiveInfo(bool[] datas)
        {
            try
            {
                //开始测试按钮（台架上的启动按钮）
                if (datas[15] == true)
                {
                    if (!startPressed)
                    {
                        StartEvent?.Invoke();
                        startPressed = true;
                    }
                }
                if (datas[15] != true)
                {
                    startPressed = false;
                }
                //台架上的结束测试按钮
                if (datas[16] == true)
                {
                    if (!finishPressed)
                    {
                        FinishEvent?.Invoke();
                        finishPressed = true;
                    }
                }
                if (datas[16] != true)
                {
                    finishPressed = false;
                }
                //手动自动的选择（电柜上的钥匙旋钮，默认选中）
                if (datas[14] == true)
                {
                    if (autoKeySelected == false)
                    {
                        AutoKeySelectChanged?.Invoke(true);
                        autoKeySelected = true;
                    }
                }
                else
                {
                    if (autoKeySelected == true)
                    {
                        AutoKeySelectChanged?.Invoke(false);
                        autoKeySelected = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 发指令给PLC的辅助方法
        /// <summary>
        /// 所有发送在这个方法里完成，以确保获取读写锁sendReadLocker，保证收发间隔50ms
        /// </summary>
        /// <param name="sendAction"></param>
        private void ClientSendPLCDatas(Action sendAction)
        {
            try
            {
                if (!Connected)
                {
                    string msg_Log = DateTime.Now.ToString("HH:mm:ss") + " PLC未连接，无法发送，尝试重连！ ";
                    LogMessageEvent?.Invoke(msg_Log);
                    return;
                }
                Task sendTask = new Task(() =>
                {
                    lock (sendReadLocker)
                    {
                        //间隔50ms
                        sendAction();
                        Thread.Sleep(50);
                    }
                });
                sendTask.ContinueWith((t) =>
                {
                    if (t.IsCompleted && t.IsFaulted == false)
                    {
                        string msg_Log1 = DateTime.Now.ToString("HH:mm:ss") + " 发送成功！ ";
                        //注意界面线程切换
                        LogMessageEvent?.Invoke(msg_Log1);
                    }
                    else
                    {
                        string msg_Log1 = DateTime.Now.ToString("HH:mm:ss") + " 发送失败！ ";
                        //注意界面线程切换
                        LogMessageEvent?.Invoke(msg_Log1);
                    }
                });
                sendTask.Start();

            }
            catch (Exception e)
            {
                string msg_Log = DateTime.Now.ToString("HH:mm:ss") + " 发送异常！ ";
                LogMessageEvent?.Invoke(msg_Log);
                log.Info("ClientSendPLCDatas:" + msg_Log, e);
                Thread.Sleep(50);
            }
        }
        #endregion
    }
}

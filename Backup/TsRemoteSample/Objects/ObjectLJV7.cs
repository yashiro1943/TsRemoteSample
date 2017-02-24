using System;
using System.Collections.Generic;
//for MessageBox
using System.Windows.Forms;
//for Marshal
using System.Runtime.InteropServices;
//for Thread
using System.Threading;
//for StreamWriter
using System.IO;

using System.Drawing;
using System.Drawing.Imaging;

namespace KeyenceTools
{
    class ObjectLJV7
    {
        #region Constant
        /// <summary>MAX value for the amount of data in 1 profile</summary>
        public const int MAX_PROFILE_COUNT = 3200;

        /// <summary>Device ID (fixed to 0)</summary>
        public const int DEVICE_ID = 0;

        public class STATUS
        {
            public const int INI = 0x01;
            public const int CONNECTED = 0x02;
            public const int STARTHIGHTSPEED = 0x04;
            public const int CURRENTIMAGE = 0x08;
            public const int CommunicationInitalize = 0x10;
            public const int HighSpeedDataCommunication = 0x20;
        }

        public static int ProgressCounter = 0;
        public static int FailCounter = 0;
        #endregion

        #region Field
        /// <summary>High-speed communication callback function</summary>
        private HightSpeedDataCallBack _callback;

        /// <summary>Ethernet communication settings</summary>
        private LJV7IF_ETHERNET_CONFIG _ethernetConfig;

        //收到資料後呼叫
        public delegate void CallbackReceiveData(int[] buffer, uint profileSize, uint count);
        public event CallbackReceiveData _receiveback;
        //收集完資料後呼叫
        public delegate void CallbackCollectData(FrameData_LJV7 obj);
        public event CallbackCollectData _collectback;
        //發生問題後呼叫
        public delegate void CallbackError(Object sender, int message);
        public event CallbackError _errorback;

        public int status = ObjectLJV7.STATUS.INI;

        public Object ReceivedFIFOLock = new Object();
        public List<int[]> _ReceivedFIFO = new List<int[]>();

        #endregion

        public ObjectLJV7()
        {
            _callback = new HightSpeedDataCallBack(ReceiveData);
        }

        #region Detection Target
        FrameData_LJV7 _FrameData = null;
        public void getLaserReveiced()
        {
            int tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus == 0)
            {
                Console.WriteLine(DateTime.Now.TimeOfDay + ": getLaserReveiced-尚未開啟高速模式");
                return;
            }
            if (_FrameData != null)
            {
                Console.WriteLine(DateTime.Now.TimeOfDay + ": 遺失資料(上一筆資料尚未收畢) " + ProgressCounter);
                return;
            }

            Console.WriteLine(DateTime.Now.TimeOfDay + ": 開始取資料 " + ProgressCounter);
            _FrameData = new FrameData_LJV7();
            buffer_set.Clear();
        }
        #endregion

        #region LJV7IF Hander
        /// <summary>
        /// High-speed communication callback process
        /// </summary>
        /// <param name="buffer">Pointer to beginning of received data</param>
        /// 資料格式:
        ///	LJV7IF_PROFILE_HEADER(uint*6) + A(int*800) + [B(int*800)] + LJV7IF_PROFILE_FOOTER(uint*1)
        ///	0x80000000 = Invalid data(This value is output when the peak could not be detected.)
        ///	0x80000002 = Dead zone data(This value is output when the data is located in a dead zone. This value is only output when dead zone processing is enabled.)
        ///	(X方向, Z方向, 取樣頻率): (FULL, SMALL, 8kHZ)=8000組/秒
        ///	每一組資料的最後一個距離都是0?
        /// <param name="size">Byte size per 1 profile in received data</param>
        /// <param name="count">Profile count</param>
        /// <param name="notify">Exited or not</param>
        /// <param name="user">Information passed when high-speed data communication was initialized (thread ID)</param>
        /// 距離為越小越遠(由正至負)
        /// 若為外部觸發，每一次觸發的第一筆，int[0]=0x42，且全為無效資料
        private List<int> buffer_set = new List<int>();
        private void ReceiveData(IntPtr buffer, uint size, uint count, uint notify, uint user)
        {
            uint profileSize = size / sizeof(int);
            if (FrameData_LJV7._PROFILESIZE != profileSize) FrameData_LJV7._PROFILESIZE = profileSize;

            //Console.WriteLine(DateTime.Now.TimeOfDay + ": " + profileSize + "," + count + ", " + notify + ", " + user + ", " + buffer_set.Count);

            //notify:
            //p22: 8.3.1.1 dwNotify parameter
            //bit 0: Continuous send was stopped (stop by command)
            //bit 1~3: Continuous send was stopped (automatic stop)
            //bit 8: Send interrupted by clear memory
            //bit 16, 31: Finished sending the batch measurement amount of data
            int t = Convert.ToInt32(notify) & 0x0E;
            //自動停止時要進行自動連線
            if (t > 0 && _errorback != null) _errorback(this, Convert.ToInt32(notify));
            if (count == 0 || notify > 0) return;

            //目前只開放高速模式和即時影像，及資料尚未收集完畢
            int tmpStatus = status & (ObjectLJV7.STATUS.STARTHIGHTSPEED | ObjectLJV7.STATUS.CURRENTIMAGE);
            if (tmpStatus == 0) return;

            Marshal.Copy(buffer, buffer_tmp, 0, Convert.ToInt32(profileSize * count));
            //callback to caller
            if (_receiveback != null)
            {
                //即時模式，回傳並進行繪製即時圖片
                _receiveback(buffer_tmp, profileSize, count);
            }

            //具有高速模式，才需要組合資料，否則直接離開
            tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus > 0) buffer_set.AddRange(buffer_tmp);
            else return;

            tmpStatus = status & ObjectLJV7.STATUS.CURRENTIMAGE;
            if (tmpStatus == 0)
            {
                //純高速測試模式，檢查資料起頭
                int headindex = -1;

                /*if (buffer_tmp[0] == 0x42)
                {
                    Console.WriteLine("資料起頭");
                    buffer_set.Clear();
                }*/

                //*
                for (int i = 0; i < count; i++)
                {
                    if (buffer_tmp[i * profileSize] == 0x42)
                    {
                        headindex = i;
                        Console.WriteLine("資料起頭: " + headindex);
                        //buffer_set.Clear();
                        break;
                    }
                }//*/

                if (headindex > 0)
                {
                    //若目前資料有資料起頭，移除起頭前的資料
                    //有效資料數: buffer_tmp.Length - headindex
                    buffer_set.RemoveRange(0, buffer_set.Count - (buffer_tmp.Length - headindex));
                }
            }

            //資料未收集完畢，直接離開
            if (buffer_set.Count < FrameData_LJV7._DATASETSIZE * FrameData_LJV7._PROFILESIZE) return;
            else if (buffer_set.Count > FrameData_LJV7._DATASETSIZE * FrameData_LJV7._PROFILESIZE)
            {
                //超出，則刪除前面不需測試的資料
                buffer_set.RemoveRange(0, Convert.ToInt32(buffer_set.Count - FrameData_LJV7._DATASETSIZE * FrameData_LJV7._PROFILESIZE));
            }
            //buffer_set.Count == ErrorDetector._DATASETSIZE * ErrorDetector._PROFILESIZE

            tmpStatus = status & ObjectLJV7.STATUS.CURRENTIMAGE;
            if (tmpStatus == 0)
            {
                //純高速測試模式，檢測收到的單筆資料
                lock (ReceivedFIFOLock)
                {
                    //複製buffer_tmp至FIFO
                    _ReceivedFIFO.Add(buffer_set.ToArray());
                    buffer_set.Clear();
                }
                return;
            }

            //即時影像模式時，若啟動高速模式，可收集資料並進行測試(可透過PLC觸發, getLaserReveiced)
            if (_FrameData == null) return;
            _FrameData.buffer = buffer_set.ToArray();
            _FrameData.count = FrameData_LJV7._DATASETSIZE;
            if (_collectback != null) _collectback(_FrameData);
            _FrameData = null;
            buffer_set.Clear();

        }
        #endregion

        #region LJV7IF Operations
        //連線流程:
        //Device Open
        //1. LJV7IF_Initialize
        //2. LJV7IF_EthernetOpen

        //HighSpeed: 3->4->5->6(->4->5->6)->7
        //3. LJV7IF_HighSpeedDataEthernetCommunicationInitalize
        //4. LJV7IF_PreStartHighSpeedDataCommunication
        //5. LJV7IF_StartHighSpeedDataCommunication
        //6. LJV7IF_StopHighSpeedDataCommunication
        //7. LJV7IF_HighSpeedDataCommunicationFinalize

        //Device Close
        //8. LJV7IF_CommClose
        //9. LJV7IF_Finalize

        ///// <summary>
        /// The check of a return code 
        /// </summary>
        /// <param name="rc">Return code </param>
        /// <returns>OK or not</returns>
        /// <remarks>If not OK, return false after displaying message.</remarks>
        public bool CheckReturnCode(Rc rc)
        {
            if (rc == Rc.Ok) return true;
            int irc = Convert.ToInt32(rc);
            if (irc == 0x1000)
            {
                MessageBox.Show(string.Format("Error: 連線失敗"), "連線錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (irc == 0x1001)
            {
                MessageBox.Show(string.Format("Error:  未連線"), "連線錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            MessageBox.Show(string.Format("Error: 0x{0,8:x}", rc), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public bool doInitialize(byte[] IpAddr, ushort PortNo)
        {
            LJV7IF_ETHERNET_CONFIG tmpEthernet = new LJV7IF_ETHERNET_CONFIG();
            if (_ethernetConfig.abyIpAddress != null)
            {
                MessageBox.Show("儀器已連線");
                return false;
            }

            Rc rc = Rc.Ok;

            // Initialize the DLL.
            rc = (Rc)NativeMethods.LJV7IF_Initialize();
            if (!CheckReturnCode(rc)) return false;

            // Open communication path.
            // Create Ethernet communication settings.
            try
            {
                tmpEthernet.abyIpAddress = IpAddr;
                tmpEthernet.wPortNo = PortNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            rc = (Rc)NativeMethods.LJV7IF_EthernetOpen(DEVICE_ID, ref tmpEthernet);
            if (!CheckReturnCode(rc)) return false;
            status = ObjectLJV7.STATUS.CONNECTED;
            _ethernetConfig = tmpEthernet;
            return true;
        }

        public bool doFinalize()
        {
            if (_ethernetConfig.abyIpAddress == null)
            {
                MessageBox.Show("儀器未連線");
                return false;
            }

            int tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus > 0) StopHighSpeed();

            tmpStatus = status & ObjectLJV7.STATUS.CURRENTIMAGE;
            if (tmpStatus > 0) StopCurrentImage();

            Rc rc = Rc.Ok;
            // Close communication.
            rc = (Rc)NativeMethods.LJV7IF_CommClose(DEVICE_ID);
            if (!CheckReturnCode(rc)) return false;

            // Finalize the DLL.
            status = ObjectLJV7.STATUS.INI;
            rc = (Rc)NativeMethods.LJV7IF_Finalize();
            if (!CheckReturnCode(rc)) return false;
            _ethernetConfig.abyIpAddress = null;
            return true;
        }

        //frequency: 取多少樣本後呼叫ReceiveData
        private uint _frequency = 0;
        private int[] buffer_tmp = null;
        public bool StartGetValue(ushort highSpeedPort, byte bySendPos, uint frequency)
        {
            if (HighSpeedDataEthernetCommunicationInitalize(highSpeedPort, frequency) == false)
            {
                return false;
            }
            if (StartHighSpeedDataCommunication(bySendPos) == false)
            {
                return false;
            }
            return true;
        }

        public bool StopGetValue()
        {
            StopHighSpeedDataCommunication();
            HighSpeedDataCommunicationFinalize();
            return true;
        }

        public bool HighSpeedDataEthernetCommunicationInitalize(ushort highSpeedPort, uint frequency)
        {
            if ((status & ObjectLJV7.STATUS.CommunicationInitalize) > 0)
            {
                MessageBox.Show("連線已初始化");
                return false;
            }
            if ((status & ObjectLJV7.STATUS.CONNECTED) == 0)
            {
                MessageBox.Show("儀器未初始化");
                return false;
            }

            //Console.WriteLine(DateTime.Now.TimeOfDay + ": HighSpeedDataEthernetCommunicationInitalize");
            buffer_set.Clear();

            if (SetContinue(TriggerMode.Continuous) == false) return false;

            if (_frequency != frequency || buffer_tmp == null)
            {
                buffer_tmp = null;
                _frequency = frequency;
                buffer_tmp = new int[FrameData_LJV7._PROFILESIZE * _frequency];
            }

            // Initialize data.
            Rc rc = Rc.Ok;

            // Initialize the high-speed communication path.
            try
            {
                uint threadId = (uint)DEVICE_ID;

                // Initialize Ethernet high-speed data communication
                rc = (Rc)NativeMethods.LJV7IF_HighSpeedDataEthernetCommunicationInitalize(
                        DEVICE_ID, ref _ethernetConfig,
                        highSpeedPort, _callback, frequency, threadId);

                if (!CheckReturnCode(rc)) return false;
                //Send start position. 
                //0: From previous send complete position (from oldest data if 1st time),
                //1: From oldest data (reacquire, 重新取得，可能非現在資料), 
                //2: From next data
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            status |= ObjectLJV7.STATUS.CommunicationInitalize;
            return true;
        }

        public bool StartHighSpeedDataCommunication(byte bySendPos)
        {
            if ((status & ObjectLJV7.STATUS.HighSpeedDataCommunication) > 0)
            {
                MessageBox.Show("連線已建立");
                return false;
            }
            if ((status & ObjectLJV7.STATUS.CommunicationInitalize) == 0)
            {
                MessageBox.Show("連線未初始化");
                return false;
            }

            //Console.WriteLine(DateTime.Now.TimeOfDay + ": StartHighSpeedDataCommunication");
            buffer_set.Clear();

            // Initialize data.
            Rc rc = Rc.Ok;

            // High-speed data communication start prep
            LJV7IF_HIGH_SPEED_PRE_START_REQ req = new LJV7IF_HIGH_SPEED_PRE_START_REQ();
            req.bySendPos = bySendPos;

            LJV7IF_PROFILE_INFO profileInfo = new LJV7IF_PROFILE_INFO();
            rc = (Rc)NativeMethods.LJV7IF_PreStartHighSpeedDataCommunication(DEVICE_ID, ref req, ref profileInfo);
            if (!CheckReturnCode(rc)) return false;

            // Start high-speed data communication.
            rc = (Rc)NativeMethods.LJV7IF_StartHighSpeedDataCommunication(DEVICE_ID);
            if (!CheckReturnCode(rc)) return false;

            status |= ObjectLJV7.STATUS.HighSpeedDataCommunication;
            return true;
        }

        public bool StopHighSpeedDataCommunication()
        {
            if ((status & ObjectLJV7.STATUS.HighSpeedDataCommunication) == 0)
            {
                MessageBox.Show("未連線");
                return false;
            }

            buffer_set.Clear();

            //先將機器結束
            // Stop high-speed data communication.
            Rc rc = (Rc)NativeMethods.LJV7IF_StopHighSpeedDataCommunication(DEVICE_ID);
            CheckReturnCode(rc);
            status &= ~ObjectLJV7.STATUS.HighSpeedDataCommunication;
            return true;
        }
        public bool HighSpeedDataCommunicationFinalize()
        {
            if ((status & ObjectLJV7.STATUS.CommunicationInitalize) == 0)
            {
                MessageBox.Show("未連線");
                return false;
            }

            if ((status & ObjectLJV7.STATUS.HighSpeedDataCommunication) > 0)
            {
                StopHighSpeedDataCommunication();
            }

            Rc rc = (Rc)NativeMethods.LJV7IF_HighSpeedDataCommunicationFinalize(DEVICE_ID);
            CheckReturnCode(rc);

            status &= ~ObjectLJV7.STATUS.CommunicationInitalize;
            return true;
        }

        public bool StartHighSpeed(ushort highSpeedPort, byte bySendPos, uint frequency)
        {
            int tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus > 0)
            {
                MessageBox.Show("高速模式已開啟");
                return false;
            }

            //未開啟即時影像才需要開啟機器
            tmpStatus = status & ObjectLJV7.STATUS.CURRENTIMAGE;
            if (tmpStatus == 0)
            {
                if (SetContinue(TriggerMode.External) == false) return false;
                if (StartGetValue(highSpeedPort, bySendPos, frequency) == false) return false;
            }
            status |= ObjectLJV7.STATUS.STARTHIGHTSPEED;
            return true;
        }

        public bool StopHighSpeed()
        {
            int tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus == 0)
            {
                Console.WriteLine("高速模式尚未開啟");
                return false;
            }

            status &= ~ObjectLJV7.STATUS.STARTHIGHTSPEED;

            tmpStatus = status & (ObjectLJV7.STATUS.CURRENTIMAGE | ObjectLJV7.STATUS.STARTHIGHTSPEED);
            //動作皆停止才需關閉接收
            if (tmpStatus == 0) return StopGetValue();

            return true;
        }

        public bool StartCurrentImage(ushort highSpeedPort, byte bySendPos, uint frequency)
        {
            int tmpStatus = status & ObjectLJV7.STATUS.CURRENTIMAGE;
            if (tmpStatus > 0)
            {
                MessageBox.Show("即時影像模式已開啟");
                return false;
            }
            //未開高速模式才需要開啟機器
            tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus == 0)
            {
                if (SetContinue(TriggerMode.Continuous) == false) return false;
                if (StartGetValue(highSpeedPort, bySendPos, frequency) == false) return false;
            }
            else
            {
                buffer_set.Clear();
                _FrameData = null;
                MessageBox.Show("高速模式下，無法開啟即時影像模式");
                return false;
            }

            status |= ObjectLJV7.STATUS.CURRENTIMAGE;
            return true;
        }

        public bool StopCurrentImage()
        {
            int tmpStatus = status & ObjectLJV7.STATUS.CURRENTIMAGE;
            buffer_set.Clear();
            _FrameData = null;

            if (tmpStatus == 0)
            {
                MessageBox.Show("即時影像模式尚未開啟");
                return false;
            }
            //未開高速模式才需要關閉機器
            tmpStatus = status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus == 0)
            {
                if (StopGetValue() == false) return false;
            }
            if (SetContinue(TriggerMode.External) == false) return false;

            status &= ~ObjectLJV7.STATUS.CURRENTIMAGE;
            return true;
        }

        #endregion

        #region LJV7IF Settings
        public enum TriggerMode
        {
            Continuous = 0x00,
            External = 0x01,
            Encoder = 0x02,
        }
        public bool SetContinue(TriggerMode mode)
        {
            byte ProgNo = 0x10;
            ProgNo += GetActiveProgram();

            if (ProgNo == 0xFF) return false;

            //p62 Trigger mode 
            //  0: Continuous trigger, 
            //  1: External trigger,
            //  2: Encoder trigger
            return SetSetting(0x00, 0x01, ProgNo, null, new byte[1] { Convert.ToByte(mode) });
        }

        //type: 
        //  0x01: Environment settings, 
        //  0x02: Common measurement settings, 
        //  0x10: Program 0, 0x11: Program 1, ..., 0x1F: Program 15
        public bool SetSetting(byte category, byte item, byte type, byte[] targets, byte[] data)
        {
            LJV7IF_TARGET_SETTING target_setting = GetTargetSetting(category, item, type, targets);

            //0: Write settings area, 
            //1: Running settings area, 
            //2: Save area
            SettingDepth _depth = SettingDepth.Running;

            if (data == null) data = new byte[1];
            using (PinnedObject pin = new PinnedObject(data))
            {
                uint dwError = 0;
                int rc = NativeMethods.LJV7IF_SetSetting(DEVICE_ID, _depth, target_setting, pin.Pointer, (uint)data.Length, ref dwError);
                // @Point
                // † There are three setting areas: a) the write settings area, b) the running area, and c) the save area.
                //   * Specify a) for the setting level when you want to change multiple settings. However, to reflect settings in the LJ-V operations, you have to call LJV7IF_ReflectSetting.
                //	 * Specify b) for the setting level when you want to change one setting but you don't mind if this setting is returned to its value prior to the change when the power is turned off.
                //	 * Specify c) for the setting level when you want to change one setting and you want this new value to be retained even when the power is turned off.

                // @Point
                //  As a usage example, we will show how to use SettingForm to configure settings such that sending a setting, with SettingForm using its initial values,
                //  will change the sampling period in the running area to "100 Hz."
                //  Also see the GetSetting function.
                return CheckReturnCode((Rc)rc);
            }
        }

        public bool GetSetting(byte category, byte item, byte type, byte[] targets, int datasize)
        {
            LJV7IF_TARGET_SETTING target_setting = GetTargetSetting(category, item, type, targets);

            //0: Write settings area, 
            //1: Running settings area, 
            //2: Save area
            SettingDepth _depth = SettingDepth.Running;

            if (datasize <= 0) datasize = 1;
            byte[] data = new byte[datasize];
            using (PinnedObject pin = new PinnedObject(data))
            {
                int rc = NativeMethods.LJV7IF_GetSetting(DEVICE_ID, _depth, target_setting, pin.Pointer, (uint)data.Length);
                // @Point
                // † There are three setting areas: a) the write settings area, b) the running area, and c) the save area.
                //   * Specify a) for the setting level when you want to change multiple settings. However, to reflect settings in the LJ-V operations, you have to call LJV7IF_ReflectSetting.
                //	 * Specify b) for the setting level when you want to change one setting but you don't mind if this setting is returned to its value prior to the change when the power is turned off.
                //	 * Specify c) for the setting level when you want to change one setting and you want this new value to be retained even when the power is turned off.

                // @Point
                //  As a usage example, we will show how to use SettingForm to configure settings such that sending a setting, with SettingForm using its initial values,
                //  will change the sampling period in the running area to "100 Hz."
                //  Also see the GetSetting function.
                return CheckReturnCode((Rc)rc);
            }
        }

        public LJV7IF_TARGET_SETTING GetTargetSetting(byte category, byte item, byte type, byte[] targets)
        {
            LJV7IF_TARGET_SETTING target_setting = new LJV7IF_TARGET_SETTING();
            target_setting.byCategory = category;
            target_setting.byItem = item;
            target_setting.byType = type;
            if (targets != null)
            {
                if (targets.Length > 0) target_setting.byTarget1 = targets[0];
                if (targets.Length > 1) target_setting.byTarget2 = targets[1];
                if (targets.Length > 2) target_setting.byTarget3 = targets[2];
                if (targets.Length > 3) target_setting.byTarget4 = targets[3];
            }
            return target_setting;
        }


        private byte GetActiveProgram()
        {
            byte ProgNo = 0xFF;
            int rc = NativeMethods.LJV7IF_GetActiveProgram(DEVICE_ID, ref ProgNo);
            if (CheckReturnCode((Rc)rc) == false) return 0xFF;
            return ProgNo;
        }

        #endregion
    }
}

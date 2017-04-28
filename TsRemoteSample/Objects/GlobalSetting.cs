using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
//for StreamWriter
using System.IO;
//for Application
using System.Windows.Forms;

namespace PHTools
{
    class GlobalSetting
    {
        public static Dictionary<string, string> _theLang = null;
        public static int BoderSize = 2;

        public class DEBUGTYPE
        {
            public const int NONE = 0x00;
            public const int FRAME = 0x01;
        }
        public static int _DebugType = DEBUGTYPE.FRAME;

        #region System Config

        public static int _TotalCount = 0;
        public static StreamWriter SW_TotalCount = new StreamWriter(Application.StartupPath + "\\count.boltun");
        public static System.Timers.Timer timer_HighSpeedReceived = new System.Timers.Timer();
        public static System.Timers.Timer timer_HighSpeedSend = new System.Timers.Timer();
        public static System.Timers.Timer timer_CheckStop = new System.Timers.Timer();

        //連線設定資料
        public static string _LJV7IP = "192.168.0.4";
        public static string _LJV7CommandPort = "24691";
        public static string _LJV7HighSpeedPort = "24692";
        public static string _LJV7CallbackFrequency = "200";

        //資料目錄
        public static string _LJV7OriginPathRoot = Application.StartupPath + @"\original"; //原始資料放置根目錄
        public static string _LJV7OriginPathDir = ""; //原始資料放置目錄

        //容錯率(連續資料數異常需列為考慮)
        public static string _FaultTolerance = "2";
        //斜率值大於多少，當作錯誤
        //輪緣及螺紋判斷錯誤閥值
        //應該使用斜率做為判斷
        //使用二值法可能造成誤判特異點
        public static string _FaultSlope_Tooth = "16.5";
        public static string _FaultSlope_Flange = "15";

        //儲存用
        public static DateTime _LastDatetime = DateTime.Today; //用來記錄最後一次儲存時間

        public static void readConfig(System.IO.FileStream FileStream)
        {
            byte[] buffer = new byte[8];
            //Start Bit
            FileStream.Read(buffer, 0, 2);
            if (buffer[0] != 0xFF || buffer[1] != 0x00) return;
            //Version
            FileStream.Read(buffer, 0, 2);
            if (buffer[0] != 0xFF) return;
            int version = buffer[1];
            //data size
            FileStream.Read(buffer, 0, 5);
            if (buffer[0] != 0xFF) return;
            int length = (int)buffer[1] + (int)(buffer[2] << 8) + (int)(buffer[3] << 16) + (int)(buffer[4] << 24);
            //Console.WriteLine("資料長度: " + length);
            if (version == 1) readCongif_v1(FileStream, length);
            return;
        }

        public static void readCongif_v1(System.IO.FileStream FileStream, int length)
        {
            byte[] buffer = new byte[2];
            //開啟讀取資料
            List<char> temp = new List<char>();
            byte[] tmp_byte = new byte[2] { 0x00, 0x00 };
            int waitData = -1;
            for (int i = 0; i < length; i++)
            {
                buffer[0] = Convert.ToByte(FileStream.ReadByte());

                //可能為指令，也可能是整數
                if (buffer[0] == 0xFF && waitData <= 0)
                {
                    buffer[1] = Convert.ToByte(FileStream.ReadByte());
                    //因為量測範圍16個byte所組成的4個整數，因此看到0x86或0x87後，須等待16個byte，才能進行下一筆資料讀寫
                    if (buffer[1] == 0x86 || buffer[1] == 0x87)
                    {
                        waitData = 16;
                    }
                    //必須判斷是否結束，因為有多讀的情況，所以i一定<length
                    else if (buffer[1] == 0xFF) return;
                    if (temp.Count <= 0) continue;

                    //end
                    if (buffer[1] == 0x80) GlobalSetting._LJV7IP = new String(temp.ToArray());
                    else if (buffer[1] == 0x81) GlobalSetting._LJV7CommandPort = new String(temp.ToArray());
                    else if (buffer[1] == 0x82) GlobalSetting._LJV7HighSpeedPort = new String(temp.ToArray());
                    else if (buffer[1] == 0x83) GlobalSetting._LJV7CallbackFrequency = new String(temp.ToArray());
                    else if (buffer[1] == 0x84) GlobalSetting._LJV7OriginPathRoot = new String(temp.ToArray());

                   
                    //start next
                    temp.Clear();
                    continue;
                }
                waitData--;
                tmp_byte[0] = buffer[0];
                temp.Add(BitConverter.ToChar(tmp_byte, 0));
            }
        }

        public static void checkConfig()
        {
            //偵測設定檔是否存在
            if (File.Exists(Application.StartupPath + @"\config.boltun"))
            {
                //設定檔存在則讀取資料
                // Open file for reading
                System.IO.FileStream FileStream = new System.IO.FileStream(Application.StartupPath + @"\config.boltun", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                readConfig(FileStream);
                // close file stream
                FileStream.Close();
            }

            //設定檔不存在則使用預設資料
            if (GlobalSetting._LJV7IP == "") GlobalSetting._LJV7IP = "192.168.10.20";
            if (GlobalSetting._LJV7CommandPort == "") GlobalSetting._LJV7CommandPort = "24691";
            if (GlobalSetting._LJV7HighSpeedPort == "") GlobalSetting._LJV7HighSpeedPort = "24692";
            if (GlobalSetting._LJV7OriginPathRoot == "") GlobalSetting._LJV7OriginPathRoot = Application.StartupPath + @"\original";

            GlobalSetting._LJV7OriginPathRoot = checkDirector(GlobalSetting._LJV7OriginPathRoot, Application.StartupPath + @"\original");
            GlobalSetting._LJV7OriginPathDir = GlobalSetting.checkDirector(GlobalSetting._LJV7OriginPathRoot + @"\" + DateTime.Today.ToString("yyyyMMdd"), null);
        }

        public static string checkDirector(string path, string defaultpath)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    if (defaultpath != null && defaultpath != "")
                    {
                        path = defaultpath;
                        Directory.CreateDirectory(defaultpath);
                    }
                }
            }
            return path;
        }

        //檔案結構:
        //0xFF, 0x00, 0xFF, Version, 0xFF, data size (int), (data...), 0xFF, 0xFF
        //data frame: 0xFF, type, data, 0xFF, type
        public static void writeConfig()
        {
            List<byte> data = new List<byte>();
            data.AddRange(new byte[] { 0xFF, 0x80 });//_LJV7IP
            data.AddRange(ConverterString(GlobalSetting._LJV7IP.ToCharArray()));
            data.AddRange(new byte[] { 0xFF, 0x80 });//_LJV7IP
            data.AddRange(new byte[] { 0xFF, 0x81 });//_LJV7CommandPort
            data.AddRange(ConverterString(GlobalSetting._LJV7CommandPort.ToCharArray()));
            data.AddRange(new byte[] { 0xFF, 0x81 });//_LJV7CommandPort
            data.AddRange(new byte[] { 0xFF, 0x82 });//_LJV7HighSpeedPort
            data.AddRange(ConverterString(GlobalSetting._LJV7HighSpeedPort.ToCharArray()));
            data.AddRange(new byte[] { 0xFF, 0x82 });//_LJV7HighSpeedPort
            data.AddRange(new byte[] { 0xFF, 0x83 });//_LJV7CallbackFrequency
            data.AddRange(ConverterString(GlobalSetting._LJV7CallbackFrequency.ToCharArray()));
            data.AddRange(new byte[] { 0xFF, 0x83 });//_LJV7CallbackFrequency
            data.AddRange(new byte[] { 0xFF, 0x84 });//_LJV7OriginPathRoot
            data.AddRange(ConverterString(GlobalSetting._LJV7OriginPathRoot.ToCharArray()));
            data.AddRange(new byte[] { 0xFF, 0x84 });//_LJV7OriginPat

            data.AddRange(new byte[] { 0xFF, 0xFF });//END bit

            //data size
            data.InsertRange(0, BitConverter.GetBytes(data.Count));
            data.Insert(0, 0xFF);
            data.InsertRange(0, new byte[] { 0xFF, 0x01 });//Version
            data.InsertRange(0, new byte[] { 0xFF, 0x00 });//Start bit

            // Open file for reading
            System.IO.FileStream FileStream = new System.IO.FileStream("config.boltun", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            FileStream.Write(data.ToArray(), 0, data.ToArray().Length);
            // close file stream
            FileStream.Close();
        }
        #endregion

        public static byte[] ConverterString(char[] str)
        {
            byte[] buffer = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                buffer[i] = BitConverter.GetBytes(str[i])[0];
            }
            return buffer;
        }
    }
}

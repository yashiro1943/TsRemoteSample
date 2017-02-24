using System;
using System.Collections.Generic;
using System.Text;

//for StreamWriter
using System.IO;
//for Application
using System.Windows.Forms;

namespace PHTools
{
    class Languages
    {
        public static Dictionary<string, Dictionary<string, string>> langs = new Dictionary<string, Dictionary<string, string>>();

        public static Dictionary<string, string> getLang(string lang_name)
        {
            if (langs.ContainsKey(lang_name) == false)
            {
                if(langs.ContainsKey("default") == true) {
                    //預設資料先放default
                    Dictionary<string, string> lang_def = langs["default"];
                    langs[lang_name] = new Dictionary<string, string>(lang_def);
                }
                else langs[lang_name] = new Dictionary<string, string>();

            } 
            return langs[lang_name];
        }
        
        public static void init()
        {
            setDefaultLang();
        }

        public static void readLanguage(string lang_name)
        {
            //偵測語系檔是否存在
            if (File.Exists(Application.StartupPath + @"\" + lang_name + ".lang"))
            {
                Dictionary<string, string> lang = getLang(lang_name);

                // Read the file and display it line by line.
                System.IO.StreamReader file =  new System.IO.StreamReader(Application.StartupPath + @"\" + lang_name + ".lang");
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] lang_set = line.Replace(";", "").Split("=".ToCharArray());
                    if (lang_set.Length != 2) continue;

                    int start = lang_set[0].IndexOf("\"") + 1;
                    int end = lang_set[0].LastIndexOf("\"");
                    if (start == -1 || end == -1) continue;
                    string key = lang_set[0].Substring(start, end-start);
                    if (key == "") continue;

                    start = lang_set[1].IndexOf("\"") + 1;
                    end = lang_set[1].LastIndexOf("\"");
                    if (start == -1 || end == -1) continue;
                    string value = lang_set[1].Substring(start, end - start);
                    if (value == "") continue;
                    
                    lang[key] = value;
                }
                
                // close file stream
                file.Close();
            }
        }

        public static void setDefaultLang()
        {
            Dictionary<string, string> lang = getLang("default");
            lang["AppTitle"] = "螺絲檢測系統-恒耀工業股份有限公司製作";
            lang["BtnClose"] = "關閉";
            {
                lang["Machine Setting"] = "儀器設定";
                {
                    {
                        lang["IP"] = "IP address";
                        lang["TCP Command"] = "TCP port number(Command)";
                        lang["TCP High"] = "TCP port number(High-speed)";
                        lang["Call Back Freq"] = "Callback func calling frequency";
                        lang["High Speed Start No"] = "Starting profiler number";
                    }
                    lang["Current Image"] = "即時影像";
                    {
                        lang["Current Image Stop"] = "停止即時影像";
                        lang["Open Current Image"] = "開啟即時影像";
                        {
                            lang["Run Once"] = "單次執行";
                            lang["Initialize"] = "初始化";
                            lang["Finalize"] = "離線";
                        }
                        lang["Detector"] = "檢測設定";
                        {
                            lang["Fault Slope Nut"] = "輪裂檢測比:";
                            lang["Fault Slope Tooth"] = "牙傷檢測比:";
                            lang["Fault Tolerance"] = "容錯資料數:";
                            lang["Control by PLC"] = "進行觸發";
                            lang["Send to PLC"] = "傳送結果";
                            lang["PLC Detection On"] = "控制開啟";
                            lang["PLC Detection Off"] = "控制關閉";
                            lang["Detect Flanges"] = "測試輪緣";
                            lang["Detect Tooths"] = "測試螺牙";
                        }
                        lang["Drawing Time"] = "繪圖時間";
                        lang["Data Path"] = "資料目錄";
                        {
                            lang["Directory"] = "目錄";
                            lang["Choose Origin Path"] = "選擇原始資料放置目錄";
                            lang["Choose Result Path"] = "選擇檢測結果放置目錄";
                        }
                    }
                }
                lang["Test Result"] = "檢測結果";
                {
                    lang["3D Model"] = "3D模型";
                    lang["3D Origin Model"] = "3D原始模型";
                    lang["Mouse Position"] = "滑鼠位置";
                    lang["Testing"] = "進行檢測";
                    {
                        lang["Start Run"] = "開始運行";
                        lang["Stop Run"] = "結束運行";
                        lang["Display Compute Time"] = "顯示演算法平均計算時間";
                        lang["Result Name"] = "結果名稱：";
                        lang["Read Image"] = "讀取圖片";
                        lang["Set Flange"] = "輪緣範圍";
                        lang["Set Tooth"] = "螺牙範圍";
                        lang["Cancel Set Frame"] = "取消框選";
                        lang["Reset OpenGL"] = "重置視角";
                        lang["Close 3D"] = "關閉3D";
                        lang["Open 3D"] = "開啟3D";
                        lang["Test Result"] = "檢測結果";
                        lang["Test Name"] = "檢測名稱";
                    }
                }
                lang["Offline Test"] = "離線測試";
                {
                    lang["Choose File"] = "選擇檔案";
                    lang["Auto Set Range"] = "自動設定範圍";
                    lang["Result 3D Drawing time"] = "顯示3D繪圖時間";
                    lang["Origin 3D Drawing time"] = "顯示3D繪圖時間";
                }
            }

            //Message
            lang["File Load Error"] = "檔案讀取錯誤";
            lang["Flanges Not Set"] = "尚未設定輪緣測試區域";
            lang["Tooth Not Set"] = "尚未設定螺牙測試區域";
            lang["Set Error"] = "設定錯誤";
            lang["Result"] = "結果";
            lang["Compute Time"] = "平均計算時間";
            lang["Machine Connect Success"] = "儀器連線成功";
            lang["Machine Disconnect Success"] = "儀器離線成功";
            lang["Detecting..."] = "檢測中...";
            lang["Capturing Current Image..."] = "擷取即時影像中...";
        }
    }
}

using System;
//for Thread
using System.Threading;
//for StreamWriter
using System.IO;
//for Bitmap
using System.Drawing;
//for List
using System.Collections.Generic;
//for MessageBox
using System.Windows.Forms;
//for PixelFormat
using System.Drawing.Imaging;
//for GraphicPath
using System.Drawing.Drawing2D;

namespace KeyenceTools
{
    //雷射及演算法資料資料放置結構
    public class FrameData_LJV7
    {
        #region Constant
        //演算法計算資料組數，不可小於回傳的取樣頻率
        public const uint _COMPUTESIZE = 700; //演算法計算組數
        public const uint _DATASETSIZE = 800; //收取資料總組數
        public static uint _PROFILESIZE = 807; //一組原始資料筆數
        //資料均化
        //回傳資料=mm*100000
        //資料大於9mm表示有負值，需往下平移9mm
        public const float div_unit2mm = 100000F;
        public const float div_avg = div_unit2mm;  //to mm        
        #endregion

        #region Field
        //buffer的size = profileSize*count
        public int[] buffer = null;
        public uint count = 0;
		
		//buffer_compute的size = LJVcount*size_per_frame
        public float[] buffer_compute = null;
		public uint LJVcount = 0;				    //800 with one laser
        public uint size_per_frame = _COMPUTESIZE;	//default: COMPUTESIZE(700)

        //全區最大最小值
        public float Max_Buffer_Compute = -999;
        public float Min_Buffer_Compute = 999;
        #endregion

        //只需要中間穩定的COMPUTESIZE組數
        //及檢查資料是否正確
        public void SetComputeBuffer()
        {
            Max_Buffer_Compute = -999;
            Min_Buffer_Compute = 999;
            //取中間
			//int start = (int)(count - size_per_frame) / 2;
            //取後段
            int start = (int)(count - size_per_frame);

			LJVcount = _PROFILESIZE-7;	//800 with one laser
            //只需要計算中間size_per_frame組
            buffer_compute = new float[LJVcount*size_per_frame];

            for (int i = start; i < start + size_per_frame; i++)
            {
				for(int j = 0; j < LJVcount; j++) {
                    int pos = Convert.ToInt32(i * LJVcount + j);
                    if (i * _PROFILESIZE + 6 + j >= buffer.Length)
                    {
                        //error
                        Console.WriteLine(DateTime.Now.TimeOfDay + ": Error: buffer size error");
                        //MessageBox.Show(string.Format("Error: buffer size error"), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    if ((buffer[i * _PROFILESIZE + 6 + j] & 0xffff0000) == 0x80000000)
                    {
                        buffer_compute[pos] = -999;
                        continue;
                    }

                    //LJV7IF_PROFILE_HEADER = 6*uint
                    float dtmp = 60 - buffer[i * _PROFILESIZE + 6 + j] / div_avg;
                    //距離=60mm-回傳資料;
                    buffer_compute[pos] = dtmp;

                    if (dtmp == -999 || dtmp == 0) continue;
                    if (dtmp > Max_Buffer_Compute) Max_Buffer_Compute = dtmp;
                    else if (dtmp < Min_Buffer_Compute) Min_Buffer_Compute = dtmp;
				}
            }
        }

        public void DoWriteFile(string path)
        {
            ThreadWriteFile writer = new ThreadWriteFile();
            writer.frame = this;
            writer.filepath = path;
            Thread thread = new Thread(writer.DoWriteFile); //啟動Thread
            thread.Start();
            Console.WriteLine(DateTime.Now.TimeOfDay + ": DoWriteFile開啟執行緒" + thread.ManagedThreadId);
        }

        //560ms
        private class ThreadWriteFile
        {
            public FrameData_LJV7 frame;
            public string filepath;
            //輸出格式
            public static string str_format = "{000.0000}";
            public void DoWriteFile()
            {
                float[] buffer = frame.buffer_compute;
                uint size = frame.LJVcount;
                uint count = frame.size_per_frame;

                StreamWriter sw = new StreamWriter(filepath);
                for (int i = 0; i < count; i++)
                {
                    //sw.Write(string.Format(str_format, buffer[i * size]));
                    sw.Write(buffer[i * size].ToString("+000.0000;-000.0000"));
                    for (int j = 1; j < size; j++)
                    {
                        //sw.Write("," + string.Format(str_format, buffer[i * size + j]));
                        sw.Write("," + buffer[i * size + j].ToString("+000.0000;-000.0000"));
                    }
                    sw.WriteLine();
                }
                sw.Close();
                sw = null;
                Console.WriteLine(DateTime.Now.TimeOfDay + ": 寫檔完畢" + filepath);
            }

            public static void DoWriteFile(FrameData_LJV7 frame, string filepath)
            {
                ThreadWriteFile writer = new ThreadWriteFile();
                writer.frame = frame;
                writer.filepath = filepath;
                writer.DoWriteFile();
            }
        }
		
		public void DoWriteProFile(string path)
        {
            ThreadWriteProFile writer = new ThreadWriteProFile();
            writer.frame = this;
            writer.filepath = path;
            Thread thread = new Thread(writer.DoWriteProFile); //啟動Thread
            thread.Start();
            Console.WriteLine(DateTime.Now.TimeOfDay + ": DoWriteProFile開啟執行緒" + thread.ManagedThreadId);
        }

        private class ThreadWriteProFile
        {
            public FrameData_LJV7 frame;
            public string filepath;
            //輸出格式
            public static string str_format = "{0:X8}";
			public void DoWriteProFile()
            {
                int[] buffer = frame.buffer;
                uint profileSize = FrameData_LJV7._PROFILESIZE;
                uint count = frame.count;

                StreamWriter sw = new StreamWriter(filepath);
				for (int i = 0; i < count; i++)
                {
					sw.Write(string.Format(str_format, buffer[i * profileSize]));
                    for (int j = 1; j < profileSize; j++)
                    {
						sw.Write("," + string.Format(str_format, buffer[i * profileSize + j]));
                    }
                    sw.WriteLine();
                }				
                sw.Close();
                sw = null;
                Console.WriteLine(DateTime.Now.TimeOfDay + ": 寫檔完畢" + filepath);
            }
        }

        public void DoWriteByteFile(string path, float[] buffer_float)
        {
            ThreadWriteByteFile writer = new ThreadWriteByteFile();
            writer.buffer_float = buffer_float;
            writer.filepath = path;
            Thread thread = new Thread(writer.DoWriteByteFile); //啟動Thread
            thread.Start();
            //Console.WriteLine(DateTime.Now.TimeOfDay + ": DoWriteByteFile開啟執行緒" + thread.ManagedThreadId);
        }

        //50ms以下
        private class ThreadWriteByteFile
        {
            public float[] buffer_float;
            public string filepath;
            public void DoWriteByteFile()
            {
                try
                {
                    byte[] buffer = new byte[buffer_float.Length * sizeof(float)];
                    Buffer.BlockCopy(buffer_float, 0, buffer, 0, buffer.Length);
                
                    // Open file for reading
                    System.IO.FileStream FileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    FileStream.Write(buffer, 0, buffer.Length);
                    // close file stream
                    FileStream.Close();
                    //Console.WriteLine(DateTime.Now.TimeOfDay + ": 寫檔完畢" + filepath);

                    buffer = null;
                    return;
                }
                catch (Exception _Exception)
                {
                    // Error
                    Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                }
            }
        }

        public void DoWritePreByteFile(string path, int[] buffer_int)
        {
            ThreadWritePreByteFile writer = new ThreadWritePreByteFile();
            writer.buffer_int = buffer_int;
            writer.filepath = path;
            Thread thread = new Thread(writer.DoWriteByteFile); //啟動Thread
            thread.Start();
            //Console.WriteLine(DateTime.Now.TimeOfDay + ": DoWriteByteFile開啟執行緒" + thread.ManagedThreadId);
        }

        private class ThreadWritePreByteFile
        {
            public int[] buffer_int;
            public string filepath;
            public void DoWriteByteFile()
            {
                try
                {
                    byte[] buffer = new byte[buffer_int.Length * sizeof(int)];
                    Buffer.BlockCopy(buffer_int, 0, buffer, 0, buffer.Length);

                    // Open file for reading
                    System.IO.FileStream FileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    FileStream.Write(buffer, 0, buffer.Length);
                    // close file stream
                    FileStream.Close();
                    //Console.WriteLine(DateTime.Now.TimeOfDay + ": 寫檔完畢" + filepath);

                    buffer = null;
                    return;
                }
                catch (Exception _Exception)
                {
                    // Error
                    Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                }
            }
        }

        public static FrameData_LJV7 DoReadByteFile(string filepath)
        {
            FrameData_LJV7 frame = new FrameData_LJV7();
            frame.Max_Buffer_Compute = -999;
            frame.Min_Buffer_Compute = 999;
            
            frame.LJVcount = FrameData_LJV7._DATASETSIZE;
            frame.size_per_frame = FrameData_LJV7._COMPUTESIZE;
            try
            {
                frame.buffer_compute = new float[frame.LJVcount * frame.size_per_frame];
                byte[] buffer = new byte[frame.buffer_compute.Length * sizeof(float)];
            
                // Open file for reading
                System.IO.FileStream FileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                FileStream.Read(buffer, 0, buffer.Length);
                // close file stream
                FileStream.Close();


                Buffer.BlockCopy(buffer, 0, frame.buffer_compute, 0, buffer.Length);
                //ThreadWriteFile.DoWriteFile(frame, filepath + ".csv"); //for test

                //計算最大與最小
                for (int i = 0; i < frame.buffer_compute.Length; i++)
                {
                    if (frame.buffer_compute[i] == -999) continue;
                    if (frame.buffer_compute[i] > frame.Max_Buffer_Compute) frame.Max_Buffer_Compute = frame.buffer_compute[i];
                    else if (frame.buffer_compute[i] < frame.Min_Buffer_Compute) frame.Min_Buffer_Compute = frame.buffer_compute[i];
                }

                FileStream = null;
                buffer = null;
                return frame;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.Message);
            }
            return null;
        }

        public static FrameData_LJV7 DoReadPreByteFile(string filepath)
        {
            FrameData_LJV7 frame = new FrameData_LJV7();
            frame.LJVcount = FrameData_LJV7._DATASETSIZE;
            frame.size_per_frame = 0;
            try
            {
                // Open file for reading
                System.IO.FileStream FileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                List<byte> buffer = new List<byte>();
                byte[] buffer_tmp = new byte[frame.LJVcount * sizeof(int)];
                while (FileStream.Read(buffer_tmp, 0, buffer_tmp.Length) > 0)
                {
                    buffer.AddRange(buffer_tmp);
                    frame.size_per_frame++;
                }
                // close file stream
                FileStream.Close();

                byte[] buffer_all = buffer.ToArray();
                frame.buffer = new int[frame.LJVcount * frame.size_per_frame];
                Buffer.BlockCopy(buffer_all, 0, frame.buffer, 0, buffer_all.Length);
                //ThreadWriteFile.DoWriteFile(frame, filepath + ".csv"); //for test

                FileStream = null;
                buffer = null;
                return frame;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.Message);
            }
            return null;
        }

    } // end class FrameData_LJV7
}

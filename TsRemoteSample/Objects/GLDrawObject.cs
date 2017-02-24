using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
//for PixelFormat
using System.Drawing.Imaging;
//for 碼錶
using System.Diagnostics;
//for MouseEventArgs
using System.Windows.Forms;
using SharpGL;
//for GraphicPath
using System.Drawing.Drawing2D;

using KeyenceTools;
namespace PHTools
{
    class GLDrawObject
    {
        public static int MaxColorH = 260;
        public static int MinColorH = 50;
        public static int MaxErrorColorH = 360;
        public static int MinErrorColorH = 330;

        #region DrawOpenGL

        public static int isNav = 1;
        public static void Trans3D(SharpGL.OpenGL gl_object, int x, int y)
        {
            //Console.Write(_RoX + ", " + _RoY + ", " + " => ");

            //	Create an array that will be the viewport.
            //3D顯示大小
            int[] viewport = new int[4];
            //	Get the viewport, then convert the mouse point to an opengl point.
            gl_object.GetInteger(OpenGL.VIEWPORT, viewport);
            double[] modelview = new double[16];
            gl_object.GetDouble(OpenGL.MODELVIEW_MATRIX, modelview);
            double[] projection = new double[16];
            gl_object.GetDouble(OpenGL.PROJECTION_MATRIX, projection);

            float winX, winY;
            winX = (x - viewport[2] / 2);
            winY = (y - viewport[3] / 2); //與範例不同的是 圖形原點是左上角 
            //Console.Write(winX + ", " + winY + ", " + " => ");

            //(posX, posY, posZ) 轉換之前的座標
            double[] posX = new double[4];
            double[] posY = new double[4];
            double[] posZ = new double[4];
            gl_object.UnProject(x, y, 0, modelview, projection, viewport, posX, posY, posZ);
            //Console.WriteLine(posX[0] + ", " + posY[0] + ", " + posZ[0]);

            isNav = 1;
            if (_RoX >= 165 && _RoX <= 200)
            {
                if (winX * posX[0] < 0 && winY * posY[0] < 0) isNav = -1;
            }
            else if (_RoX >= 45 && _RoX <= 315)
            {
                if (winX * posX[0] > 0) isNav = -1;
            }
            return;
        }
        
        public static void drawAxis(SharpGL.OpenGL gl_object)
        {
            //畫Z軸
            gl_object.Color(1.0, 1.0, 1.0);
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(-0.02, -0.02, -0.02);
            gl_object.Vertex(-0.02, -0.02, 10);
            gl_object.Vertex(0.02, -0.02, -0.02);
            gl_object.Vertex(0.02, -0.02, 10);
            gl_object.Vertex(0.02, 0.02, -0.02);
            gl_object.Vertex(0.02, 0.02, 10);
            gl_object.Vertex(-0.02, 0.02, -0.02);
            gl_object.Vertex(-0.02, 0.02, 10);
            gl_object.Vertex(-0.02, -0.02, -0.02);
            gl_object.Vertex(-0.02, -0.02, 10);
            gl_object.End();
            //up
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(-0.02, -0.02, 10);
            gl_object.Vertex(0.02, -0.02, 10);
            gl_object.Vertex(-0.02, 0.02, 10);
            gl_object.Vertex(0.02, 0.02, 10);
            gl_object.End();

            //down
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(-0.02, -0.02, -0.02);
            gl_object.Vertex(0.02, -0.02, -0.02);
            gl_object.Vertex(-0.02, 0.02, -0.02);
            gl_object.Vertex(0.02, 0.02, -0.02);
            gl_object.End();

            //畫X軸
            gl_object.Color(0, 1.0, 0);
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(0.02, -0.02, -0.02);
            gl_object.Vertex(10, -0.02, -0.02);
            gl_object.Vertex(0.02, 0.02, -0.02);
            gl_object.Vertex(10, 0.02, -0.02);
            gl_object.Vertex(0.02, 0.02, 0.02);
            gl_object.Vertex(10, 0.02, 0.02);
            gl_object.Vertex(0.02, -0.02, 0.02);
            gl_object.Vertex(10, -0.02, 0.02);
            gl_object.Vertex(0.02, -0.02, -0.02);
            gl_object.Vertex(10, -0.02, -0.02);
            gl_object.End();
            //up
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(10, -0.02, -0.02);
            gl_object.Vertex(10, 0.02, -0.02);
            gl_object.Vertex(10, -0.02, 0.02);
            gl_object.Vertex(10, 0.02, 0.02);
            gl_object.End();

            //down
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(0.02, -0.02, -0.02);
            gl_object.Vertex(0.02, 0.02, -0.02);
            gl_object.Vertex(0.02, -0.02, 0.02);
            gl_object.Vertex(0.02, 0.02, 0.02);
            gl_object.End();


            //畫Y軸
            gl_object.Color(0, 0, 1.0);
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(-0.02, 0.02, -0.02);
            gl_object.Vertex(-0.02, 10, -0.02);
            gl_object.Vertex(0.02, 0.02, -0.02);
            gl_object.Vertex(0.02, 10, -0.02);
            gl_object.Vertex(0.02, 0.02, 0.02);
            gl_object.Vertex(0.02, 10, 0.02);
            gl_object.Vertex(-0.02, 0.02, 0.02);
            gl_object.Vertex(-0.02, 10, 0.02);
            gl_object.Vertex(-0.02, 0.02, -0.02);
            gl_object.Vertex(-0.02, 10, -0.02);
            gl_object.End();
            //up
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(-0.02, 10, -0.02);
            gl_object.Vertex(0.02, 10, -0.02);
            gl_object.Vertex(-0.02, 10, 0.02);
            gl_object.Vertex(0.02, 10, 0.02);
            gl_object.End();

            //down
            gl_object.Begin(OpenGL.TRIANGLE_STRIP);
            gl_object.Vertex(-0.02, 0.02, -0.02);
            gl_object.Vertex(0.02, 0.02, -0.02);
            gl_object.Vertex(-0.02, 0.02, 0.02);
            gl_object.Vertex(0.02, 0.02, 0.02);
            gl_object.End();
        }

        //繪製原始資料3D
        //以面繪製
        public static void drawGL_FrameData_Triangle(SharpGL.OpenGL gl_object, FrameData_LJV7 frame)
        {
            drawGL_FrameData_Triangle(gl_object, frame, 4);
            return;
        }
        //jumpFrame: 一次跳躍資料數
        public static void drawGL_FrameData_Triangle(SharpGL.OpenGL gl_object, FrameData_LJV7 frame, int jumpFrame)
        {
            gl_object.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);

            //可以不用每次都旋轉，可以滑鼠動作時變更即可
            //不影響效能
            gl_object.LoadIdentity();
            gl_object.Translate(_LX, _LY, _LZ);
            gl_object.Rotate(_RoX, 0.0, 1.0, 0.0);
            gl_object.Rotate(_RoY, 1.0, 0.0, 0.0);
            gl_object.Rotate(_RoZ, 0.0, 0.0, 1.0);

            double x = 0, y = 0;
            double centerx = frame.size_per_frame / 2;
            double centery = frame.LJVcount / 2;
            double diff = frame.Max_Buffer_Compute - frame.Min_Buffer_Compute;
            double r = 0, g = 0, b = 0;
            double Color_H = 0.0;
            for (int j = 0; j < frame.LJVcount - jumpFrame; j += jumpFrame)
            {
                gl_object.Begin(OpenGL.TRIANGLE_STRIP);
                for (int i = 0; i < frame.size_per_frame - jumpFrame; i += jumpFrame)
                {
                    int pos = Convert.ToInt32(i * frame.LJVcount + j);
                    if (frame.buffer_compute[pos] == -999 || frame.buffer_compute[pos + jumpFrame] == -999)
                    {
                        gl_object.End();
                        gl_object.Begin(OpenGL.TRIANGLE_STRIP);
                        continue;
                    }
                    Color_H = (frame.buffer_compute[pos] - frame.Min_Buffer_Compute) / diff;
                    Color_H = Color_H * (MaxColorH - MinColorH) + MinColorH;
                    HsvToRgb(Color_H, 1.0, 1.0, out r, out g, out b);
                    gl_object.Color(r, g, b);
                    
                    x = (Convert.ToDouble(i) - centerx) / Convert.ToDouble(100);
                    y = -(Convert.ToDouble(j) - centery) / Convert.ToDouble(100);
                    gl_object.Vertex(x, y, frame.buffer_compute[pos]);


                    Color_H = (frame.buffer_compute[pos + jumpFrame] - frame.Min_Buffer_Compute) / diff;
                    Color_H = Color_H * (MaxColorH - MinColorH) + MinColorH;
                    HsvToRgb(Color_H, 1.0, 1.0, out r, out g, out b);
                    gl_object.Color(r, g, b);

                    x = (Convert.ToDouble(i) - centerx) / Convert.ToDouble(100);
                    y = -(Convert.ToDouble(j + jumpFrame) - centery) / Convert.ToDouble(100);
                    gl_object.Vertex(x, y, frame.buffer_compute[pos + jumpFrame]);
                }
                gl_object.End();
            }

            //drawAxis(gl_object);
            gl_object.Flush();
            return;
        }
        #endregion

        public enum OperationStatus { None, Offline2D, Online2D, Offline3D, Online3D };               //操作模式
        public static OperationStatus _ReviewStatus = OperationStatus.None; //檢視模式開關
        public enum MoveStatus { None, Rotate, Shift };             //移動模式
        public static MoveStatus _3DMoveStatus = MoveStatus.None;   //是否為平移模式(右鍵模式)

        //全部3D座標系同步
        //座標
        public static float _LX = 0;
        public static float _LY = 0;
        public static float _LZ = -15f;
        //旋轉座標
        public static float _RoX = 0;
        public static float _RoY = 0;
        public static float _RoZ = 0;

        public static Point _BakMousePosition = new Point(0, 0);  //記錄原游標位置
        public static void OpenGLCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            OpenGLCtrl ctrl = (OpenGLCtrl)sender;
            if (_3DMoveStatus == MoveStatus.Rotate)
            {
                _RoX += (e.X - _BakMousePosition.X) / 5.0f;
                _RoY += isNav * (e.Y - _BakMousePosition.Y) / 5.0f;
                if (_RoX < 0) _RoX += 360;
                else if (_RoX > 360) _RoX -= 360;
                if (_RoY < 0) _RoY += 360;
                else if (_RoY > 360) _RoY -= 360;
            }
            else if (_3DMoveStatus == MoveStatus.Shift)
            {
                _LX += (e.X - _BakMousePosition.X) / (70.0f + _LZ);
                _LY += (_BakMousePosition.Y - e.Y) / (70.0f + _LZ);
            }
            _BakMousePosition.X = e.X;
            _BakMousePosition.Y = e.Y;
        }

        public static void ResetView()
        {
            _LX = 0;
            _LY = 0;
            _LZ = -30f;
            _RoX = _RoY = _RoZ = 0.0f;
            _RoY = 0;
        }

        //繪製即時畫面
        //buffer前6筆資料為Header
        public static Bitmap getBitmap_buffer(int[] buffer, uint bsize, Size imgsize)
        {
            if (imgsize == null) return null;
            Bitmap image = new Bitmap(imgsize.Width, imgsize.Height, PixelFormat.Format24bppRgb);
            if (buffer == null || bsize == 0 || buffer.Length < bsize) return image;

            int margin = 40;
            int margin_left = 5;
            int left = 40;
            Graphics graphic_line = Graphics.FromImage(image);
            GraphicsPath gpath_line = new GraphicsPath();

            //尋找最高與最低
            int max = 0, min = 0x0fffffff;
            for (int i = 6; i < bsize; i++)
            {
                if ((buffer[i] & 0xffff0000) == 0x80000000)
                {
                    //無效資料
                }
                else
                {
                    if (buffer[i] > max) max = buffer[i];
                    if (buffer[i] < min) min = buffer[i];
                }
            }
            if (max <= min) return image;

            //繪出標準尺，10筆
            float fontH = (imgsize.Height - margin) / 20.0F;
            double tranPos = (max - min) / Convert.ToDouble(imgsize.Height - margin);
            for (int i = margin / 2; i < imgsize.Height; i += (imgsize.Height - margin) / 10)
            {
                gpath_line.AddLine(new Point(margin_left + left, imgsize.Height - i), new Point(imgsize.Width, imgsize.Height - i));
                gpath_line.CloseFigure();

                double showline = (tranPos * (i - margin / 2) + min) / FrameData_LJV7.div_unit2mm;
                gpath_line.AddString(showline.ToString("0.00"), //單位mm
                    new FontFamily("Arial"), (int)FontStyle.Regular, fontH,
                    new Point(margin_left, Convert.ToInt32(imgsize.Height - (i + fontH / 2))),
                    StringFormat.GenericDefault);
            }
            graphic_line.DrawPath(new Pen(Color.White, 1), gpath_line);
            gpath_line.Reset();

            Point[] points = new Point[bsize - 6 + 2];
            points[0] = new Point(margin_left + left, imgsize.Height - Convert.ToInt32(margin / 2));

            tranPos = (imgsize.Height - margin) / Convert.ToDouble(max - min);
            //前後各margin個pix空間
            double xd = (imgsize.Width - margin / 2 - (margin_left + left)) / Convert.ToDouble(bsize - 6);
            for (int i = 6; i < bsize - 1; i++)
            {
                if ((buffer[i] & 0xffff0000) == 0x80000000)
                {
                    //無效資料
                    points[i - 6 + 1] = new Point(Convert.ToInt32((i - 6) * xd + (margin_left + left)), imgsize.Height - Convert.ToInt32(margin / 2));
                }
                else
                {
                    points[i - 6 + 1] = new Point(Convert.ToInt32((i - 6) * xd + (margin_left + left)), imgsize.Height - Convert.ToInt32((buffer[i] - min) * tranPos + margin / 2));
                }
            }

            if ((buffer[bsize - 2] & 0xffff0000) == 0x80000000)
            {
                points[bsize - 6 + 2 - 2] = points[bsize - 6 + 2 - 1] = new Point(imgsize.Width, imgsize.Height - Convert.ToInt32(margin / 2));
            }
            else
            {
                points[bsize - 6 + 2 - 2] = points[bsize - 6 + 2 - 1] = new Point(imgsize.Width, imgsize.Height - Convert.ToInt32((buffer[bsize - 2] - min) * tranPos + margin / 2));
            }
            gpath_line.AddLines(points);
            graphic_line.DrawPath(new Pen(Color.Yellow, 1), gpath_line);

            return image;
        }
        
        public static void HsvToRgb(double h, double S, double V, out double r, out double g, out double b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color
                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color
                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color
                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color
                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.
                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = R;
            g = G;
            b = B;
        }
    }
}

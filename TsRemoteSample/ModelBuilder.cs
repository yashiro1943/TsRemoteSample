using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows.Forms;
using System.IO;

//for TsPointS
using TsRemoteLib;

using KeyenceTools;
using SharpGL;
using PHTools;

//for DPoint
using ToshibaTools;

using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace TsRemoteSample
{
    public class ModelBuilder
    {
        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
            public void setPoint(float valueX, float valueY, float valueZ)
            {
                x = valueX;
                y = valueY;
                z = valueZ;
            }
            public float[] getPoint()
            {
                return new float[3] { x, y, z };
            }
        }

        public struct Triangle
        {
            public Vector3 a;
            public Vector3 b;
            public Vector3 c;
        }

        //private Vector3 builder_3D = new Vector3();


        public const int _EACHSIZE = 100;

        public bool _Received = false;
        public List<TsPointS> _PointList = new List<TsPointS>();
        public List<int> _BufferList = new List<int>();
        public List<Vector3> _LaserData = new List<Vector3>();
        public List<Triangle> _data3D = new List<Triangle>();


        public void Add(TsPointS point, int[] buffer)
        {
            //if (_PointList[_PointList.Count - 1].C == point.C) return;
            _BufferList.AddRange(buffer);
            _PointList.Add(point);
            //角度
            //Console.WriteLine("Angle=" + point.C);
        }

        public void Clear() {
            _BufferList.Clear();
            _PointList.Clear();
            _ShowPoints = null;
        }

        #region Model Builder
        public FrameData_LJV7 _LJV7Frame = new FrameData_LJV7();
        private DPoint[] _ShowPoints = null;
        public uint LJVcount = FrameData_LJV7._PROFILESIZE - 7;	//800 with one laser
        public void doBuild() { doBuild(-1, -1); }
        public void doBuild_Excel() { doBuild_Excel(-1, -1); }

        public int Y_Distance;
        
        public void doBuild(double range_max, double range_min)
        {
            _LJV7Frame.buffer = _BufferList.ToArray();

            
            _LJV7Frame.size_per_frame = Convert.ToUInt32(_PointList.Count * _EACHSIZE);
            //_LJV7Frame.size_per_frame = Convert.ToUInt32(850);
            _LJV7Frame.count = _LJV7Frame.size_per_frame;
            _LJV7Frame.SetComputeBuffer();
            _LJV7Frame.DoWritePreByteFile("C:\\Users\\user\\Desktop\\3DModel.boltun", _LJV7Frame.buffer);

            _ShowPoints = null;
            GC.Collect();
            _ShowPoints = new DPoint[_LJV7Frame.buffer_compute.Length];
            for (int i = 0; i < _LJV7Frame.buffer_compute.Length; i++) _ShowPoints[i] = new DPoint();

            reBuild();
        }

        public void doBuild_Excel(double range_max, double range_min)
        {
            _LJV7Frame.buffer = new int[564900];
            //_PointList = new List<TsPointS>();
            _LJV7Frame.size_per_frame = Convert.ToUInt32(MainForm.lines.Length);
            //_LJV7Frame.size_per_frame = Convert.ToUInt32(850);
            _LJV7Frame.count = _LJV7Frame.size_per_frame;
            _LJV7Frame.SetComputeBuffer();

            _LJV7Frame.Min_Buffer_Compute= _LJV7Frame.Min_Buffer_Compute_Excel;
            _LJV7Frame.Max_Buffer_Compute = _LJV7Frame.Max_Buffer_Compute_Excel;


            _LJV7Frame.DoWritePreByteFile("C:\\Users\\user\\Desktop\\3DModel.boltun", _LJV7Frame.buffer);
            //_LJV7Frame.buffer_compute = new float[_LJV7Frame.LJVcount * 700];
            //_LJV7Frame.buffer_compute 
            _ShowPoints = null;
            GC.Collect();
            _ShowPoints = new DPoint[_LJV7Frame.buffer_compute.Length];


            for (int i = 0; i < _LJV7Frame.buffer_compute.Length; i++) _ShowPoints[i] = new DPoint();

            _LJV7Frame.buffer_compute = _LJV7Frame.buffer_compute_Excel;
            reBuildExcel();
        }



        public void reBuild() { reBuild(-1, -1); }
        public void reBuild(double range_max, double range_min) {
            reBuild_MoveObject(range_max, range_min); 
            //reBuild_MoveLser(range_max, range_min); 
        }


        public void reBuildExcel() { reBuildExcel(-1, -1); }
        public void reBuildExcel(double range_max, double range_min)
        {
            reBuild_Excel(range_max, range_min);
            //reBuild_MoveLser(range_max, range_min); 
        }

        public TsPointS StartPoint = null;
        public DPoint center = new DPoint();
        public double _Radio;
        
        #region With Move Object
        public DPoint _LaserPos = new DPoint(0, 600);

        //正規劃用
        public double Normalization(double Max)
        {
            if (Max <= 0)
            {
                return Max;
            }
            else
            {
                if (Max - (_LJV7Frame.Min_Buffer_Compute) == 0)
                {
                    ;
                }

                return ((Max * (_LJV7Frame.Max_Buffer_Compute / _LJV7Frame.Min_Buffer_Compute) - _LJV7Frame.Min_Buffer_Compute) * (_LJV7Frame.Max_Buffer_Compute)) / Calculate_Ratio(_LJV7Frame.Min_Buffer_Compute);//((_LJV7Frame.Min_Buffer_Compute * 12.07914973) - 615.1213363);//200
            }
        }



        public void reBuild_MoveObject(double range_max, double range_min)
        {
            if (_LJV7Frame.buffer == null || _LJV7Frame.buffer_compute == null || _ShowPoints == null) return;
            
            //Console.WriteLine("build: r=" + _Radio + ", angle=" + StartPoint.C + ", center=(" + center.X + ", " + center.Y + ")");

            double Y_Conversion = Distance_Conversion(Y_Distance);
            //double Y_Conversion = (double)Y_Distance/200;
            if (Y_Conversion < 0)
            {
                Y_Conversion = -Y_Conversion;
            }

            double diff = _LJV7Frame.Max_Buffer_Compute - _LJV7Frame.Min_Buffer_Compute;
            if (range_max > 0 && range_min > 0) diff = range_max - range_min;
            else
            {
                range_max = _LJV7Frame.Max_Buffer_Compute;
                range_min = _LJV7Frame.Min_Buffer_Compute;
            }
            double min_distance = 9999;
            TsPointS startp = StartPoint;
            for (int i = 0, k = 0; i < _LJV7Frame.size_per_frame; i++, k++)
            {
                TsPointS point = _PointList[i / _EACHSIZE];
                //Console.WriteLine("Angle=" + (startp.C + diffAngle * k));
                double arc = DPoint.Angle2Arc(startp.C + ((point.C - startp.C) / _EACHSIZE) * k);
                /*if (arc > 0)
                {
                    arc = arc;
                    ;
                }*/
                for (int j = 0; j < _LJV7Frame.LJVcount; j++)
                {
                    int pos = Convert.ToInt32(i * _LJV7Frame.LJVcount + j);


                    if (_LJV7Frame.buffer_compute[pos] == -999 || _LJV7Frame.buffer_compute[pos] < range_min || _LJV7Frame.buffer_compute[pos] > range_max)
                    {
                        _ShowPoints[pos].X = _ShowPoints[pos].Y = _ShowPoints[pos].Z = -999;
                        continue;
                    }

                    double Color_H = (_LJV7Frame.buffer_compute[pos] - range_min) / diff;
                    Color_H = Color_H * (GLDrawObject.MaxColorH - GLDrawObject.MinColorH) + GLDrawObject.MinColorH;
                    GLDrawObject.HsvToRgb(Color_H, 1.0, 1.0, out _ShowPoints[pos].r, out _ShowPoints[pos].g, out _ShowPoints[pos].b);

                    //對應到新座標: 拉平座標(實際接收資料)
                    /*_ShowPoints[pos].X = (Convert.ToDouble(i)) / Convert.ToDouble(100);
                    _ShowPoints[pos].Z = _Radio - _LJV7Frame.buffer_compute[pos];*/
                    if (_LJV7Frame.buffer_compute[pos] < min_distance) min_distance = _LJV7Frame.buffer_compute[pos];

                    //對應到新座標: 對應回現實座標
                    //*
                    //_ShowPoints[pos].X = _Radio * Math.Cos(arc);
                    //_ShowPoints[pos].X *= (_Radio - _LJV7Frame.buffer_compute[pos]) / _Radio;
                    //_ShowPoints[pos].Z = _Radio * Math.Sin(arc);
                    //_ShowPoints[pos].Z *= (_Radio - _LJV7Frame.buffer_compute[pos]) / _Radio;

                    /*
                    //原本程式
                    _ShowPoints[pos].X = (_Radio - _LJV7Frame.buffer_compute[pos]) * Math.Cos(diffPoint.C);
                    _ShowPoints[pos].Z = (_Radio - _LJV7Frame.buffer_compute[pos]) * Math.Sin(diffPoint.C);
                     * _ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / 2.0 - Convert.ToDouble(j)) / Convert.ToDouble(70)) + 2;
                     */


                    

                    _ShowPoints[pos].X = (Convert.ToDouble(i)) / Convert.ToDouble(250);
                    //_ShowPoints[pos].Z = (_Radio - _LJV7Frame.buffer_compute[pos]) - 800; // Convert.ToDouble(10);

                    _ShowPoints[pos].Z = ((_LJV7Frame.buffer_compute[pos])-55); // Convert.ToDouble(10);

                    /*_ShowPoints[pos].X = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Cos(arc);
                    _ShowPoints[pos].Z = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Sin(arc);*/
                    _ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / 2.0 - Convert.ToDouble(j))/ Convert.ToDouble(100));
                    if (i == 80)
                    {
                        ;
                    }
                    //_ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / 2.0 - (Convert.ToDouble(j)+2)) / Convert.ToDouble(100)) - 2;
                    //_ShowPoints[pos].Y = (_LJV7Frame.buffer_compute[pos] - _LJV7Frame.Min_Buffer_Compute) * 8;
                    //_ShowPoints[pos].Y = (_LJV7Frame.LJVcount/ Convert.ToDouble(400));


                    /*_ShowPoints[pos].X = (i/15);
                    _ShowPoints[pos].Z = (double)(j-288)/25;
                    _ShowPoints[pos].Y = (_LJV7Frame.buffer_compute[pos] - _LJV7Frame.Min_Buffer_Compute)*8;*/
                }
                if (k == 99)
                {
                    k = -1;
                    startp = point;
                }
            }

            SaveComputeDataToCSV();
            //Console.WriteLine("Max Distance = " + min_distance);
        }
        #endregion


        public void reBuild_Excel(double range_max, double range_min)
        {

            //double diff = _LJV7Frame.Max_Buffer_Compute - _LJV7Frame.Min_Buffer_Compute;

            double diff = 3.35440063;

            //double diff = _LJV7Frame.Max_Buffer_Compute - _LJV7Frame.Min_Buffer_Compute;
            if (range_max > 0 && range_min > 0)
            {
                diff = range_max - range_min;
            }
            else
            {
                range_max = _LJV7Frame.Max_Buffer_Compute;
                range_min = _LJV7Frame.Min_Buffer_Compute;
                diff = _LJV7Frame.Max_Buffer_Compute - _LJV7Frame.Min_Buffer_Compute;
            }

            //TsPointS startp = StartPoint;
            for (int i = 0, k = 0; i < _LJV7Frame.size_per_frame; i++, k++)
            {
                //TsPointS point = _PointList[i / _EACHSIZE];
                //Console.WriteLine("Angle=" + (startp.C + diffAngle * k));
                //double arc = DPoint.Angle2Arc(startp.C + ((point.C - startp.C) / _EACHSIZE) * k);

                for (int j = 0; j < _LJV7Frame.LJVcount; j++)
                {
                    int pos = Convert.ToInt32(i * _LJV7Frame.LJVcount + j);

                    //_ShowPoints[pos].C = arc;

                    if (_LJV7Frame.buffer_compute[pos] == -999 || _LJV7Frame.buffer_compute[pos] < range_min || _LJV7Frame.buffer_compute[pos] > range_max)
                    {
                        _ShowPoints[pos].X = _ShowPoints[pos].Y = _ShowPoints[pos].Z = -999;
                        continue;
                    }


                    double Color_H = (_LJV7Frame.buffer_compute[pos] - range_min) / diff;
                    Color_H = Color_H * (GLDrawObject.MaxColorH - GLDrawObject.MinColorH) + GLDrawObject.MinColorH;
                    GLDrawObject.HsvToRgb(Color_H, 1.0, 1.0, out _ShowPoints[pos].r, out _ShowPoints[pos].g, out _ShowPoints[pos].b);

                    _ShowPoints[pos].X = (Convert.ToDouble(i)) / Convert.ToDouble(250);
                    _ShowPoints[pos].Z = (_LJV7Frame.buffer_compute[pos])-55; // Convert.ToDouble(10);

                    /*_ShowPoints[pos].X = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Cos(arc);
                    _ShowPoints[pos].Z = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Sin(arc);*/
                    _ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / 2.0 - Convert.ToDouble(j)) / Convert.ToDouble(100));
                }
            }

            //SaveComputeDataToCSV();
        }

        #region With Move Laser
        public void reBuild_MoveLser(double range_max, double range_min)
        {
            if (_LJV7Frame.buffer == null || _LJV7Frame.buffer_compute == null || _ShowPoints == null) return;
            Console.WriteLine("build: r=" + _Radio + ", angle=" + StartPoint.C + ", center=(" + center.X + ", " + center.Y + ")");

            double Y_Conversion = Distance_Conversion(Y_Distance);

            double diff = _LJV7Frame.Max_Buffer_Compute - _LJV7Frame.Min_Buffer_Compute;
            if (range_max > 0 && range_min > 0) diff = range_max - range_min;
            else
            {
                range_max = _LJV7Frame.Max_Buffer_Compute;
                range_min = _LJV7Frame.Min_Buffer_Compute;
            }

            TsPointS startp = StartPoint;
            TsPointS diffPoint = new TsPointS();
            for (int i = 0, k = 0; i < _LJV7Frame.size_per_frame; i++, k++)
            {
                TsPointS point = _PointList[i / _EACHSIZE];
                diffPoint.X = startp.X + (point.X - startp.X) * k / _EACHSIZE;
                diffPoint.Y = startp.Y + (point.Y - startp.Y) * k / _EACHSIZE;
                diffPoint.C = DPoint.Angle2Arc(startp.C + ((point.C - startp.C) / _EACHSIZE) * k);

                double arc = DPoint.Angle2Arc(startp.C + ((point.C - startp.C) / _EACHSIZE) * k);
                for (int j = 0; j < _LJV7Frame.LJVcount; j++)
                {
                    int pos = Convert.ToInt32(i * _LJV7Frame.LJVcount + j);
                    _ShowPoints[pos].C = diffPoint.C;

                    if (_LJV7Frame.buffer_compute[pos] == -999 || _LJV7Frame.buffer_compute[pos] < range_min || _LJV7Frame.buffer_compute[pos] > range_max)
                    {
                        _ShowPoints[pos].X = _ShowPoints[pos].Y = _ShowPoints[pos].Z = -999;
                        continue;
                    }

                    double Color_H = (_LJV7Frame.buffer_compute[pos] - range_min) / diff;
                    Color_H = Color_H * (GLDrawObject.MaxColorH - GLDrawObject.MinColorH) + GLDrawObject.MinColorH;
                    GLDrawObject.HsvToRgb(Color_H, 1.0, 1.0, out _ShowPoints[pos].r, out _ShowPoints[pos].g, out _ShowPoints[pos].b);

                    //對應到新座標: 拉平座標(實際接收資料)
                    //_ShowPoints[pos].X = (Convert.ToDouble(i)) / Convert.ToDouble(100);
                    //_ShowPoints[pos].Z = _Radio - _LJV7Frame.buffer_compute[pos];+

                    /*
                    //原本程式
                    _ShowPoints[pos].X = (_Radio - _LJV7Frame.buffer_compute[pos]) * Math.Cos(diffPoint.C);
                    _ShowPoints[pos].Z = (_Radio - _LJV7Frame.buffer_compute[pos]) * Math.Sin(diffPoint.C);
                     */

                    //對應到新座標: 對應回現實座標
                    /*
                    _ShowPoints[pos].X = diffPoint.X - _LJV7Frame.buffer_compute[pos] * Math.Cos(diffPoint.C) - center.X;
                    _ShowPoints[pos].Z = diffPoint.Y - _LJV7Frame.buffer_compute[pos] * Math.Sin(diffPoint.C) - center.Y;
                    _ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / 2.0 - Convert.ToDouble(j)) / Convert.ToDouble(70)) + 2;
                    */


                    _ShowPoints[pos].X = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Cos(arc);
                    _ShowPoints[pos].Z = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Sin(arc);
                    _ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / Y_Conversion - Convert.ToDouble(j)) / Convert.ToDouble(100));

                    /*_ShowPoints[pos].X = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Cos(diffPoint.C);
                    _ShowPoints[pos].Z = (Normalization(_LJV7Frame.buffer_compute[pos])) * Math.Sin(diffPoint.C);
                    _ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / Y_Conversion - Convert.ToDouble(j)) / Convert.ToDouble(100)) + 2;*/
                    //_ShowPoints[pos].Y = ((_LJV7Frame.LJVcount / 2.0 - Convert.ToDouble(j)) / Convert.ToDouble(70)) + 2;


                }
                if (k == 99)
                {
                    k = -1;
                    startp = point;
                }
            }
        }
        #endregion
        #endregion

        #region Size Compute
        public static double CHECKANGLE = 0.5;
        public void doComputeMaxRadio()
        {
            if (_LJV7Frame.buffer == null || _LJV7Frame.buffer_compute == null || _ShowPoints == null) return;
            double[] avg_radio = new double[_LJV7Frame.LJVcount];
            DPoint center = new DPoint(0, 0);
            double max_distance = 0.0;
            int defaultoffset = Convert.ToInt32(_LJV7Frame.size_per_frame / 4);
            for (int i = 0, k = 0; i < _LJV7Frame.size_per_frame; i++, k++)
            {
                //都取第一個點判斷角度是否為對角即可
                int spos = Convert.ToInt32(i * _LJV7Frame.LJVcount);
                DPoint points = _ShowPoints[spos];
                if (points.X == -999) continue;

                //Console.WriteLine(i + ": Angle=" + DPoint.Arc2Angle(points.C));
                //尋找對角小於CHECKANGLE之位置
                int offset = i + defaultoffset;
                int checkangle = -1;
                Array.Clear(avg_radio, 0, avg_radio.Length);
                double diffangle_min = 99999;
                for (int j = 0; j < _LJV7Frame.size_per_frame / 2; j++)
                {
                    int epos = j + offset;
                    if (epos >= _LJV7Frame.size_per_frame)
                    {
                        epos -= Convert.ToInt32(_LJV7Frame.size_per_frame);
                    }
                    epos = Convert.ToInt32(epos * _LJV7Frame.LJVcount);
                    DPoint pointe = _ShowPoints[epos];
                    if (pointe.X == -999) continue;

                    double diffangle = Math.Abs(180 - Math.Abs(DPoint.Arc2Angle(pointe.C) - DPoint.Arc2Angle(points.C)));
                    if (diffangle <= diffangle_min) diffangle_min = diffangle;
                    //差距已經開始增加，表示不會找到更接近的對角
                    else if (diffangle > CHECKANGLE)
                    {
                        break;
                    }

                    //找到對角，開始計算平均
                    if (checkangle < 0 && diffangle < CHECKANGLE) checkangle = 0;
                    if (checkangle >= 0)
                    {
                        //對角大於CHECKANGLE，離開
                        if (diffangle > CHECKANGLE) break;
                        //加總距離
                        for (int v = 0; v < avg_radio.Length; v++)
                        {
                            if (_ShowPoints[spos + v].X == -999 || _ShowPoints[epos + v].X == -999) continue;
                            avg_radio[v] += DPoint.ComputeDistance(_ShowPoints[epos + v], _ShowPoints[spos + v]);
                        }
                        checkangle++;
                    }
                }
                if (checkangle <= 0)
                {
                    //Console.WriteLine(i + ": Angle=" + DPoint.Arc2Angle(points.C) + ", min=" + diffangle_min);
                    continue;
                }
                for (int v = 0; v < avg_radio.Length; v++)
                {
                    //計算平均
                    avg_radio[v] /= checkangle;
                    if (avg_radio[v] > max_distance) max_distance = avg_radio[v];
                }
            }
            Console.WriteLine("Max=" + max_distance);
        }
        #endregion

        #region OpenGL Draw
        public static void drawGL_FrameData_Triangle(SharpGL.OpenGL gl_object, ModelBuilder builder)
        {
            drawGL_FrameData_Triangle(gl_object, builder, 8);
            return;
        }
        
        public static void drawGL_FrameData_Triangle(SharpGL.OpenGL gl_object, ModelBuilder builder, int jumpFrame)
        {
            if (builder._ShowPoints == null || builder._ShowPoints.Length <= 0) return;

            gl_object.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);

            //可以不用每次都旋轉，可以滑鼠動作時變更即可
            //不影響效能
            gl_object.LoadIdentity();
            gl_object.Translate(GLDrawObject._LX, GLDrawObject._LY, GLDrawObject._LZ);
            gl_object.Rotate(GLDrawObject._RoX, 0.0, 1.0, 0.0);
            gl_object.Rotate(GLDrawObject._RoY, 1.0, 0.0, 0.0);
            gl_object.Rotate(GLDrawObject._RoZ, 0.0, 0.0, 1.0);

            for (int j = 0; j < builder._LJV7Frame.LJVcount - jumpFrame; j += jumpFrame)
            {
                gl_object.Begin(OpenGL.TRIANGLE_STRIP);
                for (int i = 0; i < builder._LJV7Frame.size_per_frame - jumpFrame; i += jumpFrame)
                {
                    int pos = Convert.ToInt32(i * builder._LJV7Frame.LJVcount + j);
                    if (builder._ShowPoints[pos].Z == -999 || builder._ShowPoints[pos + jumpFrame].Z == -999)
                    {
                        gl_object.End();
                        gl_object.Begin(OpenGL.TRIANGLE_STRIP);
                        continue;
                    }

                    DPoint point = builder._ShowPoints[pos];
                    gl_object.Color(point.r, point.g, point.b);
                    gl_object.Vertex(point.X, point.Y, point.Z);

                    point = builder._ShowPoints[pos + jumpFrame];
                    gl_object.Color(point.r, point.g, point.b);
                    gl_object.Vertex(point.X, point.Y, point.Z);

                    //Console.WriteLine("X:" + point.r);
                    //Console.WriteLine("Y:" + point.g);
                    //Console.WriteLine("Z:" + point.b);
                }
                gl_object.End();
            }

            GLDrawObject.drawAxis(gl_object);
            gl_object.Flush();
            return;
        }

        public static void drawGL_FrameData_Triangle_SAVE(ModelBuilder builder)
        {
            drawGL_FrameData_Triangle_SAVE(builder, 8);
            return;
        }


        int X_num, Y_num;
        public static void drawGL_FrameData_Triangle_SAVE(ModelBuilder builder, int jump)
        {
            //int aaaa = 0;
            
            /*float centerx = builder._LJV7Frame.size_per_frame ;
            float centery = builder.LJVcount ;*/

            //跳幅數
            //int jump = 2;
            //座標倍率
            //int DivValue = 10;
            //int DivValue_X = 20;
            
            //高度倍率
            //int Magnification = 20;
            Vector3 Point = new Vector3();
            Vector3 Point2 = new Vector3();
            Vector3 Point3 = new Vector3();
            Vector3 Point4 = new Vector3();

            for (int j = 0; j < builder.LJVcount - jump; j += jump)
            {
                //aaaa = 0;
                for (int i = 0; i < builder._LJV7Frame.size_per_frame - jump; i += jump)
                {


                    int pos = Convert.ToInt32(i * builder.LJVcount + j);
                    int pos1 = Convert.ToInt32((i + jump) * builder.LJVcount + j);
                    if (builder._ShowPoints[pos].Z != -999 && builder._ShowPoints[pos].Z != 0 &&
                        builder._ShowPoints[pos + jump].Z != -999 && builder._ShowPoints[pos + jump].Z != 0 &&
                        builder._ShowPoints[pos1].Z != -999 && builder._ShowPoints[pos1].Z != 0 &&
                        builder._ShowPoints[pos1 + jump].Z != -999 && builder._ShowPoints[pos1 + jump].Z != 0)
                    /*if (builder._ShowPoints[pos].X == -999 || builder._ShowPoints[pos].X == 0 ||
                        builder._ShowPoints[pos].Y == -999 || builder._ShowPoints[pos].Y == 0 ||
                        builder._ShowPoints[pos].Z == -999 || builder._ShowPoints[pos].Z == 0 ||
                        builder._ShowPoints[pos + jump].X == -999 || builder._ShowPoints[pos + jump].X == 0 ||
                        builder._ShowPoints[pos + jump].Y == -999 || builder._ShowPoints[pos + jump].Y == 0 ||
                        builder._ShowPoints[pos + jump].Z == -999 || builder._ShowPoints[pos + jump].Z == 0 ||
                        builder._ShowPoints[pos1].X == -999 || builder._ShowPoints[pos1].X == 0 ||
                        builder._ShowPoints[pos1].Y == -999 || builder._ShowPoints[pos1].Y == 0 ||
                        builder._ShowPoints[pos1].Z == -999 || builder._ShowPoints[pos1].Z == 0 ||
                        builder._ShowPoints[pos1 + jump].X == -999 || builder._ShowPoints[pos1 + jump].X == 0 ||
                        builder._ShowPoints[pos1 + jump].Y == -999 || builder._ShowPoints[pos1 + jump].Y == 0 ||
                        builder._ShowPoints[pos1 + jump].Z == -999 || builder._ShowPoints[pos1 + jump].Z == 0)
                    {
                        //gl_object.End();
                        //gl_object.Begin(OpenGL.TRIANGLE_STRIP);
                        //continue;
                    }
                    else*/
                    {
                        builder.X_num = pos;
                        builder.Y_num = pos1;


                        //一個面需要兩個三角形組成，兩個三角形需要六個點
                        //  @*****@            @
                        //  *    *           * *
                        //  *   *    +      *  *
                        //  *  *           *   *
                        //  * *           *    *
                        //  @            @*****@
                        ////////////////////////////////////////////////

                        //計算三角形座標點位
                        /*Vector3 Point = new Vector3();
                        Point.x = ((float)(i) - centerx) / (float)(DivValue);
                        Point.y = -((float)(j) - centery) / (float)(DivValue);
                        Point.z = (float)builder._ShowPoints[pos].Z * Magnification;

                        Vector3 Point2 = new Vector3();
                        Point2.x = ((float)(i) - centerx) / (float)(DivValue);
                        Point2.y = -((float)(j + jump) - centery) / (float)(DivValue);
                        Point2.z = (float)builder._ShowPoints[pos + jump].Z * Magnification;

                        Vector3 Point3 = new Vector3();
                        Point3.x = ((float)(i + jump) - centerx) / (float)(DivValue);
                        Point3.y = -((float)(j) - centery) / (float)(DivValue);
                        Point3.z = (float)builder._ShowPoints[pos1].Z * Magnification;

                        Vector3 Point4 = new Vector3();
                        Point4.x = ((float)(i + jump) - centerx) / (float)(DivValue);
                        Point4.y = -((float)(j + jump) - centery) / (float)(DivValue);
                        Point4.z = (float)builder._ShowPoints[pos1 + jump].Z * Magnification;*/

                        //Vector3 Point = new Vector3();
                        Point.x = (float)builder._ShowPoints[pos].X * 2;
                        Point.y = (float)builder._ShowPoints[pos].Y * 2;
                        Point.z = (float)builder._ShowPoints[pos].Z * 2;

                        //Vector3 Point2 = new Vector3();
                        Point2.x = (float)builder._ShowPoints[pos + jump].X * 2;
                        Point2.y = (float)builder._ShowPoints[pos + jump].Y * 2;
                        Point2.z = (float)builder._ShowPoints[pos + jump].Z * 2;

                        //Vector3 Point3 = new Vector3();
                        Point3.x = (float)builder._ShowPoints[pos1].X * 2;
                        Point3.y = (float)builder._ShowPoints[pos1].Y * 2;
                        Point3.z = (float)builder._ShowPoints[pos1].Z * 2;

                        //Vector3 Point4 = new Vector3();
                        Point4.x = (float)builder._ShowPoints[pos1 + jump].X * 2;
                        Point4.y = (float)builder._ShowPoints[pos1 + jump].Y * 2;
                        Point4.z = (float)builder._ShowPoints[pos1 + jump].Z * 2;

                        //新增點位至List
                        builder._LaserData.Add(Point);
                        builder._LaserData.Add(Point2);
                        builder._LaserData.Add(Point3);
                        builder._LaserData.Add(Point3);
                        builder._LaserData.Add(Point4);
                        builder._LaserData.Add(Point2);
                        /*if (aaaa == 0)
                        {
                            //X_num = j;
                            aaaa = 1;
                            //Console.WriteLine(X_num.ToString());
                            Point.x = (float)builder._ShowPoints[pos].X * 2;
                            Point.y = (float)builder._ShowPoints[pos].Y * 2;
                            Point.z = (float)builder._ShowPoints[pos].Z * 2;

                            Point2.x = (float)builder._ShowPoints[pos].X * 2;
                            Point2.y = (float)0;
                            Point2.z = (float)builder._ShowPoints[pos].Z * 2;

                            Point3.x = (float)builder._ShowPoints[pos1].X * 2;
                            Point3.y = (float)builder._ShowPoints[pos1].Y * 2;
                            Point3.z = (float)builder._ShowPoints[pos1].Z * 2;

                            Point4.x = (float)builder._ShowPoints[pos1].X * 2;
                            Point4.y = (float)0;
                            Point4.z = (float)builder._ShowPoints[pos1].Z * 2;

                            builder._LaserData.Add(Point);
                            builder._LaserData.Add(Point2);
                            builder._LaserData.Add(Point3);
                            builder._LaserData.Add(Point3);
                            builder._LaserData.Add(Point4);
                            builder._LaserData.Add(Point2);

                        }*/
                        


                    }
                }
                //Vector3 Point = new Vector3();
                /*Point.x = (float)builder._ShowPoints[builder.X_num + jump].X * 2;
                Point.y = (float)builder._ShowPoints[builder.X_num + jump].Y * 2;
                Point.z = (float)builder._ShowPoints[builder.X_num + jump].Z * 2;

                //Vector3 Point = new Vector3();
                Point2.x = (float)builder._ShowPoints[builder.X_num + jump].X * 2;
                Point2.y = (float)0;
                Point2.z = (float)builder._ShowPoints[builder.X_num + jump].Z * 2;

                //Vector3 Point = new Vector3();
                Point3.x = (float)builder._ShowPoints[builder.Y_num + jump].X * 2;
                Point3.y = (float)builder._ShowPoints[builder.Y_num + jump].Y * 2;
                Point3.z = (float)builder._ShowPoints[builder.Y_num + jump].Z * 2;

                //Vector3 Point = new Vector3();
                Point4.x = (float)builder._ShowPoints[builder.Y_num + jump].X * 2;
                Point4.y = (float)0;
                Point4.z = (float)builder._ShowPoints[builder.Y_num + jump].Z * 2;

                builder._LaserData.Add(Point);
                builder._LaserData.Add(Point2);
                builder._LaserData.Add(Point3);
                builder._LaserData.Add(Point3);
                builder._LaserData.Add(Point4);
                builder._LaserData.Add(Point2);*/


            }

            for (int i = 0; i < builder._LaserData.Count - 3; i += 3)
            {
                //每三個點轉換成三角形
                Triangle tmp = new Triangle();
                tmp.a.x = builder._LaserData[i].x;
                tmp.a.y = builder._LaserData[i].y;
                tmp.a.z = builder._LaserData[i].z;

                tmp.b.x = builder._LaserData[i + 1].x;
                tmp.b.y = builder._LaserData[i + 1].y;
                tmp.b.z = builder._LaserData[i + 1].z;

                tmp.c.x = builder._LaserData[i + 2].x;
                tmp.c.y = builder._LaserData[i + 2].y;
                tmp.c.z = builder._LaserData[i + 2].z;

                //新增至三角形List裡面
                builder._data3D.Add(tmp);
            }

            FileStream fs = new FileStream("new" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".STL", FileMode.CreateNew);
            List<byte> data = new List<byte>();

            //寫入檔名
            data.AddRange(Encoding.ASCII.GetBytes("solid new" + DateTime.Now.ToString("yyyyMMdd-HHmmss")));

            //不足80個byte,補齊至80個byte
            while (data.Count < 80) data.Add((byte)'\0');

            //寫入長度
            data.AddRange(BitConverter.GetBytes(builder._data3D.Count));

            Vector3 n = new Vector3();
            for (int i = 0; i < builder._data3D.Count; i++)
            {
                //兩組_data3D為同一個面，因此兩組_data3D法向量須相同
                if (i % 2 == 0)
                {
                    //計算法向量
                    n = builder.getNormalVector(builder._data3D[i]);
                }

                //法向量寫入
                data.AddRange(BitConverter.GetBytes(n.x));
                data.AddRange(BitConverter.GetBytes(n.y));
                data.AddRange(BitConverter.GetBytes(n.z));

                //三角形寫入
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].a.x));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].a.y));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].a.z));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].b.x));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].b.y));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].b.z));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].c.x));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].c.y));
                data.AddRange(BitConverter.GetBytes(builder._data3D[i].c.z));

                data.AddRange(new byte[2]);
            }
            fs.Write(data.ToArray(), 0, data.Count);
            data.Clear();
            fs.Close();
            
        }


        #endregion  

        /*public Vector3 getNormalVector_New(DPoint tri_1, DPoint tri_2, DPoint tri_3)
        {
            Vector3 v1 = new Vector3();
            v1.x = (float)(tri_3.X - tri_2.X);
            v1.y = (float)(tri_3.Y - tri_2.Y);
            v1.z = (float)(tri_3.Z - tri_2.Z);

            Vector3 v2 = new Vector3();
            v2.x = (float)(tri_1.X - tri_2.X);
            v2.y = (float)(tri_1.Y - tri_2.Y);
            v2.z = (float)(tri_1.Z - tri_2.Z);

            Vector3 vn = new Vector3();
            vn.x = (v1.y * v2.z) - (v2.y * v1.z);
            vn.y = (v1.z * v2.x) - (v2.z * v1.x);
            vn.z = (v1.x * v2.y) - (v2.x * v1.y);

            //(未知)實際應該除的數字
            vn.x /= 10000.0f;
            vn.y /= 10000.0f;
            vn.z /= 10000.0f;
            return vn;
        }*/

        //計算法向量
        private Vector3 getNormalVector(Triangle tri)
        {
            Vector3 v1 = new Vector3();
            v1.x = (tri.c.x - tri.b.x);
            v1.y = (tri.c.y - tri.b.y);
            v1.z = (tri.c.z - tri.b.z);

            Vector3 v2 = new Vector3();
            v2.x = (tri.a.x - tri.b.x);
            v2.y = (tri.a.y - tri.b.y);
            v2.z = (tri.a.z - tri.b.z);

            Vector3 vn = new Vector3();
            vn.x = (v1.y * v2.z) - (v2.y * v1.z);
            vn.y = (v1.z * v2.x) - (v2.z * v1.x);
            vn.z = (v1.x * v2.y) - (v2.x * v1.y);

            //(未知)實際應該除的數字
            vn.x /= 10000.0f;
            vn.y /= 10000.0f;
            vn.z /= 10000.0f;
            return vn;
        }

        private double Distance_Conversion(double Distance)//依機械手臂Y軸距離算出3D模型Y軸長度
        {
            return ((Distance * 0.0076923) + 0.1384635);
        }

        private double Calculate_Ratio(double Distance)//依最小值算出物件直徑為多少
        {
            return ((Distance * 12.07914973) - 615.1213363);
        }



        private void SaveComputeDataToCSV()
        {
            if (_LJV7Frame.buffer_compute == null) return;

            FileStream fs = new FileStream("LaserData" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < _LJV7Frame.size_per_frame; i++)
            {
                string StrData = "";
                for (int j = 0; j < _LJV7Frame.LJVcount; j++)
                {
                    StrData += _LJV7Frame.buffer_compute[i * _LJV7Frame.LJVcount + j].ToString() + ",";
                }
                if (StrData.Length > 0) StrData = StrData.Substring(0, StrData.Length - 1);
                sw.WriteLine(StrData);
            }
            //清空暫存
            sw.Flush();
            //關閉檔案
            sw.Close();
            fs.Close();
            ////

        }

    }
}

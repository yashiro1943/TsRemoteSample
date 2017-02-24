using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TsRemoteLib;

//for Thread
using System.Threading;

//for FILE
using System.IO;


using PHTools;
using KeyenceTools;
using ToshibaTools;

//for OpenGL
using SharpGL;

namespace TsRemoteSample
{

    public partial class MainForm : Form
    {
        /*public struct Vector3
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
        }*/


        int Count_aa;
        #region Form Funs
        Thread thread;
        public MainForm()
        {
            obj_LJV7 = new ObjectLJV7();
            obj_LJV7._receiveback += new ObjectLJV7.CallbackReceiveData(ReceiveData);
            obj_LJV7._errorback += new ObjectLJV7.CallbackError(CallbackError);

            InitializeComponent();

            thread = new Thread(moveAuto); //啟動Thread
            thread.Start();
            
        }

        private void writeConfig()
        {
            GlobalSetting._LJV7CommandPort = textBox_LJV7Port_CommandPort.Text;
            GlobalSetting._LJV7HighSpeedPort = textBox_LJV7Port_HighSpeedPort.Text;
            GlobalSetting._LJV7IP = textBox_LJV7IPAddr.Text;
            GlobalSetting._LJV7CallbackFrequency = textBox_LJV7CallbackFrequency.Text;

            GlobalSetting.writeConfig();
        }

        //關閉程式
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Auto_YorN = 0;
            TsRemote.stopControl();
            TsRemote = null;

            writeConfig();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int tmpType = GlobalSetting._DebugType & GlobalSetting.DEBUGTYPE.FRAME;
            this.TopMost = false;
            if (tmpType > 0)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = false;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
            }

            GlobalSetting.checkConfig();

            textBox_LJV7IPAddr.Text = GlobalSetting._LJV7IP;

            //設定語系
            Languages.init();
            Languages.readLanguage("en");
            setUILanguage("en");

            TsRemote._statusback += new ObjectTsRemote.CallbackReceiveStatusEvent(ReceivedStatusEvent);
            domainUpDown_Config.SelectedIndex = 0;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            tabControl_main.Height = this.ClientSize.Height * 9 / 10;
            tabControl_main.Width = this.ClientSize.Width - tabControl_main.Left-20;

            //設定元件位置
            tabControl_main.Location = new Point(
                (this.ClientSize.Width - tabControl_main.Size.Width) / 2,
                (this.ClientSize.Height - tabControl_main.Size.Height) / 2);
            btnCloseApp.Location = new Point(this.ClientSize.Width - btnCloseApp.Width - 20, 5);

            /*OpenGLCtrl_Orig.Top = 25;
            OpenGLCtrl_Orig.Height = tabControl_main.Height - 25 - OpenGLCtrl_Orig.Top * 2;
            OpenGLCtrl_Orig.Width = tabControl_main.Width - OpenGLCtrl_Orig.Left - OpenGLCtrl_Orig.Top;*/

            /*OpenGLCtrl_Orig_Auto.Top = 25;
            OpenGLCtrl_Orig_Auto.Height = tabControl_main.Height - 25 - OpenGLCtrl_Orig_Auto.Top * 2;
            OpenGLCtrl_Orig_Auto.Width = tabControl_main.Width - OpenGLCtrl_Orig_Auto.Left - OpenGLCtrl_Orig_Auto.Top;*/
        }

        private void setUILanguage(string lang_name)
        {
            GlobalSetting._theLang = Languages.getLang(lang_name);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Auto_System = 0;//將自動模式關閉
            this.Close();
        }
        #endregion

        #region Toshiba
        ObjectTsRemote TsRemote = new ObjectTsRemote();

        #region Controlls
        //P.4 Overvie
        //T為外部軸，目前沒有接
        private void btnStartControl_Click(object sender, EventArgs e)
        {
            if (TsRemote.startControl_Network(textBox_ToshibaIPAddr.Text, Convert.ToInt32(textBox_ToshibaPort.Text), 100, ObjectTsRemote.ALARMLEVEL.MessageOnly) == true)
            {
                //與LJV7-60相距55最佳

                GetPsnFbk();
                
                TsStatusMonitor status_m = TsRemote._Robot.GetStatusMonitor();
                numericUpDown_Override.Value = status_m.Override;

                //timerGetCoordinate.Enabled = true;

                btnStartControl.Visible = false;
                btnStopControl.Visible = true;

                btnStartControl_3D.Visible = false;
                btnStopControl_3D.Visible = true;
                
                //MessageBox.Show("連線成功");

                return;
            }

            string errmsg = "連線失敗\n";
            //已連線的情況下，可進行查詢錯誤可能原因
            errmsg += TsRemote.getStatusAll();

            TsRemote.stopControl();
            MessageBox.Show(errmsg);
        }

        private void btnStopControl_Click(object sender, EventArgs e)
        {
            TsRemote.stopControl();
            timerGetCoordinate.Enabled = false;

            btnStartControl.Visible = true;
            btnStopControl.Visible = false;

            btnStartControl_3D.Visible = true;
            btnStopControl_3D.Visible = false;
        }

        private void btnResetAlarm_Click(object sender, EventArgs e)
        {
            TsRemote.ResetAll();
            btnResetAlarm.BackColor = Color.Transparent;
            btnResetAlarm.ForeColor = Color.Black;
        }

        private void btnOverride_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.CONNECTED) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }
            try
            {
                TsRemote._Robot.SetOVRD(Convert.ToInt32(numericUpDown_Override.Value));
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnTest_Click: " + ex.Message);
            }
        }
        #endregion

        #region Moves
        private void btnStartMove_Click(object sender, EventArgs e)
        {
            
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                TsPointS point = new TsPointS();
                point.X = Convert.ToDouble(numericUpDownX.Value);
                point.Y = Convert.ToDouble(numericUpDownY.Value);
                point.Z = Convert.ToDouble(numericUpDownZ.Value);
                point.C = Convert.ToDouble(numericUpDownC.Value);
                //以下指定無用途
                //TsRemote._Robot.MvConfig = (ConfigS)domainUpDown_Config.SelectedIndex;
                TsRemote._Robot.Moves(point);
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnStartMove_Click: " + ex.Message);
            }
            GetPsnFbk();
        }



        private void btnAutoStartMove(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
                {
                    MessageBox.Show("尚未連線");
                    decimal aa=numericUpDownX.Value;
                    return;
                }

                try
                {

                    


                    //TsRemote._Robot.Stop = true;
                    //MessageBox.Show(TsRemote._Robot.GetStatus().ToString());
                    if (TsRemote._Robot.GetStatus().RunStatus == 0)
                    {
                        TsPointS point = new TsPointS();
                        point.X = Convert.ToDouble(numericUpDownX.Value);
                        point.Y = Convert.ToDouble(numericUpDownY.Value);
                        point.Z = Convert.ToDouble(numericUpDownZ.Value);
                        point.C = Convert.ToDouble(numericUpDownC.Value);
                        //以下指定無用途
                        //TsRemote._Robot.MvConfig = (ConfigS)domainUpDown_Config.SelectedIndex;
                        TsRemote._Robot.Moves(point);
                        Thread.Sleep(100);
                    }
                }
                catch (TsRemoteSException ex)
                {
                    //Error processing
                    Console.WriteLine("btnStartMove_Click: " + ex.Message);
                }
                GetPsnFbk();
            }
            else if (checkBox1.Checked == false)
            {
                return;
            }
        }

        private void btnBox_check(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                labWarning_Signs.Text = "此為自動移動模" + '\n' + "式，慎防撞機";
            }
            else if (checkBox1.Checked == false)
            {
                labWarning_Signs.Text = "";
            }
        }


        private class ThreadMoveJoint
        {
            int joint_no;
            double position;
            public TsRemoteS _Robot = null;
            private void moveJoint()
            {
                while (true)
                {
                    try
                    {
                        _Robot.Movea(joint_no, position);
                        break;
                    }
                    catch (TsRemoteSException ex)
                    {
                        //Error processing
                        Console.WriteLine("ThreadMoveJoint: " + ex.Message);
                    }
                }
            }

            public ThreadMoveJoint(int no, double pos, TsRemoteS robot)
            {
                joint_no = no;
                position = pos;
                _Robot = robot;
                Thread thread = new Thread(moveJoint); //啟動Thread
                thread.Start();
            }
        }
        private void btnStartMoveJoint_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                //無法一次操作4軸，成功執行該函式後，後續動作皆會被強制略過
                //因此須透過執行緒強制執行到成功為止
                //1軸: 基座上
                new ThreadMoveJoint(1, Convert.ToDouble(numericUpDown_J1.Value), TsRemote._Robot);
                //2軸: 橫桿
                new ThreadMoveJoint(2, Convert.ToDouble(numericUpDown_J2.Value), TsRemote._Robot);
                //3軸: 上下
                new ThreadMoveJoint(3, Convert.ToDouble(numericUpDown_J3.Value), TsRemote._Robot);
                //4軸: 旋轉
                new ThreadMoveJoint(4, Convert.ToDouble(numericUpDown_J4.Value), TsRemote._Robot);
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnStartMoveJoint_Click: " + ex.Message);
            }
        }

        private void btnDrawCycle_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                int movingStep = Convert.ToInt32(numericUpDown_drawAngle.Value);

                //設定位置為起始點
                TsPointS point = new TsPointS();
                point.X = Convert.ToDouble(numericUpDownX.Value);
                point.Y = Convert.ToDouble(numericUpDownY.Value);
                point.Z = Convert.ToDouble(numericUpDownZ.Value);
                point.C = Convert.ToDouble(numericUpDownC.Value);

                //設定中心位置
                double centerx = Convert.ToDouble(numericUpDown_CenterX.Value);
                double centery = Convert.ToDouble(numericUpDown_CenterY.Value);
                double radio = DPoint.ComputeDistance(point, new DPoint(centerx, centery));
                if (radio < 0) return;

                point.C = DPoint.ComputeDefaultAngle(point, new DPoint(centerx, centery), radio);
                TsRemote._Robot.Moves(point);

                for (int i = 0; i < 360; i += movingStep)
                {
                    string command = GetCommand_MOVEC(point, new DPoint(centerx, centery), radio, movingStep);
                    TsRemote.DirectDo(command, point);
                }

                //回存目前位置
                GetPsnFbk();
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnDrawCycle_Click: " + ex.Message);
            }
        }

        private void btnDrawArc_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                //設定位置為起始點
                TsPointS point = TsRemote._Robot.GetPsnFbkWork();
                point.X = Convert.ToDouble(numericUpDownX.Value);
                point.Y = Convert.ToDouble(numericUpDownY.Value);
                point.Z = Convert.ToDouble(numericUpDownZ.Value);

                //設定中心位置
                double centerx = Convert.ToDouble(numericUpDown_CenterX.Value);
                double centery = Convert.ToDouble(numericUpDown_CenterY.Value);

                double radio = DPoint.ComputeDistance(point, new DPoint(centerx, centery));
                if (radio < 0) return;
                TsRemote._Robot.Moves(point);

                string command = GetCommand_MOVEC(point, new DPoint(centerx, centery), radio, Convert.ToInt32(numericUpDown_arcAngle.Value));
                TsRemote.DirectDo(command, point);
                
                //回存目前位置
                GetPsnFbk();
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnDrawArc_Click: " + ex.Message);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                //設定位置為起始點
                TsPointS point = TsRemote._Robot.GetPsnFbkWork();
                point.X = Convert.ToDouble(numericUpDownX.Value);
                point.Y = Convert.ToDouble(numericUpDownY.Value);
                point.Z = Convert.ToDouble(numericUpDownZ.Value);

                point.C = 360;
                TsRemote.DirectDo("MOVE POINT(" + point.X.ToString("000.000") + ", " + point.Y.ToString("000.000") + ", " + point.Z.ToString("000.000") + ", " + point.C.ToString("000.000") + ") WITH SPEED=50");
                /*
                //設定中心位置
                double centerx = Convert.ToDouble(numericUpDown_CenterX.Value);
                double centery = Convert.ToDouble(numericUpDown_CenterY.Value);
                
                double radio = ComputeRadio(point, centerx, centery);
                if (radio < 0) return;
                double diff = ComputeDefaultAngle(point, centerx, centery, radio);
                double direct = 1;
                if (point.C - diff >= 360)
                {
                    diff += 360;
                    direct = -1;
                }
                point.C = diff;
                TsRemote._Robot.Moves(point);

                string command = GetCommand_MOVEC(ref point, centerx, centery, radio, direct*350);
                TsRemote.DirectDo(command, point);

                command = GetCommand_MOVEC(ref point, centerx, centery, radio, direct*-80);
                TsRemote.DirectDo(command, point);

                command = GetCommand_MOVEC(ref point, centerx, centery, radio, direct*180);
                TsRemote.DirectDo(command, point);

                command = GetCommand_MOVEC(ref point, centerx, centery, radio, direct*-90);
                TsRemote.DirectDo(command, point);
                */
                //回存目前位置
                GetPsnFbk();
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnTest_Click: " + ex.Message);
            }
        }

        private void GetPsnFbk()
        {
            TsPointS point = TsRemote._Robot.GetPsnFbkWork();
            numericUpDownX.Value = Convert.ToInt32(point.X);
            numericUpDownY.Value = Convert.ToInt32(point.Y);
            int tmp = Convert.ToInt32(point.Z);
            numericUpDownZ.Value = (tmp > numericUpDownZ.Maximum) ? numericUpDownY.Maximum : ((tmp < numericUpDownZ.Minimum) ? numericUpDownZ.Minimum : tmp);
            numericUpDownC.Value = Convert.ToInt32(point.C);
            numericUpDownT.Value = Convert.ToInt32(point.T);

            TsJointS joint = TsRemote._Robot.GetPsnFbkJoint();
            numericUpDown_J1.Value = Convert.ToInt32(joint.J1);
            numericUpDown_J2.Value = Convert.ToInt32(joint.J2);
            numericUpDown_J3.Value = Convert.ToInt32(joint.J3);
            numericUpDown_J4.Value = Convert.ToInt32(joint.J4);
            numericUpDown_J5.Value = Convert.ToInt32(joint.J5);
        }
        #endregion
        
        #region Mouse Controll
        private TsPointS backPoint = new TsPointS();
        private void panel_MouseCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0) return;
            if (MoveLock == true) return;
            backPoint.X = e.X;
            backPoint.Y = e.Y;
            label_MovePos.BackColor = Color.Red;
        }

        private bool MoveLock = false;
        private void panel_MouseCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0) return;
            if (MoveLock == true) return;
            try
            {
                MoveLock = true;
                timerGetCoordinate.Enabled = true;
                double diffx = e.X - backPoint.X;
                double diffy = e.Y - backPoint.Y;

                TsPointS point = TsRemote._Robot.GetPsnFbkWork();
                point.X += diffx;
                point.Y -= diffy;

                if (point.X >1000)
                {
                    point.X = 1000;
                }
                else if (point.X < -1000)
                {
                    point.X = -1000;
                }
                else if (point.Y > 1000)
                {
                    point.Y = 1000;
                }
                else if (point.Y < -1000)
                {
                    point.Y = -1000;
                }

                TsRemote._Robot.Moves(point);
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("panel_MouseCtrl_MouseUp: " + ex.Message);
            }

            GetPsnFbk();
            MoveLock = false;
            timerGetCoordinate.Enabled = false;
            label_MovePos.BackColor = Color.White;
        }

        private void panel_MouseCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveLock == true) return;
            label_MovePos.Location = new Point(e.X - label_MovePos.Width / 2, e.Y - label_MovePos.Height / 2);
        }

        private void btnCtrlByMouse_Click(object sender, EventArgs e)
        {
            panel_MouseCtrl.Visible = !panel_MouseCtrl.Visible;
        }
        #endregion  
        #endregion

        #region Drawing funcs

        private string GetCommand_MOVEC(TsPointS start, TsPointS center, double radio, double add_angle)
        {
            //計算中繼點
            start.C += add_angle / 2;
            start.X = center.X - radio * Math.Sin(DPoint.Angle2Arc(start.C));
            start.Y = center.Y + radio * Math.Cos(DPoint.Angle2Arc(start.C));
            string command = "MOVEC POINT (" + start.X.ToString("000.000") + ", " + start.Y.ToString("000.000") + ", " + start.Z.ToString("000.000") + ", " + start.C.ToString("000.000") + ") ";

            //計算終點
            start.C += add_angle / 2;
            start.X = center.X - radio * Math.Sin(DPoint.Angle2Arc(start.C));
            start.Y = center.Y + radio * Math.Cos(DPoint.Angle2Arc(start.C));
            command += " POINT (" + start.X.ToString("000.000") + ", " + start.Y.ToString("000.000") + ", " + start.Z.ToString("000.000") + ", " + start.C.ToString("000.000") + ")";
            Console.WriteLine(command);
            return command;
        }
        #endregion
        
        #region Logs
        private void ReceivedStatusEvent(TsStatusMonitor para)
        {
            //切換到畫面的執行緒，進行畫面更新
            object[] dataArray = new object[1];
            dataArray[0] = para;
            this.BeginInvoke(new ReceivedStatusEventDelegate(UpdateUI_ReceivedStatusEvent), dataArray);
        }

        private delegate void ReceivedStatusEventDelegate(TsStatusMonitor para);
        private void UpdateUI_ReceivedStatusEvent(TsStatusMonitor para)
        {
            if (para.EmergencyStop == 1 || para.SafetyStop == 1 || para.SafetySwitch == 1)
            {
                btnResetAlarm.BackColor = Color.Red;
                btnResetAlarm.ForeColor = Color.White;
            }
            else
            {
                btnResetAlarm.BackColor = Color.Transparent;
                btnResetAlarm.ForeColor = Color.Black;
            }

            try
            {
                if (textBox_Message.Text.Length > 1024) textBox_Message.Text = "";

                //P.37
                List<TsAlarm> alarms = TsRemote._Robot.GetCurrentAlarm();
                
                for (int i = 0; i < alarms.Count; i++)
                {
                    textBox_Message.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + alarms[i].AlarmMes + "(" + alarms[i].AlarmNo + ")\r\n" + textBox_Message.Text;
                }
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("UpdateUI_ReceivedStatusEvent: " + ex.Message);
            }
        }

        //command(Cmd): 目標座標
        //feedback(Fbk): 目前所在座標
        private void timerGetCoordinate_Tick(object sender, EventArgs e)
        {
            TsPointS point = TsRemote._Robot.GetPsnFbkWork();
            numericUpDownX.Value = Convert.ToInt32(point.X);
            numericUpDownY.Value = Convert.ToInt32(point.Y);
            int tmp = Convert.ToInt32(point.Z);
            numericUpDownZ.Value = (tmp > numericUpDownZ.Maximum) ? numericUpDownY.Maximum : ((tmp < numericUpDownZ.Minimum) ? numericUpDownZ.Minimum : tmp);
            numericUpDownC.Value = Convert.ToInt32(point.C);
            numericUpDownT.Value = Convert.ToInt32(point.T);
            
            toolStripStatusLabel_Content.Text = point.X.ToString("000.0000") + ", " + point.Y.ToString("000.0000") + ", " + point.Z.ToString("000.0000") + ", " + point.C.ToString("000.0000");
            
            //TsJointS joint = TsRemote._Robot.GetPsnFbkJoint();
            //toolStripStatusLabel_Content.Text = joint.J1.ToString("000.0000") + ", " + joint.J2.ToString("000.0000") + ", " + joint.J3.ToString("000.0000") + ", " + joint.J4.ToString("000.0000") + ", " + joint.J5.ToString("000.0000");
        }

        private void GetStatusAll_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.CONNECTED) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                MessageBox.Show(TsRemote.getStatusAll());
                TsStatusMonitor status_m = TsRemote._Robot.GetStatusMonitor();
                numericUpDown_Override.Value = status_m.Override;
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("GetStatusAll_Click: " + ex.Message);
            }
        }

        //command(Cmd): 目標座標
        //feedback(Fbk): 目前所在座標
        private void btnGetPosition_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.CONNECTED) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                TsPointS point = TsRemote._Robot.GetPsnFbkWork();
                MessageBox.Show(point.X + ", " + point.Y + ", " + point.Z + ", " + point.C);

                GetPsnFbk();
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnGetPosition_Click: " + ex.Message);
            }
        }

        private void btnClearMessage_Click(object sender, EventArgs e)
        {
            textBox_Message.Text = "";
        }
        #endregion  
        
        #region LJV7
        #region LJV7 Event handler
        //發生問題後呼叫
        private void CallbackError(Object sender, int message)
        {
            //切換到畫面的執行緒，進行畫面更新
            MethodInvoker mi = new MethodInvoker(this.UpdateUI_Error);
            this.BeginInvoke(mi, null);
        }
        private void UpdateUI_Error()
        {
        }

        //收到資料後呼叫
        private ModelBuilder builder = new ModelBuilder();

        public ModelBuilder builder1 = new ModelBuilder();
        public ModelBuilder builder2 = new ModelBuilder();

        private void ReceiveData(int[] buffer, uint profileSize, uint count)
        {
            int tmpStatus = obj_LJV7.status & (ObjectLJV7.STATUS.STARTHIGHTSPEED | ObjectLJV7.STATUS.CURRENTIMAGE);
            if (tmpStatus == 0) return;

            tmpStatus = obj_LJV7.status & ObjectLJV7.STATUS.CURRENTIMAGE;
            //只有即時影像
            if (tmpStatus > 0)
            {
                //繪製即時影像
                pictureBox_CurrentImage.Tag = buffer;
                //計算FPS
                CurrentCount += (int)count;
                //紀錄該筆資料結束時的位置資訊，及資料
                if (builder._Received == true)
                {
                    builder.Add(TsRemote._Robot.GetPsnFbkWork(), buffer);
                }
            }
            else { }
            
            //切換到畫面的執行緒，進行畫面更新
            MethodInvoker mi = new MethodInvoker(this.UpdateUI_Receive);
            this.BeginInvoke(mi, null);
            Application.DoEvents();
        }

        private void UpdateUI_Receive()
        {
            int tmpStatus = obj_LJV7.status & (ObjectLJV7.STATUS.STARTHIGHTSPEED | ObjectLJV7.STATUS.CURRENTIMAGE);
            if (tmpStatus == 0) return;

            tmpStatus = obj_LJV7.status & ObjectLJV7.STATUS.CURRENTIMAGE;
            //非即時模式，直接離開
            if (tmpStatus == 0) return;
            
            //繪製即時畫面
            int[] buffer = (int[])pictureBox_CurrentImage.Tag;

            pictureBox_CurrentImage.Image = GLDrawObject.getBitmap_buffer(buffer, FrameData_LJV7._PROFILESIZE,
                new Size(pictureBox_CurrentImage.Width, pictureBox_CurrentImage.Height));
            pictureBox1.Image = pictureBox_CurrentImage.Image;
            pictureBox_CurrentImage_Auto.Image = pictureBox_CurrentImage.Image;
            pictureBox_CurrentImage.Image = pictureBox_CurrentImage.Image;
        }
        #endregion
       
        #region LJV7 UI handler
        private ObjectLJV7 obj_LJV7 = null;
        private void btnCurrentImage_Click(object sender, EventArgs e)
        {
            if (doInitialize() == false) return;

            //固定80筆收一次即時資訊，頻率=雷射頻率/80
            if (obj_LJV7.StartCurrentImage(
                   Convert.ToUInt16(textBox_LJV7Port_HighSpeedPort.Text),
                   Convert.ToByte(textBox_LJV7StartProfileNo.Text), 80) == true)
            {
                btnCurrentImage_Stop.Visible = true;
                btnCurrentImage.Visible = false;
                CurrentCount = 0;
                timer_CurrentFPS.Enabled = true;
            }
        }

        private void btnCurrentImage_Stop_Click(object sender, EventArgs e)
        {
            int tmpStatus = obj_LJV7.status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
            if (tmpStatus > 0)
            {
                //正在執行run once
                obj_LJV7.status &= ~ObjectLJV7.STATUS.STARTHIGHTSPEED;
            }

            obj_LJV7.StopCurrentImage();
            obj_LJV7.doFinalize();

            btnCurrentImage_Stop.Visible = false;
            btnCurrentImage.Visible = true;
            CurrentCount = 0;
            timer_CurrentFPS.Enabled = false;
        }

        private void btnHighSpeedDataEthernetCommunicationInitalize_Click(object sender, EventArgs e)
        {
            obj_LJV7.HighSpeedDataEthernetCommunicationInitalize(Convert.ToUInt16(textBox_LJV7Port_HighSpeedPort.Text), 80);
            btnInitialize.Visible = false;
            btnHighSpeedDataEthernetCommunicationInitalize.Visible = false;
            btnStartHighSpeedDataCommunication.Visible = true;
            btnStopHighSpeedDataCommunication.Visible = false;
            btnHighSpeedDataCommunicationFinalize.Visible = true;
            btnFinalize.Visible = true;
        }
        private void StartHighSpeedDataCommunication(object sender, EventArgs e)
        {
            if (obj_LJV7.StartHighSpeedDataCommunication(Convert.ToByte(textBox_LJV7StartProfileNo.Text)) == true)
            {
                obj_LJV7.status |= ObjectLJV7.STATUS.CURRENTIMAGE;
                btnInitialize.Visible = false;
                btnHighSpeedDataEthernetCommunicationInitalize.Visible = false;
                btnStartHighSpeedDataCommunication.Visible = false;
                btnStopHighSpeedDataCommunication.Visible = true;
                btnHighSpeedDataCommunicationFinalize.Visible = true;
                btnFinalize.Visible = true;
            }
        }

        private void btnStopHighSpeedDataCommunication_Click(object sender, EventArgs e)
        {
            if (obj_LJV7.StopHighSpeedDataCommunication() == true)
            {
                obj_LJV7.status &= ~ObjectLJV7.STATUS.CURRENTIMAGE;
                btnInitialize.Visible = false;
                btnHighSpeedDataEthernetCommunicationInitalize.Visible = false;
                btnStartHighSpeedDataCommunication.Visible = true;
                btnStopHighSpeedDataCommunication.Visible = false;
                btnHighSpeedDataCommunicationFinalize.Visible = true;
                btnFinalize.Visible = true;
            }
        }
        private void btnHighSpeedDataCommunicationFinalize_Click(object sender, EventArgs e)
        {
            obj_LJV7.HighSpeedDataCommunicationFinalize();
            btnInitialize.Visible = false;
            btnHighSpeedDataEthernetCommunicationInitalize.Visible = true;
            btnStartHighSpeedDataCommunication.Visible = false;
            btnStopHighSpeedDataCommunication.Visible = false;
            btnHighSpeedDataCommunicationFinalize.Visible = false;
            btnFinalize.Visible = true;
        }

        //計算FPS用
        private int CurrentCount = 0;
        private void timer_CurrentFPS_Tick(object sender, EventArgs e)
        {
            label_CurrentFPS.Text = "FPS: " + CurrentCount.ToString();
            CurrentCount = 0;
        }

        private bool doInitialize()
        {
            string sIP = textBox_LJV7IPAddr.Text.Replace(" ", "");
            string[] sIPs = sIP.Split('.');
            if (sIPs.Length != 4)
            {
                MessageBox.Show("IP格式錯誤");
                return false;
            }

            try
            {
                byte[] byteIPs = new byte[] { Convert.ToByte(sIPs[0]), Convert.ToByte(sIPs[1]), Convert.ToByte(sIPs[2]), Convert.ToByte(sIPs[3]) };
                if (obj_LJV7.doInitialize(byteIPs, Convert.ToUInt16(textBox_LJV7Port_CommandPort.Text)) == false)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            if (doInitialize() == true)
            {
                MessageBox.Show(this, GlobalSetting._theLang["Machine Connect Success"]);
                btnInitialize.Visible = false;
                btnHighSpeedDataEthernetCommunicationInitalize.Visible = true;
                btnStartHighSpeedDataCommunication.Visible = false;
                btnStopHighSpeedDataCommunication.Visible = false;
                btnHighSpeedDataCommunicationFinalize.Visible = false;
                btnFinalize.Visible = true;
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (obj_LJV7.doFinalize() == true)
            {
                MessageBox.Show(this, GlobalSetting._theLang["Machine Disconnect Success"]);
                btnInitialize.Visible = true;
                btnHighSpeedDataEthernetCommunicationInitalize.Visible = false;
                btnStartHighSpeedDataCommunication.Visible = false;
                btnStopHighSpeedDataCommunication.Visible = false;
                btnHighSpeedDataCommunicationFinalize.Visible = false;
                btnFinalize.Visible = false;
            }
        }
        #endregion
        #endregion  

        #region 3D Model
        private void btnStart3DModel_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.CONNECTED) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }
            OpenGLCtrl_Orig.Tag = null;
            OpenGLCtrl_Orig_Auto.Tag = null;
            try
            {
                //設定位置為起始點(圓心)
                TsPointS point = TsRemote._Robot.GetPsnFbkWork();


                point.X = 7;
                point.Y = 266;
                builder.Y_Distance = Convert.ToInt32(point.Y);
                point.Z = 25;//大隻
                //point.Z = 52;//小隻
                //point.Z = 300;//小隻
                point.C = 0;
                TsRemote._Robot.SetOVRD(20);
                TsRemote._Robot.Move(point);

                //設定開始點
                builder.StartPoint = TsRemote._Robot.GetPsnFbkWork();
                builder.center.X = builder.StartPoint.X;
                builder.center.Y = builder.StartPoint.Y;
                builder._Radio = DPoint.ComputeDistance(builder.StartPoint, builder._LaserPos);
                point.C = 360;
                TsRemote._Robot.SetOVRD(5);
                Thread.Sleep(1000);

                if (obj_LJV7.StartHighSpeedDataCommunication(Convert.ToByte(textBox_LJV7StartProfileNo.Text)) == false)
                {
                    MessageBox.Show("雷射尚未開啟");
                    return;
                }

                obj_LJV7.status |= ObjectLJV7.STATUS.CURRENTIMAGE;
                btnStart3DModel.Text = "量測中，請勿動作";
                groupBox_Testing.Enabled = false;
                builder.Clear();

                builder._Received = true;
                TsRemote._Robot.Move(point);
                Thread.Sleep(100);
                builder._Received = false;
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnStart3DModel_Click: " + ex.Message);
            }

            obj_LJV7.StopHighSpeedDataCommunication();
            obj_LJV7.status &= ~ObjectLJV7.STATUS.CURRENTIMAGE;
            builder.doBuild();
            maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
            maskedTextBox_RangeMin.Text = builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000");
            OpenGLCtrl_Orig.Tag = builder;
            OpenGLCtrl_Orig_Auto.Tag = builder;

            builder.doComputeMaxRadio();

            btnStart3DModel.Text = "進行量測";
            groupBox_Testing.Enabled = true;


            //btnExportSTL.Enabled = true;
            //pictureBox_CurrentImage_Auto.Visible = false;
            //OpenGLCtrl_Orig_Auto.Visible = true;


            ThreeD_YorN = 1;
            //Console.WriteLine(builder.ToString());
        }

        private void btnStart3DModel_Laser_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }

            try
            {
                int movingStep = Convert.ToInt32(numericUpDown_drawAngle.Value);

                //設定位置為起始點
                TsPointS point = new TsPointS();
                point.X = Convert.ToDouble(numericUpDownX.Value);
                point.Y = Convert.ToDouble(numericUpDownY.Value);
                point.Z = Convert.ToDouble(numericUpDownZ.Value);
                point.C = Convert.ToDouble(numericUpDownC.Value);

                //設定中心位置
                double centerx = Convert.ToDouble(numericUpDown_CenterX.Value);
                double centery = Convert.ToDouble(numericUpDown_CenterY.Value);
                double radio = DPoint.ComputeDistance(point, new DPoint(centerx, centery));
                if (radio < 0) return;

                point.C = DPoint.ComputeDefaultAngle(point, new DPoint(centerx, centery), radio);
                TsRemote._Robot.SetOVRD(20);
                TsRemote._Robot.Moves(point);

                builder.StartPoint = TsRemote._Robot.GetPsnFbkWork();
                builder.center.X = centerx;
                builder.center.Y = centery;
                builder._Radio = DPoint.ComputeDistance(point, builder.center);
                TsRemote._Robot.SetOVRD(5);
                Thread.Sleep(1000);

                if (obj_LJV7.StartHighSpeedDataCommunication(Convert.ToByte(textBox_LJV7StartProfileNo.Text)) == false)
                {
                    MessageBox.Show("雷射尚未開啟");
                    return;
                }
                obj_LJV7.status |= ObjectLJV7.STATUS.CURRENTIMAGE;
                btnStart3DModel.Text = "量測中，請勿動作";
                groupBox_Testing.Enabled = false;
                builder.Clear();

                builder._Received = true;
                /*for (int i = 0; i < 360; i += movingStep)
                {
                    string command = GetCommand_MOVEC(point, new DPoint(centerx, centery), radio, movingStep);
                    TsRemote.DirectDo(command, point);
                }*/
                string command = GetCommand_MOVEC(point, new DPoint(centerx, centery), radio, movingStep);
                TsRemote.DirectDo(command, point);
                builder._Received = false;

                //回存目前位置
                GetPsnFbk();
            }
            catch (TsRemoteSException ex)
            {
                //Error processing
                Console.WriteLine("btnDrawCycle_Click: " + ex.Message);
            }

            obj_LJV7.StopHighSpeedDataCommunication();
            obj_LJV7.status &= ~ObjectLJV7.STATUS.CURRENTIMAGE;
            builder.doBuild();
            maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
            maskedTextBox_RangeMin.Text = builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000");
            OpenGLCtrl_Orig.Tag = builder;
            OpenGLCtrl_Orig_Auto.Tag = builder;

            //builder.doComputeMaxRadio();

            btnStart3DModel.Text = "進行量測";
            groupBox_Testing.Enabled = true;
        }

        private void btnCurrentImage_3D_Click(object sender, EventArgs e)
        {
            if (doInitialize() == false) return;
            if (obj_LJV7.HighSpeedDataEthernetCommunicationInitalize(Convert.ToUInt16(textBox_LJV7Port_HighSpeedPort.Text), ModelBuilder._EACHSIZE) == true)
            {
                //MessageBox.Show("開啟成功");
                btnCurrentImage_3D.Visible = false;
                btnCurrentImage_Stop_3D.Visible = true;
            }
        }

        private void btnCurrentImage_Stop_3D_Click(object sender, EventArgs e)
        {
            obj_LJV7.HighSpeedDataCommunicationFinalize();
            obj_LJV7.SetContinue(ObjectLJV7.TriggerMode.External);
            obj_LJV7.doFinalize();
            //MessageBox.Show("關閉成功");
            btnCurrentImage_3D.Visible = true;
            btnCurrentImage_Stop_3D.Visible = false;
        }

        private void btnShow3D_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = !pictureBox1.Visible;
            OpenGLCtrl_Orig.Visible = !OpenGLCtrl_Orig.Visible;
        }

        private void btnRstOpenGL_Click(object sender, EventArgs e)
        {
            GLDrawObject.ResetView();
        }

        private void btnChangeRange_Click(object sender, EventArgs e)
        {
            try
            {
                double min = Convert.ToDouble(maskedTextBox_RangeMin.Text);
                if (min < builder._LJV7Frame.Min_Buffer_Compute)
                {
                    maskedTextBox_RangeMin.Text = builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000");
                }
                else if (min > builder._LJV7Frame.Max_Buffer_Compute)
                {
                    maskedTextBox_RangeMin.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
                }

                double max = Convert.ToDouble(maskedTextBox_RangeMax.Text);
                if (max > builder._LJV7Frame.Max_Buffer_Compute)
                {
                    maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
                }
                else if (max < builder._LJV7Frame.Min_Buffer_Compute)
                {
                    maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
                }
                
                if (min >= max)
                {
                    maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
                    MessageBox.Show("範圍設定錯誤");
                    return;
                }

                builder.reBuild(max, min);
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnChangeRange_Click: " + ex.Message);
            }
        }

        int OOKK = 0;

        private void OpenGLCtrl_Orig_OpenGLDraw(object sender, PaintEventArgs e)
        {
            OpenGLCtrl ctl = (OpenGLCtrl)sender;
            ModelBuilder b = (ModelBuilder)ctl.Tag;
            if (b == null) return;
            if (OOKK == 0 && ThreeD_YorN==1)
            {
                ModelBuilder.drawGL_FrameData_Triangle(ctl.OpenGL, builder);
            }
            else if (ThreeD_YorN == 0) //如果不顯示3D 則清除畫面(此情形只有在自動模式 正在掃瞄時會關閉)
            {
                GLDrawObject.ResetView();
                return;
            }

        }
        
        private void OpenGLCtrl_Orig_OpenGLDraw_Auto(object sender, PaintEventArgs e)
        {
            OpenGLCtrl ctl = (OpenGLCtrl)sender;
            ModelBuilder b = (ModelBuilder)ctl.Tag;
            if (b == null) return;

            if (OOKK == 0 && ThreeD_YorN == 1)
            {
                ModelBuilder.drawGL_FrameData_Triangle(ctl.OpenGL, builder);
            }
            else if (OOKK == 1)
            {
                thread.Suspend(); 
                
                //將case設為0 讓自動先暫時停止
                //int Auto_case_new = Auto_case;
                //Auto_case = 0;
                ModelBuilder.drawGL_FrameData_Triangle_SAVE(builder); //儲存3D影像為STL
                //Thread.Sleep(100);
                Console.WriteLine("儲存完成");
                OOKK = 0;
                thread.Resume();  
                
                //儲存結束後 將自動繼續
               // Auto_case = Auto_case_new;
            }
            else if (ThreeD_YorN == 0)//如果不顯示3D 則清除畫面(此情形只有在自動模式 正在掃瞄時會關閉)
            {
                GLDrawObject.ResetView();
                return;
            }
        }
        #endregion

        #region OpenGLCtrl Controll
        private Point _BakMousePosition = new Point(0, 0);  //記錄初始游標位置
        private void OpenGLCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            OpenGLCtrl ctrl = (OpenGLCtrl)sender;
            if (ctrl.Tag == null) return;

            GLDrawObject._BakMousePosition = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                GLDrawObject._3DMoveStatus = GLDrawObject.MoveStatus.Rotate;
                GLDrawObject.Trans3D(OpenGLCtrl_Orig.OpenGL, e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                GLDrawObject._3DMoveStatus = GLDrawObject.MoveStatus.Shift;
            }
            ctrl.MouseMove += new MouseEventHandler(GLDrawObject.OpenGLCtrl_MouseMove);
            _BakMousePosition.X = e.X;
            _BakMousePosition.Y = e.Y;
        }

        private void OpenGLCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            OpenGLCtrl ctrl = (OpenGLCtrl)sender;
            if (ctrl.Tag == null) return;
            ctrl.MouseMove -= new MouseEventHandler(GLDrawObject.OpenGLCtrl_MouseMove);

            //沒有移動才判斷為點擊，置換換大小3D圖
            if (Math.Abs(_BakMousePosition.X - e.X) < 5 && Math.Abs(_BakMousePosition.Y - e.Y) < 5)
            {
                ;
            }
        }

        void OpenGLCtrl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (OpenGLCtrl_Orig.Tag == null && OpenGLCtrl_Orig_Auto.Tag == null)  return;
            GLDrawObject._LZ += e.Delta / 300.0f;
        }
        #endregion

        private void btnStopMove_Click(object sender, EventArgs e)
        {
            //string a = TsRemote.getStatusAll();
            //string a = TsRemote._Robot.GetStatus().RunStatus.ToString();
            
            if (btnStopMove.Text == "復歸" &&  TsRemote._Robot.GetStatusMonitor().ServoStatus == 0)
            {

                //取得機械手臂狀態
                /*TsRemote.getStatusAll();
                TsRemote._Robot.GetStatus();
                TsRemote._Robot.GetStatusAll();
                TsRemote._Robot.GetStatusMonitor();*/
                //取得機械手臂狀態
                //TsRemote._Robot.ServoOn();
                //TsRemote.setServoOn();

                if (TsRemote.startControl() == false)//可以開啟
                    return;
                //string a = TsRemote._Robot.GetStatusMonitor().ServoStatus.ToString();
                //Thread.Sleep(100); 
                btnStopMove.Text = "緊急停止";
                btnStopMove.BackColor = Color.Transparent;

                btnAuto.Text = "自動模式";
                btnAuto.BackColor = Color.Transparent;
                btnAuto.Enabled = true;
                //MessageBox.Show(a);
            }
            else if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }
            else if (btnStopMove.Text == "緊急停止")
            {
                //TsRemote._Robot.ResetMove();
                //TsRemote._Robot.ServoOff();
                Auto_case = "Waitting";     //等待不動作
                Auto_System = 0;

                TsRemote.setServoOff();//可以關閉

                //TsRemote.stopControl();//可以關閉


                //TsRemote._Robot.ProgramStop();
                Thread.Sleep(6000);

                //ProgramBreak()
                //TsRemote.stopControl();//可以停
                btnStopMove.Text = "復歸";
                btnStopMove.BackColor = Color.Red;

                pictureBox_CurrentImage_Auto.Visible = false;
                OpenGLCtrl_Orig_Auto.Visible = false;
                btnShow3D_Auto.Visible = false;
                btnExportSTL.Visible = false;
                btnStartMove.Visible = true;
                btnCtrlByMouse.Visible = true;
                btnAuto.Enabled = false;
                /*string a = TsRemote._Robot.GetStatusMonitor().ServoStatus.ToString();

                MessageBox.Show(a);*/
            }
            
            //TsRemote._Robot.WatchDogStop();
            //TsRemote._Robot.ProgramStop();

        }

        int Auto_YorN = 1;//關閉程式時設定為0 關閉Thread
        int Auto_System = 0;//是否開啟自動模式 0：關閉  1：開啟
        string Auto_case = "Waitting";//分多種動作
        int ThreeD_YorN = 0;

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) == 0)
            {
                MessageBox.Show("尚未連線");
                return;
            }
            
            if (btnAuto.Text == "自動模式")
            {
                pictureBox_CurrentImage_Auto.Visible = true;
                OpenGLCtrl_Orig_Auto.Visible = false;
                ThreeD_YorN = 0;
                //btnShow3D_Auto.Enabled = false;
                btnStartMove.Visible = false;
                btnCtrlByMouse.Visible = false;
                btnShow3D_Auto.Visible = true;
                btnExportSTL.Visible = true;
                btnAuto.Text = "手動模式";
                btnAuto.BackColor = Color.Red;
                Auto_case = "Reclaimer_Position";  //將自動模式的case設定為手臂到取料點並會開始循環
                Auto_System = 1;//將自動模式打開

                //開啟雷射用Srart
                if (doInitialize() == true)
                {
                    if (obj_LJV7.HighSpeedDataEthernetCommunicationInitalize(Convert.ToUInt16(textBox_LJV7Port_HighSpeedPort.Text), ModelBuilder._EACHSIZE) == true)
                    {
                        Console.WriteLine("雷射連線成功");
                    }
                    else
                    {
                        Console.WriteLine("雷射尚未開啟");
                    }
                }
                else
                {
                    Console.WriteLine("雷射連線失敗");
                }
                //開啟雷射用Srart
            }
            else if (btnAuto.Text == "手動模式")
            {
                //關閉雷射用Srart
                /*int tmpStatus = obj_LJV7.status & ObjectLJV7.STATUS.STARTHIGHTSPEED;
                if (tmpStatus > 0)
                {
                    //正在執行run once
                    obj_LJV7.HighSpeedDataCommunicationFinalize();
                    obj_LJV7.SetContinue(ObjectLJV7.TriggerMode.External);
                    obj_LJV7.doFinalize();
                }*/
                //obj_LJV7.StopCurrentImage();
                
                
                obj_LJV7.HighSpeedDataCommunicationFinalize();
                obj_LJV7.SetContinue(ObjectLJV7.TriggerMode.External);
                obj_LJV7.doFinalize();

                /*if (obj_LJV7.doFinalize() == true)
                {
                    Console.WriteLine("雷射已斷線");
                }
                else
                {
                    Console.WriteLine("雷射已連線");
                }*/
                //關閉雷射用End

                

                btnCurrentImage_Stop.Visible = true;
                btnCurrentImage.Visible = false;

                pictureBox_CurrentImage_Auto.Visible = false;

                OpenGLCtrl_Orig_Auto.Visible = false;
                btnShow3D_Auto.Text = "3D";
                btnStartMove.Visible = true;
                btnCtrlByMouse.Visible = true;
                btnShow3D_Auto.Visible = false;
                btnExportSTL.Visible = false;
                btnAuto.Text = "自動模式";
                btnAuto.BackColor = Color.Transparent;
                Auto_System = 0;//將自動模式關閉

            }
 
        }

        private void moveAuto()
        {
            TsPointS point = new TsPointS();
            while (Auto_YorN == 1)
            {
                //Console.WriteLine(obj_LJV7.);
                if ((TsRemote.ConnectStatus & ObjectTsRemote.CONNECTTYPE.WATCHDOG) > 0 && Auto_System ==1)
                {                    
                    switch (Auto_case)
                    {
                        case ("Waitting"):
                            {
                                //Console.WriteLine();
                                break;
                            }
                        case ("Reclaimer_Position"): //入料點
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    point.X = 300;
                                    point.Y = 200;
                                    point.Z = 300;
                                    point.C = 0;

                                    Robot_Moves(point);
                                    //TsRemote._Robot.Moves(point);
                                    Auto_case = "Reclaimer_Down";
                                }
                                break;
                            }
                        case ("Reclaimer_Down")://入料點下降
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    point.X = 300;
                                    point.Y = 200;
                                    point.Z = 100;
                                    point.C = 0;
                                    Robot_Moves(point);

                                    Auto_case = "Reclaimer";
                                }
                                break;
                            }
                        case ("Reclaimer"): //(取料)Reclaimer
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {

                                    //需要寫電磁鐵通電
                                    Console.WriteLine("電磁鐵通電");
                                    Thread.Sleep(1000);
                                    Auto_case = "Reclaimer_Up";
                                }
                                break;
                            }

                        case ("Reclaimer_Up")://入料點上升(如要搶ta time可刪除此步驟-須嚴防撞機可能)Stand-by Position
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    point.X = 300;
                                    point.Y = 200;
                                    point.Z = 300;
                                    point.C = 0;
                                    Robot_Moves(point);
                                    //TsRemote._Robot.Moves(point);
                                    Auto_case = "Detection_Position";
                                }
                                break;
                            }
                        case ("Detection_Position")://移動至檢測點Detection Position
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    point.X = 7;
                                    point.Y = 266;
                                    point.Z = 300;
                                    point.C = 0;
                                    Robot_Moves(point);
                                    //TsRemote._Robot.Moves(point);
                                    Auto_case = "Detection_Down";
                                }
                                break;
                            }
                        case ("Detection_Down")://檢測點下降Detection Down
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    point.X = 7;
                                    point.Y = 266;
                                    point.Z = 52;//小隻
                                    //point.Z = 25;//大隻

                                    //point.Z = 45;//上半部
                                    //point.Z = 35;//下半部

                                    //point.Z = 300;
                                    point.C = 0;
                                    Robot_Moves(point);
                                    //TsRemote._Robot.Moves(point);
                                    Auto_case = "Detection_Rotation_Detect";
                                }
                                break;
                            }
                        case ("Detection_Rotation_Detect")://檢測點旋轉Detection Rotation 並做雷射掃描 Detect
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    OpenGLCtrl_Orig_Auto.Tag = null;
                                    TsRemote._Robot.SetOVRD(35);
                                    ThreeD_YorN = 0;

                                    try
                                    {
                                        point.X = 7;
                                        point.Y = 266;
                                        point.Z = 52;//小隻
                                        //point.Z = 25;//大隻

                                        //point.Z = 45;//上半部
                                        //point.Z = 35;//下半部
                                        //point.Z = 300;
                                        point.C = 360;
                                        builder.Y_Distance = Convert.ToInt32(point.Y);
                                        //builder.Y_Distance = Convert.ToInt32(numericUpDown_Override.Value);
                                        
                                       
                                        builder.StartPoint = TsRemote._Robot.GetPsnFbkWork();
                                        builder.center.X = builder.StartPoint.X;
                                        builder.center.Y = builder.StartPoint.Y;
                                        builder._Radio = DPoint.ComputeDistance(builder.StartPoint, builder._LaserPos);
                                        //point.C = 360;

                                        Thread.Sleep(1000);


                                        if (obj_LJV7.StartHighSpeedDataCommunication(Convert.ToByte(textBox_LJV7StartProfileNo.Text)) == false)
                                        {
                                            MessageBox.Show("雷射尚未開啟");
                                            return;
                                        }
                                        obj_LJV7.status |= ObjectLJV7.STATUS.CURRENTIMAGE;
                                        /*btnStart3DModel.Text = "量測中，請勿動作";
                                        groupBox_Testing.Enabled = false;*/
                                        builder.Clear();

                                        builder._Received = true;

                                        Robot_Moves(point);
                                        //TsRemote._Robot.Move(point);
                                        Thread.Sleep(100);
                                        builder._Received = false;
                                    }
                                    catch (TsRemoteSException ex)
                                    {
                                        //Error processing
                                        Console.WriteLine("btnStart3DModel_Click: " + ex.Message);
                                    }
                                    
                                    obj_LJV7.StopHighSpeedDataCommunication();
                                    obj_LJV7.status &= ~ObjectLJV7.STATUS.CURRENTIMAGE;
                                    builder.doBuild();
                                    //maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
                                    //maskedTextBox_RangeMin.Text = builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000");

                                    Console.WriteLine(builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000"));
                                    Console.WriteLine(builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000"));
                                    //OpenGLCtrl_Orig.Tag = builder;
                                    
                                    pictureBox_CurrentImage_Auto.Tag = builder;

                                    builder1 = builder;
                                    /*OpenGLCtrl_Orig.Tag = builder;
                                    OpenGLCtrl_Orig_Auto.Tag = builder;
                                    builder.doComputeMaxRadio();*/

                                    //OpenGLCtrl_Orig.Tag = builder;
                                    
                                    //btnShow3D_Auto.Enabled = true;
                                    //Form.CheckForIllegalCrossThreadCalls = false;
                                    //ThreeD_YorN = 1;
                                    //TsRemote._Robot.SetOVRD((int)numericUpDown_Override.Value);
                                    //TsRemote._Robot.Moves(point);
                                    
                                    //ModelBuilder.drawGL_FrameData_Triangle_SAVE(builder); //儲存3D影像為STL
                                    Auto_case = "Detection_Up";
                                    //OOKK = 1;
                                }
                                break;
                            }
                        case ("Detection_Up")://檢測點上升Detection_Up
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    /*point.X = 7;
                                    point.Y = 266;
                                    point.Z = 300;
                                    point.C = 0;*/
                                    point.X = 7;
                                    point.Y = 266;
                                    point.Z = 52;//小隻
                                    //point.Z = 25;//大隻

                                    //point.Z = 45;//上半部
                                    //point.Z = 35;//下半部
                                    //point.Z = 300;
                                    point.C = 0;
                                    Robot_Moves(point);
                                    //TsRemote._Robot.Moves(point);
                                    //Thread.Sleep(20000);
                                    //Auto_case = "Discharge_Position";
                                    Auto_case = "1";
                                }
                                break;
                            }
                        case ("1")://檢測點上升Detection_Up
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    OpenGLCtrl_Orig_Auto.Tag = null;
                                    TsRemote._Robot.SetOVRD(35);
                                    ThreeD_YorN = 0;

                                    try
                                    {
                                        point.X = 7;
                                        point.Y = 266;
                                        point.Z = 52;//小隻
                                        //point.Z = 25;//大隻

                                        //point.Z = 45;//上半部
                                        //point.Z = 35;//下半部
                                        //point.Z = 300;
                                        point.C = 360;
                                        builder.Y_Distance = Convert.ToInt32(point.Y);
                                        //builder.Y_Distance = Convert.ToInt32(numericUpDown_Override.Value);


                                        builder.StartPoint = TsRemote._Robot.GetPsnFbkWork();
                                        builder.center.X = builder.StartPoint.X;
                                        builder.center.Y = builder.StartPoint.Y;
                                        builder._Radio = DPoint.ComputeDistance(builder.StartPoint, builder._LaserPos);
                                        //point.C = 360;

                                        Thread.Sleep(1000);


                                        if (obj_LJV7.StartHighSpeedDataCommunication(Convert.ToByte(textBox_LJV7StartProfileNo.Text)) == false)
                                        {
                                            MessageBox.Show("雷射尚未開啟");
                                            return;
                                        }
                                        obj_LJV7.status |= ObjectLJV7.STATUS.CURRENTIMAGE;
                                        /*btnStart3DModel.Text = "量測中，請勿動作";
                                        groupBox_Testing.Enabled = false;*/
                                        builder.Clear();

                                        builder._Received = true;

                                        Robot_Moves(point);
                                        //TsRemote._Robot.Move(point);
                                        Thread.Sleep(100);
                                        builder._Received = false;
                                    }
                                    catch (TsRemoteSException ex)
                                    {
                                        //Error processing
                                        Console.WriteLine("btnStart3DModel_Click: " + ex.Message);
                                    }

                                    obj_LJV7.StopHighSpeedDataCommunication();
                                    obj_LJV7.status &= ~ObjectLJV7.STATUS.CURRENTIMAGE;
                                    builder.doBuild();
                                    //maskedTextBox_RangeMax.Text = builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000");
                                    //maskedTextBox_RangeMin.Text = builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000");

                                    Console.WriteLine(builder._LJV7Frame.Max_Buffer_Compute.ToString("00.0000"));
                                    Console.WriteLine(builder._LJV7Frame.Min_Buffer_Compute.ToString("00.0000"));
                                    //OpenGLCtrl_Orig.Tag = builder;
                                    builder2 = builder;
                                    
                                    //TsRemote._Robot.Moves(point);

                                    //ModelBuilder.drawGL_FrameData_Triangle_SAVE(builder); //儲存3D影像為STL
                                    
                                    //OOKK = 1;
                                    Count_aa = 0;
                                    for (int aa = 0; aa < (builder2._LJV7Frame.count * builder2._LJV7Frame.LJVcount); aa++)
                                    {
                                        /*if (builder1._LJV7Frame.buffer_compute[aa] == -999 && builder2._LJV7Frame.buffer_compute[aa] != -999)
                                        {
                                            ;
                                        }
                                        else if (builder1._LJV7Frame.buffer_compute[aa] != -999 && builder2._LJV7Frame.buffer_compute[aa] == -999)
                                        {
                                            ;
                                        }
                                        else if (builder1._LJV7Frame.buffer_compute[aa] == 0 && builder2._LJV7Frame.buffer_compute[aa] != 0)
                                        {
                                            ;
                                        }
                                        else if (builder1._LJV7Frame.buffer_compute[aa] != 0 && builder2._LJV7Frame.buffer_compute[aa] == 0)
                                        {
                                            ;
                                        }
                                        else if (builder1._LJV7Frame.buffer_compute[aa] == builder2._LJV7Frame.buffer_compute[aa])
                                        {
                                            Count_aa++;
                                        }*/
                                        if (builder1._LJV7Frame.buffer_compute[aa] == -999)
                                        {
                                            builder._LJV7Frame.buffer_compute[aa] = builder2._LJV7Frame.buffer_compute[aa];
                                        }
                                        else if (builder2._LJV7Frame.buffer_compute[aa] == -999)
                                        {
                                            builder._LJV7Frame.buffer_compute[aa] = builder1._LJV7Frame.buffer_compute[aa];
                                        }
                                        if (builder1._LJV7Frame.buffer_compute[aa] == 0)
                                        {
                                            ;
                                        }
                                        else if (builder2._LJV7Frame.buffer_compute[aa] == 0)
                                        {
                                            ;
                                        }
                                        if (builder1._LJV7Frame.buffer_compute[aa] == builder2._LJV7Frame.buffer_compute[aa])
                                        {
                                            Count_aa++;
                                        }

                                        builder._LJV7Frame.buffer_compute[aa] = (builder1._LJV7Frame.buffer_compute[aa] + builder2._LJV7Frame.buffer_compute[aa]) / 2;

                                    }

                                    pictureBox_CurrentImage_Auto.Tag = builder;
                                    

                                    OpenGLCtrl_Orig.Tag = builder;
                                    OpenGLCtrl_Orig_Auto.Tag = builder;
                                    builder.doComputeMaxRadio();

                                    //OpenGLCtrl_Orig.Tag = builder;

                                    //btnShow3D_Auto.Enabled = true;
                                    //Form.CheckForIllegalCrossThreadCalls = false;
                                    ThreeD_YorN = 1;
                                    TsRemote._Robot.SetOVRD((int)numericUpDown_Override.Value);
                                    Auto_case = "Discharge_Position";
                                }
                                break;
 
                            }
                        
                        case ("Discharge_Position")://移動至出料位置Discharging position
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    /*point.X = 300;
                                    point.Y = 400;
                                    point.Z = 300;
                                    point.C = 0;

                                    Robot_Moves(point);
                                    Auto_case = "Discharge_Down";*/
                                    //Auto_case = "Discharge_Position";
                                }
                                break;
                            }
                        case ("Discharge_Down")://出料位置下降Discharge Down
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    point.X = 300;
                                    point.Y = 400;
                                    point.Z = 100;
                                    point.C = 0;

                                    Robot_Moves(point);

                                    Auto_case = "Discharge";

                                }
                                break;
                            }
                        case ("Discharge")://出料Discharge
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {
                                    //需要寫電磁鐵斷電
                                    Console.WriteLine("電磁鐵斷電");

                                    Thread.Sleep(1000);
                                    Auto_case = "Discharge_Up";
                                }
                                break;
                            }

                        case ("Discharge_Up")://出料位置上升Discharge Up
                            {
                                if (TsRemote._Robot.GetStatus().RunStatus == 0)//判斷手臂無動作時進入
                                {                               
                                    point.X = 300;
                                    point.Y = 400;
                                    point.Z = 300;
                                    point.C = 0;

                                    Robot_Moves(point);
                                    Auto_case = "Reclaimer_Position";
                                }
                                break;
                            }
                    }
                }
                Thread.Sleep(10);
            }
        }

        //手臂移動使用
        private void Robot_Moves(TsPointS point)
        {
            try
            {
                TsRemote._Robot.Moves(point);
            }
            catch
            {
                //Error processing
                Console.WriteLine("機械手臂異常停止");
            }
        }

        //顯示3D或2D
        private void btnShow3D_Auto_Click(object sender, EventArgs e)
        {
            if (btnShow3D_Auto.Text == "3D")
            {
                if (ThreeD_YorN == 0)
                {

                    return;
                }
                else
                {
                    btnExportSTL.Enabled = true;
                    pictureBox_CurrentImage_Auto.Visible = false;
                    OpenGLCtrl_Orig_Auto.Visible = true;
                    btnShow3D_Auto.Text = "2D波形";
                }
            }
            else if (btnShow3D_Auto.Text == "2D波形")
            {
                btnExportSTL.Enabled = false;
                pictureBox_CurrentImage_Auto.Visible = true;
                OpenGLCtrl_Orig_Auto.Visible = false;
                btnShow3D_Auto.Text = "3D";
            }
        }


        private void btnExportSTL_Click(object sender, EventArgs e)
        {    
            OOKK = 1;
        }
    }
}

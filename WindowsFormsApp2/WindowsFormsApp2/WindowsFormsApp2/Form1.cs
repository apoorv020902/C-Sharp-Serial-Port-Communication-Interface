using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string dataOUT;
        string sendWith;
        string DataIn;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            chBoxDtrEnable.Checked = false;
            serialPort1.DtrEnable = false;
            chBoxRtsEnable.Checked = false;
            serialPort1.RtsEnable = false;
            btnSendData.Enabled = false;
            chBoxWriteLine.Checked = false;
            chBoxWrite.Checked = true;
            chBoxProbe1.Checked= false;
            sendWith = "Write";

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {   try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDATABITS.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxSTOPBITS.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxPARITYBITS.Text);

                serialPort1.Open();
                progressBar1.Value = 100;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                lblStatusCom.Text = "ON";
            }
            catch (Exception err)
            { 
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatusCom.Text = "OFF";
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatusCom.Text = "OFF";
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                if(sendWith == "WriteLine")
                {
                    serialPort1.WriteLine(dataOUT + Environment.NewLine);
                }
                else if (sendWith == "Write")
                {
                    serialPort1.Write(dataOUT + Environment.NewLine);
                }
                tBoxDataOut.Clear();
            }

            int one, two, three, four, five, six, dec = 0;
            
            if (chBoxProbe1.Checked == true)
            {
                one = 1;
                dec = dec + one;
            }
            if(chBoxProbe2.Checked == true)
            {
                two = 2;
                dec = dec + two;
            }
            if(chBoxProbe3.Checked == true)
            {
                three = 4;
                dec = dec + three;
            }
            if (chBoxProbe4.Checked == true)
            {
                four = 8;
                dec = dec + four;
            }
            if (chBoxProbe5.Checked == true)
            {
                five = 16;
                dec = dec + five;
            }
            if (chBoxProbe6.Checked == true)
            {
                six = 32;
                dec = dec + six;
            }

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("pmon " + dec.ToString("X") + Environment.NewLine);
            }




            /* Console.WriteLine(""+one+two+three+four+five+six);

            StringBuilder Probes_Binary_String = new StringBuilder();
            Probes_Binary_String.Append(eight); Probes_Binary_String.Append(seven); Probes_Binary_String.Append(six); Probes_Binary_String.Append(five); Probes_Binary_String.Append(four); Probes_Binary_String.Append(three); Probes_Binary_String.Append(two); Probes_Binary_String.Append(one);
            string Probe_Binary_String_2 = Probes_Binary_String.ToString();
            Console.WriteLine(Probe_Binary_String_2);

            int Probes_Binary_Int;
            bool success = int.TryParse(Probe_Binary_String_2, out Probes_Binary_Int);

            //int Probes_Binary_Int = int.TryParse(Probe_Binary_String_2);


            //double Probes_Binary_Int = double.Parse(Probe_Binary_String_2);


            Console.WriteLine(Probes_Binary_Int);


            //string hexadecimalNumber = Convert.ToString(Probes_Binary_Int, 16);

            //byte[] bytes = BitConverter.GetBytes(Probes_Binary_Int);
            //string hexadecimalNumber = BitConverter.ToString(bytes).Replace("-", string.Empty);

            string hexadecimalNumber = Probes_Binary_Int.ToString("X8");
            Console.WriteLine(hexadecimalNumber);

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("pmon "+hexadecimalNumber + Environment.NewLine);
            }
            */

        }



        private void chBoxDtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxDtrEnable.Checked)
            {
                serialPort1.DtrEnable = true;
            }
            else
            { 
                serialPort1.DtrEnable = false;
            }
        }

        private void chBoxRTSEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxRtsEnable.Checked)
            {
                serialPort1.RtsEnable = true;
            }
            else
            {
                serialPort1.RtsEnable = false;
            }
        }

        private void btnClearDataOut_Click(object sender, EventArgs e)
        {
            if (tBoxDataOut.Text != "")
            {
                tBoxDataOut.Text = "";
            }
        }

        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int dataOUTLength = tBoxDataOut.TextLength;
            lblDataOutLength.Text = string.Format("{0:00}", dataOUTLength);
            if (chBoxUsingEnter.Checked)
            {
                tBoxDataOut.Text = tBoxDataOut.Text.Replace(Environment.NewLine, "");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxUsingButton.Checked)
            {
                btnSendData.Enabled = true;
            }
            else { btnSendData.Enabled = false; }
        }

        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (chBoxUsingEnter.Checked)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    if (serialPort1.IsOpen)
                    {
                        dataOUT = tBoxDataOut.Text;
                        if (sendWith == "WriteLine")
                        {   
                            serialPort1.WriteLine(dataOUT + Environment.NewLine);
                        }
                        else if (sendWith == "Write")
                        {
                            serialPort1.Write(dataOUT + Environment.NewLine);
                        }
                        tBoxDataOut.Clear();
                    }
                }
            }
        }

        private void chBoxWriteLine_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxWriteLine.Checked) 
            {
                sendWith = "WriteLine";
                chBoxWrite.Checked = false;
                chBoxWriteLine.Checked = true;
            }
        }

        private void chBoxWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxWrite.Checked)
            {
                sendWith = "Write";
                chBoxWrite.Checked = true;
                chBoxWriteLine.Checked = false;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataIn = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            tBoxDataIN.Text = DataIn;
        }

        private void cbBoxAlwaysUpdate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tBoxDataIN_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void chBoxProbe1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chBoxProbe2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chBoxProbe3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

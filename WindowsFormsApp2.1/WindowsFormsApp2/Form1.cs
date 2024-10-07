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
using System.Security.Policy;

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

            serialPort1.DtrEnable = true;
            serialPort1.RtsEnable = true;
            btnSendData.Enabled = true;
            chBoxProbe1.Checked= false;
            sendWith = "Write";

            toolStripComboBox1.Text = "Add to Old Data";

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDATABITS.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxSTOPBITS.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxPARITYBITS.Text);

                serialPort1.Open();
                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                serialPort1.Write(dataOUT + Environment.NewLine);
                tBoxDataOut.Clear();
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
            */

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
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
        }

        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {

                if(e.KeyCode == Keys.Enter)
                {
                    this.doSomething();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
        }

        private void doSomething()
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                serialPort1.WriteLine(dataOUT + Environment.NewLine);
                tBoxDataOut.Clear();
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataIn = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {   
            int dataINLength = DataIn.Length;
            lblDataInLength.Text = string.Format("{0:00}", dataINLength);

            if(toolStripComboBox1.Text == "Always Update")
            {
                tBoxDataIN.Text = DataIn;
            }
            if (toolStripComboBox1.Text == "Add to Old Data")
            {
                //tBoxDataIN.Text += DataIn;
                tBoxDataIN.Text = tBoxDataIN.Text.Insert(0, DataIn);
            }
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

        private void transmitterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tBoxDataIN.Text != "")
            {
                tBoxDataIN.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int one, two, three, four, five, six, dec = 0;

            if (chBoxProbe1.Checked == true)
            {
                one = 1;
                dec = dec + one;
            }
            if (chBoxProbe2.Checked == true)
            {
                two = 2;
                dec = dec + two;
            }
            if (chBoxProbe3.Checked == true)
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
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Apoorv Rana for Neogen Corporation", "About", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cBoxCOMPORT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
        }
    }
}

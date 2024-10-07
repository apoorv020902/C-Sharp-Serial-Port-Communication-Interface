// Import necessary namespaces for this application
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports; // Provides communication with serial ports
using System.Security.Policy; // Provides classes for security policy
using System.IO; // Provides basic file and directory operations
using System.Security.Cryptography; // Provides cryptographic algorithms

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {   

        string dataOUT;
        string sendWith;
        string DataIn;

        StreamWriter objStreamWriter;
        //static path for text file:
        //string pathfile = @"C:\Users\apoor\Downloads\codebase\GenieData.txt";

        //dynamic path for text file:
        //will initiliaze in Form1_Load Event Handler
        string pathfile;

        bool state_AppendText = true;

        public Form1()
        {   
            // Initialize and set up the components and controls on the form

            InitializeComponent();
        }

        // Event handler for when the form (Form1) is loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

            //initializing varibles
            serialPort1.DtrEnable = true;
            serialPort1.RtsEnable = true;
            btnSendData.Enabled = true;
            chBoxProbe1.Checked= false;
            sendWith = "Write";

            toolStripComboBox1.Text = "Add to Old Data";
            toolStripComboBox_AppendOrOverwriteText.Text = "Append Text";
            toolStripComboBox_WriteLineOrWriteText.Text = "WriteLine";

            //get path to current directory and save in string pathfile
            pathfile = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            pathfile += @"\DataOutputFiles\GenieData.txt";

            Console.WriteLine("------ Path for the data output text file: ------");
            Console.WriteLine(pathfile);
        }
        // Event handler for the "Open" menu item click
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {   // Configure serial port settings based on user input
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

        // Event handler for the "Close" menu item click
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }
        // Event handler for the "Send Data" button click
        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                serialPort1.Write(dataOUT + Environment.NewLine);
                tBoxDataOut.Clear();
            }
        }
        // Event handler for the "Clear" menu item click
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tBoxDataOut.Text != "")
            {
                tBoxDataOut.Text = "";
            }
        }
        // Event handler for when the text in the data output textbox changes
        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int dataOUTLength = tBoxDataOut.TextLength;
            lblDataOutLength.Text = string.Format("{0:00}", dataOUTLength);
        }

        // Event handler for key press events in the data output textbox
        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {

                if(e.KeyCode == Keys.Enter)
                {
                    this.Enter_Pressed();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
        }

        private void Enter_Pressed()
        {
            if (serialPort1.IsOpen)
            {   //Writing Data in the Textbox to the Serial Port
                dataOUT = tBoxDataOut.Text;
                serialPort1.WriteLine(dataOUT + Environment.NewLine);
                tBoxDataOut.Clear();
            }
        }
        // Event handler for when data is received on the serial port
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {  
            DataIn = serialPort1.ReadExisting();
            // Invoke the method to display the received data on the UI thread
            this.Invoke(new EventHandler(ShowData));
        }
        // Event handler to display data
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
                tBoxDataIN.Text = tBoxDataIN.Text.Insert(0, DataIn);
            }

            try
            {   // Create a StreamWriter for writing data to a file

                objStreamWriter = new StreamWriter(pathfile, state_AppendText);
                if(toolStripComboBox_WriteLineOrWriteText.Text == "WriteLine")
                {
                    objStreamWriter.WriteLine(DataIn);
                }
                else if(toolStripComboBox_WriteLineOrWriteText.Text == "Write")
                {
                    objStreamWriter.Write(DataIn + " ");
                }
                objStreamWriter.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        // Event handler for the "Clear" menu item click
        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tBoxDataIN.Text != "")
            {
                tBoxDataIN.Text = "";
            }
        }

        // Event handler for the "Start Monitoring" button click

        private void button1_Click(object sender, EventArgs e)
        {   //Declaring and initializing integer variables one through six and dec to 0
            int one, two, three, four, five, six, dec = 0;

            //checking state of check boxes
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
            //checking if serial port is Open
            if (serialPort1.IsOpen)
            {   //writing command to the serial port
                //pmon is the syntax of command to monitor data from probes
                //  --dec.ToString("X")-- converts decimal number stored in 'dec' to hexadecimal
                serialPort1.WriteLine("pmon " + dec.ToString("X") + Environment.NewLine);
            }
        }

        // Event handler for the "Stop Monitoring" button click
        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                int dec = 0;
                serialPort1.WriteLine("pmon " + dec.ToString("X") + Environment.NewLine);
            }
        }

        // Event handler for the "About" menu item click
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Apoorv Rana and Andrew Schnepp for Neogen Corporation\n\n" +
                "Select the Com Port from the COM PORT drop down\n"+
                "File: Save to text file settings and Exit\n"+
                "Com Control: Open and Close COM PORT\n"+
                "Transmitter: Send Data Textbox settings\n"+
                "Receiver: Receive Data Textbox settings\n"+
                "Select the Probes and Click Start Monitoring to start\n"+
                "Click Stop Monitoring to Stop\n\n"+
                "- The Readme.txt file is located in the same directory as the software\n"+
                "- To access the text file containing saved data from the probes: \n"+
                "Open Main Directory--> 'WindowsFormsApp2' --> 'DataOutputFiles' --> 'GenieData.txt'\n",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Event handler for when the dropdown of the "Append or Overwrite Text" tool strip combo box is closed
        private void toolStripComboBox_AppendOrOverwriteText_DropDownClosed(object sender, EventArgs e)
        {
            if (toolStripComboBox_AppendOrOverwriteText.Text == "Append Text")
            {
                state_AppendText = true;
            }
            else
            {
                state_AppendText = false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e){}
        private void groupBox7_Enter(object sender, EventArgs e){}
        private void label7_Click(object sender, EventArgs e){}
        private void label6_Click(object sender, EventArgs e){}
        private void tBoxDataIN_TextChanged(object sender, EventArgs e){}
        private void groupBox1_Enter(object sender, EventArgs e){}
        private void chBoxProbe1_CheckedChanged(object sender, EventArgs e){}
        private void chBoxProbe2_CheckedChanged(object sender, EventArgs e){}
        private void chBoxProbe3_CheckedChanged(object sender, EventArgs e){}
        private void fileToolStripMenuItem_Click(object sender, EventArgs e){}
        private void showDataToolStripMenuItem_Click(object sender, EventArgs e){}
        private void transmitterToolStripMenuItem_Click(object sender, EventArgs e){}
        private void progressBar1_Click(object sender, EventArgs e){}
        private void cBoxCOMPORT_SelectedIndexChanged(object sender, EventArgs e){}
        private void saveDataTotxtFileToolStripMenuItem_Click(object sender, EventArgs e){}

        // Event handler for the "Exit" menu item click
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

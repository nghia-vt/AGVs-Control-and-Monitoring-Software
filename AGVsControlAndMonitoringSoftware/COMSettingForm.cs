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

namespace AGVsControlAndMonitoringSoftware
{
    public partial class COMSettingForm : Form
    {
        public COMSettingForm()
        {
            InitializeComponent();
        }

        public static bool btnConnClicked = false;

        private void COMSettingForm_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbbCOMPort.Items.AddRange(ports);

            if (Communicator.SerialPort.IsOpen)
            {
                btnConnect.Text = "Disconnect";
                btnConnect.ImageIndex = 1;
                btnConnect.ForeColor = Color.Brown;
                btnReScan.Enabled = false;
                cbbCOMPort.Text = Communicator.SerialPort.PortName;
                cbbBaudRate.Text = Communicator.SerialPort.BaudRate.ToString();
                cbbDataBits.Text = Communicator.SerialPort.DataBits.ToString();
                cbbStopBits.Text = Communicator.SerialPort.StopBits.ToString();
                cbbParity.Text = Communicator.SerialPort.Parity.ToString();
            }
            else
            {
                btnConnect.Text = "Connect";
                btnConnect.ImageIndex = 0;
                btnConnect.ForeColor = Color.MediumBlue;
                btnReScan.Enabled = true;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                btnConnect.Text = "Disconnect";
                btnConnect.ImageIndex = 1;
                btnConnect.ForeColor = Color.Brown;
                btnReScan.Enabled = false;
                try
                {
                    Communicator.SerialPort.PortName = cbbCOMPort.Text;
                    Communicator.SerialPort.BaudRate = Convert.ToInt32(cbbBaudRate.Text);
                    Communicator.SerialPort.DataBits = Convert.ToInt32(cbbDataBits.Text);
                    Communicator.SerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbbStopBits.Text);
                    Communicator.SerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), cbbParity.Text);

                    Communicator.SerialPort.Open();
                    btnConnClicked = true;

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnConnect.Text = "Connect";
                    btnConnect.ImageIndex = 0;
                    btnConnect.ForeColor = Color.MediumBlue;
                    btnReScan.Enabled = true;

                    btnConnClicked = false;

                    Display.UpdateComStatus("status", 0, "Access to the port " + Communicator.SerialPort.PortName + " is denied", 
                                            System.Drawing.Color.Red);
                }
            }
            else
            {
                string mess = "This action will stop monitoring, controlling & detecting collision.\nMake sure AGVs have no task.\nDo you really want to Disconnect ?";
                DialogResult result = MessageBox.Show(mess, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    btnConnect.Text = "Connect";
                    btnConnect.ImageIndex = 0;
                    btnConnect.ForeColor = Color.MediumBlue;
                    btnReScan.Enabled = true;

                    Communicator.SerialPort.Close();
                    btnConnClicked = false;

                    Display.UpdateComStatus("status", 0, Communicator.SerialPort.PortName + " is closed", System.Drawing.Color.Red);
                }
            }

            // Send AGV Info Init/Request to AGV (except Line tracking error)
            if (Communicator.SerialPort.IsOpen)
            {
                Display.UpdateComStatus("status", 0, Communicator.SerialPort.PortName + " is opened", System.Drawing.Color.Blue);
                foreach (AGV agv in AGV.ListAGV)
                {
                    if (agv.IsInitialized == true) Communicator.SendAGVInfoRequest((uint)agv.ID, 'A');
                    else Communicator.SendAGVInitRequest((uint)agv.ID);
                }
            }
        }

        private void btnReScan_Click(object sender, EventArgs e)
        {
            cbbCOMPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbCOMPort.Items.AddRange(ports);
        }
    }
}

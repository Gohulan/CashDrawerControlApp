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
using System.Security;

namespace CashDrawerControlApp
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;

        public Form1()
        {
            InitializeComponent();
            InitializeSerialPort();
        }

        private void InitializeSerialPort()
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM4"; // Change this to match your actual COM port
            _serialPort.BaudRate = 9600;   // Adjust baud rate if needed
            
            _serialPort.Open();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenCashDrawer();


        }

        private void OpenCashDrawer()
        {
            try
            {
                // Send a command to open the cash drawer
                _serialPort.Write("Open"); 
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening cash drawer: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBoxStatus.Items.Clear(); // Clear the ListBox before updating

            string[] portNames = SerialPort.GetPortNames(); // Get names of all available COM ports

            foreach (string portName in portNames)
            {
                try
                {
                    using (SerialPort port = new SerialPort(portName))
                    {
                        port.Open(); // Attempt to open the port
                        listBoxStatus.Items.Add($"Port {portName}: Open");
                        port.Close(); // Close the port after checking
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    listBoxStatus.Items.Add($"Port {portName}: Access denied"); // Port is in use by another application
                }
                catch (Exception ex)
                {
                    listBoxStatus.Items.Add($"Port {portName}: {ex.Message}"); // Other exceptions
                }
            }
        }

    }
}


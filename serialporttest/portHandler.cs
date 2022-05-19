using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serialporttest
{
    class portHandler
    {

        private static SerialPort port;
        private BackgroundWorker background = new BackgroundWorker();
        private Queue<string> queue = new Queue<string>();

        public portHandler(SerialPort _port)
        {
            _port = port;
            _port.DataReceived += _port_DataReceived;
            background.DoWork += Background_DoWork;
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = port.ReadExisting();
            queue.Enqueue(str);
        }

        private void Background_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!port.IsOpen)
                {
                    Console.WriteLine("not open");
                    port.Open();
                }
                SpinWait.SpinUntil(() => false, 10); 
            }
        }

        public void Run()
        {
            background.RunWorkerAsync();
        }
    }
}

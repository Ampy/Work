using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RTSafe.RTDP.Messaging;

namespace MPTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgEntry ms = new MsgEntry();
            ms.Message ="测试！";
            ms.CreateTime = DateTime.Now;
            ms.Receiver = "JLX";
            ms.Sender = "MP";
            ms.Severity = System.Diagnostics.TraceEventType.Information;
            ms.Priority = 0;
            ms.Categories.Add("SMS");
            ms.Categories.Add("EMAIL");
            Messager.Write(ms);
        }
    }
}

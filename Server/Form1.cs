using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        Socket client;
        IPEndPoint clientep;
        byte[] data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(4);
            client = server.Accept();
            //lay dia chi cua EndPoint(Cleint)
            clientep = (IPEndPoint)client.RemoteEndPoint;
            textBox1.Text = clientep.Address.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            data = new byte[1024];
            int rec = client.Receive(data);
            string stringData = Encoding.UTF8.GetString(data, 0, rec);
            string sgui = ("Client: " + stringData);
            listBox1.Items.Add(sgui);
            byte[] gui = Encoding.UTF8.GetBytes(textBox2.Text);
            client.Send(gui, gui.Length, SocketFlags.None);
            string stringGui = ("Server: " + textBox2.Text);
            listBox1.Items.Add(stringGui);
            textBox2.Clear();
        }
    }
}

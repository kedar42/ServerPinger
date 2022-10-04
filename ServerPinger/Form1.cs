using System.Net.NetworkInformation;

namespace ServerPinger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            Ping myPing = new Ping();

            foreach (string line in textBox1.Text.Split("\n"))
            {
                if (line.Trim() == "")
                {
                    continue;
                }

                try
                {
                    PingReply reply = myPing.Send(line.Trim(), 100);
                    textBox2.Text += line + " : " + reply.Status + System.Environment.NewLine;

                }
                catch
                {
                    textBox2.Text += line + " does not exist" + System.Environment.NewLine;
                }
            }
        }
    }
}

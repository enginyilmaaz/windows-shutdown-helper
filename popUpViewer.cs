using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsShutdownHelper.functions;

namespace WindowsShutdownHelper
{
    public partial class popUpViewer : Form
    {
        public static language language = languageSelector.languageFile();
        public static Timer timer = new Timer();
        public static int x;
        public static int y;
        public static int showTimeSecond;

        public popUpViewer(string messageTitle, string messageContent, int _showTimeSecond, Image messageIconFile,
            int _x, int _y, int _width,
            int _height)
        {
            InitializeComponent();
            showTimeSecond = 0;
            x = _x + (_width - Width) / 2;
            y = _y + (_height - Height) / 2;
            pictureBox_main.Image = messageIconFile;
            label_content.Text = messageContent;
            label_title.Text = messageTitle;
            showTimeSecond = _showTimeSecond;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void popUpViewer_Load(object sender, EventArgs e)
        {
            Location = new Point(x, y);
            button_OK.Text = language.popupViewer_button_ok;
            timer.Interval = showTimeSecond * 1000; // 3 sec
            timer.Tick += timerTick;
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            timer.Stop();

            GC.Collect();
            GC.SuppressFinalize(this);
            Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            Close();
        }

        private void label_content_Click(object sender, EventArgs e)
        {
        }

        private void label_title_Click(object sender, EventArgs e)
        {
        }
    }
}
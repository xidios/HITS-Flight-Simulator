using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HITS_Flight_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double dt;
        const double g = 9.8;
        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;
    
        private void button1_Click(object sender, EventArgs e)
        {
            time_timer.Text = $"Времени прошло {t}";
            a = (double)edAngle.Value * Math.PI / 180;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;
            dt = 0.01;
            t = 0;
            x = 0;
            y = y0;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);
            timer1.Start();
            double max_height = v0*v0 * Math.Sin(a) * Math.Sin(a) / (2 * g)+2+y0;
            double max_lenght = v0 * v0 * Math.Sin(2*a) /  g + 2;
            chart1.ChartAreas[0].AxisY.Maximum = max_height;
            chart1.ChartAreas[0].AxisX.Maximum = max_lenght;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            time_timer.Text = $"Времени прошло {t} с";
            t += dt;
            x = v0 * Math.Cos(a) * t;
            y = y0 + v0 * Math.Sin(a) * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0) timer1.Stop();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled)
            {
                timer1.Stop();
                button2.Text = "Continue";
            }
            else if (!timer1.Enabled && y>0)
            {
                timer1.Start();
                button2.Text = "Stop";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bachelier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tb_0.Text = "0";
            tb_D.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // cumulative normal distribution (CND)
            static double CND(double d)
            {
                const double A1 = 0.31938153;
                const double A2 = -0.356563782;
                const double A3 = 1.781477937;
                const double A4 = -1.821255978;
                const double A5 = 1.330274429;
                const double RSQRT2PI = 0.39894228040143267793994605993438;
                double K = 1.0 / (1.0 + 0.2316419 * Math.Abs(d));
                double CND = RSQRT2PI * Math.Exp(-0.5 * d * d) * (K * (A1 + K * (A2 + K * (A3 + K * (A4 + K * A5)))));

                if (d > 0)
                    CND = 1.0 - CND;
                if (d == 0)
                    CND = 0.5;
                return CND;
            }

            static double CNDEV(double U)
            {
                double tmp = 1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-0.5 * U * U);
                return tmp;
            }


            if (tb_K.Text == "" || tb_st.Text == "" || tb_D.Text == "" || tb_T.Text == "" || tb_sig.Text == "" || tb_0.Text == "" || tb_r.Text == "")
            {
                MessageBox.Show("内容不能为空，请重新输入!", "警告!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string s1 = tb_st.Text.Trim();
            string s2 = tb_K.Text.Trim();
            string s3 = tb_r.Text.Trim();
            string s4 = tb_sig.Text.Trim();
            string s5 = tb_T.Text.Trim();
            string s6 = tb_0.Text.Trim();
            string s7 = tb_D.Text.Trim();
 

            double temp1 = 0, temp2 = 0, temp3 = 0, temp4 = 0, temp5 = 0, temp6 = 0, temp7 = 0;
            if (!double.TryParse(s1, out temp1) || !double.TryParse(s2, out temp2) || !double.TryParse(s3, out temp3)
                    || !double.TryParse(s4, out temp4) || !double.TryParse(s5, out temp5) || !double.TryParse(s6, out temp6)
                    || !double.TryParse(s7, out temp7))
            {
                MessageBox.Show("输入的内容只能包含数字和小数点!", "警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (temp3 <= 0 || temp4 <= 0 || temp5 <= 0)
            {
                MessageBox.Show("输入有误！输入的内容必须为正值，请重新输入。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double D = Convert.ToDouble(tb_D.Text);
            double r = Convert.ToDouble(tb_r.Text);
            double S = Convert.ToDouble(tb_st.Text);
            double vol = Convert.ToDouble(tb_sig.Text); 
            double t = Convert.ToDouble(tb_0.Text);
            double T = Convert.ToDouble(tb_T.Text);
            double K = Convert.ToDouble(tb_K.Text);

            double tmp1 = Math.Exp(-2 * D * (T - t)) - Math.Exp(-2 * r * (T - t));

            double sigma_hat = vol * Math.Sqrt(tmp1 / (2 * (r - D)));

            double tmp2 = S * Math.Exp(-D * (T - t)) - K * Math.Exp(-r * (T - t));

            double d = tmp2 / sigma_hat;

            double call_price = tmp2 * CND(d) + sigma_hat * CNDEV(d);
            double put_price = call_price - tmp2;

            tb_dd.Text = Convert.ToString(d);
            tb_sighat.Text = Convert.ToString(sigma_hat);
            tb_C.Text = Convert.ToString(call_price);
            tb_P.Text = Convert.ToString(put_price);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Control[] list = { tb_st, tb_K, tb_T, tb_r, tb_sig, tb_C, tb_P,tb_sighat, tb_dd };
            foreach (Control i in list)
            {
                i.Text = "";
            }
            tb_0.Text = "0";
            tb_D.Text = "0";

        }
    }
}

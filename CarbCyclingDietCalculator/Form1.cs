using System;
using System.Windows.Forms;

namespace CarbCyclingDietCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) //關閉按鈕的動作
        {
            this.Close();
            Environment.Exit(Environment.ExitCode);

            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //計算的動作
        {
            try
            {
                bool sex = radioButton1.Checked; //True為男性 False為女性
                double height = Convert.ToDouble(textBox1.Text) / 100;//身高單位為公尺
                double weight = Convert.ToDouble(textBox2.Text);
                double fatrate = Convert.ToDouble(textBox3.Text) / 100;//介於0.0~1.0之間
                double FFMI, BMR;
                double weightwithoutfat = weight * (1 - fatrate);
                int age = Convert.ToInt32(textBox17.Text); //年齡

                double[,] highday = new double[2, 3];
                double[,] lowday = new double[2, 3];
                //[0,-]是增肌期 [1,-]是減脂期
                //[-,0]是碳水化合物 [-,1]是蛋白質 [-,2]是脂肪

                double[] TDEE = new double[3];
                //[0]是原始 [1]是增肌期 [2]是減脂期

                FFMI = (weight * (1 - fatrate)) / Math.Pow(height, 2);
                if (height > 1.8)
                {
                    FFMI = FFMI + (6 * (height - 1.8));
                }
                FFMI = Math.Round(FFMI, 1);
                textBox4.Text = Convert.ToString(FFMI);

                if (sex)
                {
                    BMR = 13.7 * weight + 5 * 100 * height - 6.8 * age + 66;
                }
                else
                {
                    BMR = 9.6 * weight + 1.8 * 100 * height - 4.7 * age + 655;
                }
                BMR = Math.Round(BMR, 0);
                textBox18.Text = Convert.ToString(BMR);

                switch (comboBox1.SelectedItem)
                {
                    case "很少運動":
                        TDEE[0] = BMR * 1.2;
                        break;
                    case "每週運動≦3天":
                        TDEE[0] = BMR * 1.375;
                        break;
                    case "每週運動≦5天":
                        TDEE[0] = BMR * 1.55;
                        break;
                    case "每天運動":
                        TDEE[0] = BMR * 1.725;
                        break;
                    default:
                        TDEE[0] = BMR * 1.9;
                        break;
                }
                TDEE[1] = TDEE[0] * 1.05;//增肌期攝取熱量(+5%)
                TDEE[2] = TDEE[0] * 0.8;//減脂期攝取熱量(-20%)
                TDEE[0]= Math.Round(TDEE[0], 0);
                textBox19.Text = Convert.ToString(TDEE[0]);

                if (sex) //男性攝取比例算法
                {
                    if (FFMI < 20)
                    {
                        if (fatrate < 0.16)
                        {
                            highday[0, 1] = 2.3 * weightwithoutfat;
                            highday[1, 1] = 2.6 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                        else if (fatrate < 0.19)
                        {
                            highday[0, 1] = 2.2 * weightwithoutfat;
                            highday[1, 1] = 2.5 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                        else
                        {
                            highday[0, 1] = 2.1 * weightwithoutfat;
                            highday[1, 1] = 2.4 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }
                    else if (FFMI < 21)
                    {
                        if (fatrate < 0.15)
                        {
                            highday[0, 1] = 2.5 * weightwithoutfat;
                            highday[1, 1] = 2.8 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                        else
                        {
                            highday[0, 1] = 2.4 * weightwithoutfat;
                            highday[1, 1] = 2.7 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }
                    else if (FFMI < 23)
                    {
                        if (fatrate < 0.14)
                        {
                            highday[0, 1] = 2.6 * weightwithoutfat;
                            highday[1, 1] = 2.9 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                        else
                        {
                            highday[0, 1] = 2.5 * weightwithoutfat;
                            highday[1, 1] = 2.8 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }
                    else
                    {
                        if (fatrate < 0.11)
                        {
                            highday[0, 1] = 2.6 * weightwithoutfat;
                            highday[1, 1] = 2.9 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                        else
                        {
                            highday[0, 1] = 2.2 * weightwithoutfat;
                            highday[1, 1] = 2.8 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }
                    highday[0, 2] = weight * 1.2;//增肌期高碳日脂肪量
                    highday[0, 0] = (TDEE[1] - (highday[0, 1] * 4 + highday[0, 2] * 9)) / 4;//增肌期高碳日碳水量
                    lowday[0, 0] = 50;//增肌期低碳日碳水量
                    lowday[0, 2] = (TDEE[1] - (lowday[0, 0] * 4 + lowday[0, 1] * 4)) / 9;//增肌期低碳日脂肪量

                    highday[1, 2] = weight * 1.2;//減脂期高碳日脂肪量
                    highday[1, 0] = (TDEE[2] - (highday[1, 1] * 4 + highday[1, 2] * 9)) / 4;//減脂期高碳日碳水量
                    lowday[1, 0] = 50;//減脂期低碳日碳水量
                    lowday[1, 2] = (TDEE[2] - (lowday[1, 0] * 4 + lowday[1, 1] * 4)) / 9;//減脂期低碳日脂肪量
                }
                else //女性攝取比例算法
                {
                    if (FFMI < 15) 
                    {
                        if (fatrate < 0.19) 
                        {
                            highday[0, 1] = 2.4 * weightwithoutfat;
                            highday[1, 1] = 2.7 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }else if (fatrate < 0.21) 
                        {
                            highday[0, 1] = 2.3 * weightwithoutfat;
                            highday[1, 1] = 2.6 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }else
                        {
                            highday[0, 1] = 2.2 * weightwithoutfat;
                            highday[1, 1] = 2.5 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }else if (FFMI < 17) 
                    {
                        if (fatrate < 0.20) 
                        {
                            highday[0, 1] = 2.5 * weightwithoutfat;
                            highday[1, 1] = 2.8 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }else if (fatrate < 0.25) 
                        {
                            highday[0, 1] = 2.4 * weightwithoutfat;
                            highday[1, 1] = 2.7 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }else 
                        {
                            highday[0, 1] = 2.3 * weightwithoutfat;
                            highday[1, 1] = 2.6 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }else 
                    {
                        if (fatrate < 0.18) 
                        {
                            highday[0, 1] = 2.6 * weightwithoutfat;
                            highday[1, 1] = 2.9 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }else 
                        {
                            highday[0, 1] = 2.5 * weightwithoutfat;
                            highday[1, 1] = 2.8 * weightwithoutfat;
                            lowday[0, 1] = highday[0, 1];
                            lowday[1, 1] = highday[1, 1];
                        }
                    }
                    highday[0, 2] = weight * 1.1;//增肌期高碳日脂肪量
                    highday[0, 0] = (TDEE[1] - (highday[0, 1]*4 + highday[0, 2]*9))/4;//增肌期高碳日碳水量
                    lowday[0, 0] = 50;//增肌期低碳日碳水量
                    lowday[0, 2] = (TDEE[1] - (lowday[0, 0]*4 + lowday[0, 1]*4))/9;//增肌期低碳日脂肪量

                    highday[1, 2] = weight * 1.4;//減脂期高碳日脂肪量
                    highday[1, 0] = (TDEE[2] - (highday[1, 1] * 4 + highday[1, 2] * 9)) / 4;//減脂期高碳日碳水量
                    lowday[1, 0] = 50;//減脂期低碳日碳水量
                    lowday[1, 2] = (TDEE[2] - (lowday[1, 0]*4 + lowday[1, 1]*4))/9;//減脂期低碳日脂肪量
                }
                for(int i = 0; i < 2; i++) 
                {
                    for(int j = 0; j < 3; j++) 
                    {
                        highday[i, j] = Math.Round(highday[i, j], 0);
                        lowday[i, j] = Math.Round(lowday[i, j], 0);
                    }
                }
                textBox5.Text = Convert.ToString(highday[0, 0]);
                textBox6.Text = Convert.ToString(highday[0, 1]);
                textBox7.Text = Convert.ToString(highday[0, 2]);

                textBox8.Text = Convert.ToString(lowday[0, 0]);
                textBox9.Text = Convert.ToString(lowday[0, 1]);
                textBox10.Text = Convert.ToString(lowday[0, 2]);

                textBox11.Text = Convert.ToString(highday[1, 0]);
                textBox13.Text = Convert.ToString(highday[1, 1]);
                textBox15.Text = Convert.ToString(highday[1, 2]);

                textBox12.Text = Convert.ToString(lowday[1, 0]);
                textBox14.Text = Convert.ToString(lowday[1, 1]);
                textBox16.Text = Convert.ToString(lowday[1, 2]);

                textBox20.Text = Convert.ToString(Math.Round(highday[0, 0] * 4 / TDEE[1] * 100, 0) + "% : " + Math.Round(highday[0, 1] * 4 / TDEE[1] * 100, 0) + "% : " + Math.Round(highday[0, 2] * 9 / TDEE[1] * 100, 0) + "%");
                textBox21.Text = Convert.ToString(Math.Round(lowday[0, 0] * 4 / TDEE[1] * 100, 0) + "% : " + Math.Round(lowday[0, 1] * 4 / TDEE[1] * 100, 0) + "% : " + Math.Round(lowday[0, 2] * 9 / TDEE[1] * 100, 0) + "%");
                textBox22.Text = Convert.ToString(Math.Round(highday[1, 0] * 4 / TDEE[2] * 100, 0) + "% : " + Math.Round(highday[1, 1] * 4 / TDEE[2] * 100, 0) + "% : " + Math.Round(highday[1, 2] * 9 / TDEE[2] * 100, 0) + "%");
                textBox23.Text = Convert.ToString(Math.Round(lowday[1, 0] * 4 / TDEE[2] * 100, 0) + "% : " + Math.Round(lowday[1, 1] * 4 / TDEE[2] * 100, 0) + "% : " + Math.Round(lowday[1, 2] * 9 / TDEE[2] * 100, 0) + "%");
            }
            catch (Exception) 
            {
                MessageBox.Show("欄位不可為空白，請輸入數值", "注意", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }
    }
}

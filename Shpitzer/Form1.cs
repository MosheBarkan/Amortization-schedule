using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Shpitzer
{

    public partial class Form1 : Form
    {
        float PV;
        float r;
        int n;
        float PMT;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            clearAll();
            PV = float.Parse(textBox1.Text); //סכום הלוואה
            n = int.Parse(textBox2.Text);     //מספר תשלומים
            r = float.Parse(textBox3.Text);
            r = r / 100.0F;
            float x = 1 - (1 / (float)(Math.Pow((1 + r), n)));
            x = x / r;
            PMT = PV / x;

            //
            Console.WriteLine("PV = " + PV);
            Console.WriteLine("n = " + n);
            Console.WriteLine("r = " + r);
            Console.WriteLine("PMT = " + PMT);
            fillRaw();

        }

        void fillRaw()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                listBox2.Items.Add(PV.ToString());
                for (int i = 0; i < n; i++)
                {
                    //
                    listBox1.Items.Add((i + 1).ToString());
                    //
                    //
                    listBox3.Items.Add(PMT.ToString());
                    //
                    float TashlumRibit = r * float.Parse(listBox2.Items[i].ToString());
                    listBox4.Items.Add(TashlumRibit);
                    //
                    float EhzerKeren = PMT - float.Parse(listBox4.Items[i].ToString());
                    listBox5.Items.Add(EhzerKeren);
                    //
                    listBox6.Items.Add(float.Parse(listBox2.Items[i].ToString()) - float.Parse(listBox5.Items[i].ToString()));
                    //
                    listBox2.Items.Add(listBox6.Items[i]);
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                listBox2.Items.Add(PV.ToString());
                for (int i = 0; i < n; i++)
                {
                    listBox1.Items.Add((i + 1).ToString());
                    //
                    //
                    listBox3.Items.Add(PV / n);
                    listBox4.Items.Add(r * float.Parse(listBox2.Items[i].ToString()));
                    listBox5.Items.Add(float.Parse(listBox3.Items[i].ToString()) + float.Parse(listBox4.Items[i].ToString()));
                    listBox6.Items.Add(float.Parse(listBox2.Items[i].ToString()) - float.Parse(listBox3.Items[i].ToString()));
                    listBox2.Items.Add(listBox6.Items[i]);
                }
            }
        }
        void clearAll()
        {

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(e.KeyChar);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            string[] fileContent = tableToCSV().ToArray();

            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.CheckPathExists)
                File.WriteAllLines(saveFileDialog1.FileName+".csv", fileContent);
        }

        public List<string> tableToCSV()
        {
            List<string> toFile = new List<string>();
            toFile.Add(label4.Text + "," + label5.Text + "," + label6.Text + "," + label7.Text + "," + label8.Text + "," + label9.Text + ",");

            for(int i=0;i<listBox1.Items.Count;i++)
            {
                string toAdd = "";
                toAdd = listBox1.Items[i] + "," + listBox2.Items[i] + "," + listBox3.Items[i] + "," + listBox4.Items[i] + "," + listBox5.Items[i] + "," + listBox6.Items[i];
                toFile.Add(toAdd);
            }
            return toFile;
        }
    }
}

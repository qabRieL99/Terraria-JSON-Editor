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
using System.Net;

namespace Darari
{
    public partial class toolTerraria : Form
    {
        public toolTerraria()
        {
            InitializeComponent();
        }



        bool degisken;

        void Yukle()
        {
            listBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();
            trackBar1.Value = 0;

            textBox3.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            foreach (string item in textBox3.Text.Split('\n'))
            {
                if (item != "")
                {
                    listBox1.Items.Add(item.ToString().Trim());
                }
            }
            listBox1.SetSelected(0, true);
            toolStripStatusLabel1.Text = listBox1.SelectedIndex.ToString() + " / " + (listBox1.Items.Count - 1).ToString() + "   %" + Math.Round((decimal)(listBox1.SelectedIndex * 100) / (listBox1.Items.Count - 1), 1);
            int max = listBox1.Items.Count - 1;
            trackBar1.Maximum = max;
        }

        void Ayristir()
        {
            if (listBox1.SelectedItem.ToString() == "{" || listBox1.SelectedItem.ToString() == "}," || listBox1.SelectedItem.ToString() == "}" || listBox1.SelectedItem.ToString().Substring(listBox1.SelectedItem.ToString().Length - 1) == "{")
            {
                toolStripStatusLabel1.Text = listBox1.SelectedIndex.ToString() + " / " + (listBox1.Items.Count - 1).ToString() + "   %" + Math.Round((decimal)(listBox1.SelectedIndex * 100) / (listBox1.Items.Count - 1), 1);
                textBox1.Text = listBox1.SelectedItem.ToString();
                textBox2.Text = "***************";
                // textBox2.Enabled = false;
                button3.Enabled = false;
                richTextBox1.Enabled = false;
                richTextBox1.Clear();
            }
            else
            {
                if (listBox1.SelectedItem.ToString().Substring(listBox1.SelectedItem.ToString().Length - 1) == ",")
                {
                    degisken = true;
                    textBox2.Clear();



                    if (richTextBox1.Text.Length == 0)
                    {
                        button3.Enabled = false;
                    }
                    else
                    {
                        button3.Enabled = true;
                    }




                    button4.Enabled = true;
                    richTextBox1.Enabled = true;
                    String[] parcalar = listBox1.SelectedItem.ToString().Split(new string[] { ":" }, 2, StringSplitOptions.None);
                    textBox1.Text = parcalar[0];
                    textBox2.Text = parcalar[1].Remove(0, 2).Remove(parcalar[1].Length - 4, 2);
                    toolStripStatusLabel1.Text = listBox1.SelectedIndex.ToString() + " / " + (listBox1.Items.Count - 1).ToString() + "   %" + Math.Round((decimal)(listBox1.SelectedIndex * 100) / (listBox1.Items.Count - 1), 1);
                    trackBar1.Value = listBox1.SelectedIndex;
                    richTextBox1.Clear();
                }
                else
                {
                    degisken = false;
                    textBox2.Clear();

                    if (richTextBox1.Text.Length ==0)
                    {
                        button3.Enabled = false;    
                    }
                    else
                    {
                        button3.Enabled = true;
                    }
                    
                    
                    button4.Enabled = true;
                    richTextBox1.Enabled = true;
                    String[] parcalar = listBox1.SelectedItem.ToString().Split(new string[] { ":" }, 2, StringSplitOptions.None);
                    textBox1.Text = parcalar[0];
                    textBox2.Text = parcalar[1].Remove(0, 2).Remove(parcalar[1].Length - 3, 1);
                    toolStripStatusLabel1.Text = listBox1.SelectedIndex.ToString() + " / " + (listBox1.Items.Count - 1).ToString() + "   %" + Math.Round((decimal)(listBox1.SelectedIndex * 100) / (listBox1.Items.Count - 1), 1);
                    trackBar1.Value = listBox1.SelectedIndex;
                    richTextBox1.Clear();
                }
            }

            try
            {
                TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
                client = new TranslatorService.LanguageServiceClient();
                string strTranslatedText = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", textBox2.Text, "", "tr");
                label1.Text = strTranslatedText;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void Kaydet()
        {
            if (degisken)
            {
                int satır = listBox1.SelectedIndex;
                string num = textBox1.Text + ": " + "\"" + richTextBox1.Text + "\",";
                listBox1.Items[satır] = num;
                richTextBox1.Clear();
            }
            else
            {
                int satır = listBox1.SelectedIndex;
                string num = textBox1.Text + ": " + "\"" + richTextBox1.Text + "\"";
                listBox1.Items[satır] = num;
                richTextBox1.Clear();
            }
            listBox1.SelectedIndex++;
        }

        void DosyaKaydet()
        {
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(openFileDialog1.FileName);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }

            MessageBox.Show("Saved!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SaveFile.Close();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = trackBar1.Value;
            Ayristir();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Yukle();
                toolStripStatusLabel2.Text = openFileDialog1.SafeFileName;
                Ayristir();
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DosyaKaydet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex <= 0)
            {

            }
            else
            {
                listBox1.SelectedIndex--;
            }
            Ayristir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= listBox1.Items.Count - 1)
            {

            }
            else
            {
                listBox1.SelectedIndex++;
            }
            Ayristir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kaydet();
            Ayristir();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Kaydet();
                Ayristir();
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += textBox2.Text;
            richTextBox1.Focus();
            richTextBox1.Select(richTextBox1.Text.Length, 0);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
          
        }
    }
}



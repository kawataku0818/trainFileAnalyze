using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trainFileAnalyze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        HObject _image;
        HObject rectangle;
        private void button1_Click(object sender, EventArgs e)
        {
            //HOperatorSet.ReadImage(out _image, "letters");
            //hSmartWindowControl1.HalconWindow.DispObj(_image);
        }

        bool flg = false;
        double X;
        double Y;
        private void hSmartWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            //flg = true;
            //X = e.X;
            //Y = e.Y;
        }

        private void hSmartWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            //if (!flg)
            //{
            //    return;
            //}
            //HOperatorSet.GenRectangle1(out rectangle, Y, X, e.Y, e.X);
            //hSmartWindowControl1.HalconWindow.DispObj(_image);
            //hSmartWindowControl1.HalconWindow.SetDraw("margin");
            //hSmartWindowControl1.HalconWindow.SetColor("red");
            //hSmartWindowControl1.HalconWindow.DispObj(rectangle);

        }

        private void hSmartWindowControl1_HMouseUp(object sender, HMouseEventArgs e)
        {
            //flg = false;
            //hSmartWindowControl1.HMoveContent = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //hSmartWindowControl1.HMoveContent = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //HOperatorSet.GenImageConst(out HObject image, "byte", 9, 10);
            //HOperatorSet.SetSystem("ocr_trainf_version", 1);
            //HOperatorSet.WriteOcrTrainf(image, image, "a", "train_verity.trf");
            //HOperatorSet.AppendOcrTrainf(image, image, "a", "train_verity.trf");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //HOperatorSet.ReadOcrTrainfNames("train_ocr_char_all.trf", out HTuple characterNames, out HTuple characterCount);

            //StreamReader sr = new StreamReader("train_ocr_char_all.trf");
            //string s = sr.ReadToEnd();
            //sr.Close();
            //richTextBox1.Text = s;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string moji = textBox1.Text.Substring(0, 1);
            //string iti = textBox1.Text.Substring(1, 1);
            //bool r = Regex.IsMatch(richTextBox1.Text, moji);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string moji = textBox1.Text.Substring(0, 1);
            int number = Convert.ToInt32(textBox1.Text.Substring(1, 1));
            //string trainFile = "train_ocr_char_all.trf";
            string trainFile = "train_verity.trf";
            List<string> lines = File.ReadAllLines(trainFile).ToList();
            List<string> removedLines = removeCharData(moji, number, lines);

            if (removedLines != null)
            {
                File.WriteAllLines("removed.trf", removedLines.ToArray());
                //richTextBox3.Text = result;
            }

        }

        private static List<string> removeCharData(string moji, int number, List<string> lines)
        {
            int count = -1;
            for (int i = 1; i < lines.Count; i++)
            {
                // 1行目はバージョンなので無視
                // 文字
                if (lines[i] == moji)
                {
                    count++;
                }
                if (count == number)
                {
                    int startIndex = i;
                    // 画像　高さ
                    int height = Convert.ToInt32(lines[++i]);
                    i += height;
                    // 画像　幅　高さ
                    string[] arr = lines[++i].Split(' ');
                    int width = Convert.ToInt32(arr[0]);
                    height = Convert.ToInt32(arr[1]);
                    i += height;
                    int endIndex = i;
                    int removeCount = endIndex - startIndex + 1;
                    //
                    lines.RemoveRange(startIndex, removeCount);

                    return lines;
                    //result = string.Join(Environment.NewLine, lines.ToArray());

                }
            }
            if (count == -1)
            {
                MessageBox.Show("トレーニングファイルに文字が見つかりませんでした。");
            }

            return null;
        }
    }
}

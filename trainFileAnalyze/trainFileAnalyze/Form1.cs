using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace trainFileAnalyze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string trainFile = textBox2.Text;
            richTextBox1.Text =  File.ReadAllText(trainFile);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string moji = textBox1.Text.Substring(0, 1);
            int number = Convert.ToInt32(textBox1.Text.Substring(1, 1));
            //string trainFile = "train_ocr_char_all.trf";
            //string trainFile = "train_verity.trf";
            string trainFile = textBox2.Text;
            List<string> lines = File.ReadAllLines(trainFile).ToList();
            List<string> removedLines = removeCharData(moji, number, lines);

            if (removedLines != null)
            {
                File.WriteAllLines("removed.trf", removedLines.ToArray());
                richTextBox3.Text = string.Join(Environment.NewLine, removedLines.ToArray()); ;
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

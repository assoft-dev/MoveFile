using DevExpress.Utils.CommonDialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MoveFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        private void btnScan_Click_1(object sender, EventArgs e)
        {
            var search = Directory.GetFiles(textBox1.Text);
            progressBar1.Maximum = search.Length;

            var listaArquivosMover = new List<FilesObjects>();

            foreach (string item in search)
            {
                var data1 = item.Split('\\');
                var NameFile = data1.LastOrDefault();

                var foldercomposer = string.Format("{0}\\{1}\\{2}\\", data1[0], data1[1], data1[2].Replace(".mkv", "").Replace(".avi","").Replace(".mp4",""));

                if (!Directory.Exists(foldercomposer))
                    Directory.CreateDirectory(foldercomposer);

                //Mover o arquivo
                listaArquivosMover.Add(new FilesObjects { FileOriginal = item, FileOriginalMove = string.Format("{0}{1}",foldercomposer, NameFile) });
                listBox1.Items.Add(item);                
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                int i = 0;
                foreach (var item1 in listaArquivosMover)
                {
                    progressBar1.Value = i;
                    File.Move(item1.FileOriginal, item1.FileOriginalMove);

                    i ++;
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
            btnScan_Click_1(sender, e);
        }
    }
}

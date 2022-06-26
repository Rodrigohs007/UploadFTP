using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Upload_FTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                FileInfo ArquivoInfo = new FileInfo(txt_arquivo.Text);
                string caminho_completo = txt_caminho.Text; ;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(caminho_completo + "/" + ArquivoInfo.Name));

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(txt_login.Text, txt_senha.Text);
                request.UseBinary = true;
                request.ContentLength = ArquivoInfo.Length;

                using (FileStream fs = ArquivoInfo.OpenRead())
                {
                    byte[] buffer = new byte[2040];
                    int bytesSent = 0;
                    int bytes = 0;

                    using (Stream stream = request.GetRequestStream())
                    {
                        while (bytesSent < ArquivoInfo.Length)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytes);
                            bytesSent += bytes;
                        }
                    }
                }

                MessageBox.Show("Arquivo, " + ArquivoInfo.Name + ", foi inserido com sucesso");
            }

            catch (Exception ex)
            {

                MessageBox.Show("Erro do tentar inserir o arquivo no FTP, segue o erro para análise:  " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                        //Mostra o nome do arquivo
                        //-- fileInfo.Name;
                        //Mostra a extensão do arquivo
                        //--fileInfo.Extension;
                        //Mostra o caminho completo do arquivo junto com o nome
                        //-- fileInfo.FullName;

                        txt_arquivo.Text = fileInfo.FullName;
                    }
                }
            }
        }
    }
}

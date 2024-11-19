using System;
using System.IO; 
using System.Windows.Forms;
using System.Diagnostics;

namespace Dev_Cpp_IDEA
{
    public partial class Form1 : Form
    {
        static string userName = Environment.UserName;
        static string compilerPath = @"C:\Program Files (x86)\Embarcadero\Dev-Cpp\TDM-GCC-64\bin\g++.exe";
        static string sourceFilePath = @"C:\Users\" + userName + @"\Desktop\salam.cpp";
        static string outputFilePath = @"C:\Users\" + userName + @"\Desktop\salam.exe";

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                SaveToFile();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveToFile()
        {
            string filePath = @"C:\Users\" + userName + @"\Desktop\salam.cpp";
            string textToWrite = richTextBox1.Text;

            try
            {

                File.WriteAllText(filePath, textToWrite);
                MessageBox.Show("File saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string arguments = $"\"{sourceFilePath}\" -o \"{outputFilePath}\"";

            try
            {

                ProcessStartInfo processInfo = new ProcessStartInfo(compilerPath, arguments)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("Successfully compiled.");
                    }
                    else
                    {
                        MessageBox.Show("Error: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void execute_Click_1(object sender, EventArgs e)
        {
            try
            {

                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", $"/K \"{outputFilePath}\"")
                {
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("Program successfully run.");
                    }
                    else
                    {
                        MessageBox.Show("Program end with error.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;

            try
            {
                string[] codes = File.ReadAllLines(filePath);
                string codeLines = "";

                foreach (string c in codes)
                {
                    codeLines += c + "\n";
                }
                richTextBox1.Text = codeLines;
            }
            catch (Exception ex)
            {

            }
            


        }
    }
}

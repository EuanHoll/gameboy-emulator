using Gameboy_Emulator.GBCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gameboy_Emulator
{
	public partial class Form1 : Form
	{
		public string fileloc = "";
		public GBCPU gameboy;
		public Thread GBThread;

		public Form1()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				Title = "Choose a Rom File",
				Filter = "gb files (*.gb)|*.gb",
				DefaultExt = "gb",
				CheckFileExists = true,
				CheckPathExists = true,
				Multiselect = false
			};
			openFileDialog.ShowDialog();
			fileloc = openFileDialog.FileName;
			OpenFile();
		}

		private void OpenFile()
		{
			FileInfo file = new FileInfo(fileloc);
			if (file.Length > 32768 || file.Exists == false)
			{
				InvalidFile();
				return;
			}
			StartUpPanel.Visible = false;
			GBPanel.Visible = true;
			StartGB();
		}

		private void StartGB()
		{
			gameboy = new GBCPU();
			GBThread = new Thread(() => { gameboy.CPUStart(File.ReadAllBytes(fileloc)); });
			GBThread.Start();
		}

		private void InvalidFile()
		{
			MessageBox.Show("The Rom you choose is not supported.");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			GBThread.Abort();
			GBThread.Join();
		}
	}
}

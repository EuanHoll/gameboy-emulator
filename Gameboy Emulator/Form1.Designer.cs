namespace Gameboy_Emulator
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ChooseRomlbl = new System.Windows.Forms.Label();
			this.ChooseRombtn = new System.Windows.Forms.Button();
			this.StartUpPanel = new System.Windows.Forms.Panel();
			this.GBPanel = new System.Windows.Forms.Panel();
			this.GBScreen = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.StartUpPanel.SuspendLayout();
			this.GBPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GBScreen)).BeginInit();
			this.SuspendLayout();
			// 
			// ChooseRomlbl
			// 
			this.ChooseRomlbl.AutoSize = true;
			this.ChooseRomlbl.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ChooseRomlbl.Location = new System.Drawing.Point(289, 93);
			this.ChooseRomlbl.Name = "ChooseRomlbl";
			this.ChooseRomlbl.Size = new System.Drawing.Size(239, 39);
			this.ChooseRomlbl.TabIndex = 0;
			this.ChooseRomlbl.Text = "Choose A Rom";
			// 
			// ChooseRombtn
			// 
			this.ChooseRombtn.Location = new System.Drawing.Point(352, 188);
			this.ChooseRombtn.Name = "ChooseRombtn";
			this.ChooseRombtn.Size = new System.Drawing.Size(121, 23);
			this.ChooseRombtn.TabIndex = 1;
			this.ChooseRombtn.Text = "Choose Rom";
			this.ChooseRombtn.UseVisualStyleBackColor = true;
			this.ChooseRombtn.Click += new System.EventHandler(this.Button1_Click);
			// 
			// StartUpPanel
			// 
			this.StartUpPanel.Controls.Add(this.ChooseRombtn);
			this.StartUpPanel.Controls.Add(this.ChooseRomlbl);
			this.StartUpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.StartUpPanel.Location = new System.Drawing.Point(0, 0);
			this.StartUpPanel.Name = "StartUpPanel";
			this.StartUpPanel.Size = new System.Drawing.Size(800, 450);
			this.StartUpPanel.TabIndex = 2;
			// 
			// GBPanel
			// 
			this.GBPanel.Controls.Add(this.label1);
			this.GBPanel.Controls.Add(this.GBScreen);
			this.GBPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GBPanel.Location = new System.Drawing.Point(0, 0);
			this.GBPanel.Name = "GBPanel";
			this.GBPanel.Size = new System.Drawing.Size(800, 450);
			this.GBPanel.TabIndex = 2;
			this.GBPanel.Visible = false;
			// 
			// GBScreen
			// 
			this.GBScreen.Location = new System.Drawing.Point(165, 0);
			this.GBScreen.Name = "GBScreen";
			this.GBScreen.Size = new System.Drawing.Size(477, 337);
			this.GBScreen.TabIndex = 0;
			this.GBScreen.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(352, 386);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.GBPanel);
			this.Controls.Add(this.StartUpPanel);
			this.Name = "Form1";
			this.Text = "Gameboy Emulator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.StartUpPanel.ResumeLayout(false);
			this.StartUpPanel.PerformLayout();
			this.GBPanel.ResumeLayout(false);
			this.GBPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.GBScreen)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label ChooseRomlbl;
		private System.Windows.Forms.Button ChooseRombtn;
		private System.Windows.Forms.Panel StartUpPanel;
		private System.Windows.Forms.Panel GBPanel;
		public System.Windows.Forms.PictureBox GBScreen;
		private System.Windows.Forms.Label label1;
	}
}


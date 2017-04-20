namespace IconsSaveRestore
{
	partial class IconSaveRestore
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconSaveRestore));
			this.Save_Button = new System.Windows.Forms.Button();
			this.Load_Button = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.UsedFileName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SelectFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Save_Button
			// 
			this.Save_Button.Location = new System.Drawing.Point(15, 32);
			this.Save_Button.Name = "Save_Button";
			this.Save_Button.Size = new System.Drawing.Size(75, 23);
			this.Save_Button.TabIndex = 0;
			this.Save_Button.Text = "Save Icons";
			this.Save_Button.UseVisualStyleBackColor = true;
			this.Save_Button.Click += new System.EventHandler(this.SavePositions_Click);
			// 
			// Load_Button
			// 
			this.Load_Button.Location = new System.Drawing.Point(96, 32);
			this.Load_Button.Name = "Load_Button";
			this.Load_Button.Size = new System.Drawing.Size(75, 23);
			this.Load_Button.TabIndex = 1;
			this.Load_Button.Text = "Restore Icons";
			this.Load_Button.UseVisualStyleBackColor = true;
			this.Load_Button.Click += new System.EventHandler(this.LoadPositions_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "xml";
			// 
			// UsedFileName
			// 
			this.UsedFileName.Location = new System.Drawing.Point(69, 6);
			this.UsedFileName.Name = "UsedFileName";
			this.UsedFileName.Size = new System.Drawing.Size(241, 20);
			this.UsedFileName.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "FileName";
			// 
			// SelectFile
			// 
			this.SelectFile.Location = new System.Drawing.Point(316, 6);
			this.SelectFile.Name = "SelectFile";
			this.SelectFile.Size = new System.Drawing.Size(75, 23);
			this.SelectFile.TabIndex = 4;
			this.SelectFile.Text = "Select";
			this.SelectFile.UseVisualStyleBackColor = true;
			this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
			// 
			// IconSaveRestore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(399, 68);
			this.Controls.Add(this.SelectFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.UsedFileName);
			this.Controls.Add(this.Load_Button);
			this.Controls.Add(this.Save_Button);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "IconSaveRestore";
			this.Text = "UserInterface";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Save_Button;
		private System.Windows.Forms.Button Load_Button;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox UsedFileName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button SelectFile;
	}
}
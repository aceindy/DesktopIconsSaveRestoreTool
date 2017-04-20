using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using IconsSaveRestore.Code;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace IconsSaveRestore
{
	public partial class IconSaveRestore : Form
	{
		#region Initialize
		public IconSaveRestore()
		{
			InitializeComponent();

			//Get last used filename from registry
			UsedFileName.Text = Program.GetWindowsRegistry();
			openFileDialog1.InitialDirectory = Path.GetDirectoryName(UsedFileName.Text);
			openFileDialog1.FileName = Path.GetFileName(UsedFileName.Text);
		}
		#endregion

		#region EventHandlers
		private void SavePositions_Click(object sender, EventArgs e)
		{
			Program.SavePositions(UsedFileName.Text.ToString(), false);
			Program.SetWindowsRegistry(UsedFileName.Text.ToString());
		}
		private void LoadPositions_Click(object sender, EventArgs e)
		{
			Program.RestorePositions(UsedFileName.Text.ToString(), false);
			Program.SetWindowsRegistry(UsedFileName.Text.ToString());
		}
		private void SelectFile_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				UsedFileName.Text = openFileDialog1.FileName.ToString();
			}
		}
		#endregion
	}
}

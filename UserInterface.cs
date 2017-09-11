using System;
using System.IO;
using System.Windows.Forms;

namespace IconsSaveRestore
{
	public partial class IconSaveRestore : Form
	{
		#region Initialize
		public IconSaveRestore()
		{
			InitializeComponent();

			//Get last used filename from registry
			var cname = Program.GetWindowsRegistry();
			openFileDialog1.InitialDirectory = Path.GetDirectoryName(cname);
			openFileDialog1.FileName = Path.GetFileName(cname);

			if (openFileDialog1.InitialDirectory != null)
			{
				var existingFIles = Directory.GetFiles(openFileDialog1.InitialDirectory);
				UsedFileName.Items.AddRange(existingFIles);
			}
			UsedFileName.Text = cname;
		}
		#endregion

		#region EventHandlers
		private void SavePositions_Click(object sender, EventArgs e)
		{
			Program.SavePositions(UsedFileName.Text, false);
			Program.SetWindowsRegistry(UsedFileName.Text);
		}
		private void LoadPositions_Click(object sender, EventArgs e)
		{
			Program.RestorePositions(UsedFileName.Text, false);
			Program.SetWindowsRegistry(UsedFileName.Text);
		}
		private void SelectFile_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				UsedFileName.Text = openFileDialog1.FileName;
			}
		}
		#endregion
	}
}

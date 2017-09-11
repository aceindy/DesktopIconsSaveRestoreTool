// 
//  Author:     Stanislav Povolotsky <stas.dev[at]povolotsky.info>
//  Created:       
// 

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IconsSaveRestore
{
	internal class Program
	{
		[STAThread]
		#region Main

		private static void Main()
		{
			//Check if ConfigFolder exists, create if it doesn't
			var configpath = Environment.ExpandEnvironmentVariables(PathWithEnv);
			if (!Directory.Exists(configpath))
			{
				Directory.CreateDirectory(configpath);
			}
			Application.Run(new IconSaveRestore());
		}
		#endregion

		#region Helper
		public static void SetWindowsRegistry(string value)
		{
			RegistryKey currentuser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

			var reg = currentuser.OpenSubKey("Software\\IconsSaveRestore", true) ?? currentuser.CreateSubKey("Software\\IconsSaveRestore");
			reg?.SetValue("LastFile", value);
		}

		public static string GetWindowsRegistry()
		{
			RegistryKey currentuser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

			var reg = currentuser.OpenSubKey("Software\\IconsSaveRestore", true) ?? currentuser.CreateSubKey("Software\\IconsSaveRestore");

			if (reg != null && reg.GetValue("LastFile") == null)
			{
				reg.SetValue("LastFile", Defaultfilename);
			}
			return reg?.GetValue("LastFile").ToString();
		}

		public static void SavePositions(string sFileName, bool bSaveReg)
		{
			DesktopRegistry registry = new DesktopRegistry();
			Desktop desktop = new Desktop();
			Storage storage = new Storage(sFileName);

			var registryValues = bSaveReg ? registry.GetRegistryValues() :
				new Dictionary<string, string>();

			var iconPositions = desktop.GetIconsPositions();

			storage.SaveIconPositions(iconPositions, registryValues);
		}

		public static void RestorePositions(string sFileName, bool bLoadReg)
		{
			DesktopRegistry registry = new DesktopRegistry();
			Desktop desktop = new Desktop();
			Storage storage = new Storage(sFileName);

			var registryValues = bLoadReg ? storage.GetRegistryValues() :
				new Dictionary<string, string>();

			registry.SetRegistryValues(registryValues);

			var iconPositions = storage.GetIconPositions();
			//Console.WriteLine("Loaded {0} icons", System.Linq.Enumerable.Count(iconPositions));

			desktop.SetIconPositions(iconPositions);

			desktop.Refresh();
		}
		#endregion

		#region Constants
		public static string PathWithEnv = @"%USERPROFILE%\Documents\desktopicons";
		public static string Defaultfilename = @"%USERPROFILE%\Documents\desktopicons\desktop.xml"; 
		#endregion
	}
}

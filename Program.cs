// 
//  Author:     Stanislav Povolotsky <stas.dev[at]povolotsky.info>
//  Created:       
// 
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IconsSaveRestore.Code;
using Microsoft.Win32;

namespace IconsSaveRestore
{
	class Program
	{
		[STAThread]
		#region Main
		static void Main()
		{
			//Check if ConfigFolder exists, create if it doesn't
			var configpath = Environment.ExpandEnvironmentVariables(pathWithEnv);
			if (!System.IO.Directory.Exists(configpath))
			{
				System.IO.Directory.CreateDirectory(configpath);
			}
			Application.Run(new IconSaveRestore());
		}
		#endregion

		#region Helper
		public static void ConfigureWindowsRegistry()
		{
			RegistryKey currentuser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

			var reg = currentuser.OpenSubKey("Software\\IconsSaveRestore", true);
			if (reg == null)
			{
				reg = currentuser.CreateSubKey("Software\\IconsSaveRestore");
			}

			if (reg.GetValue("LastFile") == null)
			{
				reg.SetValue("LastFile", defaultfilename);
			}
		}

		public static void SetWindowsRegistry(string value)
		{
			RegistryKey currentuser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

			var reg = currentuser.OpenSubKey("Software\\IconsSaveRestore", true);
			if (reg == null)
			{
				reg = currentuser.CreateSubKey("Software\\IconsSaveRestore");
			}
			reg.SetValue("LastFile", value);
		}

		public static string GetWindowsRegistry()
		{
			RegistryKey currentuser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

			var reg = currentuser.OpenSubKey("Software\\IconsSaveRestore", true);
			if (reg == null)
			{
				reg = currentuser.CreateSubKey("Software\\IconsSaveRestore");
			}

			if (reg.GetValue("LastFile") == null)
			{
				reg.SetValue("LastFile", defaultfilename);
			}
			return reg.GetValue("LastFile").ToString();
		}

		public static void SavePositions(string sFileName, bool bSaveReg)
		{
			DesktopRegistry _registry = new DesktopRegistry();
			Desktop _desktop = new Desktop();
			Storage _storage = new Storage(sFileName);

			var registryValues = bSaveReg ? _registry.GetRegistryValues() :
				new Dictionary<string, string>();

			var iconPositions = _desktop.GetIconsPositions();

			_storage.SaveIconPositions(iconPositions, registryValues);
		}

		public static void RestorePositions(string sFileName, bool bLoadReg)
		{
			DesktopRegistry _registry = new DesktopRegistry();
			Desktop _desktop = new Desktop();
			Storage _storage = new Storage(sFileName);

			var registryValues = bLoadReg ? _storage.GetRegistryValues() :
				new Dictionary<string, string>();

			_registry.SetRegistryValues(registryValues);

			var iconPositions = _storage.GetIconPositions();
			//Console.WriteLine("Loaded {0} icons", System.Linq.Enumerable.Count(iconPositions));

			_desktop.SetIconPositions(iconPositions);

			_desktop.Refresh();
		}
		#endregion

		#region Constants
		public static string pathWithEnv = @"%USERPROFILE%\Documents\desktopicons";
		public static string defaultfilename = @"%USERPROFILE%\Documents\desktopicons\desktop.xml"; 
		#endregion
	}
}

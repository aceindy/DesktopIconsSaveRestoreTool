// This code was taken from
// http://www.codeproject.com/Articles/639486/Save-and-restore-icon-positions-on-desktop
// License: http://www.codeproject.com/info/cpol10.aspx
//
// Modified:
//      19.06.2016 by Stanislav Povolotsky <stas.dev[at]povolotsky.info>
//                 Removed IsolatedStorageFile to use Storage class in console application

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace IconsSaveRestore
{
    internal class UseStorage
    {
        internal static FileStream OpenFile(string path, FileMode mode)
        {
            return File.Open(path, mode);
        }

        internal static FileStream OpenFile(string path, FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access);
        }

        internal static FileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return File.Open(path, mode, access, share);
        }

        internal static bool FileExists(string sFileName)
        {
            return File.Exists(sFileName);
        }

        internal static void DeleteFile(string sFileName)
        {
            File.Delete(sFileName);
        }

        internal static FileStream CreateFile(string sFileName)
        {
            return File.Create(sFileName);
        }
    }

    internal class Storage
    {
        private readonly string _mSFileName;

        public Storage(string sFileName)
        {
            _mSFileName = sFileName;
        }

        public void SaveIconPositions(IEnumerable<NamedDesktopPoint> iconPositions, IDictionary<string, string> registryValues)
        {
            XElement desktop;
            var xDoc = new XDocument(
                desktop = new XElement("Desktop"));

            if (iconPositions.Any())
            {
                desktop.Add(
                    new XElement("Icons",
                        iconPositions.Select(p => new XElement("Icon",
                            new XAttribute("x", p.X),
                            new XAttribute("y", p.Y),
                            new XText(p.Name)))));
            }
            if (registryValues.Any())
            {
                desktop.Add(new XElement("Registry",
                        registryValues.Select(p => new XElement("Value",
                            new XElement("Name", new XCData(p.Key)),
                            new XElement("Data", new XCData(p.Value))))));
            }

            //using (var use_storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (UseStorage.FileExists(_mSFileName))
                { UseStorage.DeleteFile(_mSFileName); }

                using (var stream = UseStorage.CreateFile(_mSFileName))
                {
                    XmlWriterSettings settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
                    using (var writer = XmlWriter.Create(stream, settings))
                    {
                        xDoc.WriteTo(writer);
                    }
                }
            }
        }

        public IEnumerable<NamedDesktopPoint> GetIconPositions()
        {
            //using (var use_storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                //if (use_storage.FileExists(m_sFileName) == false)
                //{ return new NamedDesktopPoint[0]; }

                using (var stream = UseStorage.OpenFile(_mSFileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = XmlReader.Create(stream))
                    {
                        var xDoc = XDocument.Load(reader);

	                    var icons = xDoc.Root?.Element("Icons");
	                    if(icons != null)
	                    {
		                    return icons.Elements("Icon")
			                    .Select(el => new NamedDesktopPoint(el.Value, int.Parse(el.Attribute("x").Value), int.Parse(el.Attribute("y").Value)))
			                    .ToArray();
	                    }
                    }
                }
            }
            return new NamedDesktopPoint[0];
        }

        public IDictionary<string, string> GetRegistryValues()
        {
            //using (var use_storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                //if (use_storage.FileExists(m_sFileName) == false)
                //{ return new Dictionary<string, string>(); }

                using (var stream = UseStorage.OpenFile(_mSFileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = XmlReader.Create(stream))
                    {
                        var xDoc = XDocument.Load(reader);

	                    var elRegistry = xDoc.Root?.Element("Registry");
	                    if (elRegistry != null)
	                    {
		                    return elRegistry.Elements("Value")
			                    .ToDictionary(el => el.Element("Name").Value, el => el.Element("Data").Value);
	                    }
                    }
                }
            }
            return new Dictionary<string, string>();
        }
    }
}

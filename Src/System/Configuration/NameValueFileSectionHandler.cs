using System;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides access to a configuration file. This type supports the .NET Framework configuration infrastructure and is not intended to be used directly from your code.</summary>
	// Token: 0x02000095 RID: 149
	public class NameValueFileSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <param name="parent">The parent object.</param>
		/// <param name="configContext">The configuration context object.</param>
		/// <param name="section">The section XML node.</param>
		/// <returns>A configuration object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The file specified in the <see langword="file" /> attribute of <paramref name="section" /> exists but cannot be loaded.  
		/// -or-
		///  The <see langword="name" /> attribute of <paramref name="section" /> does not match the root element of the file specified in the <see langword="file" /> attribute.</exception>
		// Token: 0x06000585 RID: 1413 RVA: 0x000226E4 File Offset: 0x000208E4
		public object Create(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			XmlNode xmlNode = section.Attributes.RemoveNamedItem("file");
			obj = NameValueSectionHandler.CreateStatic(obj, section);
			if (xmlNode != null && xmlNode.Value.Length != 0)
			{
				string value = xmlNode.Value;
				IConfigErrorInfo configErrorInfo = xmlNode as IConfigErrorInfo;
				if (configErrorInfo == null)
				{
					return null;
				}
				string filename = configErrorInfo.Filename;
				string directoryName = Path.GetDirectoryName(filename);
				string text = Path.Combine(directoryName, value);
				if (File.Exists(text))
				{
					ConfigXmlDocument configXmlDocument = new ConfigXmlDocument();
					try
					{
						configXmlDocument.Load(text);
					}
					catch (XmlException ex)
					{
						throw new ConfigurationErrorsException(ex.Message, ex, text, ex.LineNumber);
					}
					if (section.Name != configXmlDocument.DocumentElement.Name)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_name_value_file_section_file_invalid_root", new object[] { section.Name }), configXmlDocument.DocumentElement);
					}
					obj = NameValueSectionHandler.CreateStatic(obj, configXmlDocument.DocumentElement);
				}
			}
			return obj;
		}
	}
}

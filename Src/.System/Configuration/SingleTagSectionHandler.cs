using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Handles configuration sections that are represented by a single XML tag in the .config file.</summary>
	// Token: 0x020000B3 RID: 179
	public class SingleTagSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Used internally to create a new instance of this object.</summary>
		/// <param name="parent">The parent of this object.</param>
		/// <param name="context">The context of this object.</param>
		/// <param name="section">The <see cref="T:System.Xml.XmlNode" /> object in the configuration.</param>
		/// <returns>The created object handler.</returns>
		// Token: 0x06000610 RID: 1552 RVA: 0x00023CE8 File Offset: 0x00021EE8
		public virtual object Create(object parent, object context, XmlNode section)
		{
			Hashtable hashtable;
			if (parent == null)
			{
				hashtable = new Hashtable();
			}
			else
			{
				hashtable = new Hashtable((IDictionary)parent);
			}
			HandlerBase.CheckForChildNodes(section);
			foreach (object obj in section.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				hashtable[xmlAttribute.Name] = xmlAttribute.Value;
			}
			return hashtable;
		}
	}
}

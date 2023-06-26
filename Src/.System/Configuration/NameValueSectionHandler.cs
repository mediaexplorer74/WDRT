using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides name/value-pair configuration information from a configuration section.</summary>
	// Token: 0x02000096 RID: 150
	public class NameValueSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>A configuration object.</returns>
		// Token: 0x06000587 RID: 1415 RVA: 0x000227F0 File Offset: 0x000209F0
		public object Create(object parent, object context, XmlNode section)
		{
			return NameValueSectionHandler.CreateStatic(parent, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00022805 File Offset: 0x00020A05
		internal static object CreateStatic(object parent, XmlNode section)
		{
			return NameValueSectionHandler.CreateStatic(parent, section, "key", "value");
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00022818 File Offset: 0x00020A18
		internal static object CreateStatic(object parent, XmlNode section, string keyAttriuteName, string valueAttributeName)
		{
			ReadOnlyNameValueCollection readOnlyNameValueCollection;
			if (parent == null)
			{
				readOnlyNameValueCollection = new ReadOnlyNameValueCollection(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				ReadOnlyNameValueCollection readOnlyNameValueCollection2 = (ReadOnlyNameValueCollection)parent;
				readOnlyNameValueCollection = new ReadOnlyNameValueCollection(readOnlyNameValueCollection2);
			}
			HandlerBase.CheckForUnrecognizedAttributes(section);
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					if (xmlNode.Name == "add")
					{
						string text = HandlerBase.RemoveRequiredAttribute(xmlNode, keyAttriuteName);
						string text2 = HandlerBase.RemoveRequiredAttribute(xmlNode, valueAttributeName, true);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						readOnlyNameValueCollection[text] = text2;
					}
					else if (xmlNode.Name == "remove")
					{
						string text3 = HandlerBase.RemoveRequiredAttribute(xmlNode, keyAttriuteName);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						readOnlyNameValueCollection.Remove(text3);
					}
					else if (xmlNode.Name.Equals("clear"))
					{
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						readOnlyNameValueCollection.Clear();
					}
					else
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
				}
			}
			readOnlyNameValueCollection.SetReadOnly();
			return readOnlyNameValueCollection;
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the key attribute.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00022934 File Offset: 0x00020B34
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the value attribute.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0002293B File Offset: 0x00020B3B
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}

		// Token: 0x04000C2F RID: 3119
		private const string defaultKeyAttribute = "key";

		// Token: 0x04000C30 RID: 3120
		private const string defaultValueAttribute = "value";
	}
}

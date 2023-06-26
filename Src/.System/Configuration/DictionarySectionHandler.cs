using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides key/value pair configuration information from a configuration section.</summary>
	// Token: 0x0200008C RID: 140
	public class DictionarySectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>A configuration object.</returns>
		// Token: 0x0600054D RID: 1357 RVA: 0x000218C0 File Offset: 0x0001FAC0
		public virtual object Create(object parent, object context, XmlNode section)
		{
			Hashtable hashtable;
			if (parent == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashtable = (Hashtable)((Hashtable)parent).Clone();
			}
			HandlerBase.CheckForUnrecognizedAttributes(section);
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					if (xmlNode.Name == "add")
					{
						HandlerBase.CheckForChildNodes(xmlNode);
						string text = HandlerBase.RemoveRequiredAttribute(xmlNode, this.KeyAttributeName);
						string text2;
						if (this.ValueRequired)
						{
							text2 = HandlerBase.RemoveRequiredAttribute(xmlNode, this.ValueAttributeName);
						}
						else
						{
							text2 = HandlerBase.RemoveAttribute(xmlNode, this.ValueAttributeName);
						}
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						if (text2 == null)
						{
							text2 = "";
						}
						hashtable[text] = text2;
					}
					else if (xmlNode.Name == "remove")
					{
						HandlerBase.CheckForChildNodes(xmlNode);
						string text3 = HandlerBase.RemoveRequiredAttribute(xmlNode, this.KeyAttributeName);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						hashtable.Remove(text3);
					}
					else if (xmlNode.Name.Equals("clear"))
					{
						HandlerBase.CheckForChildNodes(xmlNode);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						hashtable.Clear();
					}
					else
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
				}
			}
			return hashtable;
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A string value containing the name of the key attribute.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00021A18 File Offset: 0x0001FC18
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A string value containing the name of the value attribute.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00021A1F File Offset: 0x0001FC1F
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00021A26 File Offset: 0x0001FC26
		internal virtual bool ValueRequired
		{
			get
			{
				return false;
			}
		}
	}
}

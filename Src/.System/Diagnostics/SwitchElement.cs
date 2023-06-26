using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x020004A8 RID: 1192
	internal class SwitchElement : ConfigurationElement
	{
		// Token: 0x06002C1E RID: 11294 RVA: 0x000C71E8 File Offset: 0x000C53E8
		static SwitchElement()
		{
			SwitchElement._properties.Add(SwitchElement._propName);
			SwitchElement._properties.Add(SwitchElement._propValue);
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000C7257 File Offset: 0x000C5457
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000C7277 File Offset: 0x000C5477
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SwitchElement._propName];
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000C7289 File Offset: 0x000C5489
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SwitchElement._properties;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000C7290 File Offset: 0x000C5490
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base[SwitchElement._propValue];
			}
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000C72A2 File Offset: 0x000C54A2
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000C72B4 File Offset: 0x000C54B4
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000C7308 File Offset: 0x000C5508
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000C7340 File Offset: 0x000C5540
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SwitchElement switchElement = sourceElement as SwitchElement;
			if (switchElement != null && switchElement._attributes != null)
			{
				this._attributes = switchElement._attributes;
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000C7374 File Offset: 0x000C5574
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SwitchElement._properties.Clear();
				SwitchElement._properties.Add(SwitchElement._propName);
				SwitchElement._properties.Add(SwitchElement._propValue);
			}
		}

		// Token: 0x040026A7 RID: 9895
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040026A8 RID: 9896
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040026A9 RID: 9897
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040026AA RID: 9898
		private Hashtable _attributes;
	}
}

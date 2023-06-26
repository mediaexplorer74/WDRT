using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a simplified configuration element used for updating elements in the configuration. This class cannot be inherited.</summary>
	// Token: 0x020000B9 RID: 185
	public sealed class SettingElement : ConfigurationElement
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x00023E4C File Offset: 0x0002204C
		static SettingElement()
		{
			SettingElement._properties.Add(SettingElement._propName);
			SettingElement._properties.Add(SettingElement._propSerializeAs);
			SettingElement._properties.Add(SettingElement._propValue);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class.</summary>
		// Token: 0x06000623 RID: 1571 RVA: 0x00023EF4 File Offset: 0x000220F4
		public SettingElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class based on supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used as the serialization scheme to store configuration settings.</param>
		// Token: 0x06000624 RID: 1572 RVA: 0x00023EFC File Offset: 0x000220FC
		public SettingElement(string name, SettingsSerializeAs serializeAs)
			: this()
		{
			this.Name = name;
			this.SerializeAs = serializeAs;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00023F12 File Offset: 0x00022112
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingElement" /> instance to the specified object.</summary>
		/// <param name="settings">The object to compare with.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.SettingElement" /> instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000626 RID: 1574 RVA: 0x00023F1C File Offset: 0x0002211C
		public override bool Equals(object settings)
		{
			SettingElement settingElement = settings as SettingElement;
			return settingElement != null && base.Equals(settings) && object.Equals(settingElement.Value, this.Value);
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</returns>
		// Token: 0x06000627 RID: 1575 RVA: 0x00023F4F File Offset: 0x0002214F
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00023F63 File Offset: 0x00022163
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingElement._properties;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00023F6A File Offset: 0x0002216A
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00023F7C File Offset: 0x0002217C
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SettingElement._propName];
			}
			set
			{
				base[SettingElement._propName] = value;
			}
		}

		/// <summary>Gets or sets the serialization mechanism used to persist the values of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00023F8A File Offset: 0x0002218A
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00023F9C File Offset: 0x0002219C
		[ConfigurationProperty("serializeAs", IsRequired = true, DefaultValue = SettingsSerializeAs.String)]
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return (SettingsSerializeAs)base[SettingElement._propSerializeAs];
			}
			set
			{
				base[SettingElement._propSerializeAs] = value;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingElement" /> object by using a <see cref="T:System.Configuration.SettingValueElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingValueElement" /> object containing the value of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00023FAF File Offset: 0x000221AF
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00023FC1 File Offset: 0x000221C1
		[ConfigurationProperty("value", IsRequired = true, DefaultValue = null)]
		public SettingValueElement Value
		{
			get
			{
				return (SettingValueElement)base[SettingElement._propValue];
			}
			set
			{
				base[SettingElement._propValue] = value;
			}
		}

		// Token: 0x04000C5F RID: 3167
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C60 RID: 3168
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000C61 RID: 3169
		private static readonly ConfigurationProperty _propSerializeAs = new ConfigurationProperty("serializeAs", typeof(SettingsSerializeAs), SettingsSerializeAs.String, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000C62 RID: 3170
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(SettingValueElement), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000C63 RID: 3171
		private static XmlDocument doc = new XmlDocument();
	}
}

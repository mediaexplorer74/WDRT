using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Contains the XML representing the serialized value of the setting. This class cannot be inherited.</summary>
	// Token: 0x020000BA RID: 186
	public sealed class SettingValueElement : ConfigurationElement
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00023FCF File Offset: 0x000221CF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (SettingValueElement._properties == null)
				{
					SettingValueElement._properties = new ConfigurationPropertyCollection();
				}
				return SettingValueElement._properties;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingValueElement" /> object by using an <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> object containing the value of a <see cref="T:System.Configuration.SettingElement" />.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00023FED File Offset: 0x000221ED
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00023FF5 File Offset: 0x000221F5
		public XmlNode ValueXml
		{
			get
			{
				return this._valueXml;
			}
			set
			{
				this._valueXml = value;
				this.isModified = true;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00024005 File Offset: 0x00022205
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.ValueXml = SettingValueElement.doc.ReadNode(reader);
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingValueElement" /> instance to the specified object.</summary>
		/// <param name="settingValue">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.SettingValueElement" /> instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000633 RID: 1587 RVA: 0x00024018 File Offset: 0x00022218
		public override bool Equals(object settingValue)
		{
			SettingValueElement settingValueElement = settingValue as SettingValueElement;
			return settingValueElement != null && object.Equals(settingValueElement.ValueXml, this.ValueXml);
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</returns>
		// Token: 0x06000634 RID: 1588 RVA: 0x00024042 File Offset: 0x00022242
		public override int GetHashCode()
		{
			return this.ValueXml.GetHashCode();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0002404F File Offset: 0x0002224F
		protected override bool IsModified()
		{
			return this.isModified;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00024057 File Offset: 0x00022257
		protected override void ResetModified()
		{
			this.isModified = false;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00024060 File Offset: 0x00022260
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.ValueXml != null)
			{
				if (writer != null)
				{
					this.ValueXml.WriteTo(writer);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002407C File Offset: 0x0002227C
		protected override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			this.ValueXml = ((SettingValueElement)parentElement).ValueXml;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00024096 File Offset: 0x00022296
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			this.ValueXml = ((SettingValueElement)sourceElement).ValueXml;
		}

		// Token: 0x04000C64 RID: 3172
		private static volatile ConfigurationPropertyCollection _properties;

		// Token: 0x04000C65 RID: 3173
		private static XmlDocument doc = new XmlDocument();

		// Token: 0x04000C66 RID: 3174
		private XmlNode _valueXml;

		// Token: 0x04000C67 RID: 3175
		private bool isModified;
	}
}

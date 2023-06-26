using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020000B8 RID: 184
	public sealed class SettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00023DF5 File Offset: 0x00021FF5
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00023DF8 File Offset: 0x00021FF8
		protected override string ElementName
		{
			get
			{
				return "setting";
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00023DFF File Offset: 0x00021FFF
		protected override ConfigurationElement CreateNewElement()
		{
			return new SettingElement();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00023E06 File Offset: 0x00022006
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SettingElement)element).Key;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="elementKey">A string value representing the <see cref="T:System.Configuration.SettingElement" /> object in the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		// Token: 0x0600061D RID: 1565 RVA: 0x00023E13 File Offset: 0x00022013
		public SettingElement Get(string elementKey)
		{
			return (SettingElement)base.BaseGet(elementKey);
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingElement" /> object to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SettingElement" /> object to add to the collection.</param>
		// Token: 0x0600061E RID: 1566 RVA: 0x00023E21 File Offset: 0x00022021
		public void Add(SettingElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="element">A <see cref="T:System.Configuration.SettingElement" /> object.</param>
		// Token: 0x0600061F RID: 1567 RVA: 0x00023E2A File Offset: 0x0002202A
		public void Remove(SettingElement element)
		{
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingElement" /> objects from the collection.</summary>
		// Token: 0x06000620 RID: 1568 RVA: 0x00023E39 File Offset: 0x00022039
		public void Clear()
		{
			base.BaseClear();
		}
	}
}

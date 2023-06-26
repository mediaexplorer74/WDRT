using System;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.SchemeSettingElement" /> objects.</summary>
	// Token: 0x02000072 RID: 114
	[ConfigurationCollection(typeof(SchemeSettingElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
	public sealed class SchemeSettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> class.</summary>
		// Token: 0x06000494 RID: 1172 RVA: 0x0001F48E File Offset: 0x0001D68E
		public SchemeSettingElementCollection()
		{
			base.AddElementName = "add";
			base.ClearElementName = "clear";
			base.RemoveElementName = "remove";
		}

		/// <summary>Gets the default collection type of <see cref="T:System.Configuration.SchemeSettingElementCollection" />.</summary>
		/// <returns>The default collection type of <see cref="T:System.Configuration.SchemeSettingElementCollection" />.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001F4B7 File Offset: 0x0001D6B7
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		/// <summary>Gets an item at the specified index in the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.SchemeSettingElement" /> to return.</param>
		/// <returns>The specified <see cref="T:System.Configuration.SchemeSettingElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="index" /> parameter is less than zero.  
		///  -or-  
		///  The item specified by the parameter is <see langword="null" /> or has been removed.</exception>
		// Token: 0x17000097 RID: 151
		public SchemeSettingElement this[int index]
		{
			get
			{
				return (SchemeSettingElement)base.BaseGet(index);
			}
		}

		/// <summary>Gets an item from the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> collection.</summary>
		/// <param name="name">A string reference to the <see cref="T:System.Configuration.SchemeSettingElement" /> object within the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.SchemeSettingElement" /> object contained in the collection.</returns>
		// Token: 0x17000098 RID: 152
		public SchemeSettingElement this[string name]
		{
			get
			{
				return (SchemeSettingElement)base.BaseGet(name);
			}
		}

		/// <summary>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SchemeSettingElement" /> for the specified index location.</param>
		/// <returns>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />; otherwise, -1.</returns>
		// Token: 0x06000498 RID: 1176 RVA: 0x0001F4D6 File Offset: 0x0001D6D6
		public int IndexOf(SchemeSettingElement element)
		{
			return base.BaseIndexOf(element);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001F4DF File Offset: 0x0001D6DF
		protected override ConfigurationElement CreateNewElement()
		{
			return new SchemeSettingElement();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001F4E6 File Offset: 0x0001D6E6
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SchemeSettingElement)element).Name;
		}

		// Token: 0x04000BE3 RID: 3043
		internal const string AddItemName = "add";

		// Token: 0x04000BE4 RID: 3044
		internal const string ClearItemsName = "clear";

		// Token: 0x04000BE5 RID: 3045
		internal const string RemoveItemName = "remove";
	}
}

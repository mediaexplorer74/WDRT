using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004A7 RID: 1191
	[ConfigurationCollection(typeof(SwitchElement))]
	internal class SwitchElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000AAC RID: 2732
		public SwitchElement this[string name]
		{
			get
			{
				return (SwitchElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000C71C8 File Offset: 0x000C53C8
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000C71CB File Offset: 0x000C53CB
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwitchElement();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000C71D2 File Offset: 0x000C53D2
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwitchElement)element).Name;
		}
	}
}

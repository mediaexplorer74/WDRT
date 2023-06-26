using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004A0 RID: 1184
	[ConfigurationCollection(typeof(SourceElement), AddItemName = "source", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SourceElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A98 RID: 2712
		public SourceElement this[string name]
		{
			get
			{
				return (SourceElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000C66C3 File Offset: 0x000C48C3
		protected override string ElementName
		{
			get
			{
				return "source";
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000C66CA File Offset: 0x000C48CA
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000C66D0 File Offset: 0x000C48D0
		protected override ConfigurationElement CreateNewElement()
		{
			SourceElement sourceElement = new SourceElement();
			sourceElement.Listeners.InitializeDefaultInternal();
			return sourceElement;
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000C66EF File Offset: 0x000C48EF
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SourceElement)element).Name;
		}
	}
}

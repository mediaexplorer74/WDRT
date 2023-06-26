using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200049C RID: 1180
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000C5ED4 File Offset: 0x000C40D4
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000C5ED7 File Offset: 0x000C40D7
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000C5EDF File Offset: 0x000C40DF
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}
	}
}

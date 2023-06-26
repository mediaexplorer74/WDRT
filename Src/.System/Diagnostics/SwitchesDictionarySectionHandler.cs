using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004C6 RID: 1222
	internal class SwitchesDictionarySectionHandler : DictionarySectionHandler
	{
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000CD400 File Offset: 0x000CB600
		protected override string KeyAttributeName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000CD407 File Offset: 0x000CB607
		internal override bool ValueRequired
		{
			get
			{
				return true;
			}
		}
	}
}

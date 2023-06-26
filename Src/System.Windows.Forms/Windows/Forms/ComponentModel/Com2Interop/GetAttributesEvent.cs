using System;
using System.Collections;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A3 RID: 1187
	internal class GetAttributesEvent : EventArgs
	{
		// Token: 0x06004F21 RID: 20257 RVA: 0x00146015 File Offset: 0x00144215
		public GetAttributesEvent(ArrayList attrList)
		{
			this.attrList = attrList;
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x00146024 File Offset: 0x00144224
		public void Add(Attribute attribute)
		{
			this.attrList.Add(attribute);
		}

		// Token: 0x0400343D RID: 13373
		private ArrayList attrList;
	}
}

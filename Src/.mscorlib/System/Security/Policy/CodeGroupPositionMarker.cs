using System;

namespace System.Security.Policy
{
	// Token: 0x0200034A RID: 842
	internal class CodeGroupPositionMarker
	{
		// Token: 0x06002A12 RID: 10770 RVA: 0x0009CCFC File Offset: 0x0009AEFC
		internal CodeGroupPositionMarker(int elementIndex, int groupIndex, SecurityElement element)
		{
			this.elementIndex = elementIndex;
			this.groupIndex = groupIndex;
			this.element = element;
		}

		// Token: 0x04001136 RID: 4406
		internal int elementIndex;

		// Token: 0x04001137 RID: 4407
		internal int groupIndex;

		// Token: 0x04001138 RID: 4408
		internal SecurityElement element;
	}
}

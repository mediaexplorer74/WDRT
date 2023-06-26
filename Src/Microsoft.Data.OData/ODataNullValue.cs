using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000151 RID: 337
	public sealed class ODataNullValue : ODataValue
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001CD72 File Offset: 0x0001AF72
		internal override bool IsNullValue
		{
			get
			{
				return true;
			}
		}
	}
}

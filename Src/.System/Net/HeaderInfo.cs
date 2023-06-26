using System;

namespace System.Net
{
	// Token: 0x020001B4 RID: 436
	internal class HeaderInfo
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x0005D6B2 File Offset: 0x0005B8B2
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x040013FE RID: 5118
		internal readonly bool IsRequestRestricted;

		// Token: 0x040013FF RID: 5119
		internal readonly bool IsResponseRestricted;

		// Token: 0x04001400 RID: 5120
		internal readonly HeaderParser Parser;

		// Token: 0x04001401 RID: 5121
		internal readonly string HeaderName;

		// Token: 0x04001402 RID: 5122
		internal readonly bool AllowMultiValues;
	}
}

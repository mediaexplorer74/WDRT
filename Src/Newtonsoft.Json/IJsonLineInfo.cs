using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000015 RID: 21
	public interface IJsonLineInfo
	{
		// Token: 0x06000016 RID: 22
		bool HasLineInfo();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23
		int LineNumber { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24
		int LinePosition { get; }
	}
}

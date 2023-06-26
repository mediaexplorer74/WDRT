using System;

namespace System.Globalization
{
	// Token: 0x020003DB RID: 987
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x060032F6 RID: 13046 RVA: 0x000C5B53 File Offset: 0x000C3D53
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x04001693 RID: 5779
		internal HebrewNumber.HS state;

		// Token: 0x04001694 RID: 5780
		internal int result;
	}
}

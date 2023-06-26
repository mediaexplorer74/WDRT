using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074B RID: 1867
	[Serializable]
	internal class FixupHolder
	{
		// Token: 0x060052C9 RID: 21193 RVA: 0x001242DE File Offset: 0x001224DE
		internal FixupHolder(long id, object fixupInfo, int fixupType)
		{
			this.m_id = id;
			this.m_fixupInfo = fixupInfo;
			this.m_fixupType = fixupType;
		}

		// Token: 0x040024A7 RID: 9383
		internal const int ArrayFixup = 1;

		// Token: 0x040024A8 RID: 9384
		internal const int MemberFixup = 2;

		// Token: 0x040024A9 RID: 9385
		internal const int DelayedFixup = 4;

		// Token: 0x040024AA RID: 9386
		internal long m_id;

		// Token: 0x040024AB RID: 9387
		internal object m_fixupInfo;

		// Token: 0x040024AC RID: 9388
		internal int m_fixupType;
	}
}

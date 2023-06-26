using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000387 RID: 903
	internal static class HashHelpers
	{
		// Token: 0x06002CF2 RID: 11506 RVA: 0x000AA96C File Offset: 0x000A8B6C
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}
	}
}

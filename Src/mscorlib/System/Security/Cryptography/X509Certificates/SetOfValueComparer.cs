using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D0 RID: 720
	internal sealed class SetOfValueComparer : IComparer<ReadOnlyMemory<byte>>
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x0008A487 File Offset: 0x00088687
		internal static SetOfValueComparer Instance
		{
			get
			{
				return SetOfValueComparer._instance;
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x0008A48E File Offset: 0x0008868E
		public int Compare(ReadOnlyMemory<byte> x, ReadOnlyMemory<byte> y)
		{
			return SetOfValueComparer.Compare(x.Span, y.Span);
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0008A4A4 File Offset: 0x000886A4
		internal static int Compare(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
		{
			int num = Math.Min(x.Length, y.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)x[i];
				byte b = y[i];
				int num3 = num2 - (int)b;
				if (num3 != 0)
				{
					return num3;
				}
			}
			return x.Length - y.Length;
		}

		// Token: 0x04000E2A RID: 3626
		private static SetOfValueComparer _instance = new SetOfValueComparer();
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006A RID: 106
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StringReference
	{
		// Token: 0x170000D3 RID: 211
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00018DEB File Offset: 0x00016FEB
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00018DF3 File Offset: 0x00016FF3
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00018DFB File Offset: 0x00016FFB
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00018E03 File Offset: 0x00017003
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00018E1A File Offset: 0x0001701A
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x04000203 RID: 515
		private readonly char[] _chars;

		// Token: 0x04000204 RID: 516
		private readonly int _startIndex;

		// Token: 0x04000205 RID: 517
		private readonly int _length;
	}
}

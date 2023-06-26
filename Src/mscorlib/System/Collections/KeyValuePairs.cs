using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x020004A1 RID: 1185
	[DebuggerDisplay("{value}", Name = "[{key}]", Type = "")]
	internal class KeyValuePairs
	{
		// Token: 0x060038E4 RID: 14564 RVA: 0x000DB42D File Offset: 0x000D962D
		public KeyValuePairs(object key, object value)
		{
			this.value = value;
			this.key = key;
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000DB443 File Offset: 0x000D9643
		public object Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000DB44B File Offset: 0x000D964B
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400190A RID: 6410
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object key;

		// Token: 0x0400190B RID: 6411
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object value;
	}
}

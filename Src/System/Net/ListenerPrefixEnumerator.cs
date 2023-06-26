using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020000FA RID: 250
	internal class ListenerPrefixEnumerator : IEnumerator<string>, IDisposable, IEnumerator
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00032A92 File Offset: 0x00030C92
		internal ListenerPrefixEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00032AA1 File Offset: 0x00030CA1
		public string Current
		{
			get
			{
				return (string)this.enumerator.Current;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00032AB3 File Offset: 0x00030CB3
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00032AC0 File Offset: 0x00030CC0
		public void Dispose()
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00032AC2 File Offset: 0x00030CC2
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00032ACF File Offset: 0x00030CCF
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x04000E04 RID: 3588
		private IEnumerator enumerator;
	}
}

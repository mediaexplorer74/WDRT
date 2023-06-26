using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068D RID: 1677
	[Serializable]
	internal class CaptureEnumerator : IEnumerator
	{
		// Token: 0x06003DEA RID: 15850 RVA: 0x000FD5B3 File Offset: 0x000FB7B3
		internal CaptureEnumerator(CaptureCollection rcc)
		{
			this._curindex = -1;
			this._rcc = rcc;
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000FD5CC File Offset: 0x000FB7CC
		public bool MoveNext()
		{
			int count = this._rcc.Count;
			if (this._curindex >= count)
			{
				return false;
			}
			this._curindex++;
			return this._curindex < count;
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000FD607 File Offset: 0x000FB807
		public object Current
		{
			get
			{
				return this.Capture;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000FD60F File Offset: 0x000FB80F
		public Capture Capture
		{
			get
			{
				if (this._curindex < 0 || this._curindex >= this._rcc.Count)
				{
					throw new InvalidOperationException(SR.GetString("EnumNotStarted"));
				}
				return this._rcc[this._curindex];
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000FD64E File Offset: 0x000FB84E
		public void Reset()
		{
			this._curindex = -1;
		}

		// Token: 0x04002CEA RID: 11498
		internal CaptureCollection _rcc;

		// Token: 0x04002CEB RID: 11499
		internal int _curindex;
	}
}

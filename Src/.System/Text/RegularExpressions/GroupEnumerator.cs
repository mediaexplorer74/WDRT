using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000699 RID: 1689
	internal class GroupEnumerator : IEnumerator
	{
		// Token: 0x06003EC6 RID: 16070 RVA: 0x00105088 File Offset: 0x00103288
		internal GroupEnumerator(GroupCollection rgc)
		{
			this._curindex = -1;
			this._rgc = rgc;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x001050A0 File Offset: 0x001032A0
		public bool MoveNext()
		{
			int count = this._rgc.Count;
			if (this._curindex >= count)
			{
				return false;
			}
			this._curindex++;
			return this._curindex < count;
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x001050DB File Offset: 0x001032DB
		public object Current
		{
			get
			{
				return this.Capture;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x001050E3 File Offset: 0x001032E3
		public Capture Capture
		{
			get
			{
				if (this._curindex < 0 || this._curindex >= this._rgc.Count)
				{
					throw new InvalidOperationException(SR.GetString("EnumNotStarted"));
				}
				return this._rgc[this._curindex];
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x00105122 File Offset: 0x00103322
		public void Reset()
		{
			this._curindex = -1;
		}

		// Token: 0x04002DC8 RID: 11720
		internal GroupCollection _rgc;

		// Token: 0x04002DC9 RID: 11721
		internal int _curindex;
	}
}

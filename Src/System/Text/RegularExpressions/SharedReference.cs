using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000689 RID: 1673
	internal sealed class SharedReference
	{
		// Token: 0x06003DD1 RID: 15825 RVA: 0x000FCE4C File Offset: 0x000FB04C
		internal object Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				object target = this._ref.Target;
				this._locked = 0;
				return target;
			}
			return null;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000FCE7D File Offset: 0x000FB07D
		internal void Cache(object obj)
		{
			if (Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				this._ref.Target = obj;
				this._locked = 0;
			}
		}

		// Token: 0x04002CD8 RID: 11480
		private WeakReference _ref = new WeakReference(null);

		// Token: 0x04002CD9 RID: 11481
		private int _locked;
	}
}

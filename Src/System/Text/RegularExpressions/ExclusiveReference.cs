using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000688 RID: 1672
	internal sealed class ExclusiveReference
	{
		// Token: 0x06003DCE RID: 15822 RVA: 0x000FCDA4 File Offset: 0x000FAFA4
		internal object Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			object @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000FCDDC File Offset: 0x000FAFDC
		internal void Release(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = (RegexRunner)obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x04002CD5 RID: 11477
		private RegexRunner _ref;

		// Token: 0x04002CD6 RID: 11478
		private object _obj;

		// Token: 0x04002CD7 RID: 11479
		private int _locked;
	}
}

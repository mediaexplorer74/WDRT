using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000334 RID: 820
	[ComVisible(false)]
	internal class IdentityReferenceEnumerator : IEnumerator<IdentityReference>, IDisposable, IEnumerator
	{
		// Token: 0x06002928 RID: 10536 RVA: 0x00098D18 File Offset: 0x00096F18
		internal IdentityReferenceEnumerator(IdentityReferenceCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._Collection = collection;
			this._Current = -1;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x00098D3C File Offset: 0x00096F3C
		object IEnumerator.Current
		{
			get
			{
				return this._Collection.Identities[this._Current];
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x00098D54 File Offset: 0x00096F54
		public IdentityReference Current
		{
			get
			{
				return ((IEnumerator)this).Current as IdentityReference;
			}
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00098D61 File Offset: 0x00096F61
		public bool MoveNext()
		{
			this._Current++;
			return this._Current < this._Collection.Count;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00098D84 File Offset: 0x00096F84
		public void Reset()
		{
			this._Current = -1;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x00098D8D File Offset: 0x00096F8D
		public void Dispose()
		{
		}

		// Token: 0x04001099 RID: 4249
		private int _Current;

		// Token: 0x0400109A RID: 4250
		private readonly IdentityReferenceCollection _Collection;
	}
}

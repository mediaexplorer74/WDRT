using System;
using System.Collections;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000040 RID: 64
	internal sealed class EnumReferenceIdentity : IEnumerator
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00006F54 File Offset: 0x00005154
		internal EnumReferenceIdentity(IEnumReferenceIdentity e)
		{
			this._enum = e;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006F6F File Offset: 0x0000516F
		private ReferenceIdentity GetCurrent()
		{
			if (this._current == null)
			{
				throw new InvalidOperationException();
			}
			return new ReferenceIdentity(this._current);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006F8A File Offset: 0x0000518A
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006F8A File Offset: 0x0000518A
		public ReferenceIdentity Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006F92 File Offset: 0x00005192
		public bool MoveNext()
		{
			if (this._enum.Next(1U, this._fetchList) == 1U)
			{
				this._current = this._fetchList[0];
				return true;
			}
			this._current = null;
			return false;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006FC1 File Offset: 0x000051C1
		public void Reset()
		{
			this._current = null;
			this._enum.Reset();
		}

		// Token: 0x04000133 RID: 307
		private IEnumReferenceIdentity _enum;

		// Token: 0x04000134 RID: 308
		private IReferenceIdentity _current;

		// Token: 0x04000135 RID: 309
		private IReferenceIdentity[] _fetchList = new IReferenceIdentity[1];
	}
}

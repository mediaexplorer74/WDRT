using System;
using System.Collections;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003E RID: 62
	internal sealed class EnumDefinitionIdentity : IEnumerator
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00006ECA File Offset: 0x000050CA
		internal EnumDefinitionIdentity(IEnumDefinitionIdentity e)
		{
			if (e == null)
			{
				throw new ArgumentNullException();
			}
			this._enum = e;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006EEE File Offset: 0x000050EE
		private DefinitionIdentity GetCurrent()
		{
			if (this._current == null)
			{
				throw new InvalidOperationException();
			}
			return new DefinitionIdentity(this._current);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006F09 File Offset: 0x00005109
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006F09 File Offset: 0x00005109
		public DefinitionIdentity Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006F11 File Offset: 0x00005111
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

		// Token: 0x06000138 RID: 312 RVA: 0x00006F40 File Offset: 0x00005140
		public void Reset()
		{
			this._current = null;
			this._enum.Reset();
		}

		// Token: 0x04000130 RID: 304
		private IEnumDefinitionIdentity _enum;

		// Token: 0x04000131 RID: 305
		private IDefinitionIdentity _current;

		// Token: 0x04000132 RID: 306
		private IDefinitionIdentity[] _fetchList = new IDefinitionIdentity[1];
	}
}

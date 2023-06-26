using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003B RID: 59
	internal sealed class DefinitionIdentity
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00006E63 File Offset: 0x00005063
		internal DefinitionIdentity(IDefinitionIdentity i)
		{
			if (i == null)
			{
				throw new ArgumentNullException();
			}
			this._id = i;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006E7B File Offset: 0x0000507B
		private string GetAttribute(string ns, string n)
		{
			return this._id.GetAttribute(ns, n);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006E8A File Offset: 0x0000508A
		private string GetAttribute(string n)
		{
			return this._id.GetAttribute(null, n);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006E99 File Offset: 0x00005099
		private void SetAttribute(string ns, string n, string v)
		{
			this._id.SetAttribute(ns, n, v);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006EA9 File Offset: 0x000050A9
		private void SetAttribute(string n, string v)
		{
			this.SetAttribute(null, n, v);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006EB4 File Offset: 0x000050B4
		private void DeleteAttribute(string ns, string n)
		{
			this.SetAttribute(ns, n, null);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006EBF File Offset: 0x000050BF
		private void DeleteAttribute(string n)
		{
			this.SetAttribute(null, n, null);
		}

		// Token: 0x0400012F RID: 303
		internal IDefinitionIdentity _id;
	}
}

using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000038 RID: 56
	internal sealed class ReferenceIdentity
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00006DFC File Offset: 0x00004FFC
		internal ReferenceIdentity(IReferenceIdentity i)
		{
			if (i == null)
			{
				throw new ArgumentNullException();
			}
			this._id = i;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006E14 File Offset: 0x00005014
		private string GetAttribute(string ns, string n)
		{
			return this._id.GetAttribute(ns, n);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006E23 File Offset: 0x00005023
		private string GetAttribute(string n)
		{
			return this._id.GetAttribute(null, n);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006E32 File Offset: 0x00005032
		private void SetAttribute(string ns, string n, string v)
		{
			this._id.SetAttribute(ns, n, v);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006E42 File Offset: 0x00005042
		private void SetAttribute(string n, string v)
		{
			this.SetAttribute(null, n, v);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006E4D File Offset: 0x0000504D
		private void DeleteAttribute(string ns, string n)
		{
			this.SetAttribute(ns, n, null);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006E58 File Offset: 0x00005058
		private void DeleteAttribute(string n)
		{
			this.SetAttribute(null, n, null);
		}

		// Token: 0x0400012E RID: 302
		internal IReferenceIdentity _id;
	}
}

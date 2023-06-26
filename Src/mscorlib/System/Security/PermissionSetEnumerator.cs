using System;
using System.Collections;

namespace System.Security
{
	// Token: 0x020001DB RID: 475
	internal class PermissionSetEnumerator : IEnumerator
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00062480 File Offset: 0x00060680
		public object Current
		{
			get
			{
				return this.enm.Current;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0006248D File Offset: 0x0006068D
		public bool MoveNext()
		{
			return this.enm.MoveNext();
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0006249A File Offset: 0x0006069A
		public void Reset()
		{
			this.enm.Reset();
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000624A7 File Offset: 0x000606A7
		internal PermissionSetEnumerator(PermissionSet permSet)
		{
			this.enm = new PermissionSetEnumeratorInternal(permSet);
		}

		// Token: 0x04000A08 RID: 2568
		private PermissionSetEnumeratorInternal enm;
	}
}

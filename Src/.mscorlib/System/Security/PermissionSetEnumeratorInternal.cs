using System;
using System.Security.Util;

namespace System.Security
{
	// Token: 0x020001DC RID: 476
	internal struct PermissionSetEnumeratorInternal
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000624BB File Offset: 0x000606BB
		public object Current
		{
			get
			{
				return this.enm.Current;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000624C8 File Offset: 0x000606C8
		internal PermissionSetEnumeratorInternal(PermissionSet permSet)
		{
			this.m_permSet = permSet;
			this.enm = new TokenBasedSetEnumerator(permSet.m_permSet);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000624E2 File Offset: 0x000606E2
		public int GetCurrentIndex()
		{
			return this.enm.Index;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000624EF File Offset: 0x000606EF
		public void Reset()
		{
			this.enm.Reset();
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000624FC File Offset: 0x000606FC
		public bool MoveNext()
		{
			while (this.enm.MoveNext())
			{
				object current = this.enm.Current;
				IPermission permission = current as IPermission;
				if (permission != null)
				{
					this.enm.Current = permission;
					return true;
				}
				SecurityElement securityElement = current as SecurityElement;
				if (securityElement != null)
				{
					permission = this.m_permSet.CreatePermission(securityElement, this.enm.Index);
					if (permission != null)
					{
						this.enm.Current = permission;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000A09 RID: 2569
		private PermissionSet m_permSet;

		// Token: 0x04000A0A RID: 2570
		private TokenBasedSetEnumerator enm;
	}
}

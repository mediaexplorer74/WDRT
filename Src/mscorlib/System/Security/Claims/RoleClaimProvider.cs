using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x0200031F RID: 799
	[ComVisible(false)]
	internal class RoleClaimProvider
	{
		// Token: 0x060028A2 RID: 10402 RVA: 0x000963AD File Offset: 0x000945AD
		public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
		{
			this.m_issuer = issuer;
			this.m_roles = roles;
			this.m_subject = subject;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000963CC File Offset: 0x000945CC
		public IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_roles.Length; i = num + 1)
				{
					if (this.m_roles[i] != null)
					{
						yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x0400100F RID: 4111
		private string m_issuer;

		// Token: 0x04001010 RID: 4112
		private string[] m_roles;

		// Token: 0x04001011 RID: 4113
		private ClaimsIdentity m_subject;
	}
}

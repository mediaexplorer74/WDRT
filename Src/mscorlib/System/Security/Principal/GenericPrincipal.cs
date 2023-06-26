using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic principal.</summary>
	// Token: 0x02000321 RID: 801
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericPrincipal" /> class from a user identity and an array of role names to which the user represented by that identity belongs.</summary>
		/// <param name="identity">A basic implementation of <see cref="T:System.Security.Principal.IIdentity" /> that represents any user.</param>
		/// <param name="roles">An array of role names to which the user represented by the <paramref name="identity" /> parameter belongs.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060028AF RID: 10415 RVA: 0x0009653C File Offset: 0x0009473C
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
			else
			{
				this.m_roles = null;
			}
			this.AddIdentityWithRoles(this.m_identity, this.m_roles);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000965AC File Offset: 0x000947AC
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in base.Identities)
			{
				if (claimsIdentity2 != null)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
			}
			if (this.m_roles != null && this.m_roles.Length != 0 && claimsIdentity != null)
			{
				claimsIdentity.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", this.m_roles, claimsIdentity).Claims);
				return;
			}
			if (claimsIdentity == null)
			{
				this.AddIdentityWithRoles(this.m_identity, this.m_roles);
			}
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00096648 File Offset: 0x00094848
		[SecuritySafeCritical]
		private void AddIdentityWithRoles(IIdentity identity, string[] roles)
		{
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				claimsIdentity = claimsIdentity.Clone();
			}
			else
			{
				claimsIdentity = new ClaimsIdentity(identity);
			}
			if (roles != null && roles.Length != 0)
			{
				claimsIdentity.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", roles, claimsIdentity).Claims);
			}
			base.AddIdentity(claimsIdentity);
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the current <see cref="T:System.Security.Principal.GenericPrincipal" />.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the <see cref="T:System.Security.Principal.GenericPrincipal" />.</returns>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x00096699 File Offset: 0x00094899
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.Principal.GenericPrincipal" /> belongs to the specified role.</summary>
		/// <param name="role">The name of the role for which to check membership.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.Principal.GenericPrincipal" /> is a member of the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028B3 RID: 10419 RVA: 0x000966A4 File Offset: 0x000948A4
		public override bool IsInRole(string role)
		{
			if (role == null || this.m_roles == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_roles.Length; i++)
			{
				if (this.m_roles[i] != null && string.Compare(this.m_roles[i], role, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return base.IsInRole(role);
		}

		// Token: 0x04001014 RID: 4116
		private IIdentity m_identity;

		// Token: 0x04001015 RID: 4117
		private string[] m_roles;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Security.Claims
{
	/// <summary>The single method, <see cref="M:System.Security.Claims.DynamicRoleClaimProvider.AddDynamicRoleClaims(System.Security.Claims.ClaimsIdentity,System.Collections.Generic.IEnumerable{System.Security.Claims.Claim})" />, exposed by this class is obsolete. You can use a <see cref="T:System.Security.Claims.ClaimsAuthenticationManager" /> object to add claims to a <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
	// Token: 0x02000439 RID: 1081
	public static class DynamicRoleClaimProvider
	{
		/// <summary>You can use a <see cref="T:System.Security.Claims.ClaimsAuthenticationManager" /> object to add claims to a <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
		/// <param name="claimsIdentity">The claims identity to which to add the claims.</param>
		/// <param name="claims">The claims to add.</param>
		// Token: 0x0600286C RID: 10348 RVA: 0x000B9C60 File Offset: 0x000B7E60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ClaimsAuthenticationManager to add claims to a ClaimsIdentity", true)]
		public static void AddDynamicRoleClaims(ClaimsIdentity claimsIdentity, IEnumerable<Claim> claims)
		{
			claimsIdentity.ExternalClaims.Add(claims);
		}
	}
}

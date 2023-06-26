using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines the method that creates a new identity permission.</summary>
	// Token: 0x02000353 RID: 851
	[ComVisible(true)]
	public interface IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission for the specified evidence.</summary>
		/// <param name="evidence">The evidence from which to create the new identity permission.</param>
		/// <returns>The new identity permission.</returns>
		// Token: 0x06002A87 RID: 10887
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}

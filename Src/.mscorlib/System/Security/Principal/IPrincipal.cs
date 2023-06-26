using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of a principal object.</summary>
	// Token: 0x02000323 RID: 803
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IPrincipal
	{
		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060028B7 RID: 10423
		[__DynamicallyInvokable]
		IIdentity Identity
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Determines whether the current principal belongs to the specified role.</summary>
		/// <param name="role">The name of the role for which to check membership.</param>
		/// <returns>
		///   <see langword="true" /> if the current principal is a member of the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028B8 RID: 10424
		[__DynamicallyInvokable]
		bool IsInRole(string role);
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of an identity object.</summary>
	// Token: 0x02000322 RID: 802
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IIdentity
	{
		/// <summary>Gets the name of the current user.</summary>
		/// <returns>The name of the user on whose behalf the code is running.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060028B4 RID: 10420
		[__DynamicallyInvokable]
		string Name
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the type of authentication used.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060028B5 RID: 10421
		[__DynamicallyInvokable]
		string AuthenticationType
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that indicates whether the user has been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the user was authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060028B6 RID: 10422
		[__DynamicallyInvokable]
		bool IsAuthenticated
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}

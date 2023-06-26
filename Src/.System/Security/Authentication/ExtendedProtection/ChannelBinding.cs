using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class encapsulates a pointer to the opaque data used to bind an authenticated transaction to a secure channel.</summary>
	// Token: 0x02000440 RID: 1088
	[global::__DynamicallyInvokable]
	public abstract class ChannelBinding : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		// Token: 0x06002875 RID: 10357 RVA: 0x000B9CB8 File Offset: 0x000B7EB8
		[global::__DynamicallyInvokable]
		protected ChannelBinding()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		/// <param name="ownsHandle">A Boolean value that indicates if the application owns the safe handle to a native memory region containing the byte data that would be passed to native calls that provide extended protection for integrated windows authentication.</param>
		// Token: 0x06002876 RID: 10358 RVA: 0x000B9CC1 File Offset: 0x000B7EC1
		[global::__DynamicallyInvokable]
		protected ChannelBinding(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.ChannelBinding.Size" /> property gets the size, in bytes, of the channel binding token associated with the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</summary>
		/// <returns>The size, in bytes, of the channel binding token in the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002877 RID: 10359
		[global::__DynamicallyInvokable]
		public abstract int Size
		{
			[global::__DynamicallyInvokable]
			get;
		}
	}
}

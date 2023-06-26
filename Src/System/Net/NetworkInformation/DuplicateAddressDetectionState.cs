using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the current state of an IP address.</summary>
	// Token: 0x0200029B RID: 667
	[global::__DynamicallyInvokable]
	public enum DuplicateAddressDetectionState
	{
		/// <summary>The address is not valid. A nonvalid address is expired and no longer assigned to an interface; applications should not send data packets to it.</summary>
		// Token: 0x04001894 RID: 6292
		[global::__DynamicallyInvokable]
		Invalid,
		/// <summary>The duplicate address detection procedure's evaluation of the address has not completed successfully. Applications should not use the address because it is not yet valid and packets sent to it are discarded.</summary>
		// Token: 0x04001895 RID: 6293
		[global::__DynamicallyInvokable]
		Tentative,
		/// <summary>The address is not unique. This address should not be assigned to the network interface.</summary>
		// Token: 0x04001896 RID: 6294
		[global::__DynamicallyInvokable]
		Duplicate,
		/// <summary>The address is valid, but it is nearing its lease lifetime and should not be used by applications.</summary>
		// Token: 0x04001897 RID: 6295
		[global::__DynamicallyInvokable]
		Deprecated,
		/// <summary>The address is valid and its use is unrestricted.</summary>
		// Token: 0x04001898 RID: 6296
		[global::__DynamicallyInvokable]
		Preferred
	}
}

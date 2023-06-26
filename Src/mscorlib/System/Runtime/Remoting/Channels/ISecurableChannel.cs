using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>The <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> contains one property, <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />, which gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
	// Token: 0x02000852 RID: 2130
	public interface ISecurableChannel
	{
		/// <summary>Gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
		/// <returns>A Boolean value that indicates whether the current channel is secure.</returns>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005A7D RID: 23165
		// (set) Token: 0x06005A7E RID: 23166
		bool IsSecured
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}

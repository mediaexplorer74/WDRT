using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides the base interface for channel sinks.</summary>
	// Token: 0x02000846 RID: 2118
	[ComVisible(true)]
	public interface IChannelSinkBase
	{
		/// <summary>Gets a dictionary through which properties on the sink can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink can be accessed, or <see langword="null" /> if the channel sink does not support properties.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005A3A RID: 23098
		IDictionary Properties
		{
			[SecurityCritical]
			get;
		}
	}
}

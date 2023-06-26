using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	/// <summary>Provides envoy information.</summary>
	// Token: 0x020007B5 RID: 1973
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		/// <summary>Gets or sets the list of envoys that were contributed by the server context and object chains when the object was marshaled.</summary>
		/// <returns>A chain of envoy sinks.</returns>
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x060055A1 RID: 21921
		// (set) Token: 0x060055A2 RID: 21922
		IMessageSink EnvoySinks
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}

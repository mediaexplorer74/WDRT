using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
	/// <summary>Provides custom channel information that is carried along with the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
	// Token: 0x020007B4 RID: 1972
	[ComVisible(true)]
	public interface IChannelInfo
	{
		/// <summary>Gets or sets the channel data for each channel.</summary>
		/// <returns>The channel data for each channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600559F RID: 21919
		// (set) Token: 0x060055A0 RID: 21920
		object[] ChannelData
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}

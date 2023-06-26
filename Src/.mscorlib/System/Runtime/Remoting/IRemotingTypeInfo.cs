using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
	/// <summary>Provides type information for an object.</summary>
	// Token: 0x020007B3 RID: 1971
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		/// <summary>Gets or sets the fully qualified type name of the server object in a <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The fully qualified type name of the server object in a <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600559C RID: 21916
		// (set) Token: 0x0600559D RID: 21917
		string TypeName
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Checks whether the proxy that represents the specified object type can be cast to the type represented by the <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> interface.</summary>
		/// <param name="fromType">The type to cast to.</param>
		/// <param name="o">The object for which to check casting.</param>
		/// <returns>
		///   <see langword="true" /> if cast will succeed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x0600559E RID: 21918
		[SecurityCritical]
		bool CanCastTo(Type fromType, object o);
	}
}

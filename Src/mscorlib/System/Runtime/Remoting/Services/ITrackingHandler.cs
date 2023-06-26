using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Services
{
	/// <summary>Indicates that the implementing object must be notified of marshaling, unmarshaling, and disconnection of objects and proxies by the remoting infrastructure.</summary>
	// Token: 0x02000804 RID: 2052
	[ComVisible(true)]
	public interface ITrackingHandler
	{
		/// <summary>Notifies the current instance that an object has been marshaled.</summary>
		/// <param name="obj">The object that has been marshaled.</param>
		/// <param name="or">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that results from marshaling and represents the specified object.</param>
		// Token: 0x0600588A RID: 22666
		[SecurityCritical]
		void MarshaledObject(object obj, ObjRef or);

		/// <summary>Notifies the current instance that an object has been unmarshaled.</summary>
		/// <param name="obj">The unmarshaled object.</param>
		/// <param name="or">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the specified object.</param>
		// Token: 0x0600588B RID: 22667
		[SecurityCritical]
		void UnmarshaledObject(object obj, ObjRef or);

		/// <summary>Notifies the current instance that an object has been disconnected from its proxy.</summary>
		/// <param name="obj">The disconnected object.</param>
		// Token: 0x0600588C RID: 22668
		[SecurityCritical]
		void DisconnectedObject(object obj);
	}
}

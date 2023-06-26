using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPoint" /> instead.</summary>
	// Token: 0x0200097B RID: 2427
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPoint instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPoint
	{
		/// <summary>Returns the IID of the outgoing interface managed by this connection point.</summary>
		/// <param name="pIID">On successful return, contains the IID of the outgoing interface managed by this connection point.</param>
		// Token: 0x0600629A RID: 25242
		void GetConnectionInterface(out Guid pIID);

		/// <summary>Retrieves the <see langword="IConnectionPointContainer" /> interface pointer to the connectable object that conceptually owns this connection point.</summary>
		/// <param name="ppCPC">On successful return, contains the connectable object's <see langword="IConnectionPointContainer" /> interface.</param>
		// Token: 0x0600629B RID: 25243
		void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

		/// <summary>Establishes an advisory connection between the connection point and the caller's sink object.</summary>
		/// <param name="pUnkSink">Reference to the sink to receive calls for the outgoing interface managed by this connection point.</param>
		/// <param name="pdwCookie">On successful return, contains the connection cookie.</param>
		// Token: 0x0600629C RID: 25244
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		/// <summary>Terminates an advisory connection previously established through <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />.</summary>
		/// <param name="dwCookie">The connection cookie previously returned from <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />.</param>
		// Token: 0x0600629D RID: 25245
		void Unadvise(int dwCookie);

		/// <summary>Creates an enumerator object for iteration through the connections that exist to this connection point.</summary>
		/// <param name="ppEnum">On successful return, contains the newly created enumerator.</param>
		// Token: 0x0600629E RID: 25246
		void EnumConnections(out UCOMIEnumConnections ppEnum);
	}
}

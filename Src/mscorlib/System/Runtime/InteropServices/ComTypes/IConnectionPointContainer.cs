using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="IConnectionPointContainer" /> interface.</summary>
	// Token: 0x02000A23 RID: 2595
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IConnectionPointContainer
	{
		/// <summary>Creates an enumerator of all the connection points supported in the connectable object, one connection point per IID.</summary>
		/// <param name="ppEnum">When this method returns, contains the interface pointer of the enumerator. This parameter is passed uninitialized.</param>
		// Token: 0x0600662B RID: 26155
		[__DynamicallyInvokable]
		void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

		/// <summary>Asks the connectable object if it has a connection point for a particular IID, and if so, returns the <see langword="IConnectionPoint" /> interface pointer to that connection point.</summary>
		/// <param name="riid">A reference to the outgoing interface IID whose connection point is being requested.</param>
		/// <param name="ppCP">When this method returns, contains the connection point that manages the outgoing interface <paramref name="riid" />. This parameter is passed uninitialized.</param>
		// Token: 0x0600662C RID: 26156
		[__DynamicallyInvokable]
		void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
	}
}

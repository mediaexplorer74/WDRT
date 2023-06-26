using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x020002B3 RID: 691
	[global::__DynamicallyInvokable]
	public abstract class IPv6InterfaceProperties
	{
		/// <summary>Gets the index of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the index of the network interface for IPv6 address.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060019A7 RID: 6567
		[global::__DynamicallyInvokable]
		public abstract int Index
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060019A8 RID: 6568
		[global::__DynamicallyInvokable]
		public abstract int Mtu
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the scope ID of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <param name="scopeLevel">The scope level.</param>
		/// <returns>The scope ID of the network interface associated with an IPv6 address.</returns>
		// Token: 0x060019A9 RID: 6569 RVA: 0x0007DEBC File Offset: 0x0007C0BC
		[global::__DynamicallyInvokable]
		public virtual long GetScopeId(ScopeLevel scopeLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPv6InterfaceProperties" /> class.</summary>
		// Token: 0x060019AA RID: 6570 RVA: 0x0007DEC3 File Offset: 0x0007C0C3
		[global::__DynamicallyInvokable]
		protected IPv6InterfaceProperties()
		{
		}
	}
}

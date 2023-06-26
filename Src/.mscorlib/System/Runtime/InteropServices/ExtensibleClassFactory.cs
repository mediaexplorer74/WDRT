using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Enables customization of managed objects that extend from unmanaged objects during creation.</summary>
	// Token: 0x02000961 RID: 2401
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x0600623B RID: 25147 RVA: 0x00150EDD File Offset: 0x0014F0DD
		private ExtensibleClassFactory()
		{
		}

		/// <summary>Registers a <see langword="delegate" /> that is called when an instance of a managed type, that extends from an unmanaged type, needs to allocate the aggregated unmanaged object.</summary>
		/// <param name="callback">A <see langword="delegate" /> that is called in place of <see langword="CoCreateInstance" />.</param>
		// Token: 0x0600623C RID: 25148
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterObjectCreationCallback(ObjectCreationDelegate callback);
	}
}

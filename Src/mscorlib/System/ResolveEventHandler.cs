using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a method that handles the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, or <see cref="E:System.AppDomain.AssemblyResolve" /> event of an <see cref="T:System.AppDomain" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="args">The event data.</param>
	/// <returns>The assembly that resolves the type, assembly, or resource; or <see langword="null" /> if the assembly cannot be resolved.</returns>
	// Token: 0x02000093 RID: 147
	// (Invoke) Token: 0x06000778 RID: 1912
	[ComVisible(true)]
	[Serializable]
	public delegate Assembly ResolveEventHandler(object sender, ResolveEventArgs args);
}

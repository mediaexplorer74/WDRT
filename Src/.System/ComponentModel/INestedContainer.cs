using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for nested containers, which logically contain zero or more other components and are owned by a parent component.</summary>
	// Token: 0x02000565 RID: 1381
	public interface INestedContainer : IContainer, IDisposable
	{
		/// <summary>Gets the owning component for the nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns the nested container.</returns>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600339C RID: 13212
		IComponent Owner { get; }
	}
}

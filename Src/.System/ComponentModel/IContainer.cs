using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for containers. Containers are objects that logically contain zero or more components.</summary>
	// Token: 0x0200055D RID: 1373
	[ComVisible(true)]
	public interface IContainer : IDisposable
	{
		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add.</param>
		// Token: 0x0600337C RID: 13180
		void Add(IComponent component);

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list, and assigns a name to the component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add.</param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.  
		///  -or-  
		///  <see langword="null" /> that leaves the component unnamed.</param>
		// Token: 0x0600337D RID: 13181
		void Add(IComponent component, string name);

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.IComponent" /> objects that represents all the components in the <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600337E RID: 13182
		ComponentCollection Components { get; }

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove.</param>
		// Token: 0x0600337F RID: 13183
		void Remove(IComponent component);
	}
}

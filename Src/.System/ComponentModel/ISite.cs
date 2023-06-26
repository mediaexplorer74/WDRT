using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by sites.</summary>
	// Token: 0x02000575 RID: 1397
	[ComVisible(true)]
	public interface ISite : IServiceProvider
	{
		/// <summary>Gets the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060033CF RID: 13263
		IComponent Component { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060033D0 RID: 13264
		IContainer Container { get; }

		/// <summary>Determines whether the component is in design mode when implemented by a class.</summary>
		/// <returns>
		///   <see langword="true" /> if the component is in design mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060033D1 RID: 13265
		bool DesignMode { get; }

		/// <summary>Gets or sets the name of the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The name of the component associated with the <see cref="T:System.ComponentModel.ISite" />; or <see langword="null" />, if no name is assigned to the component.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060033D2 RID: 13266
		// (set) Token: 0x060033D3 RID: 13267
		string Name { get; set; }
	}
}

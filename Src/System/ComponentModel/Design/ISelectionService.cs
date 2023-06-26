using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for a designer to select components.</summary>
	// Token: 0x020005F5 RID: 1525
	[ComVisible(true)]
	public interface ISelectionService
	{
		/// <summary>Gets the object that is currently the primary selected object.</summary>
		/// <returns>The object that is currently the primary selected object.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06003835 RID: 14389
		object PrimarySelection { get; }

		/// <summary>Gets the count of selected objects.</summary>
		/// <returns>The number of selected objects.</returns>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003836 RID: 14390
		int SelectionCount { get; }

		/// <summary>Occurs when the current selection changes.</summary>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06003837 RID: 14391
		// (remove) Token: 0x06003838 RID: 14392
		event EventHandler SelectionChanged;

		/// <summary>Occurs when the current selection is about to change.</summary>
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06003839 RID: 14393
		// (remove) Token: 0x0600383A RID: 14394
		event EventHandler SelectionChanging;

		/// <summary>Gets a value indicating whether the specified component is currently selected.</summary>
		/// <param name="component">The component to test.</param>
		/// <returns>
		///   <see langword="true" /> if the component is part of the user's current selection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600383B RID: 14395
		bool GetComponentSelected(object component);

		/// <summary>Gets a collection of components that are currently selected.</summary>
		/// <returns>A collection that represents the current set of components that are selected.</returns>
		// Token: 0x0600383C RID: 14396
		ICollection GetSelectedComponents();

		/// <summary>Selects the specified collection of components.</summary>
		/// <param name="components">The collection of components to select.</param>
		// Token: 0x0600383D RID: 14397
		void SetSelectedComponents(ICollection components);

		/// <summary>Selects the components from within the specified collection of components that match the specified selection type.</summary>
		/// <param name="components">The collection of components to select.</param>
		/// <param name="selectionType">A value from the <see cref="T:System.ComponentModel.Design.SelectionTypes" /> enumeration. The default is <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />.</param>
		// Token: 0x0600383E RID: 14398
		void SetSelectedComponents(ICollection components, SelectionTypes selectionType);
	}
}

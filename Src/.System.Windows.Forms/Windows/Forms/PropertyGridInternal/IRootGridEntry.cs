using System;
using System.ComponentModel;

namespace System.Windows.Forms.PropertyGridInternal
{
	/// <summary>Defines methods and a property that allow filtering on specific attributes.</summary>
	// Token: 0x0200050C RID: 1292
	public interface IRootGridEntry
	{
		/// <summary>Gets or sets the attributes on which the property browser filters.</summary>
		/// <returns>The attributes on which the property browser filters.</returns>
		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060054BE RID: 21694
		// (set) Token: 0x060054BF RID: 21695
		AttributeCollection BrowsableAttributes { get; set; }

		/// <summary>Resets the <see cref="P:System.Windows.Forms.PropertyGridInternal.IRootGridEntry.BrowsableAttributes" /> property to the default value.</summary>
		// Token: 0x060054C0 RID: 21696
		void ResetBrowsableAttributes();

		/// <summary>Sorts the properties in the property browser.</summary>
		/// <param name="showCategories">
		///   <see langword="true" /> to group the properties by category; otherwise, <see langword="false" />.</param>
		// Token: 0x060054C1 RID: 21697
		void ShowCategories(bool showCategories);
	}
}

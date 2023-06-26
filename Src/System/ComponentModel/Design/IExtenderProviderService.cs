using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for adding and removing extender providers at design time.</summary>
	// Token: 0x020005EE RID: 1518
	public interface IExtenderProviderService
	{
		/// <summary>Adds the specified extender provider.</summary>
		/// <param name="provider">The extender provider to add.</param>
		// Token: 0x06003819 RID: 14361
		void AddExtenderProvider(IExtenderProvider provider);

		/// <summary>Removes the specified extender provider.</summary>
		/// <param name="provider">The extender provider to remove.</param>
		// Token: 0x0600381A RID: 14362
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}

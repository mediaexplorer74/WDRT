using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for root-level designer view technologies.</summary>
	// Token: 0x020005F4 RID: 1524
	[ComVisible(true)]
	public interface IRootDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets the set of technologies that this designer can support for its display.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06003833 RID: 14387
		ViewTechnology[] SupportedTechnologies { get; }

		/// <summary>Gets a view object for the specified view technology.</summary>
		/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
		/// <returns>An object that represents the view for this designer.</returns>
		/// <exception cref="T:System.ArgumentException">The specified view technology is not supported or does not exist.</exception>
		// Token: 0x06003834 RID: 14388
		object GetView(ViewTechnology technology);
	}
}

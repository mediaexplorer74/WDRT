using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x0200052F RID: 1327
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ContainerFilterService
	{
		/// <summary>Filters the component collection.</summary>
		/// <param name="components">The component collection to filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		// Token: 0x06003223 RID: 12835 RVA: 0x000E0C4C File Offset: 0x000DEE4C
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}

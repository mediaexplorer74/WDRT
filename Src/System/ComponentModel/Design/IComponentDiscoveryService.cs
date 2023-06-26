using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Enables enumeration of components at design time.</summary>
	// Token: 0x020005E3 RID: 1507
	public interface IComponentDiscoveryService
	{
		/// <summary>Gets the list of available component types.</summary>
		/// <param name="designerHost">The designer host providing design-time services. Can be <see langword="null" />.</param>
		/// <param name="baseType">The base type specifying the components to retrieve. Can be <see langword="null" />.</param>
		/// <returns>The list of available component types.</returns>
		// Token: 0x060037D7 RID: 14295
		ICollection GetComponentTypes(IDesignerHost designerHost, Type baseType);
	}
}

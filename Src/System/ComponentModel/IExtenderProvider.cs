using System;

namespace System.ComponentModel
{
	/// <summary>Defines the interface for extending properties to other components in a container.</summary>
	// Token: 0x02000561 RID: 1377
	public interface IExtenderProvider
	{
		/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
		/// <param name="extendee">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		/// <returns>
		///   <see langword="true" /> if this object can provide extender properties to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003391 RID: 13201
		bool CanExtend(object extendee);
	}
}

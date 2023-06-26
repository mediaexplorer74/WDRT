using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the base class for a custom component editor.</summary>
	// Token: 0x02000517 RID: 1303
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ComponentEditor
	{
		/// <summary>Edits the component and returns a value indicating whether the component was modified.</summary>
		/// <param name="component">The component to be edited.</param>
		/// <returns>
		///   <see langword="true" /> if the component was modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600315C RID: 12636 RVA: 0x000DF088 File Offset: 0x000DD288
		public bool EditComponent(object component)
		{
			return this.EditComponent(null, component);
		}

		/// <summary>Edits the component and returns a value indicating whether the component was modified based upon a given context.</summary>
		/// <param name="context">An optional context object that can be used to obtain further information about the edit.</param>
		/// <param name="component">The component to be edited.</param>
		/// <returns>
		///   <see langword="true" /> if the component was modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600315D RID: 12637
		public abstract bool EditComponent(ITypeDescriptorContext context, object component);
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> events.</summary>
	// Token: 0x020005CF RID: 1487
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>Gets the component associated with the event.</summary>
		/// <returns>The component associated with the event.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000EFC55 File Offset: 0x000EDE55
		public virtual IComponent Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> class.</summary>
		/// <param name="component">The component that is the source of the event.</param>
		// Token: 0x06003762 RID: 14178 RVA: 0x000EFC5D File Offset: 0x000EDE5D
		public ComponentEventArgs(IComponent component)
		{
			this.component = component;
		}

		// Token: 0x04002ADA RID: 10970
		private IComponent component;
	}
}

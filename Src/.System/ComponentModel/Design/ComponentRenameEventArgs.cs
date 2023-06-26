using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	// Token: 0x020005D1 RID: 1489
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ComponentRenameEventArgs : EventArgs
	{
		/// <summary>Gets the component that is being renamed.</summary>
		/// <returns>The component that is being renamed.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x000EFC6C File Offset: 0x000EDE6C
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the name of the component before the rename event.</summary>
		/// <returns>The previous name of the component.</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000EFC74 File Offset: 0x000EDE74
		public virtual string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		/// <summary>Gets the name of the component after the rename event.</summary>
		/// <returns>The name of the component after the rename event.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x000EFC7C File Offset: 0x000EDE7C
		public virtual string NewName
		{
			get
			{
				return this.newName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> class.</summary>
		/// <param name="component">The component to be renamed.</param>
		/// <param name="oldName">The old name of the component.</param>
		/// <param name="newName">The new name of the component.</param>
		// Token: 0x0600376A RID: 14186 RVA: 0x000EFC84 File Offset: 0x000EDE84
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.oldName = oldName;
			this.newName = newName;
			this.component = component;
		}

		// Token: 0x04002ADB RID: 10971
		private object component;

		// Token: 0x04002ADC RID: 10972
		private string oldName;

		// Token: 0x04002ADD RID: 10973
		private string newName;
	}
}

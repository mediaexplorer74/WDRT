using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event that occurs when components are added to the toolbox.</summary>
	// Token: 0x0200007B RID: 123
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ToolboxComponentsCreatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> class.</summary>
		/// <param name="components">The components to include in the toolbox.</param>
		// Token: 0x06000869 RID: 2153 RVA: 0x00020E01 File Offset: 0x0001F001
		public ToolboxComponentsCreatedEventArgs(IComponent[] components)
		{
			this.comps = components;
		}

		/// <summary>Gets or sets an array containing the components to add to the toolbox.</summary>
		/// <returns>An array of type <see cref="T:System.ComponentModel.IComponent" /> indicating the components to add to the toolbox.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00020E10 File Offset: 0x0001F010
		public IComponent[] Components
		{
			get
			{
				return (IComponent[])this.comps.Clone();
			}
		}

		// Token: 0x0400070B RID: 1803
		private readonly IComponent[] comps;
	}
}

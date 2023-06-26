using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event that occurs when components are added to the toolbox.</summary>
	// Token: 0x0200007D RID: 125
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ToolboxComponentsCreatingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> class.</summary>
		/// <param name="host">The designer host that is making the request.</param>
		// Token: 0x0600086F RID: 2159 RVA: 0x00020E22 File Offset: 0x0001F022
		public ToolboxComponentsCreatingEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components, or <see langword="null" /> if no designer host was provided to the toolbox item.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00020E31 File Offset: 0x0001F031
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x0400070C RID: 1804
		private readonly IDesignerHost host;
	}
}

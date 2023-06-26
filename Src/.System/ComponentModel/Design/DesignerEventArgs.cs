using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events.</summary>
	// Token: 0x020005DD RID: 1501
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> class.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</param>
		// Token: 0x060037B9 RID: 14265 RVA: 0x000F067A File Offset: 0x000EE87A
		public DesignerEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		/// <summary>Gets the host of the document.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</returns>
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000F0689 File Offset: 0x000EE889
		public IDesignerHost Designer
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x04002AEA RID: 10986
		private readonly IDesignerHost host;
	}
}

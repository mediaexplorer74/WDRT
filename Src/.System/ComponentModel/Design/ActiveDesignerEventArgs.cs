using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="P:System.ComponentModel.Design.IDesignerEventService.ActiveDesigner" /> event.</summary>
	// Token: 0x020005C7 RID: 1479
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ActiveDesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignerEventArgs" /> class.</summary>
		/// <param name="oldDesigner">The document that is losing activation.</param>
		/// <param name="newDesigner">The document that is gaining activation.</param>
		// Token: 0x0600373E RID: 14142 RVA: 0x000EFAA4 File Offset: 0x000EDCA4
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.oldDesigner = oldDesigner;
			this.newDesigner = newDesigner;
		}

		/// <summary>Gets the document that is losing activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document losing activation.</returns>
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000EFABA File Offset: 0x000EDCBA
		public IDesignerHost OldDesigner
		{
			get
			{
				return this.oldDesigner;
			}
		}

		/// <summary>Gets the document that is gaining activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document gaining activation.</returns>
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000EFAC2 File Offset: 0x000EDCC2
		public IDesignerHost NewDesigner
		{
			get
			{
				return this.newDesigner;
			}
		}

		// Token: 0x04002ACF RID: 10959
		private readonly IDesignerHost oldDesigner;

		// Token: 0x04002AD0 RID: 10960
		private readonly IDesignerHost newDesigner;
	}
}

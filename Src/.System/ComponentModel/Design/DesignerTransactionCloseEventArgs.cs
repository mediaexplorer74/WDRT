using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events.</summary>
	// Token: 0x020005D5 RID: 1493
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class, using the specified value that indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		// Token: 0x06003783 RID: 14211 RVA: 0x000EFEE3 File Offset: 0x000EE0E3
		[Obsolete("This constructor is obsolete. Use DesignerTransactionCloseEventArgs(bool, bool) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public DesignerTransactionCloseEventArgs(bool commit)
			: this(commit, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		/// <param name="lastTransaction">
		///   <see langword="true" /> if this is the last transaction to close; otherwise, <see langword="false" />.</param>
		// Token: 0x06003784 RID: 14212 RVA: 0x000EFEED File Offset: 0x000EE0ED
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.commit = commit;
			this.lastTransaction = lastTransaction;
		}

		/// <summary>Indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000EFF03 File Offset: 0x000EE103
		public bool TransactionCommitted
		{
			get
			{
				return this.commit;
			}
		}

		/// <summary>Gets a value indicating whether this is the last transaction to close.</summary>
		/// <returns>
		///   <see langword="true" />, if this is the last transaction to close; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x000EFF0B File Offset: 0x000EE10B
		public bool LastTransaction
		{
			get
			{
				return this.lastTransaction;
			}
		}

		// Token: 0x04002AE3 RID: 10979
		private bool commit;

		// Token: 0x04002AE4 RID: 10980
		private bool lastTransaction;
	}
}

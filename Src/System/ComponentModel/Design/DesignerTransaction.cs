using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a way to group a series of design-time actions to improve performance and enable most types of changes to be undone.</summary>
	// Token: 0x020005D4 RID: 1492
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class DesignerTransaction : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class with no description.</summary>
		// Token: 0x06003777 RID: 14199 RVA: 0x000EFE05 File Offset: 0x000EE005
		protected DesignerTransaction()
			: this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class using the specified transaction description.</summary>
		/// <param name="description">A description for this transaction.</param>
		// Token: 0x06003778 RID: 14200 RVA: 0x000EFE12 File Offset: 0x000EE012
		protected DesignerTransaction(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets a value indicating whether the transaction was canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the transaction was canceled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000EFE21 File Offset: 0x000EE021
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
		}

		/// <summary>Gets a value indicating whether the transaction was committed.</summary>
		/// <returns>
		///   <see langword="true" /> if the transaction was committed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x000EFE29 File Offset: 0x000EE029
		public bool Committed
		{
			get
			{
				return this.committed;
			}
		}

		/// <summary>Gets a description for the transaction.</summary>
		/// <returns>A description for the transaction.</returns>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000EFE31 File Offset: 0x000EE031
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		/// <summary>Cancels the transaction and attempts to roll back the changes made by the events of the transaction.</summary>
		// Token: 0x0600377C RID: 14204 RVA: 0x000EFE39 File Offset: 0x000EE039
		public void Cancel()
		{
			if (!this.canceled && !this.committed)
			{
				this.canceled = true;
				GC.SuppressFinalize(this);
				this.suppressedFinalization = true;
				this.OnCancel();
			}
		}

		/// <summary>Commits this transaction.</summary>
		// Token: 0x0600377D RID: 14205 RVA: 0x000EFE65 File Offset: 0x000EE065
		public void Commit()
		{
			if (!this.committed && !this.canceled)
			{
				this.committed = true;
				GC.SuppressFinalize(this);
				this.suppressedFinalization = true;
				this.OnCommit();
			}
		}

		/// <summary>Raises the <see langword="Cancel" /> event.</summary>
		// Token: 0x0600377E RID: 14206
		protected abstract void OnCancel();

		/// <summary>Performs the actual work of committing a transaction.</summary>
		// Token: 0x0600377F RID: 14207
		protected abstract void OnCommit();

		/// <summary>Releases the resources associated with this object. This override commits this transaction if it was not already committed.</summary>
		// Token: 0x06003780 RID: 14208 RVA: 0x000EFE94 File Offset: 0x000EE094
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" />.</summary>
		// Token: 0x06003781 RID: 14209 RVA: 0x000EFEC4 File Offset: 0x000EE0C4
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			if (!this.suppressedFinalization)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003782 RID: 14210 RVA: 0x000EFEDB File Offset: 0x000EE0DB
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
		}

		// Token: 0x04002ADF RID: 10975
		private bool committed;

		// Token: 0x04002AE0 RID: 10976
		private bool canceled;

		// Token: 0x04002AE1 RID: 10977
		private bool suppressedFinalization;

		// Token: 0x04002AE2 RID: 10978
		private string desc;
	}
}

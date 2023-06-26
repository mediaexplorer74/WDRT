using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores linked resources to be sent as part of an email message.</summary>
	// Token: 0x02000269 RID: 617
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x00076514 File Offset: 0x00074714
		internal LinkedResourceCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.LinkedResourceCollection" />.</summary>
		// Token: 0x06001722 RID: 5922 RVA: 0x0007651C File Offset: 0x0007471C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			foreach (LinkedResource linkedResource in this)
			{
				linkedResource.Dispose();
			}
			base.Clear();
			this.disposed = true;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0007657C File Offset: 0x0007477C
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0007659E File Offset: 0x0007479E
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000765BF File Offset: 0x000747BF
		protected override void SetItem(int index, LinkedResource item)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000765F0 File Offset: 0x000747F0
		protected override void InsertItem(int index, LinkedResource item)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x04001793 RID: 6035
		private bool disposed;
	}
}

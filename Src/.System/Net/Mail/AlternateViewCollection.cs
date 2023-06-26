using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Represents a collection of <see cref="T:System.Net.Mail.AlternateView" /> objects.</summary>
	// Token: 0x02000255 RID: 597
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x00075308 File Offset: 0x00073508
		internal AlternateViewCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AlternateViewCollection" />.</summary>
		// Token: 0x060016AB RID: 5803 RVA: 0x00075310 File Offset: 0x00073510
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			foreach (AlternateView alternateView in this)
			{
				alternateView.Dispose();
			}
			base.Clear();
			this.disposed = true;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00075370 File Offset: 0x00073570
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00075392 File Offset: 0x00073592
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000753B3 File Offset: 0x000735B3
		protected override void SetItem(int index, AlternateView item)
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

		// Token: 0x060016AF RID: 5807 RVA: 0x000753E4 File Offset: 0x000735E4
		protected override void InsertItem(int index, AlternateView item)
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

		// Token: 0x04001757 RID: 5975
		private bool disposed;
	}
}

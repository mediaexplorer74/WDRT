using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores attachments to be sent as part of an email message.</summary>
	// Token: 0x02000258 RID: 600
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x00075CC8 File Offset: 0x00073EC8
		internal AttachmentCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AttachmentCollection" />.</summary>
		// Token: 0x060016DC RID: 5852 RVA: 0x00075CD0 File Offset: 0x00073ED0
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			foreach (Attachment attachment in this)
			{
				attachment.Dispose();
			}
			base.Clear();
			this.disposed = true;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00075D30 File Offset: 0x00073F30
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00075D52 File Offset: 0x00073F52
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00075D73 File Offset: 0x00073F73
		protected override void SetItem(int index, Attachment item)
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

		// Token: 0x060016E0 RID: 5856 RVA: 0x00075DA4 File Offset: 0x00073FA4
		protected override void InsertItem(int index, Attachment item)
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

		// Token: 0x0400175C RID: 5980
		private bool disposed;
	}
}

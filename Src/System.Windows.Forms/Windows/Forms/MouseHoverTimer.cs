using System;

namespace System.Windows.Forms
{
	// Token: 0x020003B0 RID: 944
	internal class MouseHoverTimer : IDisposable
	{
		// Token: 0x06003ECA RID: 16074 RVA: 0x0011056C File Offset: 0x0010E76C
		public MouseHoverTimer()
		{
			int num = SystemInformation.MouseHoverTime;
			if (num == 0)
			{
				num = 400;
			}
			this.mouseHoverTimer.Interval = num;
			this.mouseHoverTimer.Tick += this.OnTick;
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x001105BC File Offset: 0x0010E7BC
		public void Start(ToolStripItem item)
		{
			if (item != this.currentItem)
			{
				this.Cancel(this.currentItem);
			}
			this.currentItem = item;
			if (this.currentItem != null)
			{
				this.mouseHoverTimer.Enabled = true;
			}
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x001105EE File Offset: 0x0010E7EE
		public void Cancel()
		{
			this.mouseHoverTimer.Enabled = false;
			this.currentItem = null;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x00110603 File Offset: 0x0010E803
		public void Cancel(ToolStripItem item)
		{
			if (item == this.currentItem)
			{
				this.Cancel();
			}
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00110614 File Offset: 0x0010E814
		public void Dispose()
		{
			if (this.mouseHoverTimer != null)
			{
				this.Cancel();
				this.mouseHoverTimer.Dispose();
				this.mouseHoverTimer = null;
			}
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x00110636 File Offset: 0x0010E836
		private void OnTick(object sender, EventArgs e)
		{
			this.mouseHoverTimer.Enabled = false;
			if (this.currentItem != null && !this.currentItem.IsDisposed)
			{
				this.currentItem.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseHover);
			}
		}

		// Token: 0x04002486 RID: 9350
		private Timer mouseHoverTimer = new Timer();

		// Token: 0x04002487 RID: 9351
		private const int SPI_GETMOUSEHOVERTIME_WIN9X = 400;

		// Token: 0x04002488 RID: 9352
		private ToolStripItem currentItem;
	}
}

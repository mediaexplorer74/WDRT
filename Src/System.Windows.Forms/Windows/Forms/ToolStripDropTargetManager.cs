using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x020003C2 RID: 962
	internal class ToolStripDropTargetManager : IDropTarget
	{
		// Token: 0x06004152 RID: 16722 RVA: 0x0011744E File Offset: 0x0011564E
		public ToolStripDropTargetManager(ToolStrip owner)
		{
			this.owner = owner;
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x0011746B File Offset: 0x0011566B
		public void EnsureRegistered(IDropTarget dropTarget)
		{
			this.SetAcceptDrops(true);
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x00117474 File Offset: 0x00115674
		public void EnsureUnRegistered(IDropTarget dropTarget)
		{
			for (int i = 0; i < this.owner.Items.Count; i++)
			{
				if (this.owner.Items[i].AllowDrop)
				{
					return;
				}
			}
			if (this.owner.AllowDrop || this.owner.AllowItemReorder)
			{
				return;
			}
			this.SetAcceptDrops(false);
			this.owner.DropTargetManager = null;
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x001174E3 File Offset: 0x001156E3
		private ToolStripItem FindItemAtPoint(int x, int y)
		{
			return this.owner.GetItemAt(this.owner.PointToClient(new Point(x, y)));
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x00117504 File Offset: 0x00115704
		public void OnDragEnter(DragEventArgs e)
		{
			if (this.owner.AllowItemReorder && e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				this.lastDropTarget = this.owner.ItemReorderDropTarget;
			}
			else
			{
				ToolStripItem toolStripItem = this.FindItemAtPoint(e.X, e.Y);
				if (toolStripItem != null && toolStripItem.AllowDrop)
				{
					this.lastDropTarget = toolStripItem;
				}
				else if (this.owner.AllowDrop)
				{
					this.lastDropTarget = this.owner;
				}
				else
				{
					this.lastDropTarget = null;
				}
			}
			if (this.lastDropTarget != null)
			{
				this.lastDropTarget.OnDragEnter(e);
			}
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x001175A8 File Offset: 0x001157A8
		public void OnDragOver(DragEventArgs e)
		{
			IDropTarget dropTarget;
			if (this.owner.AllowItemReorder && e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				dropTarget = this.owner.ItemReorderDropTarget;
			}
			else
			{
				ToolStripItem toolStripItem = this.FindItemAtPoint(e.X, e.Y);
				if (toolStripItem != null && toolStripItem.AllowDrop)
				{
					dropTarget = toolStripItem;
				}
				else if (this.owner.AllowDrop)
				{
					dropTarget = this.owner;
				}
				else
				{
					dropTarget = null;
				}
			}
			if (dropTarget != this.lastDropTarget)
			{
				this.UpdateDropTarget(dropTarget, e);
			}
			if (this.lastDropTarget != null)
			{
				this.lastDropTarget.OnDragOver(e);
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00117648 File Offset: 0x00115848
		public void OnDragLeave(EventArgs e)
		{
			if (this.lastDropTarget != null)
			{
				this.lastDropTarget.OnDragLeave(e);
			}
			this.lastDropTarget = null;
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x00117665 File Offset: 0x00115865
		public void OnDragDrop(DragEventArgs e)
		{
			if (this.lastDropTarget != null)
			{
				this.lastDropTarget.OnDragDrop(e);
			}
			this.lastDropTarget = null;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x00117684 File Offset: 0x00115884
		private void SetAcceptDrops(bool accept)
		{
			if (this.owner.AllowDrop && accept)
			{
				IntSecurity.ClipboardRead.Demand();
			}
			if (accept && this.owner.IsHandleCreated)
			{
				try
				{
					if (Application.OleRequired() != ApartmentState.STA)
					{
						throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
					}
					if (accept)
					{
						int num = UnsafeNativeMethods.RegisterDragDrop(new HandleRef(this.owner, this.owner.Handle), new DropTarget(this));
						if (num != 0 && num != -2147221247)
						{
							throw new Win32Exception(num);
						}
					}
					else
					{
						IntSecurity.ClipboardRead.Assert();
						try
						{
							int num2 = UnsafeNativeMethods.RevokeDragDrop(new HandleRef(this.owner, this.owner.Handle));
							if (num2 != 0 && num2 != -2147221248)
							{
								throw new Win32Exception(num2);
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), ex);
				}
			}
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x00117780 File Offset: 0x00115980
		private void UpdateDropTarget(IDropTarget newTarget, DragEventArgs e)
		{
			if (newTarget != this.lastDropTarget)
			{
				if (this.lastDropTarget != null)
				{
					this.OnDragLeave(new EventArgs());
				}
				this.lastDropTarget = newTarget;
				if (newTarget != null)
				{
					this.OnDragEnter(new DragEventArgs(e.Data, e.KeyState, e.X, e.Y, e.AllowedEffect, e.Effect)
					{
						Effect = DragDropEffects.None
					});
				}
			}
		}

		// Token: 0x04002507 RID: 9479
		private IDropTarget lastDropTarget;

		// Token: 0x04002508 RID: 9480
		private ToolStrip owner;

		// Token: 0x04002509 RID: 9481
		internal static readonly TraceSwitch DragDropDebug;
	}
}

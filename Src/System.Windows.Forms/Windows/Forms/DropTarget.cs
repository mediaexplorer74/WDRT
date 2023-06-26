using System;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms
{
	// Token: 0x02000247 RID: 583
	internal class DropTarget : UnsafeNativeMethods.IOleDropTarget
	{
		// Token: 0x0600250B RID: 9483 RVA: 0x000AD3A8 File Offset: 0x000AB5A8
		public DropTarget(IDropTarget owner)
		{
			this.owner = owner;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000AD3B8 File Offset: 0x000AB5B8
		private DragEventArgs CreateDragEventArgs(object pDataObj, int grfKeyState, NativeMethods.POINTL pt, int pdwEffect)
		{
			IDataObject dataObject;
			if (pDataObj == null)
			{
				dataObject = this.lastDataObject;
			}
			else if (pDataObj is IDataObject)
			{
				dataObject = (IDataObject)pDataObj;
			}
			else
			{
				if (!(pDataObj is IDataObject))
				{
					return null;
				}
				dataObject = new DataObject(pDataObj);
			}
			DragEventArgs dragEventArgs = new DragEventArgs(dataObject, grfKeyState, pt.x, pt.y, (DragDropEffects)pdwEffect, this.lastEffect);
			this.lastDataObject = dataObject;
			return dragEventArgs;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000AD41C File Offset: 0x000AB61C
		int UnsafeNativeMethods.IOleDropTarget.OleDragEnter(object pDataObj, int grfKeyState, UnsafeNativeMethods.POINTSTRUCT pt, ref int pdwEffect)
		{
			DragEventArgs dragEventArgs = this.CreateDragEventArgs(pDataObj, grfKeyState, new NativeMethods.POINTL
			{
				x = pt.x,
				y = pt.y
			}, pdwEffect);
			if (dragEventArgs != null)
			{
				this.owner.OnDragEnter(dragEventArgs);
				pdwEffect = (int)dragEventArgs.Effect;
				this.lastEffect = dragEventArgs.Effect;
			}
			else
			{
				pdwEffect = 0;
			}
			return 0;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000AD480 File Offset: 0x000AB680
		int UnsafeNativeMethods.IOleDropTarget.OleDragOver(int grfKeyState, UnsafeNativeMethods.POINTSTRUCT pt, ref int pdwEffect)
		{
			DragEventArgs dragEventArgs = this.CreateDragEventArgs(null, grfKeyState, new NativeMethods.POINTL
			{
				x = pt.x,
				y = pt.y
			}, pdwEffect);
			this.owner.OnDragOver(dragEventArgs);
			pdwEffect = (int)dragEventArgs.Effect;
			this.lastEffect = dragEventArgs.Effect;
			return 0;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000AD4D8 File Offset: 0x000AB6D8
		int UnsafeNativeMethods.IOleDropTarget.OleDragLeave()
		{
			this.owner.OnDragLeave(EventArgs.Empty);
			return 0;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000AD4EC File Offset: 0x000AB6EC
		int UnsafeNativeMethods.IOleDropTarget.OleDrop(object pDataObj, int grfKeyState, UnsafeNativeMethods.POINTSTRUCT pt, ref int pdwEffect)
		{
			DragEventArgs dragEventArgs = this.CreateDragEventArgs(pDataObj, grfKeyState, new NativeMethods.POINTL
			{
				x = pt.x,
				y = pt.y
			}, pdwEffect);
			if (dragEventArgs != null)
			{
				this.owner.OnDragDrop(dragEventArgs);
				pdwEffect = (int)dragEventArgs.Effect;
			}
			else
			{
				pdwEffect = 0;
			}
			this.lastEffect = DragDropEffects.None;
			this.lastDataObject = null;
			return 0;
		}

		// Token: 0x04000F5B RID: 3931
		private IDataObject lastDataObject;

		// Token: 0x04000F5C RID: 3932
		private DragDropEffects lastEffect;

		// Token: 0x04000F5D RID: 3933
		private IDropTarget owner;
	}
}

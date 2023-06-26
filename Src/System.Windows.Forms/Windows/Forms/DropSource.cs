using System;

namespace System.Windows.Forms
{
	// Token: 0x02000246 RID: 582
	internal class DropSource : UnsafeNativeMethods.IOleDropSource
	{
		// Token: 0x06002508 RID: 9480 RVA: 0x000AD2EE File Offset: 0x000AB4EE
		public DropSource(ISupportOleDropSource peer)
		{
			if (peer == null)
			{
				throw new ArgumentNullException("peer");
			}
			this.peer = peer;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000AD30C File Offset: 0x000AB50C
		public int OleQueryContinueDrag(int fEscapePressed, int grfKeyState)
		{
			bool flag = fEscapePressed != 0;
			DragAction dragAction = DragAction.Continue;
			if (flag)
			{
				dragAction = DragAction.Cancel;
			}
			else if ((grfKeyState & 1) == 0 && (grfKeyState & 2) == 0 && (grfKeyState & 16) == 0)
			{
				dragAction = DragAction.Drop;
			}
			QueryContinueDragEventArgs queryContinueDragEventArgs = new QueryContinueDragEventArgs(grfKeyState, flag, dragAction);
			this.peer.OnQueryContinueDrag(queryContinueDragEventArgs);
			int num = 0;
			DragAction action = queryContinueDragEventArgs.Action;
			if (action != DragAction.Drop)
			{
				if (action == DragAction.Cancel)
				{
					num = 262401;
				}
			}
			else
			{
				num = 262400;
			}
			return num;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000AD378 File Offset: 0x000AB578
		public int OleGiveFeedback(int dwEffect)
		{
			GiveFeedbackEventArgs giveFeedbackEventArgs = new GiveFeedbackEventArgs((DragDropEffects)dwEffect, true);
			this.peer.OnGiveFeedback(giveFeedbackEventArgs);
			if (giveFeedbackEventArgs.UseDefaultCursors)
			{
				return 262402;
			}
			return 0;
		}

		// Token: 0x04000F57 RID: 3927
		private const int DragDropSDrop = 262400;

		// Token: 0x04000F58 RID: 3928
		private const int DragDropSCancel = 262401;

		// Token: 0x04000F59 RID: 3929
		private const int DragDropSUseDefaultCursors = 262402;

		// Token: 0x04000F5A RID: 3930
		private ISupportOleDropSource peer;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event, which occurs during a drag operation.</summary>
	// Token: 0x02000266 RID: 614
	[ComVisible(true)]
	public class GiveFeedbackEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> class.</summary>
		/// <param name="effect">The type of drag-and-drop operation. Possible values are obtained by applying the bitwise OR (|) operation to the constants defined in the <see cref="T:System.Windows.Forms.DragDropEffects" />.</param>
		/// <param name="useDefaultCursors">
		///   <see langword="true" /> if default pointers are used; otherwise, <see langword="false" />.</param>
		// Token: 0x0600278C RID: 10124 RVA: 0x000B8CE8 File Offset: 0x000B6EE8
		public GiveFeedbackEventArgs(DragDropEffects effect, bool useDefaultCursors)
		{
			this.effect = effect;
			this.useDefaultCursors = useDefaultCursors;
		}

		/// <summary>Gets the drag-and-drop operation feedback that is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x000B8CFE File Offset: 0x000B6EFE
		public DragDropEffects Effect
		{
			get
			{
				return this.effect;
			}
		}

		/// <summary>Gets or sets whether drag operation should use the default cursors that are associated with drag-drop effects.</summary>
		/// <returns>
		///   <see langword="true" /> if the default pointers are used; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x000B8D06 File Offset: 0x000B6F06
		// (set) Token: 0x0600278F RID: 10127 RVA: 0x000B8D0E File Offset: 0x000B6F0E
		public bool UseDefaultCursors
		{
			get
			{
				return this.useDefaultCursors;
			}
			set
			{
				this.useDefaultCursors = value;
			}
		}

		// Token: 0x04001048 RID: 4168
		private readonly DragDropEffects effect;

		// Token: 0x04001049 RID: 4169
		private bool useDefaultCursors;
	}
}

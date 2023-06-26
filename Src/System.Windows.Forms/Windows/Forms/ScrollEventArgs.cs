using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see langword="Scroll" /> event.</summary>
	// Token: 0x02000359 RID: 857
	[ComVisible(true)]
	public class ScrollEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" /> and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</param>
		/// <param name="newValue">The new value for the scroll bar.</param>
		// Token: 0x0600384B RID: 14411 RVA: 0x000FA252 File Offset: 0x000F8452
		public ScrollEventArgs(ScrollEventType type, int newValue)
		{
			this.type = type;
			this.newValue = newValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</param>
		/// <param name="newValue">The new value for the scroll bar.</param>
		/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values.</param>
		// Token: 0x0600384C RID: 14412 RVA: 0x000FA26F File Offset: 0x000F846F
		public ScrollEventArgs(ScrollEventType type, int newValue, ScrollOrientation scroll)
		{
			this.type = type;
			this.newValue = newValue;
			this.scrollOrientation = scroll;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</param>
		/// <param name="oldValue">The old value for the scroll bar.</param>
		/// <param name="newValue">The new value for the scroll bar.</param>
		// Token: 0x0600384D RID: 14413 RVA: 0x000FA293 File Offset: 0x000F8493
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue)
		{
			this.type = type;
			this.newValue = newValue;
			this.oldValue = oldValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</param>
		/// <param name="oldValue">The old value for the scroll bar.</param>
		/// <param name="newValue">The new value for the scroll bar.</param>
		/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values.</param>
		// Token: 0x0600384E RID: 14414 RVA: 0x000FA2B7 File Offset: 0x000F84B7
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
		{
			this.type = type;
			this.newValue = newValue;
			this.scrollOrientation = scroll;
			this.oldValue = oldValue;
		}

		/// <summary>Gets the scroll bar orientation that raised the <see langword="Scroll" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000FA2E3 File Offset: 0x000F84E3
		public ScrollOrientation ScrollOrientation
		{
			get
			{
				return this.scrollOrientation;
			}
		}

		/// <summary>Gets the type of scroll event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000FA2EB File Offset: 0x000F84EB
		public ScrollEventType Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets or sets the new <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
		/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property will be changed to.</returns>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000FA2F3 File Offset: 0x000F84F3
		// (set) Token: 0x06003852 RID: 14418 RVA: 0x000FA2FB File Offset: 0x000F84FB
		public int NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		/// <summary>Gets the old <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
		/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property contained before it changed.</returns>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000FA304 File Offset: 0x000F8504
		public int OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x0400218F RID: 8591
		private readonly ScrollEventType type;

		// Token: 0x04002190 RID: 8592
		private int newValue;

		// Token: 0x04002191 RID: 8593
		private ScrollOrientation scrollOrientation;

		// Token: 0x04002192 RID: 8594
		private int oldValue = -1;
	}
}

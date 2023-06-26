using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	// Token: 0x020002B1 RID: 689
	[ComVisible(true)]
	public class KeyEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyEventArgs" /> class.</summary>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> representing the key that was pressed, combined with any modifier flags that indicate which CTRL, SHIFT, and ALT keys were pressed at the same time. Possible values are obtained be applying the bitwise OR (|) operator to constants from the <see cref="T:System.Windows.Forms.Keys" /> enumeration.</param>
		// Token: 0x06002A77 RID: 10871 RVA: 0x000C003B File Offset: 0x000BE23B
		public KeyEventArgs(Keys keyData)
		{
			this.keyData = keyData;
		}

		/// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the ALT key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000C004A File Offset: 0x000BE24A
		public virtual bool Alt
		{
			get
			{
				return (this.keyData & Keys.Alt) == Keys.Alt;
			}
		}

		/// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the CTRL key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x000C005F File Offset: 0x000BE25F
		public bool Control
		{
			get
			{
				return (this.keyData & Keys.Control) == Keys.Control;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>
		///   <see langword="true" /> to bypass the control's default handling; otherwise, <see langword="false" /> to also pass the event along to the default control handler.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000C0074 File Offset: 0x000BE274
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x000C007C File Offset: 0x000BE27C
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		/// <summary>Gets the keyboard code for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> value that is the key code for the event.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000C0088 File Offset: 0x000BE288
		public Keys KeyCode
		{
			get
			{
				Keys keys = this.keyData & Keys.KeyCode;
				if (!Enum.IsDefined(typeof(Keys), (int)keys))
				{
					return Keys.None;
				}
				return keys;
			}
		}

		/// <summary>Gets the keyboard value for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>The integer representation of the <see cref="P:System.Windows.Forms.KeyEventArgs.KeyCode" /> property.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x000C00BC File Offset: 0x000BE2BC
		public int KeyValue
		{
			get
			{
				return (int)(this.keyData & Keys.KeyCode);
			}
		}

		/// <summary>Gets the key data for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> representing the key code for the key that was pressed, combined with modifier flags that indicate which combination of CTRL, SHIFT, and ALT keys was pressed at the same time.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000C00CA File Offset: 0x000BE2CA
		public Keys KeyData
		{
			get
			{
				return this.keyData;
			}
		}

		/// <summary>Gets the modifier flags for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event. The flags indicate which combination of CTRL, SHIFT, and ALT keys was pressed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> value representing one or more modifier flags.</returns>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000C00D2 File Offset: 0x000BE2D2
		public Keys Modifiers
		{
			get
			{
				return this.keyData & Keys.Modifiers;
			}
		}

		/// <summary>Gets a value indicating whether the SHIFT key was pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the SHIFT key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000C00E0 File Offset: 0x000BE2E0
		public virtual bool Shift
		{
			get
			{
				return (this.keyData & Keys.Shift) == Keys.Shift;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key event should be passed on to the underlying control.</summary>
		/// <returns>
		///   <see langword="true" /> if the key event should not be sent to the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x000C00F5 File Offset: 0x000BE2F5
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x000C00FD File Offset: 0x000BE2FD
		public bool SuppressKeyPress
		{
			get
			{
				return this.suppressKeyPress;
			}
			set
			{
				this.suppressKeyPress = value;
				this.handled = value;
			}
		}

		// Token: 0x0400112E RID: 4398
		private readonly Keys keyData;

		// Token: 0x0400112F RID: 4399
		private bool handled;

		// Token: 0x04001130 RID: 4400
		private bool suppressKeyPress;
	}
}

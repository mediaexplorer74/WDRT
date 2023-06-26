using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	// Token: 0x020002B3 RID: 691
	[ComVisible(true)]
	public class KeyPressEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> class.</summary>
		/// <param name="keyChar">The ASCII character corresponding to the key the user pressed.</param>
		// Token: 0x06002A87 RID: 10887 RVA: 0x000C010D File Offset: 0x000BE30D
		public KeyPressEventArgs(char keyChar)
		{
			this.keyChar = keyChar;
		}

		/// <summary>Gets or sets the character corresponding to the key pressed.</summary>
		/// <returns>The ASCII character that is composed. For example, if the user presses SHIFT + K, this property returns an uppercase K.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000C011C File Offset: 0x000BE31C
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x000C0124 File Offset: 0x000BE324
		public char KeyChar
		{
			get
			{
				return this.keyChar;
			}
			set
			{
				this.keyChar = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event was handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event is handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000C012D File Offset: 0x000BE32D
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x000C0135 File Offset: 0x000BE335
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

		// Token: 0x04001131 RID: 4401
		private char keyChar;

		// Token: 0x04001132 RID: 4402
		private bool handled;
	}
}

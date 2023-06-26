using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
	// Token: 0x02000323 RID: 803
	public class PreviewKeyDownEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> class with the specified key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x06003302 RID: 13058 RVA: 0x000E3936 File Offset: 0x000E1B36
		public PreviewKeyDownEventArgs(Keys keyData)
		{
			this._keyData = keyData;
		}

		/// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the ALT key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000E3945 File Offset: 0x000E1B45
		public bool Alt
		{
			get
			{
				return (this._keyData & Keys.Alt) == Keys.Alt;
			}
		}

		/// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the CTRL key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000E395A File Offset: 0x000E1B5A
		public bool Control
		{
			get
			{
				return (this._keyData & Keys.Control) == Keys.Control;
			}
		}

		/// <summary>Gets the keyboard code for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000E3970 File Offset: 0x000E1B70
		public Keys KeyCode
		{
			get
			{
				Keys keys = this._keyData & Keys.KeyCode;
				if (!Enum.IsDefined(typeof(Keys), (int)keys))
				{
					return Keys.None;
				}
				return keys;
			}
		}

		/// <summary>Gets the keyboard value for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the keyboard value.</returns>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000E39A4 File Offset: 0x000E1BA4
		public int KeyValue
		{
			get
			{
				return (int)(this._keyData & Keys.KeyCode);
			}
		}

		/// <summary>Gets the key code combined with key modifiers such as the SHIFT, CONTROL, and ALT keys for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000E39B2 File Offset: 0x000E1BB2
		public Keys KeyData
		{
			get
			{
				return this._keyData;
			}
		}

		/// <summary>Gets the modifier flags for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x000E39BA File Offset: 0x000E1BBA
		public Keys Modifiers
		{
			get
			{
				return this._keyData & Keys.Modifiers;
			}
		}

		/// <summary>Gets a value indicating whether the SHIFT key was pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the SHIFT key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000E39C8 File Offset: 0x000E1BC8
		public bool Shift
		{
			get
			{
				return (this._keyData & Keys.Shift) == Keys.Shift;
			}
		}

		/// <summary>Gets or sets a value indicating whether a key is a regular input key.</summary>
		/// <returns>
		///   <see langword="true" /> if the key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000E39DD File Offset: 0x000E1BDD
		// (set) Token: 0x0600330B RID: 13067 RVA: 0x000E39E5 File Offset: 0x000E1BE5
		public bool IsInputKey
		{
			get
			{
				return this._isInputKey;
			}
			set
			{
				this._isInputKey = value;
			}
		}

		// Token: 0x04001EB2 RID: 7858
		private readonly Keys _keyData;

		// Token: 0x04001EB3 RID: 7859
		private bool _isInputKey;
	}
}

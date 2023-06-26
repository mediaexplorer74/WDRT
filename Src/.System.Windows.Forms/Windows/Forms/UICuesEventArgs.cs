using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
	// Token: 0x02000426 RID: 1062
	public class UICuesEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UICuesEventArgs" /> class with the specified <see cref="T:System.Windows.Forms.UICues" />.</summary>
		/// <param name="uicues">A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values.</param>
		// Token: 0x060049E1 RID: 18913 RVA: 0x00137201 File Offset: 0x00135401
		public UICuesEventArgs(UICues uicues)
		{
			this.uicues = uicues;
		}

		/// <summary>Gets a value indicating whether focus rectangles are shown after the change.</summary>
		/// <returns>
		///   <see langword="true" /> if focus rectangles are shown after the change; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00137210 File Offset: 0x00135410
		public bool ShowFocus
		{
			get
			{
				return (this.uicues & UICues.ShowFocus) > UICues.None;
			}
		}

		/// <summary>Gets a value indicating whether keyboard cues are underlined after the change.</summary>
		/// <returns>
		///   <see langword="true" /> if keyboard cues are underlined after the change; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x0013721D File Offset: 0x0013541D
		public bool ShowKeyboard
		{
			get
			{
				return (this.uicues & UICues.ShowKeyboard) > UICues.None;
			}
		}

		/// <summary>Gets a value indicating whether the state of the focus cues has changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the state of the focus cues has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x0013722A File Offset: 0x0013542A
		public bool ChangeFocus
		{
			get
			{
				return (this.uicues & UICues.ChangeFocus) > UICues.None;
			}
		}

		/// <summary>Gets a value indicating whether the state of the keyboard cues has changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the state of the keyboard cues has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x00137237 File Offset: 0x00135437
		public bool ChangeKeyboard
		{
			get
			{
				return (this.uicues & UICues.ChangeKeyboard) > UICues.None;
			}
		}

		/// <summary>Gets the bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values. The default is <see cref="F:System.Windows.Forms.UICues.Changed" />.</returns>
		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x00137244 File Offset: 0x00135444
		public UICues Changed
		{
			get
			{
				return this.uicues & UICues.Changed;
			}
		}

		// Token: 0x040027B8 RID: 10168
		private readonly UICues uicues;
	}
}

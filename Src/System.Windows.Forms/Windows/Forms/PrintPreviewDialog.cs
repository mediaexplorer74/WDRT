using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a dialog box form that contains a <see cref="T:System.Windows.Forms.PrintPreviewControl" /> for printing from a Windows Forms application.</summary>
	// Token: 0x0200044B RID: 1099
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true)]
	[DefaultProperty("Document")]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(true)]
	[SRDescription("DescriptionPrintPreviewDialog")]
	public partial class PrintPreviewDialog : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> class.</summary>
		// Token: 0x06004C7A RID: 19578 RVA: 0x0013DE88 File Offset: 0x0013C088
		public PrintPreviewDialog()
		{
			base.AutoScaleBaseSize = new Size(5, 13);
			this.previewControl = new PrintPreviewControl();
			this.imageList = new ImageList();
			Bitmap bitmap = new Bitmap(typeof(PrintPreviewDialog), "PrintPreviewStrip.bmp");
			bitmap.MakeTransparent();
			this.imageList.Images.AddStrip(bitmap);
			this.InitForm();
		}

		/// <summary>Gets or sets the button on the form that is clicked when the user presses the ENTER key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the button to use as the accept button for the form.</returns>
		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x0013DEF2 File Offset: 0x0013C0F2
		// (set) Token: 0x06004C7C RID: 19580 RVA: 0x0013DEFA File Offset: 0x0013C0FA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl AcceptButton
		{
			get
			{
				return base.AcceptButton;
			}
			set
			{
				base.AcceptButton = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.</summary>
		/// <returns>
		///   <see langword="true" /> if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x0013DF03 File Offset: 0x0013C103
		// (set) Token: 0x06004C7E RID: 19582 RVA: 0x0013DF0B File Offset: 0x0013C10B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool AutoScale
		{
			get
			{
				return base.AutoScale;
			}
			set
			{
				base.AutoScale = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form enables autoscrolling.</summary>
		/// <returns>Represents a Boolean value.</returns>
		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x0013DF14 File Offset: 0x0013C114
		// (set) Token: 0x06004C80 RID: 19584 RVA: 0x0013DF1C File Offset: 0x0013C11C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should automatically resize to fit its contents.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should resize to fit its contents; otherwise, <see langword="false" />.</returns>
		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x00108FA6 File Offset: 0x001071A6
		// (set) Token: 0x06004C82 RID: 19586 RVA: 0x00108FAE File Offset: 0x001071AE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.AutoSize" /> property changes.</summary>
		// Token: 0x140003F0 RID: 1008
		// (add) Token: 0x06004C83 RID: 19587 RVA: 0x00108FB7 File Offset: 0x001071B7
		// (remove) Token: 0x06004C84 RID: 19588 RVA: 0x00108FC0 File Offset: 0x001071C0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Gets or sets how the control performs validation when the user changes focus to another control.</summary>
		/// <returns>Determines how a control validates its data when it loses user input focus.</returns>
		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x0013DF25 File Offset: 0x0013C125
		// (set) Token: 0x06004C86 RID: 19590 RVA: 0x0013DF2D File Offset: 0x0013C12D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.AutoValidate" /> property changes.</summary>
		// Token: 0x140003F1 RID: 1009
		// (add) Token: 0x06004C87 RID: 19591 RVA: 0x0013DF36 File Offset: 0x0013C136
		// (remove) Token: 0x06004C88 RID: 19592 RVA: 0x0013DF3F File Offset: 0x0013C13F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>Gets or sets the background color of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x0013DF48 File Offset: 0x0013C148
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x0013DF50 File Offset: 0x0013C150
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackColor" /> property changes.</summary>
		// Token: 0x140003F2 RID: 1010
		// (add) Token: 0x06004C8B RID: 19595 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x06004C8C RID: 19596 RVA: 0x00058BFB File Offset: 0x00056DFB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>Gets or sets the cancel button for the <see cref="T:System.Windows.Forms.PrintPreviewDialog" />.</summary>
		/// <returns>Allows a control to act like a button on a form.</returns>
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x0013DF59 File Offset: 0x0013C159
		// (set) Token: 0x06004C8E RID: 19598 RVA: 0x0013DF61 File Offset: 0x0013C161
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl CancelButton
		{
			get
			{
				return base.CancelButton;
			}
			set
			{
				base.CancelButton = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> if the form displays a control box in the upper-left corner of the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x0013DF6A File Offset: 0x0013C16A
		// (set) Token: 0x06004C90 RID: 19600 RVA: 0x0013DF72 File Offset: 0x0013C172
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool ControlBox
		{
			get
			{
				return base.ControlBox;
			}
			set
			{
				base.ControlBox = value;
			}
		}

		/// <summary>Gets or sets how the short cut menu for the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x00011936 File Offset: 0x0000FB36
		// (set) Token: 0x06004C92 RID: 19602 RVA: 0x00112AB6 File Offset: 0x00110CB6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ContextMenuStrip" /> property changes.</summary>
		// Token: 0x140003F3 RID: 1011
		// (add) Token: 0x06004C93 RID: 19603 RVA: 0x00112ABF File Offset: 0x00110CBF
		// (remove) Token: 0x06004C94 RID: 19604 RVA: 0x00112AC8 File Offset: 0x00110CC8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		/// <summary>Gets or sets the border style of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormBorderStyle" /> that represents the style of border to display for the form. The default is <see cref="F:System.Windows.Forms.FormBorderStyle.Sizable" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06004C95 RID: 19605 RVA: 0x0013DF7B File Offset: 0x0013C17B
		// (set) Token: 0x06004C96 RID: 19606 RVA: 0x0013DF83 File Offset: 0x0013C183
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FormBorderStyle FormBorderStyle
		{
			get
			{
				return base.FormBorderStyle;
			}
			set
			{
				base.FormBorderStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.</summary>
		/// <returns>
		///   <see langword="true" /> to display a Help button in the form's caption bar; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06004C97 RID: 19607 RVA: 0x0013DF8C File Offset: 0x0013C18C
		// (set) Token: 0x06004C98 RID: 19608 RVA: 0x0013DF94 File Offset: 0x0013C194
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool HelpButton
		{
			get
			{
				return base.HelpButton;
			}
			set
			{
				base.HelpButton = value;
			}
		}

		/// <summary>Gets or sets the icon for the form.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon for the form.</returns>
		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x0013DF9D File Offset: 0x0013C19D
		// (set) Token: 0x06004C9A RID: 19610 RVA: 0x0013DFA5 File Offset: 0x0013C1A5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Icon Icon
		{
			get
			{
				return base.Icon;
			}
			set
			{
				base.Icon = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is a container for multiple document interface (MDI) child forms.</summary>
		/// <returns>
		///   <see langword="true" /> if the form is a container for MDI child forms; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0013DFAE File Offset: 0x0013C1AE
		// (set) Token: 0x06004C9C RID: 19612 RVA: 0x0013DFB6 File Offset: 0x0013C1B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool IsMdiContainer
		{
			get
			{
				return base.IsMdiContainer;
			}
			set
			{
				base.IsMdiContainer = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the form will receive all key events; <see langword="false" /> if the currently selected control on the form receives key events. The default is <see langword="false" />.</returns>
		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x0013DFBF File Offset: 0x0013C1BF
		// (set) Token: 0x06004C9E RID: 19614 RVA: 0x0013DFC7 File Offset: 0x0013C1C7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool KeyPreview
		{
			get
			{
				return base.KeyPreview;
			}
			set
			{
				base.KeyPreview = value;
			}
		}

		/// <summary>Gets or sets the maximum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the maximum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> are less than 0.</exception>
		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x0013DFD0 File Offset: 0x0013C1D0
		// (set) Token: 0x06004CA0 RID: 19616 RVA: 0x0013DFD8 File Offset: 0x0013C1D8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.MaximumSize" /> property changes.</summary>
		// Token: 0x140003F4 RID: 1012
		// (add) Token: 0x06004CA1 RID: 19617 RVA: 0x0013DFE1 File Offset: 0x0013C1E1
		// (remove) Token: 0x06004CA2 RID: 19618 RVA: 0x0013DFEA File Offset: 0x0013C1EA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MaximumSizeChanged
		{
			add
			{
				base.MaximumSizeChanged += value;
			}
			remove
			{
				base.MaximumSizeChanged -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the maximize button is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> to display a maximize button for the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x0013DFF3 File Offset: 0x0013C1F3
		// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x0013DFFB File Offset: 0x0013C1FB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MaximizeBox
		{
			get
			{
				return base.MaximizeBox;
			}
			set
			{
				base.MaximizeBox = value;
			}
		}

		/// <summary>Gets or sets the margins for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x0013E004 File Offset: 0x0013C204
		// (set) Token: 0x06004CA6 RID: 19622 RVA: 0x0013E00C File Offset: 0x0013C20C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Margin" /> property changes.</summary>
		// Token: 0x140003F5 RID: 1013
		// (add) Token: 0x06004CA7 RID: 19623 RVA: 0x0013E015 File Offset: 0x0013C215
		// (remove) Token: 0x06004CA8 RID: 19624 RVA: 0x0013E01E File Offset: 0x0013C21E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.MainMenu" /> that is displayed in the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the menu to display in the form.</returns>
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06004CA9 RID: 19625 RVA: 0x0013E027 File Offset: 0x0013C227
		// (set) Token: 0x06004CAA RID: 19626 RVA: 0x0013E02F File Offset: 0x0013C22F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new MainMenu Menu
		{
			get
			{
				return base.Menu;
			}
			set
			{
				base.Menu = value;
			}
		}

		/// <summary>Gets the minimum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> are less than 0.</exception>
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06004CAB RID: 19627 RVA: 0x0013E038 File Offset: 0x0013C238
		// (set) Token: 0x06004CAC RID: 19628 RVA: 0x0013E040 File Offset: 0x0013C240
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.MinimumSize" /> property changes.</summary>
		// Token: 0x140003F6 RID: 1014
		// (add) Token: 0x06004CAD RID: 19629 RVA: 0x0013E049 File Offset: 0x0013C249
		// (remove) Token: 0x06004CAE RID: 19630 RVA: 0x0013E052 File Offset: 0x0013C252
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MinimumSizeChanged
		{
			add
			{
				base.MinimumSizeChanged += value;
			}
			remove
			{
				base.MinimumSizeChanged -= value;
			}
		}

		/// <summary>Gets or sets the padding for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004CAF RID: 19631 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06004CB0 RID: 19632 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Padding" /> property changes.</summary>
		// Token: 0x140003F7 RID: 1015
		// (add) Token: 0x06004CB1 RID: 19633 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06004CB2 RID: 19634 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Gets or sets the size of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form.</returns>
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x0013E05B File Offset: 0x0013C25B
		// (set) Token: 0x06004CB4 RID: 19636 RVA: 0x0013E063 File Offset: 0x0013C263
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Size" /> property changes.</summary>
		// Token: 0x140003F8 RID: 1016
		// (add) Token: 0x06004CB5 RID: 19637 RVA: 0x0013E06C File Offset: 0x0013C26C
		// (remove) Token: 0x06004CB6 RID: 19638 RVA: 0x0013E075 File Offset: 0x0013C275
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler SizeChanged
		{
			add
			{
				base.SizeChanged += value;
			}
			remove
			{
				base.SizeChanged -= value;
			}
		}

		/// <summary>Gets or sets the starting position of the dialog box at run time.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormStartPosition" /> that represents the starting position of the dialog box.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004CB7 RID: 19639 RVA: 0x0013E07E File Offset: 0x0013C27E
		// (set) Token: 0x06004CB8 RID: 19640 RVA: 0x0013E086 File Offset: 0x0013C286
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FormStartPosition StartPosition
		{
			get
			{
				return base.StartPosition;
			}
			set
			{
				base.StartPosition = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form should be displayed as the topmost form of your application.</summary>
		/// <returns>
		///   <see langword="true" /> to display the form as a topmost form; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x0013E08F File Offset: 0x0013C28F
		// (set) Token: 0x06004CBA RID: 19642 RVA: 0x0013E097 File Offset: 0x0013C297
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TopMost
		{
			get
			{
				return base.TopMost;
			}
			set
			{
				base.TopMost = value;
			}
		}

		/// <summary>Gets or sets the color that will represent transparent areas of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display transparently on the form.</returns>
		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x0013E0A0 File Offset: 0x0013C2A0
		// (set) Token: 0x06004CBC RID: 19644 RVA: 0x0013E0A8 File Offset: 0x0013C2A8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Color TransparencyKey
		{
			get
			{
				return base.TransparencyKey;
			}
			set
			{
				base.TransparencyKey = value;
			}
		}

		/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
		/// <returns>
		///   <see langword="true" /> to use the wait cursor for the current control and all child controls; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004CBD RID: 19645 RVA: 0x00139B97 File Offset: 0x00137D97
		// (set) Token: 0x06004CBE RID: 19646 RVA: 0x0013E0B1 File Offset: 0x0013C2B1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				base.UseWaitCursor = value;
			}
		}

		/// <summary>Gets or sets the form's window state.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormWindowState" /> that represents the window state of the form. The default is <see cref="F:System.Windows.Forms.FormWindowState.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06004CBF RID: 19647 RVA: 0x0013E0BA File Offset: 0x0013C2BA
		// (set) Token: 0x06004CC0 RID: 19648 RVA: 0x0013E0C2 File Offset: 0x0013C2C2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FormWindowState WindowState
		{
			get
			{
				return base.WindowState;
			}
			set
			{
				base.WindowState = value;
			}
		}

		/// <summary>The accessible role of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleRole.Default" />.</returns>
		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x0013E0CB File Offset: 0x0013C2CB
		// (set) Token: 0x06004CC2 RID: 19650 RVA: 0x0013E0D3 File Offset: 0x0013C2D3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new AccessibleRole AccessibleRole
		{
			get
			{
				return base.AccessibleRole;
			}
			set
			{
				base.AccessibleRole = value;
			}
		}

		/// <summary>Gets or sets the accessible description of the control.</summary>
		/// <returns>The accessible description of the control. The default is <see langword="null" />.</returns>
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x0013E0DC File Offset: 0x0013C2DC
		// (set) Token: 0x06004CC4 RID: 19652 RVA: 0x0013E0E4 File Offset: 0x0013C2E4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleDescription
		{
			get
			{
				return base.AccessibleDescription;
			}
			set
			{
				base.AccessibleDescription = value;
			}
		}

		/// <summary>Gets or sets the accessible name of the control.</summary>
		/// <returns>The accessible name of the control. The default is <see langword="null" />.</returns>
		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x0013E0ED File Offset: 0x0013C2ED
		// (set) Token: 0x06004CC6 RID: 19654 RVA: 0x0013E0F5 File Offset: 0x0013C2F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleName
		{
			get
			{
				return base.AccessibleName;
			}
			set
			{
				base.AccessibleName = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether entering the control causes validation for all controls that require validation.</summary>
		/// <returns>
		///   <see langword="true" /> if entering the control causes validation to be performed on controls requiring validation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x000E28D7 File Offset: 0x000E0AD7
		// (set) Token: 0x06004CC8 RID: 19656 RVA: 0x000E28DF File Offset: 0x000E0ADF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.CausesValidation" /> property changes.</summary>
		// Token: 0x140003F9 RID: 1017
		// (add) Token: 0x06004CC9 RID: 19657 RVA: 0x000E28E8 File Offset: 0x000E0AE8
		// (remove) Token: 0x06004CCA RID: 19658 RVA: 0x000E28F1 File Offset: 0x000E0AF1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>Gets the data bindings for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects for the control.</returns>
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x0013E0FE File Offset: 0x0013C2FE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ControlBindingsCollection DataBindings
		{
			get
			{
				return base.DataBindings;
			}
		}

		/// <summary>Gets the default minimum size, in pixels, of the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure representing the default minimum size.</returns>
		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x0013E106 File Offset: 0x0013C306
		protected override Size DefaultMinimumSize
		{
			get
			{
				return new Size(375, 250);
			}
		}

		/// <summary>Get or sets a value indicating whether the control is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x0001A0C5 File Offset: 0x000182C5
		// (set) Token: 0x06004CCE RID: 19662 RVA: 0x0001A0CD File Offset: 0x000182CD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Enabled" /> property changes.</summary>
		// Token: 0x140003FA RID: 1018
		// (add) Token: 0x06004CCF RID: 19663 RVA: 0x0010711A File Offset: 0x0010531A
		// (remove) Token: 0x06004CD0 RID: 19664 RVA: 0x00107123 File Offset: 0x00105323
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0013E117 File Offset: 0x0013C317
		// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x0013E11F File Offset: 0x0013C31F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Location" /> property changes.</summary>
		// Token: 0x140003FB RID: 1019
		// (add) Token: 0x06004CD3 RID: 19667 RVA: 0x000FFD62 File Offset: 0x000FDF62
		// (remove) Token: 0x06004CD4 RID: 19668 RVA: 0x000FFD6B File Offset: 0x000FDF6B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>Gets or sets the object that contains data about the control.</summary>
		/// <returns>An object that contains data about the control. The default is <see langword="null" />.</returns>
		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x0013E128 File Offset: 0x0013C328
		// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x0013E130 File Offset: 0x0013C330
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new object Tag
		{
			get
			{
				return base.Tag;
			}
			set
			{
				base.Tag = value;
			}
		}

		/// <summary>Gets or sets whether the control can accept data that the user drags onto it.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop operations are allowed in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x000B8E45 File Offset: 0x000B7045
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>Gets or sets the cursor for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the control.</returns>
		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x0001A0A0 File Offset: 0x000182A0
		// (set) Token: 0x06004CDA RID: 19674 RVA: 0x0001A0A8 File Offset: 0x000182A8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Cursor" /> property changes.</summary>
		// Token: 0x140003FC RID: 1020
		// (add) Token: 0x06004CDB RID: 19675 RVA: 0x0004620F File Offset: 0x0004440F
		// (remove) Token: 0x06004CDC RID: 19676 RVA: 0x00046218 File Offset: 0x00044418
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>Gets or sets the background image for the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06004CDE RID: 19678 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImage" /> property changes.</summary>
		// Token: 0x140003FD RID: 1021
		// (add) Token: 0x06004CDF RID: 19679 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06004CE0 RID: 19680 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Gets or sets the layout of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImage" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06004CE2 RID: 19682 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140003FE RID: 1022
		// (add) Token: 0x06004CE3 RID: 19683 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06004CE4 RID: 19684 RVA: 0x000118B9 File Offset: 0x0000FAB9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values.</exception>
		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06004CE6 RID: 19686 RVA: 0x0001A059 File Offset: 0x00018259
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ImeMode" /> property changes.</summary>
		// Token: 0x140003FF RID: 1023
		// (add) Token: 0x06004CE7 RID: 19687 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x06004CE8 RID: 19688 RVA: 0x00023F79 File Offset: 0x00022179
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width, in pixels, of the auto-scroll margin.</returns>
		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x0001180F File Offset: 0x0000FA0F
		// (set) Token: 0x06004CEA RID: 19690 RVA: 0x00011817 File Offset: 0x0000FA17
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>Gets or sets the minimum size of the automatic scroll bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width, in pixels, of the scroll bars.</returns>
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x06004CEC RID: 19692 RVA: 0x00011828 File Offset: 0x0000FA28
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>Gets or sets the anchor style for the control.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values. The default is <see langword="Top" /> and <see langword="Left" />.</returns>
		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x000FFC2C File Offset: 0x000FDE2C
		// (set) Token: 0x06004CEE RID: 19694 RVA: 0x000FFC34 File Offset: 0x000FDE34
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visible.</summary>
		/// <returns>This property is not relevant for this class.  
		///  <see langword="true" /> if the control is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x000FFCF9 File Offset: 0x000FDEF9
		// (set) Token: 0x06004CF0 RID: 19696 RVA: 0x000FFD01 File Offset: 0x000FDF01
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Visible" /> property changes.</summary>
		// Token: 0x14000400 RID: 1024
		// (add) Token: 0x06004CF1 RID: 19697 RVA: 0x000FFD3E File Offset: 0x000FDF3E
		// (remove) Token: 0x06004CF2 RID: 19698 RVA: 0x000FFD47 File Offset: 0x000FDF47
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x06004CF4 RID: 19700 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ForeColor" /> property changes.</summary>
		// Token: 0x14000401 RID: 1025
		// (add) Token: 0x06004CF5 RID: 19701 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x06004CF6 RID: 19702 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x000E322B File Offset: 0x000E142B
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x000C5F21 File Offset: 0x000C4121
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should be laid out from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> contents should be laid out from right to left; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x0013E139 File Offset: 0x0013C339
		// (set) Token: 0x06004CFA RID: 19706 RVA: 0x0013E141 File Offset: 0x0013C341
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool RightToLeftLayout
		{
			get
			{
				return base.RightToLeftLayout;
			}
			set
			{
				base.RightToLeftLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.RightToLeft" /> property changes.</summary>
		// Token: 0x14000402 RID: 1026
		// (add) Token: 0x06004CFB RID: 19707 RVA: 0x000E3233 File Offset: 0x000E1433
		// (remove) Token: 0x06004CFC RID: 19708 RVA: 0x000E323C File Offset: 0x000E143C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000403 RID: 1027
		// (add) Token: 0x06004CFD RID: 19709 RVA: 0x0013E14A File Offset: 0x0013C34A
		// (remove) Token: 0x06004CFE RID: 19710 RVA: 0x0013E153 File Offset: 0x0013C353
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.RightToLeftLayoutChanged += value;
			}
			remove
			{
				base.RightToLeftLayoutChanged -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to this control using the TAB key; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x0013E15C File Offset: 0x0013C35C
		// (set) Token: 0x06004D00 RID: 19712 RVA: 0x0013E164 File Offset: 0x0013C364
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.TabStop" /> property changes.</summary>
		// Token: 0x14000404 RID: 1028
		// (add) Token: 0x06004D01 RID: 19713 RVA: 0x0013E16D File Offset: 0x0013C36D
		// (remove) Token: 0x06004D02 RID: 19714 RVA: 0x0013E176 File Offset: 0x0013C376
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>Gets or sets the text displayed on the control.</summary>
		/// <returns>Represents text as a series of Unicode characters.</returns>
		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x0013E17F File Offset: 0x0013C37F
		// (set) Token: 0x06004D04 RID: 19716 RVA: 0x0013E187 File Offset: 0x0013C387
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Text" /> property changes.</summary>
		// Token: 0x14000405 RID: 1029
		// (add) Token: 0x06004D05 RID: 19717 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06004D06 RID: 19718 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Gets or sets how the control should be docked in its parent control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x06004D08 RID: 19720 RVA: 0x000FFC4E File Offset: 0x000FDE4E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Dock" /> property changes.</summary>
		// Token: 0x14000406 RID: 1030
		// (add) Token: 0x06004D09 RID: 19721 RVA: 0x000FFD50 File Offset: 0x000FDF50
		// (remove) Token: 0x06004D0A RID: 19722 RVA: 0x000FFD59 File Offset: 0x000FDF59
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>Gets or sets the font used for the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06004D0C RID: 19724 RVA: 0x0001A0DE File Offset: 0x000182DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Font" /> property changes.</summary>
		// Token: 0x14000407 RID: 1031
		// (add) Token: 0x06004D0D RID: 19725 RVA: 0x0005A900 File Offset: 0x00058B00
		// (remove) Token: 0x06004D0E RID: 19726 RVA: 0x0005A909 File Offset: 0x00058B09
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> that represents the shortcut menu associated with the control.</returns>
		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x00011919 File Offset: 0x0000FB19
		// (set) Token: 0x06004D10 RID: 19728 RVA: 0x0001A0B1 File Offset: 0x000182B1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ContextMenu" /> property changes.</summary>
		// Token: 0x14000408 RID: 1032
		// (add) Token: 0x06004D11 RID: 19729 RVA: 0x00112AA4 File Offset: 0x00110CA4
		// (remove) Token: 0x06004D12 RID: 19730 RVA: 0x00112AAD File Offset: 0x00110CAD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				base.ContextMenuChanged += value;
			}
			remove
			{
				base.ContextMenuChanged -= value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.ScrollableControl.DockPadding" /> property.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x000119C6 File Offset: 0x0000FBC6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		/// <summary>Gets or sets a value indicating whether printing uses the anti-aliasing features of the operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if anti-aliasing is used; otherwise, <see langword="false" />.</returns>
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004D14 RID: 19732 RVA: 0x0013E190 File Offset: 0x0013C390
		// (set) Token: 0x06004D15 RID: 19733 RVA: 0x0013E19D File Offset: 0x0013C39D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PrintPreviewAntiAliasDescr")]
		public bool UseAntiAlias
		{
			get
			{
				return this.PrintPreviewControl.UseAntiAlias;
			}
			set
			{
				this.PrintPreviewControl.UseAntiAlias = value;
			}
		}

		/// <summary>The <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> class does not support the <see cref="P:System.Windows.Forms.PrintPreviewDialog.AutoScaleBaseSize" /> property.</summary>
		/// <returns>Stores an ordered pair of integers, typically the width and height of a rectangle.</returns>
		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004D16 RID: 19734 RVA: 0x0013E1AB File Offset: 0x0013C3AB
		// (set) Token: 0x06004D17 RID: 19735 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This property has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override Size AutoScaleBaseSize
		{
			get
			{
				return base.AutoScaleBaseSize;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the document to preview.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> representing the document to preview.</returns>
		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x0013E1B3 File Offset: 0x0013C3B3
		// (set) Token: 0x06004D19 RID: 19737 RVA: 0x0013E1C0 File Offset: 0x0013C3C0
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("PrintPreviewDocumentDescr")]
		public PrintDocument Document
		{
			get
			{
				return this.previewControl.Document;
			}
			set
			{
				this.previewControl.Document = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the minimize button is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> to display a minimize button for the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004D1A RID: 19738 RVA: 0x0013E1CE File Offset: 0x0013C3CE
		// (set) Token: 0x06004D1B RID: 19739 RVA: 0x0013E1D6 File Offset: 0x0013C3D6
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MinimizeBox
		{
			get
			{
				return base.MinimizeBox;
			}
			set
			{
				base.MinimizeBox = value;
			}
		}

		/// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> contained in this form.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.PrintPreviewControl" /> contained in this form.</returns>
		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x0013E1DF File Offset: 0x0013C3DF
		[SRCategory("CatBehavior")]
		[SRDescription("PrintPreviewPrintPreviewControlDescr")]
		[Browsable(false)]
		public PrintPreviewControl PrintPreviewControl
		{
			get
			{
				return this.previewControl;
			}
		}

		/// <summary>Gets or sets the opacity level of the form.</summary>
		/// <returns>The level of opacity for the control.</returns>
		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x0013E1E7 File Offset: 0x0013C3E7
		// (set) Token: 0x06004D1E RID: 19742 RVA: 0x0013E1EF File Offset: 0x0013C3EF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new double Opacity
		{
			get
			{
				return base.Opacity;
			}
			set
			{
				base.Opacity = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is displayed in the Windows taskbar.</summary>
		/// <returns>
		///   <see langword="true" /> to display the form in the Windows taskbar at run time; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x0013E1F8 File Offset: 0x0013C3F8
		// (set) Token: 0x06004D20 RID: 19744 RVA: 0x0013E200 File Offset: 0x0013C400
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool ShowInTaskbar
		{
			get
			{
				return base.ShowInTaskbar;
			}
			set
			{
				base.ShowInTaskbar = value;
			}
		}

		/// <summary>Gets or sets the style of the size grip to display in the lower-right corner of the form.</summary>
		/// <returns>Gets or sets the style of the size grip to display in the lower-right corner of the form.</returns>
		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x0013E209 File Offset: 0x0013C409
		// (set) Token: 0x06004D22 RID: 19746 RVA: 0x0013E211 File Offset: 0x0013C411
		[Browsable(false)]
		[DefaultValue(SizeGripStyle.Hide)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new SizeGripStyle SizeGripStyle
		{
			get
			{
				return base.SizeGripStyle;
			}
			set
			{
				base.SizeGripStyle = value;
			}
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0013E21C File Offset: 0x0013C41C
		private void InitForm()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PrintPreviewDialog));
			this.toolStrip1 = new ToolStrip();
			this.printToolStripButton = new ToolStripButton();
			this.zoomToolStripSplitButton = new ToolStripSplitButton();
			this.autoToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripMenuItem();
			this.toolStripMenuItem6 = new ToolStripMenuItem();
			this.toolStripMenuItem7 = new ToolStripMenuItem();
			this.toolStripMenuItem8 = new ToolStripMenuItem();
			this.separatorToolStripSeparator = new ToolStripSeparator();
			this.onepageToolStripButton = new ToolStripButton();
			this.twopagesToolStripButton = new ToolStripButton();
			this.threepagesToolStripButton = new ToolStripButton();
			this.fourpagesToolStripButton = new ToolStripButton();
			this.sixpagesToolStripButton = new ToolStripButton();
			this.separatorToolStripSeparator1 = new ToolStripSeparator();
			this.closeToolStripButton = new ToolStripButton();
			this.pageCounter = new NumericUpDown();
			this.pageToolStripLabel = new ToolStripLabel();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.pageCounter).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.printToolStripButton, this.zoomToolStripSplitButton, this.separatorToolStripSeparator, this.onepageToolStripButton, this.twopagesToolStripButton, this.threepagesToolStripButton, this.fourpagesToolStripButton, this.sixpagesToolStripButton, this.separatorToolStripSeparator1, this.closeToolStripButton });
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = ToolStripRenderMode.System;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.printToolStripButton.Name = "printToolStripButton";
			componentResourceManager.ApplyResources(this.printToolStripButton, "printToolStripButton");
			if (AccessibilityImprovements.Level5)
			{
				this.printToolStripButton.AccessibleName = componentResourceManager.GetString("printToolStripButton.AccessibleNameLevel5");
			}
			this.zoomToolStripSplitButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.zoomToolStripSplitButton.DoubleClickEnabled = true;
			this.zoomToolStripSplitButton.DropDownItems.AddRange(new ToolStripItem[] { this.autoToolStripMenuItem, this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4, this.toolStripMenuItem5, this.toolStripMenuItem6, this.toolStripMenuItem7, this.toolStripMenuItem8 });
			this.zoomToolStripSplitButton.Name = "zoomToolStripSplitButton";
			this.zoomToolStripSplitButton.SplitterWidth = 1;
			componentResourceManager.ApplyResources(this.zoomToolStripSplitButton, "zoomToolStripSplitButton");
			this.autoToolStripMenuItem.CheckOnClick = true;
			this.autoToolStripMenuItem.DoubleClickEnabled = true;
			this.autoToolStripMenuItem.Checked = true;
			this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
			componentResourceManager.ApplyResources(this.autoToolStripMenuItem, "autoToolStripMenuItem");
			this.toolStripMenuItem1.CheckOnClick = true;
			this.toolStripMenuItem1.DoubleClickEnabled = true;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			componentResourceManager.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem2.CheckOnClick = true;
			this.toolStripMenuItem2.DoubleClickEnabled = true;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			componentResourceManager.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			this.toolStripMenuItem3.CheckOnClick = true;
			this.toolStripMenuItem3.DoubleClickEnabled = true;
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			componentResourceManager.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			this.toolStripMenuItem4.CheckOnClick = true;
			this.toolStripMenuItem4.DoubleClickEnabled = true;
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			componentResourceManager.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			this.toolStripMenuItem5.CheckOnClick = true;
			this.toolStripMenuItem5.DoubleClickEnabled = true;
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			componentResourceManager.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			this.toolStripMenuItem6.CheckOnClick = true;
			this.toolStripMenuItem6.DoubleClickEnabled = true;
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			componentResourceManager.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			this.toolStripMenuItem7.CheckOnClick = true;
			this.toolStripMenuItem7.DoubleClickEnabled = true;
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			componentResourceManager.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			this.toolStripMenuItem8.CheckOnClick = true;
			this.toolStripMenuItem8.DoubleClickEnabled = true;
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			componentResourceManager.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
			this.separatorToolStripSeparator.Name = "separatorToolStripSeparator";
			this.onepageToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.onepageToolStripButton.Name = "onepageToolStripButton";
			componentResourceManager.ApplyResources(this.onepageToolStripButton, "onepageToolStripButton");
			this.twopagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.twopagesToolStripButton.Name = "twopagesToolStripButton";
			componentResourceManager.ApplyResources(this.twopagesToolStripButton, "twopagesToolStripButton");
			this.threepagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.threepagesToolStripButton.Name = "threepagesToolStripButton";
			componentResourceManager.ApplyResources(this.threepagesToolStripButton, "threepagesToolStripButton");
			this.fourpagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.fourpagesToolStripButton.Name = "fourpagesToolStripButton";
			componentResourceManager.ApplyResources(this.fourpagesToolStripButton, "fourpagesToolStripButton");
			this.sixpagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.sixpagesToolStripButton.Name = "sixpagesToolStripButton";
			componentResourceManager.ApplyResources(this.sixpagesToolStripButton, "sixpagesToolStripButton");
			this.separatorToolStripSeparator1.Name = "separatorToolStripSeparator1";
			this.closeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.closeToolStripButton.Name = "closeToolStripButton";
			componentResourceManager.ApplyResources(this.closeToolStripButton, "closeToolStripButton");
			componentResourceManager.ApplyResources(this.pageCounter, "pageCounter");
			this.pageCounter.Text = "1";
			this.pageCounter.TextAlign = HorizontalAlignment.Right;
			this.pageCounter.DecimalPlaces = 0;
			this.pageCounter.Minimum = new decimal(0.0);
			this.pageCounter.Maximum = new decimal(1000.0);
			this.pageCounter.ValueChanged += this.UpdownMove;
			this.pageCounter.Name = "pageCounter";
			this.pageToolStripLabel.Alignment = ToolStripItemAlignment.Right;
			this.pageToolStripLabel.Name = "pageToolStripLabel";
			componentResourceManager.ApplyResources(this.pageToolStripLabel, "pageToolStripLabel");
			this.previewControl.Size = new Size(792, 610);
			this.previewControl.Location = new Point(0, 43);
			this.previewControl.Dock = DockStyle.Fill;
			this.previewControl.StartPageChanged += this.previewControl_StartPageChanged;
			this.printToolStripButton.Click += this.OnprintToolStripButtonClick;
			this.autoToolStripMenuItem.Click += this.ZoomAuto;
			this.toolStripMenuItem1.Click += this.Zoom500;
			this.toolStripMenuItem2.Click += this.Zoom250;
			this.toolStripMenuItem3.Click += this.Zoom150;
			this.toolStripMenuItem4.Click += this.Zoom100;
			this.toolStripMenuItem5.Click += this.Zoom75;
			this.toolStripMenuItem6.Click += this.Zoom50;
			this.toolStripMenuItem7.Click += this.Zoom25;
			this.toolStripMenuItem8.Click += this.Zoom10;
			this.onepageToolStripButton.Click += this.OnonepageToolStripButtonClick;
			this.twopagesToolStripButton.Click += this.OntwopagesToolStripButtonClick;
			this.threepagesToolStripButton.Click += this.OnthreepagesToolStripButtonClick;
			this.fourpagesToolStripButton.Click += this.OnfourpagesToolStripButtonClick;
			this.sixpagesToolStripButton.Click += this.OnsixpagesToolStripButtonClick;
			this.closeToolStripButton.Click += this.OncloseToolStripButtonClick;
			this.closeToolStripButton.Paint += this.OncloseToolStripButtonPaint;
			this.toolStrip1.ImageList = this.imageList;
			this.printToolStripButton.ImageIndex = 0;
			this.zoomToolStripSplitButton.ImageIndex = 1;
			this.onepageToolStripButton.ImageIndex = 2;
			this.twopagesToolStripButton.ImageIndex = 3;
			this.threepagesToolStripButton.ImageIndex = 4;
			this.fourpagesToolStripButton.ImageIndex = 5;
			this.sixpagesToolStripButton.ImageIndex = 6;
			this.previewControl.TabIndex = 0;
			this.toolStrip1.TabIndex = 1;
			this.zoomToolStripSplitButton.DefaultItem = this.autoToolStripMenuItem;
			ToolStripDropDownMenu toolStripDropDownMenu = this.zoomToolStripSplitButton.DropDown as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null)
			{
				toolStripDropDownMenu.ShowCheckMargin = true;
				toolStripDropDownMenu.ShowImageMargin = false;
				toolStripDropDownMenu.RenderMode = ToolStripRenderMode.System;
			}
			ToolStripControlHost toolStripControlHost = new ToolStripControlHost(this.pageCounter);
			toolStripControlHost.Alignment = ToolStripItemAlignment.Right;
			this.toolStrip1.Items.Add(toolStripControlHost);
			this.toolStrip1.Items.Add(this.pageToolStripLabel);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.previewControl);
			base.Controls.Add(this.toolStrip1);
			base.ClientSize = new Size(400, 300);
			this.MinimizeBox = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = SizeGripStyle.Hide;
			this.toolStrip1.ResumeLayout(false);
			((ISupportInitialize)this.pageCounter).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.</summary>
		/// <param name="e">Provides data for a cancelable event.</param>
		// Token: 0x06004D24 RID: 19748 RVA: 0x0013EC29 File Offset: 0x0013CE29
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			this.previewControl.InvalidatePreview();
		}

		/// <summary>Creates the handle for the form that encapsulates the <see cref="T:System.Windows.Forms.PrintPreviewDialog" />.</summary>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer settings in <see cref="P:System.Windows.Forms.PrintPreviewDialog.Document" /> are not valid.</exception>
		// Token: 0x06004D25 RID: 19749 RVA: 0x0013EC3D File Offset: 0x0013CE3D
		protected override void CreateHandle()
		{
			if (this.Document != null && !this.Document.PrinterSettings.IsValid)
			{
				throw new InvalidPrinterException(this.Document.PrinterSettings);
			}
			base.CreateHandle();
		}

		/// <summary>Determines whether a key should be processed further.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the key should be processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D26 RID: 19750 RVA: 0x0013EC70 File Offset: 0x0013CE70
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys - Keys.Left <= 3)
				{
					return false;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes the TAB key.</summary>
		/// <param name="forward">
		///   <see langword="true" /> to cycle forward through the controls in the form; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the TAB key was successfully processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D27 RID: 19751 RVA: 0x0013EC9E File Offset: 0x0013CE9E
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (base.ActiveControl == this.previewControl)
			{
				this.pageCounter.FocusInternal();
				return true;
			}
			return false;
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool ShouldSerializeAutoScaleBaseSize()
		{
			return false;
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0013ECBD File Offset: 0x0013CEBD
		internal override bool ShouldSerializeText()
		{
			return !this.Text.Equals(SR.GetString("PrintPreviewDialog_PrintPreview"));
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x0013ECD7 File Offset: 0x0013CED7
		private void OncloseToolStripButtonClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0013ECDF File Offset: 0x0013CEDF
		private void previewControl_StartPageChanged(object sender, EventArgs e)
		{
			this.pageCounter.Value = this.previewControl.StartPage + 1;
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0013ED00 File Offset: 0x0013CF00
		private void CheckZoomMenu(ToolStripMenuItem toChecked)
		{
			foreach (object obj in this.zoomToolStripSplitButton.DropDownItems)
			{
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
				toolStripMenuItem.Checked = toChecked == toolStripMenuItem;
			}
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0013ED64 File Offset: 0x0013CF64
		private void ZoomAuto(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.AutoZoom = true;
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0013ED8C File Offset: 0x0013CF8C
		private void Zoom500(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 5.0;
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0013EDBC File Offset: 0x0013CFBC
		private void Zoom250(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 2.5;
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0013EDEC File Offset: 0x0013CFEC
		private void Zoom150(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 1.5;
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0013EE1C File Offset: 0x0013D01C
		private void Zoom100(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 1.0;
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0013EE4C File Offset: 0x0013D04C
		private void Zoom75(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 0.75;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0013EE7C File Offset: 0x0013D07C
		private void Zoom50(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 0.5;
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0013EEAC File Offset: 0x0013D0AC
		private void Zoom25(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 0.25;
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0013EEDC File Offset: 0x0013D0DC
		private void Zoom10(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toolStripMenuItem);
			this.previewControl.Zoom = 0.1;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0013EF0C File Offset: 0x0013D10C
		private void OncloseToolStripButtonPaint(object sender, PaintEventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null && !toolStripItem.Selected)
			{
				Rectangle rectangle = new Rectangle(0, 0, toolStripItem.Bounds.Width - 1, toolStripItem.Bounds.Height - 1);
				using (Pen pen = new Pen(SystemColors.ControlDark))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
				}
			}
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x0013EF88 File Offset: 0x0013D188
		private void OnprintToolStripButtonClick(object sender, EventArgs e)
		{
			if (this.previewControl.Document != null)
			{
				this.previewControl.Document.Print();
			}
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0013EFA7 File Offset: 0x0013D1A7
		private void OnzoomToolStripSplitButtonClick(object sender, EventArgs e)
		{
			this.ZoomAuto(null, EventArgs.Empty);
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0013EFB5 File Offset: 0x0013D1B5
		private void OnonepageToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 1;
			this.previewControl.Columns = 1;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0013EFCF File Offset: 0x0013D1CF
		private void OntwopagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 1;
			this.previewControl.Columns = 2;
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0013EFE9 File Offset: 0x0013D1E9
		private void OnthreepagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 1;
			this.previewControl.Columns = 3;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0013F003 File Offset: 0x0013D203
		private void OnfourpagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 2;
			this.previewControl.Columns = 2;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0013F01D File Offset: 0x0013D21D
		private void OnsixpagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 2;
			this.previewControl.Columns = 3;
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0013F038 File Offset: 0x0013D238
		private void UpdownMove(object sender, EventArgs eventargs)
		{
			int num = (int)this.pageCounter.Value - 1;
			if (num >= 0)
			{
				this.previewControl.StartPage = num;
				return;
			}
			this.pageCounter.Value = this.previewControl.StartPage + 1;
		}

		// Token: 0x04002893 RID: 10387
		private PrintPreviewControl previewControl;

		// Token: 0x04002894 RID: 10388
		private ToolStrip toolStrip1;

		// Token: 0x04002895 RID: 10389
		private NumericUpDown pageCounter;

		// Token: 0x04002896 RID: 10390
		private ToolStripButton printToolStripButton;

		// Token: 0x04002897 RID: 10391
		private ToolStripSplitButton zoomToolStripSplitButton;

		// Token: 0x04002898 RID: 10392
		private ToolStripMenuItem autoToolStripMenuItem;

		// Token: 0x04002899 RID: 10393
		private ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x0400289A RID: 10394
		private ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x0400289B RID: 10395
		private ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x0400289C RID: 10396
		private ToolStripMenuItem toolStripMenuItem4;

		// Token: 0x0400289D RID: 10397
		private ToolStripMenuItem toolStripMenuItem5;

		// Token: 0x0400289E RID: 10398
		private ToolStripMenuItem toolStripMenuItem6;

		// Token: 0x0400289F RID: 10399
		private ToolStripMenuItem toolStripMenuItem7;

		// Token: 0x040028A0 RID: 10400
		private ToolStripMenuItem toolStripMenuItem8;

		// Token: 0x040028A1 RID: 10401
		private ToolStripSeparator separatorToolStripSeparator;

		// Token: 0x040028A2 RID: 10402
		private ToolStripButton onepageToolStripButton;

		// Token: 0x040028A3 RID: 10403
		private ToolStripButton twopagesToolStripButton;

		// Token: 0x040028A4 RID: 10404
		private ToolStripButton threepagesToolStripButton;

		// Token: 0x040028A5 RID: 10405
		private ToolStripButton fourpagesToolStripButton;

		// Token: 0x040028A6 RID: 10406
		private ToolStripButton sixpagesToolStripButton;

		// Token: 0x040028A7 RID: 10407
		private ToolStripSeparator separatorToolStripSeparator1;

		// Token: 0x040028A8 RID: 10408
		private ToolStripButton closeToolStripButton;

		// Token: 0x040028A9 RID: 10409
		private ToolStripLabel pageToolStripLabel;

		// Token: 0x040028AA RID: 10410
		private ImageList imageList;
	}
}

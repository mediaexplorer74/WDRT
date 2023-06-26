using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows vertical scroll bar.</summary>
	// Token: 0x0200042D RID: 1069
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionVScrollBar")]
	public class VScrollBar : ScrollBar
	{
		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06004A10 RID: 18960 RVA: 0x00137578 File Offset: 0x00135778
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 1;
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06004A11 RID: 18961 RVA: 0x0013759B File Offset: 0x0013579B
		protected override Size DefaultSize
		{
			get
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					return new Size(SystemInformation.GetVerticalScrollBarWidthForDpi(this.deviceDpi), base.LogicalToDeviceUnits(80));
				}
				return new Size(SystemInformation.VerticalScrollBarWidth, 80);
			}
		}

		/// <summary>Gets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.RightToLeft.No" /> value.</returns>
		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004A12 RID: 18962 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06004A13 RID: 18963 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return RightToLeft.No;
			}
			set
			{
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.VScrollBar.RightToLeft" /> property changes.</summary>
		// Token: 0x140003B9 RID: 953
		// (add) Token: 0x06004A14 RID: 18964 RVA: 0x000E3233 File Offset: 0x000E1433
		// (remove) Token: 0x06004A15 RID: 18965 RVA: 0x000E323C File Offset: 0x000E143C
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

		// Token: 0x040027C9 RID: 10185
		private const int VERTICAL_SCROLLBAR_HEIGHT = 80;
	}
}

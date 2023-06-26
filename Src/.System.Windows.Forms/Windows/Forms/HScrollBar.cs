using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows horizontal scroll bar.</summary>
	// Token: 0x02000276 RID: 630
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionHScrollBar")]
	public class HScrollBar : ScrollBar
	{
		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000BAC54 File Offset: 0x000B8E54
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 0;
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000BAC77 File Offset: 0x000B8E77
		protected override Size DefaultSize
		{
			get
			{
				return new Size(80, SystemInformation.HorizontalScrollBarHeight);
			}
		}
	}
}

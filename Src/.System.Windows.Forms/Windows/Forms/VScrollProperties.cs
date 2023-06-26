using System;

namespace System.Windows.Forms
{
	/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.VScrollBar" /> class.</summary>
	// Token: 0x0200042E RID: 1070
	public class VScrollProperties : ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollProperties" /> class.</summary>
		/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
		// Token: 0x06004A17 RID: 18967 RVA: 0x000BAC8D File Offset: 0x000B8E8D
		public VScrollProperties(ScrollableControl container)
			: base(container)
		{
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x001375CC File Offset: 0x001357CC
		internal override int PageSize
		{
			get
			{
				return base.ParentControl.ClientRectangle.Height;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06004A19 RID: 18969 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override int Orientation
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06004A1A RID: 18970 RVA: 0x001375EC File Offset: 0x001357EC
		internal override int HorizontalDisplayPosition
		{
			get
			{
				return base.ParentControl.DisplayRectangle.X;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06004A1B RID: 18971 RVA: 0x000BACB8 File Offset: 0x000B8EB8
		internal override int VerticalDisplayPosition
		{
			get
			{
				return -this.value;
			}
		}
	}
}

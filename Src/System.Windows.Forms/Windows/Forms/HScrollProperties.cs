using System;

namespace System.Windows.Forms
{
	/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.HScrollBar" /></summary>
	// Token: 0x02000277 RID: 631
	public class HScrollProperties : ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollProperties" /> class.</summary>
		/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
		// Token: 0x06002831 RID: 10289 RVA: 0x000BAC8D File Offset: 0x000B8E8D
		public HScrollProperties(ScrollableControl container)
			: base(container)
		{
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000BAC98 File Offset: 0x000B8E98
		internal override int PageSize
		{
			get
			{
				return base.ParentControl.ClientRectangle.Width;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override int Orientation
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x000BACB8 File Offset: 0x000B8EB8
		internal override int HorizontalDisplayPosition
		{
			get
			{
				return -this.value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x000BACC4 File Offset: 0x000B8EC4
		internal override int VerticalDisplayPosition
		{
			get
			{
				return base.ParentControl.DisplayRectangle.Y;
			}
		}
	}
}

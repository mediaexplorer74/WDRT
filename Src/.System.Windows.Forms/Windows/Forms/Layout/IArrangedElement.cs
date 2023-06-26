using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CA RID: 1226
	internal interface IArrangedElement : IComponent, IDisposable
	{
		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600508F RID: 20623
		Rectangle Bounds { get; }

		// Token: 0x06005090 RID: 20624
		void SetBounds(Rectangle bounds, BoundsSpecified specified);

		// Token: 0x06005091 RID: 20625
		Size GetPreferredSize(Size proposedSize);

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06005092 RID: 20626
		Rectangle DisplayRectangle { get; }

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06005093 RID: 20627
		bool ParticipatesInLayout { get; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06005094 RID: 20628
		PropertyStore Properties { get; }

		// Token: 0x06005095 RID: 20629
		void PerformLayout(IArrangedElement affectedElement, string propertyName);

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06005096 RID: 20630
		IArrangedElement Container { get; }

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06005097 RID: 20631
		ArrangedElementCollection Children { get; }
	}
}

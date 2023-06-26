using System;
using System.Drawing;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003CB RID: 971
	internal class ToolStripItemInternalLayout
	{
		// Token: 0x060042F7 RID: 17143 RVA: 0x0011C177 File Offset: 0x0011A377
		public ToolStripItemInternalLayout(ToolStripItem ownerItem)
		{
			if (ownerItem == null)
			{
				throw new ArgumentNullException("ownerItem");
			}
			this.ownerItem = ownerItem;
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060042F8 RID: 17144 RVA: 0x0011C19F File Offset: 0x0011A39F
		protected virtual ToolStripItem Owner
		{
			get
			{
				return this.ownerItem;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x0011C1A8 File Offset: 0x0011A3A8
		public virtual Rectangle ImageRectangle
		{
			get
			{
				Rectangle imageBounds = this.LayoutData.imageBounds;
				imageBounds.Intersect(this.layoutData.field);
				return imageBounds;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060042FA RID: 17146 RVA: 0x0011C1D4 File Offset: 0x0011A3D4
		internal ButtonBaseAdapter.LayoutData LayoutData
		{
			get
			{
				this.EnsureLayout();
				return this.layoutData;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x0011C1E3 File Offset: 0x0011A3E3
		public Size PreferredImageSize
		{
			get
			{
				return this.Owner.PreferredImageSize;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x0011C1F0 File Offset: 0x0011A3F0
		protected virtual ToolStrip ParentInternal
		{
			get
			{
				if (this.ownerItem == null)
				{
					return null;
				}
				return this.ownerItem.ParentInternal;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x0011C208 File Offset: 0x0011A408
		public virtual Rectangle TextRectangle
		{
			get
			{
				Rectangle textBounds = this.LayoutData.textBounds;
				textBounds.Intersect(this.layoutData.field);
				return textBounds;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x0011C234 File Offset: 0x0011A434
		public virtual Rectangle ContentRectangle
		{
			get
			{
				return this.LayoutData.field;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x0011C241 File Offset: 0x0011A441
		public virtual TextFormatFlags TextFormat
		{
			get
			{
				if (this.currentLayoutOptions != null)
				{
					return this.currentLayoutOptions.gdiTextFormatFlags;
				}
				return this.CommonLayoutOptions().gdiTextFormatFlags;
			}
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x0011C264 File Offset: 0x0011A464
		internal static TextFormatFlags ContentAlignToTextFormat(ContentAlignment alignment, bool rightToLeft)
		{
			TextFormatFlags textFormatFlags = TextFormatFlags.Default;
			if (rightToLeft)
			{
				textFormatFlags |= TextFormatFlags.RightToLeft;
			}
			textFormatFlags |= ControlPaint.TranslateAlignmentForGDI(alignment);
			return textFormatFlags | ControlPaint.TranslateLineAlignmentForGDI(alignment);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x0011C294 File Offset: 0x0011A494
		protected virtual ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
		{
			ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = new ToolStripItemInternalLayout.ToolStripItemLayoutOptions();
			Rectangle rectangle = new Rectangle(Point.Empty, this.ownerItem.Size);
			toolStripItemLayoutOptions.client = rectangle;
			toolStripItemLayoutOptions.growBorderBy1PxWhenDefault = false;
			toolStripItemLayoutOptions.borderSize = 2;
			toolStripItemLayoutOptions.paddingSize = 0;
			toolStripItemLayoutOptions.maxFocus = true;
			toolStripItemLayoutOptions.focusOddEvenFixup = false;
			toolStripItemLayoutOptions.font = this.ownerItem.Font;
			toolStripItemLayoutOptions.text = (((this.Owner.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text) ? this.Owner.Text : string.Empty);
			toolStripItemLayoutOptions.imageSize = this.PreferredImageSize;
			toolStripItemLayoutOptions.checkSize = 0;
			toolStripItemLayoutOptions.checkPaddingSize = 0;
			toolStripItemLayoutOptions.checkAlign = ContentAlignment.TopLeft;
			toolStripItemLayoutOptions.imageAlign = this.Owner.ImageAlign;
			toolStripItemLayoutOptions.textAlign = this.Owner.TextAlign;
			toolStripItemLayoutOptions.hintTextUp = false;
			toolStripItemLayoutOptions.shadowedText = !this.ownerItem.Enabled;
			toolStripItemLayoutOptions.layoutRTL = RightToLeft.Yes == this.Owner.RightToLeft;
			toolStripItemLayoutOptions.textImageRelation = this.Owner.TextImageRelation;
			toolStripItemLayoutOptions.textImageInset = 0;
			toolStripItemLayoutOptions.everettButtonCompat = false;
			toolStripItemLayoutOptions.gdiTextFormatFlags = ToolStripItemInternalLayout.ContentAlignToTextFormat(this.Owner.TextAlign, this.Owner.RightToLeft == RightToLeft.Yes);
			toolStripItemLayoutOptions.gdiTextFormatFlags = (this.Owner.ShowKeyboardCues ? toolStripItemLayoutOptions.gdiTextFormatFlags : (toolStripItemLayoutOptions.gdiTextFormatFlags | TextFormatFlags.HidePrefix));
			return toolStripItemLayoutOptions;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x0011C3FE File Offset: 0x0011A5FE
		private bool EnsureLayout()
		{
			if (this.layoutData == null || this.parentLayoutData == null || !this.parentLayoutData.IsCurrent(this.ParentInternal))
			{
				this.PerformLayout();
				return true;
			}
			return false;
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x0011C42C File Offset: 0x0011A62C
		private ButtonBaseAdapter.LayoutData GetLayoutData()
		{
			this.currentLayoutOptions = this.CommonLayoutOptions();
			if (this.Owner.TextDirection != ToolStripTextDirection.Horizontal)
			{
				this.currentLayoutOptions.verticalText = true;
			}
			return this.currentLayoutOptions.Layout();
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x0011C46C File Offset: 0x0011A66C
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			Size empty = Size.Empty;
			this.EnsureLayout();
			if (this.ownerItem != null)
			{
				this.lastPreferredSize = this.currentLayoutOptions.GetPreferredSizeCore(constrainingSize);
				return this.lastPreferredSize;
			}
			return Size.Empty;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0011C4AC File Offset: 0x0011A6AC
		internal void PerformLayout()
		{
			this.layoutData = this.GetLayoutData();
			ToolStrip parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				this.parentLayoutData = new ToolStripItemInternalLayout.ToolStripLayoutData(parentInternal);
				return;
			}
			this.parentLayoutData = null;
		}

		// Token: 0x04002587 RID: 9607
		private ToolStripItemInternalLayout.ToolStripItemLayoutOptions currentLayoutOptions;

		// Token: 0x04002588 RID: 9608
		private ToolStripItem ownerItem;

		// Token: 0x04002589 RID: 9609
		private ButtonBaseAdapter.LayoutData layoutData;

		// Token: 0x0400258A RID: 9610
		private const int BORDER_WIDTH = 2;

		// Token: 0x0400258B RID: 9611
		private const int BORDER_HEIGHT = 3;

		// Token: 0x0400258C RID: 9612
		private static readonly Size INVALID_SIZE = new Size(int.MinValue, int.MinValue);

		// Token: 0x0400258D RID: 9613
		private Size lastPreferredSize = ToolStripItemInternalLayout.INVALID_SIZE;

		// Token: 0x0400258E RID: 9614
		private ToolStripItemInternalLayout.ToolStripLayoutData parentLayoutData;

		// Token: 0x02000804 RID: 2052
		internal class ToolStripItemLayoutOptions : ButtonBaseAdapter.LayoutOptions
		{
			// Token: 0x06006EDF RID: 28383 RVA: 0x00196160 File Offset: 0x00194360
			protected override Size GetTextSize(Size proposedConstraints)
			{
				if (this.cachedSize != LayoutUtils.InvalidSize && (this.cachedProposedConstraints == proposedConstraints || this.cachedSize.Width <= proposedConstraints.Width))
				{
					return this.cachedSize;
				}
				this.cachedSize = base.GetTextSize(proposedConstraints);
				this.cachedProposedConstraints = proposedConstraints;
				return this.cachedSize;
			}

			// Token: 0x040042FB RID: 17147
			private Size cachedSize = LayoutUtils.InvalidSize;

			// Token: 0x040042FC RID: 17148
			private Size cachedProposedConstraints = LayoutUtils.InvalidSize;
		}

		// Token: 0x02000805 RID: 2053
		private class ToolStripLayoutData
		{
			// Token: 0x06006EE1 RID: 28385 RVA: 0x001961E0 File Offset: 0x001943E0
			public ToolStripLayoutData(ToolStrip toolStrip)
			{
				this.layoutStyle = toolStrip.LayoutStyle;
				this.autoSize = toolStrip.AutoSize;
				this.size = toolStrip.Size;
			}

			// Token: 0x06006EE2 RID: 28386 RVA: 0x0019620C File Offset: 0x0019440C
			public bool IsCurrent(ToolStrip toolStrip)
			{
				return toolStrip != null && (toolStrip.Size == this.size && toolStrip.LayoutStyle == this.layoutStyle) && toolStrip.AutoSize == this.autoSize;
			}

			// Token: 0x040042FD RID: 17149
			private ToolStripLayoutStyle layoutStyle;

			// Token: 0x040042FE RID: 17150
			private bool autoSize;

			// Token: 0x040042FF RID: 17151
			private Size size;
		}
	}
}

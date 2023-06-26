using System;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003E7 RID: 999
	internal class ToolStripMenuItemInternalLayout : ToolStripItemInternalLayout
	{
		// Token: 0x06004431 RID: 17457 RVA: 0x001208EE File Offset: 0x0011EAEE
		public ToolStripMenuItemInternalLayout(ToolStripMenuItem ownerItem)
			: base(ownerItem)
		{
			this.ownerItem = ownerItem;
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x00120900 File Offset: 0x0011EB00
		public bool ShowCheckMargin
		{
			get
			{
				ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
				return toolStripDropDownMenu != null && toolStripDropDownMenu.ShowCheckMargin;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x0012092C File Offset: 0x0011EB2C
		public bool ShowImageMargin
		{
			get
			{
				ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
				return toolStripDropDownMenu != null && toolStripDropDownMenu.ShowImageMargin;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x00120955 File Offset: 0x0011EB55
		public bool PaintCheck
		{
			get
			{
				return this.ShowCheckMargin || this.ShowImageMargin;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x00120967 File Offset: 0x0011EB67
		public bool PaintImage
		{
			get
			{
				return this.ShowImageMargin;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x00120970 File Offset: 0x0011EB70
		public Rectangle ArrowRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						Rectangle arrowRectangle = toolStripDropDownMenu.ArrowRectangle;
						arrowRectangle.Y = LayoutUtils.VAlign(arrowRectangle.Size, this.ownerItem.ClientBounds, ContentAlignment.MiddleCenter).Y;
						return arrowRectangle;
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x001209D0 File Offset: 0x0011EBD0
		public Rectangle CheckRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						Rectangle checkRectangle = toolStripDropDownMenu.CheckRectangle;
						if (this.ownerItem.CheckedImage != null)
						{
							int height = this.ownerItem.CheckedImage.Height;
							checkRectangle.Y += (checkRectangle.Height - height) / 2;
							checkRectangle.Height = height;
							return checkRectangle;
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x00120A48 File Offset: 0x0011EC48
		public override Rectangle ImageRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						Rectangle imageRectangle = toolStripDropDownMenu.ImageRectangle;
						if (this.ownerItem.ImageScaling == ToolStripItemImageScaling.SizeToFit)
						{
							imageRectangle.Size = toolStripDropDownMenu.ImageScalingSize;
						}
						else
						{
							Image image = this.ownerItem.Image ?? this.ownerItem.CheckedImage;
							imageRectangle.Size = image.Size;
						}
						imageRectangle.Y = LayoutUtils.VAlign(imageRectangle.Size, this.ownerItem.ClientBounds, ContentAlignment.MiddleCenter).Y;
						return imageRectangle;
					}
				}
				return base.ImageRectangle;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x00120AF0 File Offset: 0x0011ECF0
		public override Rectangle TextRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						return toolStripDropDownMenu.TextRectangle;
					}
				}
				return base.TextRectangle;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x00120B26 File Offset: 0x0011ED26
		public bool UseMenuLayout
		{
			get
			{
				return this.ownerItem.Owner is ToolStripDropDownMenu;
			}
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00120B3C File Offset: 0x0011ED3C
		public override Size GetPreferredSize(Size constrainingSize)
		{
			if (this.UseMenuLayout)
			{
				ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					return toolStripDropDownMenu.MaxItemSize;
				}
			}
			return base.GetPreferredSize(constrainingSize);
		}

		// Token: 0x04002611 RID: 9745
		private ToolStripMenuItem ownerItem;
	}
}

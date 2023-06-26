using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the look and feel of a column in a table layout.</summary>
	// Token: 0x02000394 RID: 916
	public class ColumnStyle : TableLayoutStyle
	{
		/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class to its default state.</summary>
		// Token: 0x06003C12 RID: 15378 RVA: 0x00106A6C File Offset: 0x00104C6C
		public ColumnStyle()
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> value.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the column should be should be sized relative to its containing table.</param>
		// Token: 0x06003C13 RID: 15379 RVA: 0x00106A74 File Offset: 0x00104C74
		public ColumnStyle(SizeType sizeType)
		{
			base.SizeType = sizeType;
		}

		/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> and width values.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the column should be should be sized relative to its containing table.</param>
		/// <param name="width">The preferred width, in pixels or percentage, depending on the <paramref name="sizeType" /> parameter.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="width" /> is less than 0.</exception>
		// Token: 0x06003C14 RID: 15380 RVA: 0x00106A83 File Offset: 0x00104C83
		public ColumnStyle(SizeType sizeType, float width)
		{
			base.SizeType = sizeType;
			this.Width = width;
		}

		/// <summary>Gets or sets the width value for a column.</summary>
		/// <returns>The preferred width, in pixels or percentage, depending on the <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 when setting this property.</exception>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x00106A99 File Offset: 0x00104C99
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x00106AA1 File Offset: 0x00104CA1
		public float Width
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
	}
}

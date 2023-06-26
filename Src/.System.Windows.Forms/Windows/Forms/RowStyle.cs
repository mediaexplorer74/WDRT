using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the look and feel of a row in a table layout.</summary>
	// Token: 0x02000395 RID: 917
	public class RowStyle : TableLayoutStyle
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class to its default state.</summary>
		// Token: 0x06003C17 RID: 15383 RVA: 0x00106A6C File Offset: 0x00104C6C
		public RowStyle()
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> value.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the row should be should be sized relative to its containing table.</param>
		// Token: 0x06003C18 RID: 15384 RVA: 0x00106A74 File Offset: 0x00104C74
		public RowStyle(SizeType sizeType)
		{
			base.SizeType = sizeType;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> and height values.</summary>
		/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the row should be should be sized relative to its containing table.</param>
		/// <param name="height">The preferred height in pixels or percentage of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />, depending on <paramref name="sizeType" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="height" /> is less than 0.</exception>
		// Token: 0x06003C19 RID: 15385 RVA: 0x00106AAA File Offset: 0x00104CAA
		public RowStyle(SizeType sizeType, float height)
		{
			base.SizeType = sizeType;
			this.Height = height;
		}

		/// <summary>Gets or sets the height of a row.</summary>
		/// <returns>The preferred height of a row in pixels or percentage of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />, depending on the <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 when setting this property.</exception>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x00106A99 File Offset: 0x00104C99
		// (set) Token: 0x06003C1B RID: 15387 RVA: 0x00106AA1 File Offset: 0x00104CA1
		public float Height
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

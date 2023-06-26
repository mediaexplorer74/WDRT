using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Contains border styles for the cells in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x0200018F RID: 399
	public sealed class DataGridViewAdvancedBorderStyle : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> class.</summary>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x00086217 File Offset: 0x00084417
		public DataGridViewAdvancedBorderStyle()
			: this(null, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet)
		{
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00086223 File Offset: 0x00084423
		internal DataGridViewAdvancedBorderStyle(DataGridView owner)
			: this(owner, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet)
		{
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00086230 File Offset: 0x00084430
		internal DataGridViewAdvancedBorderStyle(DataGridView owner, DataGridViewAdvancedCellBorderStyle banned1, DataGridViewAdvancedCellBorderStyle banned2, DataGridViewAdvancedCellBorderStyle banned3)
		{
			this.owner = owner;
			this.banned1 = banned1;
			this.banned2 = banned2;
			this.banned3 = banned3;
		}

		/// <summary>Gets or sets the border style for all of the borders of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.  
		///  -or-  
		///  The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" />, <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetPartial" />, or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance was retrieved through the <see cref="P:System.Windows.Forms.DataGridView.AdvancedCellBorderStyle" /> property.</exception>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00086283 File Offset: 0x00084483
		// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x00086298 File Offset: 0x00084498
		public DataGridViewAdvancedCellBorderStyle All
		{
			get
			{
				if (!this.all)
				{
					return DataGridViewAdvancedCellBorderStyle.NotSet;
				}
				return this.top;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet || value == this.banned1 || value == this.banned2 || value == this.banned3)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "All" }));
				}
				if (!this.all || this.top != value)
				{
					this.all = true;
					this.bottom = value;
					this.right = value;
					this.left = value;
					this.top = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		/// <summary>Gets or sets the style for the bottom border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.</exception>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00086355 File Offset: 0x00084555
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x0008636C File Offset: 0x0008456C
		public DataGridViewAdvancedCellBorderStyle Bottom
		{
			get
			{
				if (this.all)
				{
					return this.top;
				}
				return this.bottom;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "Bottom" }));
				}
				this.BottomInternal = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x000863C8 File Offset: 0x000845C8
		internal DataGridViewAdvancedCellBorderStyle BottomInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.bottom != value))
				{
					if (this.all && this.right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
					{
						this.right = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					this.all = false;
					this.bottom = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		/// <summary>Gets the style for the left border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.  
		///  -or-  
		///  The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance has an associated <see cref="T:System.Windows.Forms.DataGridView" /> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of <see langword="true" />.</exception>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00086431 File Offset: 0x00084631
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x00086448 File Offset: 0x00084648
		public DataGridViewAdvancedCellBorderStyle Left
		{
			get
			{
				if (this.all)
				{
					return this.top;
				}
				return this.left;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "Left" }));
				}
				this.LeftInternal = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x000864A4 File Offset: 0x000846A4
		internal DataGridViewAdvancedCellBorderStyle LeftInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.left != value))
				{
					if (this.owner != null && this.owner.RightToLeftInternal && (value == DataGridViewAdvancedCellBorderStyle.InsetDouble || value == DataGridViewAdvancedCellBorderStyle.OutsetDouble))
					{
						throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "Left" }));
					}
					if (this.all)
					{
						if (this.right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.right = DataGridViewAdvancedCellBorderStyle.Outset;
						}
						if (this.bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.bottom = DataGridViewAdvancedCellBorderStyle.Outset;
						}
					}
					this.all = false;
					this.left = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		/// <summary>Gets the style for the right border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.  
		///  -or-  
		///  The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance has an associated <see cref="T:System.Windows.Forms.DataGridView" /> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of <see langword="false" />.</exception>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0008655E File Offset: 0x0008475E
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x00086578 File Offset: 0x00084778
		public DataGridViewAdvancedCellBorderStyle Right
		{
			get
			{
				if (this.all)
				{
					return this.top;
				}
				return this.right;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "Right" }));
				}
				this.RightInternal = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x000865D4 File Offset: 0x000847D4
		internal DataGridViewAdvancedCellBorderStyle RightInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.right != value))
				{
					if (this.owner != null && !this.owner.RightToLeftInternal && (value == DataGridViewAdvancedCellBorderStyle.InsetDouble || value == DataGridViewAdvancedCellBorderStyle.OutsetDouble))
					{
						throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "Right" }));
					}
					if (this.all && this.bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
					{
						this.bottom = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					this.all = false;
					this.right = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		/// <summary>Gets the style for the top border of a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.</exception>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00086678 File Offset: 0x00084878
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x00086680 File Offset: 0x00084880
		public DataGridViewAdvancedCellBorderStyle Top
		{
			get
			{
				return this.top;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[] { "Top" }));
				}
				this.TopInternal = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x000866DC File Offset: 0x000848DC
		internal DataGridViewAdvancedCellBorderStyle TopInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.top != value))
				{
					if (this.all)
					{
						if (this.right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.right = DataGridViewAdvancedCellBorderStyle.Outset;
						}
						if (this.bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.bottom = DataGridViewAdvancedCellBorderStyle.Outset;
						}
					}
					this.all = false;
					this.top = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</summary>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is a <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> and the values for the <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Top" />, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Bottom" />, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Left" />, and <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Right" /> properties are equal to their counterpart in the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00086758 File Offset: 0x00084958
		public override bool Equals(object other)
		{
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = other as DataGridViewAdvancedBorderStyle;
			return dataGridViewAdvancedBorderStyle != null && (dataGridViewAdvancedBorderStyle.all == this.all && dataGridViewAdvancedBorderStyle.top == this.top && dataGridViewAdvancedBorderStyle.left == this.left && dataGridViewAdvancedBorderStyle.bottom == this.bottom) && dataGridViewAdvancedBorderStyle.right == this.right;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x000867B9 File Offset: 0x000849B9
		public override int GetHashCode()
		{
			return WindowsFormsUtils.GetCombinedHashCodes(new int[]
			{
				(int)this.top,
				(int)this.left,
				(int)this.bottom,
				(int)this.right
			});
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</summary>
		/// <returns>A string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</returns>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x000867EC File Offset: 0x000849EC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewAdvancedBorderStyle { All=",
				this.All.ToString(),
				", Left=",
				this.Left.ToString(),
				", Right=",
				this.Right.ToString(),
				", Top=",
				this.Top.ToString(),
				", Bottom=",
				this.Bottom.ToString(),
				" }"
			});
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x06001CB4 RID: 7348 RVA: 0x000868AC File Offset: 0x00084AAC
		object ICloneable.Clone()
		{
			return new DataGridViewAdvancedBorderStyle(this.owner, this.banned1, this.banned2, this.banned3)
			{
				all = this.all,
				top = this.top,
				right = this.right,
				bottom = this.bottom,
				left = this.left
			};
		}

		// Token: 0x04000C19 RID: 3097
		private DataGridView owner;

		// Token: 0x04000C1A RID: 3098
		private bool all = true;

		// Token: 0x04000C1B RID: 3099
		private DataGridViewAdvancedCellBorderStyle banned1;

		// Token: 0x04000C1C RID: 3100
		private DataGridViewAdvancedCellBorderStyle banned2;

		// Token: 0x04000C1D RID: 3101
		private DataGridViewAdvancedCellBorderStyle banned3;

		// Token: 0x04000C1E RID: 3102
		private DataGridViewAdvancedCellBorderStyle top = DataGridViewAdvancedCellBorderStyle.None;

		// Token: 0x04000C1F RID: 3103
		private DataGridViewAdvancedCellBorderStyle left = DataGridViewAdvancedCellBorderStyle.None;

		// Token: 0x04000C20 RID: 3104
		private DataGridViewAdvancedCellBorderStyle right = DataGridViewAdvancedCellBorderStyle.None;

		// Token: 0x04000C21 RID: 3105
		private DataGridViewAdvancedCellBorderStyle bottom = DataGridViewAdvancedCellBorderStyle.None;
	}
}

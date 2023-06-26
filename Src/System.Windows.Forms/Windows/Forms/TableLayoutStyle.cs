using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality that represents the appearance and behavior of a table layout.</summary>
	// Token: 0x02000396 RID: 918
	[TypeConverter(typeof(TableLayoutSettings.StyleConverter))]
	public abstract class TableLayoutStyle
	{
		/// <summary>Gets or sets a flag indicating how a row or column should be sized relative to its containing table.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SizeType" /> values that specifies how rows or columns of user interface (UI) elements should be sized relative to their container. The default is <see cref="F:System.Windows.Forms.SizeType.AutoSize" />.</returns>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x00106AC0 File Offset: 0x00104CC0
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x00106AC8 File Offset: 0x00104CC8
		[DefaultValue(SizeType.AutoSize)]
		public SizeType SizeType
		{
			get
			{
				return this._sizeType;
			}
			set
			{
				if (this._sizeType != value)
				{
					this._sizeType = value;
					if (this.Owner != null)
					{
						LayoutTransaction.DoLayout(this.Owner, this.Owner, PropertyNames.Style);
						Control control = this.Owner as Control;
						if (control != null)
						{
							control.Invalidate();
						}
					}
				}
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x00106B18 File Offset: 0x00104D18
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x00106B20 File Offset: 0x00104D20
		internal float Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("Size", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"Size",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this._size != value)
				{
					this._size = value;
					if (this.Owner != null)
					{
						LayoutTransaction.DoLayout(this.Owner, this.Owner, PropertyNames.Style);
						Control control = this.Owner as Control;
						if (control != null)
						{
							control.Invalidate();
						}
					}
				}
			}
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x00106BBB File Offset: 0x00104DBB
		private bool ShouldSerializeSize()
		{
			return this.SizeType > SizeType.AutoSize;
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x00106BC6 File Offset: 0x00104DC6
		// (set) Token: 0x06003C22 RID: 15394 RVA: 0x00106BCE File Offset: 0x00104DCE
		internal IArrangedElement Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x00106BD7 File Offset: 0x00104DD7
		internal void SetSize(float size)
		{
			this._size = size;
		}

		// Token: 0x0400238D RID: 9101
		private IArrangedElement _owner;

		// Token: 0x0400238E RID: 9102
		private SizeType _sizeType;

		// Token: 0x0400238F RID: 9103
		private float _size;
	}
}

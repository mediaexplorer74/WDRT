using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents an adjustable arrow-shaped line cap. This class cannot be inherited.</summary>
	// Token: 0x020000B2 RID: 178
	public sealed class AdjustableArrowCap : CustomLineCap
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0002581B File Offset: 0x00023A1B
		internal AdjustableArrowCap(IntPtr nativeCap)
			: base(nativeCap)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width and height. The arrow end caps created with this constructor are always filled.</summary>
		/// <param name="width">The width of the arrow.</param>
		/// <param name="height">The height of the arrow.</param>
		// Token: 0x06000A28 RID: 2600 RVA: 0x00025824 File Offset: 0x00023A24
		public AdjustableArrowCap(float width, float height)
			: this(width, height, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width, height, and fill property. Whether an arrow end cap is filled depends on the argument passed to the <paramref name="isFilled" /> parameter.</summary>
		/// <param name="width">The width of the arrow.</param>
		/// <param name="height">The height of the arrow.</param>
		/// <param name="isFilled">
		///   <see langword="true" /> to fill the arrow cap; otherwise, <see langword="false" />.</param>
		// Token: 0x06000A29 RID: 2601 RVA: 0x00025830 File Offset: 0x00023A30
		public AdjustableArrowCap(float width, float height, bool isFilled)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateAdjustableArrowCap(height, width, isFilled, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeLineCap(zero);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00025868 File Offset: 0x00023A68
		private void _SetHeight(float height)
		{
			int num = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapHeight(new HandleRef(this, this.nativeCap), height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00025898 File Offset: 0x00023A98
		private float _GetHeight()
		{
			float num2;
			int num = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapHeight(new HandleRef(this, this.nativeCap), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2;
		}

		/// <summary>Gets or sets the height of the arrow cap.</summary>
		/// <returns>The height of the arrow cap.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x000258C9 File Offset: 0x00023AC9
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x000258D1 File Offset: 0x00023AD1
		public float Height
		{
			get
			{
				return this._GetHeight();
			}
			set
			{
				this._SetHeight(value);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000258DC File Offset: 0x00023ADC
		private void _SetWidth(float width)
		{
			int num = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapWidth(new HandleRef(this, this.nativeCap), width);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002590C File Offset: 0x00023B0C
		private float _GetWidth()
		{
			float num2;
			int num = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapWidth(new HandleRef(this, this.nativeCap), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2;
		}

		/// <summary>Gets or sets the width of the arrow cap.</summary>
		/// <returns>The width, in units, of the arrow cap.</returns>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002593D File Offset: 0x00023B3D
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x00025945 File Offset: 0x00023B45
		public float Width
		{
			get
			{
				return this._GetWidth();
			}
			set
			{
				this._SetWidth(value);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00025950 File Offset: 0x00023B50
		private void _SetMiddleInset(float middleInset)
		{
			int num = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapMiddleInset(new HandleRef(this, this.nativeCap), middleInset);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00025980 File Offset: 0x00023B80
		private float _GetMiddleInset()
		{
			float num2;
			int num = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapMiddleInset(new HandleRef(this, this.nativeCap), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2;
		}

		/// <summary>Gets or sets the number of units between the outline of the arrow cap and the fill.</summary>
		/// <returns>The number of units between the outline of the arrow cap and the fill of the arrow cap.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x000259B1 File Offset: 0x00023BB1
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x000259B9 File Offset: 0x00023BB9
		public float MiddleInset
		{
			get
			{
				return this._GetMiddleInset();
			}
			set
			{
				this._SetMiddleInset(value);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000259C4 File Offset: 0x00023BC4
		private void _SetFillState(bool isFilled)
		{
			int num = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapFillState(new HandleRef(this, this.nativeCap), isFilled);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000259F4 File Offset: 0x00023BF4
		private bool _IsFilled()
		{
			bool flag = false;
			int num = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapFillState(new HandleRef(this, this.nativeCap), out flag);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return flag;
		}

		/// <summary>Gets or sets whether the arrow cap is filled.</summary>
		/// <returns>This property is <see langword="true" /> if the arrow cap is filled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00025A27 File Offset: 0x00023C27
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00025A2F File Offset: 0x00023C2F
		public bool Filled
		{
			get
			{
				return this._IsFilled();
			}
			set
			{
				this._SetFillState(value);
			}
		}
	}
}

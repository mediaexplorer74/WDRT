using System;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EC RID: 1260
	internal sealed class WindowsSolidBrush : WindowsBrush
	{
		// Token: 0x06005228 RID: 21032 RVA: 0x0015526C File Offset: 0x0015346C
		protected override void CreateBrush()
		{
			IntPtr intPtr = IntSafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(base.Color));
			intPtr == IntPtr.Zero;
			base.NativeHandle = intPtr;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0015529D File Offset: 0x0015349D
		public WindowsSolidBrush(DeviceContext dc)
			: base(dc)
		{
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x001552A6 File Offset: 0x001534A6
		public WindowsSolidBrush(DeviceContext dc, Color color)
			: base(dc, color)
		{
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x001552B0 File Offset: 0x001534B0
		public override object Clone()
		{
			return new WindowsSolidBrush(base.DC, base.Color);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x001552C3 File Offset: 0x001534C3
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: Color={1}", new object[]
			{
				base.GetType().Name,
				base.Color
			});
		}
	}
}

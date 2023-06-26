using System;
using System.Drawing;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives
{
	// Token: 0x02000005 RID: 5
	public sealed class ManufacturerInfo
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021E7 File Offset: 0x000003E7
		public ManufacturerInfo(string name, Bitmap bitmap)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			this.Name = name;
			this.Bitmap = bitmap;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002219 File Offset: 0x00000419
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002221 File Offset: 0x00000421
		public string Name { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000222A File Offset: 0x0000042A
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002232 File Offset: 0x00000432
		public Bitmap Bitmap { get; private set; }
	}
}

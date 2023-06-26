using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives
{
	// Token: 0x02000006 RID: 6
	public sealed class ModelInfo
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000223C File Offset: 0x0000043C
		public ModelInfo(string name, Bitmap bitmap, DetectionInfo detectionInfo, IEnumerable<VariantInfo> variants)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			if (detectionInfo == null)
			{
				throw new ArgumentNullException("detectionInfo");
			}
			if (variants == null)
			{
				throw new ArgumentNullException("variants");
			}
			VariantInfo[] array = variants.ToArray<VariantInfo>();
			if (array.Length == 0)
			{
				throw new ArgumentException("variants should have at least one element");
			}
			this.Name = name;
			this.Bitmap = bitmap;
			this.DetectionInfo = detectionInfo;
			this.Variants = array;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022BB File Offset: 0x000004BB
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022C3 File Offset: 0x000004C3
		public string Name { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022D4 File Offset: 0x000004D4
		public Bitmap Bitmap { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022DD File Offset: 0x000004DD
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022E5 File Offset: 0x000004E5
		public DetectionInfo DetectionInfo { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022EE File Offset: 0x000004EE
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000022F6 File Offset: 0x000004F6
		public VariantInfo[] Variants { get; private set; }
	}
}

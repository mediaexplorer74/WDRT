using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000294 RID: 660
	internal class ImageListConverter : ComponentConverter
	{
		// Token: 0x060029F1 RID: 10737 RVA: 0x000BEEAB File Offset: 0x000BD0AB
		public ImageListConverter()
			: base(typeof(ImageList))
		{
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}

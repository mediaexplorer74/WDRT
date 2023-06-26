using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x02000050 RID: 80
	public class VplFile
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00007067 File Offset: 0x00005267
		public VplFile(string name, string fileType, string fileSubtype, bool signed, bool optional, string crc)
		{
			this.Name = name;
			this.FileType = fileType;
			this.FileSubtype = fileSubtype;
			this.Signed = signed;
			this.Optional = optional;
			this.Crc = crc;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000070A4 File Offset: 0x000052A4
		// (set) Token: 0x06000298 RID: 664 RVA: 0x000070AC File Offset: 0x000052AC
		public string Name { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000070B5 File Offset: 0x000052B5
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000070BD File Offset: 0x000052BD
		public string FileType { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000070C6 File Offset: 0x000052C6
		// (set) Token: 0x0600029C RID: 668 RVA: 0x000070CE File Offset: 0x000052CE
		public string FileSubtype { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000070D7 File Offset: 0x000052D7
		// (set) Token: 0x0600029E RID: 670 RVA: 0x000070DF File Offset: 0x000052DF
		public bool Signed { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000070E8 File Offset: 0x000052E8
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x000070F0 File Offset: 0x000052F0
		public bool Optional { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000070F9 File Offset: 0x000052F9
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00007101 File Offset: 0x00005301
		public string Crc { get; private set; }
	}
}

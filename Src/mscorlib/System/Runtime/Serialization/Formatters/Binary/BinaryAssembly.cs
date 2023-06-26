using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000781 RID: 1921
	internal sealed class BinaryAssembly : IStreamable
	{
		// Token: 0x060053E4 RID: 21476 RVA: 0x00128789 File Offset: 0x00126989
		internal BinaryAssembly()
		{
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x00128791 File Offset: 0x00126991
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x001287A1 File Offset: 0x001269A1
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x001287C3 File Offset: 0x001269C3
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x001287DD File Offset: 0x001269DD
		public void Dump()
		{
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x001287DF File Offset: 0x001269DF
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025C9 RID: 9673
		internal int assemId;

		// Token: 0x040025CA RID: 9674
		internal string assemblyString;
	}
}

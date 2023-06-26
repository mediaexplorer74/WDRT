using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000782 RID: 1922
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x060053EA RID: 21482 RVA: 0x001287EC File Offset: 0x001269EC
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x001287F4 File Offset: 0x001269F4
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x00128816 File Offset: 0x00126A16
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00128830 File Offset: 0x00126A30
		public void Dump()
		{
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x00128832 File Offset: 0x00126A32
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025CB RID: 9675
		internal int assemId;

		// Token: 0x040025CC RID: 9676
		internal int assemblyIndex;
	}
}

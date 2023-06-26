using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000788 RID: 1928
	internal sealed class BinaryCrossAppDomainMap : IStreamable
	{
		// Token: 0x0600540F RID: 21519 RVA: 0x001294FB File Offset: 0x001276FB
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00129503 File Offset: 0x00127703
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(18);
			sout.WriteInt32(this.crossAppDomainArrayIndex);
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00129519 File Offset: 0x00127719
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x00129527 File Offset: 0x00127727
		public void Dump()
		{
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x00129529 File Offset: 0x00127729
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025EC RID: 9708
		internal int crossAppDomainArrayIndex;
	}
}

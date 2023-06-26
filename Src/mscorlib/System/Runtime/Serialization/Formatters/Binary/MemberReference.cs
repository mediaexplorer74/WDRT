using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200078E RID: 1934
	internal sealed class MemberReference : IStreamable
	{
		// Token: 0x06005432 RID: 21554 RVA: 0x00129DF5 File Offset: 0x00127FF5
		internal MemberReference()
		{
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00129DFD File Offset: 0x00127FFD
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x00129E06 File Offset: 0x00128006
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00129E1C File Offset: 0x0012801C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00129E2A File Offset: 0x0012802A
		public void Dump()
		{
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x00129E2C File Offset: 0x0012802C
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002609 RID: 9737
		internal int idRef;
	}
}

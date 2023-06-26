using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000787 RID: 1927
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x0600540A RID: 21514 RVA: 0x001294A8 File Offset: 0x001276A8
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x001294B0 File Offset: 0x001276B0
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x001294D2 File Offset: 0x001276D2
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x001294EC File Offset: 0x001276EC
		public void Dump()
		{
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x001294EE File Offset: 0x001276EE
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025EA RID: 9706
		internal int objectId;

		// Token: 0x040025EB RID: 9707
		internal int value;
	}
}

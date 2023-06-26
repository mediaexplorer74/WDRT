using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000786 RID: 1926
	internal sealed class BinaryObjectString : IStreamable
	{
		// Token: 0x06005404 RID: 21508 RVA: 0x00129446 File Offset: 0x00127646
		internal BinaryObjectString()
		{
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0012944E File Offset: 0x0012764E
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0012945E File Offset: 0x0012765E
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0012947F File Offset: 0x0012767F
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00129499 File Offset: 0x00127699
		public void Dump()
		{
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0012949B File Offset: 0x0012769B
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025E8 RID: 9704
		internal int objectId;

		// Token: 0x040025E9 RID: 9705
		internal string value;
	}
}

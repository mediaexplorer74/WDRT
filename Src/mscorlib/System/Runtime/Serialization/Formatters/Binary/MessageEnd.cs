using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000790 RID: 1936
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x0600543F RID: 21567 RVA: 0x00129F22 File Offset: 0x00128122
		internal MessageEnd()
		{
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x00129F2A File Offset: 0x0012812A
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00129F34 File Offset: 0x00128134
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x00129F36 File Offset: 0x00128136
		public void Dump()
		{
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00129F38 File Offset: 0x00128138
		public void Dump(Stream sout)
		{
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00129F3C File Offset: 0x0012813C
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}

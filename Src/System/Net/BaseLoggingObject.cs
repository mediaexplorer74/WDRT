using System;

namespace System.Net
{
	// Token: 0x020001C1 RID: 449
	internal class BaseLoggingObject
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x00060028 File Offset: 0x0005E228
		internal BaseLoggingObject()
		{
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00060030 File Offset: 0x0005E230
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00060032 File Offset: 0x0005E232
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00060034 File Offset: 0x0005E234
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00060036 File Offset: 0x0005E236
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00060038 File Offset: 0x0005E238
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0006003A File Offset: 0x0005E23A
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0006003C File Offset: 0x0005E23C
		internal virtual void Flush()
		{
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0006003E File Offset: 0x0005E23E
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00060040 File Offset: 0x0005E240
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00060042 File Offset: 0x0005E242
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00060044 File Offset: 0x0005E244
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00060046 File Offset: 0x0005E246
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00060048 File Offset: 0x0005E248
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}

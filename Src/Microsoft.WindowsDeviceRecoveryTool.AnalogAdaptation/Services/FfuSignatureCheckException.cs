using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public sealed class FfuSignatureCheckException : Exception
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003EE5 File Offset: 0x000020E5
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003EED File Offset: 0x000020ED
		public int ProcessExitCode { get; private set; }

		// Token: 0x0600003C RID: 60 RVA: 0x00003EB8 File Offset: 0x000020B8
		public FfuSignatureCheckException()
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003EC2 File Offset: 0x000020C2
		public FfuSignatureCheckException(string message)
			: base(message)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003EF6 File Offset: 0x000020F6
		public FfuSignatureCheckException(string message, int processExitCode)
			: base(message)
		{
			this.ProcessExitCode = processExitCode;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003ECD File Offset: 0x000020CD
		public FfuSignatureCheckException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003ED9 File Offset: 0x000020D9
		private FfuSignatureCheckException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

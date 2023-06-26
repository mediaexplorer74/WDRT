using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class ReadPhoneInformationException : Exception
	{
		// Token: 0x06000208 RID: 520 RVA: 0x000063BE File Offset: 0x000045BE
		public ReadPhoneInformationException()
			: base("Could not read ProductCode or ProductType")
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006350 File Offset: 0x00004550
		public ReadPhoneInformationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000635B File Offset: 0x0000455B
		public ReadPhoneInformationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006367 File Offset: 0x00004567
		protected ReadPhoneInformationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

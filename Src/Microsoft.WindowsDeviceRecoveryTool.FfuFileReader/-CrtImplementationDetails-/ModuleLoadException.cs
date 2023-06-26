using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020002FC RID: 764
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00012B70 File Offset: 0x00011F70
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00012B58 File Offset: 0x00011F58
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00012B44 File Offset: 0x00011F44
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x04000285 RID: 645
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}

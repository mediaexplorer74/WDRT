using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020002FF RID: 767
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00012FEC File Offset: 0x000123EC
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00012FD8 File Offset: 0x000123D8
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}

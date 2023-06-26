using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace System.Data.Services.Client
{
	// Token: 0x020000FD RID: 253
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class DataServiceQueryException : InvalidOperationException
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x000233F9 File Offset: 0x000215F9
		public DataServiceQueryException()
			: base(Strings.DataServiceException_GeneralError)
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00023406 File Offset: 0x00021606
		public DataServiceQueryException(string message)
			: base(message)
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002340F File Offset: 0x0002160F
		public DataServiceQueryException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00023419 File Offset: 0x00021619
		public DataServiceQueryException(string message, Exception innerException, QueryOperationResponse response)
			: base(message, innerException)
		{
			this.response = response;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002342A File Offset: 0x0002162A
		protected DataServiceQueryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00023434 File Offset: 0x00021634
		public QueryOperationResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040004EC RID: 1260
		[NonSerialized]
		private readonly QueryOperationResponse response;
	}
}

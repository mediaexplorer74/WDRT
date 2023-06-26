using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace System.Data.Services.Client
{
	// Token: 0x020000FF RID: 255
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class DataServiceRequestException : InvalidOperationException
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x000234EF File Offset: 0x000216EF
		public DataServiceRequestException()
			: base(Strings.DataServiceException_GeneralError)
		{
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000234FC File Offset: 0x000216FC
		public DataServiceRequestException(string message)
			: base(message)
		{
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00023505 File Offset: 0x00021705
		public DataServiceRequestException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002350F File Offset: 0x0002170F
		public DataServiceRequestException(string message, Exception innerException, DataServiceResponse response)
			: base(message, innerException)
		{
			this.response = response;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00023520 File Offset: 0x00021720
		protected DataServiceRequestException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0002352A File Offset: 0x0002172A
		public DataServiceResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040004EE RID: 1262
		[NonSerialized]
		private readonly DataServiceResponse response;
	}
}

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace System.Data.Services.Client
{
	// Token: 0x02000108 RID: 264
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class DataServiceClientException : InvalidOperationException
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x00024304 File Offset: 0x00022504
		public DataServiceClientException()
			: this(Strings.DataServiceException_GeneralError)
		{
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00024311 File Offset: 0x00022511
		public DataServiceClientException(string message)
			: this(message, null)
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002431B File Offset: 0x0002251B
		public DataServiceClientException(string message, Exception innerException)
			: this(message, innerException, 500)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002432A File Offset: 0x0002252A
		public DataServiceClientException(string message, int statusCode)
			: this(message, null, statusCode)
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00024348 File Offset: 0x00022548
		public DataServiceClientException(string message, Exception innerException, int statusCode)
			: base(message, innerException)
		{
			this.state.StatusCode = statusCode;
			base.SerializeObjectState += delegate(object sender, SafeSerializationEventArgs e)
			{
				e.AddSerializedState(this.state);
			};
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00024382 File Offset: 0x00022582
		public int StatusCode
		{
			get
			{
				return this.state.StatusCode;
			}
		}

		// Token: 0x0400050D RID: 1293
		[NonSerialized]
		private DataServiceClientException.DataServiceClientExceptionSerializationState state;

		// Token: 0x02000109 RID: 265
		[Serializable]
		private struct DataServiceClientExceptionSerializationState : ISafeSerializationData
		{
			// Token: 0x170001FF RID: 511
			// (get) Token: 0x060008AE RID: 2222 RVA: 0x0002438F File Offset: 0x0002258F
			// (set) Token: 0x060008AF RID: 2223 RVA: 0x00024397 File Offset: 0x00022597
			public int StatusCode { get; set; }

			// Token: 0x060008B0 RID: 2224 RVA: 0x000243A0 File Offset: 0x000225A0
			void ISafeSerializationData.CompleteDeserialization(object deserialized)
			{
				DataServiceClientException ex = (DataServiceClientException)deserialized;
				ex.state = this;
			}
		}
	}
}

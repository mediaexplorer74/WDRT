using System;
using System.Runtime.Serialization;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class DataServiceTransportException : InvalidOperationException
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public DataServiceTransportException(IODataResponseMessage response, Exception innerException)
			: base(innerException.Message, innerException)
		{
			Util.CheckArgumentNull<Exception>(innerException, "innerException");
			this.state.ResponseMessage = response;
			base.SerializeObjectState += delegate(object sender, SafeSerializationEventArgs e)
			{
				e.AddSerializedState(this.state);
			};
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000E20B File Offset: 0x0000C40B
		public IODataResponseMessage Response
		{
			get
			{
				return this.state.ResponseMessage;
			}
		}

		// Token: 0x04000286 RID: 646
		[NonSerialized]
		private DataServiceTransportException.DataServiceWebExceptionSerializationState state;

		// Token: 0x02000060 RID: 96
		[Serializable]
		private struct DataServiceWebExceptionSerializationState : ISafeSerializationData
		{
			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E218 File Offset: 0x0000C418
			// (set) Token: 0x0600032B RID: 811 RVA: 0x0000E220 File Offset: 0x0000C420
			public IODataResponseMessage ResponseMessage { get; set; }

			// Token: 0x0600032C RID: 812 RVA: 0x0000E22C File Offset: 0x0000C42C
			void ISafeSerializationData.CompleteDeserialization(object deserialized)
			{
				DataServiceTransportException ex = (DataServiceTransportException)deserialized;
				ex.state = this;
			}
		}
	}
}

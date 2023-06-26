using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023E RID: 574
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataErrorException : ODataException
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x000451E3 File Offset: 0x000433E3
		public ODataErrorException()
			: this(Strings.ODataErrorException_GeneralError)
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000451F0 File Offset: 0x000433F0
		public ODataErrorException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000451FA File Offset: 0x000433FA
		public ODataErrorException(string message, Exception innerException)
			: this(message, innerException, new ODataError())
		{
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00045209 File Offset: 0x00043409
		public ODataErrorException(ODataError error)
			: this(Strings.ODataErrorException_GeneralError, null, error)
		{
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00045218 File Offset: 0x00043418
		public ODataErrorException(string message, ODataError error)
			: this(message, null, error)
		{
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00045238 File Offset: 0x00043438
		public ODataErrorException(string message, Exception innerException, ODataError error)
			: base(message, innerException)
		{
			this.state.ODataError = error;
			base.SerializeObjectState += delegate(object exception, SafeSerializationEventArgs eventArgs)
			{
				eventArgs.AddSerializedState(this.state);
			};
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00045272 File Offset: 0x00043472
		public ODataError Error
		{
			get
			{
				return this.state.ODataError;
			}
		}

		// Token: 0x040006A4 RID: 1700
		[NonSerialized]
		private ODataErrorException.ODataErrorExceptionSafeSerializationState state;

		// Token: 0x0200023F RID: 575
		[Serializable]
		private struct ODataErrorExceptionSafeSerializationState : ISafeSerializationData
		{
			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004527F File Offset: 0x0004347F
			// (set) Token: 0x06001264 RID: 4708 RVA: 0x00045287 File Offset: 0x00043487
			public ODataError ODataError { get; set; }

			// Token: 0x06001265 RID: 4709 RVA: 0x00045290 File Offset: 0x00043490
			void ISafeSerializationData.CompleteDeserialization(object obj)
			{
				ODataErrorException ex = obj as ODataErrorException;
				ex.state = this;
			}
		}
	}
}

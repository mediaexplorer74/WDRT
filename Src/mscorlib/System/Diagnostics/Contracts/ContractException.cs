using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000416 RID: 1046
	[Serializable]
	internal sealed class ContractException : Exception
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000C83BB File Offset: 0x000C65BB
		public ContractFailureKind Kind
		{
			get
			{
				return this._Kind;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000C83C3 File Offset: 0x000C65C3
		public string Failure
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000C83CB File Offset: 0x000C65CB
		public string UserMessage
		{
			get
			{
				return this._UserMessage;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000C83D3 File Offset: 0x000C65D3
		public string Condition
		{
			get
			{
				return this._Condition;
			}
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000C83DB File Offset: 0x000C65DB
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000C83EE File Offset: 0x000C65EE
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
			: base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._Kind = kind;
			this._UserMessage = userMessage;
			this._Condition = condition;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000C841A File Offset: 0x000C661A
		private ContractException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._Kind = (ContractFailureKind)info.GetInt32("Kind");
			this._UserMessage = info.GetString("UserMessage");
			this._Condition = info.GetString("Condition");
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000C8458 File Offset: 0x000C6658
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._Kind);
			info.AddValue("UserMessage", this._UserMessage);
			info.AddValue("Condition", this._Condition);
		}

		// Token: 0x04001726 RID: 5926
		private readonly ContractFailureKind _Kind;

		// Token: 0x04001727 RID: 5927
		private readonly string _UserMessage;

		// Token: 0x04001728 RID: 5928
		private readonly string _Condition;
	}
}

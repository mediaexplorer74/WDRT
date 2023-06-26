using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.IO.Compression
{
	// Token: 0x02000425 RID: 1061
	[Serializable]
	internal class ZLibException : IOException, ISerializable
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x000B6531 File Offset: 0x000B4731
		public ZLibException(string message, string zlibErrorContext, int zlibErrorCode, string zlibErrorMessage)
			: base(message)
		{
			this.Init(zlibErrorContext, (ZLibNative.ErrorCode)zlibErrorCode, zlibErrorMessage);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000B6544 File Offset: 0x000B4744
		public ZLibException()
		{
			this.Init();
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000B6552 File Offset: 0x000B4752
		public ZLibException(string message)
			: base(message)
		{
			this.Init();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000B6561 File Offset: 0x000B4761
		public ZLibException(string message, Exception inner)
			: base(message, inner)
		{
			this.Init();
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000B6574 File Offset: 0x000B4774
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected ZLibException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			string @string = info.GetString("zlibErrorContext");
			ZLibNative.ErrorCode @int = (ZLibNative.ErrorCode)info.GetInt32("zlibErrorCode");
			string string2 = info.GetString("zlibErrorMessage");
			this.Init(@string, @int, string2);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000B65B6 File Offset: 0x000B47B6
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
			si.AddValue("zlibErrorContext", this.zlibErrorContext);
			si.AddValue("zlibErrorCode", (int)this.zlibErrorCode);
			si.AddValue("zlibErrorMessage", this.zlibErrorMessage);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000B65F3 File Offset: 0x000B47F3
		private void Init()
		{
			this.Init("", ZLibNative.ErrorCode.Ok, "");
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000B6606 File Offset: 0x000B4806
		private void Init(string zlibErrorContext, ZLibNative.ErrorCode zlibErrorCode, string zlibErrorMessage)
		{
			this.zlibErrorContext = zlibErrorContext;
			this.zlibErrorCode = zlibErrorCode;
			this.zlibErrorMessage = zlibErrorMessage;
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000B661D File Offset: 0x000B481D
		public string ZLibContext
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.zlibErrorContext;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x000B6625 File Offset: 0x000B4825
		public int ZLibErrorCode
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return (int)this.zlibErrorCode;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000B662D File Offset: 0x000B482D
		public string ZLibErrorMessage
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.zlibErrorMessage;
			}
		}

		// Token: 0x04002177 RID: 8567
		private string zlibErrorContext;

		// Token: 0x04002178 RID: 8568
		private string zlibErrorMessage;

		// Token: 0x04002179 RID: 8569
		private ZLibNative.ErrorCode zlibErrorCode;
	}
}

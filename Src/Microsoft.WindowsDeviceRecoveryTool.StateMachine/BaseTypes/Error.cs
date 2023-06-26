using System;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000014 RID: 20
	public class Error : EventArgs<Exception>
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000038A1 File Offset: 0x00001AA1
		public Error(Exception ex)
			: base(ex)
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000038AC File Offset: 0x00001AAC
		public Type ExceptionType
		{
			get
			{
				return base.Value.GetType();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000038C9 File Offset: 0x00001AC9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000038D1 File Offset: 0x00001AD1
		public string Message { get; set; }
	}
}

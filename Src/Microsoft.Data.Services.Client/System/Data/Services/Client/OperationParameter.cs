using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200002B RID: 43
	public abstract class OperationParameter
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000800A File Offset: 0x0000620A
		protected OperationParameter(string name, object value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Strings.Context_MissingOperationParameterName);
			}
			this.parameterName = name;
			this.parameterValue = value;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00008033 File Offset: 0x00006233
		public string Name
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000803B File Offset: 0x0000623B
		public object Value
		{
			get
			{
				return this.parameterValue;
			}
		}

		// Token: 0x040001DE RID: 478
		private string parameterName;

		// Token: 0x040001DF RID: 479
		private object parameterValue;
	}
}

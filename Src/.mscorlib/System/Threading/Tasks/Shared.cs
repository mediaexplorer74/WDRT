using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200055A RID: 1370
	internal class Shared<T>
	{
		// Token: 0x06004088 RID: 16520 RVA: 0x000F1D91 File Offset: 0x000EFF91
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x04001AF5 RID: 6901
		internal T Value;
	}
}

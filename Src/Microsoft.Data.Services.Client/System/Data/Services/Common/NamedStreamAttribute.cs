using System;

namespace System.Data.Services.Common
{
	// Token: 0x020000FC RID: 252
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public sealed class NamedStreamAttribute : Attribute
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x000233D9 File Offset: 0x000215D9
		public NamedStreamAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000233E8 File Offset: 0x000215E8
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x000233F0 File Offset: 0x000215F0
		public string Name { get; private set; }
	}
}

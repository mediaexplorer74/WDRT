using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0E RID: 2574
	[Guid("30DA92C0-23E8-42A0-AE7C-734A0E5D2782")]
	[ComImport]
	internal interface ICustomProperty
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060065BD RID: 26045
		Type Type { get; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060065BE RID: 26046
		string Name { get; }

		// Token: 0x060065BF RID: 26047
		object GetValue(object target);

		// Token: 0x060065C0 RID: 26048
		void SetValue(object target, object value);

		// Token: 0x060065C1 RID: 26049
		object GetValue(object target, object indexValue);

		// Token: 0x060065C2 RID: 26050
		void SetValue(object target, object value, object indexValue);

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060065C3 RID: 26051
		bool CanWrite { get; }

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060065C4 RID: 26052
		bool CanRead { get; }
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the access modes for a dynamic assembly.</summary>
	// Token: 0x0200062C RID: 1580
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		/// <summary>The dynamic assembly can be executed, but not saved.</summary>
		// Token: 0x04001E85 RID: 7813
		Run = 1,
		/// <summary>The dynamic assembly can be saved, but not executed.</summary>
		// Token: 0x04001E86 RID: 7814
		Save = 2,
		/// <summary>The dynamic assembly can be executed and saved.</summary>
		// Token: 0x04001E87 RID: 7815
		RunAndSave = 3,
		/// <summary>The dynamic assembly is loaded into the reflection-only context, and cannot be executed.</summary>
		// Token: 0x04001E88 RID: 7816
		ReflectionOnly = 6,
		/// <summary>The dynamic assembly will be automatically unloaded and its memory reclaimed, when it's no longer accessible.</summary>
		// Token: 0x04001E89 RID: 7817
		RunAndCollect = 9
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Holds the public GUIDs for language types to be used with the symbol store.</summary>
	// Token: 0x02000405 RID: 1029
	[ComVisible(true)]
	public class SymLanguageType
	{
		/// <summary>Specifies the GUID of the C language type to be used with the symbol store.</summary>
		// Token: 0x04001702 RID: 5890
		public static readonly Guid C = new Guid(1671464724, -969, 4562, 144, 76, 0, 192, 79, 163, 2, 161);

		/// <summary>Specifies the GUID of the C++ language type to be used with the symbol store.</summary>
		// Token: 0x04001703 RID: 5891
		public static readonly Guid CPlusPlus = new Guid(974311607, -15764, 4560, 180, 66, 0, 160, 36, 74, 29, 210);

		/// <summary>Specifies the GUID of the C# language type to be used with the symbol store.</summary>
		// Token: 0x04001704 RID: 5892
		public static readonly Guid CSharp = new Guid(1062298360, 1990, 4563, 144, 83, 0, 192, 79, 163, 2, 161);

		/// <summary>Specifies the GUID of the Basic language type to be used with the symbol store.</summary>
		// Token: 0x04001705 RID: 5893
		public static readonly Guid Basic = new Guid(974311608, -15764, 4560, 180, 66, 0, 160, 36, 74, 29, 210);

		/// <summary>Specifies the GUID of the Java language type to be used with the symbol store.</summary>
		// Token: 0x04001706 RID: 5894
		public static readonly Guid Java = new Guid(974311604, -15764, 4560, 180, 66, 0, 160, 36, 74, 29, 210);

		/// <summary>Specifies the GUID of the Cobol language type to be used with the symbol store.</summary>
		// Token: 0x04001707 RID: 5895
		public static readonly Guid Cobol = new Guid(-1358664495, -12063, 4562, 151, 124, 0, 160, 201, 180, 213, 12);

		/// <summary>Specifies the GUID of the Pascal language type to be used with the symbol store.</summary>
		// Token: 0x04001708 RID: 5896
		public static readonly Guid Pascal = new Guid(-1358664494, -12063, 4562, 151, 124, 0, 160, 201, 180, 213, 12);

		/// <summary>Specifies the GUID of the ILAssembly language type to be used with the symbol store.</summary>
		// Token: 0x04001709 RID: 5897
		public static readonly Guid ILAssembly = new Guid(-1358664493, -12063, 4562, 151, 124, 0, 160, 201, 180, 213, 12);

		/// <summary>Specifies the GUID of the JScript language type to be used with the symbol store.</summary>
		// Token: 0x0400170A RID: 5898
		public static readonly Guid JScript = new Guid(974311606, -15764, 4560, 180, 66, 0, 160, 36, 74, 29, 210);

		/// <summary>Specifies the GUID of the SMC language type to be used with the symbol store.</summary>
		// Token: 0x0400170B RID: 5899
		public static readonly Guid SMC = new Guid(228302715, 26129, 4563, 189, 42, 0, 0, 248, 8, 73, 189);

		/// <summary>Specifies the GUID of the C++ language type to be used with the symbol store.</summary>
		// Token: 0x0400170C RID: 5900
		public static readonly Guid MCPlusPlus = new Guid(1261829608, 1990, 4563, 144, 83, 0, 192, 79, 163, 2, 161);
	}
}

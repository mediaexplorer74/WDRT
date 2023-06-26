using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Defines member attribute identifiers for class members.</summary>
	// Token: 0x0200066A RID: 1642
	[ComVisible(true)]
	[Serializable]
	public enum MemberAttributes
	{
		/// <summary>An abstract member.</summary>
		// Token: 0x04002C3B RID: 11323
		Abstract = 1,
		/// <summary>A member that cannot be overridden in a derived class.</summary>
		// Token: 0x04002C3C RID: 11324
		Final,
		/// <summary>A static member. In Visual Basic, this is equivalent to the <see langword="Shared" /> keyword.</summary>
		// Token: 0x04002C3D RID: 11325
		Static,
		/// <summary>A member that overrides a base class member.</summary>
		// Token: 0x04002C3E RID: 11326
		Override,
		/// <summary>A constant member.</summary>
		// Token: 0x04002C3F RID: 11327
		Const,
		/// <summary>A new member.</summary>
		// Token: 0x04002C40 RID: 11328
		New = 16,
		/// <summary>An overloaded member. Some languages, such as Visual Basic, require overloaded members to be explicitly indicated.</summary>
		// Token: 0x04002C41 RID: 11329
		Overloaded = 256,
		/// <summary>A member that is accessible to any class within the same assembly.</summary>
		// Token: 0x04002C42 RID: 11330
		Assembly = 4096,
		/// <summary>A member that is accessible within its class, and derived classes in the same assembly.</summary>
		// Token: 0x04002C43 RID: 11331
		FamilyAndAssembly = 8192,
		/// <summary>A member that is accessible within the family of its class and derived classes.</summary>
		// Token: 0x04002C44 RID: 11332
		Family = 12288,
		/// <summary>A member that is accessible within its class, its derived classes in any assembly, and any class in the same assembly.</summary>
		// Token: 0x04002C45 RID: 11333
		FamilyOrAssembly = 16384,
		/// <summary>A private member.</summary>
		// Token: 0x04002C46 RID: 11334
		Private = 20480,
		/// <summary>A public member.</summary>
		// Token: 0x04002C47 RID: 11335
		Public = 24576,
		/// <summary>An access mask.</summary>
		// Token: 0x04002C48 RID: 11336
		AccessMask = 61440,
		/// <summary>A scope mask.</summary>
		// Token: 0x04002C49 RID: 11337
		ScopeMask = 15,
		/// <summary>A VTable mask.</summary>
		// Token: 0x04002C4A RID: 11338
		VTableMask = 240
	}
}

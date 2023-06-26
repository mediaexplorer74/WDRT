using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the presence of object types for Access Control Entries (ACEs).</summary>
	// Token: 0x02000208 RID: 520
	[Flags]
	public enum ObjectAceFlags
	{
		/// <summary>No object types are present.</summary>
		// Token: 0x04000AFA RID: 2810
		None = 0,
		/// <summary>The type of object that is associated with the ACE is present.</summary>
		// Token: 0x04000AFB RID: 2811
		ObjectAceTypePresent = 1,
		/// <summary>The type of object that can inherit the ACE.</summary>
		// Token: 0x04000AFC RID: 2812
		InheritedObjectAceTypePresent = 2
	}
}

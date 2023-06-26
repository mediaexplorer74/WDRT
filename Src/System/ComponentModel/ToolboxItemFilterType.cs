using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x020005B0 RID: 1456
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x04002A86 RID: 10886
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string.</summary>
		// Token: 0x04002A87 RID: 10887
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed.</summary>
		// Token: 0x04002A88 RID: 10888
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled.</summary>
		// Token: 0x04002A89 RID: 10889
		Require
	}
}

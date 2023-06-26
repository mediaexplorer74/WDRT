﻿using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the attributes for a manifest resource.</summary>
	// Token: 0x0200061D RID: 1565
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ResourceAttributes
	{
		/// <summary>A mask used to retrieve public manifest resources.</summary>
		// Token: 0x04001E22 RID: 7714
		Public = 1,
		/// <summary>A mask used to retrieve private manifest resources.</summary>
		// Token: 0x04001E23 RID: 7715
		Private = 2
	}
}

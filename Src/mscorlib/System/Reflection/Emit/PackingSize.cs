using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies one of two factors that determine the memory alignment of fields when a type is marshaled.</summary>
	// Token: 0x02000660 RID: 1632
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum PackingSize
	{
		/// <summary>The packing size is not specified.</summary>
		// Token: 0x040021B1 RID: 8625
		[__DynamicallyInvokable]
		Unspecified,
		/// <summary>The packing size is 1 byte.</summary>
		// Token: 0x040021B2 RID: 8626
		[__DynamicallyInvokable]
		Size1,
		/// <summary>The packing size is 2 bytes.</summary>
		// Token: 0x040021B3 RID: 8627
		[__DynamicallyInvokable]
		Size2,
		/// <summary>The packing size is 4 bytes.</summary>
		// Token: 0x040021B4 RID: 8628
		[__DynamicallyInvokable]
		Size4 = 4,
		/// <summary>The packing size is 8 bytes.</summary>
		// Token: 0x040021B5 RID: 8629
		[__DynamicallyInvokable]
		Size8 = 8,
		/// <summary>The packing size is 16 bytes.</summary>
		// Token: 0x040021B6 RID: 8630
		[__DynamicallyInvokable]
		Size16 = 16,
		/// <summary>The packing size is 32 bytes.</summary>
		// Token: 0x040021B7 RID: 8631
		[__DynamicallyInvokable]
		Size32 = 32,
		/// <summary>The packing size is 64 bytes.</summary>
		// Token: 0x040021B8 RID: 8632
		[__DynamicallyInvokable]
		Size64 = 64,
		/// <summary>The packing size is 128 bytes.</summary>
		// Token: 0x040021B9 RID: 8633
		[__DynamicallyInvokable]
		Size128 = 128
	}
}

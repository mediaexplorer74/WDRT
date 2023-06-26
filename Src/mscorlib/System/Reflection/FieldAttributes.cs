using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags that describe the attributes of a field.</summary>
	// Token: 0x020005E2 RID: 1506
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FieldAttributes
	{
		/// <summary>Specifies the access level of a given field.</summary>
		// Token: 0x04001CAD RID: 7341
		[__DynamicallyInvokable]
		FieldAccessMask = 7,
		/// <summary>Specifies that the field cannot be referenced.</summary>
		// Token: 0x04001CAE RID: 7342
		[__DynamicallyInvokable]
		PrivateScope = 0,
		/// <summary>Specifies that the field is accessible only by the parent type.</summary>
		// Token: 0x04001CAF RID: 7343
		[__DynamicallyInvokable]
		Private = 1,
		/// <summary>Specifies that the field is accessible only by subtypes in this assembly.</summary>
		// Token: 0x04001CB0 RID: 7344
		[__DynamicallyInvokable]
		FamANDAssem = 2,
		/// <summary>Specifies that the field is accessible throughout the assembly.</summary>
		// Token: 0x04001CB1 RID: 7345
		[__DynamicallyInvokable]
		Assembly = 3,
		/// <summary>Specifies that the field is accessible only by type and subtypes.</summary>
		// Token: 0x04001CB2 RID: 7346
		[__DynamicallyInvokable]
		Family = 4,
		/// <summary>Specifies that the field is accessible by subtypes anywhere, as well as throughout this assembly.</summary>
		// Token: 0x04001CB3 RID: 7347
		[__DynamicallyInvokable]
		FamORAssem = 5,
		/// <summary>Specifies that the field is accessible by any member for whom this scope is visible.</summary>
		// Token: 0x04001CB4 RID: 7348
		[__DynamicallyInvokable]
		Public = 6,
		/// <summary>Specifies that the field represents the defined type, or else it is per-instance.</summary>
		// Token: 0x04001CB5 RID: 7349
		[__DynamicallyInvokable]
		Static = 16,
		/// <summary>Specifies that the field is initialized only, and can be set only in the body of a constructor.</summary>
		// Token: 0x04001CB6 RID: 7350
		[__DynamicallyInvokable]
		InitOnly = 32,
		/// <summary>Specifies that the field's value is a compile-time (static or early bound) constant. Any attempt to set it throws a <see cref="T:System.FieldAccessException" />.</summary>
		// Token: 0x04001CB7 RID: 7351
		[__DynamicallyInvokable]
		Literal = 64,
		/// <summary>Specifies that the field does not have to be serialized when the type is remoted.</summary>
		// Token: 0x04001CB8 RID: 7352
		[__DynamicallyInvokable]
		NotSerialized = 128,
		/// <summary>Specifies a special method, with the name describing how the method is special.</summary>
		// Token: 0x04001CB9 RID: 7353
		[__DynamicallyInvokable]
		SpecialName = 512,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001CBA RID: 7354
		[__DynamicallyInvokable]
		PinvokeImpl = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04001CBB RID: 7355
		ReservedMask = 38144,
		/// <summary>Specifies that the common language runtime (metadata internal APIs) should check the name encoding.</summary>
		// Token: 0x04001CBC RID: 7356
		[__DynamicallyInvokable]
		RTSpecialName = 1024,
		/// <summary>Specifies that the field has marshaling information.</summary>
		// Token: 0x04001CBD RID: 7357
		[__DynamicallyInvokable]
		HasFieldMarshal = 4096,
		/// <summary>Specifies that the field has a default value.</summary>
		// Token: 0x04001CBE RID: 7358
		[__DynamicallyInvokable]
		HasDefault = 32768,
		/// <summary>Specifies that the field has a relative virtual address (RVA). The RVA is the location of the method body in the current image, as an address relative to the start of the image file in which it is located.</summary>
		// Token: 0x04001CBF RID: 7359
		[__DynamicallyInvokable]
		HasFieldRVA = 256
	}
}

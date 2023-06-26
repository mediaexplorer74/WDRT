using System;

namespace System.Reflection
{
	/// <summary>Describes the constraints on a generic type parameter of a generic type or method.</summary>
	// Token: 0x020005E7 RID: 1511
	[Flags]
	[__DynamicallyInvokable]
	public enum GenericParameterAttributes
	{
		/// <summary>There are no special flags.</summary>
		// Token: 0x04001CCE RID: 7374
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Selects the combination of all variance flags. This value is the result of using logical OR to combine the following flags: <see cref="F:System.Reflection.GenericParameterAttributes.Contravariant" /> and <see cref="F:System.Reflection.GenericParameterAttributes.Covariant" />.</summary>
		// Token: 0x04001CCF RID: 7375
		[__DynamicallyInvokable]
		VarianceMask = 3,
		/// <summary>The generic type parameter is covariant. A covariant type parameter can appear as the result type of a method, the type of a read-only field, a declared base type, or an implemented interface.</summary>
		// Token: 0x04001CD0 RID: 7376
		[__DynamicallyInvokable]
		Covariant = 1,
		/// <summary>The generic type parameter is contravariant. A contravariant type parameter can appear as a parameter type in method signatures.</summary>
		// Token: 0x04001CD1 RID: 7377
		[__DynamicallyInvokable]
		Contravariant = 2,
		/// <summary>Selects the combination of all special constraint flags. This value is the result of using logical OR to combine the following flags: <see cref="F:System.Reflection.GenericParameterAttributes.DefaultConstructorConstraint" />, <see cref="F:System.Reflection.GenericParameterAttributes.ReferenceTypeConstraint" />, and <see cref="F:System.Reflection.GenericParameterAttributes.NotNullableValueTypeConstraint" />.</summary>
		// Token: 0x04001CD2 RID: 7378
		[__DynamicallyInvokable]
		SpecialConstraintMask = 28,
		/// <summary>A type can be substituted for the generic type parameter only if it is a reference type.</summary>
		// Token: 0x04001CD3 RID: 7379
		[__DynamicallyInvokable]
		ReferenceTypeConstraint = 4,
		/// <summary>A type can be substituted for the generic type parameter only if it is a value type and is not nullable.</summary>
		// Token: 0x04001CD4 RID: 7380
		[__DynamicallyInvokable]
		NotNullableValueTypeConstraint = 8,
		/// <summary>A type can be substituted for the generic type parameter only if it has a parameterless constructor.</summary>
		// Token: 0x04001CD5 RID: 7381
		[__DynamicallyInvokable]
		DefaultConstructorConstraint = 16
	}
}

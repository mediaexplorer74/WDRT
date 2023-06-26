using System;

namespace System.Reflection
{
	/// <summary>Represents a context that can provide reflection objects.</summary>
	// Token: 0x0200061B RID: 1563
	[__DynamicallyInvokable]
	public abstract class ReflectionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionContext" /> class.</summary>
		// Token: 0x060048B5 RID: 18613 RVA: 0x00108C7A File Offset: 0x00106E7A
		[__DynamicallyInvokable]
		protected ReflectionContext()
		{
		}

		/// <summary>Gets the representation, in this reflection context, of an assembly that is represented by an object from another reflection context.</summary>
		/// <param name="assembly">The external representation of the assembly to represent in this context.</param>
		/// <returns>The representation of the assembly in this reflection context.</returns>
		// Token: 0x060048B6 RID: 18614
		[__DynamicallyInvokable]
		public abstract Assembly MapAssembly(Assembly assembly);

		/// <summary>Gets the representation, in this reflection context, of a type represented by an object from another reflection context.</summary>
		/// <param name="type">The external representation of the type to represent in this context.</param>
		/// <returns>The representation of the type in this reflection context.</returns>
		// Token: 0x060048B7 RID: 18615
		[__DynamicallyInvokable]
		public abstract TypeInfo MapType(TypeInfo type);

		/// <summary>Gets the representation of the type of the specified object in this reflection context.</summary>
		/// <param name="value">The object to represent.</param>
		/// <returns>An object that represents the type of the specified object.</returns>
		// Token: 0x060048B8 RID: 18616 RVA: 0x00108C82 File Offset: 0x00106E82
		[__DynamicallyInvokable]
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}

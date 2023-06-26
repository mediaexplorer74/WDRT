using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides support for type equivalence.</summary>
	// Token: 0x0200090E RID: 2318
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class TypeIdentifierAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> class.</summary>
		// Token: 0x06006007 RID: 24583 RVA: 0x0014CD7D File Offset: 0x0014AF7D
		[__DynamicallyInvokable]
		public TypeIdentifierAttribute()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> class with the specified scope and identifier.</summary>
		/// <param name="scope">The first type equivalence string.</param>
		/// <param name="identifier">The second type equivalence string.</param>
		// Token: 0x06006008 RID: 24584 RVA: 0x0014CD85 File Offset: 0x0014AF85
		[__DynamicallyInvokable]
		public TypeIdentifierAttribute(string scope, string identifier)
		{
			this.Scope_ = scope;
			this.Identifier_ = identifier;
		}

		/// <summary>Gets the value of the <paramref name="scope" /> parameter that was passed to the <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> constructor.</summary>
		/// <returns>The value of the constructor's <paramref name="scope" /> parameter.</returns>
		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x0014CD9B File Offset: 0x0014AF9B
		[__DynamicallyInvokable]
		public string Scope
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Scope_;
			}
		}

		/// <summary>Gets the value of the <paramref name="identifier" /> parameter that was passed to the <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> constructor.</summary>
		/// <returns>The value of the constructor's <paramref name="identifier" /> parameter.</returns>
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600600A RID: 24586 RVA: 0x0014CDA3 File Offset: 0x0014AFA3
		[__DynamicallyInvokable]
		public string Identifier
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Identifier_;
			}
		}

		// Token: 0x04002A6A RID: 10858
		internal string Scope_;

		// Token: 0x04002A6B RID: 10859
		internal string Identifier_;
	}
}

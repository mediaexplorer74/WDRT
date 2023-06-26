using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a source <see cref="T:System.Type" /> in another assembly.</summary>
	// Token: 0x020008DF RID: 2271
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		// Token: 0x06005DF2 RID: 24050 RVA: 0x0014B164 File Offset: 0x00149364
		private TypeForwardedFromAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" /> class.</summary>
		/// <param name="assemblyFullName">The source <see cref="T:System.Type" /> in another assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFullName" /> is <see langword="null" /> or empty.</exception>
		// Token: 0x06005DF3 RID: 24051 RVA: 0x0014B16C File Offset: 0x0014936C
		[__DynamicallyInvokable]
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.assemblyFullName = assemblyFullName;
		}

		/// <summary>Gets the assembly-qualified name of the source type.</summary>
		/// <returns>The assembly-qualified name of the source type.</returns>
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x0014B18E File Offset: 0x0014938E
		[__DynamicallyInvokable]
		public string AssemblyFullName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.assemblyFullName;
			}
		}

		// Token: 0x04002A42 RID: 10818
		private string assemblyFullName;
	}
}

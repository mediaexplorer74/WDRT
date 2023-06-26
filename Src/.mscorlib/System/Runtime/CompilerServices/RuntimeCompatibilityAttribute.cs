using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object. This class cannot be inherited.</summary>
	// Token: 0x020008E1 RID: 2273
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RuntimeCompatibilityAttribute" /> class.</summary>
		// Token: 0x06005DF8 RID: 24056 RVA: 0x0014B1B5 File Offset: 0x001493B5
		[__DynamicallyInvokable]
		public RuntimeCompatibilityAttribute()
		{
		}

		/// <summary>Gets or sets a value that indicates whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if exceptions that do not derive from the <see cref="T:System.Exception" /> class should appear wrapped with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x0014B1BD File Offset: 0x001493BD
		// (set) Token: 0x06005DFA RID: 24058 RVA: 0x0014B1C5 File Offset: 0x001493C5
		[__DynamicallyInvokable]
		public bool WrapNonExceptionThrows
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_wrapNonExceptionThrows;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_wrapNonExceptionThrows = value;
			}
		}

		// Token: 0x04002A44 RID: 10820
		private bool m_wrapNonExceptionThrows;
	}
}

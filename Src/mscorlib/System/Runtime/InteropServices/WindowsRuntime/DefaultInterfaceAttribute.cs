using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the default interface of a managed Windows Runtime class.</summary>
	// Token: 0x020009C4 RID: 2500
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class DefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute" /> class.</summary>
		/// <param name="defaultInterface">The interface type that is specified as the default interface for the class the attribute is applied to.</param>
		// Token: 0x060063D6 RID: 25558 RVA: 0x00156021 File Offset: 0x00154221
		[__DynamicallyInvokable]
		public DefaultInterfaceAttribute(Type defaultInterface)
		{
			this.m_defaultInterface = defaultInterface;
		}

		/// <summary>Gets the type of the default interface.</summary>
		/// <returns>The type of the default interface.</returns>
		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060063D7 RID: 25559 RVA: 0x00156030 File Offset: 0x00154230
		[__DynamicallyInvokable]
		public Type DefaultInterface
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultInterface;
			}
		}

		// Token: 0x04002CE2 RID: 11490
		private Type m_defaultInterface;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that an importing compiler must fully understand the semantics of a type definition, or refuse to use it.  This class cannot be inherited.</summary>
	// Token: 0x020008C0 RID: 2240
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> class.</summary>
		/// <param name="requiredContract">A type that an importing compiler must fully understand.  
		///  This parameter is not supported in the .NET Framework version 2.0 and later.</param>
		// Token: 0x06005DD2 RID: 24018 RVA: 0x0014AFE8 File Offset: 0x001491E8
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		/// <summary>Gets a type that an importing compiler must fully understand.</summary>
		/// <returns>A type that an importing compiler must fully understand.</returns>
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0014AFF7 File Offset: 0x001491F7
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04002A33 RID: 10803
		private Type requiredContract;
	}
}

using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a field should be treated as containing a fixed number of elements of the specified primitive type. This class cannot be inherited.</summary>
	// Token: 0x020008B5 RID: 2229
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class FixedBufferAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" /> class.</summary>
		/// <param name="elementType">The type of the elements contained in the buffer.</param>
		/// <param name="length">The number of elements in the buffer.</param>
		// Token: 0x06005DC1 RID: 24001 RVA: 0x0014AF1A File Offset: 0x0014911A
		[__DynamicallyInvokable]
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.elementType = elementType;
			this.length = length;
		}

		/// <summary>Gets the type of the elements contained in the fixed buffer.</summary>
		/// <returns>The type of the elements.</returns>
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x0014AF30 File Offset: 0x00149130
		[__DynamicallyInvokable]
		public Type ElementType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.elementType;
			}
		}

		/// <summary>Gets the number of elements in the fixed buffer.</summary>
		/// <returns>The number of elements in the fixed buffer.</returns>
		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x0014AF38 File Offset: 0x00149138
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.length;
			}
		}

		// Token: 0x04002A1E RID: 10782
		private Type elementType;

		// Token: 0x04002A1F RID: 10783
		private int length;
	}
}

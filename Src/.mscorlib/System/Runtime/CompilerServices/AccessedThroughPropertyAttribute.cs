using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the name of the property that accesses the attributed field.</summary>
	// Token: 0x020008A0 RID: 2208
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="AccessedThroughPropertyAttribute" /> class with the name of the property used to access the attributed field.</summary>
		/// <param name="propertyName">The name of the property used to access the attributed field.</param>
		// Token: 0x06005D8F RID: 23951 RVA: 0x0014AA8E File Offset: 0x00148C8E
		[__DynamicallyInvokable]
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property used to access the attributed field.</summary>
		/// <returns>The name of the property used to access the attributed field.</returns>
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x0014AA9D File Offset: 0x00148C9D
		[__DynamicallyInvokable]
		public string PropertyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002A17 RID: 10775
		private readonly string propertyName;
	}
}

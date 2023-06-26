using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C4 RID: 2244
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x06005DD9 RID: 24025 RVA: 0x0014B03C File Offset: 0x0014923C
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04002A3B RID: 10811
		private string typeName;
	}
}

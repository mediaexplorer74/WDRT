using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x0200062A RID: 1578
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x0600398E RID: 14734 RVA: 0x000F269C File Offset: 0x000F089C
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000F26C5 File Offset: 0x000F08C5
		public CodeExpressionCollection BaseConstructorArgs
		{
			get
			{
				return this.baseConstructorArgs;
			}
		}

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000F26CD File Offset: 0x000F08CD
		public CodeExpressionCollection ChainedConstructorArgs
		{
			get
			{
				return this.chainedConstructorArgs;
			}
		}

		// Token: 0x04002BA3 RID: 11171
		private CodeExpressionCollection baseConstructorArgs = new CodeExpressionCollection();

		// Token: 0x04002BA4 RID: 11172
		private CodeExpressionCollection chainedConstructorArgs = new CodeExpressionCollection();
	}
}

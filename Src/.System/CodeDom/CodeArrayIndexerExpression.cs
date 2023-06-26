using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an index of an array.</summary>
	// Token: 0x02000617 RID: 1559
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class.</summary>
		// Token: 0x060038FC RID: 14588 RVA: 0x000F1C3D File Offset: 0x000EFE3D
		public CodeArrayIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class using the specified target object and indexes.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the array the indexer targets.</param>
		/// <param name="indices">The index or indexes to reference.</param>
		// Token: 0x060038FD RID: 14589 RVA: 0x000F1C45 File Offset: 0x000EFE45
		public CodeArrayIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.targetObject = targetObject;
			this.indices = new CodeExpressionCollection();
			this.indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object of the array indexer.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the array being indexed.</returns>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000F1C6B File Offset: 0x000EFE6B
		// (set) Token: 0x060038FF RID: 14591 RVA: 0x000F1C73 File Offset: 0x000EFE73
		public CodeExpression TargetObject
		{
			get
			{
				return this.targetObject;
			}
			set
			{
				this.targetObject = value;
			}
		}

		/// <summary>Gets or sets the index or indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000F1C7C File Offset: 0x000EFE7C
		public CodeExpressionCollection Indices
		{
			get
			{
				if (this.indices == null)
				{
					this.indices = new CodeExpressionCollection();
				}
				return this.indices;
			}
		}

		// Token: 0x04002B70 RID: 11120
		private CodeExpression targetObject;

		// Token: 0x04002B71 RID: 11121
		private CodeExpressionCollection indices;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an indexer property of an object.</summary>
	// Token: 0x02000638 RID: 1592
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class.</summary>
		// Token: 0x060039DA RID: 14810 RVA: 0x000F2B75 File Offset: 0x000F0D75
		public CodeIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class using the specified target object and index.</summary>
		/// <param name="targetObject">The target object.</param>
		/// <param name="indices">The index or indexes of the indexer expression.</param>
		// Token: 0x060039DB RID: 14811 RVA: 0x000F2B7D File Offset: 0x000F0D7D
		public CodeIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.targetObject = targetObject;
			this.indices = new CodeExpressionCollection();
			this.indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object that can be indexed.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the indexer object.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000F2BA3 File Offset: 0x000F0DA3
		// (set) Token: 0x060039DD RID: 14813 RVA: 0x000F2BAB File Offset: 0x000F0DAB
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

		/// <summary>Gets the collection of indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x000F2BB4 File Offset: 0x000F0DB4
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

		// Token: 0x04002BB3 RID: 11187
		private CodeExpression targetObject;

		// Token: 0x04002BB4 RID: 11188
		private CodeExpressionCollection indices;
	}
}

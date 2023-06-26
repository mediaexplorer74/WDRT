using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a delegate declaration.</summary>
	// Token: 0x0200065D RID: 1629
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeDelegate : CodeTypeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		// Token: 0x06003AF9 RID: 15097 RVA: 0x000F44A4 File Offset: 0x000F26A4
		public CodeTypeDelegate()
		{
			base.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			base.TypeAttributes |= TypeAttributes.NotPublic;
			base.BaseTypes.Clear();
			base.BaseTypes.Add(new CodeTypeReference("System.Delegate"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		/// <param name="name">The name of the delegate.</param>
		// Token: 0x06003AFA RID: 15098 RVA: 0x000F4500 File Offset: 0x000F2700
		public CodeTypeDelegate(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the return type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the return type of the delegate.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000F450F File Offset: 0x000F270F
		// (set) Token: 0x06003AFC RID: 15100 RVA: 0x000F452F File Offset: 0x000F272F
		public CodeTypeReference ReturnType
		{
			get
			{
				if (this.returnType == null)
				{
					this.returnType = new CodeTypeReference("");
				}
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		/// <summary>Gets the parameters of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the parameters of the delegate.</returns>
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000F4538 File Offset: 0x000F2738
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002C19 RID: 11289
		private CodeParameterDeclarationExpressionCollection parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002C1A RID: 11290
		private CodeTypeReference returnType;
	}
}

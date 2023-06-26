using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x02000641 RID: 1601
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06003A26 RID: 14886 RVA: 0x000F3220 File Offset: 0x000F1420
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target.</param>
		/// <param name="methodName">The name of the method to call.</param>
		// Token: 0x06003A27 RID: 14887 RVA: 0x000F3228 File Offset: 0x000F1428
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object, method name, and generic type arguments.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target.</param>
		/// <param name="methodName">The name of the method to call.</param>
		/// <param name="typeParameters">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values that specify the <see cref="P:System.CodeDom.CodeMethodReferenceExpression.TypeArguments" /> for this <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</param>
		// Token: 0x06003A28 RID: 14888 RVA: 0x000F323E File Offset: 0x000F143E
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
			if (typeParameters != null && typeParameters.Length != 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000F3267 File Offset: 0x000F1467
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x000F326F File Offset: 0x000F146F
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

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000F3278 File Offset: 0x000F1478
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x000F328E File Offset: 0x000F148E
		public string MethodName
		{
			get
			{
				if (this.methodName != null)
				{
					return this.methodName;
				}
				return string.Empty;
			}
			set
			{
				this.methodName = value;
			}
		}

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000F3297 File Offset: 0x000F1497
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x04002BDA RID: 11226
		private CodeExpression targetObject;

		// Token: 0x04002BDB RID: 11227
		private string methodName;

		// Token: 0x04002BDC RID: 11228
		[OptionalField]
		private CodeTypeReferenceCollection typeArguments;
	}
}

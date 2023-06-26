using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that invokes a method.</summary>
	// Token: 0x02000640 RID: 1600
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class.</summary>
		// Token: 0x06003A20 RID: 14880 RVA: 0x000F318F File Offset: 0x000F138F
		public CodeMethodInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified method and parameters.</summary>
		/// <param name="method">A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters with which to invoke the method.</param>
		// Token: 0x06003A21 RID: 14881 RVA: 0x000F31A2 File Offset: 0x000F13A2
		public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
		{
			this.method = method;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified target object, method name, and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object with the method to invoke.</param>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters to call the method with.</param>
		// Token: 0x06003A22 RID: 14882 RVA: 0x000F31C8 File Offset: 0x000F13C8
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this.method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the method to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</returns>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000F31F4 File Offset: 0x000F13F4
		// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000F320F File Offset: 0x000F140F
		public CodeMethodReferenceExpression Method
		{
			get
			{
				if (this.method == null)
				{
					this.method = new CodeMethodReferenceExpression();
				}
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		/// <summary>Gets the parameters to invoke the method with.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to invoke the method with.</returns>
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000F3218 File Offset: 0x000F1418
		public CodeExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002BD8 RID: 11224
		private CodeMethodReferenceExpression method;

		// Token: 0x04002BD9 RID: 11225
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}

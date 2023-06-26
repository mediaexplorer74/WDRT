using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to the metadata and MSIL for the body of a method.</summary>
	// Token: 0x02000611 RID: 1553
	[ComVisible(true)]
	public class MethodBody
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.MethodBody" /> class.</summary>
		// Token: 0x0600481C RID: 18460 RVA: 0x001077FC File Offset: 0x001059FC
		protected MethodBody()
		{
		}

		/// <summary>Gets a metadata token for the signature that describes the local variables for the method in metadata.</summary>
		/// <returns>An integer that represents the metadata token.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x00107804 File Offset: 0x00105A04
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return this.m_localSignatureMetadataToken;
			}
		}

		/// <summary>Gets the list of local variables declared in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.LocalVariableInfo" /> objects that describe the local variables declared in the method body.</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x0010780C File Offset: 0x00105A0C
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.m_localVariables);
			}
		}

		/// <summary>Gets the maximum number of items on the operand stack when the method is executing.</summary>
		/// <returns>The maximum number of items on the operand stack when the method is executing.</returns>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x00107819 File Offset: 0x00105A19
		public virtual int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		/// <summary>Gets a value indicating whether local variables in the method body are initialized to the default values for their types.</summary>
		/// <returns>
		///   <see langword="true" /> if the method body contains code to initialize local variables to <see langword="null" /> for reference types, or to the zero-initialized value for value types; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x00107821 File Offset: 0x00105A21
		public virtual bool InitLocals
		{
			get
			{
				return this.m_initLocals;
			}
		}

		/// <summary>Returns the MSIL for the method body, as an array of bytes.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the MSIL for the method body.</returns>
		// Token: 0x06004821 RID: 18465 RVA: 0x00107829 File Offset: 0x00105A29
		public virtual byte[] GetILAsByteArray()
		{
			return this.m_IL;
		}

		/// <summary>Gets a list that includes all the exception-handling clauses in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.ExceptionHandlingClause" /> objects representing the exception-handling clauses in the body of the method.</returns>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00107831 File Offset: 0x00105A31
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.m_exceptionHandlingClauses);
			}
		}

		// Token: 0x04001DDE RID: 7646
		private byte[] m_IL;

		// Token: 0x04001DDF RID: 7647
		private ExceptionHandlingClause[] m_exceptionHandlingClauses;

		// Token: 0x04001DE0 RID: 7648
		private LocalVariableInfo[] m_localVariables;

		// Token: 0x04001DE1 RID: 7649
		internal MethodBase m_methodBase;

		// Token: 0x04001DE2 RID: 7650
		private int m_localSignatureMetadataToken;

		// Token: 0x04001DE3 RID: 7651
		private int m_maxStackSize;

		// Token: 0x04001DE4 RID: 7652
		private bool m_initLocals;
	}
}

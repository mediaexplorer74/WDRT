using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a local variable within a method or constructor.</summary>
	// Token: 0x02000643 RID: 1603
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ComVisible(true)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x001115EB File Offset: 0x0010F7EB
		private LocalBuilder()
		{
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x001115F3 File Offset: 0x0010F7F3
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder)
			: this(localIndex, localType, methodBuilder, false)
		{
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x001115FF File Offset: 0x0010F7FF
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder, bool isPinned)
		{
			this.m_isPinned = isPinned;
			this.m_localIndex = localIndex;
			this.m_localType = localType;
			this.m_methodBuilder = methodBuilder;
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x00111624 File Offset: 0x0010F824
		internal int GetLocalIndex()
		{
			return this.m_localIndex;
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x0011162C File Offset: 0x0010F82C
		internal MethodInfo GetMethodBuilder()
		{
			return this.m_methodBuilder;
		}

		/// <summary>Gets a value indicating whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>
		///   <see langword="true" /> if the object referred to by the local variable is pinned in memory; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x00111634 File Offset: 0x0010F834
		public override bool IsPinned
		{
			get
			{
				return this.m_isPinned;
			}
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the local variable.</returns>
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x0011163C File Offset: 0x0010F83C
		public override Type LocalType
		{
			get
			{
				return this.m_localType;
			}
		}

		/// <summary>Gets the zero-based index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x00111644 File Offset: 0x0010F844
		public override int LocalIndex
		{
			get
			{
				return this.m_localIndex;
			}
		}

		/// <summary>Sets the name of this local variable.</summary>
		/// <param name="name">The name of the local variable.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  There is no symbolic writer defined for the containing module.</exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x06004B2C RID: 19244 RVA: 0x0011164C File Offset: 0x0010F84C
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		/// <summary>Sets the name and lexical scope of this local variable.</summary>
		/// <param name="name">The name of the local variable.</param>
		/// <param name="startOffset">The beginning offset of the lexical scope of the local variable.</param>
		/// <param name="endOffset">The ending offset of the lexical scope of the local variable.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  There is no symbolic writer defined for the containing module.</exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x06004B2D RID: 19245 RVA: 0x00111658 File Offset: 0x0010F858
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)methodBuilder.Module;
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			if (moduleBuilder.GetSymWriter() == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(moduleBuilder);
			fieldSigHelper.AddArgument(this.m_localType);
			int num;
			byte[] array = fieldSigHelper.InternalGetSignature(out num);
			byte[] array2 = new byte[num - 1];
			Array.Copy(array, 1, array2, 0, num - 1);
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddLocalSymInfo(name, array2, this.m_localIndex, startOffset, endOffset);
				return;
			}
			methodBuilder.GetILGenerator().m_ScopeTree.AddLocalSymInfoToCurrentScope(name, array2, this.m_localIndex, startOffset, endOffset);
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004B2E RID: 19246 RVA: 0x0011173F File Offset: 0x0010F93F
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004B2F RID: 19247 RVA: 0x00111746 File Offset: 0x0010F946
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004B30 RID: 19248 RVA: 0x0011174D File Offset: 0x0010F94D
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004B31 RID: 19249 RVA: 0x00111754 File Offset: 0x0010F954
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001F0E RID: 7950
		private int m_localIndex;

		// Token: 0x04001F0F RID: 7951
		private Type m_localType;

		// Token: 0x04001F10 RID: 7952
		private MethodInfo m_methodBuilder;

		// Token: 0x04001F11 RID: 7953
		private bool m_isPinned;
	}
}

using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="ITypeComp" /> interface.</summary>
	// Token: 0x02000A37 RID: 2615
	[Guid("00020403-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeComp
	{
		/// <summary>Maps a name to a member of a type, or binds global variables and functions contained in a type library.</summary>
		/// <param name="szName">The name to bind.</param>
		/// <param name="lHashVal">A hash value for <paramref name="szName" /> computed by <see langword="LHashValOfNameSys" />.</param>
		/// <param name="wFlags">A flags word containing one or more of the invoke flags defined in the <see langword="INVOKEKIND" /> enumeration.</param>
		/// <param name="ppTInfo">When this method returns, contains a reference to the type description that contains the item to which it is bound, if a <see langword="FUNCDESC" /> or <see langword="VARDESC" /> was returned. This parameter is passed uninitialized.</param>
		/// <param name="pDescKind">When this method returns, contains a reference to a <see langword="DESCKIND" /> enumerator that indicates whether the name bound-to is a <see langword="VARDESC" />, <see langword="FUNCDESC" />, or <see langword="TYPECOMP" />. This parameter is passed uninitialized.</param>
		/// <param name="pBindPtr">When this method returns, contains a reference to the bound-to <see langword="VARDESC" />, <see langword="FUNCDESC" />, or <see langword="ITypeComp" /> interface. This parameter is passed uninitialized.</param>
		// Token: 0x06006686 RID: 26246
		[__DynamicallyInvokable]
		void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out ITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

		/// <summary>Binds to the type descriptions contained within a type library.</summary>
		/// <param name="szName">The name to bind.</param>
		/// <param name="lHashVal">A hash value for <paramref name="szName" /> determined by <see langword="LHashValOfNameSys" />.</param>
		/// <param name="ppTInfo">When this method returns, contains a reference to an <see langword="ITypeInfo" /> of the type to which <paramref name="szName" /> was bound. This parameter is passed uninitialized.</param>
		/// <param name="ppTComp">When this method returns, contains a reference to an <see langword="ITypeComp" /> variable. This parameter is passed uninitialized.</param>
		// Token: 0x06006687 RID: 26247
		[__DynamicallyInvokable]
		void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out ITypeInfo ppTInfo, out ITypeComp ppTComp);
	}
}

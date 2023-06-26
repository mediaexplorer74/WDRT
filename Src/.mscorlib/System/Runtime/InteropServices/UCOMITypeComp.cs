using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> instead.</summary>
	// Token: 0x0200098E RID: 2446
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeComp instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020403-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeComp
	{
		/// <summary>Maps a name to a member of a type, or binds global variables and functions contained in a type library.</summary>
		/// <param name="szName">The name to bind.</param>
		/// <param name="lHashVal">A hash value for <paramref name="szName" /> computed by <see langword="LHashValOfNameSys" />.</param>
		/// <param name="wFlags">A flags word containing one or more of the invoke flags defined in the <see langword="INVOKEKIND" /> enumeration.</param>
		/// <param name="ppTInfo">On successful return, a reference to the type description that contains the item to which it is bound, if a <see langword="FUNCDESC" /> or <see langword="VARDESC" /> was returned.</param>
		/// <param name="pDescKind">A reference to a <see langword="DESCKIND" /> enumerator that indicates whether the name bound to is a <see langword="VARDESC" />, <see langword="FUNCDESC" />, or <see langword="TYPECOMP" />.</param>
		/// <param name="pBindPtr">A reference to the bound-to <see langword="VARDESC" />, <see langword="FUNCDESC" />, or <see langword="ITypeComp" /> interface.</param>
		// Token: 0x060062F3 RID: 25331
		void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out UCOMITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

		/// <summary>Binds to the type descriptions contained within a type library.</summary>
		/// <param name="szName">The name to bind.</param>
		/// <param name="lHashVal">A hash value for <paramref name="szName" /> determined by <see langword="LHashValOfNameSys" />.</param>
		/// <param name="ppTInfo">On successful return, a reference to an <see langword="ITypeInfo" /> of the type to which <paramref name="szName" /> was bound.</param>
		/// <param name="ppTComp">On successful return, a reference to an <see langword="ITypeComp" /> variable.</param>
		// Token: 0x060062F4 RID: 25332
		void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out UCOMITypeInfo ppTInfo, out UCOMITypeComp ppTComp);
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Represents the class that describes how to marshal a field from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x02000669 RID: 1641
	[ComVisible(true)]
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class UnmanagedMarshal
	{
		/// <summary>Specifies a given type that is to be marshaled to unmanaged code.</summary>
		/// <param name="unmanagedType">The unmanaged type to which the type is to be marshaled.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06004F21 RID: 20257 RVA: 0x0011D83A File Offset: 0x0011BA3A
		public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
		{
			if (unmanagedType == UnmanagedType.ByValTStr || unmanagedType == UnmanagedType.SafeArray || unmanagedType == UnmanagedType.CustomMarshaler || unmanagedType == UnmanagedType.ByValArray || unmanagedType == UnmanagedType.LPArray)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotASimpleNativeType"));
			}
			return new UnmanagedMarshal(unmanagedType, Guid.Empty, 0, (UnmanagedType)0);
		}

		/// <summary>Specifies a string in a fixed array buffer (ByValTStr) to marshal to unmanaged code.</summary>
		/// <param name="elemCount">The number of elements in the fixed array buffer.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06004F22 RID: 20258 RVA: 0x0011D872 File Offset: 0x0011BA72
		public static UnmanagedMarshal DefineByValTStr(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValTStr, Guid.Empty, elemCount, (UnmanagedType)0);
		}

		/// <summary>Specifies a <see langword="SafeArray" /> to marshal to unmanaged code.</summary>
		/// <param name="elemType">The base type or the <see langword="UnmanagedType" /> of each element of the array.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06004F23 RID: 20259 RVA: 0x0011D882 File Offset: 0x0011BA82
		public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.SafeArray, Guid.Empty, 0, elemType);
		}

		/// <summary>Specifies a fixed-length array (ByValArray) to marshal to unmanaged code.</summary>
		/// <param name="elemCount">The number of elements in the fixed-length array.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06004F24 RID: 20260 RVA: 0x0011D892 File Offset: 0x0011BA92
		public static UnmanagedMarshal DefineByValArray(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValArray, Guid.Empty, elemCount, (UnmanagedType)0);
		}

		/// <summary>Specifies an <see langword="LPArray" /> to marshal to unmanaged code. The length of an <see langword="LPArray" /> is determined at runtime by the size of the actual marshaled array.</summary>
		/// <param name="elemType">The unmanaged type to which to marshal the array.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06004F25 RID: 20261 RVA: 0x0011D8A2 File Offset: 0x0011BAA2
		public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, Guid.Empty, 0, elemType);
		}

		/// <summary>Indicates an unmanaged type. This property is read-only.</summary>
		/// <returns>An <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> object.</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x0011D8B2 File Offset: 0x0011BAB2
		public UnmanagedType GetUnmanagedType
		{
			get
			{
				return this.m_unmanagedType;
			}
		}

		/// <summary>Gets a GUID. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a custom marshaler.</exception>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x0011D8BA File Offset: 0x0011BABA
		public Guid IIDGuid
		{
			get
			{
				if (this.m_unmanagedType == UnmanagedType.CustomMarshaler)
				{
					return this.m_guid;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_NotACustomMarshaler"));
			}
		}

		/// <summary>Gets a number element. This property is read-only.</summary>
		/// <returns>An integer indicating the element count.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not an unmanaged element count.</exception>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x0011D8DC File Offset: 0x0011BADC
		public int ElementCount
		{
			get
			{
				if (this.m_unmanagedType != UnmanagedType.ByValArray && this.m_unmanagedType != UnmanagedType.ByValTStr)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NoUnmanagedElementCount"));
				}
				return this.m_numElem;
			}
		}

		/// <summary>Gets an unmanaged base type. This property is read-only.</summary>
		/// <returns>An <see langword="UnmanagedType" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The unmanaged type is not an <see langword="LPArray" /> or a <see langword="SafeArray" />.</exception>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004F29 RID: 20265 RVA: 0x0011D908 File Offset: 0x0011BB08
		public UnmanagedType BaseType
		{
			get
			{
				if (this.m_unmanagedType != UnmanagedType.LPArray && this.m_unmanagedType != UnmanagedType.SafeArray)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NoNestedMarshal"));
				}
				return this.m_baseType;
			}
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0011D934 File Offset: 0x0011BB34
		private UnmanagedMarshal(UnmanagedType unmanagedType, Guid guid, int numElem, UnmanagedType type)
		{
			this.m_unmanagedType = unmanagedType;
			this.m_guid = guid;
			this.m_numElem = numElem;
			this.m_baseType = type;
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0011D95C File Offset: 0x0011BB5C
		internal byte[] InternalGetBytes()
		{
			if (this.m_unmanagedType == UnmanagedType.SafeArray || this.m_unmanagedType == UnmanagedType.LPArray)
			{
				int num = 2;
				byte[] array = new byte[num];
				array[0] = (byte)this.m_unmanagedType;
				array[1] = (byte)this.m_baseType;
				return array;
			}
			if (this.m_unmanagedType == UnmanagedType.ByValArray || this.m_unmanagedType == UnmanagedType.ByValTStr)
			{
				int num2 = 0;
				int num3;
				if (this.m_numElem <= 127)
				{
					num3 = 1;
				}
				else if (this.m_numElem <= 16383)
				{
					num3 = 2;
				}
				else
				{
					num3 = 4;
				}
				num3++;
				byte[] array = new byte[num3];
				array[num2++] = (byte)this.m_unmanagedType;
				if (this.m_numElem <= 127)
				{
					array[num2++] = (byte)(this.m_numElem & 255);
				}
				else if (this.m_numElem <= 16383)
				{
					array[num2++] = (byte)((this.m_numElem >> 8) | 128);
					array[num2++] = (byte)(this.m_numElem & 255);
				}
				else if (this.m_numElem <= 536870911)
				{
					array[num2++] = (byte)((this.m_numElem >> 24) | 192);
					array[num2++] = (byte)((this.m_numElem >> 16) & 255);
					array[num2++] = (byte)((this.m_numElem >> 8) & 255);
					array[num2++] = (byte)(this.m_numElem & 255);
				}
				return array;
			}
			return new byte[] { (byte)this.m_unmanagedType };
		}

		// Token: 0x040021E3 RID: 8675
		internal UnmanagedType m_unmanagedType;

		// Token: 0x040021E4 RID: 8676
		internal Guid m_guid;

		// Token: 0x040021E5 RID: 8677
		internal int m_numElem;

		// Token: 0x040021E6 RID: 8678
		internal UnmanagedType m_baseType;
	}
}

using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Lets you control the physical layout of the data fields of a class or structure in memory.</summary>
	// Token: 0x02000933 RID: 2355
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class StructLayoutAttribute : Attribute
	{
		// Token: 0x06006056 RID: 24662 RVA: 0x0014D488 File Offset: 0x0014B688
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if (!StructLayoutAttribute.IsDefined(type))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			LayoutKind layoutKind = LayoutKind.Auto;
			TypeAttributes typeAttributes = type.Attributes & TypeAttributes.LayoutMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.SequentialLayout)
				{
					if (typeAttributes == TypeAttributes.ExplicitLayout)
					{
						layoutKind = LayoutKind.Explicit;
					}
				}
				else
				{
					layoutKind = LayoutKind.Sequential;
				}
			}
			else
			{
				layoutKind = LayoutKind.Auto;
			}
			CharSet charSet = CharSet.None;
			TypeAttributes typeAttributes2 = type.Attributes & TypeAttributes.StringFormatMask;
			if (typeAttributes2 != TypeAttributes.NotPublic)
			{
				if (typeAttributes2 != TypeAttributes.UnicodeClass)
				{
					if (typeAttributes2 == TypeAttributes.AutoClass)
					{
						charSet = CharSet.Auto;
					}
				}
				else
				{
					charSet = CharSet.Unicode;
				}
			}
			else
			{
				charSet = CharSet.Ansi;
			}
			type.GetRuntimeModule().MetadataImport.GetClassLayout(type.MetadataToken, out num, out num2);
			if (num == 0)
			{
				num = 8;
			}
			return new StructLayoutAttribute(layoutKind, num, num2, charSet);
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x0014D527 File Offset: 0x0014B727
		internal static bool IsDefined(RuntimeType type)
		{
			return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x0014D544 File Offset: 0x0014B744
		internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
		{
			this._val = layoutKind;
			this.Pack = pack;
			this.Size = size;
			this.CharSet = charSet;
		}

		/// <summary>Initalizes a new instance of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.LayoutKind" /> enumeration member.</summary>
		/// <param name="layoutKind">One of the enumeration values that specifes how the class or structure should be arranged.</param>
		// Token: 0x06006059 RID: 24665 RVA: 0x0014D569 File Offset: 0x0014B769
		[__DynamicallyInvokable]
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this._val = layoutKind;
		}

		/// <summary>Initalizes a new instance of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.LayoutKind" /> enumeration member.</summary>
		/// <param name="layoutKind">A 16-bit integer that represents one of the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> values that specifes how the class or structure should be arranged.</param>
		// Token: 0x0600605A RID: 24666 RVA: 0x0014D578 File Offset: 0x0014B778
		public StructLayoutAttribute(short layoutKind)
		{
			this._val = (LayoutKind)layoutKind;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> value that specifies how the class or structure is arranged.</summary>
		/// <returns>One of the enumeration values that specifies how the class or structure is arranged.</returns>
		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x0014D587 File Offset: 0x0014B787
		[__DynamicallyInvokable]
		public LayoutKind Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B23 RID: 11043
		private const int DEFAULT_PACKING_SIZE = 8;

		// Token: 0x04002B24 RID: 11044
		internal LayoutKind _val;

		/// <summary>Controls the alignment of data fields of a class or structure in memory.</summary>
		// Token: 0x04002B25 RID: 11045
		[__DynamicallyInvokable]
		public int Pack;

		/// <summary>Indicates the absolute size of the class or structure.</summary>
		// Token: 0x04002B26 RID: 11046
		[__DynamicallyInvokable]
		public int Size;

		/// <summary>Indicates whether string data fields within the class should be marshaled as <see langword="LPWSTR" /> or <see langword="LPSTR" /> by default.</summary>
		// Token: 0x04002B27 RID: 11047
		[__DynamicallyInvokable]
		public CharSet CharSet;
	}
}

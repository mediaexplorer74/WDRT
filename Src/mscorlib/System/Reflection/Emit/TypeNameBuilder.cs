using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200062D RID: 1581
	internal class TypeNameBuilder
	{
		// Token: 0x060049A5 RID: 18853
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr CreateTypeNameBuilder();

		// Token: 0x060049A6 RID: 18854
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ReleaseTypeNameBuilder(IntPtr pAQN);

		// Token: 0x060049A7 RID: 18855
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void OpenGenericArguments(IntPtr tnb);

		// Token: 0x060049A8 RID: 18856
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CloseGenericArguments(IntPtr tnb);

		// Token: 0x060049A9 RID: 18857
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void OpenGenericArgument(IntPtr tnb);

		// Token: 0x060049AA RID: 18858
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CloseGenericArgument(IntPtr tnb);

		// Token: 0x060049AB RID: 18859
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddName(IntPtr tnb, string name);

		// Token: 0x060049AC RID: 18860
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddPointer(IntPtr tnb);

		// Token: 0x060049AD RID: 18861
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddByRef(IntPtr tnb);

		// Token: 0x060049AE RID: 18862
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddSzArray(IntPtr tnb);

		// Token: 0x060049AF RID: 18863
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddArray(IntPtr tnb, int rank);

		// Token: 0x060049B0 RID: 18864
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddAssemblySpec(IntPtr tnb, string assemblySpec);

		// Token: 0x060049B1 RID: 18865
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ToString(IntPtr tnb, StringHandleOnStack retString);

		// Token: 0x060049B2 RID: 18866
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void Clear(IntPtr tnb);

		// Token: 0x060049B3 RID: 18867 RVA: 0x0010C0C4 File Offset: 0x0010A2C4
		[SecuritySafeCritical]
		internal static string ToString(Type type, TypeNameBuilder.Format format)
		{
			if ((format == TypeNameBuilder.Format.FullName || format == TypeNameBuilder.Format.AssemblyQualifiedName) && !type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				return null;
			}
			TypeNameBuilder typeNameBuilder = new TypeNameBuilder(TypeNameBuilder.CreateTypeNameBuilder());
			typeNameBuilder.Clear();
			typeNameBuilder.ConstructAssemblyQualifiedNameWorker(type, format);
			string text = typeNameBuilder.ToString();
			typeNameBuilder.Dispose();
			return text;
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0010C112 File Offset: 0x0010A312
		private TypeNameBuilder(IntPtr typeNameBuilder)
		{
			this.m_typeNameBuilder = typeNameBuilder;
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0010C121 File Offset: 0x0010A321
		[SecurityCritical]
		internal void Dispose()
		{
			TypeNameBuilder.ReleaseTypeNameBuilder(this.m_typeNameBuilder);
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0010C130 File Offset: 0x0010A330
		[SecurityCritical]
		private void AddElementType(Type elementType)
		{
			if (elementType.HasElementType)
			{
				this.AddElementType(elementType.GetElementType());
			}
			if (elementType.IsPointer)
			{
				this.AddPointer();
				return;
			}
			if (elementType.IsByRef)
			{
				this.AddByRef();
				return;
			}
			if (elementType.IsSzArray)
			{
				this.AddSzArray();
				return;
			}
			if (elementType.IsArray)
			{
				this.AddArray(elementType.GetArrayRank());
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0010C194 File Offset: 0x0010A394
		[SecurityCritical]
		private void ConstructAssemblyQualifiedNameWorker(Type type, TypeNameBuilder.Format format)
		{
			Type type2 = type;
			while (type2.HasElementType)
			{
				type2 = type2.GetElementType();
			}
			List<Type> list = new List<Type>();
			Type type3 = type2;
			while (type3 != null)
			{
				list.Add(type3);
				type3 = (type3.IsGenericParameter ? null : type3.DeclaringType);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Type type4 = list[i];
				string text = type4.Name;
				if (i == list.Count - 1 && type4.Namespace != null && type4.Namespace.Length != 0)
				{
					text = type4.Namespace + "." + text;
				}
				this.AddName(text);
			}
			if (type2.IsGenericType && (!type2.IsGenericTypeDefinition || format == TypeNameBuilder.Format.ToString))
			{
				Type[] genericArguments = type2.GetGenericArguments();
				this.OpenGenericArguments();
				for (int j = 0; j < genericArguments.Length; j++)
				{
					TypeNameBuilder.Format format2 = ((format == TypeNameBuilder.Format.FullName) ? TypeNameBuilder.Format.AssemblyQualifiedName : format);
					this.OpenGenericArgument();
					this.ConstructAssemblyQualifiedNameWorker(genericArguments[j], format2);
					this.CloseGenericArgument();
				}
				this.CloseGenericArguments();
			}
			this.AddElementType(type);
			if (format == TypeNameBuilder.Format.AssemblyQualifiedName)
			{
				this.AddAssemblySpec(type.Module.Assembly.FullName);
			}
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0010C2C2 File Offset: 0x0010A4C2
		[SecurityCritical]
		private void OpenGenericArguments()
		{
			TypeNameBuilder.OpenGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0010C2CF File Offset: 0x0010A4CF
		[SecurityCritical]
		private void CloseGenericArguments()
		{
			TypeNameBuilder.CloseGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0010C2DC File Offset: 0x0010A4DC
		[SecurityCritical]
		private void OpenGenericArgument()
		{
			TypeNameBuilder.OpenGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0010C2E9 File Offset: 0x0010A4E9
		[SecurityCritical]
		private void CloseGenericArgument()
		{
			TypeNameBuilder.CloseGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x0010C2F6 File Offset: 0x0010A4F6
		[SecurityCritical]
		private void AddName(string name)
		{
			TypeNameBuilder.AddName(this.m_typeNameBuilder, name);
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x0010C304 File Offset: 0x0010A504
		[SecurityCritical]
		private void AddPointer()
		{
			TypeNameBuilder.AddPointer(this.m_typeNameBuilder);
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0010C311 File Offset: 0x0010A511
		[SecurityCritical]
		private void AddByRef()
		{
			TypeNameBuilder.AddByRef(this.m_typeNameBuilder);
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0010C31E File Offset: 0x0010A51E
		[SecurityCritical]
		private void AddSzArray()
		{
			TypeNameBuilder.AddSzArray(this.m_typeNameBuilder);
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0010C32B File Offset: 0x0010A52B
		[SecurityCritical]
		private void AddArray(int rank)
		{
			TypeNameBuilder.AddArray(this.m_typeNameBuilder, rank);
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0010C339 File Offset: 0x0010A539
		[SecurityCritical]
		private void AddAssemblySpec(string assemblySpec)
		{
			TypeNameBuilder.AddAssemblySpec(this.m_typeNameBuilder, assemblySpec);
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0010C348 File Offset: 0x0010A548
		[SecuritySafeCritical]
		public override string ToString()
		{
			string text = null;
			TypeNameBuilder.ToString(this.m_typeNameBuilder, JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0010C36A File Offset: 0x0010A56A
		[SecurityCritical]
		private void Clear()
		{
			TypeNameBuilder.Clear(this.m_typeNameBuilder);
		}

		// Token: 0x04001E8A RID: 7818
		private IntPtr m_typeNameBuilder;

		// Token: 0x02000C38 RID: 3128
		internal enum Format
		{
			// Token: 0x04003746 RID: 14150
			ToString,
			// Token: 0x04003747 RID: 14151
			FullName,
			// Token: 0x04003748 RID: 14152
			AssemblyQualifiedName
		}
	}
}

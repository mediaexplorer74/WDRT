using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates generic type parameters for dynamically defined generic types and methods. This class cannot be inherited.</summary>
	// Token: 0x02000663 RID: 1635
	[ComVisible(true)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="typeInfo">The object to test.</param>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E46 RID: 20038 RVA: 0x0011CC95 File Offset: 0x0011AE95
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x0011CCAE File Offset: 0x0011AEAE
		internal GenericTypeParameterBuilder(TypeBuilder type)
		{
			this.m_type = type;
		}

		/// <summary>Returns a string representation of the current generic type parameter.</summary>
		/// <returns>A string that contains the name of the generic type parameter.</returns>
		// Token: 0x06004E48 RID: 20040 RVA: 0x0011CCBD File Offset: 0x0011AEBD
		public override string ToString()
		{
			return this.m_type.Name;
		}

		/// <summary>Tests whether the given object is an instance of <see langword="EventToken" /> and is equal to the current instance.</summary>
		/// <param name="o">The object to be compared with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is an instance of <see langword="EventToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E49 RID: 20041 RVA: 0x0011CCCC File Offset: 0x0011AECC
		public override bool Equals(object o)
		{
			GenericTypeParameterBuilder genericTypeParameterBuilder = o as GenericTypeParameterBuilder;
			return !(genericTypeParameterBuilder == null) && genericTypeParameterBuilder.m_type == this.m_type;
		}

		/// <summary>Returns a 32-bit integer hash code for the current instance.</summary>
		/// <returns>A 32-bit integer hash code.</returns>
		// Token: 0x06004E4A RID: 20042 RVA: 0x0011CCF9 File Offset: 0x0011AEF9
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		/// <summary>Gets the generic type definition or generic method definition to which the generic type parameter belongs.</summary>
		/// <returns>If the type parameter belongs to a generic type, a <see cref="T:System.Type" /> object representing that generic type; if the type parameter belongs to a generic method, a <see cref="T:System.Type" /> object representing that type that declared that generic method.</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004E4B RID: 20043 RVA: 0x0011CD06 File Offset: 0x0011AF06
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</returns>
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x0011CD13 File Offset: 0x0011AF13
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		/// <summary>Gets the name of the generic type parameter.</summary>
		/// <returns>The name of the generic type parameter.</returns>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004E4D RID: 20045 RVA: 0x0011CD20 File Offset: 0x0011AF20
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		/// <summary>Gets the dynamic module that contains the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module that contains the generic type parameter.</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x0011CD2D File Offset: 0x0011AF2D
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x0011CD3A File Offset: 0x0011AF3A
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_type.MetadataTokenInternal;
			}
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</returns>
		// Token: 0x06004E50 RID: 20048 RVA: 0x0011CD47 File Offset: 0x0011AF47
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</returns>
		// Token: 0x06004E51 RID: 20049 RVA: 0x0011CD5A File Offset: 0x0011AF5A
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		/// <summary>Returns the type of a one-dimensional array whose element type is the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of a one-dimensional array whose element type is the generic type parameter.</returns>
		// Token: 0x06004E52 RID: 20050 RVA: 0x0011CD6D File Offset: 0x0011AF6D
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		/// <summary>Returns the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid number of dimensions. For example, its value is less than 1.</exception>
		// Token: 0x06004E53 RID: 20051 RVA: 0x0011CD80 File Offset: 0x0011AF80
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0) as SymbolType;
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x0011CDE6 File Offset: 0x0011AFE6
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="invokeAttr">Not supported.</param>
		/// <param name="binder">Not supported.</param>
		/// <param name="target">Not supported.</param>
		/// <param name="args">Not supported.</param>
		/// <param name="modifiers">Not supported.</param>
		/// <param name="culture">Not supported.</param>
		/// <param name="namedParameters">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E55 RID: 20053 RVA: 0x0011CDED File Offset: 0x0011AFED
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004E56 RID: 20054 RVA: 0x0011CDF4 File Offset: 0x0011AFF4
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004E57 RID: 20055 RVA: 0x0011CE01 File Offset: 0x0011B001
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets <see langword="null" /> in all cases.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x0011CE08 File Offset: 0x0011B008
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets <see langword="null" /> in all cases.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x0011CE0B File Offset: 0x0011B00B
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets <see langword="null" /> in all cases.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x0011CE0E File Offset: 0x0011B00E
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the base type constraint of the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the base type constraint of the generic type parameter, or <see langword="null" /> if the type parameter has no base type constraint.</returns>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x0011CE11 File Offset: 0x0011B011
		public override Type BaseType
		{
			get
			{
				return this.m_type.BaseType;
			}
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x0011CE1E File Offset: 0x0011B01E
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E5D RID: 20061 RVA: 0x0011CE25 File Offset: 0x0011B025
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0011CE2C File Offset: 0x0011B02C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E5F RID: 20063 RVA: 0x0011CE33 File Offset: 0x0011B033
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E60 RID: 20064 RVA: 0x0011CE3A File Offset: 0x0011B03A
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E61 RID: 20065 RVA: 0x0011CE41 File Offset: 0x0011B041
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to search without regard for case; <see langword="false" /> to make a case-sensitive search.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E62 RID: 20066 RVA: 0x0011CE48 File Offset: 0x0011B048
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E63 RID: 20067 RVA: 0x0011CE4F File Offset: 0x0011B04F
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E64 RID: 20068 RVA: 0x0011CE56 File Offset: 0x0011B056
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E65 RID: 20069 RVA: 0x0011CE5D File Offset: 0x0011B05D
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x0011CE64 File Offset: 0x0011B064
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E67 RID: 20071 RVA: 0x0011CE6B File Offset: 0x0011B06B
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E68 RID: 20072 RVA: 0x0011CE72 File Offset: 0x0011B072
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E69 RID: 20073 RVA: 0x0011CE79 File Offset: 0x0011B079
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="type">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E6A RID: 20074 RVA: 0x0011CE80 File Offset: 0x0011B080
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="interfaceType">A <see cref="T:System.Type" /> object that represents the interface type for which the mapping is to be retrieved.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E6B RID: 20075 RVA: 0x0011CE87 File Offset: 0x0011B087
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E6C RID: 20076 RVA: 0x0011CE8E File Offset: 0x0011B08E
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E6D RID: 20077 RVA: 0x0011CE95 File Offset: 0x0011B095
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x0011CE9C File Offset: 0x0011B09C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x0011CE9F File Offset: 0x0011B09F
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0011CEA2 File Offset: 0x0011B0A2
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0011CEA5 File Offset: 0x0011B0A5
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0011CEA8 File Offset: 0x0011B0A8
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0011CEAB File Offset: 0x0011B0AB
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <returns>The type referred to by the current array type, pointer type, or <see langword="ByRef" /> type; or <see langword="null" /> if the current type is not an array type, is not a pointer type, and is not passed by reference.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E74 RID: 20084 RVA: 0x0011CEAE File Offset: 0x0011B0AE
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0011CEB5 File Offset: 0x0011B0B5
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		/// <summary>Gets the current generic type parameter.</summary>
		/// <returns>The current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> object.</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004E76 RID: 20086 RVA: 0x0011CEB8 File Offset: 0x0011B0B8
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06004E77 RID: 20087 RVA: 0x0011CEBB File Offset: 0x0011B0BB
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets <see langword="false" /> in all cases.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004E78 RID: 20088 RVA: 0x0011CEC2 File Offset: 0x0011B0C2
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns <see langword="false" /> in all cases.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x0011CEC5 File Offset: 0x0011B0C5
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets <see langword="true" /> in all cases.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x0011CEC8 File Offset: 0x0011B0C8
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x0011CECB File Offset: 0x0011B0CB
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</summary>
		/// <returns>The position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x0011CECE File Offset: 0x0011B0CE
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_type.GenericParameterPosition;
			}
		}

		/// <summary>Gets <see langword="true" /> in all cases.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x0011CEDB File Offset: 0x0011B0DB
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_type.ContainsGenericParameters;
			}
		}

		/// <summary>Gets a combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> flags that describe the covariance and special constraints of the current generic type parameter.</summary>
		/// <returns>A bitwise combination of values that describes the covariance and special constraints of the current generic type parameter.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x0011CEE8 File Offset: 0x0011B0E8
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return this.m_type.GenericParameterAttributes;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004E7F RID: 20095 RVA: 0x0011CEF5 File Offset: 0x0011B0F5
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.m_type.DeclaringMethod;
			}
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06004E80 RID: 20096 RVA: 0x0011CF02 File Offset: 0x0011B102
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Not valid for incomplete generic type parameters.</summary>
		/// <param name="typeArguments">An array of type arguments.</param>
		/// <returns>This method is invalid for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06004E81 RID: 20097 RVA: 0x0011CF09 File Offset: 0x0011B109
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0011CF1A File Offset: 0x0011B11A
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="c">The object to test.</param>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E83 RID: 20099 RVA: 0x0011CF1D File Offset: 0x0011B11D
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="c">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E84 RID: 20100 RVA: 0x0011CF24 File Offset: 0x0011B124
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E85 RID: 20101 RVA: 0x0011CF2B File Offset: 0x0011B12B
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E86 RID: 20102 RVA: 0x0011CF32 File Offset: 0x0011B132
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="attributeType">Not supported.</param>
		/// <param name="inherit">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06004E87 RID: 20103 RVA: 0x0011CF39 File Offset: 0x0011B139
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="binaryAttribute" /> is a null reference.</exception>
		// Token: 0x06004E88 RID: 20104 RVA: 0x0011CF40 File Offset: 0x0011B140
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_type.SetGenParamCustomAttribute(con, binaryAttribute);
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class that defines the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		// Token: 0x06004E89 RID: 20105 RVA: 0x0011CF4F File Offset: 0x0011B14F
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_type.SetGenParamCustomAttribute(customBuilder);
		}

		/// <summary>Sets the base type that a type must inherit in order to be substituted for the type parameter.</summary>
		/// <param name="baseTypeConstraint">The <see cref="T:System.Type" /> that must be inherited by any type that is to be substituted for the type parameter.</param>
		// Token: 0x06004E8A RID: 20106 RVA: 0x0011CF5D File Offset: 0x0011B15D
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.m_type.CheckContext(new Type[] { baseTypeConstraint });
			this.m_type.SetParent(baseTypeConstraint);
		}

		/// <summary>Sets the interfaces a type must implement in order to be substituted for the type parameter.</summary>
		/// <param name="interfaceConstraints">An array of <see cref="T:System.Type" /> objects that represent the interfaces a type must implement in order to be substituted for the type parameter.</param>
		// Token: 0x06004E8B RID: 20107 RVA: 0x0011CF80 File Offset: 0x0011B180
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.m_type.CheckContext(interfaceConstraints);
			this.m_type.SetInterfaces(interfaceConstraints);
		}

		/// <summary>Sets the variance characteristics and special constraints of the generic parameter, such as the parameterless constructor constraint.</summary>
		/// <param name="genericParameterAttributes">A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that represent the variance characteristics and special constraints of the generic type parameter.</param>
		// Token: 0x06004E8C RID: 20108 RVA: 0x0011CF9A File Offset: 0x0011B19A
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_type.SetGenParamAttributes(genericParameterAttributes);
		}

		// Token: 0x040021D8 RID: 8664
		internal TypeBuilder m_type;
	}
}

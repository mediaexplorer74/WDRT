using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates new instances of classes during run time.</summary>
	// Token: 0x02000661 RID: 1633
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_TypeBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class TypeBuilder : TypeInfo, _TypeBuilder
	{
		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Reflection.TypeInfo" /> object can be assigned to this object.</summary>
		/// <param name="typeInfo">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="typeInfo" /> can be assigned to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D59 RID: 19801 RVA: 0x00119DAF File Offset: 0x00117FAF
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		/// <summary>Returns the method of the specified constructed generic type that corresponds to the specified method of the generic type definition.</summary>
		/// <param name="type">The constructed generic type whose method is returned.</param>
		/// <param name="method">A method on the generic type definition of <paramref name="type" />, which specifies which method of <paramref name="type" /> to return.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the method of <paramref name="type" /> corresponding to <paramref name="method" />, which specifies a method belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is a generic method that is not a generic method definition.  
		/// -or-  
		/// <paramref name="type" /> does not represent a generic type.  
		/// -or-  
		/// <paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// The declaring type of <paramref name="method" /> is not a generic type definition.  
		/// -or-  
		/// The declaring type of <paramref name="method" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06004D5A RID: 19802 RVA: 0x00119DC8 File Offset: 0x00117FC8
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
			}
			if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedGenericMethodDefinition"), "method");
			}
			if (method.DeclaringType == null || !method.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MethodNeedGenericDeclaringType"), "method");
			}
			if (type.GetGenericTypeDefinition() != method.DeclaringType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMethodDeclaringType"), "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (!(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
			}
			return MethodOnTypeBuilderInstantiation.GetMethod(method, type as TypeBuilderInstantiation);
		}

		/// <summary>Returns the constructor of the specified constructed generic type that corresponds to the specified constructor of the generic type definition.</summary>
		/// <param name="type">The constructed generic type whose constructor is returned.</param>
		/// <param name="constructor">A constructor on the generic type definition of <paramref name="type" />, which specifies which constructor of <paramref name="type" /> to return.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor of <paramref name="type" /> corresponding to <paramref name="constructor" />, which specifies a constructor belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a generic type.  
		/// -or-  
		/// <paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// The declaring type of <paramref name="constructor" /> is not a generic type definition.  
		/// -or-  
		/// The declaring type of <paramref name="constructor" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06004D5B RID: 19803 RVA: 0x00119EB4 File Offset: 0x001180B4
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
			}
			if (!constructor.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConstructorNeedGenericDeclaringType"), "constructor");
			}
			if (!(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
			}
			if (type is TypeBuilder && type.IsGenericTypeDefinition)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (type.GetGenericTypeDefinition() != constructor.DeclaringType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorDeclaringType"), "type");
			}
			return ConstructorOnTypeBuilderInstantiation.GetConstructor(constructor, type as TypeBuilderInstantiation);
		}

		/// <summary>Returns the field of the specified constructed generic type that corresponds to the specified field of the generic type definition.</summary>
		/// <param name="type">The constructed generic type whose field is returned.</param>
		/// <param name="field">A field on the generic type definition of <paramref name="type" />, which specifies which field of <paramref name="type" /> to return.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object that represents the field of <paramref name="type" /> corresponding to <paramref name="field" />, which specifies a field belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a generic type.  
		/// -or-  
		/// <paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// The declaring type of <paramref name="field" /> is not a generic type definition.  
		/// -or-  
		/// The declaring type of <paramref name="field" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06004D5C RID: 19804 RVA: 0x00119F74 File Offset: 0x00118174
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
			}
			if (!field.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FieldNeedGenericDeclaringType"), "field");
			}
			if (!(type is TypeBuilderInstantiation))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
			}
			if (type is TypeBuilder && type.IsGenericTypeDefinition)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (type.GetGenericTypeDefinition() != field.DeclaringType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldDeclaringType"), "type");
			}
			return FieldOnTypeBuilderInstantiation.GetField(field, type as TypeBuilderInstantiation);
		}

		// Token: 0x06004D5D RID: 19805
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetParentType(RuntimeModule module, int tdTypeDef, int tkParent);

		// Token: 0x06004D5E RID: 19806
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddInterfaceImpl(RuntimeModule module, int tdTypeDef, int tkInterface);

		// Token: 0x06004D5F RID: 19807
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineMethod(RuntimeModule module, int tkParent, string name, byte[] signature, int sigLength, MethodAttributes attributes);

		// Token: 0x06004D60 RID: 19808
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineMethodSpec(RuntimeModule module, int tkParent, byte[] signature, int sigLength);

		// Token: 0x06004D61 RID: 19809
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineField(RuntimeModule module, int tkParent, string name, byte[] signature, int sigLength, FieldAttributes attributes);

		// Token: 0x06004D62 RID: 19810
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetMethodIL(RuntimeModule module, int tk, bool isInitLocals, byte[] body, int bodyLength, byte[] LocalSig, int sigLength, int maxStackSize, ExceptionHandler[] exceptions, int numExceptions, int[] tokenFixups, int numTokenFixups);

		// Token: 0x06004D63 RID: 19811
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DefineCustomAttribute(RuntimeModule module, int tkAssociate, int tkConstructor, byte[] attr, int attrLength, bool toDisk, bool updateCompilerFlags);

		// Token: 0x06004D64 RID: 19812 RVA: 0x0011A034 File Offset: 0x00118234
		[SecurityCritical]
		internal static void DefineCustomAttribute(ModuleBuilder module, int tkAssociate, int tkConstructor, byte[] attr, bool toDisk, bool updateCompilerFlags)
		{
			byte[] array = null;
			if (attr != null)
			{
				array = new byte[attr.Length];
				Array.Copy(attr, array, attr.Length);
			}
			TypeBuilder.DefineCustomAttribute(module.GetNativeHandle(), tkAssociate, tkConstructor, array, (array != null) ? array.Length : 0, toDisk, updateCompilerFlags);
		}

		// Token: 0x06004D65 RID: 19813
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetPInvokeData(RuntimeModule module, string DllName, string name, int token, int linkFlags);

		// Token: 0x06004D66 RID: 19814
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineProperty(RuntimeModule module, int tkParent, string name, PropertyAttributes attributes, byte[] signature, int sigLength);

		// Token: 0x06004D67 RID: 19815
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int DefineEvent(RuntimeModule module, int tkParent, string name, EventAttributes attributes, int tkEventType);

		// Token: 0x06004D68 RID: 19816
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void DefineMethodSemantics(RuntimeModule module, int tkAssociation, MethodSemanticsAttributes semantics, int tkMethod);

		// Token: 0x06004D69 RID: 19817
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void DefineMethodImpl(RuntimeModule module, int tkType, int tkBody, int tkDecl);

		// Token: 0x06004D6A RID: 19818
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetMethodImpl(RuntimeModule module, int tkMethod, MethodImplAttributes MethodImplAttributes);

		// Token: 0x06004D6B RID: 19819
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int SetParamInfo(RuntimeModule module, int tkMethod, int iSequence, ParameterAttributes iParamAttributes, string strParamName);

		// Token: 0x06004D6C RID: 19820
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int GetTokenFromSig(RuntimeModule module, byte[] signature, int sigLength);

		// Token: 0x06004D6D RID: 19821
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetFieldLayoutOffset(RuntimeModule module, int fdToken, int iOffset);

		// Token: 0x06004D6E RID: 19822
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetClassLayout(RuntimeModule module, int tk, PackingSize iPackingSize, int iTypeSize);

		// Token: 0x06004D6F RID: 19823
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetFieldMarshal(RuntimeModule module, int tk, byte[] ubMarshal, int ubSize);

		// Token: 0x06004D70 RID: 19824
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void SetConstantValue(RuntimeModule module, int tk, int corType, void* pValue);

		// Token: 0x06004D71 RID: 19825
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void AddDeclarativeSecurity(RuntimeModule module, int parent, SecurityAction action, byte[] blob, int cb);

		// Token: 0x06004D72 RID: 19826 RVA: 0x0011A074 File Offset: 0x00118274
		private static bool IsPublicComType(Type type)
		{
			Type declaringType = type.DeclaringType;
			if (declaringType != null)
			{
				if (TypeBuilder.IsPublicComType(declaringType) && (type.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
				{
					return true;
				}
			}
			else if ((type.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0011A0B4 File Offset: 0x001182B4
		internal static bool IsTypeEqual(Type t1, Type t2)
		{
			if (t1 == t2)
			{
				return true;
			}
			TypeBuilder typeBuilder = null;
			TypeBuilder typeBuilder2 = null;
			Type type;
			if (t1 is TypeBuilder)
			{
				typeBuilder = (TypeBuilder)t1;
				type = typeBuilder.m_bakedRuntimeType;
			}
			else
			{
				type = t1;
			}
			Type type2;
			if (t2 is TypeBuilder)
			{
				typeBuilder2 = (TypeBuilder)t2;
				type2 = typeBuilder2.m_bakedRuntimeType;
			}
			else
			{
				type2 = t2;
			}
			return (typeBuilder != null && typeBuilder2 != null && typeBuilder == typeBuilder2) || (type != null && type2 != null && type == type2);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x0011A140 File Offset: 0x00118340
		[SecurityCritical]
		internal unsafe static void SetConstantValue(ModuleBuilder module, int tk, Type destType, object value)
		{
			if (value != null)
			{
				Type type = value.GetType();
				if (destType.IsByRef)
				{
					destType = destType.GetElementType();
				}
				if (destType.IsEnum)
				{
					EnumBuilder enumBuilder;
					Type type2;
					TypeBuilder typeBuilder;
					if ((enumBuilder = destType as EnumBuilder) != null)
					{
						type2 = enumBuilder.GetEnumUnderlyingType();
						if (type != enumBuilder.m_typeBuilder.m_bakedRuntimeType && type != type2)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
						}
					}
					else if ((typeBuilder = destType as TypeBuilder) != null)
					{
						type2 = typeBuilder.m_enumUnderlyingType;
						if (type2 == null || (type != typeBuilder.UnderlyingSystemType && type != type2))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
						}
					}
					else
					{
						type2 = Enum.GetUnderlyingType(destType);
						if (type != destType && type != type2)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
						}
					}
					type = type2;
				}
				else if (!destType.IsAssignableFrom(type))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
				}
				CorElementType corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType)type);
				if (corElementType - CorElementType.Boolean <= 11)
				{
					fixed (byte* ptr = &JitHelpers.GetPinningHelper(value).m_data)
					{
						byte* ptr2 = ptr;
						TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, (int)corElementType, (void*)ptr2);
					}
					return;
				}
				if (type == typeof(string))
				{
					fixed (string text = (string)value)
					{
						char* ptr3 = text;
						if (ptr3 != null)
						{
							ptr3 += RuntimeHelpers.OffsetToStringData / 2;
						}
						TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 14, (void*)ptr3);
					}
					return;
				}
				if (type == typeof(DateTime))
				{
					long ticks = ((DateTime)value).Ticks;
					TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 10, (void*)(&ticks));
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_ConstantNotSupported", new object[] { type.ToString() }));
			}
			else
			{
				if (destType.IsValueType && (!destType.IsGenericType || !(destType.GetGenericTypeDefinition() == typeof(Nullable<>))))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ConstantNull"));
				}
				TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 18, null);
				return;
			}
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0011A363 File Offset: 0x00118563
		internal TypeBuilder(ModuleBuilder module)
		{
			this.m_tdType = new TypeToken(33554432);
			this.m_isHiddenGlobalType = true;
			this.m_module = module;
			this.m_listMethods = new List<MethodBuilder>();
			this.m_lastTokenizedMethod = -1;
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x0011A39B File Offset: 0x0011859B
		internal TypeBuilder(string szName, int genParamPos, MethodBuilder declMeth)
		{
			this.m_declMeth = declMeth;
			this.m_DeclaringType = this.m_declMeth.GetTypeBuilder();
			this.m_module = declMeth.GetModuleBuilder();
			this.InitAsGenericParam(szName, genParamPos);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x0011A3CF File Offset: 0x001185CF
		private TypeBuilder(string szName, int genParamPos, TypeBuilder declType)
		{
			this.m_DeclaringType = declType;
			this.m_module = declType.GetModuleBuilder();
			this.InitAsGenericParam(szName, genParamPos);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x0011A3F2 File Offset: 0x001185F2
		private void InitAsGenericParam(string szName, int genParamPos)
		{
			this.m_strName = szName;
			this.m_genParamPos = genParamPos;
			this.m_bIsGenParam = true;
			this.m_typeInterfaces = new List<Type>();
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0011A414 File Offset: 0x00118614
		[SecurityCritical]
		internal TypeBuilder(string name, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
		{
			this.Init(name, attr, parent, interfaces, module, iPackingSize, iTypeSize, enclosingType);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0011A43C File Offset: 0x0011863C
		[SecurityCritical]
		private void Init(string fullname, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (fullname.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "fullname");
			}
			if (fullname[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "fullname");
			}
			if (fullname.Length > 1023)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeNameTooLong"), "fullname");
			}
			this.m_module = module;
			this.m_DeclaringType = enclosingType;
			AssemblyBuilder containingAssemblyBuilder = this.m_module.ContainingAssemblyBuilder;
			containingAssemblyBuilder.m_assemblyData.CheckTypeNameConflict(fullname, enclosingType);
			if (enclosingType != null && ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadNestedTypeFlags"), "attr");
			}
			int[] array = null;
			if (interfaces != null)
			{
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (interfaces[i] == null)
					{
						throw new ArgumentNullException("interfaces");
					}
				}
				array = new int[interfaces.Length + 1];
				for (int i = 0; i < interfaces.Length; i++)
				{
					array[i] = this.m_module.GetTypeTokenInternal(interfaces[i]).Token;
				}
			}
			int num = fullname.LastIndexOf('.');
			if (num == -1 || num == 0)
			{
				this.m_strNameSpace = string.Empty;
				this.m_strName = fullname;
			}
			else
			{
				this.m_strNameSpace = fullname.Substring(0, num);
				this.m_strName = fullname.Substring(num + 1);
			}
			this.VerifyTypeAttributes(attr);
			this.m_iAttr = attr;
			this.SetParent(parent);
			this.m_listMethods = new List<MethodBuilder>();
			this.m_lastTokenizedMethod = -1;
			this.SetInterfaces(interfaces);
			int num2 = 0;
			if (this.m_typeParent != null)
			{
				num2 = this.m_module.GetTypeTokenInternal(this.m_typeParent).Token;
			}
			int num3 = 0;
			if (enclosingType != null)
			{
				num3 = enclosingType.m_tdType.Token;
			}
			this.m_tdType = new TypeToken(TypeBuilder.DefineType(this.m_module.GetNativeHandle(), fullname, num2, this.m_iAttr, num3, array));
			this.m_iPackingSize = iPackingSize;
			this.m_iTypeSize = iTypeSize;
			if (this.m_iPackingSize != PackingSize.Unspecified || this.m_iTypeSize != 0)
			{
				TypeBuilder.SetClassLayout(this.GetModuleBuilder().GetNativeHandle(), this.m_tdType.Token, this.m_iPackingSize, this.m_iTypeSize);
			}
			if (TypeBuilder.IsPublicComType(this))
			{
				if (containingAssemblyBuilder.IsPersistable() && !this.m_module.IsTransient())
				{
					containingAssemblyBuilder.m_assemblyData.AddPublicComType(this);
				}
				if (!this.m_module.Equals(containingAssemblyBuilder.ManifestModule))
				{
					containingAssemblyBuilder.DefineExportedTypeInMemory(this, this.m_module.m_moduleData.FileToken, this.m_tdType.Token);
				}
			}
			this.m_module.AddType(this.FullName, this);
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x0011A704 File Offset: 0x00118904
		[SecurityCritical]
		private MethodBuilder DefinePInvokeMethodHelper(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			this.CheckContext(new Type[] { returnType });
			this.CheckContext(new Type[][] { returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes });
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			AppDomain.CheckDefinePInvokeSupported();
			object syncRoot = this.SyncRoot;
			MethodBuilder methodBuilder;
			lock (syncRoot)
			{
				methodBuilder = this.DefinePInvokeMethodHelperNoLock(name, dllName, importName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
			}
			return methodBuilder;
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0011A7A0 File Offset: 0x001189A0
		[SecurityCritical]
		private MethodBuilder DefinePInvokeMethodHelperNoLock(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (dllName == null)
			{
				throw new ArgumentNullException("dllName");
			}
			if (dllName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "dllName");
			}
			if (importName == null)
			{
				throw new ArgumentNullException("importName");
			}
			if (importName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "importName");
			}
			if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadPInvokeMethod"));
			}
			if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadPInvokeOnInterface"));
			}
			this.ThrowIfCreated();
			attributes |= MethodAttributes.PinvokeImpl;
			MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this, false);
			int num;
			byte[] array = methodBuilder.GetMethodSignature().InternalGetSignature(out num);
			if (this.m_listMethods.Contains(methodBuilder))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MethodRedefined"));
			}
			this.m_listMethods.Add(methodBuilder);
			MethodToken token = methodBuilder.GetToken();
			int num2 = 0;
			switch (nativeCallConv)
			{
			case CallingConvention.Winapi:
				num2 = 256;
				break;
			case CallingConvention.Cdecl:
				num2 = 512;
				break;
			case CallingConvention.StdCall:
				num2 = 768;
				break;
			case CallingConvention.ThisCall:
				num2 = 1024;
				break;
			case CallingConvention.FastCall:
				num2 = 1280;
				break;
			}
			switch (nativeCharSet)
			{
			case CharSet.None:
				num2 |= 0;
				break;
			case CharSet.Ansi:
				num2 |= 2;
				break;
			case CharSet.Unicode:
				num2 |= 4;
				break;
			case CharSet.Auto:
				num2 |= 6;
				break;
			}
			TypeBuilder.SetPInvokeData(this.m_module.GetNativeHandle(), dllName, importName, token.Token, num2);
			methodBuilder.SetToken(token);
			return methodBuilder;
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x0011A97C File Offset: 0x00118B7C
		[SecurityCritical]
		private FieldBuilder DefineDataHelper(string name, byte[] data, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (size <= 0 || size >= 4128768)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"));
			}
			this.ThrowIfCreated();
			string text = "$ArrayType$" + size.ToString();
			Type type = this.m_module.FindTypeBuilderWithName(text, false);
			TypeBuilder typeBuilder = type as TypeBuilder;
			if (typeBuilder == null)
			{
				TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed;
				typeBuilder = this.m_module.DefineType(text, typeAttributes, typeof(ValueType), PackingSize.Size1, size);
				typeBuilder.CreateType();
			}
			FieldBuilder fieldBuilder = this.DefineField(name, typeBuilder, attributes | FieldAttributes.Static);
			fieldBuilder.SetData(data, size);
			return fieldBuilder;
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x0011AA48 File Offset: 0x00118C48
		private void VerifyTypeAttributes(TypeAttributes attr)
		{
			if (this.DeclaringType == null)
			{
				if ((attr & TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.VisibilityMask) != TypeAttributes.Public)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrNestedVisibilityOnNonNestedType"));
				}
			}
			else if ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrNonNestedVisibilityNestedType"));
			}
			if ((attr & TypeAttributes.LayoutMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.LayoutMask) != TypeAttributes.SequentialLayout && (attr & TypeAttributes.LayoutMask) != TypeAttributes.ExplicitLayout)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrInvalidLayout"));
			}
			if ((attr & TypeAttributes.ReservedMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrReservedBitsSet"));
			}
		}

		/// <summary>Returns a value that indicates whether the current dynamic type has been created.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has been called; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004D7F RID: 19839 RVA: 0x0011AAD7 File Offset: 0x00118CD7
		public bool IsCreated()
		{
			return this.m_hasBeenCreated;
		}

		// Token: 0x06004D80 RID: 19840
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int DefineType(RuntimeModule module, string fullname, int tkParent, TypeAttributes attributes, int tkEnclosingType, int[] interfaceTokens);

		// Token: 0x06004D81 RID: 19841
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int DefineGenericParam(RuntimeModule module, string name, int tkParent, GenericParameterAttributes attributes, int position, int[] constraints);

		// Token: 0x06004D82 RID: 19842
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void TermCreateClass(RuntimeModule module, int tk, ObjectHandleOnStack type);

		// Token: 0x06004D83 RID: 19843 RVA: 0x0011AADF File Offset: 0x00118CDF
		internal void ThrowIfCreated()
		{
			if (this.IsCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x0011AAF9 File Offset: 0x00118CF9
		internal object SyncRoot
		{
			get
			{
				return this.m_module.SyncRoot;
			}
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x0011AB06 File Offset: 0x00118D06
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.m_module;
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x0011AB0E File Offset: 0x00118D0E
		internal RuntimeType BakedRuntimeType
		{
			get
			{
				return this.m_bakedRuntimeType;
			}
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x0011AB16 File Offset: 0x00118D16
		internal void SetGenParamAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_genParamAttributes = genericParameterAttributes;
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x0011AB20 File Offset: 0x00118D20
		internal void SetGenParamCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			TypeBuilder.CustAttr custAttr = new TypeBuilder.CustAttr(con, binaryAttribute);
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetGenParamCustomAttributeNoLock(custAttr);
			}
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x0011AB6C File Offset: 0x00118D6C
		internal void SetGenParamCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			TypeBuilder.CustAttr custAttr = new TypeBuilder.CustAttr(customBuilder);
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetGenParamCustomAttributeNoLock(custAttr);
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x0011ABB4 File Offset: 0x00118DB4
		private void SetGenParamCustomAttributeNoLock(TypeBuilder.CustAttr ca)
		{
			if (this.m_ca == null)
			{
				this.m_ca = new List<TypeBuilder.CustAttr>();
			}
			this.m_ca.Add(ca);
		}

		/// <summary>Returns the name of the type excluding the namespace.</summary>
		/// <returns>Read-only. The name of the type excluding the namespace.</returns>
		// Token: 0x06004D8B RID: 19851 RVA: 0x0011ABD5 File Offset: 0x00118DD5
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		/// <summary>Returns the type that declared this type.</summary>
		/// <returns>Read-only. The type that declared this type.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x0011ABDE File Offset: 0x00118DDE
		public override Type DeclaringType
		{
			get
			{
				return this.m_DeclaringType;
			}
		}

		/// <summary>Returns the type that was used to obtain this type.</summary>
		/// <returns>Read-only. The type that was used to obtain this type.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x0011ABE6 File Offset: 0x00118DE6
		public override Type ReflectedType
		{
			get
			{
				return this.m_DeclaringType;
			}
		}

		/// <summary>Retrieves the name of this type.</summary>
		/// <returns>Read-only. Retrieves the <see cref="T:System.String" /> name of this type.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x0011ABEE File Offset: 0x00118DEE
		public override string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic module that contains this type definition.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004D8F RID: 19855 RVA: 0x0011ABF6 File Offset: 0x00118DF6
		public override Module Module
		{
			get
			{
				return this.GetModuleBuilder();
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x0011ABFE File Offset: 0x00118DFE
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_tdType.Token;
			}
		}

		/// <summary>Retrieves the GUID of this type.</summary>
		/// <returns>Read-only. Retrieves the GUID of this type</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types.</exception>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004D91 RID: 19857 RVA: 0x0011AC0B File Offset: 0x00118E0B
		public override Guid GUID
		{
			get
			{
				if (!this.IsCreated())
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
				}
				return this.m_bakedRuntimeType.GUID;
			}
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If binder is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored.</param>
		/// <param name="args">An argument list. This is an array of Objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null.</param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for more details.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is null, the <see langword="CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a String that represents 1000 to a Double value, since 1000 is represented differently by different cultures.)</param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order.</param>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types.</exception>
		// Token: 0x06004D92 RID: 19858 RVA: 0x0011AC30 File Offset: 0x00118E30
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		/// <summary>Retrieves the dynamic assembly that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic assembly that contains this type definition.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004D93 RID: 19859 RVA: 0x0011AC6D File Offset: 0x00118E6D
		public override Assembly Assembly
		{
			get
			{
				return this.m_module.Assembly;
			}
		}

		/// <summary>Not supported in dynamic modules.</summary>
		/// <returns>Read-only.</returns>
		/// <exception cref="T:System.NotSupportedException">Not supported in dynamic modules.</exception>
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004D94 RID: 19860 RVA: 0x0011AC7A File Offset: 0x00118E7A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		/// <summary>Retrieves the full path of this type.</summary>
		/// <returns>Read-only. Retrieves the full path of this type.</returns>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004D95 RID: 19861 RVA: 0x0011AC8B File Offset: 0x00118E8B
		public override string FullName
		{
			get
			{
				if (this.m_strFullQualName == null)
				{
					this.m_strFullQualName = TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
				}
				return this.m_strFullQualName;
			}
		}

		/// <summary>Retrieves the namespace where this <see langword="TypeBuilder" /> is defined.</summary>
		/// <returns>Read-only. Retrieves the namespace where this <see langword="TypeBuilder" /> is defined.</returns>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x0011ACA8 File Offset: 0x00118EA8
		public override string Namespace
		{
			get
			{
				return this.m_strNameSpace;
			}
		}

		/// <summary>Returns the full name of this type qualified by the display name of the assembly.</summary>
		/// <returns>Read-only. The full name of this type qualified by the display name of the assembly.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004D97 RID: 19863 RVA: 0x0011ACB0 File Offset: 0x00118EB0
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		/// <summary>Retrieves the base type of this type.</summary>
		/// <returns>Read-only. Retrieves the base type of this type.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x0011ACB9 File Offset: 0x00118EB9
		public override Type BaseType
		{
			get
			{
				return this.m_typeParent;
			}
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0011ACC1 File Offset: 0x00118EC1
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004D9A RID: 19866 RVA: 0x0011ACED File Offset: 0x00118EED
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetConstructors(bindingAttr);
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0011AD13 File Offset: 0x00118F13
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			if (types == null)
			{
				return this.m_bakedRuntimeType.GetMethod(name, bindingAttr);
			}
			return this.m_bakedRuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004D9C RID: 19868 RVA: 0x0011AD53 File Offset: 0x00118F53
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetMethods(bindingAttr);
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <param name="name">The name of the field to get.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches then <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004D9D RID: 19869 RVA: 0x0011AD79 File Offset: 0x00118F79
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetField(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004D9E RID: 19870 RVA: 0x0011ADA0 File Offset: 0x00118FA0
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetFields(bindingAttr);
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this class with the fully qualified name matching the given interface name.</summary>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">If <see langword="true" />, the search is case-insensitive. If <see langword="false" />, the search is case-sensitive.</param>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004D9F RID: 19871 RVA: 0x0011ADC6 File Offset: 0x00118FC6
		public override Type GetInterface(string name, bool ignoreCase)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an array of all the interfaces implemented on this type and its base types.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x06004DA0 RID: 19872 RVA: 0x0011ADED File Offset: 0x00118FED
		public override Type[] GetInterfaces()
		{
			if (this.m_bakedRuntimeType != null)
			{
				return this.m_bakedRuntimeType.GetInterfaces();
			}
			if (this.m_typeInterfaces == null)
			{
				return EmptyArray<Type>.Value;
			}
			return this.m_typeInterfaces.ToArray();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <param name="name">The name of the event to search for.</param>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search.</param>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name, or <see langword="null" /> if there are no matches.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA1 RID: 19873 RVA: 0x0011AE22 File Offset: 0x00119022
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the public events declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public events declared or inherited by this type. An empty array is returned if there are no public events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA2 RID: 19874 RVA: 0x0011AE49 File Offset: 0x00119049
		public override EventInfo[] GetEvents()
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetEvents();
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x0011AE6E File Offset: 0x0011906E
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see langword="PropertyInfo" /> objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA4 RID: 19876 RVA: 0x0011AE7F File Offset: 0x0011907F
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetProperties(bindingAttr);
		}

		/// <summary>Returns the public and non-public nested types that are declared or inherited by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA5 RID: 19877 RVA: 0x0011AEA5 File Offset: 0x001190A5
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetNestedTypes(bindingAttr);
		}

		/// <summary>Returns the public and non-public nested types that are declared by this type.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to conduct a case-sensitive search for public methods.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA6 RID: 19878 RVA: 0x0011AECB File Offset: 0x001190CB
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns all the public and non-public members declared or inherited by this type, as specified.</summary>
		/// <param name="name">The name of the member.</param>
		/// <param name="type">The type of the member to return.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA7 RID: 19879 RVA: 0x0011AEF2 File Offset: 0x001190F2
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns an interface mapping for the requested interface.</summary>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface for which the mapping is to be retrieved.</param>
		/// <returns>Returns the requested interface mapping.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA8 RID: 19880 RVA: 0x0011AF1A File Offset: 0x0011911A
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetInterfaceMap(interfaceType);
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the events declared or inherited by this type that match the specified binding flags. An empty array is returned if there are no matching events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DA9 RID: 19881 RVA: 0x0011AF40 File Offset: 0x00119140
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetEvents(bindingAttr);
		}

		/// <summary>Returns the members for the public and non-public members declared or inherited by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x06004DAA RID: 19882 RVA: 0x0011AF66 File Offset: 0x00119166
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return this.m_bakedRuntimeType.GetMembers(bindingAttr);
		}

		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Type" /> can be assigned to this object.</summary>
		/// <param name="c">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="c" /> parameter and the current type represent the same type, or if the current type is in the inheritance hierarchy of <paramref name="c" />, or if the current type is an interface that <paramref name="c" /> supports. <see langword="false" /> if none of these conditions are valid, or if <paramref name="c" /> is <see langword="null" />.</returns>
		// Token: 0x06004DAB RID: 19883 RVA: 0x0011AF8C File Offset: 0x0011918C
		public override bool IsAssignableFrom(Type c)
		{
			if (TypeBuilder.IsTypeEqual(c, this))
			{
				return true;
			}
			TypeBuilder typeBuilder = c as TypeBuilder;
			Type type;
			if (typeBuilder != null)
			{
				type = typeBuilder.m_bakedRuntimeType;
			}
			else
			{
				type = c;
			}
			if (type != null && type is RuntimeType)
			{
				return !(this.m_bakedRuntimeType == null) && this.m_bakedRuntimeType.IsAssignableFrom(type);
			}
			if (typeBuilder == null)
			{
				return false;
			}
			if (typeBuilder.IsSubclassOf(this))
			{
				return true;
			}
			if (!base.IsInterface)
			{
				return false;
			}
			Type[] interfaces = typeBuilder.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (TypeBuilder.IsTypeEqual(interfaces[i], this))
				{
					return true;
				}
				if (interfaces[i].IsSubclassOf(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0011B03F File Offset: 0x0011923F
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_iAttr;
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x0011B047 File Offset: 0x00119247
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x0011B04A File Offset: 0x0011924A
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x0011B04D File Offset: 0x0011924D
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0011B050 File Offset: 0x00119250
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0011B053 File Offset: 0x00119253
		protected override bool IsCOMObjectImpl()
		{
			return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) != TypeAttributes.NotPublic;
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004DB2 RID: 19890 RVA: 0x0011B066 File Offset: 0x00119266
		public override Type GetElementType()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x0011B077 File Offset: 0x00119277
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		/// <summary>Gets a value that indicates whether the current type is security-critical or security-safe-critical, and therefore can perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current type is security-critical or security-safe-critical; <see langword="false" /> if it is transparent.</returns>
		/// <exception cref="T:System.NotSupportedException">The current dynamic type has not been created by calling the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.</exception>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x0011B07A File Offset: 0x0011927A
		public override bool IsSecurityCritical
		{
			get
			{
				if (!this.IsCreated())
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
				}
				return this.m_bakedRuntimeType.IsSecurityCritical;
			}
		}

		/// <summary>Gets a value that indicates whether the current type is security-safe-critical; that is, whether it can perform critical operations and can be accessed by transparent code.</summary>
		/// <returns>
		///   <see langword="true" /> if the current type is security-safe-critical; <see langword="false" /> if it is security-critical or transparent.</returns>
		/// <exception cref="T:System.NotSupportedException">The current dynamic type has not been created by calling the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.</exception>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x0011B09F File Offset: 0x0011929F
		public override bool IsSecuritySafeCritical
		{
			get
			{
				if (!this.IsCreated())
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
				}
				return this.m_bakedRuntimeType.IsSecuritySafeCritical;
			}
		}

		/// <summary>Gets a value that indicates whether the current type is transparent, and therefore cannot perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is security-transparent; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The current dynamic type has not been created by calling the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.</exception>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x0011B0C4 File Offset: 0x001192C4
		public override bool IsSecurityTransparent
		{
			get
			{
				if (!this.IsCreated())
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
				}
				return this.m_bakedRuntimeType.IsSecurityTransparent;
			}
		}

		/// <summary>Determines whether this type is derived from a specified type.</summary>
		/// <param name="c">A <see cref="T:System.Type" /> that is to be checked.</param>
		/// <returns>Read-only. Returns <see langword="true" /> if this type is the same as the type <paramref name="c" />, or is a subtype of type <paramref name="c" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004DB7 RID: 19895 RVA: 0x0011B0EC File Offset: 0x001192EC
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (TypeBuilder.IsTypeEqual(this, c))
			{
				return false;
			}
			Type type = this.BaseType;
			while (type != null)
			{
				if (TypeBuilder.IsTypeEqual(type, c))
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		/// <summary>Returns the underlying system type for this <see langword="TypeBuilder" />.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type is an enumeration, but there is no underlying system type.</exception>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x0011B12C File Offset: 0x0011932C
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.m_bakedRuntimeType != null)
				{
					return this.m_bakedRuntimeType;
				}
				if (!this.IsEnum)
				{
					return this;
				}
				if (this.m_enumUnderlyingType == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoUnderlyingTypeOnEnum"));
				}
				return this.m_enumUnderlyingType;
			}
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</returns>
		// Token: 0x06004DB9 RID: 19897 RVA: 0x0011B17C File Offset: 0x0011937C
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a <see langword="ref" /> parameter (<see langword="ByRef" /> in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a <see langword="ref" /> parameter (<see langword="ByRef" /> in Visual Basic).</returns>
		// Token: 0x06004DBA RID: 19898 RVA: 0x0011B18F File Offset: 0x0011938F
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array type whose element type is the current type, with a lower bound of zero.</returns>
		// Token: 0x06004DBB RID: 19899 RVA: 0x0011B1A2 File Offset: 0x001193A2
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents an array of the current type, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid array dimension.</exception>
		// Token: 0x06004DBC RID: 19900 RVA: 0x0011B1B8 File Offset: 0x001193B8
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
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		/// <summary>Returns all the custom attributes defined for this type.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Returns an array of objects representing all the custom attributes of this type.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />.</exception>
		// Token: 0x06004DBD RID: 19901 RVA: 0x0011B217 File Offset: 0x00119417
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, typeof(object) as RuntimeType, inherit);
		}

		/// <summary>Returns all the custom attributes of the current type that are assignable to a specified type.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>An array of custom attributes defined on the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		// Token: 0x06004DBE RID: 19902 RVA: 0x0011B24C File Offset: 0x0011944C
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, runtimeType, inherit);
		}

		/// <summary>Determines whether a custom attribute is applied to the current type.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instances of <paramref name="attributeType" />, or an attribute derived from <paramref name="attributeType" />, is defined on this type; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not defined.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		// Token: 0x06004DBF RID: 19903 RVA: 0x0011B2BC File Offset: 0x001194BC
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.IsCreated())
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "caType");
			}
			return CustomAttribute.IsDefined(this.m_bakedRuntimeType, runtimeType, inherit);
		}

		/// <summary>Gets a value that indicates the covariance and special constraints of the current generic type parameter.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x0011B32C File Offset: 0x0011952C
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return this.m_genParamAttributes;
			}
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x0011B334 File Offset: 0x00119534
		internal void SetInterfaces(params Type[] interfaces)
		{
			this.ThrowIfCreated();
			this.m_typeInterfaces = new List<Type>();
			if (interfaces != null)
			{
				this.m_typeInterfaces.AddRange(interfaces);
			}
		}

		/// <summary>Defines the generic type parameters for the current type, specifying their number and their names, and returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to set their constraints.</summary>
		/// <param name="names">An array of names for the generic type parameters.</param>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to define the constraints of the generic type parameters for the current type.</returns>
		/// <exception cref="T:System.InvalidOperationException">Generic type parameters have already been defined for this type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="names" /> is <see langword="null" />.  
		/// -or-  
		/// An element of <paramref name="names" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="names" /> is an empty array.</exception>
		// Token: 0x06004DC2 RID: 19906 RVA: 0x0011B358 File Offset: 0x00119558
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i] == null)
				{
					throw new ArgumentNullException("names");
				}
			}
			if (this.m_inst != null)
			{
				throw new InvalidOperationException();
			}
			this.m_inst = new GenericTypeParameterBuilder[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				this.m_inst[j] = new GenericTypeParameterBuilder(new TypeBuilder(names[j], j, this));
			}
			return this.m_inst;
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition, and returns the resulting constructed type.</summary>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type definition.</param>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent the definition of a generic type. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> returns <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is <see langword="null" />.  
		/// -or-  
		/// Any element of <paramref name="typeArguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Type.Module" /> property of any element of <paramref name="typeArguments" /> is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Reflection.Module.Assembly" /> property of the module of any element of <paramref name="typeArguments" /> is <see langword="null" />.</exception>
		// Token: 0x06004DC3 RID: 19907 RVA: 0x0011B3E2 File Offset: 0x001195E2
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			this.CheckContext(typeArguments);
			return TypeBuilderInstantiation.MakeGenericType(this, typeArguments);
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects. The elements of the array represent the type arguments of a generic type or the type parameters of a generic type definition.</returns>
		// Token: 0x06004DC4 RID: 19908 RVA: 0x0011B3F4 File Offset: 0x001195F4
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> represents a generic type definition from which other generic types can be constructed.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type definition; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x0011B409 File Offset: 0x00119609
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.IsGenericType;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if the type represented by the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object is generic; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x0011B411 File Offset: 0x00119611
		public override bool IsGenericType
		{
			get
			{
				return this.m_inst != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x0011B41C File Offset: 0x0011961C
		public override bool IsGenericParameter
		{
			get
			{
				return this.m_bIsGenParam;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x0011B424 File Offset: 0x00119624
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the position of a type parameter in the type parameter list of the generic type that declared the parameter.</summary>
		/// <returns>If the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter, the position of the type parameter in the type parameter list of the generic type that declared the parameter; otherwise, undefined.</returns>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x0011B427 File Offset: 0x00119627
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_genParamPos;
			}
		}

		/// <summary>Gets the method that declared the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> that represents the method that declared the current type, if the current type is a generic type parameter; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x0011B42F File Offset: 0x0011962F
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.m_declMeth;
			}
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current type can be obtained.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type definition from which the current type can be obtained.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not generic. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> returns <see langword="false" />.</exception>
		// Token: 0x06004DCB RID: 19915 RVA: 0x0011B437 File Offset: 0x00119637
		public override Type GetGenericTypeDefinition()
		{
			if (this.IsGenericTypeDefinition)
			{
				return this;
			}
			if (this.m_genTypeDef == null)
			{
				throw new InvalidOperationException();
			}
			return this.m_genTypeDef;
		}

		/// <summary>Specifies a given method body that implements a given method declaration, potentially with a different name.</summary>
		/// <param name="methodInfoBody">The method body to be used. This should be a <see langword="MethodBuilder" /> object.</param>
		/// <param name="methodInfoDeclaration">The method whose declaration is to be used.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodInfoBody" /> does not belong to this class.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="methodInfoBody" /> or <paramref name="methodInfoDeclaration" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The declaring type of <paramref name="methodInfoBody" /> is not the type represented by this <see cref="T:System.Reflection.Emit.TypeBuilder" />.</exception>
		// Token: 0x06004DCC RID: 19916 RVA: 0x0011B460 File Offset: 0x00119660
		[SecuritySafeCritical]
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineMethodOverrideNoLock(methodInfoBody, methodInfoDeclaration);
			}
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0011B4A4 File Offset: 0x001196A4
		[SecurityCritical]
		private void DefineMethodOverrideNoLock(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			if (methodInfoBody == null)
			{
				throw new ArgumentNullException("methodInfoBody");
			}
			if (methodInfoDeclaration == null)
			{
				throw new ArgumentNullException("methodInfoDeclaration");
			}
			this.ThrowIfCreated();
			if (methodInfoBody.DeclaringType != this)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_BadMethodImplBody"));
			}
			MethodToken methodTokenInternal = this.m_module.GetMethodTokenInternal(methodInfoBody);
			MethodToken methodTokenInternal2 = this.m_module.GetMethodTokenInternal(methodInfoDeclaration);
			TypeBuilder.DefineMethodImpl(this.m_module.GetNativeHandle(), this.m_tdType.Token, methodTokenInternal.Token, methodTokenInternal2.Token);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and method signature.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <returns>The defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DCE RID: 19918 RVA: 0x0011B53B File Offset: 0x0011973B
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		/// <summary>Adds a new method to the type, with the specified name and method attributes.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DCF RID: 19919 RVA: 0x0011B549 File Offset: 0x00119749
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, null, null);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and calling convention.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The calling convention of the method.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DD0 RID: 19920 RVA: 0x0011B556 File Offset: 0x00119756
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			return this.DefineMethod(name, attributes, callingConvention, null, null);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, and method signature.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The calling convention of the method.</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DD1 RID: 19921 RVA: 0x0011B564 File Offset: 0x00119764
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, method signature, and custom modifiers.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The calling convention of the method.</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> object representing the newly added method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).  
		///  -or-  
		///  The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DD2 RID: 19922 RVA: 0x0011B584 File Offset: 0x00119784
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			object syncRoot = this.SyncRoot;
			MethodBuilder methodBuilder;
			lock (syncRoot)
			{
				methodBuilder = this.DefineMethodNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			}
			return methodBuilder;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0011B5D8 File Offset: 0x001197D8
		private MethodBuilder DefineMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			this.CheckContext(new Type[] { returnType });
			this.CheckContext(new Type[][] { returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes });
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			if (parameterTypes != null)
			{
				if (parameterTypeOptionalCustomModifiers != null && parameterTypeOptionalCustomModifiers.Length != parameterTypes.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", new object[] { "parameterTypeOptionalCustomModifiers", "parameterTypes" }));
				}
				if (parameterTypeRequiredCustomModifiers != null && parameterTypeRequiredCustomModifiers.Length != parameterTypes.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", new object[] { "parameterTypeRequiredCustomModifiers", "parameterTypes" }));
				}
			}
			this.ThrowIfCreated();
			if (!this.m_isHiddenGlobalType && (this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
			}
			MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this, false);
			if (!this.m_isHiddenGlobalType && (methodBuilder.Attributes & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope && methodBuilder.Name.Equals(ConstructorInfo.ConstructorName))
			{
				this.m_constructorCount++;
			}
			this.m_listMethods.Add(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines the initializer for this type.</summary>
		/// <returns>Returns a type initializer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DD4 RID: 19924 RVA: 0x0011B754 File Offset: 0x00119954
		[SecuritySafeCritical]
		[ComVisible(true)]
		public ConstructorBuilder DefineTypeInitializer()
		{
			object syncRoot = this.SyncRoot;
			ConstructorBuilder constructorBuilder;
			lock (syncRoot)
			{
				constructorBuilder = this.DefineTypeInitializerNoLock();
			}
			return constructorBuilder;
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0011B798 File Offset: 0x00119998
		[SecurityCritical]
		private ConstructorBuilder DefineTypeInitializerNoLock()
		{
			this.ThrowIfCreated();
			MethodAttributes methodAttributes = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName;
			return new ConstructorBuilder(ConstructorInfo.TypeConstructorName, methodAttributes, CallingConventions.Standard, null, this.m_module, this);
		}

		/// <summary>Defines the default constructor. The constructor defined here will simply call the default constructor of the parent.</summary>
		/// <param name="attributes">A <see langword="MethodAttributes" /> object representing the attributes to be applied to the constructor.</param>
		/// <returns>Returns the constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">The parent type (base type) does not have a default constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DD6 RID: 19926 RVA: 0x0011B7C8 File Offset: 0x001199C8
		[ComVisible(true)]
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface"));
			}
			object syncRoot = this.SyncRoot;
			ConstructorBuilder constructorBuilder;
			lock (syncRoot)
			{
				constructorBuilder = this.DefineDefaultConstructorNoLock(attributes);
			}
			return constructorBuilder;
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x0011B828 File Offset: 0x00119A28
		private ConstructorBuilder DefineDefaultConstructorNoLock(MethodAttributes attributes)
		{
			ConstructorInfo constructorInfo = null;
			if (this.m_typeParent is TypeBuilderInstantiation)
			{
				Type type = this.m_typeParent.GetGenericTypeDefinition();
				if (type is TypeBuilder)
				{
					type = ((TypeBuilder)type).m_bakedRuntimeType;
				}
				if (type == null)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
				}
				Type type2 = type.MakeGenericType(this.m_typeParent.GetGenericArguments());
				if (type2 is TypeBuilderInstantiation)
				{
					constructorInfo = TypeBuilder.GetConstructor(type2, type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null));
				}
				else
				{
					constructorInfo = type2.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
				}
			}
			if (constructorInfo == null)
			{
				constructorInfo = this.m_typeParent.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			}
			if (constructorInfo == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoParentDefaultConstructor"));
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, null);
			this.m_constructorCount++;
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructorInfo);
			ilgenerator.Emit(OpCodes.Ret);
			constructorBuilder.m_isDefaultConstructor = true;
			return constructorBuilder;
		}

		/// <summary>Adds a new constructor to the type, with the given attributes and signature.</summary>
		/// <param name="attributes">The attributes of the constructor.</param>
		/// <param name="callingConvention">The calling convention of the constructor.</param>
		/// <param name="parameterTypes">The parameter types of the constructor.</param>
		/// <returns>The defined constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DD8 RID: 19928 RVA: 0x0011B943 File Offset: 0x00119B43
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes, signature, and custom modifiers.</summary>
		/// <param name="attributes">The attributes of the constructor.</param>
		/// <param name="callingConvention">The calling convention of the constructor.</param>
		/// <param name="parameterTypes">The parameter types of the constructor.</param>
		/// <param name="requiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="optionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>The defined constructor.</returns>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="requiredCustomModifiers" /> or <paramref name="optionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DD9 RID: 19929 RVA: 0x0011B950 File Offset: 0x00119B50
		[SecuritySafeCritical]
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & MethodAttributes.Static) != MethodAttributes.Static)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface"));
			}
			object syncRoot = this.SyncRoot;
			ConstructorBuilder constructorBuilder;
			lock (syncRoot)
			{
				constructorBuilder = this.DefineConstructorNoLock(attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			}
			return constructorBuilder;
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0011B9C0 File Offset: 0x00119BC0
		[SecurityCritical]
		private ConstructorBuilder DefineConstructorNoLock(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.CheckContext(parameterTypes);
			this.CheckContext(requiredCustomModifiers);
			this.CheckContext(optionalCustomModifiers);
			this.ThrowIfCreated();
			string text;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				text = ConstructorInfo.ConstructorName;
			}
			else
			{
				text = ConstructorInfo.TypeConstructorName;
			}
			attributes |= MethodAttributes.SpecialName;
			ConstructorBuilder constructorBuilder = new ConstructorBuilder(text, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, this.m_module, this);
			this.m_constructorCount++;
			return constructorBuilder;
		}

		/// <summary>Defines a <see langword="PInvoke" /> method given its name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the <see langword="PInvoke" /> flags.</summary>
		/// <param name="name">The name of the <see langword="PInvoke" /> method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="dllName">The name of the DLL in which the <see langword="PInvoke" /> method is defined.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The method's calling convention.</param>
		/// <param name="returnType">The method's return type.</param>
		/// <param name="parameterTypes">The types of the method's parameters.</param>
		/// <param name="nativeCallConv">The native calling convention.</param>
		/// <param name="nativeCharSet">The method's native character set.</param>
		/// <returns>The defined <see langword="PInvoke" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">The method is not static.  
		///  -or-  
		///  The parent type is an interface.  
		///  -or-  
		///  The method is abstract.  
		///  -or-  
		///  The method was previously defined.  
		///  -or-  
		///  The length of <paramref name="name" /> or <paramref name="dllName" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DDB RID: 19931 RVA: 0x0011BA2C File Offset: 0x00119C2C
		[SecuritySafeCritical]
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethodHelper(name, dllName, name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a <see langword="PInvoke" /> method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the <see langword="PInvoke" /> flags.</summary>
		/// <param name="name">The name of the <see langword="PInvoke" /> method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="dllName">The name of the DLL in which the <see langword="PInvoke" /> method is defined.</param>
		/// <param name="entryName">The name of the entry point in the DLL.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The method's calling convention.</param>
		/// <param name="returnType">The method's return type.</param>
		/// <param name="parameterTypes">The types of the method's parameters.</param>
		/// <param name="nativeCallConv">The native calling convention.</param>
		/// <param name="nativeCharSet">The method's native character set.</param>
		/// <returns>The defined <see langword="PInvoke" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">The method is not static.  
		///  -or-  
		///  The parent type is an interface.  
		///  -or-  
		///  The method is abstract.  
		///  -or-  
		///  The method was previously defined.  
		///  -or-  
		///  The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DDC RID: 19932 RVA: 0x0011BA54 File Offset: 0x00119C54
		[SecuritySafeCritical]
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a <see langword="PInvoke" /> method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, the <see langword="PInvoke" /> flags, and custom modifiers for the parameters and return type.</summary>
		/// <param name="name">The name of the <see langword="PInvoke" /> method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="dllName">The name of the DLL in which the <see langword="PInvoke" /> method is defined.</param>
		/// <param name="entryName">The name of the entry point in the DLL.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The method's calling convention.</param>
		/// <param name="returnType">The method's return type.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the method's parameters.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="nativeCallConv">The native calling convention.</param>
		/// <param name="nativeCharSet">The method's native character set.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the defined <see langword="PInvoke" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">The method is not static.  
		///  -or-  
		///  The parent type is an interface.  
		///  -or-  
		///  The method is abstract.  
		///  -or-  
		///  The method was previously defined.  
		///  -or-  
		///  The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero.  
		///  -or-  
		///  The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004DDD RID: 19933 RVA: 0x0011BA7C File Offset: 0x00119C7C
		[SecuritySafeCritical]
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a nested type, given its name.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004DDE RID: 19934 RVA: 0x0011BAA8 File Offset: 0x00119CA8
		[SecuritySafeCritical]
		public TypeBuilder DefineNestedType(string name)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				typeBuilder = this.DefineNestedTypeNoLock(name, TypeAttributes.NestedPrivate, null, null, PackingSize.Unspecified, 0);
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, the type that it extends, and the interfaces that it implements.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="interfaces">The interfaces that the nested type implements.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// An element of the <paramref name="interfaces" /> array is <see langword="null" />.</exception>
		// Token: 0x06004DDF RID: 19935 RVA: 0x0011BAF0 File Offset: 0x00119CF0
		[SecuritySafeCritical]
		[ComVisible(true)]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				this.CheckContext(new Type[] { parent });
				this.CheckContext(interfaces);
				typeBuilder = this.DefineNestedTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, and the type that it extends.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004DE0 RID: 19936 RVA: 0x0011BB54 File Offset: 0x00119D54
		[SecuritySafeCritical]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				typeBuilder = this.DefineNestedTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, 0);
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name and attributes.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004DE1 RID: 19937 RVA: 0x0011BB9C File Offset: 0x00119D9C
		[SecuritySafeCritical]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				typeBuilder = this.DefineNestedTypeNoLock(name, attr, null, null, PackingSize.Unspecified, 0);
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, the total size of the type, and the type that it extends.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="typeSize">The total size of the type.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004DE2 RID: 19938 RVA: 0x0011BBE4 File Offset: 0x00119DE4
		[SecuritySafeCritical]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				typeBuilder = this.DefineNestedTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, typeSize);
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, the type that it extends, and the packing size.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="packSize">The packing size of the type.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004DE3 RID: 19939 RVA: 0x0011BC30 File Offset: 0x00119E30
		[SecuritySafeCritical]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				typeBuilder = this.DefineNestedTypeNoLock(name, attr, parent, null, packSize, 0);
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, size, and the type that it extends.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded null values.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="packSize">The packing size of the type.</param>
		/// <param name="typeSize">The total size of the type.</param>
		/// <returns>The defined nested type.</returns>
		// Token: 0x06004DE4 RID: 19940 RVA: 0x0011BC7C File Offset: 0x00119E7C
		[SecuritySafeCritical]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder typeBuilder;
			lock (syncRoot)
			{
				typeBuilder = this.DefineNestedTypeNoLock(name, attr, parent, null, packSize, typeSize);
			}
			return typeBuilder;
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0011BCC8 File Offset: 0x00119EC8
		[SecurityCritical]
		private TypeBuilder DefineNestedTypeNoLock(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
		{
			return new TypeBuilder(name, attr, parent, interfaces, this.m_module, packSize, typeSize, this);
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, and field type.</summary>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls.</param>
		/// <param name="type">The type of the field</param>
		/// <param name="attributes">The attributes of the field.</param>
		/// <returns>The defined field.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.  
		///  -or-  
		///  <paramref name="type" /> is System.Void.  
		///  -or-  
		///  A total size was specified for the parent class of this field.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DE6 RID: 19942 RVA: 0x0011BCDF File Offset: 0x00119EDF
		public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
		{
			return this.DefineField(fieldName, type, null, null, attributes);
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, field type, and custom modifiers.</summary>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls.</param>
		/// <param name="type">The type of the field</param>
		/// <param name="requiredCustomModifiers">An array of types representing the required custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="optionalCustomModifiers">An array of types representing the optional custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="attributes">The attributes of the field.</param>
		/// <returns>The defined field.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.  
		///  -or-  
		///  <paramref name="type" /> is System.Void.  
		///  -or-  
		///  A total size was specified for the parent class of this field.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DE7 RID: 19943 RVA: 0x0011BCEC File Offset: 0x00119EEC
		[SecuritySafeCritical]
		public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder fieldBuilder;
			lock (syncRoot)
			{
				fieldBuilder = this.DefineFieldNoLock(fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
			}
			return fieldBuilder;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0011BD38 File Offset: 0x00119F38
		[SecurityCritical]
		private FieldBuilder DefineFieldNoLock(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			this.ThrowIfCreated();
			this.CheckContext(new Type[] { type });
			this.CheckContext(requiredCustomModifiers);
			if (this.m_enumUnderlyingType == null && this.IsEnum && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
			{
				this.m_enumUnderlyingType = type;
			}
			return new FieldBuilder(this, fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
		}

		/// <summary>Defines initialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="data">The blob of data.</param>
		/// <param name="attributes">The attributes for the field.</param>
		/// <returns>A field to reference the data.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The size of the data is less than or equal to zero, or greater than or equal to 0x3f0000.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been previously called.</exception>
		// Token: 0x06004DE9 RID: 19945 RVA: 0x0011BD94 File Offset: 0x00119F94
		[SecuritySafeCritical]
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder fieldBuilder;
			lock (syncRoot)
			{
				fieldBuilder = this.DefineInitializedDataNoLock(name, data, attributes);
			}
			return fieldBuilder;
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0011BDDC File Offset: 0x00119FDC
		[SecurityCritical]
		private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.DefineDataHelper(name, data, data.Length, attributes);
		}

		/// <summary>Defines an uninitialized data field in the <see langword=".sdata" /> section of the portable executable (PE) file.</summary>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="size">The size of the data field.</param>
		/// <param name="attributes">The attributes for the field.</param>
		/// <returns>A field to reference the data.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero.  
		///  -or-  
		///  <paramref name="size" /> is less than or equal to zero, or greater than or equal to 0x003f0000.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DEB RID: 19947 RVA: 0x0011BDF8 File Offset: 0x00119FF8
		[SecuritySafeCritical]
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder fieldBuilder;
			lock (syncRoot)
			{
				fieldBuilder = this.DefineUninitializedDataNoLock(name, size, attributes);
			}
			return fieldBuilder;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0011BE40 File Offset: 0x0011A040
		[SecurityCritical]
		private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
		{
			return this.DefineDataHelper(name, null, size, attributes);
		}

		/// <summary>Adds a new property to the type, with the given name and property signature.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DED RID: 19949 RVA: 0x0011BE4C File Offset: 0x0011A04C
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new property to the type, with the given name, attributes, calling convention, and property signature.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="callingConvention">The calling convention of the property accessors.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DEE RID: 19950 RVA: 0x0011BE68 File Offset: 0x0011A068
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new property to the type, with the given name, property signature, and custom modifiers.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" /></exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DEF RID: 19951 RVA: 0x0011BE88 File Offset: 0x0011A088
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
		}

		/// <summary>Adds a new property to the type, with the given name, calling convention, property signature, and custom modifiers.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="callingConvention">The calling convention of the property accessors.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DF0 RID: 19952 RVA: 0x0011BEAC File Offset: 0x0011A0AC
		[SecuritySafeCritical]
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			object syncRoot = this.SyncRoot;
			PropertyBuilder propertyBuilder;
			lock (syncRoot)
			{
				propertyBuilder = this.DefinePropertyNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			}
			return propertyBuilder;
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0011BF00 File Offset: 0x0011A100
		[SecurityCritical]
		private PropertyBuilder DefinePropertyNoLock(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			this.CheckContext(new Type[] { returnType });
			this.CheckContext(new Type[][] { returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes });
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			this.ThrowIfCreated();
			SignatureHelper propertySigHelper = SignatureHelper.GetPropertySigHelper(this.m_module, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			int num;
			byte[] array = propertySigHelper.InternalGetSignature(out num);
			PropertyToken propertyToken = new PropertyToken(TypeBuilder.DefineProperty(this.m_module.GetNativeHandle(), this.m_tdType.Token, name, attributes, array, num));
			return new PropertyBuilder(this.m_module, name, propertySigHelper, attributes, returnType, propertyToken, this);
		}

		/// <summary>Adds a new event to the type, with the given name, attributes and event type.</summary>
		/// <param name="name">The name of the event. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the event.</param>
		/// <param name="eventtype">The type of the event.</param>
		/// <returns>The defined event.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="eventtype" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DF2 RID: 19954 RVA: 0x0011BFD4 File Offset: 0x0011A1D4
		[SecuritySafeCritical]
		public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
		{
			object syncRoot = this.SyncRoot;
			EventBuilder eventBuilder;
			lock (syncRoot)
			{
				eventBuilder = this.DefineEventNoLock(name, attributes, eventtype);
			}
			return eventBuilder;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0011C01C File Offset: 0x0011A21C
		[SecurityCritical]
		private EventBuilder DefineEventNoLock(string name, EventAttributes attributes, Type eventtype)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			this.CheckContext(new Type[] { eventtype });
			this.ThrowIfCreated();
			int token = this.m_module.GetTypeTokenInternal(eventtype).Token;
			EventToken eventToken = new EventToken(TypeBuilder.DefineEvent(this.m_module.GetNativeHandle(), this.m_tdType.Token, name, attributes, token));
			return new EventBuilder(this.m_module, name, attributes, this, eventToken);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x06004DF4 RID: 19956 RVA: 0x0011C0D4 File Offset: 0x0011A2D4
		[SecuritySafeCritical]
		public TypeInfo CreateTypeInfo()
		{
			object syncRoot = this.SyncRoot;
			TypeInfo typeInfo;
			lock (syncRoot)
			{
				typeInfo = this.CreateTypeNoLock();
			}
			return typeInfo;
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for the class. After defining fields and methods on the class, <see langword="CreateType" /> is called in order to load its <see langword="Type" /> object.</summary>
		/// <returns>Returns the new <see cref="T:System.Type" /> object for this class.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enclosing type has not been created.  
		///  -or-  
		///  This type is non-abstract and contains an abstract method.  
		///  -or-  
		///  This type is not an abstract class or an interface and has a method without a method body.</exception>
		/// <exception cref="T:System.NotSupportedException">The type contains invalid Microsoft intermediate language (MSIL) code.  
		///  -or-  
		///  The branch target is specified using a 1-byte offset, but the target is at a distance greater than 127 bytes from the branch.</exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. For example, it contains a <see langword="static" /> method that has the calling convention <see cref="F:System.Reflection.CallingConventions.HasThis" />.</exception>
		// Token: 0x06004DF5 RID: 19957 RVA: 0x0011C118 File Offset: 0x0011A318
		[SecuritySafeCritical]
		public Type CreateType()
		{
			object syncRoot = this.SyncRoot;
			Type type;
			lock (syncRoot)
			{
				type = this.CreateTypeNoLock();
			}
			return type;
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x0011C15C File Offset: 0x0011A35C
		internal void CheckContext(params Type[][] typess)
		{
			this.m_module.CheckContext(typess);
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x0011C16A File Offset: 0x0011A36A
		internal void CheckContext(params Type[] types)
		{
			this.m_module.CheckContext(types);
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0011C178 File Offset: 0x0011A378
		[SecurityCritical]
		private TypeInfo CreateTypeNoLock()
		{
			if (this.IsCreated())
			{
				return this.m_bakedRuntimeType;
			}
			this.ThrowIfCreated();
			if (this.m_typeInterfaces == null)
			{
				this.m_typeInterfaces = new List<Type>();
			}
			int[] array = new int[this.m_typeInterfaces.Count];
			for (int i = 0; i < this.m_typeInterfaces.Count; i++)
			{
				array[i] = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[i]).Token;
			}
			int num = 0;
			if (this.m_typeParent != null)
			{
				num = this.m_module.GetTypeTokenInternal(this.m_typeParent).Token;
			}
			if (this.IsGenericParameter)
			{
				int[] array2;
				if (this.m_typeParent != null)
				{
					array2 = new int[this.m_typeInterfaces.Count + 2];
					array2[array2.Length - 2] = num;
				}
				else
				{
					array2 = new int[this.m_typeInterfaces.Count + 1];
				}
				for (int j = 0; j < this.m_typeInterfaces.Count; j++)
				{
					array2[j] = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[j]).Token;
				}
				int num2 = ((this.m_declMeth == null) ? this.m_DeclaringType.m_tdType.Token : this.m_declMeth.GetToken().Token);
				this.m_tdType = new TypeToken(TypeBuilder.DefineGenericParam(this.m_module.GetNativeHandle(), this.m_strName, num2, this.m_genParamAttributes, this.m_genParamPos, array2));
				if (this.m_ca != null)
				{
					foreach (TypeBuilder.CustAttr custAttr in this.m_ca)
					{
						custAttr.Bake(this.m_module, this.MetadataTokenInternal);
					}
				}
				this.m_hasBeenCreated = true;
				return this;
			}
			if ((this.m_tdType.Token & 16777215) != 0 && (num & 16777215) != 0)
			{
				TypeBuilder.SetParentType(this.m_module.GetNativeHandle(), this.m_tdType.Token, num);
			}
			if (this.m_inst != null)
			{
				foreach (GenericTypeParameterBuilder type in this.m_inst)
				{
					if (type is GenericTypeParameterBuilder)
					{
						((GenericTypeParameterBuilder)type).m_type.CreateType();
					}
				}
			}
			if (!this.m_isHiddenGlobalType && this.m_constructorCount == 0 && (this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !base.IsValueType && (this.m_iAttr & (TypeAttributes.Abstract | TypeAttributes.Sealed)) != (TypeAttributes.Abstract | TypeAttributes.Sealed))
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			int count = this.m_listMethods.Count;
			for (int l = 0; l < count; l++)
			{
				MethodBuilder methodBuilder = this.m_listMethods[l];
				if (methodBuilder.IsGenericMethodDefinition)
				{
					methodBuilder.GetToken();
				}
				MethodAttributes attributes = methodBuilder.Attributes;
				if ((methodBuilder.GetMethodImplementationFlags() & (MethodImplAttributes)135) == MethodImplAttributes.IL && (attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
				{
					int num3;
					byte[] localSignature = methodBuilder.GetLocalSignature(out num3);
					if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope && (this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadTypeAttributesNotAbstract"));
					}
					byte[] array3 = methodBuilder.GetBody();
					if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
					{
						if (array3 != null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadMethodBody"));
						}
					}
					else if (array3 == null || array3.Length == 0)
					{
						if (methodBuilder.m_ilGenerator != null)
						{
							methodBuilder.CreateMethodBodyHelper(methodBuilder.GetILGenerator());
						}
						array3 = methodBuilder.GetBody();
						if ((array3 == null || array3.Length == 0) && !methodBuilder.m_canBeRuntimeImpl)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", new object[] { methodBuilder.Name }));
						}
					}
					int maxStack = methodBuilder.GetMaxStack();
					ExceptionHandler[] exceptionHandlers = methodBuilder.GetExceptionHandlers();
					int[] tokenFixups = methodBuilder.GetTokenFixups();
					TypeBuilder.SetMethodIL(this.m_module.GetNativeHandle(), methodBuilder.GetToken().Token, methodBuilder.InitLocals, array3, (array3 != null) ? array3.Length : 0, localSignature, num3, maxStack, exceptionHandlers, (exceptionHandlers != null) ? exceptionHandlers.Length : 0, tokenFixups, (tokenFixups != null) ? tokenFixups.Length : 0);
					if (this.m_module.ContainingAssemblyBuilder.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
					{
						methodBuilder.ReleaseBakedStructures();
					}
				}
			}
			this.m_hasBeenCreated = true;
			RuntimeType runtimeType = null;
			TypeBuilder.TermCreateClass(this.m_module.GetNativeHandle(), this.m_tdType.Token, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			if (!this.m_isHiddenGlobalType)
			{
				this.m_bakedRuntimeType = runtimeType;
				if (this.m_DeclaringType != null && this.m_DeclaringType.m_bakedRuntimeType != null)
				{
					this.m_DeclaringType.m_bakedRuntimeType.InvalidateCachedNestedType();
				}
				return runtimeType;
			}
			return null;
		}

		/// <summary>Retrieves the total size of a type.</summary>
		/// <returns>Read-only. Retrieves this type's total size.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x0011C650 File Offset: 0x0011A850
		public int Size
		{
			get
			{
				return this.m_iTypeSize;
			}
		}

		/// <summary>Retrieves the packing size of this type.</summary>
		/// <returns>Read-only. Retrieves the packing size of this type.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x0011C658 File Offset: 0x0011A858
		public PackingSize PackingSize
		{
			get
			{
				return this.m_iPackingSize;
			}
		}

		/// <summary>Sets the base type of the type currently under construction.</summary>
		/// <param name="parent">The new base type.</param>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  <paramref name="parent" /> is <see langword="null" />, and the current instance represents an interface whose attributes do not include <see cref="F:System.Reflection.TypeAttributes.Abstract" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> is an interface. This exception condition is new in the .NET Framework version 2.0.</exception>
		// Token: 0x06004DFB RID: 19963 RVA: 0x0011C660 File Offset: 0x0011A860
		public void SetParent(Type parent)
		{
			this.ThrowIfCreated();
			if (parent != null)
			{
				this.CheckContext(new Type[] { parent });
				if (parent.IsInterface)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_CannotSetParentToInterface"));
				}
				this.m_typeParent = parent;
				return;
			}
			else
			{
				if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.ClassSemanticsMask)
				{
					this.m_typeParent = typeof(object);
					return;
				}
				if ((this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInterfaceNotAbstract"));
				}
				this.m_typeParent = null;
				return;
			}
		}

		/// <summary>Adds an interface that this type implements.</summary>
		/// <param name="interfaceType">The interface that this type implements.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004DFC RID: 19964 RVA: 0x0011C6F0 File Offset: 0x0011A8F0
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void AddInterfaceImplementation(Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			this.CheckContext(new Type[] { interfaceType });
			this.ThrowIfCreated();
			TypeToken typeTokenInternal = this.m_module.GetTypeTokenInternal(interfaceType);
			TypeBuilder.AddInterfaceImpl(this.m_module.GetNativeHandle(), this.m_tdType.Token, typeTokenInternal.Token);
			this.m_typeInterfaces.Add(interfaceType);
		}

		/// <summary>Adds declarative security to this type.</summary>
		/// <param name="action">The security action to be taken such as Demand, Assert, and so on.</param>
		/// <param name="pset">The set of permissions the action applies to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="action" /> is invalid (<see langword="RequestMinimum" />, <see langword="RequestOptional" />, and <see langword="RequestRefuse" /> are invalid).</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The permission set <paramref name="pset" /> contains an action that was added earlier by <see langword="AddDeclarativeSecurity" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is <see langword="null" />.</exception>
		// Token: 0x06004DFD RID: 19965 RVA: 0x0011C764 File Offset: 0x0011A964
		[SecuritySafeCritical]
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.AddDeclarativeSecurityNoLock(action, pset);
			}
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0011C7A8 File Offset: 0x0011A9A8
		[SecurityCritical]
		private void AddDeclarativeSecurityNoLock(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			this.ThrowIfCreated();
			byte[] array = null;
			int num = 0;
			if (!pset.IsEmpty())
			{
				array = pset.EncodeXml();
				num = array.Length;
			}
			TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.m_tdType.Token, action, array, num);
		}

		/// <summary>Returns the type token of this type.</summary>
		/// <returns>Read-only. Returns the <see langword="TypeToken" /> of this type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004DFF RID: 19967 RVA: 0x0011C82E File Offset: 0x0011AA2E
		public TypeToken TypeToken
		{
			get
			{
				if (this.IsGenericParameter)
				{
					this.ThrowIfCreated();
				}
				return this.m_tdType;
			}
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004E00 RID: 19968 RVA: 0x0011C844 File Offset: 0x0011AA44
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.DefineCustomAttribute(this.m_module, this.m_tdType.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004E01 RID: 19969 RVA: 0x0011C8A0 File Offset: 0x0011AAA0
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute(this.m_module, this.m_tdType.Token);
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004E02 RID: 19970 RVA: 0x0011C8C7 File Offset: 0x0011AAC7
		void _TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004E03 RID: 19971 RVA: 0x0011C8CE File Offset: 0x0011AACE
		void _TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x06004E04 RID: 19972 RVA: 0x0011C8D5 File Offset: 0x0011AAD5
		void _TypeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x06004E05 RID: 19973 RVA: 0x0011C8DC File Offset: 0x0011AADC
		void _TypeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents that total size for the type is not specified.</summary>
		// Token: 0x040021BA RID: 8634
		public const int UnspecifiedTypeSize = 0;

		// Token: 0x040021BB RID: 8635
		private List<TypeBuilder.CustAttr> m_ca;

		// Token: 0x040021BC RID: 8636
		private TypeToken m_tdType;

		// Token: 0x040021BD RID: 8637
		private ModuleBuilder m_module;

		// Token: 0x040021BE RID: 8638
		private string m_strName;

		// Token: 0x040021BF RID: 8639
		private string m_strNameSpace;

		// Token: 0x040021C0 RID: 8640
		private string m_strFullQualName;

		// Token: 0x040021C1 RID: 8641
		private Type m_typeParent;

		// Token: 0x040021C2 RID: 8642
		private List<Type> m_typeInterfaces;

		// Token: 0x040021C3 RID: 8643
		private TypeAttributes m_iAttr;

		// Token: 0x040021C4 RID: 8644
		private GenericParameterAttributes m_genParamAttributes;

		// Token: 0x040021C5 RID: 8645
		internal List<MethodBuilder> m_listMethods;

		// Token: 0x040021C6 RID: 8646
		internal int m_lastTokenizedMethod;

		// Token: 0x040021C7 RID: 8647
		private int m_constructorCount;

		// Token: 0x040021C8 RID: 8648
		private int m_iTypeSize;

		// Token: 0x040021C9 RID: 8649
		private PackingSize m_iPackingSize;

		// Token: 0x040021CA RID: 8650
		private TypeBuilder m_DeclaringType;

		// Token: 0x040021CB RID: 8651
		private Type m_enumUnderlyingType;

		// Token: 0x040021CC RID: 8652
		internal bool m_isHiddenGlobalType;

		// Token: 0x040021CD RID: 8653
		private bool m_hasBeenCreated;

		// Token: 0x040021CE RID: 8654
		private RuntimeType m_bakedRuntimeType;

		// Token: 0x040021CF RID: 8655
		private int m_genParamPos;

		// Token: 0x040021D0 RID: 8656
		private GenericTypeParameterBuilder[] m_inst;

		// Token: 0x040021D1 RID: 8657
		private bool m_bIsGenParam;

		// Token: 0x040021D2 RID: 8658
		private MethodBuilder m_declMeth;

		// Token: 0x040021D3 RID: 8659
		private TypeBuilder m_genTypeDef;

		// Token: 0x02000C3D RID: 3133
		private class CustAttr
		{
			// Token: 0x0600707F RID: 28799 RVA: 0x00184A34 File Offset: 0x00182C34
			public CustAttr(ConstructorInfo con, byte[] binaryAttribute)
			{
				if (con == null)
				{
					throw new ArgumentNullException("con");
				}
				if (binaryAttribute == null)
				{
					throw new ArgumentNullException("binaryAttribute");
				}
				this.m_con = con;
				this.m_binaryAttribute = binaryAttribute;
			}

			// Token: 0x06007080 RID: 28800 RVA: 0x00184A6C File Offset: 0x00182C6C
			public CustAttr(CustomAttributeBuilder customBuilder)
			{
				if (customBuilder == null)
				{
					throw new ArgumentNullException("customBuilder");
				}
				this.m_customBuilder = customBuilder;
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x00184A8C File Offset: 0x00182C8C
			[SecurityCritical]
			public void Bake(ModuleBuilder module, int token)
			{
				if (this.m_customBuilder == null)
				{
					TypeBuilder.DefineCustomAttribute(module, token, module.GetConstructorToken(this.m_con).Token, this.m_binaryAttribute, false, false);
					return;
				}
				this.m_customBuilder.CreateCustomAttribute(module, token);
			}

			// Token: 0x04003757 RID: 14167
			private ConstructorInfo m_con;

			// Token: 0x04003758 RID: 14168
			private byte[] m_binaryAttribute;

			// Token: 0x04003759 RID: 14169
			private CustomAttributeBuilder m_customBuilder;
		}
	}
}

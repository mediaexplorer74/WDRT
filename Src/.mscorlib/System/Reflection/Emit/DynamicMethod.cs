using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic method that can be compiled, executed, and discarded. Discarded methods are available for garbage collection.</summary>
	// Token: 0x02000636 RID: 1590
	[ComVisible(true)]
	public sealed class DynamicMethod : MethodInfo
	{
		// Token: 0x06004A49 RID: 19017 RVA: 0x0010DEAD File Offset: 0x0010C0AD
		private DynamicMethod()
		{
		}

		/// <summary>Initializes an anonymously hosted dynamic method, specifying the method name, return type, and parameter types.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A4A RID: 19018 RVA: 0x0010DEB8 File Offset: 0x0010C0B8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, false, true, ref stackCrawlMark);
		}

		/// <summary>Initializes an anonymously hosted dynamic method, specifying the method name, return type, parameter types, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="restrictedSkipVisibility">
		///   <see langword="true" /> to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method, with this restriction: the trust level of the assemblies that contain those types and members must be equal to or less than the trust level of the call stack that emits the dynamic method; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A4B RID: 19019 RVA: 0x0010DEE0 File Offset: 0x0010C0E0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, restrictedSkipVisibility, true, ref stackCrawlMark);
		}

		/// <summary>Creates a dynamic method that is global to a module, specifying the method name, return type, parameter types, and module.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="m">A <see cref="T:System.Reflection.Module" /> representing the module with which the dynamic method is to be logically associated.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.  
		///  -or-  
		///  <paramref name="m" /> is a module that provides anonymous hosting for dynamic methods.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="m" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A4C RID: 19020 RVA: 0x0010DF08 File Offset: 0x0010C108
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(m, ref stackCrawlMark, false);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, m, false, false, ref stackCrawlMark);
		}

		/// <summary>Creates a dynamic method that is global to a module, specifying the method name, return type, parameter types, module, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="m">A <see cref="T:System.Reflection.Module" /> representing the module with which the dynamic method is to be logically associated.</param>
		/// <param name="skipVisibility">
		///   <see langword="true" /> to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.  
		///  -or-  
		///  <paramref name="m" /> is a module that provides anonymous hosting for dynamic methods.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="m" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A4D RID: 19021 RVA: 0x0010DF3C File Offset: 0x0010C13C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(m, ref stackCrawlMark, skipVisibility);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, m, skipVisibility, false, ref stackCrawlMark);
		}

		/// <summary>Creates a dynamic method that is global to a module, specifying the method name, attributes, calling convention, return type, parameter types, module, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.MethodAttributes" /> values that specifies the attributes of the dynamic method. The only combination allowed is <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the dynamic method. Must be <see cref="F:System.Reflection.CallingConventions.Standard" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="m">A <see cref="T:System.Reflection.Module" /> representing the module with which the dynamic method is to be logically associated.</param>
		/// <param name="skipVisibility">
		///   <see langword="true" /> to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.  
		///  -or-  
		///  <paramref name="m" /> is a module that provides anonymous hosting for dynamic methods.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="m" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="attributes" /> is a combination of flags other than <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.  
		/// -or-  
		/// <paramref name="callingConvention" /> is not <see cref="F:System.Reflection.CallingConventions.Standard" />.  
		/// -or-  
		/// <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A4E RID: 19022 RVA: 0x0010DF74 File Offset: 0x0010C174
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(m, ref stackCrawlMark, skipVisibility);
			this.Init(name, attributes, callingConvention, returnType, parameterTypes, null, m, skipVisibility, false, ref stackCrawlMark);
		}

		/// <summary>Creates a dynamic method, specifying the method name, return type, parameter types, and the type with which the dynamic method is logically associated.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.  
		///  -or-  
		///  <paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="owner" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is <see langword="null" />, or is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A4F RID: 19023 RVA: 0x0010DFAC File Offset: 0x0010C1AC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(owner, ref stackCrawlMark, false);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, null, false, false, ref stackCrawlMark);
		}

		/// <summary>Creates a dynamic method, specifying the method name, return type, parameter types, the type with which the dynamic method is logically associated, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type.</param>
		/// <param name="skipVisibility">
		///   <see langword="true" /> to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.  
		///  -or-  
		///  <paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="owner" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is <see langword="null" />, or is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A50 RID: 19024 RVA: 0x0010DFE0 File Offset: 0x0010C1E0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(owner, ref stackCrawlMark, skipVisibility);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, null, skipVisibility, false, ref stackCrawlMark);
		}

		/// <summary>Creates a dynamic method, specifying the method name, attributes, calling convention, return type, parameter types, the type with which the dynamic method is logically associated, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be <see langword="null" />.</param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.MethodAttributes" /> values that specifies the attributes of the dynamic method. The only combination allowed is <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the dynamic method. Must be <see cref="F:System.Reflection.CallingConventions.Standard" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or <see langword="null" /> if the method has no return type.</param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or <see langword="null" /> if the method has no parameters.</param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type.</param>
		/// <param name="skipVisibility">
		///   <see langword="true" /> to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is <see langword="null" /> or <see cref="T:System.Void" />.  
		///  -or-  
		///  <paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="owner" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="attributes" /> is a combination of flags other than <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.  
		/// -or-  
		/// <paramref name="callingConvention" /> is not <see cref="F:System.Reflection.CallingConventions.Standard" />.  
		/// -or-  
		/// <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x06004A51 RID: 19025 RVA: 0x0010E018 File Offset: 0x0010C218
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(owner, ref stackCrawlMark, skipVisibility);
			this.Init(name, attributes, callingConvention, returnType, parameterTypes, owner, null, skipVisibility, false, ref stackCrawlMark);
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x0010E050 File Offset: 0x0010C250
		private static void CheckConsistency(MethodAttributes attributes, CallingConventions callingConvention)
		{
			if ((attributes & ~MethodAttributes.MemberAccessMask) != MethodAttributes.Static)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
			if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
			if (callingConvention != CallingConventions.Standard && callingConvention != CallingConventions.VarArgs)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
			if (callingConvention == CallingConventions.VarArgs)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x0010E0B8 File Offset: 0x0010C2B8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static RuntimeModule GetDynamicMethodsModule()
		{
			if (DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != null)
			{
				return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
			}
			object obj = DynamicMethod.s_anonymouslyHostedDynamicMethodsModuleLock;
			lock (obj)
			{
				if (DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != null)
				{
					return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
				}
				ConstructorInfo constructor = typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes);
				CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, EmptyArray<object>.Value);
				List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
				list.Add(customAttributeBuilder);
				ConstructorInfo constructor2 = typeof(SecurityRulesAttribute).GetConstructor(new Type[] { typeof(SecurityRuleSet) });
				CustomAttributeBuilder customAttributeBuilder2 = new CustomAttributeBuilder(constructor2, new object[] { SecurityRuleSet.Level1 });
				list.Add(customAttributeBuilder2);
				AssemblyName assemblyName = new AssemblyName("Anonymously Hosted DynamicMethods Assembly");
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
				AssemblyBuilder assemblyBuilder = AssemblyBuilder.InternalDefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, null, null, null, null, null, ref stackCrawlMark, list, SecurityContextSource.CurrentAssembly);
				AppDomain.PublishAnonymouslyHostedDynamicMethodsAssembly(assemblyBuilder.GetNativeHandle());
				DynamicMethod.s_anonymouslyHostedDynamicMethodsModule = (InternalModuleBuilder)assemblyBuilder.ManifestModule;
			}
			return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x0010E1E8 File Offset: 0x0010C3E8
		[SecurityCritical]
		private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] signature, Type owner, Module m, bool skipVisibility, bool transparentMethod, ref StackCrawlMark stackMark)
		{
			DynamicMethod.CheckConsistency(attributes, callingConvention);
			if (signature != null)
			{
				this.m_parameterTypes = new RuntimeType[signature.Length];
				for (int i = 0; i < signature.Length; i++)
				{
					if (signature[i] == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
					}
					this.m_parameterTypes[i] = signature[i].UnderlyingSystemType as RuntimeType;
					if (this.m_parameterTypes[i] == null || this.m_parameterTypes[i] == null || this.m_parameterTypes[i] == (RuntimeType)typeof(void))
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
					}
				}
			}
			else
			{
				this.m_parameterTypes = new RuntimeType[0];
			}
			this.m_returnType = ((returnType == null) ? ((RuntimeType)typeof(void)) : (returnType.UnderlyingSystemType as RuntimeType));
			if (this.m_returnType == null || this.m_returnType == null || this.m_returnType.IsByRef)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_InvalidTypeInRetType"));
			}
			if (transparentMethod)
			{
				this.m_module = DynamicMethod.GetDynamicMethodsModule();
				if (skipVisibility)
				{
					this.m_restrictedSkipVisibility = true;
				}
				this.m_creationContext = CompressedStack.Capture();
			}
			else
			{
				if (m != null)
				{
					this.m_module = m.ModuleHandle.GetRuntimeModule();
				}
				else
				{
					RuntimeType runtimeType = null;
					if (owner != null)
					{
						runtimeType = owner.UnderlyingSystemType as RuntimeType;
					}
					if (runtimeType != null)
					{
						if (runtimeType.HasElementType || runtimeType.ContainsGenericParameters || runtimeType.IsGenericParameter || runtimeType.IsInterface)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeForDynamicMethod"));
						}
						this.m_typeOwner = runtimeType;
						this.m_module = runtimeType.GetRuntimeModule();
					}
				}
				this.m_skipVisibility = skipVisibility;
			}
			this.m_ilGenerator = null;
			this.m_fInitLocals = true;
			this.m_methodHandle = null;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (AppDomain.ProfileAPICheck)
			{
				if (this.m_creatorAssembly == null)
				{
					this.m_creatorAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
				}
				if (this.m_creatorAssembly != null && !this.m_creatorAssembly.IsFrameworkAssembly())
				{
					this.m_profileAPICheck = true;
				}
			}
			this.m_dynMethod = new DynamicMethod.RTDynamicMethod(this, name, attributes, callingConvention);
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x0010E43C File Offset: 0x0010C63C
		[SecurityCritical]
		private void PerformSecurityCheck(Module m, ref StackCrawlMark stackMark, bool skipVisibility)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			ModuleBuilder moduleBuilder = m as ModuleBuilder;
			RuntimeModule runtimeModule;
			if (moduleBuilder != null)
			{
				runtimeModule = moduleBuilder.InternalModule;
			}
			else
			{
				runtimeModule = m as RuntimeModule;
			}
			if (runtimeModule == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeModule"), "m");
			}
			if (runtimeModule == DynamicMethod.s_anonymouslyHostedDynamicMethodsModule)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "m");
			}
			if (skipVisibility)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			RuntimeType callerType = RuntimeMethodHandle.GetCallerType(ref stackMark);
			this.m_creatorAssembly = callerType.GetRuntimeAssembly();
			if (m.Assembly != this.m_creatorAssembly)
			{
				CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, m.Assembly.PermissionSet);
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x0010E508 File Offset: 0x0010C708
		[SecurityCritical]
		private void PerformSecurityCheck(Type owner, ref StackCrawlMark stackMark, bool skipVisibility)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			RuntimeType runtimeType = owner as RuntimeType;
			if (runtimeType == null)
			{
				runtimeType = owner.UnderlyingSystemType as RuntimeType;
			}
			if (runtimeType == null)
			{
				throw new ArgumentNullException("owner", Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			RuntimeType callerType = RuntimeMethodHandle.GetCallerType(ref stackMark);
			if (skipVisibility)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			else if (callerType != runtimeType)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			this.m_creatorAssembly = callerType.GetRuntimeAssembly();
			if (runtimeType.Assembly != this.m_creatorAssembly)
			{
				CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, owner.Assembly.PermissionSet);
			}
		}

		/// <summary>Completes the dynamic method and creates a delegate that can be used to execute it.</summary>
		/// <param name="delegateType">A delegate type whose signature matches that of the dynamic method.</param>
		/// <returns>A delegate of the specified type, which can be used to execute the dynamic method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method has no method body.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="delegateType" /> has the wrong number of parameters or the wrong parameter types.</exception>
		// Token: 0x06004A57 RID: 19031 RVA: 0x0010E5C0 File Offset: 0x0010C7C0
		[SecuritySafeCritical]
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType)
		{
			if (this.m_restrictedSkipVisibility)
			{
				this.GetMethodDescriptor();
				RuntimeHelpers._CompileMethod(this.m_methodHandle);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)Delegate.CreateDelegateNoSecurityCheck(delegateType, null, this.GetMethodDescriptor());
			multicastDelegate.StoreDynamicMethod(this.GetMethodInfo());
			return multicastDelegate;
		}

		/// <summary>Completes the dynamic method and creates a delegate that can be used to execute it, specifying the delegate type and an object the delegate is bound to.</summary>
		/// <param name="delegateType">A delegate type whose signature matches that of the dynamic method, minus the first parameter.</param>
		/// <param name="target">An object the delegate is bound to. Must be of the same type as the first parameter of the dynamic method.</param>
		/// <returns>A delegate of the specified type, which can be used to execute the dynamic method with the specified target object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method has no method body.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not the same type as the first parameter of the dynamic method, and is not assignable to that type.  
		/// -or-  
		/// <paramref name="delegateType" /> has the wrong number of parameters or the wrong parameter types.</exception>
		// Token: 0x06004A58 RID: 19032 RVA: 0x0010E608 File Offset: 0x0010C808
		[SecuritySafeCritical]
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType, object target)
		{
			if (this.m_restrictedSkipVisibility)
			{
				this.GetMethodDescriptor();
				RuntimeHelpers._CompileMethod(this.m_methodHandle);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)Delegate.CreateDelegateNoSecurityCheck(delegateType, target, this.GetMethodDescriptor());
			multicastDelegate.StoreDynamicMethod(this.GetMethodInfo());
			return multicastDelegate;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x0010E64F File Offset: 0x0010C84F
		// (set) Token: 0x06004A5A RID: 19034 RVA: 0x0010E657 File Offset: 0x0010C857
		internal bool ProfileAPICheck
		{
			get
			{
				return this.m_profileAPICheck;
			}
			[FriendAccessAllowed]
			set
			{
				this.m_profileAPICheck = value;
			}
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0010E660 File Offset: 0x0010C860
		[SecurityCritical]
		internal RuntimeMethodHandle GetMethodDescriptor()
		{
			if (this.m_methodHandle == null)
			{
				lock (this)
				{
					if (this.m_methodHandle == null)
					{
						if (this.m_DynamicILInfo != null)
						{
							this.m_DynamicILInfo.GetCallableMethod(this.m_module, this);
						}
						else
						{
							if (this.m_ilGenerator == null || this.m_ilGenerator.ILOffset == 0)
							{
								throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", new object[] { this.Name }));
							}
							this.m_ilGenerator.GetCallableMethod(this.m_module, this);
						}
					}
				}
			}
			return new RuntimeMethodHandle(this.m_methodHandle);
		}

		/// <summary>Returns the signature of the method, represented as a string.</summary>
		/// <returns>A string representing the method signature.</returns>
		// Token: 0x06004A5C RID: 19036 RVA: 0x0010E718 File Offset: 0x0010C918
		public override string ToString()
		{
			return this.m_dynMethod.ToString();
		}

		/// <summary>Gets the name of the dynamic method.</summary>
		/// <returns>The simple name of the method.</returns>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0010E725 File Offset: 0x0010C925
		public override string Name
		{
			get
			{
				return this.m_dynMethod.Name;
			}
		}

		/// <summary>Gets the type that declares the method, which is always <see langword="null" /> for dynamic methods.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x0010E732 File Offset: 0x0010C932
		public override Type DeclaringType
		{
			get
			{
				return this.m_dynMethod.DeclaringType;
			}
		}

		/// <summary>Gets the class that was used in reflection to obtain the method.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x0010E73F File Offset: 0x0010C93F
		public override Type ReflectedType
		{
			get
			{
				return this.m_dynMethod.ReflectedType;
			}
		}

		/// <summary>Gets the module with which the dynamic method is logically associated.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> with which the current dynamic method is associated.</returns>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x0010E74C File Offset: 0x0010C94C
		public override Module Module
		{
			get
			{
				return this.m_dynMethod.Module;
			}
		}

		/// <summary>Not supported for dynamic methods.</summary>
		/// <returns>Not supported for dynamic methods.</returns>
		/// <exception cref="T:System.InvalidOperationException">Not allowed for dynamic methods.</exception>
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x0010E759 File Offset: 0x0010C959
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
			}
		}

		/// <summary>Gets the attributes specified when the dynamic method was created.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Reflection.MethodAttributes" /> values representing the attributes for the method.</returns>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x0010E76A File Offset: 0x0010C96A
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_dynMethod.Attributes;
			}
		}

		/// <summary>Gets the calling convention specified when the dynamic method was created.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.CallingConventions" /> values that indicates the calling convention of the method.</returns>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x0010E777 File Offset: 0x0010C977
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_dynMethod.CallingConvention;
			}
		}

		/// <summary>Returns the base implementation for the method.</summary>
		/// <returns>The base implementation of the method.</returns>
		// Token: 0x06004A64 RID: 19044 RVA: 0x0010E784 File Offset: 0x0010C984
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns the parameters of the dynamic method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> objects that represent the parameters of the dynamic method.</returns>
		// Token: 0x06004A65 RID: 19045 RVA: 0x0010E787 File Offset: 0x0010C987
		public override ParameterInfo[] GetParameters()
		{
			return this.m_dynMethod.GetParameters();
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.MethodImplAttributes" /> values representing the implementation flags for the method.</returns>
		// Token: 0x06004A66 RID: 19046 RVA: 0x0010E794 File Offset: 0x0010C994
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dynMethod.GetMethodImplementationFlags();
		}

		/// <summary>Gets a value that indicates whether the current dynamic method is security-critical or security-safe-critical, and therefore can perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current dynamic method is security-critical or security-safe-critical; <see langword="false" /> if it is transparent.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method doesn't have a method body.</exception>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x0010E7A4 File Offset: 0x0010C9A4
		public override bool IsSecurityCritical
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_methodHandle != null)
				{
					return RuntimeMethodHandle.IsSecurityCritical(this.m_methodHandle);
				}
				if (this.m_typeOwner != null)
				{
					RuntimeAssembly runtimeAssembly = this.m_typeOwner.Assembly as RuntimeAssembly;
					return runtimeAssembly.IsAllSecurityCritical();
				}
				RuntimeAssembly runtimeAssembly2 = this.m_module.Assembly as RuntimeAssembly;
				return runtimeAssembly2.IsAllSecurityCritical();
			}
		}

		/// <summary>Gets a value that indicates whether the current dynamic method is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.</summary>
		/// <returns>
		///   <see langword="true" /> if the dynamic method is security-safe-critical at the current trust level; <see langword="false" /> if it is security-critical or transparent.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method doesn't have a method body.</exception>
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x0010E804 File Offset: 0x0010CA04
		public override bool IsSecuritySafeCritical
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_methodHandle != null)
				{
					return RuntimeMethodHandle.IsSecuritySafeCritical(this.m_methodHandle);
				}
				if (this.m_typeOwner != null)
				{
					RuntimeAssembly runtimeAssembly = this.m_typeOwner.Assembly as RuntimeAssembly;
					return runtimeAssembly.IsAllPublicAreaSecuritySafeCritical();
				}
				RuntimeAssembly runtimeAssembly2 = this.m_module.Assembly as RuntimeAssembly;
				return runtimeAssembly2.IsAllSecuritySafeCritical();
			}
		}

		/// <summary>Gets a value that indicates whether the current dynamic method is transparent at the current trust level, and therefore cannot perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the dynamic method is security-transparent at the current trust level; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method doesn't have a method body.</exception>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004A69 RID: 19049 RVA: 0x0010E864 File Offset: 0x0010CA64
		public override bool IsSecurityTransparent
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_methodHandle != null)
				{
					return RuntimeMethodHandle.IsSecurityTransparent(this.m_methodHandle);
				}
				if (this.m_typeOwner != null)
				{
					RuntimeAssembly runtimeAssembly = this.m_typeOwner.Assembly as RuntimeAssembly;
					return !runtimeAssembly.IsAllSecurityCritical();
				}
				RuntimeAssembly runtimeAssembly2 = this.m_module.Assembly as RuntimeAssembly;
				return !runtimeAssembly2.IsAllSecurityCritical();
			}
		}

		/// <summary>Invokes the dynamic method using the specified parameters, under the constraints of the specified binder, with the specified culture information.</summary>
		/// <param name="obj">This parameter is ignored for dynamic methods, because they are static. Specify <see langword="null" />.</param>
		/// <param name="invokeAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this parameter should be <see langword="null" />.</param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. For example, this information is needed to correctly convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, because 1000 is represented differently by different cultures.</param>
		/// <returns>A <see cref="T:System.Object" /> containing the return value of the invoked method.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="F:System.Reflection.CallingConventions.VarArgs" /> calling convention is not supported.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of elements in <paramref name="parameters" /> does not match the number of parameters in the dynamic method.</exception>
		/// <exception cref="T:System.ArgumentException">The type of one or more elements of <paramref name="parameters" /> does not match the type of the corresponding parameter of the dynamic method.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The dynamic method is associated with a module, is not anonymously hosted, and was constructed with <paramref name="skipVisibility" /> set to <see langword="false" />, but the dynamic method accesses members that are not <see langword="public" /> or <see langword="internal" /> (<see langword="Friend" /> in Visual Basic).  
		///  -or-  
		///  The dynamic method is anonymously hosted and was constructed with <paramref name="skipVisibility" /> set to <see langword="false" />, but it accesses members that are not <see langword="public" />.  
		///  -or-  
		///  The dynamic method contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004A6A RID: 19050 RVA: 0x0010E8C8 File Offset: 0x0010CAC8
		[SecuritySafeCritical]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CallToVarArg"));
			}
			RuntimeMethodHandle methodDescriptor = this.GetMethodDescriptor();
			Signature signature = new Signature(this.m_methodHandle, this.m_parameterTypes, this.m_returnType, this.CallingConvention);
			int num = signature.Arguments.Length;
			int num2 = ((parameters != null) ? parameters.Length : 0);
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			object obj2;
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				obj2 = RuntimeMethodHandle.InvokeMethod(null, array, signature, false);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
			}
			else
			{
				obj2 = RuntimeMethodHandle.InvokeMethod(null, null, signature, false);
			}
			GC.KeepAlive(this);
			return obj2;
		}

		/// <summary>Returns the custom attributes of the specified type that have been applied to the method.</summary>
		/// <param name="attributeType">A <see cref="T:System.Type" /> representing the type of custom attribute to return.</param>
		/// <param name="inherit">
		///   <see langword="true" /> to search the method's inheritance chain to find the custom attributes; <see langword="false" /> to check only the current method.</param>
		/// <returns>An array of objects representing the attributes of the method that are of type <paramref name="attributeType" /> or derive from type <paramref name="attributeType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		// Token: 0x06004A6B RID: 19051 RVA: 0x0010E992 File Offset: 0x0010CB92
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_dynMethod.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes defined for the method.</summary>
		/// <param name="inherit">
		///   <see langword="true" /> to search the method's inheritance chain to find the custom attributes; <see langword="false" /> to check only the current method.</param>
		/// <returns>An array of objects representing all the custom attributes of the method.</returns>
		// Token: 0x06004A6C RID: 19052 RVA: 0x0010E9A1 File Offset: 0x0010CBA1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_dynMethod.GetCustomAttributes(inherit);
		}

		/// <summary>Indicates whether the specified custom attribute type is defined.</summary>
		/// <param name="attributeType">A <see cref="T:System.Type" /> representing the type of custom attribute to search for.</param>
		/// <param name="inherit">
		///   <see langword="true" /> to search the method's inheritance chain to find the custom attributes; <see langword="false" /> to check only the current method.</param>
		/// <returns>
		///   <see langword="true" /> if the specified custom attribute type is defined; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A6D RID: 19053 RVA: 0x0010E9AF File Offset: 0x0010CBAF
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_dynMethod.IsDefined(attributeType, inherit);
		}

		/// <summary>Gets the type of return value for the dynamic method.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the return value of the current method; <see cref="T:System.Void" /> if the method has no return type.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x0010E9BE File Offset: 0x0010CBBE
		public override Type ReturnType
		{
			get
			{
				return this.m_dynMethod.ReturnType;
			}
		}

		/// <summary>Gets the return parameter of the dynamic method.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x0010E9CB File Offset: 0x0010CBCB
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return this.m_dynMethod.ReturnParameter;
			}
		}

		/// <summary>Gets the custom attributes of the return type for the dynamic method.</summary>
		/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> representing the custom attributes of the return type for the dynamic method.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x0010E9D8 File Offset: 0x0010CBD8
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this.m_dynMethod.ReturnTypeCustomAttributes;
			}
		}

		/// <summary>Defines a parameter of the dynamic method.</summary>
		/// <param name="position">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter.</param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.ParameterAttributes" /> values that specifies the attributes of the parameter.</param>
		/// <param name="parameterName">The name of the parameter. The name can be a zero-length string.</param>
		/// <returns>Always returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The method has no parameters.  
		///  -or-  
		///  <paramref name="position" /> is less than 0.  
		///  -or-  
		///  <paramref name="position" /> is greater than the number of the method's parameters.</exception>
		// Token: 0x06004A71 RID: 19057 RVA: 0x0010E9E8 File Offset: 0x0010CBE8
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
		{
			if (position < 0 || position > this.m_parameterTypes.Length)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			position--;
			if (position >= 0)
			{
				ParameterInfo[] array = this.m_dynMethod.LoadParameters();
				array[position].SetName(parameterName);
				array[position].SetAttributes(attributes);
			}
			return null;
		}

		/// <summary>Returns a <see cref="T:System.Reflection.Emit.DynamicILInfo" /> object that can be used to generate a method body from metadata tokens, scopes, and Microsoft intermediate language (MSIL) streams.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.DynamicILInfo" /> object that can be used to generate a method body from metadata tokens, scopes, and MSIL streams.</returns>
		// Token: 0x06004A72 RID: 19058 RVA: 0x0010EA3C File Offset: 0x0010CC3C
		[SecuritySafeCritical]
		public DynamicILInfo GetDynamicILInfo()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			if (this.m_DynamicILInfo != null)
			{
				return this.m_DynamicILInfo;
			}
			return this.GetDynamicILInfo(new DynamicScope());
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x0010EA64 File Offset: 0x0010CC64
		[SecurityCritical]
		internal DynamicILInfo GetDynamicILInfo(DynamicScope scope)
		{
			if (this.m_DynamicILInfo == null)
			{
				Module module = null;
				CallingConventions callingConvention = this.CallingConvention;
				Type returnType = this.ReturnType;
				Type[] array = null;
				Type[] array2 = null;
				Type[] parameterTypes = this.m_parameterTypes;
				byte[] signature = SignatureHelper.GetMethodSigHelper(module, callingConvention, returnType, array, array2, parameterTypes, null, null).GetSignature(true);
				this.m_DynamicILInfo = new DynamicILInfo(scope, this, signature);
			}
			return this.m_DynamicILInfo;
		}

		/// <summary>Returns a Microsoft intermediate language (MSIL) generator for the method with a default MSIL stream size of 64 bytes.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for the method.</returns>
		// Token: 0x06004A74 RID: 19060 RVA: 0x0010EAB2 File Offset: 0x0010CCB2
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns a Microsoft intermediate language (MSIL) generator for the method with the specified MSIL stream size.</summary>
		/// <param name="streamSize">The size of the MSIL stream, in bytes.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for the method, with the specified MSIL stream size.</returns>
		// Token: 0x06004A75 RID: 19061 RVA: 0x0010EABC File Offset: 0x0010CCBC
		[SecuritySafeCritical]
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_ilGenerator == null)
			{
				Module module = null;
				CallingConventions callingConvention = this.CallingConvention;
				Type returnType = this.ReturnType;
				Type[] array = null;
				Type[] array2 = null;
				Type[] parameterTypes = this.m_parameterTypes;
				byte[] signature = SignatureHelper.GetMethodSigHelper(module, callingConvention, returnType, array, array2, parameterTypes, null, null).GetSignature(true);
				this.m_ilGenerator = new DynamicILGenerator(this, signature, streamSize);
			}
			return this.m_ilGenerator;
		}

		/// <summary>Gets or sets a value indicating whether the local variables in the method are zero-initialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the local variables in the method are zero-initialized; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x0010EB0A File Offset: 0x0010CD0A
		// (set) Token: 0x06004A77 RID: 19063 RVA: 0x0010EB12 File Offset: 0x0010CD12
		public bool InitLocals
		{
			get
			{
				return this.m_fInitLocals;
			}
			set
			{
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0010EB1B File Offset: 0x0010CD1B
		internal MethodInfo GetMethodInfo()
		{
			return this.m_dynMethod;
		}

		// Token: 0x04001EA5 RID: 7845
		private RuntimeType[] m_parameterTypes;

		// Token: 0x04001EA6 RID: 7846
		internal IRuntimeMethodInfo m_methodHandle;

		// Token: 0x04001EA7 RID: 7847
		private RuntimeType m_returnType;

		// Token: 0x04001EA8 RID: 7848
		private DynamicILGenerator m_ilGenerator;

		// Token: 0x04001EA9 RID: 7849
		private DynamicILInfo m_DynamicILInfo;

		// Token: 0x04001EAA RID: 7850
		private bool m_fInitLocals;

		// Token: 0x04001EAB RID: 7851
		private RuntimeModule m_module;

		// Token: 0x04001EAC RID: 7852
		internal bool m_skipVisibility;

		// Token: 0x04001EAD RID: 7853
		internal RuntimeType m_typeOwner;

		// Token: 0x04001EAE RID: 7854
		private DynamicMethod.RTDynamicMethod m_dynMethod;

		// Token: 0x04001EAF RID: 7855
		internal DynamicResolver m_resolver;

		// Token: 0x04001EB0 RID: 7856
		private bool m_profileAPICheck;

		// Token: 0x04001EB1 RID: 7857
		private RuntimeAssembly m_creatorAssembly;

		// Token: 0x04001EB2 RID: 7858
		internal bool m_restrictedSkipVisibility;

		// Token: 0x04001EB3 RID: 7859
		internal CompressedStack m_creationContext;

		// Token: 0x04001EB4 RID: 7860
		private static volatile InternalModuleBuilder s_anonymouslyHostedDynamicMethodsModule;

		// Token: 0x04001EB5 RID: 7861
		private static readonly object s_anonymouslyHostedDynamicMethodsModuleLock = new object();

		// Token: 0x02000C3B RID: 3131
		internal class RTDynamicMethod : MethodInfo
		{
			// Token: 0x06007065 RID: 28773 RVA: 0x00184820 File Offset: 0x00182A20
			private RTDynamicMethod()
			{
			}

			// Token: 0x06007066 RID: 28774 RVA: 0x00184828 File Offset: 0x00182A28
			internal RTDynamicMethod(DynamicMethod owner, string name, MethodAttributes attributes, CallingConventions callingConvention)
			{
				this.m_owner = owner;
				this.m_name = name;
				this.m_attributes = attributes;
				this.m_callingConvention = callingConvention;
			}

			// Token: 0x06007067 RID: 28775 RVA: 0x0018484D File Offset: 0x00182A4D
			public override string ToString()
			{
				return this.ReturnType.FormatTypeName() + " " + base.FormatNameAndSig();
			}

			// Token: 0x17001342 RID: 4930
			// (get) Token: 0x06007068 RID: 28776 RVA: 0x0018486A File Offset: 0x00182A6A
			public override string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17001343 RID: 4931
			// (get) Token: 0x06007069 RID: 28777 RVA: 0x00184872 File Offset: 0x00182A72
			public override Type DeclaringType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001344 RID: 4932
			// (get) Token: 0x0600706A RID: 28778 RVA: 0x00184875 File Offset: 0x00182A75
			public override Type ReflectedType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001345 RID: 4933
			// (get) Token: 0x0600706B RID: 28779 RVA: 0x00184878 File Offset: 0x00182A78
			public override Module Module
			{
				get
				{
					return this.m_owner.m_module;
				}
			}

			// Token: 0x17001346 RID: 4934
			// (get) Token: 0x0600706C RID: 28780 RVA: 0x00184885 File Offset: 0x00182A85
			public override RuntimeMethodHandle MethodHandle
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
				}
			}

			// Token: 0x17001347 RID: 4935
			// (get) Token: 0x0600706D RID: 28781 RVA: 0x00184896 File Offset: 0x00182A96
			public override MethodAttributes Attributes
			{
				get
				{
					return this.m_attributes;
				}
			}

			// Token: 0x17001348 RID: 4936
			// (get) Token: 0x0600706E RID: 28782 RVA: 0x0018489E File Offset: 0x00182A9E
			public override CallingConventions CallingConvention
			{
				get
				{
					return this.m_callingConvention;
				}
			}

			// Token: 0x0600706F RID: 28783 RVA: 0x001848A6 File Offset: 0x00182AA6
			public override MethodInfo GetBaseDefinition()
			{
				return this;
			}

			// Token: 0x06007070 RID: 28784 RVA: 0x001848AC File Offset: 0x00182AAC
			public override ParameterInfo[] GetParameters()
			{
				ParameterInfo[] array = this.LoadParameters();
				ParameterInfo[] array2 = new ParameterInfo[array.Length];
				Array.Copy(array, array2, array.Length);
				return array2;
			}

			// Token: 0x06007071 RID: 28785 RVA: 0x001848D4 File Offset: 0x00182AD4
			public override MethodImplAttributes GetMethodImplementationFlags()
			{
				return MethodImplAttributes.NoInlining;
			}

			// Token: 0x06007072 RID: 28786 RVA: 0x001848D7 File Offset: 0x00182AD7
			public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "this");
			}

			// Token: 0x06007073 RID: 28787 RVA: 0x001848F0 File Offset: 0x00182AF0
			public override object[] GetCustomAttributes(Type attributeType, bool inherit)
			{
				if (attributeType == null)
				{
					throw new ArgumentNullException("attributeType");
				}
				if (attributeType.IsAssignableFrom(typeof(MethodImplAttribute)))
				{
					return new object[]
					{
						new MethodImplAttribute(this.GetMethodImplementationFlags())
					};
				}
				return EmptyArray<object>.Value;
			}

			// Token: 0x06007074 RID: 28788 RVA: 0x0018493D File Offset: 0x00182B3D
			public override object[] GetCustomAttributes(bool inherit)
			{
				return new object[]
				{
					new MethodImplAttribute(this.GetMethodImplementationFlags())
				};
			}

			// Token: 0x06007075 RID: 28789 RVA: 0x00184953 File Offset: 0x00182B53
			public override bool IsDefined(Type attributeType, bool inherit)
			{
				if (attributeType == null)
				{
					throw new ArgumentNullException("attributeType");
				}
				return attributeType.IsAssignableFrom(typeof(MethodImplAttribute));
			}

			// Token: 0x17001349 RID: 4937
			// (get) Token: 0x06007076 RID: 28790 RVA: 0x0018497E File Offset: 0x00182B7E
			public override bool IsSecurityCritical
			{
				get
				{
					return this.m_owner.IsSecurityCritical;
				}
			}

			// Token: 0x1700134A RID: 4938
			// (get) Token: 0x06007077 RID: 28791 RVA: 0x0018498B File Offset: 0x00182B8B
			public override bool IsSecuritySafeCritical
			{
				get
				{
					return this.m_owner.IsSecuritySafeCritical;
				}
			}

			// Token: 0x1700134B RID: 4939
			// (get) Token: 0x06007078 RID: 28792 RVA: 0x00184998 File Offset: 0x00182B98
			public override bool IsSecurityTransparent
			{
				get
				{
					return this.m_owner.IsSecurityTransparent;
				}
			}

			// Token: 0x1700134C RID: 4940
			// (get) Token: 0x06007079 RID: 28793 RVA: 0x001849A5 File Offset: 0x00182BA5
			public override Type ReturnType
			{
				get
				{
					return this.m_owner.m_returnType;
				}
			}

			// Token: 0x1700134D RID: 4941
			// (get) Token: 0x0600707A RID: 28794 RVA: 0x001849B2 File Offset: 0x00182BB2
			public override ParameterInfo ReturnParameter
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700134E RID: 4942
			// (get) Token: 0x0600707B RID: 28795 RVA: 0x001849B5 File Offset: 0x00182BB5
			public override ICustomAttributeProvider ReturnTypeCustomAttributes
			{
				get
				{
					return this.GetEmptyCAHolder();
				}
			}

			// Token: 0x0600707C RID: 28796 RVA: 0x001849C0 File Offset: 0x00182BC0
			internal ParameterInfo[] LoadParameters()
			{
				if (this.m_parameters == null)
				{
					Type[] parameterTypes = this.m_owner.m_parameterTypes;
					Type[] array = parameterTypes;
					ParameterInfo[] array2 = new ParameterInfo[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new RuntimeParameterInfo(this, null, array[i], i);
					}
					if (this.m_parameters == null)
					{
						this.m_parameters = array2;
					}
				}
				return this.m_parameters;
			}

			// Token: 0x0600707D RID: 28797 RVA: 0x00184A1D File Offset: 0x00182C1D
			private ICustomAttributeProvider GetEmptyCAHolder()
			{
				return new DynamicMethod.RTDynamicMethod.EmptyCAHolder();
			}

			// Token: 0x04003750 RID: 14160
			internal DynamicMethod m_owner;

			// Token: 0x04003751 RID: 14161
			private ParameterInfo[] m_parameters;

			// Token: 0x04003752 RID: 14162
			private string m_name;

			// Token: 0x04003753 RID: 14163
			private MethodAttributes m_attributes;

			// Token: 0x04003754 RID: 14164
			private CallingConventions m_callingConvention;

			// Token: 0x02000D09 RID: 3337
			private class EmptyCAHolder : ICustomAttributeProvider
			{
				// Token: 0x06007238 RID: 29240 RVA: 0x0018ADA3 File Offset: 0x00188FA3
				internal EmptyCAHolder()
				{
				}

				// Token: 0x06007239 RID: 29241 RVA: 0x0018ADAB File Offset: 0x00188FAB
				object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
				{
					return EmptyArray<object>.Value;
				}

				// Token: 0x0600723A RID: 29242 RVA: 0x0018ADB2 File Offset: 0x00188FB2
				object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
				{
					return EmptyArray<object>.Value;
				}

				// Token: 0x0600723B RID: 29243 RVA: 0x0018ADB9 File Offset: 0x00188FB9
				bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
				{
					return false;
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x0200060C RID: 1548
	[Serializable]
	internal class RuntimeModule : Module
	{
		// Token: 0x060047D8 RID: 18392 RVA: 0x001067D3 File Offset: 0x001049D3
		internal RuntimeModule()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060047D9 RID: 18393
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetType(RuntimeModule module, string className, bool ignoreCase, bool throwOnError, ObjectHandleOnStack type);

		// Token: 0x060047DA RID: 18394
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		private static extern bool nIsTransientInternal(RuntimeModule module);

		// Token: 0x060047DB RID: 18395
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetScopeName(RuntimeModule module, StringHandleOnStack retString);

		// Token: 0x060047DC RID: 18396
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFullyQualifiedName(RuntimeModule module, StringHandleOnStack retString);

		// Token: 0x060047DD RID: 18397
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeType[] GetTypes(RuntimeModule module);

		// Token: 0x060047DE RID: 18398 RVA: 0x001067E0 File Offset: 0x001049E0
		[SecuritySafeCritical]
		internal RuntimeType[] GetDefinedTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x060047DF RID: 18399
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsResource(RuntimeModule module);

		// Token: 0x060047E0 RID: 18400
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetSignerCertificate(RuntimeModule module, ObjectHandleOnStack retData);

		// Token: 0x060047E1 RID: 18401 RVA: 0x001067F0 File Offset: 0x001049F0
		private static RuntimeTypeHandle[] ConvertToTypeHandleArray(Type[] genericArguments)
		{
			if (genericArguments == null)
			{
				return null;
			}
			int num = genericArguments.Length;
			RuntimeTypeHandle[] array = new RuntimeTypeHandle[num];
			for (int i = 0; i < num; i++)
			{
				Type type = genericArguments[i];
				if (type == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				type = type.UnderlyingSystemType;
				if (type == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				if (!(type is RuntimeType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				array[i] = type.GetTypeHandleInternal();
			}
			return array;
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0010687C File Offset: 0x00104A7C
		[SecuritySafeCritical]
		public override byte[] ResolveSignature(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			if (!metadataToken2.IsMemberRef && !metadataToken2.IsMethodDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsSignature && !metadataToken2.IsFieldDef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }), "metadataToken");
			}
			ConstArray constArray;
			if (metadataToken2.IsMemberRef)
			{
				constArray = this.MetadataImport.GetMemberRefProps(metadataToken);
			}
			else
			{
				constArray = this.MetadataImport.GetSignatureFromToken(metadataToken);
			}
			byte[] array = new byte[constArray.Length];
			for (int i = 0; i < constArray.Length; i++)
			{
				array[i] = constArray[i];
			}
			return array;
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x00106980 File Offset: 0x00104B80
		[SecuritySafeCritical]
		public unsafe override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			RuntimeTypeHandle[] array = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] array2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			MethodBase methodBase;
			try
			{
				if (!metadataToken2.IsMethodDef && !metadataToken2.IsMethodSpec)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMethod", new object[] { metadataToken2, this }));
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMethod", new object[] { metadataToken2, this }));
					}
				}
				IRuntimeMethodInfo runtimeMethodInfo = ModuleHandle.ResolveMethodHandleInternal(this.GetNativeHandle(), metadataToken2, array, array2);
				Type type = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if (type.IsGenericType || type.IsArray)
				{
					MetadataToken metadataToken3 = new MetadataToken(this.MetadataImport.GetParentToken(metadataToken2));
					if (metadataToken2.IsMethodSpec)
					{
						metadataToken3 = new MetadataToken(this.MetadataImport.GetParentToken(metadataToken3));
					}
					type = this.ResolveType(metadataToken3, genericTypeArguments, genericMethodArguments);
				}
				methodBase = RuntimeType.GetMethodBase(type as RuntimeType, runtimeMethodInfo);
			}
			catch (BadImageFormatException ex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), ex);
			}
			return methodBase;
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x00106B44 File Offset: 0x00104D44
		[SecurityCritical]
		private FieldInfo ResolveLiteralField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2) || !metadataToken2.IsFieldDef)
			{
				throw new ArgumentOutOfRangeException("metadataToken", string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }), Array.Empty<object>()));
			}
			string text = this.MetadataImport.GetName(metadataToken2).ToString();
			int parentToken = this.MetadataImport.GetParentToken(metadataToken2);
			Type type = this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
			type.GetFields();
			FieldInfo field;
			try
			{
				field = type.GetField(text, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			catch
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveField", new object[] { metadataToken2, this }), "metadataToken");
			}
			return field;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x00106C44 File Offset: 0x00104E44
		[SecuritySafeCritical]
		public unsafe override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			RuntimeTypeHandle[] array = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] array2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			FieldInfo fieldInfo;
			try
			{
				IRuntimeFieldInfo runtimeFieldInfo;
				if (!metadataToken2.IsFieldDef)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveField", new object[] { metadataToken2, this }));
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() != 6)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveField", new object[] { metadataToken2, this }));
					}
					runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken2, array, array2);
				}
				runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken, array, array2);
				RuntimeType runtimeType = RuntimeFieldHandle.GetApproxDeclaringType(runtimeFieldInfo.Value);
				if (runtimeType.IsGenericType || runtimeType.IsArray)
				{
					int parentToken = ModuleHandle.GetMetadataImport(this.GetNativeHandle()).GetParentToken(metadataToken);
					runtimeType = (RuntimeType)this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
				}
				fieldInfo = RuntimeType.GetFieldInfo(runtimeType, runtimeFieldInfo);
			}
			catch (MissingFieldException)
			{
				fieldInfo = this.ResolveLiteralField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			catch (BadImageFormatException ex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), ex);
			}
			return fieldInfo;
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x00106E10 File Offset: 0x00105010
		[SecuritySafeCritical]
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsGlobalTypeDefToken)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveModuleType", new object[] { metadataToken2 }), "metadataToken");
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			if (!metadataToken2.IsTypeDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsTypeRef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", new object[] { metadataToken2, this }), "metadataToken");
			}
			RuntimeTypeHandle[] array = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] array2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			Type type;
			try
			{
				Type runtimeType = this.GetModuleHandle().ResolveTypeHandle(metadataToken, array, array2).GetRuntimeType();
				if (runtimeType == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", new object[] { metadataToken2, this }), "metadataToken");
				}
				type = runtimeType;
			}
			catch (BadImageFormatException ex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), ex);
			}
			return type;
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x00106F5C File Offset: 0x0010515C
		[SecuritySafeCritical]
		public unsafe override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsProperty)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_PropertyInfoNotAvailable"));
			}
			if (metadataToken2.IsEvent)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EventInfoNotAvailable"));
			}
			if (metadataToken2.IsMethodSpec || metadataToken2.IsMethodDef)
			{
				return this.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsFieldDef)
			{
				return this.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsTypeRef || metadataToken2.IsTypeDef || metadataToken2.IsTypeSpec)
			{
				return this.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (!metadataToken2.IsMemberRef)
			{
				throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMember", new object[] { metadataToken2, this }));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
			{
				return this.ResolveField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			return this.ResolveMethod(metadataToken2, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x001070B0 File Offset: 0x001052B0
		[SecuritySafeCritical]
		public override string ResolveString(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!metadataToken2.IsString)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), metadataToken, this.ToString()));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }), Array.Empty<object>()));
			}
			string userString = this.MetadataImport.GetUserString(metadataToken);
			if (userString == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), metadataToken, this.ToString()));
			}
			return userString;
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0010717B File Offset: 0x0010537B
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			ModuleHandle.GetPEKind(this.GetNativeHandle(), out peKind, out machine);
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x0010718A File Offset: 0x0010538A
		public override int MDStreamVersion
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetMDStreamVersion(this.GetNativeHandle());
			}
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00107197 File Offset: 0x00105397
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x001071A8 File Offset: 0x001053A8
		internal MethodInfo GetMethodInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.RuntimeType == null)
			{
				return null;
			}
			if (types == null)
			{
				return this.RuntimeType.GetMethod(name, bindingAttr);
			}
			return this.RuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x001071E0 File Offset: 0x001053E0
		internal RuntimeType RuntimeType
		{
			get
			{
				if (this.m_runtimeType == null)
				{
					this.m_runtimeType = ModuleHandle.GetModuleType(this.GetNativeHandle());
				}
				return this.m_runtimeType;
			}
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x00107207 File Offset: 0x00105407
		[SecuritySafeCritical]
		internal bool IsTransientInternal()
		{
			return RuntimeModule.nIsTransientInternal(this.GetNativeHandle());
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x00107214 File Offset: 0x00105414
		internal MetadataImport MetadataImport
		{
			[SecurityCritical]
			get
			{
				return ModuleHandle.GetMetadataImport(this.GetNativeHandle());
			}
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x00107221 File Offset: 0x00105421
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x00107238 File Offset: 0x00105438
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0010728C File Offset: 0x0010548C
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x001072DE File Offset: 0x001054DE
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x001072E6 File Offset: 0x001054E6
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0010730C File Offset: 0x0010550C
		[SecuritySafeCritical]
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			RuntimeType runtimeType = null;
			RuntimeModule.GetType(this.GetNativeHandle(), className, throwOnError, ignoreCase, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x00107340 File Offset: 0x00105540
		[SecurityCritical]
		internal string GetFullyQualifiedName()
		{
			string text = null;
			RuntimeModule.GetFullyQualifiedName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x00107364 File Offset: 0x00105564
		public override string FullyQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				if (fullyQualifiedName != null)
				{
					bool flag = true;
					try
					{
						Path.GetFullPathInternal(fullyQualifiedName);
					}
					catch (ArgumentException)
					{
						flag = false;
					}
					if (flag)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullyQualifiedName).Demand();
					}
				}
				return fullyQualifiedName;
			}
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x001073AC File Offset: 0x001055AC
		[SecuritySafeCritical]
		public override Type[] GetTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x001073C8 File Offset: 0x001055C8
		public override Guid ModuleVersionId
		{
			[SecuritySafeCritical]
			get
			{
				Guid guid;
				this.MetadataImport.GetScopeProps(out guid);
				return guid;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x001073E6 File Offset: 0x001055E6
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetToken(this.GetNativeHandle());
			}
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x001073F3 File Offset: 0x001055F3
		public override bool IsResource()
		{
			return RuntimeModule.IsResource(this.GetNativeHandle());
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x00107400 File Offset: 0x00105600
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return new FieldInfo[0];
			}
			return this.RuntimeType.GetFields(bindingFlags);
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x00107423 File Offset: 0x00105623
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.RuntimeType == null)
			{
				return null;
			}
			return this.RuntimeType.GetField(name, bindingAttr);
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x00107450 File Offset: 0x00105650
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return new MethodInfo[0];
			}
			return this.RuntimeType.GetMethods(bindingFlags);
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x00107474 File Offset: 0x00105674
		public override string ScopeName
		{
			[SecuritySafeCritical]
			get
			{
				string text = null;
				RuntimeModule.GetScopeName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text));
				return text;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x00107498 File Offset: 0x00105698
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				int num = fullyQualifiedName.LastIndexOf('\\');
				if (num == -1)
				{
					return fullyQualifiedName;
				}
				return new string(fullyQualifiedName.ToCharArray(), num + 1, fullyQualifiedName.Length - num - 1);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x001074D3 File Offset: 0x001056D3
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x001074DB File Offset: 0x001056DB
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.m_runtimeAssembly;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x001074E3 File Offset: 0x001056E3
		internal override ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(this);
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x001074EB File Offset: 0x001056EB
		internal RuntimeModule GetNativeHandle()
		{
			return this;
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x001074F0 File Offset: 0x001056F0
		[SecuritySafeCritical]
		public override X509Certificate GetSignerCertificate()
		{
			byte[] array = null;
			RuntimeModule.GetSignerCertificate(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			if (array == null)
			{
				return null;
			}
			return new X509Certificate(array);
		}

		// Token: 0x04001DC5 RID: 7621
		private RuntimeType m_runtimeType;

		// Token: 0x04001DC6 RID: 7622
		private RuntimeAssembly m_runtimeAssembly;

		// Token: 0x04001DC7 RID: 7623
		private IntPtr m_pRefClass;

		// Token: 0x04001DC8 RID: 7624
		private IntPtr m_pData;

		// Token: 0x04001DC9 RID: 7625
		private IntPtr m_pGlobals;

		// Token: 0x04001DCA RID: 7626
		private IntPtr m_pFields;
	}
}

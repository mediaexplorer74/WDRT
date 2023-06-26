using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005FC RID: 1532
	internal struct MetadataImport
	{
		// Token: 0x060046AD RID: 18093 RVA: 0x00104308 File Offset: 0x00102508
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.m_metadataImport2);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x00104315 File Offset: 0x00102515
		public override bool Equals(object obj)
		{
			return obj is MetadataImport && this.Equals((MetadataImport)obj);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x0010432D File Offset: 0x0010252D
		private bool Equals(MetadataImport import)
		{
			return import.m_metadataImport2 == this.m_metadataImport2;
		}

		// Token: 0x060046B0 RID: 18096
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMarshalAs(IntPtr pNativeType, int cNativeType, out int unmanagedType, out int safeArraySubType, out string safeArrayUserDefinedSubType, out int arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex);

		// Token: 0x060046B1 RID: 18097 RVA: 0x00104340 File Offset: 0x00102540
		[SecurityCritical]
		internal static void GetMarshalAs(ConstArray nativeType, out UnmanagedType unmanagedType, out VarEnum safeArraySubType, out string safeArrayUserDefinedSubType, out UnmanagedType arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex)
		{
			int num;
			int num2;
			int num3;
			MetadataImport._GetMarshalAs(nativeType.Signature, nativeType.Length, out num, out num2, out safeArrayUserDefinedSubType, out num3, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
			unmanagedType = (UnmanagedType)num;
			safeArraySubType = (VarEnum)num2;
			arraySubType = (UnmanagedType)num3;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x0010437B File Offset: 0x0010257B
		internal static void ThrowError(int hResult)
		{
			throw new MetadataException(hResult);
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x00104383 File Offset: 0x00102583
		internal MetadataImport(IntPtr metadataImport2, object keepalive)
		{
			this.m_metadataImport2 = metadataImport2;
			this.m_keepalive = keepalive;
		}

		// Token: 0x060046B4 RID: 18100
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Enum(IntPtr scope, int type, int parent, out MetadataEnumResult result);

		// Token: 0x060046B5 RID: 18101 RVA: 0x00104393 File Offset: 0x00102593
		[SecurityCritical]
		public void Enum(MetadataTokenType type, int parent, out MetadataEnumResult result)
		{
			MetadataImport._Enum(this.m_metadataImport2, (int)type, parent, out result);
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x001043A3 File Offset: 0x001025A3
		[SecurityCritical]
		public void EnumNestedTypes(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.TypeDef, mdTypeDef, out result);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x001043B2 File Offset: 0x001025B2
		[SecurityCritical]
		public void EnumCustomAttributes(int mdToken, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.CustomAttribute, mdToken, out result);
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x001043C1 File Offset: 0x001025C1
		[SecurityCritical]
		public void EnumParams(int mdMethodDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.ParamDef, mdMethodDef, out result);
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x001043D0 File Offset: 0x001025D0
		[SecurityCritical]
		public void EnumFields(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.FieldDef, mdTypeDef, out result);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x001043DF File Offset: 0x001025DF
		[SecurityCritical]
		public void EnumProperties(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Property, mdTypeDef, out result);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x001043EE File Offset: 0x001025EE
		[SecurityCritical]
		public void EnumEvents(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Event, mdTypeDef, out result);
		}

		// Token: 0x060046BC RID: 18108
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string _GetDefaultValue(IntPtr scope, int mdToken, out long value, out int length, out int corElementType);

		// Token: 0x060046BD RID: 18109 RVA: 0x00104400 File Offset: 0x00102600
		[SecurityCritical]
		public string GetDefaultValue(int mdToken, out long value, out int length, out CorElementType corElementType)
		{
			int num;
			string text = MetadataImport._GetDefaultValue(this.m_metadataImport2, mdToken, out value, out length, out num);
			corElementType = (CorElementType)num;
			return text;
		}

		// Token: 0x060046BE RID: 18110
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetUserString(IntPtr scope, int mdToken, void** name, out int length);

		// Token: 0x060046BF RID: 18111 RVA: 0x00104424 File Offset: 0x00102624
		[SecurityCritical]
		public unsafe string GetUserString(int mdToken)
		{
			void* ptr;
			int num;
			MetadataImport._GetUserString(this.m_metadataImport2, mdToken, &ptr, out num);
			if (ptr == null)
			{
				return null;
			}
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (char)Marshal.ReadInt16((IntPtr)((void*)((byte*)ptr + (IntPtr)i * 2)));
			}
			return new string(array);
		}

		// Token: 0x060046C0 RID: 18112
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetName(IntPtr scope, int mdToken, void** name);

		// Token: 0x060046C1 RID: 18113 RVA: 0x00104478 File Offset: 0x00102678
		[SecurityCritical]
		public unsafe Utf8String GetName(int mdToken)
		{
			void* ptr;
			MetadataImport._GetName(this.m_metadataImport2, mdToken, &ptr);
			return new Utf8String(ptr);
		}

		// Token: 0x060046C2 RID: 18114
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetNamespace(IntPtr scope, int mdToken, void** namesp);

		// Token: 0x060046C3 RID: 18115 RVA: 0x0010449C File Offset: 0x0010269C
		[SecurityCritical]
		public unsafe Utf8String GetNamespace(int mdToken)
		{
			void* ptr;
			MetadataImport._GetNamespace(this.m_metadataImport2, mdToken, &ptr);
			return new Utf8String(ptr);
		}

		// Token: 0x060046C4 RID: 18116
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetEventProps(IntPtr scope, int mdToken, void** name, out int eventAttributes);

		// Token: 0x060046C5 RID: 18117 RVA: 0x001044C0 File Offset: 0x001026C0
		[SecurityCritical]
		public unsafe void GetEventProps(int mdToken, out void* name, out EventAttributes eventAttributes)
		{
			void* ptr;
			int num;
			MetadataImport._GetEventProps(this.m_metadataImport2, mdToken, &ptr, out num);
			name = ptr;
			eventAttributes = (EventAttributes)num;
		}

		// Token: 0x060046C6 RID: 18118
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldDefProps(IntPtr scope, int mdToken, out int fieldAttributes);

		// Token: 0x060046C7 RID: 18119 RVA: 0x001044E4 File Offset: 0x001026E4
		[SecurityCritical]
		public void GetFieldDefProps(int mdToken, out FieldAttributes fieldAttributes)
		{
			int num;
			MetadataImport._GetFieldDefProps(this.m_metadataImport2, mdToken, out num);
			fieldAttributes = (FieldAttributes)num;
		}

		// Token: 0x060046C8 RID: 18120
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPropertyProps(IntPtr scope, int mdToken, void** name, out int propertyAttributes, out ConstArray signature);

		// Token: 0x060046C9 RID: 18121 RVA: 0x00104504 File Offset: 0x00102704
		[SecurityCritical]
		public unsafe void GetPropertyProps(int mdToken, out void* name, out PropertyAttributes propertyAttributes, out ConstArray signature)
		{
			void* ptr;
			int num;
			MetadataImport._GetPropertyProps(this.m_metadataImport2, mdToken, &ptr, out num, out signature);
			name = ptr;
			propertyAttributes = (PropertyAttributes)num;
		}

		// Token: 0x060046CA RID: 18122
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParentToken(IntPtr scope, int mdToken, out int tkParent);

		// Token: 0x060046CB RID: 18123 RVA: 0x0010452C File Offset: 0x0010272C
		[SecurityCritical]
		public int GetParentToken(int tkToken)
		{
			int num;
			MetadataImport._GetParentToken(this.m_metadataImport2, tkToken, out num);
			return num;
		}

		// Token: 0x060046CC RID: 18124
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParamDefProps(IntPtr scope, int parameterToken, out int sequence, out int attributes);

		// Token: 0x060046CD RID: 18125 RVA: 0x00104548 File Offset: 0x00102748
		[SecurityCritical]
		public void GetParamDefProps(int parameterToken, out int sequence, out ParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetParamDefProps(this.m_metadataImport2, parameterToken, out sequence, out num);
			attributes = (ParameterAttributes)num;
		}

		// Token: 0x060046CE RID: 18126
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetGenericParamProps(IntPtr scope, int genericParameter, out int flags);

		// Token: 0x060046CF RID: 18127 RVA: 0x00104568 File Offset: 0x00102768
		[SecurityCritical]
		public void GetGenericParamProps(int genericParameter, out GenericParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetGenericParamProps(this.m_metadataImport2, genericParameter, out num);
			attributes = (GenericParameterAttributes)num;
		}

		// Token: 0x060046D0 RID: 18128
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetScopeProps(IntPtr scope, out Guid mvid);

		// Token: 0x060046D1 RID: 18129 RVA: 0x00104586 File Offset: 0x00102786
		[SecurityCritical]
		public void GetScopeProps(out Guid mvid)
		{
			MetadataImport._GetScopeProps(this.m_metadataImport2, out mvid);
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x00104594 File Offset: 0x00102794
		[SecurityCritical]
		public ConstArray GetMethodSignature(MetadataToken token)
		{
			if (token.IsMemberRef)
			{
				return this.GetMemberRefProps(token);
			}
			return this.GetSigOfMethodDef(token);
		}

		// Token: 0x060046D3 RID: 18131
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfMethodDef(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x060046D4 RID: 18132 RVA: 0x001045B8 File Offset: 0x001027B8
		[SecurityCritical]
		public ConstArray GetSigOfMethodDef(int methodToken)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetSigOfMethodDef(this.m_metadataImport2, methodToken, ref constArray);
			return constArray;
		}

		// Token: 0x060046D5 RID: 18133
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSignatureFromToken(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x060046D6 RID: 18134 RVA: 0x001045DC File Offset: 0x001027DC
		[SecurityCritical]
		public ConstArray GetSignatureFromToken(int token)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetSignatureFromToken(this.m_metadataImport2, token, ref constArray);
			return constArray;
		}

		// Token: 0x060046D7 RID: 18135
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMemberRefProps(IntPtr scope, int memberTokenRef, out ConstArray signature);

		// Token: 0x060046D8 RID: 18136 RVA: 0x00104600 File Offset: 0x00102800
		[SecurityCritical]
		public ConstArray GetMemberRefProps(int memberTokenRef)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetMemberRefProps(this.m_metadataImport2, memberTokenRef, out constArray);
			return constArray;
		}

		// Token: 0x060046D9 RID: 18137
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetCustomAttributeProps(IntPtr scope, int customAttributeToken, out int constructorToken, out ConstArray signature);

		// Token: 0x060046DA RID: 18138 RVA: 0x00104624 File Offset: 0x00102824
		[SecurityCritical]
		public void GetCustomAttributeProps(int customAttributeToken, out int constructorToken, out ConstArray signature)
		{
			MetadataImport._GetCustomAttributeProps(this.m_metadataImport2, customAttributeToken, out constructorToken, out signature);
		}

		// Token: 0x060046DB RID: 18139
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetClassLayout(IntPtr scope, int typeTokenDef, out int packSize, out int classSize);

		// Token: 0x060046DC RID: 18140 RVA: 0x00104634 File Offset: 0x00102834
		[SecurityCritical]
		public void GetClassLayout(int typeTokenDef, out int packSize, out int classSize)
		{
			MetadataImport._GetClassLayout(this.m_metadataImport2, typeTokenDef, out packSize, out classSize);
		}

		// Token: 0x060046DD RID: 18141
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _GetFieldOffset(IntPtr scope, int typeTokenDef, int fieldTokenDef, out int offset);

		// Token: 0x060046DE RID: 18142 RVA: 0x00104644 File Offset: 0x00102844
		[SecurityCritical]
		public bool GetFieldOffset(int typeTokenDef, int fieldTokenDef, out int offset)
		{
			return MetadataImport._GetFieldOffset(this.m_metadataImport2, typeTokenDef, fieldTokenDef, out offset);
		}

		// Token: 0x060046DF RID: 18143
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfFieldDef(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x060046E0 RID: 18144 RVA: 0x00104654 File Offset: 0x00102854
		[SecurityCritical]
		public ConstArray GetSigOfFieldDef(int fieldToken)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetSigOfFieldDef(this.m_metadataImport2, fieldToken, ref constArray);
			return constArray;
		}

		// Token: 0x060046E1 RID: 18145
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldMarshal(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x060046E2 RID: 18146 RVA: 0x00104678 File Offset: 0x00102878
		[SecurityCritical]
		public ConstArray GetFieldMarshal(int fieldToken)
		{
			ConstArray constArray = default(ConstArray);
			MetadataImport._GetFieldMarshal(this.m_metadataImport2, fieldToken, ref constArray);
			return constArray;
		}

		// Token: 0x060046E3 RID: 18147
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPInvokeMap(IntPtr scope, int token, out int attributes, void** importName, void** importDll);

		// Token: 0x060046E4 RID: 18148 RVA: 0x0010469C File Offset: 0x0010289C
		[SecurityCritical]
		public unsafe void GetPInvokeMap(int token, out PInvokeAttributes attributes, out string importName, out string importDll)
		{
			int num;
			void* ptr;
			void* ptr2;
			MetadataImport._GetPInvokeMap(this.m_metadataImport2, token, out num, &ptr, &ptr2);
			importName = new Utf8String(ptr).ToString();
			importDll = new Utf8String(ptr2).ToString();
			attributes = (PInvokeAttributes)num;
		}

		// Token: 0x060046E5 RID: 18149
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _IsValidToken(IntPtr scope, int token);

		// Token: 0x060046E6 RID: 18150 RVA: 0x001046ED File Offset: 0x001028ED
		[SecurityCritical]
		public bool IsValidToken(int token)
		{
			return MetadataImport._IsValidToken(this.m_metadataImport2, token);
		}

		// Token: 0x04001D5B RID: 7515
		private IntPtr m_metadataImport2;

		// Token: 0x04001D5C RID: 7516
		private object m_keepalive;

		// Token: 0x04001D5D RID: 7517
		internal static readonly MetadataImport EmptyImport = new MetadataImport((IntPtr)0, null);
	}
}

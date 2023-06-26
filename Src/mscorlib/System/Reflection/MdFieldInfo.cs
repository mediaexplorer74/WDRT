using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005E6 RID: 1510
	[Serializable]
	internal sealed class MdFieldInfo : RuntimeFieldInfo, ISerializable
	{
		// Token: 0x06004653 RID: 18003 RVA: 0x00103A88 File Offset: 0x00101C88
		internal MdFieldInfo(int tkField, FieldAttributes fieldAttributes, RuntimeTypeHandle declaringTypeHandle, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags)
			: base(reflectedTypeCache, declaringTypeHandle.GetRuntimeType(), bindingFlags)
		{
			this.m_tkField = tkField;
			this.m_name = null;
			this.m_fieldAttributes = fieldAttributes;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x00103AB0 File Offset: 0x00101CB0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			MdFieldInfo mdFieldInfo = o as MdFieldInfo;
			return mdFieldInfo != null && mdFieldInfo.m_tkField == this.m_tkField && this.m_declaringType.GetTypeHandleInternal().GetModuleHandle().Equals(mdFieldInfo.m_declaringType.GetTypeHandleInternal().GetModuleHandle());
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x00103B08 File Offset: 0x00101D08
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.GetRuntimeModule().MetadataImport.GetName(this.m_tkField).ToString();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x00103B50 File Offset: 0x00101D50
		public override int MetadataToken
		{
			get
			{
				return this.m_tkField;
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x00103B58 File Offset: 0x00101D58
		internal override RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x00103B65 File Offset: 0x00101D65
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x00103B6C File Offset: 0x00101D6C
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600465A RID: 18010 RVA: 0x00103B74 File Offset: 0x00101D74
		public override bool IsSecurityCritical
		{
			get
			{
				return this.DeclaringType.IsSecurityCritical;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x00103B81 File Offset: 0x00101D81
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.DeclaringType.IsSecuritySafeCritical;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x00103B8E File Offset: 0x00101D8E
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.DeclaringType.IsSecurityTransparent;
			}
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x00103B9B File Offset: 0x00101D9B
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValueDirect(TypedReference obj)
		{
			return this.GetValue(null);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x00103BA4 File Offset: 0x00101DA4
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x00103BB5 File Offset: 0x00101DB5
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj)
		{
			return this.GetValue(false);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x00103BBE File Offset: 0x00101DBE
		public override object GetRawConstantValue()
		{
			return this.GetValue(true);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x00103BC8 File Offset: 0x00101DC8
		[SecuritySafeCritical]
		private object GetValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_tkField, this.FieldType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
			}
			return value;
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x00103C11 File Offset: 0x00101E11
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x00103C24 File Offset: 0x00101E24
		public override Type FieldType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_fieldType == null)
				{
					ConstArray sigOfFieldDef = this.GetRuntimeModule().MetadataImport.GetSigOfFieldDef(this.m_tkField);
					this.m_fieldType = new Signature(sigOfFieldDef.Signature.ToPointer(), sigOfFieldDef.Length, this.m_declaringType).FieldType;
				}
				return this.m_fieldType;
			}
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x00103C8B File Offset: 0x00101E8B
		public override Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x00103C92 File Offset: 0x00101E92
		public override Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x04001CC9 RID: 7369
		private int m_tkField;

		// Token: 0x04001CCA RID: 7370
		private string m_name;

		// Token: 0x04001CCB RID: 7371
		private RuntimeType m_fieldType;

		// Token: 0x04001CCC RID: 7372
		private FieldAttributes m_fieldAttributes;
	}
}

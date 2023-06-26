using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x0200073E RID: 1854
	internal sealed class SerializationFieldInfo : FieldInfo
	{
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060051F0 RID: 20976 RVA: 0x0012160F File Offset: 0x0011F80F
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0012161C File Offset: 0x0011F81C
		public override int MetadataToken
		{
			get
			{
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00121629 File Offset: 0x0011F829
		internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
		{
			this.m_field = field;
			this.m_serializationName = namePrefix + "+" + this.m_field.Name;
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060051F3 RID: 20979 RVA: 0x00121654 File Offset: 0x0011F854
		public override string Name
		{
			get
			{
				return this.m_serializationName;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060051F4 RID: 20980 RVA: 0x0012165C File Offset: 0x0011F85C
		public override Type DeclaringType
		{
			get
			{
				return this.m_field.DeclaringType;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x00121669 File Offset: 0x0011F869
		public override Type ReflectedType
		{
			get
			{
				return this.m_field.ReflectedType;
			}
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00121676 File Offset: 0x0011F876
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00121684 File Offset: 0x0011F884
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00121693 File Offset: 0x0011F893
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060051F9 RID: 20985 RVA: 0x001216A2 File Offset: 0x0011F8A2
		public override Type FieldType
		{
			get
			{
				return this.m_field.FieldType;
			}
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x001216AF File Offset: 0x0011F8AF
		public override object GetValue(object obj)
		{
			return this.m_field.GetValue(obj);
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x001216C0 File Offset: 0x0011F8C0
		[SecurityCritical]
		internal object InternalGetValue(object obj)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				return rtFieldInfo.UnsafeGetValue(obj);
			}
			return this.m_field.GetValue(obj);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x001216FD File Offset: 0x0011F8FD
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00121714 File Offset: 0x0011F914
		[SecurityCritical]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				rtFieldInfo.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
				return;
			}
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060051FE RID: 20990 RVA: 0x0012175D File Offset: 0x0011F95D
		internal RuntimeFieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x00121765 File Offset: 0x0011F965
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.m_field.FieldHandle;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06005200 RID: 20992 RVA: 0x00121772 File Offset: 0x0011F972
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x00121780 File Offset: 0x0011F980
		internal RemotingFieldCachedData RemotingCache
		{
			get
			{
				RemotingFieldCachedData remotingFieldCachedData = this.m_cachedData;
				if (remotingFieldCachedData == null)
				{
					remotingFieldCachedData = new RemotingFieldCachedData(this);
					RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData, null);
					if (remotingFieldCachedData2 != null)
					{
						remotingFieldCachedData = remotingFieldCachedData2;
					}
				}
				return remotingFieldCachedData;
			}
		}

		// Token: 0x0400244D RID: 9293
		internal const string FakeNameSeparatorString = "+";

		// Token: 0x0400244E RID: 9294
		private RuntimeFieldInfo m_field;

		// Token: 0x0400244F RID: 9295
		private string m_serializationName;

		// Token: 0x04002450 RID: 9296
		private RemotingFieldCachedData m_cachedData;
	}
}

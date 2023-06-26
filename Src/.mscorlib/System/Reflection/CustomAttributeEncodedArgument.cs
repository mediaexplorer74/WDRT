using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005D7 RID: 1495
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeEncodedArgument
	{
		// Token: 0x0600457B RID: 17787
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParseAttributeArguments(IntPtr pCa, int cCa, ref CustomAttributeCtorParameter[] CustomAttributeCtorParameters, ref CustomAttributeNamedParameter[] CustomAttributeTypedArgument, RuntimeAssembly assembly);

		// Token: 0x0600457C RID: 17788 RVA: 0x00100C54 File Offset: 0x000FEE54
		[SecurityCritical]
		internal static void ParseAttributeArguments(ConstArray attributeBlob, ref CustomAttributeCtorParameter[] customAttributeCtorParameters, ref CustomAttributeNamedParameter[] customAttributeNamedParameters, RuntimeModule customAttributeModule)
		{
			if (customAttributeModule == null)
			{
				throw new ArgumentNullException("customAttributeModule");
			}
			if (customAttributeCtorParameters.Length != 0 || customAttributeNamedParameters.Length != 0)
			{
				CustomAttributeEncodedArgument.ParseAttributeArguments(attributeBlob.Signature, attributeBlob.Length, ref customAttributeCtorParameters, ref customAttributeNamedParameters, (RuntimeAssembly)customAttributeModule.Assembly);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x00100C94 File Offset: 0x000FEE94
		public CustomAttributeType CustomAttributeType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x00100C9C File Offset: 0x000FEE9C
		public long PrimitiveValue
		{
			get
			{
				return this.m_primitiveValue;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x00100CA4 File Offset: 0x000FEEA4
		public CustomAttributeEncodedArgument[] ArrayValue
		{
			get
			{
				return this.m_arrayValue;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x00100CAC File Offset: 0x000FEEAC
		public string StringValue
		{
			get
			{
				return this.m_stringValue;
			}
		}

		// Token: 0x04001C83 RID: 7299
		private long m_primitiveValue;

		// Token: 0x04001C84 RID: 7300
		private CustomAttributeEncodedArgument[] m_arrayValue;

		// Token: 0x04001C85 RID: 7301
		private string m_stringValue;

		// Token: 0x04001C86 RID: 7302
		private CustomAttributeType m_type;
	}
}

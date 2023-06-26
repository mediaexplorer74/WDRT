using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D8 RID: 1496
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeNamedParameter
	{
		// Token: 0x06004581 RID: 17793 RVA: 0x00100CB4 File Offset: 0x000FEEB4
		public CustomAttributeNamedParameter(string argumentName, CustomAttributeEncoding fieldOrProperty, CustomAttributeType type)
		{
			if (argumentName == null)
			{
				throw new ArgumentNullException("argumentName");
			}
			this.m_argumentName = argumentName;
			this.m_fieldOrProperty = fieldOrProperty;
			this.m_padding = fieldOrProperty;
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x00100CEC File Offset: 0x000FEEEC
		public CustomAttributeEncodedArgument EncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x04001C87 RID: 7303
		private string m_argumentName;

		// Token: 0x04001C88 RID: 7304
		private CustomAttributeEncoding m_fieldOrProperty;

		// Token: 0x04001C89 RID: 7305
		private CustomAttributeEncoding m_padding;

		// Token: 0x04001C8A RID: 7306
		private CustomAttributeType m_type;

		// Token: 0x04001C8B RID: 7307
		private CustomAttributeEncodedArgument m_encodedArgument;
	}
}

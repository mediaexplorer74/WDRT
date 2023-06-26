using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Holds the value, <see cref="T:System.Type" />, and name of a serialized object.</summary>
	// Token: 0x02000740 RID: 1856
	[ComVisible(true)]
	public struct SerializationEntry
	{
		/// <summary>Gets the value contained in the object.</summary>
		/// <returns>The value contained in the object.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x0012220E File Offset: 0x0012040E
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Gets the name of the object.</summary>
		/// <returns>The name of the object.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x00122216 File Offset: 0x00120416
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x0012221E File Offset: 0x0012041E
		public Type ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00122226 File Offset: 0x00120426
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this.m_value = entryValue;
			this.m_name = entryName;
			this.m_type = entryType;
		}

		// Token: 0x04002460 RID: 9312
		private Type m_type;

		// Token: 0x04002461 RID: 9313
		private object m_value;

		// Token: 0x04002462 RID: 9314
		private string m_name;
	}
}

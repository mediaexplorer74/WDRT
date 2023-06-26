using System;

namespace System.Reflection
{
	/// <summary>Defines a key/value metadata pair for the decorated assembly.</summary>
	// Token: 0x020005C2 RID: 1474
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> class by using the specified metadata key and value.</summary>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		// Token: 0x0600448D RID: 17549 RVA: 0x000FDCCF File Offset: 0x000FBECF
		[__DynamicallyInvokable]
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.m_key = key;
			this.m_value = value;
		}

		/// <summary>Gets the metadata key.</summary>
		/// <returns>The metadata key.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x000FDCE5 File Offset: 0x000FBEE5
		[__DynamicallyInvokable]
		public string Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_key;
			}
		}

		/// <summary>Gets the metadata value.</summary>
		/// <returns>The metadata value.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x000FDCED File Offset: 0x000FBEED
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001C15 RID: 7189
		private string m_key;

		// Token: 0x04001C16 RID: 7190
		private string m_value;
	}
}

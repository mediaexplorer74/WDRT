using System;

namespace System.Configuration
{
	/// <summary>Specifies the default value for an application settings property.</summary>
	// Token: 0x0200009A RID: 154
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultSettingValueAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.DefaultSettingValueAttribute" /> class.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that represents the default value for the property.</param>
		// Token: 0x06000593 RID: 1427 RVA: 0x0002297E File Offset: 0x00020B7E
		public DefaultSettingValueAttribute(string value)
		{
			this._value = value;
		}

		/// <summary>Gets the default value for the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the default value for the property.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0002298D File Offset: 0x00020B8D
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000C31 RID: 3121
		private readonly string _value;
	}
}

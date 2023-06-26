using System;

namespace System.Configuration
{
	/// <summary>Indicates that an application settings property has a special significance. This class cannot be inherited.</summary>
	// Token: 0x020000A3 RID: 163
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SpecialSettingAttribute" /> class.</summary>
		/// <param name="specialSetting">A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</param>
		// Token: 0x060005A4 RID: 1444 RVA: 0x00022A4C File Offset: 0x00020C4C
		public SpecialSettingAttribute(SpecialSetting specialSetting)
		{
			this._specialSetting = specialSetting;
		}

		/// <summary>Gets the value describing the special setting category of the application settings property.</summary>
		/// <returns>A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00022A5B File Offset: 0x00020C5B
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this._specialSetting;
			}
		}

		// Token: 0x04000C38 RID: 3128
		private readonly SpecialSetting _specialSetting;
	}
}

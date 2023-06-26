using System;

namespace System.Configuration
{
	/// <summary>Specifies that an application settings property has a common value for all users of an application. This class cannot be inherited.</summary>
	// Token: 0x02000099 RID: 153
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ApplicationScopedSettingAttribute : SettingAttribute
	{
	}
}

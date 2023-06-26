using System;

namespace System.Configuration
{
	/// <summary>Specifies that a settings provider should disable any logic that gets invoked when an application upgrade is detected. This class cannot be inherited.</summary>
	// Token: 0x0200009B RID: 155
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NoSettingsVersionUpgradeAttribute : Attribute
	{
	}
}

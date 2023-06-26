using System;

namespace System.Runtime.InteropServices
{
	/// <summary>This attribute has been deprecated.</summary>
	// Token: 0x0200093E RID: 2366
	[Obsolete("This attribute has been deprecated.  Application Domains no longer respect Activation Context boundaries in IDispatch calls.", false)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class SetWin32ContextInIDispatchAttribute : Attribute
	{
	}
}

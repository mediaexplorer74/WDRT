using System;

namespace System.Diagnostics
{
	// Token: 0x020003E0 RID: 992
	[Serializable]
	internal abstract class AssertFilter
	{
		// Token: 0x0600330D RID: 13069
		public abstract AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle);
	}
}

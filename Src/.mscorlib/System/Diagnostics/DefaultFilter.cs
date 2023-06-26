using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003E1 RID: 993
	internal class DefaultFilter : AssertFilter
	{
		// Token: 0x0600330F RID: 13071 RVA: 0x000C628F File Offset: 0x000C448F
		internal DefaultFilter()
		{
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000C6297 File Offset: 0x000C4497
		[SecuritySafeCritical]
		public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
		{
			return (AssertFilters)Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
		}
	}
}

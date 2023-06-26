using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies kinds of exception-handling clauses.</summary>
	// Token: 0x0200060F RID: 1551
	[Flags]
	[ComVisible(true)]
	public enum ExceptionHandlingClauseOptions
	{
		/// <summary>The clause accepts all exceptions that derive from a specified type.</summary>
		// Token: 0x04001DD2 RID: 7634
		Clause = 0,
		/// <summary>The clause contains user-specified instructions that determine whether the exception should be ignored (that is, whether normal execution should resume), be handled by the associated handler, or be passed on to the next clause.</summary>
		// Token: 0x04001DD3 RID: 7635
		Filter = 1,
		/// <summary>The clause is executed whenever the try block exits, whether through normal control flow or because of an unhandled exception.</summary>
		// Token: 0x04001DD4 RID: 7636
		Finally = 2,
		/// <summary>The clause is executed if an exception occurs, but not on completion of normal control flow.</summary>
		// Token: 0x04001DD5 RID: 7637
		Fault = 4
	}
}

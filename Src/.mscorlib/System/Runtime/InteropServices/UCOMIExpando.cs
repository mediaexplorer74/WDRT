using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000984 RID: 2436
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IExpando instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIExpando : UCOMIReflect
	{
		// Token: 0x060062B7 RID: 25271
		FieldInfo AddField(string name);

		// Token: 0x060062B8 RID: 25272
		PropertyInfo AddProperty(string name);

		// Token: 0x060062B9 RID: 25273
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x060062BA RID: 25274
		void RemoveMember(MemberInfo m);
	}
}

using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2D RID: 2605
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x0600664A RID: 26186
		FieldInfo AddField(string name);

		// Token: 0x0600664B RID: 26187
		PropertyInfo AddProperty(string name);

		// Token: 0x0600664C RID: 26188
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x0600664D RID: 26189
		void RemoveMember(MemberInfo m);
	}
}

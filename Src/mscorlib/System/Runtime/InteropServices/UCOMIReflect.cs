using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000988 RID: 2440
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IReflect instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIReflect
	{
		// Token: 0x060062D5 RID: 25301
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060062D6 RID: 25302
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x060062D7 RID: 25303
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060062D8 RID: 25304
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060062D9 RID: 25305
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060062DA RID: 25306
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x060062DB RID: 25307
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060062DC RID: 25308
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x060062DD RID: 25309
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x060062DE RID: 25310
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060062DF RID: 25311
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x060062E0 RID: 25312
		Type UnderlyingSystemType { get; }
	}
}

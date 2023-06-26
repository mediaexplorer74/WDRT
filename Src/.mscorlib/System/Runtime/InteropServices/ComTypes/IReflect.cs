using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A31 RID: 2609
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x06006668 RID: 26216
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06006669 RID: 26217
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x0600666A RID: 26218
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x0600666B RID: 26219
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x0600666C RID: 26220
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x0600666D RID: 26221
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x0600666E RID: 26222
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600666F RID: 26223
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06006670 RID: 26224
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06006671 RID: 26225
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06006672 RID: 26226
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06006673 RID: 26227
		Type UnderlyingSystemType { get; }
	}
}

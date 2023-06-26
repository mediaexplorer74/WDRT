using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000281 RID: 641
	internal class HtmlToClrEventProxy : IReflect
	{
		// Token: 0x06002915 RID: 10517 RVA: 0x000BCBFC File Offset: 0x000BADFC
		public HtmlToClrEventProxy(object sender, string eventName, EventHandler eventHandler)
		{
			this.eventHandler = eventHandler;
			this.eventName = eventName;
			Type typeFromHandle = typeof(HtmlToClrEventProxy);
			this.typeIReflectImplementation = typeFromHandle;
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x000BCC2F File Offset: 0x000BAE2F
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000BCC37 File Offset: 0x000BAE37
		[DispId(0)]
		public void OnHtmlEvent()
		{
			this.InvokeClrEvent();
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000BCC3F File Offset: 0x000BAE3F
		private void InvokeClrEvent()
		{
			if (this.eventHandler != null)
			{
				this.eventHandler(this.sender, EventArgs.Empty);
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x000BCC5F File Offset: 0x000BAE5F
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return this.typeIReflectImplementation.UnderlyingSystemType;
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000BCC6C File Offset: 0x000BAE6C
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetField(name, bindingAttr);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000BCC7B File Offset: 0x000BAE7B
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetFields(bindingAttr);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000BCC89 File Offset: 0x000BAE89
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMember(name, bindingAttr);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000BCC98 File Offset: 0x000BAE98
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMembers(bindingAttr);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000BCCA6 File Offset: 0x000BAEA6
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMethod(name, bindingAttr);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000BCCB5 File Offset: 0x000BAEB5
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeIReflectImplementation.GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000BCCC9 File Offset: 0x000BAEC9
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMethods(bindingAttr);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000BCCD7 File Offset: 0x000BAED7
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetProperties(bindingAttr);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000BCCE5 File Offset: 0x000BAEE5
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetProperty(name, bindingAttr);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000BCCF4 File Offset: 0x000BAEF4
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeIReflectImplementation.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000BCD0C File Offset: 0x000BAF0C
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if (name == "[DISPID=0]")
			{
				this.OnHtmlEvent();
				return null;
			}
			return this.typeIReflectImplementation.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x040010CC RID: 4300
		private EventHandler eventHandler;

		// Token: 0x040010CD RID: 4301
		private IReflect typeIReflectImplementation;

		// Token: 0x040010CE RID: 4302
		private object sender;

		// Token: 0x040010CF RID: 4303
		private string eventName;
	}
}

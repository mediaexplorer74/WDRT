using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A1 RID: 1441
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x06003590 RID: 13712 RVA: 0x000E875C File Offset: 0x000E695C
		public ReflectEventDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "componentClass" }));
			}
			if (type == null || !typeof(Delegate).IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.GetString("ErrorInvalidEventType", new object[] { name }));
			}
			this.componentClass = componentClass;
			this.type = type;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000E87DC File Offset: 0x000E69DC
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo)
			: base(eventInfo.Name, new Attribute[0])
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "componentClass" }));
			}
			this.componentClass = componentClass;
			this.realEvent = eventInfo;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000E8830 File Offset: 0x000E6A30
		public ReflectEventDescriptor(Type componentType, EventDescriptor oldReflectEventDescriptor, Attribute[] attributes)
			: base(oldReflectEventDescriptor, attributes)
		{
			this.componentClass = componentType;
			this.type = oldReflectEventDescriptor.EventType;
			ReflectEventDescriptor reflectEventDescriptor = oldReflectEventDescriptor as ReflectEventDescriptor;
			if (reflectEventDescriptor != null)
			{
				this.addMethod = reflectEventDescriptor.addMethod;
				this.removeMethod = reflectEventDescriptor.removeMethod;
				this.filledMethods = true;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000E8881 File Offset: 0x000E6A81
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000E8889 File Offset: 0x000E6A89
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this.type;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000E8897 File Offset: 0x000E6A97
		public override bool IsMulticast
		{
			get
			{
				return typeof(MulticastDelegate).IsAssignableFrom(this.EventType);
			}
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000E88B0 File Offset: 0x000E6AB0
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(SR.GetString("ErrorInvalidEventHandler", new object[] { this.Name }));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					SecurityUtils.MethodInfoInvoke(this.addMethod, component, new object[] { value });
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000E89C0 File Offset: 0x000E6BC0
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this.realEvent != null)
			{
				this.FillEventInfoAttribute(this.realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this.removeMethod, attributes);
				this.FillSingleMethodAttribute(this.addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000E8A14 File Offset: 0x000E6C14
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingFlags);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000E8AF0 File Offset: 0x000E6CF0
		private void FillMethods()
		{
			if (this.filledMethods)
			{
				return;
			}
			if (this.realEvent != null)
			{
				this.addMethod = this.realEvent.GetAddMethod();
				this.removeMethod = this.realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this.addMethod == null || this.removeMethod == null)
				{
					Type baseType = this.componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this.realEvent.Name, bindingFlags);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this.addMethod = eventInfo.GetAddMethod();
					this.removeMethod = eventInfo.GetRemoveMethod();
					this.type = eventInfo.EventHandlerType;
				}
				else
				{
					this.type = this.realEvent.EventHandlerType;
				}
			}
			else
			{
				this.realEvent = this.componentClass.GetEvent(this.Name);
				if (this.realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] array = new Type[] { this.type };
				this.addMethod = MemberDescriptor.FindMethod(this.componentClass, "AddOn" + this.Name, array, typeof(void));
				this.removeMethod = MemberDescriptor.FindMethod(this.componentClass, "RemoveOn" + this.Name, array, typeof(void));
				if (this.addMethod == null || this.removeMethod == null)
				{
					throw new ArgumentException(SR.GetString("ErrorMissingEventAccessors", new object[] { this.Name }));
				}
			}
			this.filledMethods = true;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000E8CCC File Offset: 0x000E6ECC
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingFlags);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000E8DBC File Offset: 0x000E6FBC
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					SecurityUtils.MethodInfoInvoke(this.removeMethod, component, new object[] { value });
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x04002A44 RID: 10820
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x04002A45 RID: 10821
		private static readonly object noDefault = new object();

		// Token: 0x04002A46 RID: 10822
		private Type type;

		// Token: 0x04002A47 RID: 10823
		private readonly Type componentClass;

		// Token: 0x04002A48 RID: 10824
		private MethodInfo addMethod;

		// Token: 0x04002A49 RID: 10825
		private MethodInfo removeMethod;

		// Token: 0x04002A4A RID: 10826
		private EventInfo realEvent;

		// Token: 0x04002A4B RID: 10827
		private bool filledMethods;
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A2 RID: 1442
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600359D RID: 13725 RVA: 0x000E8EB0 File Offset: 0x000E70B0
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			try
			{
				if (type == null)
				{
					throw new ArgumentException(SR.GetString("ErrorInvalidPropertyType", new object[] { name }));
				}
				if (componentClass == null)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "componentClass" }));
				}
				this.type = type;
				this.componentClass = componentClass;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000E8F34 File Offset: 0x000E7134
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, PropertyInfo propInfo, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.propInfo = propInfo;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			if (getMethod != null && propInfo != null && setMethod == null)
			{
				this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetOnDemand] = true;
				return;
			}
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000E8FB1 File Offset: 0x000E71B1
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Type receiverType, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.receiverType = receiverType;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000E8FF0 File Offset: 0x000E71F0
		public ReflectPropertyDescriptor(Type componentClass, PropertyDescriptor oldReflectPropertyDescriptor, Attribute[] attributes)
			: base(oldReflectPropertyDescriptor, attributes)
		{
			this.componentClass = componentClass;
			this.type = oldReflectPropertyDescriptor.PropertyType;
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "componentClass" }));
			}
			ReflectPropertyDescriptor reflectPropertyDescriptor = oldReflectPropertyDescriptor as ReflectPropertyDescriptor;
			if (reflectPropertyDescriptor != null)
			{
				if (reflectPropertyDescriptor.ComponentType == componentClass)
				{
					this.propInfo = reflectPropertyDescriptor.propInfo;
					this.getMethod = reflectPropertyDescriptor.getMethod;
					this.setMethod = reflectPropertyDescriptor.setMethod;
					this.shouldSerializeMethod = reflectPropertyDescriptor.shouldSerializeMethod;
					this.resetMethod = reflectPropertyDescriptor.resetMethod;
					this.defaultValue = reflectPropertyDescriptor.defaultValue;
					this.ambientValue = reflectPropertyDescriptor.ambientValue;
					this.state = reflectPropertyDescriptor.state;
				}
				if (attributes != null)
				{
					foreach (Attribute attribute in attributes)
					{
						DefaultValueAttribute defaultValueAttribute = attribute as DefaultValueAttribute;
						if (defaultValueAttribute != null)
						{
							this.defaultValue = defaultValueAttribute.Value;
							if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
							{
								this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
							}
							this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
						}
						else
						{
							AmbientValueAttribute ambientValueAttribute = attribute as AmbientValueAttribute;
							if (ambientValueAttribute != null)
							{
								this.ambientValue = ambientValueAttribute.Value;
								this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000E917C File Offset: 0x000E737C
		private object AmbientValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitAmbientValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(AmbientValueAttribute)];
					if (attribute != null)
					{
						this.ambientValue = ((AmbientValueAttribute)attribute).Value;
					}
					else
					{
						this.ambientValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.ambientValue;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000E91EC File Offset: 0x000E73EC
		private EventDescriptor ChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitChangedQueried] = true;
					this.realChangedEvent = TypeDescriptor.GetEvents(this.ComponentType)[string.Format(CultureInfo.InvariantCulture, "{0}Changed", new object[] { this.Name })];
				}
				return this.realChangedEvent;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000E9258 File Offset: 0x000E7458
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x000E92C4 File Offset: 0x000E74C4
		private EventDescriptor IPropChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitIPropChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
					if (typeof(INotifyPropertyChanged).IsAssignableFrom(this.ComponentType))
					{
						this.realIPropChangedEvent = TypeDescriptor.GetEvents(typeof(INotifyPropertyChanged))["PropertyChanged"];
					}
				}
				return this.realIPropChangedEvent;
			}
			set
			{
				this.realIPropChangedEvent = value;
				this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000E92DE File Offset: 0x000E74DE
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000E92E8 File Offset: 0x000E74E8
		private object DefaultValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitDefaultValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(DefaultValueAttribute)];
					if (attribute != null)
					{
						this.defaultValue = ((DefaultValueAttribute)attribute).Value;
						if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
						{
							this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
						}
					}
					else
					{
						this.defaultValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x000E93A4 File Offset: 0x000E75A4
		private MethodInfo GetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitGetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitGetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.getMethod = this.propInfo.GetGetMethod(true);
						}
						if (this.getMethod == null)
						{
							throw new InvalidOperationException(SR.GetString("ErrorMissingPropertyAccessors", new object[] { this.componentClass.FullName + "." + this.Name }));
						}
					}
					else
					{
						this.getMethod = MemberDescriptor.FindMethod(this.componentClass, "Get" + this.Name, new Type[] { this.receiverType }, this.type);
						if (this.getMethod == null)
						{
							throw new ArgumentException(SR.GetString("ErrorMissingPropertyAccessors", new object[] { this.Name }));
						}
					}
				}
				return this.getMethod;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000E94F5 File Offset: 0x000E76F5
		private bool IsExtender
		{
			get
			{
				return this.receiverType != null;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000E9503 File Offset: 0x000E7703
		public override bool IsReadOnly
		{
			get
			{
				return this.SetMethodValue == null || ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000E9534 File Offset: 0x000E7734
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000E953C File Offset: 0x000E773C
		private MethodInfo ResetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitResetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitResetQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					IntSecurity.FullReflection.Assert();
					try
					{
						this.resetMethod = MemberDescriptor.FindMethod(this.componentClass, "Reset" + this.Name, array, typeof(void), false);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.resetMethod;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000E95E8 File Offset: 0x000E77E8
		private MethodInfo SetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried] && this.state[ReflectPropertyDescriptor.BitSetOnDemand])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					string name = this.propInfo.Name;
					if (this.setMethod == null)
					{
						Type type = this.ComponentType.BaseType;
						while (type != null && type != typeof(object) && !(type == null))
						{
							PropertyInfo property = type.GetProperty(name, bindingFlags, null, this.PropertyType, new Type[0], null);
							if (property != null)
							{
								this.setMethod = property.GetSetMethod();
								if (this.setMethod != null)
								{
									break;
								}
							}
							type = type.BaseType;
						}
					}
				}
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags2 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags2, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.setMethod = this.propInfo.GetSetMethod(true);
						}
					}
					else
					{
						this.setMethod = MemberDescriptor.FindMethod(this.componentClass, "Set" + this.Name, new Type[] { this.receiverType, this.type }, typeof(void));
					}
				}
				return this.setMethod;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060035AD RID: 13741 RVA: 0x000E97A0 File Offset: 0x000E79A0
		private MethodInfo ShouldSerializeMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried])
				{
					this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					IntSecurity.FullReflection.Assert();
					try
					{
						this.shouldSerializeMethod = MemberDescriptor.FindMethod(this.componentClass, "ShouldSerialize" + this.Name, array, typeof(bool), false);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.shouldSerializeMethod;
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000E984C File Offset: 0x000E7A4C
		public override void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.AddEventHandler(component, handler);
				return;
			}
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.AddEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
			base.AddValueChanged(component, handler);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000E98C4 File Offset: 0x000E7AC4
		internal bool ExtenderCanResetValue(IExtenderProvider provider, object component)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.ExtenderGetValue(provider, component), this.defaultValue);
			}
			MethodInfo resetMethodValue = this.ResetMethodValue;
			if (resetMethodValue != null)
			{
				MethodInfo shouldSerializeMethodValue = this.ShouldSerializeMethodValue;
				if (shouldSerializeMethodValue != null)
				{
					try
					{
						provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
						return (bool)shouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000E995C File Offset: 0x000E7B5C
		internal Type ExtenderGetReceiverType()
		{
			return this.receiverType;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000E9964 File Offset: 0x000E7B64
		internal Type ExtenderGetType(IExtenderProvider provider)
		{
			return this.PropertyType;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000E996C File Offset: 0x000E7B6C
		internal object ExtenderGetValue(IExtenderProvider provider, object component)
		{
			if (provider != null)
			{
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				return this.GetMethodValue.Invoke(provider, new object[] { component });
			}
			return null;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000E99A0 File Offset: 0x000E7BA0
		internal void ExtenderResetValue(IExtenderProvider provider, object component, PropertyDescriptor notifyDesc)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.DefaultValue, notifyDesc);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.AmbientValue, notifyDesc);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
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
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.ResetMethodValue != null)
				{
					this.ResetMethodValue.Invoke(provider, new object[] { component });
					if (componentChangeService != null)
					{
						object obj2 = this.ExtenderGetValue(provider, component);
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, obj2);
					}
				}
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000E9AA4 File Offset: 0x000E7CA4
		internal void ExtenderSetValue(IExtenderProvider provider, object component, object value, PropertyDescriptor notifyDesc)
		{
			if (provider != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
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
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.SetMethodValue != null)
				{
					this.SetMethodValue.Invoke(provider, new object[] { component, value });
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, value);
					}
				}
			}
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000E9B5C File Offset: 0x000E7D5C
		internal bool ExtenderShouldSerializeValue(IExtenderProvider provider, object component)
		{
			provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.ExtenderGetValue(provider, component));
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000E9C38 File Offset: 0x000E7E38
		public override bool CanResetValue(object component)
		{
			if (this.IsExtender || this.IsReadOnly)
			{
				return false;
			}
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.GetValue(component), this.DefaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					component = this.GetInvocationTarget(this.componentClass, component);
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return this.AmbientValue != ReflectPropertyDescriptor.noValue && this.ShouldSerializeValue(component);
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000E9CE8 File Offset: 0x000E7EE8
		protected override void FillAttributes(IList attributes)
		{
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute attribute = (Attribute)obj;
				attributes.Add(attribute);
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Type type = this.componentClass;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = this.componentClass;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo memberInfo;
					if (this.IsExtender)
					{
						memberInfo = type.GetMethod("Get" + this.Name, bindingFlags, null, new Type[] { this.receiverType }, null);
					}
					else
					{
						memberInfo = type.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
					}
					if (memberInfo != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(memberInfo);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute2 in array3)
						{
							AttributeProviderAttribute attributeProviderAttribute = attribute2 as AttributeProviderAttribute;
							if (attributeProviderAttribute != null)
							{
								Type type2 = Type.GetType(attributeProviderAttribute.TypeName);
								if (type2 != null)
								{
									Attribute[] array5 = null;
									if (!string.IsNullOrEmpty(attributeProviderAttribute.PropertyName))
									{
										MemberInfo[] member = type2.GetMember(attributeProviderAttribute.PropertyName);
										if (member.Length != 0 && member[0] != null)
										{
											array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(member[0]);
										}
									}
									else
									{
										array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type2);
									}
									if (array5 != null)
									{
										foreach (Attribute attribute3 in array5)
										{
											attributes.Add(attribute3);
										}
									}
								}
							}
						}
					}
				}
				foreach (Attribute[] array8 in array)
				{
					if (array8 != null)
					{
						foreach (Attribute attribute4 in array8)
						{
							attributes.Add(attribute4);
						}
					}
				}
			}
			base.FillAttributes(attributes);
			if (this.SetMethodValue == null)
			{
				attributes.Add(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000E9F84 File Offset: 0x000E8184
		public override object GetValue(object component)
		{
			if (this.IsExtender)
			{
				return null;
			}
			if (component != null)
			{
				component = this.GetInvocationTarget(this.componentClass, component);
				try
				{
					return SecurityUtils.MethodInfoInvoke(this.GetMethodValue, component, null);
				}
				catch (Exception innerException)
				{
					string text = null;
					IComponent component2 = component as IComponent;
					if (component2 != null)
					{
						ISite site = component2.Site;
						if (site != null && site.Name != null)
						{
							text = site.Name;
						}
					}
					if (text == null)
					{
						text = component.GetType().FullName;
					}
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					string text2 = innerException.Message;
					if (text2 == null)
					{
						text2 = innerException.GetType().Name;
					}
					throw new TargetInvocationException(SR.GetString("ErrorPropertyAccessorException", new object[] { this.Name, text, text2 }), innerException);
				}
			}
			return null;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000EA060 File Offset: 0x000E8260
		internal void OnINotifyPropertyChanged(object component, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName) || string.Compare(e.PropertyName, this.Name, true, CultureInfo.InvariantCulture) == 0)
			{
				this.OnValueChanged(component, e);
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000EA090 File Offset: 0x000E8290
		protected override void OnValueChanged(object component, EventArgs e)
		{
			if (this.state[ReflectPropertyDescriptor.BitChangedQueried] && this.realChangedEvent == null)
			{
				base.OnValueChanged(component, e);
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000EA0B4 File Offset: 0x000E82B4
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.RemoveEventHandler(component, handler);
				return;
			}
			base.RemoveValueChanged(component, handler);
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.RemoveEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000EA12C File Offset: 0x000E832C
		public override void ResetValue(object component)
		{
			object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.DefaultValue);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.AmbientValue);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
				if (this.ResetMethodValue != null)
				{
					SecurityUtils.MethodInfoInvoke(this.ResetMethodValue, invocationTarget, null);
					if (componentChangeService != null)
					{
						object obj2 = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						componentChangeService.OnComponentChanged(component, this, obj, obj2);
					}
				}
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000EA228 File Offset: 0x000E8428
		public override void SetValue(object component, object value)
		{
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
				if (!this.IsReadOnly)
				{
					if (site != null)
					{
						componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
					if (componentChangeService != null)
					{
						obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
					try
					{
						SecurityUtils.MethodInfoInvoke(this.SetMethodValue, invocationTarget, new object[] { value });
						this.OnValueChanged(invocationTarget, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						value = obj;
						if (ex2 is TargetInvocationException && ex2.InnerException != null)
						{
							throw ex2.InnerException;
						}
						throw ex2;
					}
					finally
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, this, obj, value);
						}
					}
				}
			}
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000EA324 File Offset: 0x000E8524
		public override bool ShouldSerializeValue(object component)
		{
			component = this.GetInvocationTarget(this.componentClass, component);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.GetValue(component));
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x000EA3E8 File Offset: 0x000E85E8
		public override bool SupportsChangeEvents
		{
			get
			{
				return this.IPropChangedEventValue != null || this.ChangedEventValue != null;
			}
		}

		// Token: 0x04002A4C RID: 10828
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x04002A4D RID: 10829
		private static readonly object noValue = new object();

		// Token: 0x04002A4E RID: 10830
		private static TraceSwitch PropDescCreateSwitch = new TraceSwitch("PropDescCreate", "ReflectPropertyDescriptor: Dump errors when creating property info");

		// Token: 0x04002A4F RID: 10831
		private static TraceSwitch PropDescUsageSwitch = new TraceSwitch("PropDescUsage", "ReflectPropertyDescriptor: Debug propertydescriptor usage");

		// Token: 0x04002A50 RID: 10832
		private static TraceSwitch PropDescSwitch = new TraceSwitch("PropDesc", "ReflectPropertyDescriptor: Debug property descriptor");

		// Token: 0x04002A51 RID: 10833
		private static readonly int BitDefaultValueQueried = BitVector32.CreateMask();

		// Token: 0x04002A52 RID: 10834
		private static readonly int BitGetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitDefaultValueQueried);

		// Token: 0x04002A53 RID: 10835
		private static readonly int BitSetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitGetQueried);

		// Token: 0x04002A54 RID: 10836
		private static readonly int BitShouldSerializeQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitSetQueried);

		// Token: 0x04002A55 RID: 10837
		private static readonly int BitResetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitShouldSerializeQueried);

		// Token: 0x04002A56 RID: 10838
		private static readonly int BitChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitResetQueried);

		// Token: 0x04002A57 RID: 10839
		private static readonly int BitIPropChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitChangedQueried);

		// Token: 0x04002A58 RID: 10840
		private static readonly int BitReadOnlyChecked = BitVector32.CreateMask(ReflectPropertyDescriptor.BitIPropChangedQueried);

		// Token: 0x04002A59 RID: 10841
		private static readonly int BitAmbientValueQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitReadOnlyChecked);

		// Token: 0x04002A5A RID: 10842
		private static readonly int BitSetOnDemand = BitVector32.CreateMask(ReflectPropertyDescriptor.BitAmbientValueQueried);

		// Token: 0x04002A5B RID: 10843
		private BitVector32 state;

		// Token: 0x04002A5C RID: 10844
		private Type componentClass;

		// Token: 0x04002A5D RID: 10845
		private Type type;

		// Token: 0x04002A5E RID: 10846
		private object defaultValue;

		// Token: 0x04002A5F RID: 10847
		private object ambientValue;

		// Token: 0x04002A60 RID: 10848
		private PropertyInfo propInfo;

		// Token: 0x04002A61 RID: 10849
		private MethodInfo getMethod;

		// Token: 0x04002A62 RID: 10850
		private MethodInfo setMethod;

		// Token: 0x04002A63 RID: 10851
		private MethodInfo shouldSerializeMethod;

		// Token: 0x04002A64 RID: 10852
		private MethodInfo resetMethod;

		// Token: 0x04002A65 RID: 10853
		private EventDescriptor realChangedEvent;

		// Token: 0x04002A66 RID: 10854
		private EventDescriptor realIPropChangedEvent;

		// Token: 0x04002A67 RID: 10855
		private Type receiverType;
	}
}

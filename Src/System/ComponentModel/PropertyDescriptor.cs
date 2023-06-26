using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides an abstraction of a property on a class.</summary>
	// Token: 0x0200059B RID: 1435
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class PropertyDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the specified name and attributes.</summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the property attributes.</param>
		// Token: 0x06003523 RID: 13603 RVA: 0x000E744C File Offset: 0x000E564C
		protected PropertyDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the property and its attributes.</param>
		// Token: 0x06003524 RID: 13604 RVA: 0x000E7456 File Offset: 0x000E5656
		protected PropertyDescriptor(MemberDescriptor descr)
			: base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> containing the name of the member and its attributes.</param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array containing the attributes you want to associate with the property.</param>
		// Token: 0x06003525 RID: 13605 RVA: 0x000E745F File Offset: 0x000E565F
		protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
			: base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of the component this property is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06003526 RID: 13606
		public abstract Type ComponentType { get; }

		/// <summary>Gets the type converter for this property.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is used to convert the <see cref="T:System.Type" /> of this property.</returns>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000E746C File Offset: 0x000E566C
		public virtual TypeConverter Converter
		{
			get
			{
				AttributeCollection attributes = this.Attributes;
				if (this.converter == null)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute.ConverterTypeName != null && typeConverterAttribute.ConverterTypeName.Length > 0)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							this.converter = (TypeConverter)this.CreateInstance(typeFromName);
						}
					}
					if (this.converter == null)
					{
						this.converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
				}
				return this.converter;
			}
		}

		/// <summary>Gets a value indicating whether this property should be localized, as specified in the <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the member is marked with the <see cref="T:System.ComponentModel.LocalizableAttribute" /> set to <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000E750D File Offset: 0x000E570D
		public virtual bool IsLocalizable
		{
			get
			{
				return LocalizableAttribute.Yes.Equals(this.Attributes[typeof(LocalizableAttribute)]);
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether this property is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003529 RID: 13609
		public abstract bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether this property should be serialized, as specified in the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> enumeration values that specifies whether this property should be serialized.</returns>
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000E7530 File Offset: 0x000E5730
		public DesignerSerializationVisibility SerializationVisibility
		{
			get
			{
				DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)this.Attributes[typeof(DesignerSerializationVisibilityAttribute)];
				return designerSerializationVisibilityAttribute.Visibility;
			}
		}

		/// <summary>When overridden in a derived class, gets the type of the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x0600352B RID: 13611
		public abstract Type PropertyType { get; }

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to add the handler for.</param>
		/// <param name="handler">The delegate to add as a listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is <see langword="null" />.</exception>
		// Token: 0x0600352C RID: 13612 RVA: 0x000E7560 File Offset: 0x000E5760
		public virtual void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.valueChangedHandlers == null)
			{
				this.valueChangedHandlers = new Hashtable();
			}
			EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
			this.valueChangedHandlers[component] = Delegate.Combine(eventHandler, handler);
		}

		/// <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
		/// <param name="component">The component to test for reset capability.</param>
		/// <returns>
		///   <see langword="true" /> if resetting the component changes its value; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600352D RID: 13613
		public abstract bool CanResetValue(object component);

		/// <summary>Compares this to another object to see if they are equivalent.</summary>
		/// <param name="obj">The object to compare to this <see cref="T:System.ComponentModel.PropertyDescriptor" />.</param>
		/// <returns>
		///   <see langword="true" /> if the values are equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600352E RID: 13614 RVA: 0x000E75C4 File Offset: 0x000E57C4
		public override bool Equals(object obj)
		{
			try
			{
				if (obj == this)
				{
					return true;
				}
				if (obj == null)
				{
					return false;
				}
				PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
				if (propertyDescriptor != null && propertyDescriptor.NameHashCode == this.NameHashCode && propertyDescriptor.PropertyType == this.PropertyType && propertyDescriptor.Name.Equals(this.Name))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		/// <summary>Creates an instance of the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create.</param>
		/// <returns>A new instance of the type.</returns>
		// Token: 0x0600352F RID: 13615 RVA: 0x000E763C File Offset: 0x000E583C
		protected object CreateInstance(Type type)
		{
			Type[] array = new Type[] { typeof(Type) };
			ConstructorInfo constructor = type.GetConstructor(array);
			if (constructor != null)
			{
				return TypeDescriptor.CreateInstance(null, type, array, new object[] { this.PropertyType });
			}
			return TypeDescriptor.CreateInstance(null, type, null, null);
		}

		/// <summary>Adds the attributes of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty.</param>
		// Token: 0x06003530 RID: 13616 RVA: 0x000E768F File Offset: 0x000E588F
		protected override void FillAttributes(IList attributeList)
		{
			this.converter = null;
			this.editors = null;
			this.editorTypes = null;
			this.editorCount = 0;
			base.FillAttributes(attributeList);
		}

		/// <summary>Returns the default <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />.</summary>
		/// <returns>A collection of property descriptor.</returns>
		// Token: 0x06003531 RID: 13617 RVA: 0x000E76B4 File Offset: 0x000E58B4
		public PropertyDescriptorCollection GetChildProperties()
		{
			return this.GetChildProperties(null, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> using a specified array of attributes as a filter.</summary>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes.</returns>
		// Token: 0x06003532 RID: 13618 RVA: 0x000E76BE File Offset: 0x000E58BE
		public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
		{
			return this.GetChildProperties(null, filter);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object.</summary>
		/// <param name="instance">A component to get the properties for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		// Token: 0x06003533 RID: 13619 RVA: 0x000E76C8 File Offset: 0x000E58C8
		public PropertyDescriptorCollection GetChildProperties(object instance)
		{
			return this.GetChildProperties(instance, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object using a specified array of attributes as a filter.</summary>
		/// <param name="instance">A component to get the properties for.</param>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		// Token: 0x06003534 RID: 13620 RVA: 0x000E76D2 File Offset: 0x000E58D2
		public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			if (instance == null)
			{
				return TypeDescriptor.GetProperties(this.PropertyType, filter);
			}
			return TypeDescriptor.GetProperties(instance, filter);
		}

		/// <summary>Gets an editor of the specified type.</summary>
		/// <param name="editorBaseType">The base type of editor, which is used to differentiate between multiple editors that a property supports.</param>
		/// <returns>An instance of the requested editor type, or <see langword="null" /> if an editor cannot be found.</returns>
		// Token: 0x06003535 RID: 13621 RVA: 0x000E76EC File Offset: 0x000E58EC
		public virtual object GetEditor(Type editorBaseType)
		{
			object obj = null;
			AttributeCollection attributes = this.Attributes;
			if (this.editorTypes != null)
			{
				for (int i = 0; i < this.editorCount; i++)
				{
					if (this.editorTypes[i] == editorBaseType)
					{
						return this.editors[i];
					}
				}
			}
			if (obj == null)
			{
				for (int j = 0; j < attributes.Count; j++)
				{
					EditorAttribute editorAttribute = attributes[j] as EditorAttribute;
					if (editorAttribute != null)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute.EditorBaseTypeName);
						if (editorBaseType == typeFromName)
						{
							Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
							if (typeFromName2 != null)
							{
								obj = this.CreateInstance(typeFromName2);
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					obj = TypeDescriptor.GetEditor(this.PropertyType, editorBaseType);
				}
				if (this.editorTypes == null)
				{
					this.editorTypes = new Type[5];
					this.editors = new object[5];
				}
				if (this.editorCount >= this.editorTypes.Length)
				{
					Type[] array = new Type[this.editorTypes.Length * 2];
					object[] array2 = new object[this.editors.Length * 2];
					Array.Copy(this.editorTypes, array, this.editorTypes.Length);
					Array.Copy(this.editors, array2, this.editors.Length);
					this.editorTypes = array;
					this.editors = array2;
				}
				this.editorTypes[this.editorCount] = editorBaseType;
				object[] array3 = this.editors;
				int num = this.editorCount;
				this.editorCount = num + 1;
				array3[num] = obj;
			}
			return obj;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for this object.</returns>
		// Token: 0x06003536 RID: 13622 RVA: 0x000E7861 File Offset: 0x000E5A61
		public override int GetHashCode()
		{
			return this.NameHashCode ^ this.PropertyType.GetHashCode();
		}

		/// <summary>This method returns the object that should be used during invocation of members.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		/// <returns>The <see cref="T:System.Object" /> that should be used during invocation of members.</returns>
		// Token: 0x06003537 RID: 13623 RVA: 0x000E7878 File Offset: 0x000E5A78
		protected override object GetInvocationTarget(Type type, object instance)
		{
			object obj = base.GetInvocationTarget(type, instance);
			ICustomTypeDescriptor customTypeDescriptor = obj as ICustomTypeDescriptor;
			if (customTypeDescriptor != null)
			{
				obj = customTypeDescriptor.GetPropertyOwner(this);
			}
			return obj;
		}

		/// <summary>Returns a type using its name.</summary>
		/// <param name="typeName">The assembly-qualified name of the type to retrieve.</param>
		/// <returns>A <see cref="T:System.Type" /> that matches the given type name, or <see langword="null" /> if a match cannot be found.</returns>
		// Token: 0x06003538 RID: 13624 RVA: 0x000E78A4 File Offset: 0x000E5AA4
		protected Type GetTypeFromName(string typeName)
		{
			if (typeName == null || typeName.Length == 0)
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			Type type2 = null;
			if (this.ComponentType != null && (type == null || this.ComponentType.Assembly.FullName.Equals(type.Assembly.FullName)))
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					typeName = typeName.Substring(0, num);
				}
				type2 = this.ComponentType.Assembly.GetType(typeName);
			}
			return type2 ?? type;
		}

		/// <summary>When overridden in a derived class, gets the current value of the property on a component.</summary>
		/// <param name="component">The component with the property for which to retrieve the value.</param>
		/// <returns>The value of a property for a given component.</returns>
		// Token: 0x06003539 RID: 13625
		public abstract object GetValue(object component);

		/// <summary>Raises the <c>ValueChanged</c> event that you implemented.</summary>
		/// <param name="component">The object that raises the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600353A RID: 13626 RVA: 0x000E7930 File Offset: 0x000E5B30
		protected virtual void OnValueChanged(object component, EventArgs e)
		{
			if (component != null && this.valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
				if (eventHandler != null)
				{
					eventHandler(component, e);
				}
			}
		}

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to remove the handler for.</param>
		/// <param name="handler">The delegate to remove as a listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is <see langword="null" />.</exception>
		// Token: 0x0600353B RID: 13627 RVA: 0x000E7968 File Offset: 0x000E5B68
		public virtual void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
				eventHandler = (EventHandler)Delegate.Remove(eventHandler, handler);
				if (eventHandler != null)
				{
					this.valueChangedHandlers[component] = eventHandler;
					return;
				}
				this.valueChangedHandlers.Remove(component);
			}
		}

		/// <summary>Retrieves the current set of <c>ValueChanged</c> event handlers for a specific component</summary>
		/// <param name="component">The component for which to retrieve event handlers.</param>
		/// <returns>A combined multicast event handler, or <see langword="null" /> if no event handlers are currently assigned to <paramref name="component" />.</returns>
		// Token: 0x0600353C RID: 13628 RVA: 0x000E79D5 File Offset: 0x000E5BD5
		protected internal EventHandler GetValueChangedHandler(object component)
		{
			if (component != null && this.valueChangedHandlers != null)
			{
				return (EventHandler)this.valueChangedHandlers[component];
			}
			return null;
		}

		/// <summary>When overridden in a derived class, resets the value for this property of the component to the default value.</summary>
		/// <param name="component">The component with the property value that is to be reset to the default value.</param>
		// Token: 0x0600353D RID: 13629
		public abstract void ResetValue(object component);

		/// <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
		/// <param name="component">The component with the property value that is to be set.</param>
		/// <param name="value">The new value.</param>
		// Token: 0x0600353E RID: 13630
		public abstract void SetValue(object component, object value);

		/// <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
		/// <param name="component">The component with the property to be examined for persistence.</param>
		/// <returns>
		///   <see langword="true" /> if the property should be persisted; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600353F RID: 13631
		public abstract bool ShouldSerializeValue(object component);

		/// <summary>Gets a value indicating whether value change notifications for this property may originate from outside the property descriptor.</summary>
		/// <returns>
		///   <see langword="true" /> if value change notifications may originate from outside the property descriptor; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000E79F5 File Offset: 0x000E5BF5
		public virtual bool SupportsChangeEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002A29 RID: 10793
		private TypeConverter converter;

		// Token: 0x04002A2A RID: 10794
		private Hashtable valueChangedHandlers;

		// Token: 0x04002A2B RID: 10795
		private object[] editors;

		// Token: 0x04002A2C RID: 10796
		private Type[] editorTypes;

		// Token: 0x04002A2D RID: 10797
		private int editorCount;
	}
}

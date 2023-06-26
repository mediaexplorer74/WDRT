using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a base class for getting and setting option values for a designer.</summary>
	// Token: 0x020005D3 RID: 1491
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class DesignerOptionService : IDesignerOptionService
	{
		/// <summary>Gets the options collection for this service.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> populated with available designer options.</returns>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000EFCA1 File Offset: 0x000EDEA1
		public DesignerOptionService.DesignerOptionCollection Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptionService.DesignerOptionCollection(this, null, string.Empty, null);
				}
				return this._options;
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name and adds it to the given parent.</summary>
		/// <param name="parent">The parent designer option collection. All collections have a parent except the root object collection.</param>
		/// <param name="name">The name of this collection.</param>
		/// <param name="value">The object providing properties for this collection. Can be <see langword="null" /> if the collection should not provide any properties.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parent" /> or <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.</exception>
		// Token: 0x06003770 RID: 14192 RVA: 0x000EFCC4 File Offset: 0x000EDEC4
		protected DesignerOptionService.DesignerOptionCollection CreateOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object value)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					name.Length.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}), "name.Length");
			}
			return new DesignerOptionService.DesignerOptionCollection(this, parent, name, value);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000EFD40 File Offset: 0x000EDF40
		private PropertyDescriptor GetOptionProperty(string pageName, string valueName)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			string[] array = pageName.Split(new char[] { '\\' });
			DesignerOptionService.DesignerOptionCollection designerOptionCollection = this.Options;
			foreach (string text in array)
			{
				designerOptionCollection = designerOptionCollection[text];
				if (designerOptionCollection == null)
				{
					return null;
				}
			}
			return designerOptionCollection.Properties[valueName];
		}

		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x06003772 RID: 14194 RVA: 0x000EFDB1 File Offset: 0x000EDFB1
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		/// <summary>Shows the options dialog box for the given object.</summary>
		/// <param name="options">The options collection containing the object to be invoked.</param>
		/// <param name="optionObject">The actual options object.</param>
		/// <returns>
		///   <see langword="true" /> if the dialog box is shown; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003773 RID: 14195 RVA: 0x000EFDB3 File Offset: 0x000EDFB3
		protected virtual bool ShowDialog(DesignerOptionService.DesignerOptionCollection options, object optionObject)
		{
			return false;
		}

		/// <summary>Gets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound.</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <returns>The value of the option named <paramref name="valueName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is <see langword="null" />.</exception>
		// Token: 0x06003774 RID: 14196 RVA: 0x000EFDB8 File Offset: 0x000EDFB8
		object IDesignerOptionService.GetOptionValue(string pageName, string valueName)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				return optionProperty.GetValue(null);
			}
			return null;
		}

		/// <summary>Sets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <param name="value">The value of the option.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is <see langword="null" />.</exception>
		// Token: 0x06003775 RID: 14197 RVA: 0x000EFDDC File Offset: 0x000EDFDC
		void IDesignerOptionService.SetOptionValue(string pageName, string valueName, object value)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				optionProperty.SetValue(null, value);
			}
		}

		// Token: 0x04002ADE RID: 10974
		private DesignerOptionService.DesignerOptionCollection _options;

		/// <summary>Contains a collection of designer options. This class cannot be inherited.</summary>
		// Token: 0x020008AA RID: 2218
		[TypeConverter(typeof(DesignerOptionService.DesignerOptionConverter))]
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public sealed class DesignerOptionCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060045D9 RID: 17881 RVA: 0x00123870 File Offset: 0x00121A70
			internal DesignerOptionCollection(DesignerOptionService service, DesignerOptionService.DesignerOptionCollection parent, string name, object value)
			{
				this._service = service;
				this._parent = parent;
				this._name = name;
				this._value = value;
				if (this._parent != null)
				{
					if (this._parent._children == null)
					{
						this._parent._children = new ArrayList(1);
					}
					this._parent._children.Add(this);
				}
			}

			/// <summary>Gets the number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</summary>
			/// <returns>The number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</returns>
			// Token: 0x17000FC9 RID: 4041
			// (get) Token: 0x060045DA RID: 17882 RVA: 0x001238D8 File Offset: 0x00121AD8
			public int Count
			{
				get
				{
					this.EnsurePopulated();
					return this._children.Count;
				}
			}

			/// <summary>Gets the name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>The name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</returns>
			// Token: 0x17000FCA RID: 4042
			// (get) Token: 0x060045DB RID: 17883 RVA: 0x001238EB File Offset: 0x00121AEB
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Gets the parent collection object.</summary>
			/// <returns>The parent collection object, or <see langword="null" /> if there is no parent.</returns>
			// Token: 0x17000FCB RID: 4043
			// (get) Token: 0x060045DC RID: 17884 RVA: 0x001238F3 File Offset: 0x00121AF3
			public DesignerOptionService.DesignerOptionCollection Parent
			{
				get
				{
					return this._parent;
				}
			}

			/// <summary>Gets the collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</summary>
			/// <returns>The collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</returns>
			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x060045DD RID: 17885 RVA: 0x001238FC File Offset: 0x00121AFC
			public PropertyDescriptorCollection Properties
			{
				get
				{
					if (this._properties == null)
					{
						ArrayList arrayList;
						if (this._value != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._value);
							arrayList = new ArrayList(properties.Count);
							using (IEnumerator enumerator = properties.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
									arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(propertyDescriptor, this._value));
								}
								goto IL_7A;
							}
						}
						arrayList = new ArrayList(1);
						IL_7A:
						this.EnsurePopulated();
						foreach (object obj2 in this._children)
						{
							DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj2;
							arrayList.AddRange(designerOptionCollection.Properties);
						}
						PropertyDescriptor[] array = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
						this._properties = new PropertyDescriptorCollection(array, true);
					}
					return this._properties;
				}
			}

			/// <summary>Gets the child collection at the given index.</summary>
			/// <param name="index">The zero-based index of the child collection to get.</param>
			/// <returns>The child collection at the specified index.</returns>
			// Token: 0x17000FCD RID: 4045
			public DesignerOptionService.DesignerOptionCollection this[int index]
			{
				get
				{
					this.EnsurePopulated();
					if (index < 0 || index >= this._children.Count)
					{
						throw new IndexOutOfRangeException("index");
					}
					return (DesignerOptionService.DesignerOptionCollection)this._children[index];
				}
			}

			/// <summary>Gets the child collection at the given name.</summary>
			/// <param name="name">The name of the child collection.</param>
			/// <returns>The child collection with the name specified by the <paramref name="name" /> parameter, or <see langword="null" /> if the name is not found.</returns>
			// Token: 0x17000FCE RID: 4046
			public DesignerOptionService.DesignerOptionCollection this[string name]
			{
				get
				{
					this.EnsurePopulated();
					foreach (object obj in this._children)
					{
						DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
						if (string.Compare(designerOptionCollection.Name, name, true, CultureInfo.InvariantCulture) == 0)
						{
							return designerOptionCollection;
						}
					}
					return null;
				}
			}

			/// <summary>Copies the entire collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The <paramref name="array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x060045E0 RID: 17888 RVA: 0x00123AC4 File Offset: 0x00121CC4
			public void CopyTo(Array array, int index)
			{
				this.EnsurePopulated();
				this._children.CopyTo(array, index);
			}

			// Token: 0x060045E1 RID: 17889 RVA: 0x00123AD9 File Offset: 0x00121CD9
			private void EnsurePopulated()
			{
				if (this._children == null)
				{
					this._service.PopulateOptionCollection(this);
					if (this._children == null)
					{
						this._children = new ArrayList(1);
					}
				}
			}

			/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</returns>
			// Token: 0x060045E2 RID: 17890 RVA: 0x00123B03 File Offset: 0x00121D03
			public IEnumerator GetEnumerator()
			{
				this.EnsurePopulated();
				return this._children.GetEnumerator();
			}

			/// <summary>Returns the index of the first occurrence of a given value in a range of this collection.</summary>
			/// <param name="value">The object to locate in the collection.</param>
			/// <returns>The index of the first occurrence of value within the entire collection, if found; otherwise, the lower bound of the collection minus 1.</returns>
			// Token: 0x060045E3 RID: 17891 RVA: 0x00123B16 File Offset: 0x00121D16
			public int IndexOf(DesignerOptionService.DesignerOptionCollection value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			// Token: 0x060045E4 RID: 17892 RVA: 0x00123B2C File Offset: 0x00121D2C
			private static object RecurseFindValue(DesignerOptionService.DesignerOptionCollection options)
			{
				if (options._value != null)
				{
					return options._value;
				}
				foreach (object obj in options)
				{
					DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
					object obj2 = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(designerOptionCollection);
					if (obj2 != null)
					{
						return obj2;
					}
				}
				return null;
			}

			/// <summary>Displays a dialog box user interface (UI) with which the user can configure the options in this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the dialog box can be displayed; otherwise, <see langword="false" />.</returns>
			// Token: 0x060045E5 RID: 17893 RVA: 0x00123B9C File Offset: 0x00121D9C
			public bool ShowDialog()
			{
				object obj = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(this);
				return obj != null && this._service.ShowDialog(this, obj);
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized and, therefore, thread safe.</summary>
			/// <returns>
			///   <see langword="true" /> if the access to the collection is synchronized; otherwise, <see langword="false" />.</returns>
			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x060045E6 RID: 17894 RVA: 0x00123BC2 File Offset: 0x00121DC2
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x060045E7 RID: 17895 RVA: 0x00123BC5 File Offset: 0x00121DC5
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
			// Token: 0x17000FD1 RID: 4049
			// (get) Token: 0x060045E8 RID: 17896 RVA: 0x00123BC8 File Offset: 0x00121DC8
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x060045E9 RID: 17897 RVA: 0x00123BCB File Offset: 0x00121DCB
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The element at the specified index.</returns>
			// Token: 0x17000FD3 RID: 4051
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>The position into which the new element was inserted.</returns>
			// Token: 0x060045EC RID: 17900 RVA: 0x00123BDE File Offset: 0x00121DDE
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x060045ED RID: 17901 RVA: 0x00123BE5 File Offset: 0x00121DE5
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060045EE RID: 17902 RVA: 0x00123BEC File Offset: 0x00121DEC
			bool IList.Contains(object value)
			{
				this.EnsurePopulated();
				return this._children.Contains(value);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
			/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
			// Token: 0x060045EF RID: 17903 RVA: 0x00123C00 File Offset: 0x00121E00
			int IList.IndexOf(object value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
			// Token: 0x060045F0 RID: 17904 RVA: 0x00123C14 File Offset: 0x00121E14
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
			// Token: 0x060045F1 RID: 17905 RVA: 0x00123C1B File Offset: 0x00121E1B
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the collection item at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x060045F2 RID: 17906 RVA: 0x00123C22 File Offset: 0x00121E22
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040037E1 RID: 14305
			private DesignerOptionService _service;

			// Token: 0x040037E2 RID: 14306
			private DesignerOptionService.DesignerOptionCollection _parent;

			// Token: 0x040037E3 RID: 14307
			private string _name;

			// Token: 0x040037E4 RID: 14308
			private object _value;

			// Token: 0x040037E5 RID: 14309
			private ArrayList _children;

			// Token: 0x040037E6 RID: 14310
			private PropertyDescriptorCollection _properties;

			// Token: 0x02000934 RID: 2356
			private sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x060046AE RID: 18094 RVA: 0x001271A8 File Offset: 0x001253A8
				internal WrappedPropertyDescriptor(PropertyDescriptor property, object target)
					: base(property.Name, null)
				{
					this.property = property;
					this.target = target;
				}

				// Token: 0x17000FE7 RID: 4071
				// (get) Token: 0x060046AF RID: 18095 RVA: 0x001271C5 File Offset: 0x001253C5
				public override AttributeCollection Attributes
				{
					get
					{
						return this.property.Attributes;
					}
				}

				// Token: 0x17000FE8 RID: 4072
				// (get) Token: 0x060046B0 RID: 18096 RVA: 0x001271D2 File Offset: 0x001253D2
				public override Type ComponentType
				{
					get
					{
						return this.property.ComponentType;
					}
				}

				// Token: 0x17000FE9 RID: 4073
				// (get) Token: 0x060046B1 RID: 18097 RVA: 0x001271DF File Offset: 0x001253DF
				public override bool IsReadOnly
				{
					get
					{
						return this.property.IsReadOnly;
					}
				}

				// Token: 0x17000FEA RID: 4074
				// (get) Token: 0x060046B2 RID: 18098 RVA: 0x001271EC File Offset: 0x001253EC
				public override Type PropertyType
				{
					get
					{
						return this.property.PropertyType;
					}
				}

				// Token: 0x060046B3 RID: 18099 RVA: 0x001271F9 File Offset: 0x001253F9
				public override bool CanResetValue(object component)
				{
					return this.property.CanResetValue(this.target);
				}

				// Token: 0x060046B4 RID: 18100 RVA: 0x0012720C File Offset: 0x0012540C
				public override object GetValue(object component)
				{
					return this.property.GetValue(this.target);
				}

				// Token: 0x060046B5 RID: 18101 RVA: 0x0012721F File Offset: 0x0012541F
				public override void ResetValue(object component)
				{
					this.property.ResetValue(this.target);
				}

				// Token: 0x060046B6 RID: 18102 RVA: 0x00127232 File Offset: 0x00125432
				public override void SetValue(object component, object value)
				{
					this.property.SetValue(this.target, value);
				}

				// Token: 0x060046B7 RID: 18103 RVA: 0x00127246 File Offset: 0x00125446
				public override bool ShouldSerializeValue(object component)
				{
					return this.property.ShouldSerializeValue(this.target);
				}

				// Token: 0x04003DC9 RID: 15817
				private object target;

				// Token: 0x04003DCA RID: 15818
				private PropertyDescriptor property;
			}
		}

		// Token: 0x020008AB RID: 2219
		internal sealed class DesignerOptionConverter : TypeConverter
		{
			// Token: 0x060045F3 RID: 17907 RVA: 0x00123C29 File Offset: 0x00121E29
			public override bool GetPropertiesSupported(ITypeDescriptorContext cxt)
			{
				return true;
			}

			// Token: 0x060045F4 RID: 17908 RVA: 0x00123C2C File Offset: 0x00121E2C
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext cxt, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				DesignerOptionService.DesignerOptionCollection designerOptionCollection = value as DesignerOptionService.DesignerOptionCollection;
				if (designerOptionCollection == null)
				{
					return propertyDescriptorCollection;
				}
				foreach (object obj in designerOptionCollection)
				{
					DesignerOptionService.DesignerOptionCollection designerOptionCollection2 = (DesignerOptionService.DesignerOptionCollection)obj;
					propertyDescriptorCollection.Add(new DesignerOptionService.DesignerOptionConverter.OptionPropertyDescriptor(designerOptionCollection2));
				}
				foreach (object obj2 in designerOptionCollection.Properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x060045F5 RID: 17909 RVA: 0x00123CF0 File Offset: 0x00121EF0
			public override object ConvertTo(ITypeDescriptorContext cxt, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return SR.GetString("CollectionConverterText");
				}
				return base.ConvertTo(cxt, culture, value, destinationType);
			}

			// Token: 0x02000935 RID: 2357
			private class OptionPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x060046B8 RID: 18104 RVA: 0x00127259 File Offset: 0x00125459
				internal OptionPropertyDescriptor(DesignerOptionService.DesignerOptionCollection option)
					: base(option.Name, null)
				{
					this._option = option;
				}

				// Token: 0x17000FEB RID: 4075
				// (get) Token: 0x060046B9 RID: 18105 RVA: 0x0012726F File Offset: 0x0012546F
				public override Type ComponentType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x17000FEC RID: 4076
				// (get) Token: 0x060046BA RID: 18106 RVA: 0x0012727C File Offset: 0x0012547C
				public override bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000FED RID: 4077
				// (get) Token: 0x060046BB RID: 18107 RVA: 0x0012727F File Offset: 0x0012547F
				public override Type PropertyType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x060046BC RID: 18108 RVA: 0x0012728C File Offset: 0x0012548C
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x060046BD RID: 18109 RVA: 0x0012728F File Offset: 0x0012548F
				public override object GetValue(object component)
				{
					return this._option;
				}

				// Token: 0x060046BE RID: 18110 RVA: 0x00127297 File Offset: 0x00125497
				public override void ResetValue(object component)
				{
				}

				// Token: 0x060046BF RID: 18111 RVA: 0x00127299 File Offset: 0x00125499
				public override void SetValue(object component, object value)
				{
				}

				// Token: 0x060046C0 RID: 18112 RVA: 0x0012729B File Offset: 0x0012549B
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x04003DCB RID: 15819
				private DesignerOptionService.DesignerOptionCollection _option;
			}
		}
	}
}

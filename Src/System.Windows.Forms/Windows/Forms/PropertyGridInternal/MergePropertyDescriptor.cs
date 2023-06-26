using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050D RID: 1293
	internal class MergePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060054C2 RID: 21698 RVA: 0x00163230 File Offset: 0x00161430
		public MergePropertyDescriptor(PropertyDescriptor[] descriptors)
			: base(descriptors[0].Name, null)
		{
			this.descriptors = descriptors;
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x00163248 File Offset: 0x00161448
		public override Type ComponentType
		{
			get
			{
				return this.descriptors[0].ComponentType;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060054C4 RID: 21700 RVA: 0x00163257 File Offset: 0x00161457
		public override TypeConverter Converter
		{
			get
			{
				return this.descriptors[0].Converter;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x00163266 File Offset: 0x00161466
		public override string DisplayName
		{
			get
			{
				return this.descriptors[0].DisplayName;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060054C6 RID: 21702 RVA: 0x00163278 File Offset: 0x00161478
		public override bool IsLocalizable
		{
			get
			{
				if (this.localizable == MergePropertyDescriptor.TriState.Unknown)
				{
					this.localizable = MergePropertyDescriptor.TriState.Yes;
					foreach (PropertyDescriptor propertyDescriptor in this.descriptors)
					{
						if (!propertyDescriptor.IsLocalizable)
						{
							this.localizable = MergePropertyDescriptor.TriState.No;
							break;
						}
					}
				}
				return this.localizable == MergePropertyDescriptor.TriState.Yes;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x001632C8 File Offset: 0x001614C8
		public override bool IsReadOnly
		{
			get
			{
				if (this.readOnly == MergePropertyDescriptor.TriState.Unknown)
				{
					this.readOnly = MergePropertyDescriptor.TriState.No;
					foreach (PropertyDescriptor propertyDescriptor in this.descriptors)
					{
						if (propertyDescriptor.IsReadOnly)
						{
							this.readOnly = MergePropertyDescriptor.TriState.Yes;
							break;
						}
					}
				}
				return this.readOnly == MergePropertyDescriptor.TriState.Yes;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060054C8 RID: 21704 RVA: 0x00163317 File Offset: 0x00161517
		public override Type PropertyType
		{
			get
			{
				return this.descriptors[0].PropertyType;
			}
		}

		// Token: 0x1700145A RID: 5210
		public PropertyDescriptor this[int index]
		{
			get
			{
				return this.descriptors[index];
			}
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x00163330 File Offset: 0x00161530
		public override bool CanResetValue(object component)
		{
			if (this.canReset == MergePropertyDescriptor.TriState.Unknown)
			{
				this.canReset = MergePropertyDescriptor.TriState.Yes;
				Array array = (Array)component;
				for (int i = 0; i < this.descriptors.Length; i++)
				{
					if (!this.descriptors[i].CanResetValue(this.GetPropertyOwnerForComponent(array, i)))
					{
						this.canReset = MergePropertyDescriptor.TriState.No;
						break;
					}
				}
			}
			return this.canReset == MergePropertyDescriptor.TriState.Yes;
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x00163390 File Offset: 0x00161590
		private object CopyValue(object value)
		{
			if (value == null)
			{
				return value;
			}
			Type type = value.GetType();
			if (type.IsValueType)
			{
				return value;
			}
			object obj = null;
			ICloneable cloneable = value as ICloneable;
			if (cloneable != null)
			{
				obj = cloneable.Clone();
			}
			if (obj == null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(value);
				if (converter.CanConvertTo(typeof(InstanceDescriptor)))
				{
					InstanceDescriptor instanceDescriptor = (InstanceDescriptor)converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(InstanceDescriptor));
					if (instanceDescriptor != null && instanceDescriptor.IsComplete)
					{
						obj = instanceDescriptor.Invoke();
					}
				}
				if (obj == null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
				{
					object obj2 = converter.ConvertToInvariantString(value);
					obj = converter.ConvertFromInvariantString((string)obj2);
				}
			}
			if (obj == null && type.IsSerializable)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream();
				binaryFormatter.Serialize(memoryStream, value);
				memoryStream.Position = 0L;
				obj = binaryFormatter.Deserialize(memoryStream);
			}
			if (obj != null)
			{
				return obj;
			}
			return value;
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x00163492 File Offset: 0x00161692
		protected override AttributeCollection CreateAttributeCollection()
		{
			return new MergePropertyDescriptor.MergedAttributeCollection(this);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0016349C File Offset: 0x0016169C
		private object GetPropertyOwnerForComponent(Array a, int i)
		{
			object obj = a.GetValue(i);
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.descriptors[i]);
			}
			return obj;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x001634CE File Offset: 0x001616CE
		public override object GetEditor(Type editorBaseType)
		{
			return this.descriptors[0].GetEditor(editorBaseType);
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x001634E0 File Offset: 0x001616E0
		public override object GetValue(object component)
		{
			bool flag;
			return this.GetValue((Array)component, out flag);
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x001634FC File Offset: 0x001616FC
		public object GetValue(Array components, out bool allEqual)
		{
			allEqual = true;
			object value = this.descriptors[0].GetValue(this.GetPropertyOwnerForComponent(components, 0));
			if (value is ICollection)
			{
				if (this.collection == null)
				{
					this.collection = new MergePropertyDescriptor.MultiMergeCollection((ICollection)value);
				}
				else
				{
					if (this.collection.Locked)
					{
						return this.collection;
					}
					this.collection.SetItems((ICollection)value);
				}
			}
			for (int i = 1; i < this.descriptors.Length; i++)
			{
				object value2 = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(components, i));
				if (this.collection != null)
				{
					if (!this.collection.MergeCollection((ICollection)value2))
					{
						allEqual = false;
						return null;
					}
				}
				else if ((value != null || value2 != null) && (value == null || !value.Equals(value2)))
				{
					allEqual = false;
					return null;
				}
			}
			if (allEqual && this.collection != null && this.collection.Count == 0)
			{
				return null;
			}
			if (this.collection == null)
			{
				return value;
			}
			return this.collection;
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x001635F8 File Offset: 0x001617F8
		internal object[] GetValues(Array components)
		{
			object[] array = new object[components.Length];
			for (int i = 0; i < components.Length; i++)
			{
				array[i] = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(components, i));
			}
			return array;
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0016363C File Offset: 0x0016183C
		public override void ResetValue(object component)
		{
			Array array = (Array)component;
			for (int i = 0; i < this.descriptors.Length; i++)
			{
				this.descriptors[i].ResetValue(this.GetPropertyOwnerForComponent(array, i));
			}
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x00163678 File Offset: 0x00161878
		private void SetCollectionValues(Array a, IList listValue)
		{
			try
			{
				if (this.collection != null)
				{
					this.collection.Locked = true;
				}
				object[] array = new object[listValue.Count];
				listValue.CopyTo(array, 0);
				for (int i = 0; i < this.descriptors.Length; i++)
				{
					IList list = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(a, i)) as IList;
					if (list != null)
					{
						list.Clear();
						foreach (object obj in array)
						{
							list.Add(obj);
						}
					}
				}
			}
			finally
			{
				if (this.collection != null)
				{
					this.collection.Locked = false;
				}
			}
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x00163730 File Offset: 0x00161930
		public override void SetValue(object component, object value)
		{
			Array array = (Array)component;
			if (value is IList && typeof(IList).IsAssignableFrom(this.PropertyType))
			{
				this.SetCollectionValues(array, (IList)value);
				return;
			}
			for (int i = 0; i < this.descriptors.Length; i++)
			{
				object obj = this.CopyValue(value);
				this.descriptors[i].SetValue(this.GetPropertyOwnerForComponent(array, i), obj);
			}
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x001637A4 File Offset: 0x001619A4
		public override bool ShouldSerializeValue(object component)
		{
			Array array = (Array)component;
			for (int i = 0; i < this.descriptors.Length; i++)
			{
				if (!this.descriptors[i].ShouldSerializeValue(this.GetPropertyOwnerForComponent(array, i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400371B RID: 14107
		private PropertyDescriptor[] descriptors;

		// Token: 0x0400371C RID: 14108
		private MergePropertyDescriptor.TriState localizable;

		// Token: 0x0400371D RID: 14109
		private MergePropertyDescriptor.TriState readOnly;

		// Token: 0x0400371E RID: 14110
		private MergePropertyDescriptor.TriState canReset;

		// Token: 0x0400371F RID: 14111
		private MergePropertyDescriptor.MultiMergeCollection collection;

		// Token: 0x0200088E RID: 2190
		private enum TriState
		{
			// Token: 0x040044BE RID: 17598
			Unknown,
			// Token: 0x040044BF RID: 17599
			Yes,
			// Token: 0x040044C0 RID: 17600
			No
		}

		// Token: 0x0200088F RID: 2191
		private class MultiMergeCollection : ICollection, IEnumerable
		{
			// Token: 0x060071F9 RID: 29177 RVA: 0x001A13A3 File Offset: 0x0019F5A3
			public MultiMergeCollection(ICollection original)
			{
				this.SetItems(original);
			}

			// Token: 0x1700190C RID: 6412
			// (get) Token: 0x060071FA RID: 29178 RVA: 0x001A13B2 File Offset: 0x0019F5B2
			public int Count
			{
				get
				{
					if (this.items != null)
					{
						return this.items.Length;
					}
					return 0;
				}
			}

			// Token: 0x1700190D RID: 6413
			// (get) Token: 0x060071FB RID: 29179 RVA: 0x001A13C6 File Offset: 0x0019F5C6
			// (set) Token: 0x060071FC RID: 29180 RVA: 0x001A13CE File Offset: 0x0019F5CE
			public bool Locked
			{
				get
				{
					return this.locked;
				}
				set
				{
					this.locked = value;
				}
			}

			// Token: 0x1700190E RID: 6414
			// (get) Token: 0x060071FD RID: 29181 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700190F RID: 6415
			// (get) Token: 0x060071FE RID: 29182 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060071FF RID: 29183 RVA: 0x001A13D7 File Offset: 0x0019F5D7
			public void CopyTo(Array array, int index)
			{
				if (this.items == null)
				{
					return;
				}
				Array.Copy(this.items, 0, array, index, this.items.Length);
			}

			// Token: 0x06007200 RID: 29184 RVA: 0x001A13F8 File Offset: 0x0019F5F8
			public IEnumerator GetEnumerator()
			{
				if (this.items != null)
				{
					return this.items.GetEnumerator();
				}
				return new object[0].GetEnumerator();
			}

			// Token: 0x06007201 RID: 29185 RVA: 0x001A141C File Offset: 0x0019F61C
			public bool MergeCollection(ICollection newCollection)
			{
				if (this.locked)
				{
					return true;
				}
				if (this.items.Length != newCollection.Count)
				{
					this.items = new object[0];
					return false;
				}
				object[] array = new object[newCollection.Count];
				newCollection.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == null != (this.items[i] == null) || (this.items[i] != null && !this.items[i].Equals(array[i])))
					{
						this.items = new object[0];
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007202 RID: 29186 RVA: 0x001A14B1 File Offset: 0x0019F6B1
			public void SetItems(ICollection collection)
			{
				if (this.locked)
				{
					return;
				}
				this.items = new object[collection.Count];
				collection.CopyTo(this.items, 0);
			}

			// Token: 0x040044C1 RID: 17601
			private object[] items;

			// Token: 0x040044C2 RID: 17602
			private bool locked;
		}

		// Token: 0x02000890 RID: 2192
		private class MergedAttributeCollection : AttributeCollection
		{
			// Token: 0x06007203 RID: 29187 RVA: 0x001A14DA File Offset: 0x0019F6DA
			public MergedAttributeCollection(MergePropertyDescriptor owner)
				: base(null)
			{
				this.owner = owner;
			}

			// Token: 0x17001910 RID: 6416
			public override Attribute this[Type attributeType]
			{
				get
				{
					return this.GetCommonAttribute(attributeType);
				}
			}

			// Token: 0x06007205 RID: 29189 RVA: 0x001A14F4 File Offset: 0x0019F6F4
			private Attribute GetCommonAttribute(Type attributeType)
			{
				if (this.attributeCollections == null)
				{
					this.attributeCollections = new AttributeCollection[this.owner.descriptors.Length];
					for (int i = 0; i < this.owner.descriptors.Length; i++)
					{
						this.attributeCollections[i] = this.owner.descriptors[i].Attributes;
					}
				}
				if (this.attributeCollections.Length == 0)
				{
					return base.GetDefaultAttribute(attributeType);
				}
				Attribute attribute;
				if (this.foundAttributes != null)
				{
					attribute = this.foundAttributes[attributeType] as Attribute;
					if (attribute != null)
					{
						return attribute;
					}
				}
				attribute = this.attributeCollections[0][attributeType];
				if (attribute == null)
				{
					return null;
				}
				for (int j = 1; j < this.attributeCollections.Length; j++)
				{
					Attribute attribute2 = this.attributeCollections[j][attributeType];
					if (!attribute.Equals(attribute2))
					{
						attribute = base.GetDefaultAttribute(attributeType);
						break;
					}
				}
				if (this.foundAttributes == null)
				{
					this.foundAttributes = new Hashtable();
				}
				this.foundAttributes[attributeType] = attribute;
				return attribute;
			}

			// Token: 0x040044C3 RID: 17603
			private MergePropertyDescriptor owner;

			// Token: 0x040044C4 RID: 17604
			private AttributeCollection[] attributeCollections;

			// Token: 0x040044C5 RID: 17605
			private IDictionary foundAttributes;
		}
	}
}

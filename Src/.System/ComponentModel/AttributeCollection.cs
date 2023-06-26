using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of attributes.</summary>
	// Token: 0x02000514 RID: 1300
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class AttributeCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributes" /> is <see langword="null" />.</exception>
		// Token: 0x0600312B RID: 12587 RVA: 0x000DE808 File Offset: 0x000DCA08
		public AttributeCollection(params Attribute[] attributes)
		{
			if (attributes == null)
			{
				attributes = new Attribute[0];
			}
			this._attributes = attributes;
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i] == null)
				{
					throw new ArgumentNullException("attributes");
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		// Token: 0x0600312C RID: 12588 RVA: 0x000DE84B File Offset: 0x000DCA4B
		protected AttributeCollection()
		{
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.AttributeCollection" /> from an existing <see cref="T:System.ComponentModel.AttributeCollection" />.</summary>
		/// <param name="existing">An <see cref="T:System.ComponentModel.AttributeCollection" /> from which to create the copy.</param>
		/// <param name="newAttributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. Can be <see langword="null" />.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.AttributeCollection" /> that is a copy of <paramref name="existing" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="existing" /> is <see langword="null" />.</exception>
		// Token: 0x0600312D RID: 12589 RVA: 0x000DE854 File Offset: 0x000DCA54
		public static AttributeCollection FromExisting(AttributeCollection existing, params Attribute[] newAttributes)
		{
			if (existing == null)
			{
				throw new ArgumentNullException("existing");
			}
			if (newAttributes == null)
			{
				newAttributes = new Attribute[0];
			}
			Attribute[] array = new Attribute[existing.Count + newAttributes.Length];
			int count = existing.Count;
			existing.CopyTo(array, 0);
			for (int i = 0; i < newAttributes.Length; i++)
			{
				if (newAttributes[i] == null)
				{
					throw new ArgumentNullException("newAttributes");
				}
				bool flag = false;
				for (int j = 0; j < existing.Count; j++)
				{
					if (array[j].TypeId.Equals(newAttributes[i].TypeId))
					{
						flag = true;
						array[j] = newAttributes[i];
						break;
					}
				}
				if (!flag)
				{
					array[count++] = newAttributes[i];
				}
			}
			Attribute[] array2;
			if (count < array.Length)
			{
				array2 = new Attribute[count];
				Array.Copy(array, 0, array2, 0, count);
			}
			else
			{
				array2 = array;
			}
			return new AttributeCollection(array2);
		}

		/// <summary>Gets the attribute collection.</summary>
		/// <returns>The attribute collection.</returns>
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000DE924 File Offset: 0x000DCB24
		protected virtual Attribute[] Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		/// <summary>Gets the number of attributes.</summary>
		/// <returns>The number of attributes.</returns>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000DE92C File Offset: 0x000DCB2C
		public int Count
		{
			get
			{
				return this.Attributes.Length;
			}
		}

		/// <summary>Gets the attribute with the specified index number.</summary>
		/// <param name="index">The zero-based index of <see cref="T:System.ComponentModel.AttributeCollection" />.</param>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified index number.</returns>
		// Token: 0x17000C05 RID: 3077
		public virtual Attribute this[int index]
		{
			get
			{
				return this.Attributes[index];
			}
		}

		/// <summary>Gets the attribute with the specified type.</summary>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the <see cref="T:System.Attribute" /> to get from the collection.</param>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified type or, if the attribute does not exist, the default value for the attribute type.</returns>
		// Token: 0x17000C06 RID: 3078
		public virtual Attribute this[Type attributeType]
		{
			get
			{
				object obj = AttributeCollection.internalSyncObject;
				Attribute defaultAttribute;
				lock (obj)
				{
					if (this._foundAttributeTypes == null)
					{
						this._foundAttributeTypes = new AttributeCollection.AttributeEntry[5];
					}
					int i = 0;
					while (i < 5)
					{
						if (this._foundAttributeTypes[i].type == attributeType)
						{
							int index = this._foundAttributeTypes[i].index;
							if (index != -1)
							{
								return this.Attributes[index];
							}
							return this.GetDefaultAttribute(attributeType);
						}
						else
						{
							if (this._foundAttributeTypes[i].type == null)
							{
								break;
							}
							i++;
						}
					}
					int index2 = this._index;
					this._index = index2 + 1;
					i = index2;
					if (this._index >= 5)
					{
						this._index = 0;
					}
					this._foundAttributeTypes[i].type = attributeType;
					int num = this.Attributes.Length;
					for (int j = 0; j < num; j++)
					{
						Attribute attribute = this.Attributes[j];
						Type type = attribute.GetType();
						if (type == attributeType)
						{
							this._foundAttributeTypes[i].index = j;
							return attribute;
						}
					}
					for (int k = 0; k < num; k++)
					{
						Attribute attribute2 = this.Attributes[k];
						Type type2 = attribute2.GetType();
						if (attributeType.IsAssignableFrom(type2))
						{
							this._foundAttributeTypes[i].index = k;
							return attribute2;
						}
					}
					this._foundAttributeTypes[i].index = -1;
					defaultAttribute = this.GetDefaultAttribute(attributeType);
				}
				return defaultAttribute;
			}
		}

		/// <summary>Determines whether this collection of attributes has the specified attribute.</summary>
		/// <param name="attribute">An <see cref="T:System.Attribute" /> to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the attribute or is the default attribute for the type of attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003132 RID: 12594 RVA: 0x000DEAF8 File Offset: 0x000DCCF8
		public bool Contains(Attribute attribute)
		{
			Attribute attribute2 = this[attribute.GetType()];
			return attribute2 != null && attribute2.Equals(attribute);
		}

		/// <summary>Determines whether this attribute collection contains all the specified attributes in the attribute array.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains all the attributes; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003133 RID: 12595 RVA: 0x000DEB24 File Offset: 0x000DCD24
		public bool Contains(Attribute[] attributes)
		{
			if (attributes == null)
			{
				return true;
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Contains(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the default <see cref="T:System.Attribute" /> of a given <see cref="T:System.Type" />.</summary>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the attribute to retrieve.</param>
		/// <returns>The default <see cref="T:System.Attribute" /> of a given <paramref name="attributeType" />.</returns>
		// Token: 0x06003134 RID: 12596 RVA: 0x000DEB54 File Offset: 0x000DCD54
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			object obj = AttributeCollection.internalSyncObject;
			Attribute attribute;
			lock (obj)
			{
				if (AttributeCollection._defaultAttributes == null)
				{
					AttributeCollection._defaultAttributes = new Hashtable();
				}
				if (AttributeCollection._defaultAttributes.ContainsKey(attributeType))
				{
					attribute = (Attribute)AttributeCollection._defaultAttributes[attributeType];
				}
				else
				{
					Attribute attribute2 = null;
					Type reflectionType = TypeDescriptor.GetReflectionType(attributeType);
					FieldInfo field = reflectionType.GetField("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					if (field != null && field.IsStatic)
					{
						attribute2 = (Attribute)field.GetValue(null);
					}
					else
					{
						ConstructorInfo constructor = reflectionType.UnderlyingSystemType.GetConstructor(new Type[0]);
						if (constructor != null)
						{
							attribute2 = (Attribute)constructor.Invoke(new object[0]);
							if (!attribute2.IsDefaultAttribute())
							{
								attribute2 = null;
							}
						}
					}
					AttributeCollection._defaultAttributes[attributeType] = attribute2;
					attribute = attribute2;
				}
			}
			return attribute;
		}

		/// <summary>Gets an enumerator for this collection.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06003135 RID: 12597 RVA: 0x000DEC4C File Offset: 0x000DCE4C
		public IEnumerator GetEnumerator()
		{
			return this.Attributes.GetEnumerator();
		}

		/// <summary>Determines whether a specified attribute is the same as an attribute in the collection.</summary>
		/// <param name="attribute">An instance of <see cref="T:System.Attribute" /> to compare with the attributes in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the attribute is contained within the collection and has the same value as the attribute in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003136 RID: 12598 RVA: 0x000DEC5C File Offset: 0x000DCE5C
		public bool Matches(Attribute attribute)
		{
			for (int i = 0; i < this.Attributes.Length; i++)
			{
				if (this.Attributes[i].Match(attribute))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the attributes in the specified array are the same as the attributes in the collection.</summary>
		/// <param name="attributes">An array of <see cref="T:System.CodeDom.MemberAttributes" /> to compare with the attributes in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if all the attributes in the array are contained in the collection and have the same values as the attributes in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003137 RID: 12599 RVA: 0x000DEC90 File Offset: 0x000DCE90
		public bool Matches(Attribute[] attributes)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Matches(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x000DECB9 File Offset: 0x000DCEB9
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is synchronized (thread-safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000DECC1 File Offset: 0x000DCEC1
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000DECC4 File Offset: 0x000DCEC4
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the collection to.</param>
		/// <param name="index">The index to start from.</param>
		// Token: 0x0600313B RID: 12603 RVA: 0x000DECC7 File Offset: 0x000DCEC7
		public void CopyTo(Array array, int index)
		{
			Array.Copy(this.Attributes, 0, array, index, this.Attributes.Length);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x0600313C RID: 12604 RVA: 0x000DECDF File Offset: 0x000DCEDF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Specifies an empty collection that you can use, rather than creating a new one. This field is read-only.</summary>
		// Token: 0x040028F8 RID: 10488
		public static readonly AttributeCollection Empty = new AttributeCollection(null);

		// Token: 0x040028F9 RID: 10489
		private static Hashtable _defaultAttributes;

		// Token: 0x040028FA RID: 10490
		private Attribute[] _attributes;

		// Token: 0x040028FB RID: 10491
		private static object internalSyncObject = new object();

		// Token: 0x040028FC RID: 10492
		private const int FOUND_TYPES_LIMIT = 5;

		// Token: 0x040028FD RID: 10493
		private AttributeCollection.AttributeEntry[] _foundAttributeTypes;

		// Token: 0x040028FE RID: 10494
		private int _index;

		// Token: 0x0200088C RID: 2188
		private struct AttributeEntry
		{
			// Token: 0x040037A2 RID: 14242
			public Type type;

			// Token: 0x040037A3 RID: 14243
			public int index;
		}
	}
}

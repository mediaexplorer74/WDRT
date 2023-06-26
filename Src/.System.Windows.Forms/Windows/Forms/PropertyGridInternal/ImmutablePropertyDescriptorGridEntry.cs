using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050B RID: 1291
	internal class ImmutablePropertyDescriptorGridEntry : PropertyDescriptorGridEntry
	{
		// Token: 0x060054B7 RID: 21687 RVA: 0x001630AA File Offset: 0x001612AA
		internal ImmutablePropertyDescriptorGridEntry(PropertyGrid ownerGrid, GridEntry peParent, PropertyDescriptor propInfo, bool hide)
			: base(ownerGrid, peParent, propInfo, hide)
		{
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060054B8 RID: 21688 RVA: 0x001630B7 File Offset: 0x001612B7
		internal override bool IsPropertyReadOnly
		{
			get
			{
				return this.ShouldRenderReadOnly;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x001630BF File Offset: 0x001612BF
		// (set) Token: 0x060054BA RID: 21690 RVA: 0x001630C8 File Offset: 0x001612C8
		public override object PropertyValue
		{
			get
			{
				return base.PropertyValue;
			}
			set
			{
				object valueOwner = this.GetValueOwner();
				GridEntry instanceParentGridEntry = this.InstanceParentGridEntry;
				TypeConverter typeConverter = instanceParentGridEntry.TypeConverter;
				PropertyDescriptorCollection properties = typeConverter.GetProperties(instanceParentGridEntry, valueOwner);
				IDictionary dictionary = new Hashtable(properties.Count);
				object obj = null;
				for (int i = 0; i < properties.Count; i++)
				{
					if (this.propertyInfo.Name != null && this.propertyInfo.Name.Equals(properties[i].Name))
					{
						dictionary[properties[i].Name] = value;
					}
					else
					{
						dictionary[properties[i].Name] = properties[i].GetValue(valueOwner);
					}
				}
				try
				{
					obj = typeConverter.CreateInstance(instanceParentGridEntry, dictionary);
				}
				catch (Exception ex)
				{
					if (string.IsNullOrEmpty(ex.Message))
					{
						throw new TargetInvocationException(SR.GetString("ExceptionCreatingObject", new object[]
						{
							this.InstanceParentGridEntry.PropertyType.FullName,
							ex.ToString()
						}), ex);
					}
					throw;
				}
				if (obj != null)
				{
					instanceParentGridEntry.PropertyValue = obj;
				}
			}
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x001631F0 File Offset: 0x001613F0
		internal override bool NotifyValueGivenParent(object obj, int type)
		{
			return this.ParentGridEntry.NotifyValue(type);
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x001631FE File Offset: 0x001613FE
		public override bool ShouldRenderReadOnly
		{
			get
			{
				return this.InstanceParentGridEntry.ShouldRenderReadOnly;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x0016320C File Offset: 0x0016140C
		private GridEntry InstanceParentGridEntry
		{
			get
			{
				GridEntry gridEntry = this.ParentGridEntry;
				if (gridEntry is CategoryGridEntry)
				{
					gridEntry = gridEntry.ParentGridEntry;
				}
				return gridEntry;
			}
		}
	}
}

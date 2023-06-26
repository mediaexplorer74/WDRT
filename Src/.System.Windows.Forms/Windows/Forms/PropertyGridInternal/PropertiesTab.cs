using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	/// <summary>Represents the Properties tab on a <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	// Token: 0x02000510 RID: 1296
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PropertiesTab : PropertyTab
	{
		/// <summary>Gets the name of the Properties tab.</summary>
		/// <returns>The string "Properties".</returns>
		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x00163FFC File Offset: 0x001621FC
		public override string TabName
		{
			get
			{
				return SR.GetString("PBRSToolTipProperties");
			}
		}

		/// <summary>Gets the Help keyword that is to be associated with this tab.</summary>
		/// <returns>The string "vs.properties".</returns>
		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x00164008 File Offset: 0x00162208
		public override string HelpKeyword
		{
			get
			{
				return "vs.properties";
			}
		}

		/// <summary>Gets the default property of the specified component.</summary>
		/// <param name="obj">The component to retrieve the default property of.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property.</returns>
		// Token: 0x060054EC RID: 21740 RVA: 0x00164010 File Offset: 0x00162210
		public override PropertyDescriptor GetDefaultProperty(object obj)
		{
			PropertyDescriptor propertyDescriptor = base.GetDefaultProperty(obj);
			if (propertyDescriptor == null)
			{
				PropertyDescriptorCollection properties = this.GetProperties(obj);
				if (properties != null)
				{
					for (int i = 0; i < properties.Count; i++)
					{
						if ("Name".Equals(properties[i].Name))
						{
							propertyDescriptor = properties[i];
							break;
						}
					}
				}
			}
			return propertyDescriptor;
		}

		/// <summary>Gets the properties of the specified component that match the specified attributes.</summary>
		/// <param name="component">The component to retrieve properties from.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties</returns>
		// Token: 0x060054ED RID: 21741 RVA: 0x00142435 File Offset: 0x00140635
		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return this.GetProperties(null, component, attributes);
		}

		/// <summary>Gets the properties of the specified component that match the specified attributes and context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context to retrieve properties from.</param>
		/// <param name="component">The component to retrieve properties from.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties matching the specified context and attributes.</returns>
		// Token: 0x060054EE RID: 21742 RVA: 0x00164068 File Offset: 0x00162268
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			if (attributes == null)
			{
				attributes = new Attribute[] { BrowsableAttribute.Yes };
			}
			if (context == null)
			{
				return TypeDescriptor.GetProperties(component, attributes);
			}
			TypeConverter typeConverter = ((context.PropertyDescriptor == null) ? TypeDescriptor.GetConverter(component) : context.PropertyDescriptor.Converter);
			if (typeConverter == null || !typeConverter.GetPropertiesSupported(context))
			{
				return TypeDescriptor.GetProperties(component, attributes);
			}
			return typeConverter.GetProperties(context, component, attributes);
		}
	}
}

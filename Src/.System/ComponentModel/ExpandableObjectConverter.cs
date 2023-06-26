using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert expandable objects to and from various other representations.</summary>
	// Token: 0x02000551 RID: 1361
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ExpandableObjectConverter : TypeConverter
	{
		/// <summary>Gets a collection of properties for the type of object specified by the value parameter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of object to get the properties for.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x0600332E RID: 13102 RVA: 0x000E33C6 File Offset: 0x000E15C6
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns <see langword="false" />.</returns>
		// Token: 0x0600332F RID: 13103 RVA: 0x000E33CF File Offset: 0x000E15CF
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}

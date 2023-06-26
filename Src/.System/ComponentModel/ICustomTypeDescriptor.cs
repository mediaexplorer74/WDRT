using System;

namespace System.ComponentModel
{
	/// <summary>Provides an interface that supplies dynamic custom type information for an object.</summary>
	// Token: 0x0200055E RID: 1374
	public interface ICustomTypeDescriptor
	{
		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
		// Token: 0x06003380 RID: 13184
		AttributeCollection GetAttributes();

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of the object, or <see langword="null" /> if the class does not have a name.</returns>
		// Token: 0x06003381 RID: 13185
		string GetClassName();

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of the object, or <see langword="null" /> if the object does not have a name.</returns>
		// Token: 0x06003382 RID: 13186
		string GetComponentName();

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or <see langword="null" /> if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
		// Token: 0x06003383 RID: 13187
		TypeConverter GetConverter();

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or <see langword="null" /> if this object does not have events.</returns>
		// Token: 0x06003384 RID: 13188
		EventDescriptor GetDefaultEvent();

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or <see langword="null" /> if this object does not have properties.</returns>
		// Token: 0x06003385 RID: 13189
		PropertyDescriptor GetDefaultProperty();

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or <see langword="null" /> if the editor cannot be found.</returns>
		// Token: 0x06003386 RID: 13190
		object GetEditor(Type editorBaseType);

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
		// Token: 0x06003387 RID: 13191
		EventDescriptorCollection GetEvents();

		/// <summary>Returns the events for this instance of a component using the specified attribute array as a filter.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
		// Token: 0x06003388 RID: 13192
		EventDescriptorCollection GetEvents(Attribute[] attributes);

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
		// Token: 0x06003389 RID: 13193
		PropertyDescriptorCollection GetProperties();

		/// <summary>Returns the properties for this instance of a component using the attribute array as a filter.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
		// Token: 0x0600338A RID: 13194
		PropertyDescriptorCollection GetProperties(Attribute[] attributes);

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		// Token: 0x0600338B RID: 13195
		object GetPropertyOwner(PropertyDescriptor pd);
	}
}

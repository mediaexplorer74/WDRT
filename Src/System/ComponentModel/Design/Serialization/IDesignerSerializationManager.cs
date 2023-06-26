using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can manage design-time serialization.</summary>
	// Token: 0x0200060A RID: 1546
	public interface IDesignerSerializationManager : IServiceProvider
	{
		/// <summary>Gets a stack-based, user-defined storage area that is useful for communication between serializers.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.ContextStack" /> that stores data.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060038A6 RID: 14502
		ContextStack Context { get; }

		/// <summary>Indicates custom properties that can be serializable with available serializers.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties to be serialized.</returns>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060038A7 RID: 14503
		PropertyDescriptorCollection Properties { get; }

		/// <summary>Occurs when <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetName(System.Object)" /> cannot locate the specified name in the serialization manager's name table.</summary>
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060038A8 RID: 14504
		// (remove) Token: 0x060038A9 RID: 14505
		event ResolveNameEventHandler ResolveName;

		/// <summary>Occurs when serialization is complete.</summary>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060038AA RID: 14506
		// (remove) Token: 0x060038AB RID: 14507
		event EventHandler SerializationComplete;

		/// <summary>Adds the specified serialization provider to the serialization manager.</summary>
		/// <param name="provider">The serialization provider to add.</param>
		// Token: 0x060038AC RID: 14508
		void AddSerializationProvider(IDesignerSerializationProvider provider);

		/// <summary>Creates an instance of the specified type and adds it to a collection of named instances.</summary>
		/// <param name="type">The data type to create.</param>
		/// <param name="arguments">The arguments to pass to the constructor for this type.</param>
		/// <param name="name">The name of the object. This name can be used to access the object later through <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetInstance(System.String)" />. If <see langword="null" /> is passed, the object is still created but cannot be accessed by name.</param>
		/// <param name="addToContainer">If <see langword="true" />, this object is added to the design container. The object must implement <see cref="T:System.ComponentModel.IComponent" /> for this to have any effect.</param>
		/// <returns>The newly created object instance.</returns>
		// Token: 0x060038AD RID: 14509
		object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer);

		/// <summary>Gets an instance of a created object of the specified name, or <see langword="null" /> if that object does not exist.</summary>
		/// <param name="name">The name of the object to retrieve.</param>
		/// <returns>An instance of the object with the given name, or <see langword="null" /> if no object by that name can be found.</returns>
		// Token: 0x060038AE RID: 14510
		object GetInstance(string name);

		/// <summary>Gets the name of the specified object, or <see langword="null" /> if the object has no name.</summary>
		/// <param name="value">The object to retrieve the name for.</param>
		/// <returns>The name of the object, or <see langword="null" /> if the object is unnamed.</returns>
		// Token: 0x060038AF RID: 14511
		string GetName(object value);

		/// <summary>Gets a serializer of the requested type for the specified object type.</summary>
		/// <param name="objectType">The type of the object to get the serializer for.</param>
		/// <param name="serializerType">The type of the serializer to retrieve.</param>
		/// <returns>An instance of the requested serializer, or <see langword="null" /> if no appropriate serializer can be located.</returns>
		// Token: 0x060038B0 RID: 14512
		object GetSerializer(Type objectType, Type serializerType);

		/// <summary>Gets a type of the specified name.</summary>
		/// <param name="typeName">The fully qualified name of the type to load.</param>
		/// <returns>An instance of the type, or <see langword="null" /> if the type cannot be loaded.</returns>
		// Token: 0x060038B1 RID: 14513
		Type GetType(string typeName);

		/// <summary>Removes a custom serialization provider from the serialization manager.</summary>
		/// <param name="provider">The provider to remove. This object must have been added using <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.AddSerializationProvider(System.ComponentModel.Design.Serialization.IDesignerSerializationProvider)" />.</param>
		// Token: 0x060038B2 RID: 14514
		void RemoveSerializationProvider(IDesignerSerializationProvider provider);

		/// <summary>Reports an error in serialization.</summary>
		/// <param name="errorInformation">The error to report. This information object can be of any object type. If it is an exception, the message of the exception is extracted and reported to the user. If it is any other type, <see cref="M:System.Object.ToString" /> is called to display the information to the user.</param>
		// Token: 0x060038B3 RID: 14515
		void ReportError(object errorInformation);

		/// <summary>Sets the name of the specified existing object.</summary>
		/// <param name="instance">The object instance to name.</param>
		/// <param name="name">The name to give the instance.</param>
		// Token: 0x060038B4 RID: 14516
		void SetName(object instance, string name);
	}
}

using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for obtaining references to objects within a project by name or type, obtaining the name of a specified object, and for locating the parent of a specified object within a designer project.</summary>
	// Token: 0x020005F2 RID: 1522
	public interface IReferenceService
	{
		/// <summary>Gets the component that contains the specified component.</summary>
		/// <param name="reference">The object to retrieve the parent component for.</param>
		/// <returns>The base <see cref="T:System.ComponentModel.IComponent" /> that contains the specified object, or <see langword="null" /> if no parent component exists.</returns>
		// Token: 0x0600382C RID: 14380
		IComponent GetComponent(object reference);

		/// <summary>Gets a reference to the component whose name matches the specified name.</summary>
		/// <param name="name">The name of the component to return a reference to.</param>
		/// <returns>An object the specified name refers to, or <see langword="null" /> if no reference is found.</returns>
		// Token: 0x0600382D RID: 14381
		object GetReference(string name);

		/// <summary>Gets the name of the specified component.</summary>
		/// <param name="reference">The object to return the name of.</param>
		/// <returns>The name of the object referenced, or <see langword="null" /> if the object reference is not valid.</returns>
		// Token: 0x0600382E RID: 14382
		string GetName(object reference);

		/// <summary>Gets all available references to project components.</summary>
		/// <returns>An array of all objects with references available to the <see cref="T:System.ComponentModel.Design.IReferenceService" />.</returns>
		// Token: 0x0600382F RID: 14383
		object[] GetReferences();

		/// <summary>Gets all available references to components of the specified type.</summary>
		/// <param name="baseType">The type of object to return references to instances of.</param>
		/// <returns>An array of all available objects of the specified type.</returns>
		// Token: 0x06003830 RID: 14384
		object[] GetReferences(Type baseType);
	}
}

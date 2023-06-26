using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020005B3 RID: 1459
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class TypeDescriptionProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class.</summary>
		// Token: 0x0600365A RID: 13914 RVA: 0x000EC5B3 File Offset: 0x000EA7B3
		protected TypeDescriptionProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class using a parent type description provider.</summary>
		/// <param name="parent">The parent type description provider.</param>
		// Token: 0x0600365B RID: 13915 RVA: 0x000EC5BB File Offset: 0x000EA7BB
		protected TypeDescriptionProvider(TypeDescriptionProvider parent)
		{
			this._parent = parent;
		}

		/// <summary>Creates an object that can substitute for another data type.</summary>
		/// <param name="provider">An optional service provider.</param>
		/// <param name="objectType">The type of object to create. This parameter is never <see langword="null" />.</param>
		/// <param name="argTypes">An optional array of types that represent the parameter types to be passed to the object's constructor. This array can be <see langword="null" /> or of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor.</param>
		/// <returns>The substitute <see cref="T:System.Object" />.</returns>
		// Token: 0x0600365C RID: 13916 RVA: 0x000EC5CA File Offset: 0x000EA7CA
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			return SecurityUtils.SecureCreateInstance(objectType, args);
		}

		/// <summary>Gets a per-object cache, accessed as an <see cref="T:System.Collections.IDictionary" /> of key/value pairs.</summary>
		/// <param name="instance">The object for which to get the cache.</param>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> if the provided object supports caching; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600365D RID: 13917 RVA: 0x000EC601 File Offset: 0x000EA801
		public virtual IDictionary GetCache(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetCache(instance);
			}
			return null;
		}

		/// <summary>Gets an extended custom type descriptor for the given object.</summary>
		/// <param name="instance">The object for which to get the extended type descriptor.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide extended metadata for the object.</returns>
		// Token: 0x0600365E RID: 13918 RVA: 0x000EC619 File Offset: 0x000EA819
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			if (this._emptyDescriptor == null)
			{
				this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyDescriptor;
		}

		/// <summary>Gets the extender providers for the specified object.</summary>
		/// <param name="instance">The object to get extender providers for.</param>
		/// <returns>An array of extender providers for <paramref name="instance" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x0600365F RID: 13919 RVA: 0x000EC649 File Offset: 0x000EA849
		protected internal virtual IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtenderProviders(instance);
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return new IExtenderProvider[0];
		}

		/// <summary>Gets the name of the specified component, or <see langword="null" /> if the component has no name.</summary>
		/// <param name="component">The specified component.</param>
		/// <returns>The name of the specified component.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is <see langword="null" />.</exception>
		// Token: 0x06003660 RID: 13920 RVA: 0x000EC674 File Offset: 0x000EA874
		public virtual string GetFullComponentName(object component)
		{
			if (this._parent != null)
			{
				return this._parent.GetFullComponentName(component);
			}
			return this.GetTypeDescriptor(component).GetComponentName();
		}

		/// <summary>Performs normal reflection against a type.</summary>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is <see langword="null" />.</exception>
		// Token: 0x06003661 RID: 13921 RVA: 0x000EC697 File Offset: 0x000EA897
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		/// <summary>Performs normal reflection against the given object.</summary>
		/// <param name="instance">An instance of the type (should not be <see langword="null" />).</param>
		/// <returns>The type of reflection for this <paramref name="instance" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x06003662 RID: 13922 RVA: 0x000EC6A1 File Offset: 0x000EA8A1
		public Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetReflectionType(instance.GetType(), instance);
		}

		/// <summary>Performs normal reflection against the given object with the given type.</summary>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <param name="instance">An instance of the type. Can be <see langword="null" />.</param>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		// Token: 0x06003663 RID: 13923 RVA: 0x000EC6BE File Offset: 0x000EA8BE
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		/// <summary>Converts a reflection type into a runtime type.</summary>
		/// <param name="reflectionType">The type to convert to its runtime equivalent.</param>
		/// <returns>A <see cref="T:System.Type" /> that represents the runtime equivalent of <paramref name="reflectionType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionType" /> is <see langword="null" />.</exception>
		// Token: 0x06003664 RID: 13924 RVA: 0x000EC6D8 File Offset: 0x000EA8D8
		public virtual Type GetRuntimeType(Type reflectionType)
		{
			if (this._parent != null)
			{
				return this._parent.GetRuntimeType(reflectionType);
			}
			if (reflectionType == null)
			{
				throw new ArgumentNullException("reflectionType");
			}
			if (reflectionType.GetType().Assembly == typeof(object).Assembly)
			{
				return reflectionType;
			}
			return reflectionType.UnderlyingSystemType;
		}

		/// <summary>Gets a custom type descriptor for the given type.</summary>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		// Token: 0x06003665 RID: 13925 RVA: 0x000EC737 File Offset: 0x000EA937
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		/// <summary>Gets a custom type descriptor for the given object.</summary>
		/// <param name="instance">An instance of the type. Can be <see langword="null" /> if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x06003666 RID: 13926 RVA: 0x000EC741 File Offset: 0x000EA941
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		/// <summary>Gets a custom type descriptor for the given type and object.</summary>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <param name="instance">An instance of the type. Can be <see langword="null" /> if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		// Token: 0x06003667 RID: 13927 RVA: 0x000EC75E File Offset: 0x000EA95E
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			if (this._emptyDescriptor == null)
			{
				this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyDescriptor;
		}

		/// <summary>Gets a value that indicates whether the specified type is compatible with the type description and its chain of type description providers.</summary>
		/// <param name="type">The type to test for compatibility.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="type" /> is compatible with the type description and its chain of type description providers; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06003668 RID: 13928 RVA: 0x000EC78F File Offset: 0x000EA98F
		public virtual bool IsSupportedType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._parent == null || this._parent.IsSupportedType(type);
		}

		// Token: 0x04002A90 RID: 10896
		private TypeDescriptionProvider _parent;

		// Token: 0x04002A91 RID: 10897
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyDescriptor;

		// Token: 0x0200089D RID: 2205
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
		}
	}
}

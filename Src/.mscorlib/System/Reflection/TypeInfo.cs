using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents type declarations for class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</summary>
	// Token: 0x02000625 RID: 1573
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x06004909 RID: 18697 RVA: 0x00109478 File Offset: 0x00107678
		[FriendAccessAllowed]
		internal TypeInfo()
		{
		}

		/// <summary>Returns a representation of the current type as a <see cref="T:System.Reflection.TypeInfo" /> object.</summary>
		/// <returns>A reference to the current type.</returns>
		// Token: 0x0600490A RID: 18698 RVA: 0x00109480 File Offset: 0x00107680
		[__DynamicallyInvokable]
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		/// <summary>Returns the current type as a <see cref="T:System.Type" /> object.</summary>
		/// <returns>The current type.</returns>
		// Token: 0x0600490B RID: 18699 RVA: 0x00109483 File Offset: 0x00107683
		[__DynamicallyInvokable]
		public virtual Type AsType()
		{
			return this;
		}

		/// <summary>Gets an array of the generic type parameters of the current instance.</summary>
		/// <returns>An array that contains the current instance's generic type parameters, or an array of <see cref="P:System.Array.Length" /> zero if the current instance has no generic type parameters.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x00109486 File Offset: 0x00107686
		[__DynamicallyInvokable]
		public virtual Type[] GenericTypeParameters
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.IsGenericTypeDefinition)
				{
					return this.GetGenericArguments();
				}
				return Type.EmptyTypes;
			}
		}

		/// <summary>Returns a value that indicates whether the specified type can be assigned to the current type.</summary>
		/// <param name="typeInfo">The type to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified type can be assigned to this type; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600490D RID: 18701 RVA: 0x0010949C File Offset: 0x0010769C
		[__DynamicallyInvokable]
		public virtual bool IsAssignableFrom(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Returns an object that represents the specified public event declared by the current type.</summary>
		/// <param name="name">The name of the event.</param>
		/// <returns>An object that represents the specified event, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600490E RID: 18702 RVA: 0x00109507 File Offset: 0x00107707
		[__DynamicallyInvokable]
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public field declared by the current type.</summary>
		/// <param name="name">The name of the field.</param>
		/// <returns>An object that represents the specified field, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600490F RID: 18703 RVA: 0x00109512 File Offset: 0x00107712
		[__DynamicallyInvokable]
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public method declared by the current type.</summary>
		/// <param name="name">The name of the method.</param>
		/// <returns>An object that represents the specified method, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004910 RID: 18704 RVA: 0x0010951D File Offset: 0x0010771D
		[__DynamicallyInvokable]
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns a collection that contains all public methods declared on the current type that match the specified name.</summary>
		/// <param name="name">The method name to search for.</param>
		/// <returns>A collection that contains methods that match <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004911 RID: 18705 RVA: 0x00109528 File Offset: 0x00107728
		[__DynamicallyInvokable]
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		/// <summary>Returns an object that represents the specified public nested type declared by the current type.</summary>
		/// <param name="name">The name of the nested type.</param>
		/// <returns>An object that represents the specified nested type, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004912 RID: 18706 RVA: 0x00109540 File Offset: 0x00107740
		[__DynamicallyInvokable]
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		/// <summary>Returns an object that represents the specified public property declared by the current type.</summary>
		/// <param name="name">The name of the property.</param>
		/// <returns>An object that represents the specified property, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004913 RID: 18707 RVA: 0x00109568 File Offset: 0x00107768
		[__DynamicallyInvokable]
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Gets a collection of the constructors declared by the current type.</summary>
		/// <returns>A collection of the constructors declared by the current type.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x00109573 File Offset: 0x00107773
		[__DynamicallyInvokable]
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the events defined by the current type.</summary>
		/// <returns>A collection of the events defined by the current type.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004915 RID: 18709 RVA: 0x0010957D File Offset: 0x0010777D
		[__DynamicallyInvokable]
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the fields defined by the current type.</summary>
		/// <returns>A collection of the fields defined by the current type.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x00109587 File Offset: 0x00107787
		[__DynamicallyInvokable]
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the members defined by the current type.</summary>
		/// <returns>A collection of the members defined by the current type.</returns>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004917 RID: 18711 RVA: 0x00109591 File Offset: 0x00107791
		[__DynamicallyInvokable]
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the methods defined by the current type.</summary>
		/// <returns>A collection of the methods defined by the current type.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x0010959B File Offset: 0x0010779B
		[__DynamicallyInvokable]
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the nested types defined by the current type.</summary>
		/// <returns>A collection of nested types defined by the current type.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x001095A8 File Offset: 0x001077A8
		[__DynamicallyInvokable]
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				foreach (Type type in this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		/// <summary>Gets a collection of the properties defined by the current type.</summary>
		/// <returns>A collection of the properties defined by the current type.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x001095C5 File Offset: 0x001077C5
		[__DynamicallyInvokable]
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the interfaces implemented by the current type.</summary>
		/// <returns>A collection of the interfaces implemented by the current type.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x001095CF File Offset: 0x001077CF
		[__DynamicallyInvokable]
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetInterfaces();
			}
		}
	}
}

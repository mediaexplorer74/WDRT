using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the information necessary to create an instance of an object. This class cannot be inherited.</summary>
	// Token: 0x0200060E RID: 1550
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class InstanceDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" /> class using the specified member information and arguments.</summary>
		/// <param name="member">The member information for the descriptor. This can be a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.ConstructorInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />. If this is a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, it must represent a <see langword="static" /> member.</param>
		/// <param name="arguments">The collection of arguments to pass to the member. This parameter can be <see langword="null" /> or an empty collection if there are no arguments. The collection can also consist of other instances of <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, and it does not represent a <see langword="static" /> member.  
		/// -or-
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.PropertyInfo" /> and is not readable.  
		/// -or-
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" /> or <see cref="T:System.Reflection.ConstructorInfo" />, and the number of arguments in <paramref name="arguments" /> does not match the signature of <paramref name="member" />.
		/// -or-
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.ConstructorInfo" /> and represents a <see langword="static" /> member.  
		/// -or-
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.FieldInfo" />, and the number of arguments in <paramref name="arguments" /> is not zero.</exception>
		// Token: 0x060038BB RID: 14523 RVA: 0x000F13E7 File Offset: 0x000EF5E7
		public InstanceDescriptor(MemberInfo member, ICollection arguments)
			: this(member, arguments, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" /> class using the specified member information, arguments, and value indicating whether the specified information completely describes the instance.</summary>
		/// <param name="member">The member information for the descriptor. This can be a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.ConstructorInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />. If this is a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, it must represent a <see langword="static" /> member.</param>
		/// <param name="arguments">The collection of arguments to pass to the member. This parameter can be <see langword="null" /> or an empty collection if there are no arguments. The collection can also consist of other instances of <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />.</param>
		/// <param name="isComplete">
		///   <see langword="true" /> if the specified information completely describes the instance; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, and it does not represent a <see langword="static" /> member  
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.PropertyInfo" /> and is not readable.  
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" /> or <see cref="T:System.Reflection.ConstructorInfo" /> and the number of arguments in <paramref name="arguments" /> does not match the signature of <paramref name="member" />.  
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.ConstructorInfo" /> and represents a <see langword="static" /> member  
		/// <paramref name="member" /> is of type <see cref="T:System.Reflection.FieldInfo" />, and the number of arguments in <paramref name="arguments" /> is not zero.</exception>
		// Token: 0x060038BC RID: 14524 RVA: 0x000F13F4 File Offset: 0x000EF5F4
		public InstanceDescriptor(MemberInfo member, ICollection arguments, bool isComplete)
		{
			this.member = member;
			this.isComplete = isComplete;
			if (arguments == null)
			{
				this.arguments = new object[0];
			}
			else
			{
				object[] array = new object[arguments.Count];
				arguments.CopyTo(array, 0);
				this.arguments = array;
			}
			if (member is FieldInfo)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				if (!fieldInfo.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeStatic"));
				}
				if (this.arguments.Count != 0)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorLengthMismatch"));
				}
			}
			else if (member is ConstructorInfo)
			{
				ConstructorInfo constructorInfo = (ConstructorInfo)member;
				if (constructorInfo.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorCannotBeStatic"));
				}
				if (this.arguments.Count != constructorInfo.GetParameters().Length)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorLengthMismatch"));
				}
			}
			else if (member is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)member;
				if (!methodInfo.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeStatic"));
				}
				if (this.arguments.Count != methodInfo.GetParameters().Length)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorLengthMismatch"));
				}
			}
			else if (member is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (!propertyInfo.CanRead)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeReadable"));
				}
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				if (getMethod != null && !getMethod.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeStatic"));
				}
			}
		}

		/// <summary>Gets the collection of arguments that can be used to reconstruct an instance of the object that this instance descriptor represents.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of arguments that can be used to create the object.</returns>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x000F1578 File Offset: 0x000EF778
		public ICollection Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		/// <summary>Gets a value indicating whether the contents of this <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" /> completely identify the instance.</summary>
		/// <returns>
		///   <see langword="true" /> if the instance is completely described; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000F1580 File Offset: 0x000EF780
		public bool IsComplete
		{
			get
			{
				return this.isComplete;
			}
		}

		/// <summary>Gets the member information that describes the instance this descriptor is associated with.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> that describes the instance that this object is associated with.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x000F1588 File Offset: 0x000EF788
		public MemberInfo MemberInfo
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Invokes this instance descriptor and returns the object the descriptor describes.</summary>
		/// <returns>The object this instance descriptor describes.</returns>
		// Token: 0x060038C0 RID: 14528 RVA: 0x000F1590 File Offset: 0x000EF790
		public object Invoke()
		{
			object[] array = new object[this.arguments.Count];
			this.arguments.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is InstanceDescriptor)
				{
					array[i] = ((InstanceDescriptor)array[i]).Invoke();
				}
			}
			if (this.member is ConstructorInfo)
			{
				return ((ConstructorInfo)this.member).Invoke(array);
			}
			if (this.member is MethodInfo)
			{
				return ((MethodInfo)this.member).Invoke(null, array);
			}
			if (this.member is PropertyInfo)
			{
				return ((PropertyInfo)this.member).GetValue(null, array);
			}
			if (this.member is FieldInfo)
			{
				return ((FieldInfo)this.member).GetValue(null);
			}
			return null;
		}

		// Token: 0x04002B5E RID: 11102
		private MemberInfo member;

		// Token: 0x04002B5F RID: 11103
		private ICollection arguments;

		// Token: 0x04002B60 RID: 11104
		private bool isComplete;
	}
}

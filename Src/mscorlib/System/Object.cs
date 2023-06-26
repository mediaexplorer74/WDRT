using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Supports all classes in the .NET Framework class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all classes in the .NET Framework; it is the root of the type hierarchy.</summary>
	// Token: 0x0200003D RID: 61
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Object
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		// Token: 0x06000229 RID: 553 RVA: 0x00005BC1 File Offset: 0x00003DC1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public Object()
		{
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600022A RID: 554 RVA: 0x00005BC3 File Offset: 0x00003DC3
		[__DynamicallyInvokable]
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600022B RID: 555 RVA: 0x00005BD0 File Offset: 0x00003DD0
		[__DynamicallyInvokable]
		public virtual bool Equals(object obj)
		{
			return RuntimeHelpers.Equals(this, obj);
		}

		/// <summary>Determines whether the specified object instances are considered equal.</summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are considered equal; otherwise, <see langword="false" />. If both <paramref name="objA" /> and <paramref name="objB" /> are null, the method returns <see langword="true" />.</returns>
		// Token: 0x0600022C RID: 556 RVA: 0x00005BD9 File Offset: 0x00003DD9
		[__DynamicallyInvokable]
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> instances are the same instance.</summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object  to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="objA" /> is the same instance as <paramref name="objB" /> or if both are null; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600022D RID: 557 RVA: 0x00005BF0 File Offset: 0x00003DF0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		/// <summary>Serves as the default hash function.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x0600022E RID: 558 RVA: 0x00005BF6 File Offset: 0x00003DF6
		[__DynamicallyInvokable]
		public virtual int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the current instance.</summary>
		/// <returns>The exact runtime type of the current instance.</returns>
		// Token: 0x0600022F RID: 559
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000230 RID: 560 RVA: 0x00005BFE File Offset: 0x00003DFE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		protected virtual void Finalize()
		{
		}

		/// <summary>Creates a shallow copy of the current <see cref="T:System.Object" />.</summary>
		/// <returns>A shallow copy of the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000231 RID: 561
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected extern object MemberwiseClone();

		// Token: 0x06000232 RID: 562 RVA: 0x00005C00 File Offset: 0x00003E00
		[SecurityCritical]
		private void FieldSetter(string typeName, string fieldName, object val)
		{
			FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
			if (fieldInfo.IsInitOnly)
			{
				throw new FieldAccessException(Environment.GetResourceString("FieldAccess_InitOnly"));
			}
			Message.CoerceArg(val, fieldInfo.FieldType);
			fieldInfo.SetValue(this, val);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00005C44 File Offset: 0x00003E44
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
			FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
			val = fieldInfo.GetValue(this);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00005C64 File Offset: 0x00003E64
		private FieldInfo GetFieldInfo(string typeName, string fieldName)
		{
			Type type = this.GetType();
			while (null != type && !type.FullName.Equals(typeName))
			{
				type = type.BaseType;
			}
			if (null == type)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), typeName));
			}
			FieldInfo field = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (null == field)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadField"), fieldName, typeName));
			}
			return field;
		}
	}
}

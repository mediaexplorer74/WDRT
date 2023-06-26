using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>Represents a delegate, which is a data structure that refers to a static method or to a class instance and an instance method of that class.</summary>
	// Token: 0x02000085 RID: 133
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Delegate : ICloneable, ISerializable
	{
		/// <summary>Initializes a delegate that invokes the specified instance method on the specified class instance.</summary>
		/// <param name="target">The class instance on which the delegate invokes <paramref name="method" />.</param>
		/// <param name="method">The name of the instance method that the delegate represents.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">There was an error binding to the target method.</exception>
		// Token: 0x060006D7 RID: 1751 RVA: 0x00017A38 File Offset: 0x00015C38
		[SecuritySafeCritical]
		protected Delegate(object target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!this.BindToMethodName(target, (RuntimeType)target.GetType(), method, (DelegateBindingFlags)10))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
		}

		/// <summary>Initializes a delegate that invokes the specified static method from the specified class.</summary>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that defines <paramref name="method" />.</param>
		/// <param name="method">The name of the static method that the delegate represents.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="target" /> represents an open generic type.</exception>
		// Token: 0x060006D8 RID: 1752 RVA: 0x00017A90 File Offset: 0x00015C90
		[SecuritySafeCritical]
		protected Delegate(Type target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.IsGenericType && target.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), "target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = target as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "target");
			}
			this.BindToMethodName(null, runtimeType, method, (DelegateBindingFlags)37);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00017B1B File Offset: 0x00015D1B
		private Delegate()
		{
		}

		/// <summary>Dynamically invokes (late-bound) the method represented by the current delegate.</summary>
		/// <param name="args">An array of objects that are the arguments to pass to the method represented by the current delegate.  
		///  -or-  
		///  <see langword="null" />, if the method represented by the current delegate does not require arguments.</param>
		/// <returns>The object returned by the method represented by the delegate.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).  
		///  -or-  
		///  The number, order, or type of parameters listed in <paramref name="args" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The method represented by the delegate is an instance method and the target object is <see langword="null" />.  
		///  -or-  
		///  One of the encapsulated methods throws an exception.</exception>
		// Token: 0x060006DA RID: 1754 RVA: 0x00017B23 File Offset: 0x00015D23
		[__DynamicallyInvokable]
		public object DynamicInvoke(params object[] args)
		{
			return this.DynamicInvokeImpl(args);
		}

		/// <summary>Dynamically invokes (late-bound) the method represented by the current delegate.</summary>
		/// <param name="args">An array of objects that are the arguments to pass to the method represented by the current delegate.  
		///  -or-  
		///  <see langword="null" />, if the method represented by the current delegate does not require arguments.</param>
		/// <returns>The object returned by the method represented by the delegate.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).  
		///  -or-  
		///  The number, order, or type of parameters listed in <paramref name="args" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The method represented by the delegate is an instance method and the target object is <see langword="null" />.  
		///  -or-  
		///  One of the encapsulated methods throws an exception.</exception>
		// Token: 0x060006DB RID: 1755 RVA: 0x00017B2C File Offset: 0x00015D2C
		[SecuritySafeCritical]
		protected virtual object DynamicInvokeImpl(object[] args)
		{
			RuntimeMethodHandleInternal runtimeMethodHandleInternal = new RuntimeMethodHandleInternal(this.GetInvokeMethod());
			RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)RuntimeType.GetMethodBase((RuntimeType)base.GetType(), runtimeMethodHandleInternal);
			return runtimeMethodInfo.UnsafeInvoke(this, BindingFlags.Default, null, args, null);
		}

		/// <summary>Determines whether the specified object and the current delegate are of the same type and share the same targets, methods, and invocation list.</summary>
		/// <param name="obj">The object to compare with the current delegate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and the current delegate have the same targets, methods, and invocation list; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		// Token: 0x060006DC RID: 1756 RVA: 0x00017B68 File Offset: 0x00015D68
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == null || !Delegate.InternalEqualTypes(this, obj))
			{
				return false;
			}
			Delegate @delegate = (Delegate)obj;
			if (this._target == @delegate._target && this._methodPtr == @delegate._methodPtr && this._methodPtrAux == @delegate._methodPtrAux)
			{
				return true;
			}
			if (this._methodPtrAux.IsNull())
			{
				if (!@delegate._methodPtrAux.IsNull())
				{
					return false;
				}
				if (this._target != @delegate._target)
				{
					return false;
				}
			}
			else
			{
				if (@delegate._methodPtrAux.IsNull())
				{
					return false;
				}
				if (this._methodPtrAux == @delegate._methodPtrAux)
				{
					return true;
				}
			}
			if (this._methodBase == null || @delegate._methodBase == null || !(this._methodBase is MethodInfo) || !(@delegate._methodBase is MethodInfo))
			{
				return Delegate.InternalEqualMethodHandles(this, @delegate);
			}
			return this._methodBase.Equals(@delegate._methodBase);
		}

		/// <summary>Returns a hash code for the delegate.</summary>
		/// <returns>A hash code for the delegate.</returns>
		// Token: 0x060006DD RID: 1757 RVA: 0x00017C53 File Offset: 0x00015E53
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		/// <summary>Concatenates the invocation lists of two delegates.</summary>
		/// <param name="a">The delegate whose invocation list comes first.</param>
		/// <param name="b">The delegate whose invocation list comes last.</param>
		/// <returns>A new delegate with an invocation list that concatenates the invocation lists of <paramref name="a" /> and <paramref name="b" /> in that order. Returns <paramref name="a" /> if <paramref name="b" /> is <see langword="null" />, returns <paramref name="b" /> if <paramref name="a" /> is a null reference, and returns a null reference if both <paramref name="a" /> and <paramref name="b" /> are null references.</returns>
		/// <exception cref="T:System.ArgumentException">Both <paramref name="a" /> and <paramref name="b" /> are not <see langword="null" />, and <paramref name="a" /> and <paramref name="b" /> are not instances of the same delegate type.</exception>
		// Token: 0x060006DE RID: 1758 RVA: 0x00017C60 File Offset: 0x00015E60
		[__DynamicallyInvokable]
		public static Delegate Combine(Delegate a, Delegate b)
		{
			if (a == null)
			{
				return b;
			}
			return a.CombineImpl(b);
		}

		/// <summary>Concatenates the invocation lists of an array of delegates.</summary>
		/// <param name="delegates">The array of delegates to combine.</param>
		/// <returns>A new delegate with an invocation list that concatenates the invocation lists of the delegates in the <paramref name="delegates" /> array. Returns <see langword="null" /> if <paramref name="delegates" /> is <see langword="null" />, if <paramref name="delegates" /> contains zero elements, or if every entry in <paramref name="delegates" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">Not all the non-null entries in <paramref name="delegates" /> are instances of the same delegate type.</exception>
		// Token: 0x060006DF RID: 1759 RVA: 0x00017C70 File Offset: 0x00015E70
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static Delegate Combine(params Delegate[] delegates)
		{
			if (delegates == null || delegates.Length == 0)
			{
				return null;
			}
			Delegate @delegate = delegates[0];
			for (int i = 1; i < delegates.Length; i++)
			{
				@delegate = Delegate.Combine(@delegate, delegates[i]);
			}
			return @delegate;
		}

		/// <summary>Returns the invocation list of the delegate.</summary>
		/// <returns>An array of delegates representing the invocation list of the current delegate.</returns>
		// Token: 0x060006E0 RID: 1760 RVA: 0x00017CA4 File Offset: 0x00015EA4
		[__DynamicallyInvokable]
		public virtual Delegate[] GetInvocationList()
		{
			return new Delegate[] { this };
		}

		/// <summary>Gets the method represented by the delegate.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> describing the method represented by the delegate.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00017CBD File Offset: 0x00015EBD
		[__DynamicallyInvokable]
		public MethodInfo Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethodImpl();
			}
		}

		/// <summary>Gets the static method represented by the current delegate.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> describing the static method represented by the current delegate.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		// Token: 0x060006E2 RID: 1762 RVA: 0x00017CC8 File Offset: 0x00015EC8
		[SecuritySafeCritical]
		protected virtual MethodInfo GetMethodImpl()
		{
			if (this._methodBase == null || !(this._methodBase is MethodInfo))
			{
				IRuntimeMethodInfo runtimeMethodInfo = this.FindMethodHandle();
				RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if ((RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType)) && (RuntimeMethodHandle.GetAttributes(runtimeMethodInfo) & MethodAttributes.Static) <= MethodAttributes.PrivateScope)
				{
					if (this._methodPtrAux == (IntPtr)0)
					{
						Type type = this._target.GetType();
						Type genericTypeDefinition = runtimeType.GetGenericTypeDefinition();
						while (type != null)
						{
							if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
							{
								runtimeType = type as RuntimeType;
								break;
							}
							type = type.BaseType;
						}
					}
					else
					{
						MethodInfo method = base.GetType().GetMethod("Invoke");
						runtimeType = (RuntimeType)method.GetParameters()[0].ParameterType;
					}
				}
				this._methodBase = (MethodInfo)RuntimeType.GetMethodBase(runtimeType, runtimeMethodInfo);
			}
			return (MethodInfo)this._methodBase;
		}

		/// <summary>Gets the class instance on which the current delegate invokes the instance method.</summary>
		/// <returns>The object on which the current delegate invokes the instance method, if the delegate represents an instance method; <see langword="null" /> if the delegate represents a static method.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00017DBF File Offset: 0x00015FBF
		[__DynamicallyInvokable]
		public object Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetTarget();
			}
		}

		/// <summary>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</summary>
		/// <param name="source">The delegate from which to remove the invocation list of <paramref name="value" />.</param>
		/// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="source" />.</param>
		/// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="source" /> and removing the last occurrence of the invocation list of <paramref name="value" />, if the invocation list of <paramref name="value" /> is found within the invocation list of <paramref name="source" />. Returns <paramref name="source" /> if <paramref name="value" /> is <see langword="null" /> or if the invocation list of <paramref name="value" /> is not found within the invocation list of <paramref name="source" />. Returns a null reference if the invocation list of <paramref name="value" /> is equal to the invocation list of <paramref name="source" /> or if <paramref name="source" /> is a null reference.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		/// <exception cref="T:System.ArgumentException">The delegate types do not match.</exception>
		// Token: 0x060006E4 RID: 1764 RVA: 0x00017DC7 File Offset: 0x00015FC7
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static Delegate Remove(Delegate source, Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			if (value == null)
			{
				return source;
			}
			if (!Delegate.InternalEqualTypes(source, value))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTypeMis"));
			}
			return source.RemoveImpl(value);
		}

		/// <summary>Removes all occurrences of the invocation list of a delegate from the invocation list of another delegate.</summary>
		/// <param name="source">The delegate from which to remove the invocation list of <paramref name="value" />.</param>
		/// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="source" />.</param>
		/// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="source" /> and removing all occurrences of the invocation list of <paramref name="value" />, if the invocation list of <paramref name="value" /> is found within the invocation list of <paramref name="source" />. Returns <paramref name="source" /> if <paramref name="value" /> is <see langword="null" /> or if the invocation list of <paramref name="value" /> is not found within the invocation list of <paramref name="source" />. Returns a null reference if the invocation list of <paramref name="value" /> is equal to the invocation list of <paramref name="source" />, if <paramref name="source" /> contains only a series of invocation lists that are equal to the invocation list of <paramref name="value" />, or if <paramref name="source" /> is a null reference.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		/// <exception cref="T:System.ArgumentException">The delegate types do not match.</exception>
		// Token: 0x060006E5 RID: 1765 RVA: 0x00017DF4 File Offset: 0x00015FF4
		[__DynamicallyInvokable]
		public static Delegate RemoveAll(Delegate source, Delegate value)
		{
			Delegate @delegate;
			do
			{
				@delegate = source;
				source = Delegate.Remove(source, value);
			}
			while (@delegate != source);
			return @delegate;
		}

		/// <summary>Concatenates the invocation lists of the specified multicast (combinable) delegate and the current multicast (combinable) delegate.</summary>
		/// <param name="d">The multicast (combinable) delegate whose invocation list to append to the end of the invocation list of the current multicast (combinable) delegate.</param>
		/// <returns>A new multicast (combinable) delegate with an invocation list that concatenates the invocation list of the current multicast (combinable) delegate and the invocation list of <paramref name="d" />, or the current multicast (combinable) delegate if <paramref name="d" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.MulticastNotSupportedException">Always thrown.</exception>
		// Token: 0x060006E6 RID: 1766 RVA: 0x00017E18 File Offset: 0x00016018
		protected virtual Delegate CombineImpl(Delegate d)
		{
			throw new MulticastNotSupportedException(Environment.GetResourceString("Multicast_Combine"));
		}

		/// <summary>Removes the invocation list of a delegate from the invocation list of another delegate.</summary>
		/// <param name="d">The delegate that supplies the invocation list to remove from the invocation list of the current delegate.</param>
		/// <returns>A new delegate with an invocation list formed by taking the invocation list of the current delegate and removing the invocation list of <paramref name="value" />, if the invocation list of <paramref name="value" /> is found within the current delegate's invocation list. Returns the current delegate if <paramref name="value" /> is <see langword="null" /> or if the invocation list of <paramref name="value" /> is not found within the current delegate's invocation list. Returns <see langword="null" /> if the invocation list of <paramref name="value" /> is equal to the current delegate's invocation list.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		// Token: 0x060006E7 RID: 1767 RVA: 0x00017E29 File Offset: 0x00016029
		protected virtual Delegate RemoveImpl(Delegate d)
		{
			if (!d.Equals(this))
			{
				return this;
			}
			return null;
		}

		/// <summary>Creates a shallow copy of the delegate.</summary>
		/// <returns>A shallow copy of the delegate.</returns>
		// Token: 0x060006E8 RID: 1768 RVA: 0x00017E37 File Offset: 0x00016037
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Creates a delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="target">The class instance on which <paramref name="method" /> is invoked.</param>
		/// <param name="method">The name of the instance method that the delegate is to represent.</param>
		/// <returns>A delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> is not an instance method.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006E9 RID: 1769 RVA: 0x00017E3F File Offset: 0x0001603F
		public static Delegate CreateDelegate(Type type, object target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified instance method to invoke on the specified class instance with the specified case-sensitivity.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="target">The class instance on which <paramref name="method" /> is invoked.</param>
		/// <param name="method">The name of the instance method that the delegate is to represent.</param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method.</param>
		/// <returns>A delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> is not an instance method.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006EA RID: 1770 RVA: 0x00017E4B File Offset: 0x0001604B
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified instance method to invoke on the specified class instance, with the specified case-sensitivity and the specified behavior on failure to bind.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="target">The class instance on which <paramref name="method" /> is invoked.</param>
		/// <param name="method">The name of the instance method that the delegate is to represent.</param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method.</param>
		/// <param name="throwOnBindFailure">
		///   <see langword="true" /> to throw an exception if <paramref name="method" /> cannot be bound; otherwise, <see langword="false" />.</param>
		/// <returns>A delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> is not an instance method.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006EB RID: 1771 RVA: 0x00017E58 File Offset: 0x00016058
		[SecuritySafeCritical]
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodName(target, (RuntimeType)target.GetType(), method, (DelegateBindingFlags)26 | (ignoreCase ? DelegateBindingFlags.CaselessMatching : ((DelegateBindingFlags)0))))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
				}
				@delegate = null;
			}
			return @delegate;
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static method of the specified class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that implements <paramref name="method" />.</param>
		/// <param name="method">The name of the static method that the delegate is to represent.</param>
		/// <returns>A delegate of the specified type that represents the specified static method of the specified class.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="target" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="target" /> is an open generic type. That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is <see langword="true" />.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="static" /> method (<see langword="Shared" /> method in Visual Basic).  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006EC RID: 1772 RVA: 0x00017F15 File Offset: 0x00016115
		public static Delegate CreateDelegate(Type type, Type target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static method of the specified class, with the specified case-sensitivity.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that implements <paramref name="method" />.</param>
		/// <param name="method">The name of the static method that the delegate is to represent.</param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method.</param>
		/// <returns>A delegate of the specified type that represents the specified static method of the specified class.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="target" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="target" /> is an open generic type. That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is <see langword="true" />.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="static" /> method (<see langword="Shared" /> method in Visual Basic).  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006ED RID: 1773 RVA: 0x00017F21 File Offset: 0x00016121
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static method of the specified class, with the specified case-sensitivity and the specified behavior on failure to bind.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that implements <paramref name="method" />.</param>
		/// <param name="method">The name of the static method that the delegate is to represent.</param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method.</param>
		/// <param name="throwOnBindFailure">
		///   <see langword="true" /> to throw an exception if <paramref name="method" /> cannot be bound; otherwise, <see langword="false" />.</param>
		/// <returns>A delegate of the specified type that represents the specified static method of the specified class.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="target" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="target" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="target" /> is an open generic type. That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is <see langword="true" />.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="static" /> method (<see langword="Shared" /> method in Visual Basic).  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006EE RID: 1774 RVA: 0x00017F30 File Offset: 0x00016130
		[SecuritySafeCritical]
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.IsGenericType && target.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), "target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			RuntimeType runtimeType2 = target as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (runtimeType2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "target");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodName(null, runtimeType2, method, (DelegateBindingFlags)5 | (ignoreCase ? DelegateBindingFlags.CaselessMatching : ((DelegateBindingFlags)0))))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
				}
				@delegate = null;
			}
			return @delegate;
		}

		/// <summary>Creates a delegate of the specified type to represent the specified static method, with the specified behavior on failure to bind.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent.</param>
		/// <param name="throwOnBindFailure">
		///   <see langword="true" /> to throw an exception if <paramref name="method" /> cannot be bound; otherwise, <see langword="false" />.</param>
		/// <returns>A delegate of the specified type to represent the specified static method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, and <paramref name="throwOnBindFailure" /> is <see langword="true" />.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="RuntimeMethodInfo" />. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006EF RID: 1775 RVA: 0x00018034 File Offset: 0x00016234
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, runtimeMethodInfo, null, (DelegateBindingFlags)132, ref stackCrawlMark);
			if (@delegate == null && throwOnBindFailure)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static or instance method, with the specified first argument.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="firstArgument">The object to which the delegate is bound, or <see langword="null" /> to treat <paramref name="method" /> as <see langword="static" /> (<see langword="Shared" /> in Visual Basic).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent.</param>
		/// <returns>A delegate of the specified type that represents the specified static or instance method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="RuntimeMethodInfo" />. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006F0 RID: 1776 RVA: 0x000180FB File Offset: 0x000162FB
		[__DynamicallyInvokable]
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static or instance method, with the specified first argument and the specified behavior on failure to bind.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> representing the type of delegate to create.</param>
		/// <param name="firstArgument">An <see cref="T:System.Object" /> that is the first argument of the method the delegate represents. For instance methods, it must be compatible with the instance type.</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent.</param>
		/// <param name="throwOnBindFailure">
		///   <see langword="true" /> to throw an exception if <paramref name="method" /> cannot be bound; otherwise, <see langword="false" />.</param>
		/// <returns>A delegate of the specified type that represents the specified static or instance method, or <see langword="null" /> if <paramref name="throwOnBindFailure" /> is <see langword="false" /> and the delegate cannot be bound to <paramref name="method" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound, and <paramref name="throwOnBindFailure" /> is <see langword="true" />.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="RuntimeMethodInfo" />. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006F1 RID: 1777 RVA: 0x00018108 File Offset: 0x00016308
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, runtimeMethodInfo, firstArgument, DelegateBindingFlags.RelaxedSignature, ref stackCrawlMark);
			if (@delegate == null && throwOnBindFailure)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		/// <summary>Determines whether the specified delegates are equal.</summary>
		/// <param name="d1">The first delegate to compare.</param>
		/// <param name="d2">The second delegate to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is equal to <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006F2 RID: 1778 RVA: 0x000181CF File Offset: 0x000163CF
		[__DynamicallyInvokable]
		public static bool operator ==(Delegate d1, Delegate d2)
		{
			if (d1 == null)
			{
				return d2 == null;
			}
			return d1.Equals(d2);
		}

		/// <summary>Determines whether the specified delegates are not equal.</summary>
		/// <param name="d1">The first delegate to compare.</param>
		/// <param name="d2">The second delegate to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is not equal to <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006F3 RID: 1779 RVA: 0x000181E0 File Offset: 0x000163E0
		[__DynamicallyInvokable]
		public static bool operator !=(Delegate d1, Delegate d2)
		{
			if (d1 == null)
			{
				return d2 != null;
			}
			return !d1.Equals(d2);
		}

		/// <summary>Not supported.</summary>
		/// <param name="info">Not supported.</param>
		/// <param name="context">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060006F4 RID: 1780 RVA: 0x000181F4 File Offset: 0x000163F4
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000181FC File Offset: 0x000163FC
		[SecurityCritical]
		internal static Delegate CreateDelegateNoSecurityCheck(Type type, object target, RuntimeMethodHandle method)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method.IsNullHandle())
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodInfo(target, method.GetMethodInfo(), RuntimeMethodHandle.GetDeclaringType(method.GetMethodInfo()), (DelegateBindingFlags)192))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000182AC File Offset: 0x000164AC
		[SecurityCritical]
		internal static Delegate CreateDelegateNoSecurityCheck(RuntimeType type, object firstArgument, MethodInfo method)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
			}
			if (!type.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.UnsafeCreateDelegate(type, runtimeMethodInfo, firstArgument, (DelegateBindingFlags)192);
			if (@delegate == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		/// <summary>Creates a delegate of the specified type to represent the specified static method.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create.</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent. Only static methods are supported in the .NET Framework version 1.0 and 1.1.</param>
		/// <returns>A delegate of the specified type to represent the specified static method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.  
		/// -or-  
		/// <paramref name="type" /> is not a <see langword="RuntimeType" />. See Runtime Types in Reflection.  
		/// -or-  
		/// <paramref name="method" /> is not a static method, and the .NET Framework version is 1.0 or 1.1.  
		/// -or-  
		/// <paramref name="method" /> cannot be bound.  
		/// -or-  
		/// <paramref name="method" /> is not a <see langword="RuntimeMethodInfo" />. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The <see langword="Invoke" /> method of <paramref name="type" /> is not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />.</exception>
		// Token: 0x060006F7 RID: 1783 RVA: 0x00018345 File Offset: 0x00016545
		[__DynamicallyInvokable]
		public static Delegate CreateDelegate(Type type, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, method, true);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00018350 File Offset: 0x00016550
		[SecuritySafeCritical]
		internal static Delegate CreateDelegateInternal(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags, ref StackCrawlMark stackMark)
		{
			bool flag = (rtMethod.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
			bool flag2 = (rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
			if (flag || flag2)
			{
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[] { flag ? rtMethod.FullName : rtType.FullName }));
				}
			}
			return Delegate.UnsafeCreateDelegate(rtType, rtMethod, firstArgument, flags);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000183C8 File Offset: 0x000165C8
		[SecurityCritical]
		internal static Delegate UnsafeCreateDelegate(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags)
		{
			Delegate @delegate = Delegate.InternalAlloc(rtType);
			if (@delegate.BindToMethodInfo(firstArgument, rtMethod, rtMethod.GetDeclaringTypeInternal(), flags))
			{
				return @delegate;
			}
			return null;
		}

		// Token: 0x060006FA RID: 1786
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool BindToMethodName(object target, RuntimeType methodType, string method, DelegateBindingFlags flags);

		// Token: 0x060006FB RID: 1787
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool BindToMethodInfo(object target, IRuntimeMethodInfo method, RuntimeType methodType, DelegateBindingFlags flags);

		// Token: 0x060006FC RID: 1788
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MulticastDelegate InternalAlloc(RuntimeType type);

		// Token: 0x060006FD RID: 1789
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MulticastDelegate InternalAllocLike(Delegate d);

		// Token: 0x060006FE RID: 1790
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalEqualTypes(object a, object b);

		// Token: 0x060006FF RID: 1791
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DelegateConstruct(object target, IntPtr slot);

		// Token: 0x06000700 RID: 1792
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetMulticastInvoke();

		// Token: 0x06000701 RID: 1793
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInvokeMethod();

		// Token: 0x06000702 RID: 1794
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IRuntimeMethodInfo FindMethodHandle();

		// Token: 0x06000703 RID: 1795
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalEqualMethodHandles(Delegate left, Delegate right);

		// Token: 0x06000704 RID: 1796
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr AdjustTarget(object target, IntPtr methodPtr);

		// Token: 0x06000705 RID: 1797
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetCallStub(IntPtr methodPtr);

		// Token: 0x06000706 RID: 1798 RVA: 0x000183F0 File Offset: 0x000165F0
		[SecuritySafeCritical]
		internal virtual object GetTarget()
		{
			if (!this._methodPtrAux.IsNull())
			{
				return null;
			}
			return this._target;
		}

		// Token: 0x06000707 RID: 1799
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareUnmanagedFunctionPtrs(Delegate d1, Delegate d2);

		// Token: 0x040002F3 RID: 755
		[SecurityCritical]
		internal object _target;

		// Token: 0x040002F4 RID: 756
		[SecurityCritical]
		internal object _methodBase;

		// Token: 0x040002F5 RID: 757
		[SecurityCritical]
		internal IntPtr _methodPtr;

		// Token: 0x040002F6 RID: 758
		[SecurityCritical]
		internal IntPtr _methodPtrAux;
	}
}

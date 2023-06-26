using System;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides lazy initialization routines.</summary>
	// Token: 0x0200053C RID: 1340
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class LazyInitializer
	{
		/// <summary>Initializes a target reference type with the type's default constructor if it hasn't already been initialized.</summary>
		/// <param name="target">A reference of type T to initialize if it has not already been initialized.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <returns>The initialized reference of type <paramref name="T" />.</returns>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06003EF8 RID: 16120 RVA: 0x000EB651 File Offset: 0x000E9851
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target) where T : class
		{
			if (Volatile.Read<T>(ref target) != null)
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, LazyHelpers<T>.s_activatorFactorySelector);
		}

		/// <summary>Initializes a target reference type by using a specified function if it hasn't already been initialized.</summary>
		/// <param name="target">The reference of type T to initialize if it hasn't already been initialized.</param>
		/// <param name="valueFactory">The function that is called to initialize the reference.</param>
		/// <typeparam name="T">The reference type of the reference to be initialized.</typeparam>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="valueFactory" /> returned null (Nothing in Visual Basic).</exception>
		// Token: 0x06003EF9 RID: 16121 RVA: 0x000EB672 File Offset: 0x000E9872
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
		{
			if (Volatile.Read<T>(ref target) != null)
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x000EB690 File Offset: 0x000E9890
		private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Lazy_StaticInit_InvalidOperation"));
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		/// <summary>Initializes a target reference or value type with its default constructor if it hasn't already been initialized.</summary>
		/// <param name="target">A reference or value of type T to initialize if it hasn't already been initialized.</param>
		/// <param name="initialized">A reference to a Boolean value that determines whether the target has already been initialized.</param>
		/// <param name="syncLock">A reference to an object used as the mutually exclusive lock for initializing <paramref name="target" />. If <paramref name="syncLock" /> is <see langword="null" />, a new object will be instantiated.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06003EFB RID: 16123 RVA: 0x000EB6D3 File Offset: 0x000E98D3
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, LazyHelpers<T>.s_activatorFactorySelector);
		}

		/// <summary>Initializes a target reference or value type by using a specified function if it hasn't already been initialized.</summary>
		/// <param name="target">A reference or value of type T to initialize if it hasn't already been initialized.</param>
		/// <param name="initialized">A reference to a Boolean value that determines whether the target has already been initialized.</param>
		/// <param name="syncLock">A reference to an object used as the mutually exclusive lock for initializing <paramref name="target" />. If <paramref name="syncLock" /> is <see langword="null" />, a new object will be instantiated.</param>
		/// <param name="valueFactory">The function that is called to initialize the reference or value.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06003EFC RID: 16124 RVA: 0x000EB6F1 File Offset: 0x000E98F1
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000EB70C File Offset: 0x000E990C
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			object obj = syncLock;
			if (obj == null)
			{
				object obj2 = new object();
				obj = Interlocked.CompareExchange(ref syncLock, obj2, null);
				if (obj == null)
				{
					obj = obj2;
				}
			}
			object obj3 = obj;
			lock (obj3)
			{
				if (!Volatile.Read(ref initialized))
				{
					target = valueFactory();
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}
	}
}

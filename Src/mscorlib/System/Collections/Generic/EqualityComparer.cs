using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
	/// <summary>Provides a base class for implementations of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface.</summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	// Token: 0x020004BE RID: 1214
	[TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
	{
		/// <summary>Returns a default equality comparer for the type specified by the generic argument.</summary>
		/// <returns>The default instance of the <see cref="T:System.Collections.Generic.EqualityComparer`1" /> class for type <typeparamref name="T" />.</returns>
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000E05A9 File Offset: 0x000DE7A9
		[__DynamicallyInvokable]
		public static EqualityComparer<T> Default
		{
			[__DynamicallyInvokable]
			get
			{
				return EqualityComparer<T>.defaultComparer;
			}
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000E05B0 File Offset: 0x000DE7B0
		[SecuritySafeCritical]
		private static EqualityComparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (runtimeType == typeof(byte))
			{
				return (EqualityComparer<T>)new ByteEqualityComparer();
			}
			if (typeof(IEquatable<T>).IsAssignableFrom(runtimeType))
			{
				return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericEqualityComparer<int>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IEquatable<>).MakeGenericType(new Type[] { runtimeType2 }).IsAssignableFrom(runtimeType2))
				{
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(NullableEqualityComparer<int>), runtimeType2);
				}
			}
			if (runtimeType.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(runtimeType)))
				{
				case TypeCode.SByte:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(SByteEnumEqualityComparer<sbyte>), runtimeType);
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(EnumEqualityComparer<int>), runtimeType);
				case TypeCode.Int16:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(ShortEnumEqualityComparer<short>), runtimeType);
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(LongEnumEqualityComparer<long>), runtimeType);
				}
			}
			return new ObjectEqualityComparer<T>();
		}

		/// <summary>When overridden in a derived class, determines whether two objects of type <typeparamref name="T" /> are equal.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003A89 RID: 14985
		[__DynamicallyInvokable]
		public abstract bool Equals(T x, T y);

		/// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
		/// <param name="obj">The object for which to get a hash code.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06003A8A RID: 14986
		[__DynamicallyInvokable]
		public abstract int GetHashCode(T obj);

		// Token: 0x06003A8B RID: 14987 RVA: 0x000E0734 File Offset: 0x000DE934
		internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000E0768 File Offset: 0x000DE968
		internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.  
		///  -or-  
		///  <paramref name="obj" /> is of a type that cannot be cast to type <typeparamref name="T" />.</exception>
		// Token: 0x06003A8D RID: 14989 RVA: 0x000E079B File Offset: 0x000DE99B
		[__DynamicallyInvokable]
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is T)
			{
				return this.GetHashCode((T)((object)obj));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return 0;
		}

		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="x" /> or <paramref name="y" /> is of a type that cannot be cast to type <typeparamref name="T" />.</exception>
		// Token: 0x06003A8E RID: 14990 RVA: 0x000E07BE File Offset: 0x000DE9BE
		[__DynamicallyInvokable]
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is T && y is T)
			{
				return this.Equals((T)((object)x), (T)((object)y));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.EqualityComparer`1" /> class.</summary>
		// Token: 0x06003A8F RID: 14991 RVA: 0x000E07F8 File Offset: 0x000DE9F8
		[__DynamicallyInvokable]
		protected EqualityComparer()
		{
		}

		// Token: 0x04001958 RID: 6488
		private static readonly EqualityComparer<T> defaultComparer = EqualityComparer<T>.CreateComparer();
	}
}

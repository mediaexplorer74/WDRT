using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System
{
	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
	// Token: 0x020000F8 RID: 248
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(System_LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class Lazy<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the default constructor of the target type is used.</summary>
		// Token: 0x06000F12 RID: 3858 RVA: 0x0002EE4D File Offset: 0x0002D04D
		[__DynamicallyInvokable]
		public Lazy()
			: this(LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the specified initialization function is used.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is <see langword="null" />.</exception>
		// Token: 0x06000F13 RID: 3859 RVA: 0x0002EE56 File Offset: 0x0002D056
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory)
			: this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the default constructor of the target type and the specified initialization mode are used.</summary>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> to make this instance usable concurrently by multiple threads; <see langword="false" /> to make the instance usable by only one thread at a time.</param>
		// Token: 0x06000F14 RID: 3860 RVA: 0x0002EE60 File Offset: 0x0002D060
		[__DynamicallyInvokable]
		public Lazy(bool isThreadSafe)
			: this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class that uses the default constructor of <paramref name="T" /> and the specified thread-safety mode.</summary>
		/// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value.</exception>
		// Token: 0x06000F15 RID: 3861 RVA: 0x0002EE6F File Offset: 0x0002D06F
		[__DynamicallyInvokable]
		public Lazy(LazyThreadSafetyMode mode)
		{
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the specified initialization function and initialization mode are used.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> to make this instance usable concurrently by multiple threads; <see langword="false" /> to make this instance usable by only one thread at a time.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is <see langword="null" />.</exception>
		// Token: 0x06000F16 RID: 3862 RVA: 0x0002EE83 File Offset: 0x0002D083
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory, bool isThreadSafe)
			: this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class that uses the specified initialization function and thread-safety mode.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is <see langword="null" />.</exception>
		// Token: 0x06000F17 RID: 3863 RVA: 0x0002EE93 File Offset: 0x0002D093
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
			this.m_valueFactory = valueFactory;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002EEBC File Offset: 0x0002D0BC
		private static object GetObjectFromMode(LazyThreadSafetyMode mode)
		{
			if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
			{
				return new object();
			}
			if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
			}
			if (mode != LazyThreadSafetyMode.None)
			{
				throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Lazy_ctor_ModeInvalid"));
			}
			return null;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			T value = this.Value;
		}

		/// <summary>Creates and returns a string representation of the <see cref="P:System.Lazy`1.Value" /> property for this instance.</summary>
		/// <returns>The result of calling the <see cref="M:System.Object.ToString" /> method on the <see cref="P:System.Lazy`1.Value" /> property for this instance, if the value has been created (that is, if the <see cref="P:System.Lazy`1.IsValueCreated" /> property returns <see langword="true" />). Otherwise, a string indicating that the value has not been created.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Lazy`1.Value" /> property is <see langword="null" />.</exception>
		// Token: 0x06000F1A RID: 3866 RVA: 0x0002EF00 File Offset: 0x0002D100
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return Environment.GetResourceString("Lazy_ToString_ValueNotCreated");
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0002EF34 File Offset: 0x0002D134
		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return ((Lazy<T>.Boxed)this.m_boxed).m_value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002EF63 File Offset: 0x0002D163
		internal LazyThreadSafetyMode Mode
		{
			get
			{
				if (this.m_threadSafeObj == null)
				{
					return LazyThreadSafetyMode.None;
				}
				if (this.m_threadSafeObj == LazyHelpers.PUBLICATION_ONLY_SENTINEL)
				{
					return LazyThreadSafetyMode.PublicationOnly;
				}
				return LazyThreadSafetyMode.ExecutionAndPublication;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002EF7F File Offset: 0x0002D17F
		internal bool IsValueFaulted
		{
			get
			{
				return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
			}
		}

		/// <summary>Gets a value that indicates whether a value has been created for this <see cref="T:System.Lazy`1" /> instance.</summary>
		/// <returns>
		///   <see langword="true" /> if a value has been created for this <see cref="T:System.Lazy`1" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0002EF8F File Offset: 0x0002D18F
		[__DynamicallyInvokable]
		public bool IsValueCreated
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_boxed != null && this.m_boxed is Lazy<T>.Boxed;
			}
		}

		/// <summary>Gets the lazily initialized value of the current <see cref="T:System.Lazy`1" /> instance.</summary>
		/// <returns>The lazily initialized value of the current <see cref="T:System.Lazy`1" /> instance.</returns>
		/// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
		/// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function tries to access <see cref="P:System.Lazy`1.Value" /> on this instance.</exception>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002EFAC File Offset: 0x0002D1AC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_boxed != null)
				{
					Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
					if (boxed != null)
					{
						return boxed.m_value;
					}
					Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
					lazyInternalExceptionHolder.m_edi.Throw();
				}
				Debugger.NotifyOfCrossThreadDependency();
				return this.LazyInitValue();
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002EFFC File Offset: 0x0002D1FC
		private T LazyInitValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (mode == LazyThreadSafetyMode.None)
			{
				boxed = this.CreateValue();
				this.m_boxed = boxed;
			}
			else if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				boxed = this.CreateValue();
				if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, boxed, null) != null)
				{
					boxed = (Lazy<T>.Boxed)this.m_boxed;
				}
				else
				{
					this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
				}
			}
			else
			{
				object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
				bool flag = false;
				try
				{
					if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						Monitor.Enter(obj, ref flag);
					}
					if (this.m_boxed == null)
					{
						boxed = this.CreateValue();
						this.m_boxed = boxed;
						Volatile.Write<object>(ref this.m_threadSafeObj, Lazy<T>.ALREADY_INVOKED_SENTINEL);
					}
					else
					{
						boxed = this.m_boxed as Lazy<T>.Boxed;
						if (boxed == null)
						{
							Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
							lazyInternalExceptionHolder.m_edi.Throw();
						}
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			return boxed.m_value;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		private Lazy<T>.Boxed CreateValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (this.m_valueFactory != null)
			{
				try
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
					}
					Func<T> valueFactory = this.m_valueFactory;
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
					}
					else if (valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						return null;
					}
					return new Lazy<T>.Boxed(valueFactory());
				}
				catch (Exception ex)
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex);
					}
					throw;
				}
			}
			try
			{
				boxed = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));
			}
			catch (MissingMethodException)
			{
				Exception ex2 = new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex2);
				}
				throw ex2;
			}
			return boxed;
		}

		// Token: 0x0400059A RID: 1434
		private static readonly Func<T> ALREADY_INVOKED_SENTINEL = () => default(T);

		// Token: 0x0400059B RID: 1435
		private object m_boxed;

		// Token: 0x0400059C RID: 1436
		[NonSerialized]
		private Func<T> m_valueFactory;

		// Token: 0x0400059D RID: 1437
		[NonSerialized]
		private object m_threadSafeObj;

		// Token: 0x02000AEC RID: 2796
		[Serializable]
		private class Boxed
		{
			// Token: 0x06006A23 RID: 27171 RVA: 0x0016EB1F File Offset: 0x0016CD1F
			internal Boxed(T value)
			{
				this.m_value = value;
			}

			// Token: 0x040031B2 RID: 12722
			internal T m_value;
		}

		// Token: 0x02000AED RID: 2797
		private class LazyInternalExceptionHolder
		{
			// Token: 0x06006A24 RID: 27172 RVA: 0x0016EB2E File Offset: 0x0016CD2E
			internal LazyInternalExceptionHolder(Exception ex)
			{
				this.m_edi = ExceptionDispatchInfo.Capture(ex);
			}

			// Token: 0x040031B3 RID: 12723
			internal ExceptionDispatchInfo m_edi;
		}
	}
}

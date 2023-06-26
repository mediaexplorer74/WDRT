using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a typed weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	/// <typeparam name="T">The type of the object referenced.</typeparam>
	// Token: 0x0200015E RID: 350
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class WeakReference<T> : ISerializable where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object.</summary>
		/// <param name="target">The object to reference, or <see langword="null" />.</param>
		// Token: 0x060015BE RID: 5566 RVA: 0x000402DB File Offset: 0x0003E4DB
		[__DynamicallyInvokable]
		public WeakReference(T target)
			: this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object and uses the specified resurrection tracking.</summary>
		/// <param name="target">The object to reference, or <see langword="null" />.</param>
		/// <param name="trackResurrection">
		///   <see langword="true" /> to track the object after finalization; <see langword="false" /> to track the object only until finalization.</param>
		// Token: 0x060015BF RID: 5567 RVA: 0x000402E5 File Offset: 0x0003E4E5
		[__DynamicallyInvokable]
		public WeakReference(T target, bool trackResurrection)
		{
			this.Create(target, trackResurrection);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000402F8 File Offset: 0x0003E4F8
		internal WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			T t = (T)((object)info.GetValue("TrackedObject", typeof(T)));
			bool boolean = info.GetBoolean("TrackResurrection");
			this.Create(t, boolean);
		}

		/// <summary>Tries to retrieve the target object that is referenced by the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="target">When this method returns, contains the target object, if it is available. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the target was retrieved; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015C1 RID: 5569 RVA: 0x00040348 File Offset: 0x0003E548
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryGetTarget(out T target)
		{
			T target2 = this.Target;
			target = target2;
			return target2 != null;
		}

		/// <summary>Sets the target object that is referenced by this <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="target">The new target object.</param>
		// Token: 0x060015C2 RID: 5570 RVA: 0x0004036C File Offset: 0x0003E56C
		[__DynamicallyInvokable]
		public void SetTarget(T target)
		{
			this.Target = target;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060015C3 RID: 5571
		// (set) Token: 0x060015C4 RID: 5572
		private extern T Target
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		/// <summary>Discards the reference to the target that is represented by the current <see cref="T:System.WeakReference`1" /> object.</summary>
		// Token: 0x060015C5 RID: 5573
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern void Finalize();

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data necessary to serialize the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="info">An object that holds all the data necessary to serialize or deserialize the current <see cref="T:System.WeakReference`1" /> object.</param>
		/// <param name="context">The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060015C6 RID: 5574 RVA: 0x00040378 File Offset: 0x0003E578
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackedObject", this.Target, typeof(T));
			info.AddValue("TrackResurrection", this.IsTrackResurrection());
		}

		// Token: 0x060015C7 RID: 5575
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Create(T target, bool trackResurrection);

		// Token: 0x060015C8 RID: 5576
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTrackResurrection();

		// Token: 0x04000749 RID: 1865
		internal IntPtr m_handle;
	}
}

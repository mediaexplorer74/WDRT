using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
	/// <summary>Represents a weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	// Token: 0x0200015D RID: 349
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[Serializable]
	public class WeakReference : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, referencing the specified object.</summary>
		/// <param name="target">The object to track or <see langword="null" />.</param>
		// Token: 0x060015B3 RID: 5555 RVA: 0x0004022F File Offset: 0x0003E42F
		[__DynamicallyInvokable]
		public WeakReference(object target)
			: this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, referencing the specified object and using the specified resurrection tracking.</summary>
		/// <param name="target">An object to track.</param>
		/// <param name="trackResurrection">Indicates when to stop tracking the object. If <see langword="true" />, the object is tracked after finalization; if <see langword="false" />, the object is only tracked until finalization.</param>
		// Token: 0x060015B4 RID: 5556 RVA: 0x00040239 File Offset: 0x0003E439
		[__DynamicallyInvokable]
		public WeakReference(object target, bool trackResurrection)
		{
			this.Create(target, trackResurrection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, using deserialized data from the specified serialization and stream objects.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference" /> object.</param>
		/// <param name="context">(Reserved) Describes the source and destination of the serialized stream specified by <paramref name="info" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060015B5 RID: 5557 RVA: 0x0004024C File Offset: 0x0003E44C
		protected WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object value = info.GetValue("TrackedObject", typeof(object));
			bool boolean = info.GetBoolean("TrackResurrection");
			this.Create(value, boolean);
		}

		/// <summary>Gets an indication whether the object referenced by the current <see cref="T:System.WeakReference" /> object has been garbage collected.</summary>
		/// <returns>
		///   <see langword="true" /> if the object referenced by the current <see cref="T:System.WeakReference" /> object has not been garbage collected and is still accessible; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060015B6 RID: 5558
		[__DynamicallyInvokable]
		public virtual extern bool IsAlive
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets an indication whether the object referenced by the current <see cref="T:System.WeakReference" /> object is tracked after it is finalized.</summary>
		/// <returns>
		///   <see langword="true" /> if the object the current <see cref="T:System.WeakReference" /> object refers to is tracked after finalization; or <see langword="false" /> if the object is only tracked until finalization.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00040297 File Offset: 0x0003E497
		[__DynamicallyInvokable]
		public virtual bool TrackResurrection
		{
			[__DynamicallyInvokable]
			get
			{
				return this.IsTrackResurrection();
			}
		}

		/// <summary>Gets or sets the object (the target) referenced by the current <see cref="T:System.WeakReference" /> object.</summary>
		/// <returns>
		///   <see langword="null" /> if the object referenced by the current <see cref="T:System.WeakReference" /> object has been garbage collected; otherwise, a reference to the object referenced by the current <see cref="T:System.WeakReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reference to the target object is invalid. This exception can be thrown while setting this property if the value is a null reference or if the object has been finalized during the set operation.</exception>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060015B8 RID: 5560
		// (set) Token: 0x060015B9 RID: 5561
		[__DynamicallyInvokable]
		public virtual extern object Target
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		/// <summary>Discards the reference to the target represented by the current <see cref="T:System.WeakReference" /> object.</summary>
		// Token: 0x060015BA RID: 5562
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern void Finalize();

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data needed to serialize the current <see cref="T:System.WeakReference" /> object.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference" /> object.</param>
		/// <param name="context">(Reserved) The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060015BB RID: 5563 RVA: 0x0004029F File Offset: 0x0003E49F
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackedObject", this.Target, typeof(object));
			info.AddValue("TrackResurrection", this.IsTrackResurrection());
		}

		// Token: 0x060015BC RID: 5564
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Create(object target, bool trackResurrection);

		// Token: 0x060015BD RID: 5565
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTrackResurrection();

		// Token: 0x04000748 RID: 1864
		internal IntPtr m_handle;
	}
}

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Management
{
	/// <summary>Represents different collections of management objects retrieved through WMI. The objects in this collection are of <see cref="T:System.Management.ManagementBaseObject" />-derived types, including <see cref="T:System.Management.ManagementObject" /> and <see cref="T:System.Management.ManagementClass" />. The collection can be the result of a WMI query executed through a <see cref="T:System.Management.ManagementObjectSearcher" />, or an enumeration of management objects of a specified type retrieved through a <see cref="T:System.Management.ManagementClass" /> representing that type. In addition, this can be a collection of management objects related in a specified way to a specific management object - in this case the collection would be retrieved through a method such as <see cref="M:System.Management.ManagementObject.GetRelated" />. The collection can be walked using the <see cref="T:System.Management.ManagementObjectCollection.ManagementObjectEnumerator" /> and objects in it can be inspected or manipulated for various management tasks.</summary>
	// Token: 0x0200001F RID: 31
	public class ManagementObjectCollection : ICollection, IEnumerable, IDisposable
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000070A0 File Offset: 0x000052A0
		internal ManagementObjectCollection(ManagementScope scope, EnumerationOptions options, IEnumWbemClassObject enumWbem)
		{
			if (options != null)
			{
				this.options = (EnumerationOptions)options.Clone();
			}
			else
			{
				this.options = new EnumerationOptions();
			}
			if (scope != null)
			{
				this.scope = scope.Clone();
			}
			else
			{
				this.scope = ManagementScope._Clone(null);
			}
			this.enumWbem = enumWbem;
		}

		/// <summary>Disposes of resources the object is holding. This is the destructor for the object. Finalizers are expressed using destructor syntax.</summary>
		// Token: 0x060000F5 RID: 245 RVA: 0x000070F8 File Offset: 0x000052F8
		~ManagementObjectCollection()
		{
			this.Dispose(false);
		}

		/// <summary>Releases resources associated with this object. After this method has been called, an attempt to use this object will result in an <see cref="T:System.ObjectDisposedException" /> being thrown.</summary>
		// Token: 0x060000F6 RID: 246 RVA: 0x00007128 File Offset: 0x00005328
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.Dispose(true);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007139 File Offset: 0x00005339
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
				this.isDisposed = true;
			}
			Marshal.ReleaseComObject(this.enumWbem);
		}

		/// <summary>Gets a value indicating the number of objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00007158 File Offset: 0x00005358
		public int Count
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException(ManagementObjectCollection.name);
				}
				int num = 0;
				IEnumerator enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets a value that indicates whether the object is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> if the object is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007190 File Offset: 0x00005390
		public bool IsSynchronized
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException(ManagementObjectCollection.name);
				}
				return false;
			}
		}

		/// <summary>Gets the object to be used for synchronization.</summary>
		/// <returns>An object that can be used for synchronization.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000071A6 File Offset: 0x000053A6
		public object SyncRoot
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException(ManagementObjectCollection.name);
				}
				return this;
			}
		}

		/// <summary>Copies the collection to an array.</summary>
		/// <param name="array">An array to copy to.</param>
		/// <param name="index">The index to start from.</param>
		// Token: 0x060000FB RID: 251 RVA: 0x000071BC File Offset: 0x000053BC
		public void CopyTo(Array array, int index)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(ManagementObjectCollection.name);
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < array.GetLowerBound(0) || index > array.GetUpperBound(0))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = array.Length - index;
			int num2 = 0;
			ArrayList arrayList = new ArrayList();
			foreach (ManagementBaseObject managementBaseObject in this)
			{
				arrayList.Add(managementBaseObject);
				num2++;
				if (num2 > num)
				{
					throw new ArgumentException(null, "index");
				}
			}
			arrayList.CopyTo(array, index);
		}

		/// <summary>Copies the items in the collection to a <see cref="T:System.Management.ManagementBaseObject" /> array.</summary>
		/// <param name="objectCollection">The target array.</param>
		/// <param name="index">The index to start from.</param>
		// Token: 0x060000FC RID: 252 RVA: 0x00007258 File Offset: 0x00005458
		public void CopyTo(ManagementBaseObject[] objectCollection, int index)
		{
			this.CopyTo(objectCollection, index);
		}

		/// <summary>Returns the enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060000FD RID: 253 RVA: 0x00007264 File Offset: 0x00005464
		public ManagementObjectCollection.ManagementObjectEnumerator GetEnumerator()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(ManagementObjectCollection.name);
			}
			if (this.options.Rewindable)
			{
				IEnumWbemClassObject enumWbemClassObject = null;
				int num = 0;
				try
				{
					num = this.scope.GetSecuredIEnumWbemClassObjectHandler(this.enumWbem).Clone_(ref enumWbemClassObject);
					if (((long)num & (long)((ulong)(-2147483648))) == 0L)
					{
						num = this.scope.GetSecuredIEnumWbemClassObjectHandler(enumWbemClassObject).Reset_();
					}
				}
				catch (COMException ex)
				{
					ManagementException.ThrowWithExtendedInfo(ex);
				}
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
				}
				else if (((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
				}
				return new ManagementObjectCollection.ManagementObjectEnumerator(this, enumWbemClassObject);
			}
			return new ManagementObjectCollection.ManagementObjectEnumerator(this, this.enumWbem);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Management.ManagementObjectCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Management.ManagementObjectCollection" />.</returns>
		// Token: 0x060000FE RID: 254 RVA: 0x00007334 File Offset: 0x00005534
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400010F RID: 271
		private static readonly string name = typeof(ManagementObjectCollection).FullName;

		// Token: 0x04000110 RID: 272
		internal ManagementScope scope;

		// Token: 0x04000111 RID: 273
		internal EnumerationOptions options;

		// Token: 0x04000112 RID: 274
		private IEnumWbemClassObject enumWbem;

		// Token: 0x04000113 RID: 275
		private bool isDisposed;

		/// <summary>Represents the enumerator on the collection.</summary>
		// Token: 0x020000C7 RID: 199
		public class ManagementObjectEnumerator : IEnumerator, IDisposable
		{
			// Token: 0x0600057F RID: 1407 RVA: 0x00026DD0 File Offset: 0x00024FD0
			internal ManagementObjectEnumerator(ManagementObjectCollection collectionObject, IEnumWbemClassObject enumWbem)
			{
				this.enumWbem = enumWbem;
				this.collectionObject = collectionObject;
				this.cachedObjects = new IWbemClassObjectFreeThreaded[collectionObject.options.BlockSize];
				this.cachedCount = 0U;
				this.cacheIndex = -1;
				this.atEndOfCollection = false;
			}

			/// <summary>Disposes of resources the object is holding. This is the destructor for the object. Finalizers are expressed using destructor syntax.</summary>
			// Token: 0x06000580 RID: 1408 RVA: 0x00026E1C File Offset: 0x0002501C
			~ManagementObjectEnumerator()
			{
				this.Dispose();
			}

			/// <summary>Releases resources associated with this object. After this method has been called, an attempt to use this object will result in an <see cref="T:System.ObjectDisposedException" /> exception being thrown.</summary>
			// Token: 0x06000581 RID: 1409 RVA: 0x00026E48 File Offset: 0x00025048
			public void Dispose()
			{
				if (!this.isDisposed)
				{
					if (this.enumWbem != null)
					{
						Marshal.ReleaseComObject(this.enumWbem);
						this.enumWbem = null;
					}
					this.cachedObjects = null;
					this.collectionObject = null;
					this.isDisposed = true;
					GC.SuppressFinalize(this);
				}
			}

			/// <summary>Gets the current <see cref="T:System.Management.ManagementBaseObject" /> that this enumerator points to.</summary>
			/// <returns>The current object in the enumeration.</returns>
			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x00026E88 File Offset: 0x00025088
			public ManagementBaseObject Current
			{
				get
				{
					if (this.isDisposed)
					{
						throw new ObjectDisposedException(ManagementObjectCollection.ManagementObjectEnumerator.name);
					}
					if (this.cacheIndex < 0)
					{
						throw new InvalidOperationException();
					}
					return ManagementBaseObject.GetBaseObject(this.cachedObjects[this.cacheIndex], this.collectionObject.scope);
				}
			}

			/// <summary>Gets the current object in the collection.</summary>
			/// <returns>The current element in the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x00026ED4 File Offset: 0x000250D4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Indicates whether the enumerator has moved to the next object in the enumeration.</summary>
			/// <returns>
			///   <see langword="true" />, if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000584 RID: 1412 RVA: 0x00026EDC File Offset: 0x000250DC
			public bool MoveNext()
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException(ManagementObjectCollection.ManagementObjectEnumerator.name);
				}
				if (this.atEndOfCollection)
				{
					return false;
				}
				this.cacheIndex++;
				if ((ulong)this.cachedCount - (ulong)((long)this.cacheIndex) == 0UL)
				{
					int num = ((this.collectionObject.options.Timeout.Ticks == long.MaxValue) ? (-1) : ((int)this.collectionObject.options.Timeout.TotalMilliseconds));
					SecurityHandler securityHandler = this.collectionObject.scope.GetSecurityHandler();
					IWbemClassObject_DoNotMarshal[] array = new IWbemClassObject_DoNotMarshal[this.collectionObject.options.BlockSize];
					int num2 = this.collectionObject.scope.GetSecuredIEnumWbemClassObjectHandler(this.enumWbem).Next_(num, (uint)this.collectionObject.options.BlockSize, array, ref this.cachedCount);
					securityHandler.Reset();
					if (num2 >= 0)
					{
						int num3 = 0;
						while ((long)num3 < (long)((ulong)this.cachedCount))
						{
							this.cachedObjects[num3] = new IWbemClassObjectFreeThreaded(Marshal.GetIUnknownForObject(array[num3]));
							num3++;
						}
					}
					if (num2 < 0)
					{
						if (((long)num2 & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
						{
							ManagementException.ThrowWithExtendedInfo((ManagementStatus)num2);
						}
						else
						{
							Marshal.ThrowExceptionForHR(num2, WmiNetUtilsHelper.GetErrorInfo_f());
						}
					}
					else
					{
						if (num2 == 262148 && this.cachedCount == 0U)
						{
							ManagementException.ThrowWithExtendedInfo((ManagementStatus)num2);
						}
						if (num2 == 1 && this.cachedCount == 0U)
						{
							this.atEndOfCollection = true;
							this.cacheIndex--;
							return false;
						}
					}
					this.cacheIndex = 0;
				}
				return true;
			}

			/// <summary>Resets the enumerator to the beginning of the collection.</summary>
			// Token: 0x06000585 RID: 1413 RVA: 0x00027070 File Offset: 0x00025270
			public void Reset()
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException(ManagementObjectCollection.ManagementObjectEnumerator.name);
				}
				if (!this.collectionObject.options.Rewindable)
				{
					throw new InvalidOperationException();
				}
				SecurityHandler securityHandler = this.collectionObject.scope.GetSecurityHandler();
				int num = 0;
				try
				{
					num = this.collectionObject.scope.GetSecuredIEnumWbemClassObjectHandler(this.enumWbem).Reset_();
				}
				catch (COMException ex)
				{
					ManagementException.ThrowWithExtendedInfo(ex);
				}
				finally
				{
					securityHandler.Reset();
				}
				if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
				{
					ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
				}
				else if (((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
				}
				int num2 = ((this.cacheIndex >= 0) ? this.cacheIndex : 0);
				while ((long)num2 < (long)((ulong)this.cachedCount))
				{
					Marshal.ReleaseComObject((IWbemClassObject_DoNotMarshal)Marshal.GetObjectForIUnknown(this.cachedObjects[num2]));
					num2++;
				}
				this.cachedCount = 0U;
				this.cacheIndex = -1;
				this.atEndOfCollection = false;
			}

			// Token: 0x0400053B RID: 1339
			private static readonly string name = typeof(ManagementObjectCollection.ManagementObjectEnumerator).FullName;

			// Token: 0x0400053C RID: 1340
			private IEnumWbemClassObject enumWbem;

			// Token: 0x0400053D RID: 1341
			private ManagementObjectCollection collectionObject;

			// Token: 0x0400053E RID: 1342
			private uint cachedCount;

			// Token: 0x0400053F RID: 1343
			private int cacheIndex;

			// Token: 0x04000540 RID: 1344
			private IWbemClassObjectFreeThreaded[] cachedObjects;

			// Token: 0x04000541 RID: 1345
			private bool atEndOfCollection;

			// Token: 0x04000542 RID: 1346
			private bool isDisposed;
		}
	}
}

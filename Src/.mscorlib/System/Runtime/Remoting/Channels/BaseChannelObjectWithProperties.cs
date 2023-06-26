using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation of a channel object that exposes a dictionary interface to its properties.</summary>
	// Token: 0x0200084F RID: 2127
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x0013EFD9 File Offset: 0x0013D1D9
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return this;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the property that is associated with the specified key.</summary>
		/// <param name="key">The key of the property to get or set.</param>
		/// <returns>The property that is associated with the specified key.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is accessed.</exception>
		// Token: 0x17000F06 RID: 3846
		public virtual object this[object key]
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
			[SecuritySafeCritical]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Collections.ICollection" /> of keys that the channel object properties are associated with.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of keys that the channel object properties are associated with.</returns>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0013EFE6 File Offset: 0x0013D1E6
		public virtual ICollection Keys
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> of the values of the properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of the values of the properties associated with the channel object.</returns>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x0013EFEC File Offset: 0x0013D1EC
		public virtual ICollection Values
		{
			[SecuritySafeCritical]
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in keys)
				{
					arrayList.Add(this[obj]);
				}
				return arrayList;
			}
		}

		/// <summary>Returns a value that indicates whether the channel object contains a property that is associated with the specified key.</summary>
		/// <param name="key">The key of the property to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the channel object contains a property associated with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005A59 RID: 23129 RVA: 0x0013F058 File Offset: 0x0013D258
		[SecuritySafeCritical]
		public virtual bool Contains(object key)
		{
			if (key == null)
			{
				return false;
			}
			ICollection keys = this.Keys;
			if (keys == null)
			{
				return false;
			}
			string text = key as string;
			foreach (object obj in keys)
			{
				if (text != null)
				{
					string text2 = obj as string;
					if (text2 != null)
					{
						if (string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						continue;
					}
				}
				if (key.Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a value that indicates whether the collection of properties in the channel object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection of properties in the channel object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x0013F0EC File Offset: 0x0013D2EC
		public virtual bool IsReadOnly
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the number of properties that can be entered into the channel object is fixed.</summary>
		/// <returns>
		///   <see langword="true" /> if the number of properties that can be entered into the channel object is fixed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0013F0EF File Offset: 0x0013D2EF
		public virtual bool IsFixedSize
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="key">The key that is associated with the object in the <paramref name="value" /> parameter.</param>
		/// <param name="value">The value to add.</param>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06005A5C RID: 23132 RVA: 0x0013F0F2 File Offset: 0x0013D2F2
		[SecuritySafeCritical]
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06005A5D RID: 23133 RVA: 0x0013F0F9 File Offset: 0x0013D2F9
		[SecuritySafeCritical]
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="key">The key of the object to be removed.</param>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06005A5E RID: 23134 RVA: 0x0013F100 File Offset: 0x0013D300
		[SecuritySafeCritical]
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Collections.IDictionaryEnumerator" /> that enumerates over all the properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionaryEnumerator" /> that enumerates over all the properties associated with the channel object.</returns>
		// Token: 0x06005A5F RID: 23135 RVA: 0x0013F107 File Offset: 0x0013D307
		[SecuritySafeCritical]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="array">The array to copy the properties to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06005A60 RID: 23136 RVA: 0x0013F10F File Offset: 0x0013D30F
		[SecuritySafeCritical]
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the number of properties associated with the channel object.</summary>
		/// <returns>The number of properties associated with the channel object.</returns>
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0013F118 File Offset: 0x0013D318
		public virtual int Count
		{
			[SecuritySafeCritical]
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return 0;
				}
				return keys.Count;
			}
		}

		/// <summary>Gets an object that is used to synchronize access to the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.</summary>
		/// <returns>An object that is used to synchronize access to the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0013F137 File Offset: 0x0013D337
		public virtual object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary of channel object properties is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the dictionary of channel object properties is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0013F13A File Offset: 0x0013D33A
		public virtual bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> that enumerates over all the properties that are associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> that enumerates over all the properties that are associated with the channel object.</returns>
		// Token: 0x06005A64 RID: 23140 RVA: 0x0013F13D File Offset: 0x0013D33D
		[SecuritySafeCritical]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}
	}
}

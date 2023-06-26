using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	// Token: 0x020000AA RID: 170
	public class SettingsPropertyCollection : IEnumerable, ICloneable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</summary>
		// Token: 0x060005CB RID: 1483 RVA: 0x00023128 File Offset: 0x00021328
		public SettingsPropertyCollection()
		{
			this._Hashtable = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsProperty" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060005CC RID: 1484 RVA: 0x00023144 File Offset: 0x00021344
		public void Add(SettingsProperty property)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnAdd(property);
			this._Hashtable.Add(property.Name, property);
			try
			{
				this.OnAddComplete(property);
			}
			catch
			{
				this._Hashtable.Remove(property.Name);
				throw;
			}
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsProperty" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060005CD RID: 1485 RVA: 0x000231A8 File Offset: 0x000213A8
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			SettingsProperty settingsProperty = (SettingsProperty)this._Hashtable[name];
			if (settingsProperty == null)
			{
				return;
			}
			this.OnRemove(settingsProperty);
			this._Hashtable.Remove(name);
			try
			{
				this.OnRemoveComplete(settingsProperty);
			}
			catch
			{
				this._Hashtable.Add(name, settingsProperty);
				throw;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object with the specified <paramref name="name" />.</returns>
		// Token: 0x170000EB RID: 235
		public SettingsProperty this[string name]
		{
			get
			{
				return this._Hashtable[name] as SettingsProperty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		// Token: 0x060005CF RID: 1487 RVA: 0x0002322B File Offset: 0x0002142B
		public IEnumerator GetEnumerator()
		{
			return this._Hashtable.Values.GetEnumerator();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</returns>
		// Token: 0x060005D0 RID: 1488 RVA: 0x0002323D File Offset: 0x0002143D
		public object Clone()
		{
			return new SettingsPropertyCollection(this._Hashtable);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		// Token: 0x060005D1 RID: 1489 RVA: 0x0002324A File Offset: 0x0002144A
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsProperty" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060005D2 RID: 1490 RVA: 0x0002325C File Offset: 0x0002145C
		public void Clear()
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnClear();
			this._Hashtable.Clear();
			this.OnClearComplete();
		}

		/// <summary>Performs additional, custom processing when adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005D3 RID: 1491 RVA: 0x00023283 File Offset: 0x00021483
		protected virtual void OnAdd(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005D4 RID: 1492 RVA: 0x00023285 File Offset: 0x00021485
		protected virtual void OnAddComplete(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing when clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x060005D5 RID: 1493 RVA: 0x00023287 File Offset: 0x00021487
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional, custom processing after clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x060005D6 RID: 1494 RVA: 0x00023289 File Offset: 0x00021489
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional, custom processing when removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005D7 RID: 1495 RVA: 0x0002328B File Offset: 0x0002148B
		protected virtual void OnRemove(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x060005D8 RID: 1496 RVA: 0x0002328D File Offset: 0x0002148D
		protected virtual void OnRemoveComplete(SettingsProperty property)
		{
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0002328F File Offset: 0x0002148F
		public int Count
		{
			get
			{
				return this._Hashtable.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.SettingsPropertyCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0002329C File Offset: 0x0002149C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0002329F File Offset: 0x0002149F
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		// Token: 0x060005DC RID: 1500 RVA: 0x000232A2 File Offset: 0x000214A2
		public void CopyTo(Array array, int index)
		{
			this._Hashtable.Values.CopyTo(array, index);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000232B6 File Offset: 0x000214B6
		private SettingsPropertyCollection(Hashtable h)
		{
			this._Hashtable = (Hashtable)h.Clone();
		}

		// Token: 0x04000C4C RID: 3148
		private Hashtable _Hashtable;

		// Token: 0x04000C4D RID: 3149
		private bool _ReadOnly;
	}
}

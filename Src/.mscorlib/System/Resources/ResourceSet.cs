using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
	/// <summary>Stores all the resources localized for one particular culture, ignoring all other cultures, including any fallback rules.</summary>
	// Token: 0x02000398 RID: 920
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceSet" /> class with default properties.</summary>
		// Token: 0x06002D81 RID: 11649 RVA: 0x000AED45 File Offset: 0x000ACF45
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000AED53 File Offset: 0x000ACF53
		internal ResourceSet(bool junk)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the system default <see cref="T:System.Resources.ResourceReader" /> that opens and reads resources from the given file.</summary>
		/// <param name="fileName">Resource file to read.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D83 RID: 11651 RVA: 0x000AED5B File Offset: 0x000ACF5B
		public ResourceSet(string fileName)
		{
			this.Reader = new ResourceReader(fileName);
			this.CommonInit();
			this.ReadResources();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the system default <see cref="T:System.Resources.ResourceReader" /> that reads resources from the given stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> of resources to be read. The stream should refer to an existing resources file.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is not readable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D84 RID: 11652 RVA: 0x000AED7B File Offset: 0x000ACF7B
		[SecurityCritical]
		public ResourceSet(Stream stream)
		{
			this.Reader = new ResourceReader(stream);
			this.CommonInit();
			this.ReadResources();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the specified resource reader.</summary>
		/// <param name="reader">The reader that will be used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="reader" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D85 RID: 11653 RVA: 0x000AED9B File Offset: 0x000ACF9B
		public ResourceSet(IResourceReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000AEDC4 File Offset: 0x000ACFC4
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		/// <summary>Closes and releases any resources used by this <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x06002D87 RID: 11655 RVA: 0x000AEDD1 File Offset: 0x000ACFD1
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases resources (other than memory) associated with the current instance, closing internal managed objects if requested.</summary>
		/// <param name="disposing">Indicates whether the objects contained in the current instance should be explicitly closed.</param>
		// Token: 0x06002D88 RID: 11656 RVA: 0x000AEDDC File Offset: 0x000ACFDC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		/// <summary>Disposes of the resources (other than memory) used by the current instance of <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x06002D89 RID: 11657 RVA: 0x000AEE18 File Offset: 0x000AD018
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Returns the preferred resource reader class for this kind of <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> for the preferred resource reader for this kind of <see cref="T:System.Resources.ResourceSet" />.</returns>
		// Token: 0x06002D8A RID: 11658 RVA: 0x000AEE21 File Offset: 0x000AD021
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		/// <summary>Returns the preferred resource writer class for this kind of <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> for the preferred resource writer for this kind of <see cref="T:System.Resources.ResourceSet" />.</returns>
		// Token: 0x06002D8B RID: 11659 RVA: 0x000AEE2D File Offset: 0x000AD02D
		public virtual Type GetDefaultWriter()
		{
			return typeof(ResourceWriter);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through the <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for this <see cref="T:System.Resources.ResourceSet" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The resource set has been closed or disposed.</exception>
		// Token: 0x06002D8C RID: 11660 RVA: 0x000AEE39 File Offset: 0x000AD039
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to avoid a race condition with <see langword="Dispose" />. This member is not intended to be used directly from your code.</summary>
		/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceSet" /> object.</returns>
		// Token: 0x06002D8D RID: 11661 RVA: 0x000AEE41 File Offset: 0x000AD041
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000AEE4C File Offset: 0x000AD04C
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return table.GetEnumerator();
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name.</summary>
		/// <param name="name">Name of the resource to search for.</param>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002D8F RID: 11663 RVA: 0x000AEE7C File Offset: 0x000AD07C
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[] { name }));
			}
			return text;
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name in a case-insensitive manner, if requested.</summary>
		/// <param name="name">Name of the resource to search for.</param>
		/// <param name="ignoreCase">Indicates whether the case of the case of the specified name should be ignored.</param>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002D90 RID: 11664 RVA: 0x000AEEC8 File Offset: 0x000AD0C8
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[] { name }));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string text2;
			try
			{
				text2 = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[] { name }));
			}
			return text2;
		}

		/// <summary>Searches for a resource object with the specified name.</summary>
		/// <param name="name">Case-sensitive name of the resource to search for.</param>
		/// <returns>The requested resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002D91 RID: 11665 RVA: 0x000AEF54 File Offset: 0x000AD154
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		/// <summary>Searches for a resource object with the specified name in a case-insensitive manner, if requested.</summary>
		/// <param name="name">Name of the resource to search for.</param>
		/// <param name="ignoreCase">Indicates whether the case of the specified name should be ignored.</param>
		/// <returns>The requested resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002D92 RID: 11666 RVA: 0x000AEF60 File Offset: 0x000AD160
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		/// <summary>Reads all the resources and stores them in a <see cref="T:System.Collections.Hashtable" /> indicated in the <see cref="F:System.Resources.ResourceSet.Table" /> property.</summary>
		// Token: 0x06002D93 RID: 11667 RVA: 0x000AEF84 File Offset: 0x000AD184
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000AEFC0 File Offset: 0x000AD1C0
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return table[name];
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000AF000 File Offset: 0x000AD200
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		/// <summary>Indicates the <see cref="T:System.Resources.IResourceReader" /> used to read the resources.</summary>
		// Token: 0x04001273 RID: 4723
		[NonSerialized]
		protected IResourceReader Reader;

		/// <summary>The <see cref="T:System.Collections.Hashtable" /> in which the resources are stored.</summary>
		// Token: 0x04001274 RID: 4724
		protected Hashtable Table;

		// Token: 0x04001275 RID: 4725
		private Hashtable _caseInsensitiveTable;
	}
}

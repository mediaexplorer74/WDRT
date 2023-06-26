using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	/// <summary>Represents the collection used to store Uniform Resource Identifier (URI) prefixes for <see cref="T:System.Net.HttpListener" /> objects.</summary>
	// Token: 0x020000FB RID: 251
	public class HttpListenerPrefixCollection : ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x00032ADC File Offset: 0x00030CDC
		internal HttpListenerPrefixCollection(HttpListener listener)
		{
			this.m_HttpListener = listener;
		}

		/// <summary>Copies the contents of an <see cref="T:System.Net.HttpListenerPrefixCollection" /> to the specified array.</summary>
		/// <param name="array">The one dimensional <see cref="T:System.Array" /> that receives the Uniform Resource Identifier (URI) prefix strings in this collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> has more than one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">This collection contains more elements than can be stored in <paramref name="array" /> starting at <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="array" /> cannot store string values.</exception>
		// Token: 0x06000900 RID: 2304 RVA: 0x00032AEC File Offset: 0x00030CEC
		public void CopyTo(Array array, int offset)
		{
			this.m_HttpListener.CheckDisposed();
			if (this.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("array", SR.GetString("net_array_too_small"));
			}
			if (offset + this.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			int num = 0;
			foreach (object obj in this.m_HttpListener.m_UriPrefixes.Keys)
			{
				string text = (string)obj;
				array.SetValue(text, offset + num++);
			}
		}

		/// <summary>Copies the contents of an <see cref="T:System.Net.HttpListenerPrefixCollection" /> to the specified string array.</summary>
		/// <param name="array">The one dimensional string array that receives the Uniform Resource Identifier (URI) prefix strings in this collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> has more than one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">This collection contains more elements than can be stored in <paramref name="array" /> starting at <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		// Token: 0x06000901 RID: 2305 RVA: 0x00032BA4 File Offset: 0x00030DA4
		public void CopyTo(string[] array, int offset)
		{
			this.m_HttpListener.CheckDisposed();
			if (this.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("array", SR.GetString("net_array_too_small"));
			}
			if (offset + this.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			int num = 0;
			foreach (object obj in this.m_HttpListener.m_UriPrefixes.Keys)
			{
				string text = (string)obj;
				array[offset + num++] = text;
			}
		}

		/// <summary>Gets the number of prefixes contained in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of prefixes in this collection.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00032C50 File Offset: 0x00030E50
		public int Count
		{
			get
			{
				return this.m_HttpListener.m_UriPrefixes.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00032C62 File Offset: 0x00030E62
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is read-only.</summary>
		/// <returns>Always returns <see langword="false" />.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00032C65 File Offset: 0x00030E65
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) prefix to the collection.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that identifies the URI information that is compared in incoming requests. The prefix must be terminated with a forward slash ("/").</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uriPrefix" /> does not use the http:// or https:// scheme. These are the only schemes supported for <see cref="T:System.Net.HttpListener" /> objects.  
		/// -or-  
		/// <paramref name="uriPrefix" /> is not a correctly formatted URI prefix. Make sure the string is terminated with a "/".</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception. This exception is thrown if another <see cref="T:System.Net.HttpListener" /> has already added the prefix <paramref name="uriPrefix" />.</exception>
		// Token: 0x06000905 RID: 2309 RVA: 0x00032C68 File Offset: 0x00030E68
		public void Add(string uriPrefix)
		{
			this.m_HttpListener.AddPrefix(uriPrefix);
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value that indicates whether the specified prefix is contained in the collection.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that contains the Uniform Resource Identifier (URI) prefix to test.</param>
		/// <returns>
		///   <see langword="true" /> if this collection contains the prefix specified by <paramref name="uriPrefix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is <see langword="null" />.</exception>
		// Token: 0x06000906 RID: 2310 RVA: 0x00032C76 File Offset: 0x00030E76
		public bool Contains(string uriPrefix)
		{
			return this.m_HttpListener.m_UriPrefixes.Contains(uriPrefix);
		}

		/// <summary>Returns an object that can be used to iterate through the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the strings in this collection.</returns>
		// Token: 0x06000907 RID: 2311 RVA: 0x00032C89 File Offset: 0x00030E89
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the strings in this collection.</returns>
		// Token: 0x06000908 RID: 2312 RVA: 0x00032C91 File Offset: 0x00030E91
		public IEnumerator<string> GetEnumerator()
		{
			return new ListenerPrefixEnumerator(this.m_HttpListener.m_UriPrefixes.Keys.GetEnumerator());
		}

		/// <summary>Removes the specified Uniform Resource Identifier (URI) from the list of prefixes handled by the <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that contains the URI prefix to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="uriPrefix" /> was found in the <see cref="T:System.Net.HttpListenerPrefixCollection" /> and removed; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. To determine the cause of the exception, check the exception's error code.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		// Token: 0x06000909 RID: 2313 RVA: 0x00032CAD File Offset: 0x00030EAD
		public bool Remove(string uriPrefix)
		{
			return this.m_HttpListener.RemovePrefix(uriPrefix);
		}

		/// <summary>Removes all the Uniform Resource Identifier (URI) prefixes from the collection.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		// Token: 0x0600090A RID: 2314 RVA: 0x00032CBB File Offset: 0x00030EBB
		public void Clear()
		{
			this.m_HttpListener.RemoveAll(true);
		}

		// Token: 0x04000E05 RID: 3589
		private HttpListener m_HttpListener;
	}
}

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides a response from a Uniform Resource Identifier (URI). This is an <see langword="abstract" /> class.</summary>
	// Token: 0x0200018B RID: 395
	[global::__DynamicallyInvokable]
	[Serializable]
	public abstract class WebResponse : MarshalByRefObject, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class.</summary>
		// Token: 0x06000F13 RID: 3859 RVA: 0x0004E615 File Offset: 0x0004C815
		[global::__DynamicallyInvokable]
		protected WebResponse()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">An instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that indicates the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class.</exception>
		// Token: 0x06000F14 RID: 3860 RVA: 0x0004E61D File Offset: 0x0004C81D
		protected WebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize <see cref="T:System.Net.WebResponse" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that will hold the serialized data for the <see cref="T:System.Net.WebResponse" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.WebResponse" />.</param>
		// Token: 0x06000F15 RID: 3861 RVA: 0x0004E625 File Offset: 0x0004C825
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000F16 RID: 3862 RVA: 0x0004E62F File Offset: 0x0004C82F
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>When overridden by a descendant class, closes the response stream.</summary>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000F17 RID: 3863 RVA: 0x0004E631 File Offset: 0x0004C831
		public virtual void Close()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebResponse" /> object.</summary>
		// Token: 0x06000F18 RID: 3864 RVA: 0x0004E633 File Offset: 0x0004C833
		[global::__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebResponse" /> object, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x06000F19 RID: 3865 RVA: 0x0004E644 File Offset: 0x0004C844
		[global::__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			try
			{
				this.Close();
			}
			catch
			{
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this response was obtained from the cache.</summary>
		/// <returns>
		///   <see langword="true" /> if the response was taken from the cache; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0004E674 File Offset: 0x0004C874
		public virtual bool IsFromCache
		{
			get
			{
				return this.m_IsFromCache;
			}
		}

		// Token: 0x17000357 RID: 855
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0004E67C File Offset: 0x0004C87C
		internal bool InternalSetFromCache
		{
			set
			{
				this.m_IsFromCache = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0004E685 File Offset: 0x0004C885
		internal virtual bool IsCacheFresh
		{
			get
			{
				return this.m_IsCacheFresh;
			}
		}

		// Token: 0x17000359 RID: 857
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0004E68D File Offset: 0x0004C88D
		internal bool InternalSetIsCacheFresh
		{
			set
			{
				this.m_IsCacheFresh = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>
		///   <see langword="true" /> if both client and server were authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0004E696 File Offset: 0x0004C896
		public virtual bool IsMutuallyAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of data being received.</summary>
		/// <returns>The number of bytes returned from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0004E699 File Offset: 0x0004C899
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0004E6A0 File Offset: 0x0004C8A0
		[global::__DynamicallyInvokable]
		public virtual long ContentLength
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the content type of the data being received.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0004E6A7 File Offset: 0x0004C8A7
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0004E6AE File Offset: 0x0004C8AE
		[global::__DynamicallyInvokable]
		public virtual string ContentType
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, returns the data stream from the Internet resource.</summary>
		/// <returns>An instance of the <see cref="T:System.IO.Stream" /> class for reading data from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000F23 RID: 3875 RVA: 0x0004E6B5 File Offset: 0x0004C8B5
		[global::__DynamicallyInvokable]
		public virtual Stream GetResponseStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a derived class, gets the URI of the Internet resource that actually responded to the request.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet resource that actually responded to the request.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0004E6BC File Offset: 0x0004C8BC
		[global::__DynamicallyInvokable]
		public virtual Uri ResponseUri
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of header name-value pairs associated with this request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.WebHeaderCollection" /> class that contains header values associated with this response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0004E6C3 File Offset: 0x0004C8C3
		[global::__DynamicallyInvokable]
		public virtual WebHeaderCollection Headers
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Gets a value that indicates if headers are supported.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.  
		///  <see langword="true" /> if headers are supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0004E6CA File Offset: 0x0004C8CA
		[global::__DynamicallyInvokable]
		public virtual bool SupportsHeaders
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x0400128C RID: 4748
		private bool m_IsCacheFresh;

		// Token: 0x0400128D RID: 4749
		private bool m_IsFromCache;
	}
}

using System;

namespace System.Net
{
	/// <summary>Provides the base interface for creating <see cref="T:System.Net.WebRequest" /> instances.</summary>
	// Token: 0x02000150 RID: 336
	[global::__DynamicallyInvokable]
	public interface IWebRequestCreate
	{
		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance.</summary>
		/// <param name="uri">The uniform resource identifier (URI) of the Web resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="uri" /> is not supported by this <see cref="T:System.Net.IWebRequestCreate" /> instance.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.  
		///
		///
		///
		///
		///  The URI specified in <paramref name="uri" /> is not a valid URI.</exception>
		// Token: 0x06000BAA RID: 2986
		[global::__DynamicallyInvokable]
		WebRequest Create(Uri uri);
	}
}

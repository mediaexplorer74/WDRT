using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines an attribute that can be used at the call site to specify the URL where the activation will happen. This class cannot be inherited.</summary>
	// Token: 0x0200089D RID: 2205
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> class.</summary>
		/// <param name="callsiteURL">The call site URL.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callsiteURL" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005D7D RID: 23933 RVA: 0x0014A6DC File Offset: 0x001488DC
		[SecurityCritical]
		public UrlAttribute(string callsiteURL)
			: base(UrlAttribute.propertyName)
		{
			if (callsiteURL == null)
			{
				throw new ArgumentNullException("callsiteURL");
			}
			this.url = callsiteURL;
		}

		/// <summary>Checks whether the specified object refers to the same URL as the current instance.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the object is a <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> with the same value; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005D7E RID: 23934 RVA: 0x0014A6FE File Offset: 0x001488FE
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			return o is IContextProperty && o is UrlAttribute && ((UrlAttribute)o).UrlValue.Equals(this.url);
		}

		/// <summary>Returns the hash value for the current <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</summary>
		/// <returns>The hash value for the current <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005D7F RID: 23935 RVA: 0x0014A728 File Offset: 0x00148928
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		/// <summary>Returns a Boolean value that indicates whether the specified <see cref="T:System.Runtime.Remoting.Contexts.Context" /> meets <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />'s requirements.</summary>
		/// <param name="ctx">The context to check against the current context attribute.</param>
		/// <param name="msg">The construction call, the parameters of which need to be checked against the current context.</param>
		/// <returns>
		///   <see langword="true" /> if the passed-in context is acceptable; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005D80 RID: 23936 RVA: 0x0014A735 File Offset: 0x00148935
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		/// <summary>Forces the creation of the context and the server object inside the context at the specified URL.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> of the server object to create.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005D81 RID: 23937 RVA: 0x0014A738 File Offset: 0x00148938
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		/// <summary>Gets the URL value of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</summary>
		/// <returns>The URL value of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0014A73A File Offset: 0x0014893A
		public string UrlValue
		{
			[SecurityCritical]
			get
			{
				return this.url;
			}
		}

		// Token: 0x04002A12 RID: 10770
		private string url;

		// Token: 0x04002A13 RID: 10771
		private static string propertyName = "UrlAttribute";
	}
}

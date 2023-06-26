using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a method. This class cannot be inherited.</summary>
	// Token: 0x020007D6 RID: 2006
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x00136283 File Offset: 0x00134483
		internal bool SoapActionExplicitySet
		{
			get
			{
				return this._bSoapActionExplicitySet;
			}
		}

		/// <summary>Gets or sets the SOAPAction header field used with HTTP requests sent with this method. This property is currently not implemented.</summary>
		/// <returns>The SOAPAction header field used with HTTP requests sent with this method.</returns>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x0013628B File Offset: 0x0013448B
		// (set) Token: 0x06005715 RID: 22293 RVA: 0x001362C1 File Offset: 0x001344C1
		public string SoapAction
		{
			[SecuritySafeCritical]
			get
			{
				if (this._SoapAction == null)
				{
					this._SoapAction = this.XmlTypeNamespaceOfDeclaringType + "#" + ((MemberInfo)this.ReflectInfo).Name;
				}
				return this._SoapAction;
			}
			set
			{
				this._SoapAction = value;
				this._bSoapActionExplicitySet = true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property.</exception>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06005716 RID: 22294 RVA: 0x001362D1 File Offset: 0x001344D1
		// (set) Token: 0x06005717 RID: 22295 RVA: 0x001362D4 File Offset: 0x001344D4
		public override bool UseAttribute
		{
			get
			{
				return false;
			}
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Attribute_UseAttributeNotsettable"));
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of remote method calls of the target method.</summary>
		/// <returns>The XML namespace that is used during serialization of remote method calls of the target method.</returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005718 RID: 22296 RVA: 0x001362E5 File Offset: 0x001344E5
		// (set) Token: 0x06005719 RID: 22297 RVA: 0x00136301 File Offset: 0x00134501
		public override string XmlNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ProtXmlNamespace == null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespaceOfDeclaringType;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element name to use for the method response to the target method.</summary>
		/// <returns>The XML element name to use for the method response to the target method.</returns>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600571A RID: 22298 RVA: 0x0013630A File Offset: 0x0013450A
		// (set) Token: 0x0600571B RID: 22299 RVA: 0x00136342 File Offset: 0x00134542
		public string ResponseXmlElementName
		{
			get
			{
				if (this._responseXmlElementName == null && this.ReflectInfo != null)
				{
					this._responseXmlElementName = ((MemberInfo)this.ReflectInfo).Name + "Response";
				}
				return this._responseXmlElementName;
			}
			set
			{
				this._responseXmlElementName = value;
			}
		}

		/// <summary>Gets or sets the XML element namesapce used for method response to the target method.</summary>
		/// <returns>The XML element namesapce used for method response to the target method.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x0013634B File Offset: 0x0013454B
		// (set) Token: 0x0600571D RID: 22301 RVA: 0x00136367 File Offset: 0x00134567
		public string ResponseXmlNamespace
		{
			get
			{
				if (this._responseXmlNamespace == null)
				{
					this._responseXmlNamespace = this.XmlNamespace;
				}
				return this._responseXmlNamespace;
			}
			set
			{
				this._responseXmlNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element name used for the return value from the target method.</summary>
		/// <returns>The XML element name used for the return value from the target method.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x00136370 File Offset: 0x00134570
		// (set) Token: 0x0600571F RID: 22303 RVA: 0x0013638B File Offset: 0x0013458B
		public string ReturnXmlElementName
		{
			get
			{
				if (this._returnXmlElementName == null)
				{
					this._returnXmlElementName = "return";
				}
				return this._returnXmlElementName;
			}
			set
			{
				this._returnXmlElementName = value;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06005720 RID: 22304 RVA: 0x00136394 File Offset: 0x00134594
		private string XmlTypeNamespaceOfDeclaringType
		{
			[SecurityCritical]
			get
			{
				if (this.ReflectInfo != null)
				{
					Type declaringType = ((MemberInfo)this.ReflectInfo).DeclaringType;
					return XmlNamespaceEncoder.GetXmlNamespaceForType((RuntimeType)declaringType, null);
				}
				return null;
			}
		}

		// Token: 0x040027E3 RID: 10211
		private string _SoapAction;

		// Token: 0x040027E4 RID: 10212
		private string _responseXmlElementName;

		// Token: 0x040027E5 RID: 10213
		private string _responseXmlNamespace;

		// Token: 0x040027E6 RID: 10214
		private string _returnXmlElementName;

		// Token: 0x040027E7 RID: 10215
		private bool _bSoapActionExplicitySet;
	}
}

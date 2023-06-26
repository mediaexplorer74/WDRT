using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for target types. This class cannot be inherited.</summary>
	// Token: 0x020007D5 RID: 2005
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		// Token: 0x06005701 RID: 22273 RVA: 0x001360D6 File Offset: 0x001342D6
		internal bool IsInteropXmlElement()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlElementName | SoapTypeAttribute.ExplicitlySet.XmlNamespace)) > SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x001360E3 File Offset: 0x001342E3
		internal bool IsInteropXmlType()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlTypeName | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace)) > SoapTypeAttribute.ExplicitlySet.None;
		}

		/// <summary>Gets or sets a <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> configuration value.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> configuration value.</returns>
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06005703 RID: 22275 RVA: 0x001360F1 File Offset: 0x001342F1
		// (set) Token: 0x06005704 RID: 22276 RVA: 0x001360F9 File Offset: 0x001342F9
		public SoapOption SoapOptions
		{
			get
			{
				return this._SoapOptions;
			}
			set
			{
				this._SoapOptions = value;
			}
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06005705 RID: 22277 RVA: 0x00136102 File Offset: 0x00134302
		// (set) Token: 0x06005706 RID: 22278 RVA: 0x00136130 File Offset: 0x00134330
		public string XmlElementName
		{
			get
			{
				if (this._XmlElementName == null && this.ReflectInfo != null)
				{
					this._XmlElementName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlElementName;
			}
			set
			{
				this._XmlElementName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlElementName;
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of the target object type.</summary>
		/// <returns>The XML namespace that is used during serialization of the target object type.</returns>
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06005707 RID: 22279 RVA: 0x00136147 File Offset: 0x00134347
		// (set) Token: 0x06005708 RID: 22280 RVA: 0x0013616B File Offset: 0x0013436B
		public override string XmlNamespace
		{
			get
			{
				if (this.ProtXmlNamespace == null && this.ReflectInfo != null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespace;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlNamespace;
			}
		}

		/// <summary>Gets or sets the XML type name for the target object type.</summary>
		/// <returns>The XML type name for the target object type.</returns>
		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06005709 RID: 22281 RVA: 0x00136182 File Offset: 0x00134382
		// (set) Token: 0x0600570A RID: 22282 RVA: 0x001361B0 File Offset: 0x001343B0
		public string XmlTypeName
		{
			get
			{
				if (this._XmlTypeName == null && this.ReflectInfo != null)
				{
					this._XmlTypeName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlTypeName;
			}
			set
			{
				this._XmlTypeName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeName;
			}
		}

		/// <summary>Gets or sets the XML type namespace for the current object type.</summary>
		/// <returns>The XML type namespace for the current object type.</returns>
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600570B RID: 22283 RVA: 0x001361C7 File Offset: 0x001343C7
		// (set) Token: 0x0600570C RID: 22284 RVA: 0x001361F6 File Offset: 0x001343F6
		public string XmlTypeNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this._XmlTypeNamespace == null && this.ReflectInfo != null)
				{
					this._XmlTypeNamespace = XmlNamespaceEncoder.GetXmlNamespaceForTypeNamespace((RuntimeType)this.ReflectInfo, null);
				}
				return this._XmlTypeNamespace;
			}
			set
			{
				this._XmlTypeNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace;
			}
		}

		/// <summary>Gets or sets the XML field order for the target object type.</summary>
		/// <returns>The XML field order for the target object type.</returns>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x0600570D RID: 22285 RVA: 0x0013620D File Offset: 0x0013440D
		// (set) Token: 0x0600570E RID: 22286 RVA: 0x00136215 File Offset: 0x00134415
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._XmlFieldOrder;
			}
			set
			{
				this._XmlFieldOrder = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property.</exception>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600570F RID: 22287 RVA: 0x0013621E File Offset: 0x0013441E
		// (set) Token: 0x06005710 RID: 22288 RVA: 0x00136221 File Offset: 0x00134421
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

		// Token: 0x06005711 RID: 22289 RVA: 0x00136234 File Offset: 0x00134434
		private static string GetTypeName(Type t)
		{
			if (!t.IsNested)
			{
				return t.Name;
			}
			string fullName = t.FullName;
			string @namespace = t.Namespace;
			if (@namespace == null || @namespace.Length == 0)
			{
				return fullName;
			}
			return fullName.Substring(@namespace.Length + 1);
		}

		// Token: 0x040027DD RID: 10205
		private SoapTypeAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x040027DE RID: 10206
		private SoapOption _SoapOptions;

		// Token: 0x040027DF RID: 10207
		private string _XmlElementName;

		// Token: 0x040027E0 RID: 10208
		private string _XmlTypeName;

		// Token: 0x040027E1 RID: 10209
		private string _XmlTypeNamespace;

		// Token: 0x040027E2 RID: 10210
		private XmlFieldOrderOption _XmlFieldOrder;

		// Token: 0x02000C6A RID: 3178
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x040037EB RID: 14315
			None = 0,
			// Token: 0x040037EC RID: 14316
			XmlElementName = 1,
			// Token: 0x040037ED RID: 14317
			XmlNamespace = 2,
			// Token: 0x040037EE RID: 14318
			XmlTypeName = 4,
			// Token: 0x040037EF RID: 14319
			XmlTypeNamespace = 8
		}
	}
}

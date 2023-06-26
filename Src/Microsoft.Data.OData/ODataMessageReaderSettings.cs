using System;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024F RID: 591
	public sealed class ODataMessageReaderSettings : ODataMessageReaderSettingsBase
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x00047858 File Offset: 0x00045A58
		public ODataMessageReaderSettings()
		{
			this.DisablePrimitiveTypeConversion = false;
			this.DisableMessageStreamDisposal = false;
			this.UndeclaredPropertyBehaviorKinds = ODataUndeclaredPropertyBehaviorKinds.None;
			this.readerBehavior = ODataReaderBehavior.DefaultBehavior;
			this.MaxProtocolVersion = ODataVersion.V3;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00047888 File Offset: 0x00045A88
		public ODataMessageReaderSettings(ODataMessageReaderSettings other)
			: base(other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(other, "other");
			this.BaseUri = other.BaseUri;
			this.DisableMessageStreamDisposal = other.DisableMessageStreamDisposal;
			this.DisablePrimitiveTypeConversion = other.DisablePrimitiveTypeConversion;
			this.UndeclaredPropertyBehaviorKinds = other.UndeclaredPropertyBehaviorKinds;
			this.MaxProtocolVersion = other.MaxProtocolVersion;
			this.atomFormatEntryXmlCustomizationCallback = other.atomFormatEntryXmlCustomizationCallback;
			this.readerBehavior = other.ReaderBehavior;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x000478FB File Offset: 0x00045AFB
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00047903 File Offset: 0x00045B03
		public Uri BaseUri { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0004790C File Offset: 0x00045B0C
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x00047914 File Offset: 0x00045B14
		public bool DisablePrimitiveTypeConversion { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004791D File Offset: 0x00045B1D
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x00047925 File Offset: 0x00045B25
		public ODataUndeclaredPropertyBehaviorKinds UndeclaredPropertyBehaviorKinds { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004792E File Offset: 0x00045B2E
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x00047936 File Offset: 0x00045B36
		public bool DisableMessageStreamDisposal { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0004793F File Offset: 0x00045B3F
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x00047947 File Offset: 0x00045B47
		public ODataVersion MaxProtocolVersion { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00047950 File Offset: 0x00045B50
		internal bool DisableStrictMetadataValidation
		{
			get
			{
				return this.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer || this.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00047970 File Offset: 0x00045B70
		internal ODataReaderBehavior ReaderBehavior
		{
			get
			{
				return this.readerBehavior;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00047978 File Offset: 0x00045B78
		internal Func<ODataEntry, XmlReader, Uri, XmlReader> AtomEntryXmlCustomizationCallback
		{
			get
			{
				return this.atomFormatEntryXmlCustomizationCallback;
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00047980 File Offset: 0x00045B80
		public bool ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (undeclaredPropertyBehaviorKinds == ODataUndeclaredPropertyBehaviorKinds.None)
			{
				return this.UndeclaredPropertyBehaviorKinds == ODataUndeclaredPropertyBehaviorKinds.None;
			}
			return this.UndeclaredPropertyBehaviorKinds.HasFlag(undeclaredPropertyBehaviorKinds);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000479A5 File Offset: 0x00045BA5
		public void SetAtomEntryXmlCustomizationCallback(Func<ODataEntry, XmlReader, Uri, XmlReader> atomEntryXmlCustomizationCallback)
		{
			this.atomFormatEntryXmlCustomizationCallback = atomEntryXmlCustomizationCallback;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000479AE File Offset: 0x00045BAE
		public void EnableDefaultBehavior()
		{
			this.SetAtomEntryXmlCustomizationCallback(null);
			this.readerBehavior = ODataReaderBehavior.DefaultBehavior;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000479C2 File Offset: 0x00045BC2
		public void EnableWcfDataServicesServerBehavior(bool usesV1Provider)
		{
			this.SetAtomEntryXmlCustomizationCallback(null);
			this.readerBehavior = ODataReaderBehavior.CreateWcfDataServicesServerBehavior(usesV1Provider);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000479D7 File Offset: 0x00045BD7
		public void EnableWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme, Func<ODataEntry, XmlReader, Uri, XmlReader> entryXmlCustomizationCallback)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(odataNamespace, "odataNamespace");
			ExceptionUtils.CheckArgumentNotNull<string>(typeScheme, "typeScheme");
			this.SetAtomEntryXmlCustomizationCallback(entryXmlCustomizationCallback);
			this.readerBehavior = ODataReaderBehavior.CreateWcfDataServicesClientBehavior(typeResolver, odataNamespace, typeScheme);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00047A05 File Offset: 0x00045C05
		[Obsolete("The 'shouldQualifyOperations' parameter is no longer needed and will be removed. Use the overload which does not take it.")]
		public void EnableWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme, Func<ODataEntry, XmlReader, Uri, XmlReader> entryXmlCustomizationCallback, Func<IEdmEntityType, bool> shouldQualifyOperations)
		{
			this.EnableWcfDataServicesClientBehavior(typeResolver, odataNamespace, typeScheme, entryXmlCustomizationCallback);
			this.readerBehavior.OperationsBoundToEntityTypeMustBeContainerQualified = shouldQualifyOperations;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00047A1F File Offset: 0x00045C1F
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.MaxProtocolVersion < ODataVersion.V3 || this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation(annotationName);
		}

		// Token: 0x040006D1 RID: 1745
		private ODataReaderBehavior readerBehavior;

		// Token: 0x040006D2 RID: 1746
		private Func<ODataEntry, XmlReader, Uri, XmlReader> atomFormatEntryXmlCustomizationCallback;
	}
}

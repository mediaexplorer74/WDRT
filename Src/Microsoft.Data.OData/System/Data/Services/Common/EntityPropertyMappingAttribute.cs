using System;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Metadata;

namespace System.Data.Services.Common
{
	// Token: 0x020001EF RID: 495
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class EntityPropertyMappingAttribute : Attribute
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00036AF8 File Offset: 0x00034CF8
		public EntityPropertyMappingAttribute(string sourcePath, SyndicationItemProperty targetSyndicationItem, SyndicationTextContentKind targetTextContentKind, bool keepInContent)
		{
			if (string.IsNullOrEmpty(sourcePath))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("sourcePath"));
			}
			this.sourcePath = sourcePath;
			this.targetPath = targetSyndicationItem.ToTargetPath();
			this.targetSyndicationItem = targetSyndicationItem;
			this.targetTextContentKind = targetTextContentKind;
			this.targetNamespacePrefix = "atom";
			this.targetNamespaceUri = "http://www.w3.org/2005/Atom";
			this.keepInContent = keepInContent;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00036B64 File Offset: 0x00034D64
		public EntityPropertyMappingAttribute(string sourcePath, string targetPath, string targetNamespacePrefix, string targetNamespaceUri, bool keepInContent)
		{
			if (string.IsNullOrEmpty(sourcePath))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("sourcePath"));
			}
			this.sourcePath = sourcePath;
			if (string.IsNullOrEmpty(targetPath))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("targetPath"));
			}
			if (targetPath[0] == '@')
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_InvalidTargetPath(targetPath));
			}
			this.targetPath = targetPath;
			this.targetSyndicationItem = SyndicationItemProperty.CustomProperty;
			this.targetTextContentKind = SyndicationTextContentKind.Plaintext;
			this.targetNamespacePrefix = targetNamespacePrefix;
			if (string.IsNullOrEmpty(targetNamespaceUri))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("targetNamespaceUri"));
			}
			this.targetNamespaceUri = targetNamespaceUri;
			Uri uri;
			if (!Uri.TryCreate(targetNamespaceUri, UriKind.Absolute, out uri))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_TargetNamespaceUriNotValid(targetNamespaceUri));
			}
			this.keepInContent = keepInContent;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00036C23 File Offset: 0x00034E23
		public string SourcePath
		{
			get
			{
				return this.sourcePath;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00036C2B File Offset: 0x00034E2B
		public string TargetPath
		{
			get
			{
				return this.targetPath;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00036C33 File Offset: 0x00034E33
		public SyndicationItemProperty TargetSyndicationItem
		{
			get
			{
				return this.targetSyndicationItem;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00036C3B File Offset: 0x00034E3B
		public string TargetNamespacePrefix
		{
			get
			{
				return this.targetNamespacePrefix;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00036C43 File Offset: 0x00034E43
		public string TargetNamespaceUri
		{
			get
			{
				return this.targetNamespaceUri;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00036C4B File Offset: 0x00034E4B
		public SyndicationTextContentKind TargetTextContentKind
		{
			get
			{
				return this.targetTextContentKind;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00036C53 File Offset: 0x00034E53
		public bool KeepInContent
		{
			get
			{
				return this.keepInContent;
			}
		}

		// Token: 0x0400054B RID: 1355
		private readonly string sourcePath;

		// Token: 0x0400054C RID: 1356
		private readonly string targetPath;

		// Token: 0x0400054D RID: 1357
		private readonly SyndicationItemProperty targetSyndicationItem;

		// Token: 0x0400054E RID: 1358
		private readonly SyndicationTextContentKind targetTextContentKind;

		// Token: 0x0400054F RID: 1359
		private readonly string targetNamespacePrefix;

		// Token: 0x04000550 RID: 1360
		private readonly string targetNamespaceUri;

		// Token: 0x04000551 RID: 1361
		private readonly bool keepInContent;
	}
}

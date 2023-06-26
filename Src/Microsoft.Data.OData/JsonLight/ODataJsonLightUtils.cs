using System;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200013F RID: 319
	internal static class ODataJsonLightUtils
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		internal static bool IsMetadataReferenceProperty(string propertyName)
		{
			return propertyName.IndexOf('#') >= 0;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001B908 File Offset: 0x00019B08
		internal static string GetFullyQualifiedFunctionImportName(Uri metadataDocumentUri, string metadataReferencePropertyName, out string firstParameterTypeName)
		{
			string text = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(metadataDocumentUri, metadataReferencePropertyName);
			firstParameterTypeName = null;
			int num = text.IndexOf('(');
			if (num > -1)
			{
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				firstParameterTypeName = text2.Split(ODataJsonLightUtils.ParameterSeparatorSplitCharacters).First<string>().Trim(ODataJsonLightUtils.CharactersToTrimFromParameters);
			}
			return text;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001B960 File Offset: 0x00019B60
		internal static string GetUriFragmentFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, propertyName).GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001B97E File Offset: 0x00019B7E
		internal static Uri GetAbsoluteUriFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			if (propertyName[0] == '#')
			{
				propertyName = UriUtils.EnsureEscapedFragment(propertyName);
				return new Uri(metadataDocumentUri, propertyName);
			}
			return new Uri(propertyName, UriKind.Absolute);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		internal static string GetMetadataReferenceName(IEdmFunctionImport functionImport)
		{
			string text = functionImport.FullName();
			bool flag = functionImport.Container.FindFunctionImports(functionImport.Name).Take(2).Count<IEdmFunctionImport>() > 1;
			if (flag)
			{
				text = functionImport.FullNameWithParameters();
			}
			return text;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		internal static ODataOperation CreateODataOperation(Uri metadataDocumentUri, string metadataReferencePropertyName, IEdmFunctionImport functionImport, out bool isAction)
		{
			isAction = functionImport.IsSideEffecting;
			ODataOperation odataOperation = (isAction ? new ODataAction() : new ODataFunction());
			odataOperation.Metadata = ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, metadataReferencePropertyName);
			return odataOperation;
		}

		// Token: 0x0400034A RID: 842
		private static readonly char[] ParameterSeparatorSplitCharacters = new char[] { ","[0] };

		// Token: 0x0400034B RID: 843
		private static readonly char[] CharactersToTrimFromParameters = new char[] { '(', ')' };
	}
}

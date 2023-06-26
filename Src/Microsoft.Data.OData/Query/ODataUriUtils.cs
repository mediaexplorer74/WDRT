using System;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020001EA RID: 490
	public static class ODataUriUtils
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x0003662F File Offset: 0x0003482F
		public static object ConvertFromUriLiteral(string value, ODataVersion version)
		{
			return ODataUriUtils.ConvertFromUriLiteral(value, version, null, null);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003663C File Offset: 0x0003483C
		public static object ConvertFromUriLiteral(string value, ODataVersion version, IEdmModel model, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(value, "value");
			if (typeReference != null && model == null)
			{
				throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel);
			}
			if (model == null)
			{
				model = EdmCoreModel.Instance;
			}
			ExpressionLexer expressionLexer = new ExpressionLexer(value, false, false);
			ExpressionToken expressionToken;
			Exception ex;
			expressionLexer.TryPeekNextToken(out expressionToken, out ex);
			if (expressionToken.Kind == ExpressionTokenKind.BracketedExpression)
			{
				return ODataUriConversionUtils.ConvertFromComplexOrCollectionValue(value, version, model, typeReference);
			}
			object obj = expressionLexer.ReadLiteralToken();
			if (typeReference != null)
			{
				obj = ODataUriConversionUtils.VerifyAndCoerceUriPrimitiveLiteral(obj, model, typeReference, version);
			}
			if (obj is ISpatial)
			{
				ODataVersionChecker.CheckSpatialValue(version);
			}
			return obj;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000366BB File Offset: 0x000348BB
		public static string ConvertToUriLiteral(object value, ODataVersion version)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, null);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000366C5 File Offset: 0x000348C5
		public static string ConvertToUriLiteral(object value, ODataVersion version, IEdmModel model)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, model, ODataFormat.VerboseJson);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000366D4 File Offset: 0x000348D4
		public static string ConvertToUriLiteral(object value, ODataVersion version, IEdmModel model, ODataFormat format)
		{
			if (value == null)
			{
				value = new ODataUriNullValue();
			}
			if (model == null)
			{
				model = EdmCoreModel.Instance;
			}
			ODataUriNullValue odataUriNullValue = value as ODataUriNullValue;
			if (odataUriNullValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriNullValue(odataUriNullValue);
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriCollectionLiteral(odataCollectionValue, model, version, format);
			}
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriComplexLiteral(odataComplexValue, model, version, format);
			}
			return ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value, version);
		}
	}
}

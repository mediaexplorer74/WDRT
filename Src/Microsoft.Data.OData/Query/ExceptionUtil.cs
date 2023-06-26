using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200000F RID: 15
	internal static class ExceptionUtil
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002F2B File Offset: 0x0000112B
		internal static ODataException CreateResourceNotFound(string identifier)
		{
			return ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ResourceNotFound(identifier));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F38 File Offset: 0x00001138
		internal static ODataException ResourceNotFoundError(string errorMessage)
		{
			return new ODataUnrecognizedPathException(errorMessage);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F40 File Offset: 0x00001140
		internal static ODataException CreateSyntaxError()
		{
			return ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SyntaxError);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F4C File Offset: 0x0000114C
		internal static ODataException CreateBadRequestError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F54 File Offset: 0x00001154
		internal static void ThrowSyntaxErrorIfNotValid(bool valid)
		{
			if (!valid)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F5F File Offset: 0x0000115F
		internal static void ThrowIfResourceDoesNotExist(bool resourceExists, string identifier)
		{
			if (!resourceExists)
			{
				throw ExceptionUtil.CreateResourceNotFound(identifier);
			}
		}
	}
}

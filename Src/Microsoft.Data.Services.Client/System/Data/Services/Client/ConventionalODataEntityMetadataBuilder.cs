using System;
using System.Data.Services.Common;
using System.Text;
using Microsoft.Data.Edm.Values;

namespace System.Data.Services.Client
{
	// Token: 0x02000034 RID: 52
	internal sealed class ConventionalODataEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00008F40 File Offset: 0x00007140
		internal ConventionalODataEntityMetadataBuilder(Uri baseUri, string entitySetName, IEdmStructuredValue entityInstance, DataServiceUrlConventions conventions)
			: this(UriResolver.CreateFromBaseUri(baseUri, "baseUri"), entitySetName, entityInstance, conventions)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008F58 File Offset: 0x00007158
		internal ConventionalODataEntityMetadataBuilder(UriResolver resolver, string entitySetName, IEdmStructuredValue entityInstance, DataServiceUrlConventions conventions)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			Util.CheckArgumentNull<IEdmStructuredValue>(entityInstance, "entityInstance");
			Util.CheckArgumentNull<DataServiceUrlConventions>(conventions, "conventions");
			this.entitySetName = entitySetName;
			this.entityInstance = entityInstance;
			this.uriBuilder = new ConventionalODataEntityMetadataBuilder.ConventionalODataUriBuilder(resolver, conventions);
			this.baseUri = resolver.BaseUriOrNull;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008FB8 File Offset: 0x000071B8
		internal override Uri GetEditLink()
		{
			Uri uri = this.uriBuilder.BuildEntitySetUri(this.baseUri, this.entitySetName);
			return this.uriBuilder.BuildEntityInstanceUri(uri, this.entityInstance);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008FF1 File Offset: 0x000071F1
		internal override string GetId()
		{
			return this.GetEditLink().AbsoluteUri;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008FFE File Offset: 0x000071FE
		internal override string GetETag()
		{
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009001 File Offset: 0x00007201
		internal override Uri GetReadLink()
		{
			return null;
		}

		// Token: 0x040001F7 RID: 503
		private readonly IEdmStructuredValue entityInstance;

		// Token: 0x040001F8 RID: 504
		private readonly string entitySetName;

		// Token: 0x040001F9 RID: 505
		private readonly Uri baseUri;

		// Token: 0x040001FA RID: 506
		private readonly ConventionalODataEntityMetadataBuilder.ConventionalODataUriBuilder uriBuilder;

		// Token: 0x02000035 RID: 53
		private class ConventionalODataUriBuilder : ODataUriBuilder
		{
			// Token: 0x06000190 RID: 400 RVA: 0x00009004 File Offset: 0x00007204
			internal ConventionalODataUriBuilder(UriResolver resolver, DataServiceUrlConventions conventions)
			{
				this.resolver = resolver;
				this.conventions = conventions;
			}

			// Token: 0x06000191 RID: 401 RVA: 0x0000901A File Offset: 0x0000721A
			internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
			{
				return this.resolver.GetEntitySetUri(entitySetName);
			}

			// Token: 0x06000192 RID: 402 RVA: 0x00009028 File Offset: 0x00007228
			internal override Uri BuildEntityInstanceUri(Uri baseUri, IEdmStructuredValue entityInstance)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (baseUri != null)
				{
					stringBuilder.Append(UriUtil.UriToString(baseUri));
				}
				this.conventions.AppendKeyExpression(entityInstance, stringBuilder);
				return UriUtil.CreateUri(stringBuilder.ToString(), UriKind.RelativeOrAbsolute);
			}

			// Token: 0x040001FB RID: 507
			private readonly UriResolver resolver;

			// Token: 0x040001FC RID: 508
			private readonly DataServiceUrlConventions conventions;
		}
	}
}

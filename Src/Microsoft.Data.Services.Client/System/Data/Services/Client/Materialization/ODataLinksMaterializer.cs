using System;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000071 RID: 113
	internal sealed class ODataLinksMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x060003CB RID: 971 RVA: 0x000102EC File Offset: 0x0000E4EC
		public ODataLinksMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000102FC File Offset: 0x0000E4FC
		internal override long CountValue
		{
			get
			{
				if (this.links == null && !this.IsDisposed)
				{
					this.ReadLinks();
				}
				if (this.links != null && this.links.Count != null)
				{
					return this.links.Count.Value;
				}
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001035A File Offset: 0x0000E55A
		internal override object CurrentValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001035D File Offset: 0x0000E55D
		internal override bool IsCountable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010360 File Offset: 0x0000E560
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			this.ReadLinks();
			Type type = Nullable.GetUnderlyingType(base.ExpectedType) ?? base.ExpectedType;
			ClientEdmModel model = base.MaterializerContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(type));
			if (clientTypeAnnotation.IsEntityType)
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidEntityType(clientTypeAnnotation.ElementTypeName));
			}
			throw Error.InvalidOperation(Strings.Deserialize_MixedTextWithComment);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000103C8 File Offset: 0x0000E5C8
		private void ReadLinks()
		{
			try
			{
				if (this.links == null)
				{
					this.links = this.messageReader.ReadEntityReferenceLinks();
				}
			}
			catch (ODataErrorException ex)
			{
				throw new DataServiceClientException(Strings.Deserialize_ServerException(ex.Error.Message), ex);
			}
			catch (ODataException ex2)
			{
				throw new InvalidOperationException(ex2.Message, ex2);
			}
		}

		// Token: 0x040002B4 RID: 692
		private ODataEntityReferenceLinks links;
	}
}

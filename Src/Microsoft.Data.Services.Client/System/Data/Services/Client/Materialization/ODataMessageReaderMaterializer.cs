using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x0200006A RID: 106
	internal abstract class ODataMessageReaderMaterializer : ODataMaterializer
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0000FD60 File Offset: 0x0000DF60
		public ODataMessageReaderMaterializer(ODataMessageReader reader, IODataMaterializerContext context, Type expectedType, bool? singleResult)
			: base(context, expectedType)
		{
			this.messageReader = reader;
			this.SingleResult = singleResult;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000FD79 File Offset: 0x0000DF79
		internal sealed override ODataFeed CurrentFeed
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		internal sealed override ODataEntry CurrentEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000FD7F File Offset: 0x0000DF7F
		internal sealed override bool IsEndOfStream
		{
			get
			{
				return this.hasReadValue;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000FD87 File Offset: 0x0000DF87
		internal override long CountValue
		{
			get
			{
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000FD93 File Offset: 0x0000DF93
		internal sealed override ProjectionPlan MaterializeEntryPlan
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000FD9A File Offset: 0x0000DF9A
		protected sealed override bool IsDisposed
		{
			get
			{
				return this.messageReader == null;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000FDA5 File Offset: 0x0000DFA5
		protected override ODataFormat Format
		{
			get
			{
				return ODataUtils.GetReadFormat(this.messageReader);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		internal sealed override void ClearLog()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		internal sealed override void ApplyLogToContext()
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		protected sealed override bool ReadImplementation()
		{
			if (!this.hasReadValue)
			{
				try
				{
					ClientEdmModel model = base.MaterializerContext.Model;
					Type type = base.ExpectedType;
					IEdmTypeReference edmTypeReference = model.GetOrCreateEdmType(type).ToEdmTypeReference(ClientTypeUtil.CanAssignNull(type));
					if (this.SingleResult != null && !this.SingleResult.Value && edmTypeReference.Definition.TypeKind != EdmTypeKind.Collection)
					{
						type = typeof(ICollection<>).MakeGenericType(new Type[] { type });
						edmTypeReference = model.GetOrCreateEdmType(type).ToEdmTypeReference(false);
					}
					IEdmTypeReference edmTypeReference2 = base.MaterializerContext.ResolveExpectedTypeForReading(type).ToEdmTypeReference(edmTypeReference.IsNullable);
					this.ReadWithExpectedType(edmTypeReference, edmTypeReference2);
				}
				catch (ODataErrorException ex)
				{
					throw new DataServiceClientException(Strings.Deserialize_ServerException(ex.Error.Message), ex);
				}
				catch (ODataException ex2)
				{
					throw new InvalidOperationException(ex2.Message, ex2);
				}
				catch (ArgumentException ex3)
				{
					throw new InvalidOperationException(ex3.Message, ex3);
				}
				finally
				{
					this.hasReadValue = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
		protected sealed override void OnDispose()
		{
			if (this.messageReader != null)
			{
				this.messageReader.Dispose();
				this.messageReader = null;
			}
		}

		// Token: 0x060003A2 RID: 930
		protected abstract void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType);

		// Token: 0x040002AB RID: 683
		protected readonly bool? SingleResult;

		// Token: 0x040002AC RID: 684
		protected ODataMessageReader messageReader;

		// Token: 0x040002AD RID: 685
		private bool hasReadValue;
	}
}

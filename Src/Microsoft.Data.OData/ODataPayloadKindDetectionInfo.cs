using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BC RID: 444
	internal sealed class ODataPayloadKindDetectionInfo
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00030894 File Offset: 0x0002EA94
		internal ODataPayloadKindDetectionInfo(MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, IEdmModel model, IEnumerable<ODataPayloadKind> possiblePayloadKinds)
		{
			ExceptionUtils.CheckArgumentNotNull<MediaType>(contentType, "contentType");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "readerSettings");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataPayloadKind>>(possiblePayloadKinds, "possiblePayloadKinds");
			this.contentType = contentType;
			this.encoding = encoding;
			this.messageReaderSettings = messageReaderSettings;
			this.model = model;
			this.possiblePayloadKinds = possiblePayloadKinds;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000308EE File Offset: 0x0002EAEE
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x000308F6 File Offset: 0x0002EAF6
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x000308FE File Offset: 0x0002EAFE
		public IEnumerable<ODataPayloadKind> PossiblePayloadKinds
		{
			get
			{
				return this.possiblePayloadKinds;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00030906 File Offset: 0x0002EB06
		internal MediaType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003090E File Offset: 0x0002EB0E
		internal object PayloadKindDetectionFormatState
		{
			get
			{
				return this.payloadKindDetectionFormatState;
			}
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00030916 File Offset: 0x0002EB16
		public Encoding GetEncoding()
		{
			return this.encoding ?? this.contentType.SelectEncoding();
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003092D File Offset: 0x0002EB2D
		public void SetPayloadKindDetectionFormatState(object state)
		{
			this.payloadKindDetectionFormatState = state;
		}

		// Token: 0x0400049C RID: 1180
		private readonly MediaType contentType;

		// Token: 0x0400049D RID: 1181
		private readonly Encoding encoding;

		// Token: 0x0400049E RID: 1182
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x0400049F RID: 1183
		private readonly IEdmModel model;

		// Token: 0x040004A0 RID: 1184
		private readonly IEnumerable<ODataPayloadKind> possiblePayloadKinds;

		// Token: 0x040004A1 RID: 1185
		private object payloadKindDetectionFormatState;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C9 RID: 457
	internal sealed class ODataVerboseJsonServiceDocumentSerializer : ODataVerboseJsonSerializer
	{
		// Token: 0x06000E37 RID: 3639 RVA: 0x0003246F File Offset: 0x0003066F
		internal ODataVerboseJsonServiceDocumentSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00032544 File Offset: 0x00030744
		internal void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			IEnumerable<ODataResourceCollectionInfo> collections = defaultWorkspace.Collections;
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				this.JsonWriter.WriteName("EntitySets");
				this.JsonWriter.StartArrayScope();
				if (collections != null)
				{
					foreach (ODataResourceCollectionInfo odataResourceCollectionInfo in collections)
					{
						ValidationUtils.ValidateResourceCollectionInfo(odataResourceCollectionInfo);
						this.JsonWriter.WriteValue(UriUtilsCommon.UriToString(odataResourceCollectionInfo.Url));
					}
				}
				this.JsonWriter.EndArrayScope();
				this.JsonWriter.EndObjectScope();
			});
		}
	}
}

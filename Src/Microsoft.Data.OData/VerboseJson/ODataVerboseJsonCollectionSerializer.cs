using System;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C5 RID: 453
	internal sealed class ODataVerboseJsonCollectionSerializer : ODataVerboseJsonPropertyAndValueSerializer
	{
		// Token: 0x06000E08 RID: 3592 RVA: 0x000317C1 File Offset: 0x0002F9C1
		internal ODataVerboseJsonCollectionSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000317CA File Offset: 0x0002F9CA
		internal void WriteCollectionStart()
		{
			if (base.WritingResponse && base.Version >= ODataVersion.V2)
			{
				base.JsonWriter.StartObjectScope();
				base.JsonWriter.WriteDataArrayName();
			}
			base.JsonWriter.StartArrayScope();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000317FE File Offset: 0x0002F9FE
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (base.WritingResponse && base.Version >= ODataVersion.V2)
			{
				base.JsonWriter.EndObjectScope();
			}
		}
	}
}

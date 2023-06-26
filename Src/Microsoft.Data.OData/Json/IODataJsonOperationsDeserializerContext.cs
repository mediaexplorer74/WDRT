using System;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000176 RID: 374
	internal interface IODataJsonOperationsDeserializerContext
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AA3 RID: 2723
		JsonReader JsonReader { get; }

		// Token: 0x06000AA4 RID: 2724
		Uri ProcessUriFromPayload(string uriFromPayload);

		// Token: 0x06000AA5 RID: 2725
		void AddActionToEntry(ODataAction action);

		// Token: 0x06000AA6 RID: 2726
		void AddFunctionToEntry(ODataFunction function);
	}
}

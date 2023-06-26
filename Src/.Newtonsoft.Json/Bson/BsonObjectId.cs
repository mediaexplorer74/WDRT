using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000105 RID: 261
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00035B51 File Offset: 0x00033D51
		public byte[] Value { get; }

		// Token: 0x06000D61 RID: 3425 RVA: 0x00035B59 File Offset: 0x00033D59
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}

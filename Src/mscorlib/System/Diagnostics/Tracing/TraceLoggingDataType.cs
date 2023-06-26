using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000480 RID: 1152
	internal enum TraceLoggingDataType
	{
		// Token: 0x04001877 RID: 6263
		Nil,
		// Token: 0x04001878 RID: 6264
		Utf16String,
		// Token: 0x04001879 RID: 6265
		MbcsString,
		// Token: 0x0400187A RID: 6266
		Int8,
		// Token: 0x0400187B RID: 6267
		UInt8,
		// Token: 0x0400187C RID: 6268
		Int16,
		// Token: 0x0400187D RID: 6269
		UInt16,
		// Token: 0x0400187E RID: 6270
		Int32,
		// Token: 0x0400187F RID: 6271
		UInt32,
		// Token: 0x04001880 RID: 6272
		Int64,
		// Token: 0x04001881 RID: 6273
		UInt64,
		// Token: 0x04001882 RID: 6274
		Float,
		// Token: 0x04001883 RID: 6275
		Double,
		// Token: 0x04001884 RID: 6276
		Boolean32,
		// Token: 0x04001885 RID: 6277
		Binary,
		// Token: 0x04001886 RID: 6278
		Guid,
		// Token: 0x04001887 RID: 6279
		FileTime = 17,
		// Token: 0x04001888 RID: 6280
		SystemTime,
		// Token: 0x04001889 RID: 6281
		HexInt32 = 20,
		// Token: 0x0400188A RID: 6282
		HexInt64,
		// Token: 0x0400188B RID: 6283
		CountedUtf16String,
		// Token: 0x0400188C RID: 6284
		CountedMbcsString,
		// Token: 0x0400188D RID: 6285
		Struct,
		// Token: 0x0400188E RID: 6286
		Char16 = 518,
		// Token: 0x0400188F RID: 6287
		Char8 = 516,
		// Token: 0x04001890 RID: 6288
		Boolean8 = 772,
		// Token: 0x04001891 RID: 6289
		HexInt8 = 1028,
		// Token: 0x04001892 RID: 6290
		HexInt16 = 1030,
		// Token: 0x04001893 RID: 6291
		Utf16Xml = 2817,
		// Token: 0x04001894 RID: 6292
		MbcsXml,
		// Token: 0x04001895 RID: 6293
		CountedUtf16Xml = 2838,
		// Token: 0x04001896 RID: 6294
		CountedMbcsXml,
		// Token: 0x04001897 RID: 6295
		Utf16Json = 3073,
		// Token: 0x04001898 RID: 6296
		MbcsJson,
		// Token: 0x04001899 RID: 6297
		CountedUtf16Json = 3094,
		// Token: 0x0400189A RID: 6298
		CountedMbcsJson,
		// Token: 0x0400189B RID: 6299
		HResult = 3847
	}
}

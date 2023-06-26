using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A02 RID: 2562
	[Guid("4bd682dd-7554-40e9-9a9b-82654ede7e62")]
	[ComImport]
	internal interface IPropertyValue
	{
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600656A RID: 25962
		PropertyType Type { get; }

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x0600656B RID: 25963
		bool IsNumericScalar { get; }

		// Token: 0x0600656C RID: 25964
		byte GetUInt8();

		// Token: 0x0600656D RID: 25965
		short GetInt16();

		// Token: 0x0600656E RID: 25966
		ushort GetUInt16();

		// Token: 0x0600656F RID: 25967
		int GetInt32();

		// Token: 0x06006570 RID: 25968
		uint GetUInt32();

		// Token: 0x06006571 RID: 25969
		long GetInt64();

		// Token: 0x06006572 RID: 25970
		ulong GetUInt64();

		// Token: 0x06006573 RID: 25971
		float GetSingle();

		// Token: 0x06006574 RID: 25972
		double GetDouble();

		// Token: 0x06006575 RID: 25973
		char GetChar16();

		// Token: 0x06006576 RID: 25974
		bool GetBoolean();

		// Token: 0x06006577 RID: 25975
		string GetString();

		// Token: 0x06006578 RID: 25976
		Guid GetGuid();

		// Token: 0x06006579 RID: 25977
		DateTimeOffset GetDateTime();

		// Token: 0x0600657A RID: 25978
		TimeSpan GetTimeSpan();

		// Token: 0x0600657B RID: 25979
		Point GetPoint();

		// Token: 0x0600657C RID: 25980
		Size GetSize();

		// Token: 0x0600657D RID: 25981
		Rect GetRect();

		// Token: 0x0600657E RID: 25982
		byte[] GetUInt8Array();

		// Token: 0x0600657F RID: 25983
		short[] GetInt16Array();

		// Token: 0x06006580 RID: 25984
		ushort[] GetUInt16Array();

		// Token: 0x06006581 RID: 25985
		int[] GetInt32Array();

		// Token: 0x06006582 RID: 25986
		uint[] GetUInt32Array();

		// Token: 0x06006583 RID: 25987
		long[] GetInt64Array();

		// Token: 0x06006584 RID: 25988
		ulong[] GetUInt64Array();

		// Token: 0x06006585 RID: 25989
		float[] GetSingleArray();

		// Token: 0x06006586 RID: 25990
		double[] GetDoubleArray();

		// Token: 0x06006587 RID: 25991
		char[] GetChar16Array();

		// Token: 0x06006588 RID: 25992
		bool[] GetBooleanArray();

		// Token: 0x06006589 RID: 25993
		string[] GetStringArray();

		// Token: 0x0600658A RID: 25994
		object[] GetInspectableArray();

		// Token: 0x0600658B RID: 25995
		Guid[] GetGuidArray();

		// Token: 0x0600658C RID: 25996
		DateTimeOffset[] GetDateTimeArray();

		// Token: 0x0600658D RID: 25997
		TimeSpan[] GetTimeSpanArray();

		// Token: 0x0600658E RID: 25998
		Point[] GetPointArray();

		// Token: 0x0600658F RID: 25999
		Size[] GetSizeArray();

		// Token: 0x06006590 RID: 26000
		Rect[] GetRectArray();
	}
}

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008D RID: 141
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001CDD9 File Offset: 0x0001AFD9
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001CE0C File Offset: 0x0001B00C
		private T GetTokenValue<[Nullable(2)] T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001CE40 File Offset: 0x0001B040
		[return: Nullable(2)]
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._reader.CreateISerializableItem(jtoken, type, this._contract, this._member);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001CE8C File Offset: 0x0001B08C
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = value as JValue;
			return System.Convert.ChangeType((jvalue != null) ? jvalue.Value : value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001CEC2 File Offset: 0x0001B0C2
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001CECB File Offset: 0x0001B0CB
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001CEDD File Offset: 0x0001B0DD
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001CEE6 File Offset: 0x0001B0E6
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001CEEF File Offset: 0x0001B0EF
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001CF01 File Offset: 0x0001B101
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001CF0A File Offset: 0x0001B10A
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001CF13 File Offset: 0x0001B113
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001CF1C File Offset: 0x0001B11C
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001CF25 File Offset: 0x0001B125
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001CF2E File Offset: 0x0001B12E
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001CF37 File Offset: 0x0001B137
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001CF40 File Offset: 0x0001B140
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04000274 RID: 628
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x04000275 RID: 629
		private readonly JsonISerializableContract _contract;

		// Token: 0x04000276 RID: 630
		[Nullable(2)]
		private readonly JsonProperty _member;
	}
}

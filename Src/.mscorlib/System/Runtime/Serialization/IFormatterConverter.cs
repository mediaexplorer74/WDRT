using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the connection between an instance of <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the formatter-provided class best suited to parse the data inside the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
	// Token: 0x02000731 RID: 1841
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051C6 RID: 20934
		object Convert(object value, Type type);

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051C7 RID: 20935
		object Convert(object value, TypeCode typeCode);

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051C8 RID: 20936
		bool ToBoolean(object value);

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051C9 RID: 20937
		char ToChar(object value);

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051CA RID: 20938
		sbyte ToSByte(object value);

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051CB RID: 20939
		byte ToByte(object value);

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051CC RID: 20940
		short ToInt16(object value);

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051CD RID: 20941
		ushort ToUInt16(object value);

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051CE RID: 20942
		int ToInt32(object value);

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051CF RID: 20943
		uint ToUInt32(object value);

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D0 RID: 20944
		long ToInt64(object value);

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D1 RID: 20945
		ulong ToUInt64(object value);

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D2 RID: 20946
		float ToSingle(object value);

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D3 RID: 20947
		double ToDouble(object value);

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D4 RID: 20948
		decimal ToDecimal(object value);

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D5 RID: 20949
		DateTime ToDateTime(object value);

		/// <summary>Converts a value to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x060051D6 RID: 20950
		string ToString(object value);
	}
}

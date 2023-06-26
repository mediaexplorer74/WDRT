using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Represents a base implementation of the <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> interface that uses the <see cref="T:System.Convert" /> class and the <see cref="T:System.IConvertible" /> interface.</summary>
	// Token: 0x0200072C RID: 1836
	[ComVisible(true)]
	public class FormatterConverter : IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is converted.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005190 RID: 20880 RVA: 0x00120926 File Offset: 0x0011EB26
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is converted.</param>
		/// <returns>The converted <paramref name="value" />, or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005191 RID: 20881 RVA: 0x00120942 File Offset: 0x0011EB42
		public object Convert(object value, TypeCode typeCode)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005192 RID: 20882 RVA: 0x0012095E File Offset: 0x0011EB5E
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005193 RID: 20883 RVA: 0x00120979 File Offset: 0x0011EB79
		public char ToChar(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToChar(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005194 RID: 20884 RVA: 0x00120994 File Offset: 0x0011EB94
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToSByte(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005195 RID: 20885 RVA: 0x001209AF File Offset: 0x0011EBAF
		public byte ToByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005196 RID: 20886 RVA: 0x001209CA File Offset: 0x0011EBCA
		public short ToInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt16(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005197 RID: 20887 RVA: 0x001209E5 File Offset: 0x0011EBE5
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt16(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005198 RID: 20888 RVA: 0x00120A00 File Offset: 0x0011EC00
		public int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005199 RID: 20889 RVA: 0x00120A1B File Offset: 0x0011EC1B
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600519A RID: 20890 RVA: 0x00120A36 File Offset: 0x0011EC36
		public long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600519B RID: 20891 RVA: 0x00120A51 File Offset: 0x0011EC51
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600519C RID: 20892 RVA: 0x00120A6C File Offset: 0x0011EC6C
		public float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600519D RID: 20893 RVA: 0x00120A87 File Offset: 0x0011EC87
		public double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600519E RID: 20894 RVA: 0x00120AA2 File Offset: 0x0011ECA2
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600519F RID: 20895 RVA: 0x00120ABD File Offset: 0x0011ECBD
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060051A0 RID: 20896 RVA: 0x00120AD8 File Offset: 0x0011ECD8
		public string ToString(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}
	}
}

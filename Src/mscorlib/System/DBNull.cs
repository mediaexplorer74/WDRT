﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a nonexistent value. This class cannot be inherited.</summary>
	// Token: 0x020000D2 RID: 210
	[ComVisible(true)]
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x00027AC0 File Offset: 0x00025CC0
		private DBNull()
		{
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00027AC8 File Offset: 0x00025CC8
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DBNullSerial"));
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.DBNull" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing information required to serialize the <see cref="T:System.DBNull" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.DBNull" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06000CFC RID: 3324 RVA: 0x00027ADF File Offset: 0x00025CDF
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2, null, null);
		}

		/// <summary>Returns an empty string (<see cref="F:System.String.Empty" />).</summary>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		// Token: 0x06000CFD RID: 3325 RVA: 0x00027AEA File Offset: 0x00025CEA
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Returns an empty string using the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> to be used to format the return value.  
		///  -or-  
		///  <see langword="null" /> to obtain the format information from the current locale setting of the operating system.</param>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		// Token: 0x06000CFE RID: 3326 RVA: 0x00027AF1 File Offset: 0x00025CF1
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		/// <summary>Gets the <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />, which is <see cref="F:System.TypeCode.DBNull" />.</returns>
		// Token: 0x06000CFF RID: 3327 RVA: 0x00027AF8 File Offset: 0x00025CF8
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00027AFB File Offset: 0x00025CFB
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D01 RID: 3329 RVA: 0x00027B0C File Offset: 0x00025D0C
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D02 RID: 3330 RVA: 0x00027B1D File Offset: 0x00025D1D
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00027B2E File Offset: 0x00025D2E
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D04 RID: 3332 RVA: 0x00027B3F File Offset: 0x00025D3F
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D05 RID: 3333 RVA: 0x00027B50 File Offset: 0x00025D50
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D06 RID: 3334 RVA: 0x00027B61 File Offset: 0x00025D61
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D07 RID: 3335 RVA: 0x00027B72 File Offset: 0x00025D72
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D08 RID: 3336 RVA: 0x00027B83 File Offset: 0x00025D83
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D09 RID: 3337 RVA: 0x00027B94 File Offset: 0x00025D94
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D0A RID: 3338 RVA: 0x00027BA5 File Offset: 0x00025DA5
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D0B RID: 3339 RVA: 0x00027BB6 File Offset: 0x00025DB6
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D0C RID: 3340 RVA: 0x00027BC7 File Offset: 0x00025DC7
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000D0D RID: 3341 RVA: 0x00027BD8 File Offset: 0x00025DD8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		/// <summary>Converts the current <see cref="T:System.DBNull" /> object to the specified type.</summary>
		/// <param name="type">The type to convert the current <see cref="T:System.DBNull" /> object to.</param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface and is used to augment the conversion. If <see langword="null" /> is specified, format information is obtained from the current culture.</param>
		/// <returns>The boxed equivalent of the current <see cref="T:System.DBNull" /> object, if that conversion is supported; otherwise, an exception is thrown and no value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06000D0E RID: 3342 RVA: 0x00027BE9 File Offset: 0x00025DE9
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.DBNull" /> class.</summary>
		// Token: 0x0400054A RID: 1354
		public static readonly DBNull Value = new DBNull();
	}
}

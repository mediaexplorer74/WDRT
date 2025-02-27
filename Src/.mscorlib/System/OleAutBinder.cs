﻿using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	internal class OleAutBinder : DefaultBinder
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x00032804 File Offset: 0x00030A04
		[SecuritySafeCritical]
		public override object ChangeType(object value, Type type, CultureInfo cultureInfo)
		{
			System.Variant variant = new System.Variant(value);
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			if (!type.IsPrimitive && type.IsInstanceOfType(value))
			{
				return value;
			}
			Type type2 = value.GetType();
			if (type.IsEnum && type2.IsPrimitive)
			{
				return Enum.Parse(type, value.ToString());
			}
			if (type2 == typeof(DBNull))
			{
				if (type == typeof(DBNull))
				{
					return value;
				}
				if ((type.IsClass && type != typeof(object)) || type.IsInterface)
				{
					return null;
				}
			}
			object obj2;
			try
			{
				object obj = OAVariantLib.ChangeType(variant, type, 16, cultureInfo).ToObject();
				obj2 = obj;
			}
			catch (NotSupportedException)
			{
				throw new COMException(Environment.GetResourceString("Interop.COM_TypeMismatch"), -2147352571);
			}
			return obj2;
		}
	}
}

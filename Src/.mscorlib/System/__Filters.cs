﻿using System;
using System.Reflection;

namespace System
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal class __Filters
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00018FB4 File Offset: 0x000171B4
		internal virtual bool FilterAttribute(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null)
			{
				throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritInt"));
			}
			MemberTypes memberType = m.MemberType;
			if (memberType != MemberTypes.Constructor)
			{
				if (memberType == MemberTypes.Field)
				{
					FieldAttributes fieldAttributes = FieldAttributes.PrivateScope;
					try
					{
						int num = (int)filterCriteria;
						fieldAttributes = (FieldAttributes)num;
					}
					catch
					{
						throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritInt"));
					}
					FieldAttributes attributes = ((FieldInfo)m).Attributes;
					return ((fieldAttributes & FieldAttributes.FieldAccessMask) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.FieldAccessMask) == (fieldAttributes & FieldAttributes.FieldAccessMask)) && ((fieldAttributes & FieldAttributes.Static) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.Literal) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.PinvokeImpl) != FieldAttributes.PrivateScope);
				}
				if (memberType != MemberTypes.Method)
				{
					return false;
				}
			}
			MethodAttributes methodAttributes = MethodAttributes.PrivateScope;
			try
			{
				int num2 = (int)filterCriteria;
				methodAttributes = (MethodAttributes)num2;
			}
			catch
			{
				throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritInt"));
			}
			MethodAttributes methodAttributes2;
			if (m.MemberType == MemberTypes.Method)
			{
				methodAttributes2 = ((MethodInfo)m).Attributes;
			}
			else
			{
				methodAttributes2 = ((ConstructorInfo)m).Attributes;
			}
			return ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.MemberAccessMask) == (methodAttributes & MethodAttributes.MemberAccessMask)) && ((methodAttributes & MethodAttributes.Static) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Static) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Final) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Final) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Abstract) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00019150 File Offset: 0x00017350
		internal virtual bool FilterName(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return text2.StartsWith(text, StringComparison.Ordinal);
			}
			return text2.Equals(text);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000191E8 File Offset: 0x000173E8
		internal virtual bool FilterIgnoreCase(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return string.Compare(text2, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x04000302 RID: 770
		internal static readonly System.__Filters Instance = new System.__Filters();
	}
}

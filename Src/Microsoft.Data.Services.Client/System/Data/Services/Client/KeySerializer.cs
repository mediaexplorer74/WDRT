using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Services.Client
{
	// Token: 0x02000012 RID: 18
	internal abstract class KeySerializer
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000037EC File Offset: 0x000019EC
		internal static KeySerializer Create(UrlConvention urlConvention)
		{
			if (urlConvention.GenerateKeyAsSegment)
			{
				return KeySerializer.SegmentInstance;
			}
			return KeySerializer.DefaultInstance;
		}

		// Token: 0x06000068 RID: 104
		internal abstract void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue);

		// Token: 0x06000069 RID: 105 RVA: 0x00003804 File Offset: 0x00001A04
		private static string GetKeyValueAsString<TProperty>(Func<TProperty, object> getPropertyValue, TProperty property, LiteralFormatter literalFormatter)
		{
			object obj = getPropertyValue(property);
			return literalFormatter.Format(obj);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003824 File Offset: 0x00001A24
		private static void AppendKeyWithParentheses<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
		{
			LiteralFormatter literalFormatter = LiteralFormatter.ForKeys(false);
			builder.Append('(');
			bool flag = true;
			foreach (TProperty tproperty in keyProperties)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(',');
				}
				if (keyProperties.Count != 1)
				{
					builder.Append(getPropertyName(tproperty));
					builder.Append('=');
				}
				string keyValueAsString = KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, tproperty, literalFormatter);
				builder.Append(keyValueAsString);
			}
			builder.Append(')');
		}

		// Token: 0x04000016 RID: 22
		private static readonly KeySerializer.DefaultKeySerializer DefaultInstance = new KeySerializer.DefaultKeySerializer();

		// Token: 0x04000017 RID: 23
		private static readonly KeySerializer.SegmentKeySerializer SegmentInstance = new KeySerializer.SegmentKeySerializer();

		// Token: 0x02000013 RID: 19
		private sealed class DefaultKeySerializer : KeySerializer
		{
			// Token: 0x0600006D RID: 109 RVA: 0x000038E6 File Offset: 0x00001AE6
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
			}
		}

		// Token: 0x02000014 RID: 20
		private sealed class SegmentKeySerializer : KeySerializer
		{
			// Token: 0x0600006F RID: 111 RVA: 0x000038FA File Offset: 0x00001AFA
			internal SegmentKeySerializer()
			{
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00003902 File Offset: 0x00001B02
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				if (keyProperties.Count > 1)
				{
					KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
					return;
				}
				KeySerializer.SegmentKeySerializer.AppendKeyWithSegments<TProperty>(builder, keyProperties, getPropertyValue);
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00003921 File Offset: 0x00001B21
			private static void AppendKeyWithSegments<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, object> getPropertyValue)
			{
				builder.Append('/');
				builder.Append(KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, keyProperties.Single<TProperty>(), LiteralFormatter.ForKeys(true)));
			}
		}
	}
}

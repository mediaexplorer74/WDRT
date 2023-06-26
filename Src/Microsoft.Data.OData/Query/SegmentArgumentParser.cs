using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000033 RID: 51
	internal sealed class SegmentArgumentParser
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000061E0 File Offset: 0x000043E0
		private SegmentArgumentParser()
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000061E8 File Offset: 0x000043E8
		private SegmentArgumentParser(Dictionary<string, string> namedValues, List<string> positionalValues, bool keysAsSegments)
		{
			this.namedValues = namedValues;
			this.positionalValues = positionalValues;
			this.keysAsSegments = keysAsSegments;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006205 File Offset: 0x00004405
		internal bool AreValuesNamed
		{
			get
			{
				return this.namedValues != null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006213 File Offset: 0x00004413
		internal bool IsEmpty
		{
			get
			{
				return this == SegmentArgumentParser.Empty;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000621D File Offset: 0x0000441D
		internal IDictionary<string, string> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006225 File Offset: 0x00004425
		internal IList<string> PositionalValues
		{
			get
			{
				return this.positionalValues;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000622D File Offset: 0x0000442D
		internal int ValueCount
		{
			get
			{
				if (this == SegmentArgumentParser.Empty)
				{
					return 0;
				}
				if (this.namedValues != null)
				{
					return this.namedValues.Count;
				}
				return this.positionalValues.Count;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006258 File Offset: 0x00004458
		internal static bool TryParseKeysFromUri(string text, out SegmentArgumentParser instance)
		{
			return SegmentArgumentParser.TryParseFromUri(text, true, false, out instance);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006264 File Offset: 0x00004464
		internal static SegmentArgumentParser FromSegment(string segmentText)
		{
			return new SegmentArgumentParser(null, new List<string> { segmentText }, true);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006286 File Offset: 0x00004486
		internal static bool TryParseNullableTokens(string text, out SegmentArgumentParser instance)
		{
			return SegmentArgumentParser.TryParseFromUri(text, false, true, out instance);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006294 File Offset: 0x00004494
		internal bool TryConvertValues(IList<IEdmStructuralProperty> keyProperties, out IEnumerable<KeyValuePair<string, object>> keyPairs)
		{
			if (this.NamedValues != null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
				keyPairs = dictionary;
				using (IEnumerator<IEdmStructuralProperty> enumerator = keyProperties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmStructuralProperty edmStructuralProperty = enumerator.Current;
						string text;
						if (!this.NamedValues.TryGetValue(edmStructuralProperty.Name, out text))
						{
							return false;
						}
						object obj;
						if (!this.TryConvertValue(edmStructuralProperty.Type.AsPrimitive(), text, out obj))
						{
							return false;
						}
						dictionary[edmStructuralProperty.Name] = obj;
					}
					return true;
				}
			}
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(this.positionalValues.Count);
			keyPairs = list;
			for (int i = 0; i < keyProperties.Count; i++)
			{
				string text2 = this.positionalValues[i];
				IEdmProperty edmProperty = keyProperties[i];
				object obj2;
				if (!this.TryConvertValue(edmProperty.Type.AsPrimitive(), text2, out obj2))
				{
					return false;
				}
				list.Add(new KeyValuePair<string, object>(edmProperty.Name, obj2));
			}
			return true;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000063C0 File Offset: 0x000045C0
		private bool TryConvertValue(IEdmPrimitiveTypeReference primitiveType, string valueText, out object convertedValue)
		{
			Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType((IEdmPrimitiveType)primitiveType.Definition, primitiveType.IsNullable);
			LiteralParser literalParser = LiteralParser.ForKeys(this.keysAsSegments);
			return literalParser.TryParseLiteral(primitiveClrType, valueText, out convertedValue);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000063FC File Offset: 0x000045FC
		private static bool TryParseFromUri(string text, bool allowNamedValues, bool allowNull, out SegmentArgumentParser instance)
		{
			Dictionary<string, string> dictionary = null;
			List<string> list = null;
			ExpressionLexer expressionLexer = new ExpressionLexer(text, true, false);
			ExpressionToken expressionToken = expressionLexer.CurrentToken;
			if (expressionToken.Kind == ExpressionTokenKind.End)
			{
				instance = SegmentArgumentParser.Empty;
				return true;
			}
			instance = null;
			for (;;)
			{
				if (expressionToken.Kind == ExpressionTokenKind.Identifier && allowNamedValues)
				{
					if (list != null)
					{
						break;
					}
					string identifier = expressionLexer.CurrentToken.GetIdentifier();
					expressionLexer.NextToken();
					if (expressionLexer.CurrentToken.Kind != ExpressionTokenKind.Equal)
					{
						return false;
					}
					expressionLexer.NextToken();
					if (!expressionLexer.CurrentToken.IsKeyValueToken)
					{
						return false;
					}
					string text2 = expressionLexer.CurrentToken.Text;
					SegmentArgumentParser.CreateIfNull<Dictionary<string, string>>(ref dictionary);
					if (dictionary.ContainsKey(identifier))
					{
						return false;
					}
					dictionary.Add(identifier, text2);
				}
				else
				{
					if (!expressionToken.IsKeyValueToken && (!allowNull || expressionToken.Kind != ExpressionTokenKind.NullLiteral))
					{
						return false;
					}
					if (dictionary != null)
					{
						return false;
					}
					SegmentArgumentParser.CreateIfNull<List<string>>(ref list);
					list.Add(expressionLexer.CurrentToken.Text);
				}
				expressionLexer.NextToken();
				expressionToken = expressionLexer.CurrentToken;
				if (expressionToken.Kind == ExpressionTokenKind.Comma)
				{
					expressionLexer.NextToken();
					expressionToken = expressionLexer.CurrentToken;
					if (expressionToken.Kind == ExpressionTokenKind.End)
					{
						return false;
					}
				}
				if (expressionToken.Kind == ExpressionTokenKind.End)
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			instance = new SegmentArgumentParser(dictionary, list, false);
			return true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006538 File Offset: 0x00004738
		private static void CreateIfNull<T>(ref T value) where T : new()
		{
			if (value == null)
			{
				value = ((default(T) == null) ? new T() : default(T));
			}
		}

		// Token: 0x04000065 RID: 101
		private static readonly SegmentArgumentParser Empty = new SegmentArgumentParser();

		// Token: 0x04000066 RID: 102
		private readonly Dictionary<string, string> namedValues;

		// Token: 0x04000067 RID: 103
		private readonly List<string> positionalValues;

		// Token: 0x04000068 RID: 104
		private readonly bool keysAsSegments;
	}
}

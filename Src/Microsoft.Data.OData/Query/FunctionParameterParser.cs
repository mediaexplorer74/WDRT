using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000011 RID: 17
	internal static class FunctionParameterParser
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003050 File Offset: 0x00001250
		internal static bool TrySplitFunctionParameters(this ExpressionLexer lexer, out ICollection<FunctionParameterToken> splitParameters)
		{
			return lexer.TrySplitFunctionParameters(ExpressionTokenKind.CloseParen, out splitParameters);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003069 File Offset: 0x00001269
		internal static bool TryParseFunctionParameters(ICollection<FunctionParameterToken> splitParameters, ODataUriParserConfiguration configuration, IEdmFunctionImport functionImport, out ICollection<FunctionParameterToken> parsedParameters)
		{
			return FunctionParameterParser.TryParseFunctionParameters<FunctionParameterToken>(splitParameters, configuration, functionImport, (string paramName, object convertedValue) => new FunctionParameterToken(paramName, new LiteralToken(convertedValue)), out parsedParameters);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003094 File Offset: 0x00001294
		internal static bool TrySplitFunctionParameters(string functionName, string parenthesisExpression, out ICollection<FunctionParameterToken> splitParameters)
		{
			ExpressionLexer expressionLexer = new ExpressionLexer(parenthesisExpression, true, false, true);
			if (expressionLexer.CurrentToken.IsFunctionParameterToken)
			{
				splitParameters = null;
				return false;
			}
			return expressionLexer.TrySplitFunctionParameters(ExpressionTokenKind.End, out splitParameters);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030D1 File Offset: 0x000012D1
		internal static bool TryParseFunctionParameters(ICollection<FunctionParameterToken> splitParameters, ODataUriParserConfiguration configuration, IEdmFunctionImport functionImport, out ICollection<OperationSegmentParameter> parsedParameters)
		{
			return FunctionParameterParser.TryParseFunctionParameters<OperationSegmentParameter>(splitParameters, configuration, functionImport, (string paramName, object convertedValue) => new OperationSegmentParameter(paramName, convertedValue), out parsedParameters);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000030FC File Offset: 0x000012FC
		private static bool TrySplitFunctionParameters(this ExpressionLexer lexer, ExpressionTokenKind endTokenKind, out ICollection<FunctionParameterToken> splitParameters)
		{
			List<FunctionParameterToken> list = new List<FunctionParameterToken>();
			splitParameters = list;
			ExpressionToken expressionToken = lexer.CurrentToken;
			if (expressionToken.Kind == endTokenKind)
			{
				return true;
			}
			if (expressionToken.Kind != ExpressionTokenKind.Identifier || lexer.PeekNextToken().Kind != ExpressionTokenKind.Equal)
			{
				return false;
			}
			while (expressionToken.Kind != endTokenKind)
			{
				lexer.ValidateToken(ExpressionTokenKind.Identifier);
				string identifier = lexer.CurrentToken.GetIdentifier();
				lexer.NextToken();
				lexer.ValidateToken(ExpressionTokenKind.Equal);
				lexer.NextToken();
				QueryToken queryToken;
				if (!FunctionParameterParser.TryCreateParameterValueToken(lexer.CurrentToken, out queryToken))
				{
					throw new ODataException(Strings.ExpressionLexer_SyntaxError(lexer.Position, lexer.ExpressionText));
				}
				list.Add(new FunctionParameterToken(identifier, queryToken));
				lexer.NextToken();
				expressionToken = lexer.CurrentToken;
				if (expressionToken.Kind == ExpressionTokenKind.Comma)
				{
					lexer.NextToken();
					expressionToken = lexer.CurrentToken;
					if (expressionToken.Kind == endTokenKind)
					{
						throw new ODataException(Strings.ExpressionLexer_SyntaxError(lexer.Position, lexer.ExpressionText));
					}
				}
			}
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003230 File Offset: 0x00001430
		private static bool TryParseFunctionParameters<TParam>(ICollection<FunctionParameterToken> splitParameters, ODataUriParserConfiguration configuration, IEdmFunctionImport functionImport, Func<string, object, TParam> createParameter, out ICollection<TParam> parsedParameters)
		{
			parsedParameters = new List<TParam>(splitParameters.Count);
			using (IEnumerator<FunctionParameterToken> enumerator = splitParameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FunctionParameterToken splitParameter = enumerator.Current;
					IEdmFunctionParameter edmFunctionParameter = null;
					try
					{
						edmFunctionParameter = functionImport.FindParameter(splitParameter.ParameterName);
					}
					catch (InvalidOperationException ex)
					{
						throw new ODataException(Strings.FunctionCallParser_DuplicateParameterName, ex);
					}
					IEdmTypeReference type = edmFunctionParameter.Type;
					TParam tparam;
					if (!FunctionParameterParser.TryCreateParameter<TParam>(splitParameter, configuration, type, (object o) => createParameter(splitParameter.ParameterName, o), out tparam))
					{
						return false;
					}
					parsedParameters.Add(tparam);
				}
			}
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003324 File Offset: 0x00001524
		private static bool TryCreateParameterValueToken(ExpressionToken expressionToken, out QueryToken parameterValue)
		{
			if (expressionToken.Kind == ExpressionTokenKind.ParameterAlias)
			{
				parameterValue = new FunctionParameterAliasToken(expressionToken.Text);
				return true;
			}
			if (expressionToken.IsFunctionParameterToken)
			{
				parameterValue = new RawFunctionParameterValueToken(expressionToken.Text);
				return true;
			}
			parameterValue = null;
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003360 File Offset: 0x00001560
		private static bool TryCreateParameter<TParam>(FunctionParameterToken parameterToken, ODataUriParserConfiguration configuration, IEdmTypeReference expectedType, Func<object, TParam> createParameter, out TParam parameter)
		{
			QueryToken valueToken = parameterToken.ValueToken;
			object obj;
			if (valueToken.Kind == QueryTokenKind.FunctionParameterAlias && configuration.FunctionParameterAliasCallback == null)
			{
				obj = new ODataUnresolvedFunctionParameterAlias(((FunctionParameterAliasToken)valueToken).Alias, expectedType);
			}
			else
			{
				string text;
				if (valueToken.Kind == QueryTokenKind.FunctionParameterAlias)
				{
					text = configuration.FunctionParameterAliasCallback(((FunctionParameterAliasToken)valueToken).Alias);
				}
				else
				{
					if (valueToken.Kind != QueryTokenKind.RawFunctionParameterValue)
					{
						parameter = default(TParam);
						return false;
					}
					text = ((RawFunctionParameterValueToken)valueToken).RawText;
				}
				if (text == null)
				{
					obj = null;
				}
				else
				{
					obj = ODataUriUtils.ConvertFromUriLiteral(text, ODataVersion.V3, configuration.Model, expectedType);
				}
			}
			parameter = createParameter(obj);
			return true;
		}
	}
}

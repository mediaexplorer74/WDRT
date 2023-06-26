using System;
using System.Globalization;
using Microsoft.Xaml.Behaviors.Core;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000007 RID: 7
	internal static class ComparisonLogic
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000026C0 File Offset: 0x000008C0
		internal static bool EvaluateImpl(object leftOperand, ComparisonConditionType operatorType, object rightOperand)
		{
			bool flag = false;
			if (leftOperand != null)
			{
				Type type = leftOperand.GetType();
				if (rightOperand != null)
				{
					rightOperand = TypeConverterHelper.DoConversionFrom(TypeConverterHelper.GetTypeConverter(type), rightOperand);
				}
			}
			IComparable comparable = leftOperand as IComparable;
			IComparable comparable2 = rightOperand as IComparable;
			if (comparable != null && comparable2 != null)
			{
				return ComparisonLogic.EvaluateComparable(comparable, operatorType, comparable2);
			}
			switch (operatorType)
			{
			case ComparisonConditionType.Equal:
				flag = object.Equals(leftOperand, rightOperand);
				break;
			case ComparisonConditionType.NotEqual:
				flag = !object.Equals(leftOperand, rightOperand);
				break;
			case ComparisonConditionType.LessThan:
			case ComparisonConditionType.LessThanOrEqual:
			case ComparisonConditionType.GreaterThan:
			case ComparisonConditionType.GreaterThanOrEqual:
				if (comparable == null && comparable2 == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.InvalidOperands, new object[]
					{
						(leftOperand != null) ? leftOperand.GetType().Name : "null",
						(rightOperand != null) ? rightOperand.GetType().Name : "null",
						operatorType.ToString()
					}));
				}
				if (comparable == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.InvalidLeftOperand, new object[]
					{
						(leftOperand != null) ? leftOperand.GetType().Name : "null",
						operatorType.ToString()
					}));
				}
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.InvalidRightOperand, new object[]
				{
					(rightOperand != null) ? rightOperand.GetType().Name : "null",
					operatorType.ToString()
				}));
			}
			return flag;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002834 File Offset: 0x00000A34
		private static bool EvaluateComparable(IComparable leftOperand, ComparisonConditionType operatorType, IComparable rightOperand)
		{
			object obj = null;
			try
			{
				obj = Convert.ChangeType(rightOperand, leftOperand.GetType(), CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
			}
			catch (InvalidCastException)
			{
			}
			if (obj == null)
			{
				return operatorType == ComparisonConditionType.NotEqual;
			}
			int num = leftOperand.CompareTo((IComparable)obj);
			bool flag = false;
			switch (operatorType)
			{
			case ComparisonConditionType.Equal:
				flag = num == 0;
				break;
			case ComparisonConditionType.NotEqual:
				flag = num != 0;
				break;
			case ComparisonConditionType.LessThan:
				flag = num < 0;
				break;
			case ComparisonConditionType.LessThanOrEqual:
				flag = num <= 0;
				break;
			case ComparisonConditionType.GreaterThan:
				flag = num > 0;
				break;
			case ComparisonConditionType.GreaterThanOrEqual:
				flag = num >= 0;
				break;
			}
			return flag;
		}
	}
}

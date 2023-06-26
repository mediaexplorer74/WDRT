using System;
using System.Security;
using System.Threading;

namespace System.Runtime.Versioning
{
	// Token: 0x02000724 RID: 1828
	internal static class MultitargetingHelpers
	{
		// Token: 0x0600517D RID: 20861 RVA: 0x001207E0 File Offset: 0x0011E9E0
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = MultitargetingHelpers.defaultConverter(type);
				}
			}
			return text;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x00120834 File Offset: 0x0011EA34
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00120869 File Offset: 0x0011EA69
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}

		// Token: 0x04002436 RID: 9270
		private static Func<Type, string> defaultConverter = (Type t) => t.AssemblyQualifiedName;
	}
}

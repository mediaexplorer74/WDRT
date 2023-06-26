using System;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x02000111 RID: 273
	internal static class Error
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00024C1C File Offset: 0x00022E1C
		internal static ArgumentException Argument(string message, string parameterName)
		{
			return Error.Trace<ArgumentException>(new ArgumentException(message, parameterName));
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00024C2A File Offset: 0x00022E2A
		internal static InvalidOperationException InvalidOperation(string message)
		{
			return Error.Trace<InvalidOperationException>(new InvalidOperationException(message));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00024C37 File Offset: 0x00022E37
		internal static InvalidOperationException InvalidOperation(string message, Exception innerException)
		{
			return Error.Trace<InvalidOperationException>(new InvalidOperationException(message, innerException));
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00024C45 File Offset: 0x00022E45
		internal static NotSupportedException NotSupported(string message)
		{
			return Error.Trace<NotSupportedException>(new NotSupportedException(message));
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00024C52 File Offset: 0x00022E52
		internal static void ThrowObjectDisposed(Type type)
		{
			throw Error.Trace<ObjectDisposedException>(new ObjectDisposedException(type.ToString()));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00024C64 File Offset: 0x00022E64
		internal static InvalidOperationException HttpHeaderFailure(int errorCode, string message)
		{
			return Error.Trace<InvalidOperationException>(new InvalidOperationException(message));
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00024C71 File Offset: 0x00022E71
		internal static NotSupportedException MethodNotSupported(MethodCallExpression m)
		{
			return Error.NotSupported(Strings.ALinq_MethodNotSupported(m.Method.Name));
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00024C88 File Offset: 0x00022E88
		internal static void ThrowBatchUnexpectedContent(InternalError value)
		{
			throw Error.InvalidOperation(Strings.Batch_UnexpectedContent((int)value));
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00024C9A File Offset: 0x00022E9A
		internal static void ThrowBatchExpectedResponse(InternalError value)
		{
			throw Error.InvalidOperation(Strings.Batch_ExpectedResponse((int)value));
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00024CAC File Offset: 0x00022EAC
		internal static InvalidOperationException InternalError(InternalError value)
		{
			return Error.InvalidOperation(Strings.Context_InternalError((int)value));
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00024CBE File Offset: 0x00022EBE
		internal static void ThrowInternalError(InternalError value)
		{
			throw Error.InternalError(value);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00024CC6 File Offset: 0x00022EC6
		private static T Trace<T>(T exception) where T : Exception
		{
			return exception;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00024CC9 File Offset: 0x00022EC9
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00024CD1 File Offset: 0x00022ED1
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00024CD9 File Offset: 0x00022ED9
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00024CE0 File Offset: 0x00022EE0
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}

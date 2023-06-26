using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	/// <summary>Contains static methods for representing program contracts such as preconditions, postconditions, and object invariants.</summary>
	// Token: 0x02000413 RID: 1043
	[__DynamicallyInvokable]
	public static class Contract
	{
		/// <summary>Instructs code analysis tools to assume that the specified condition is <see langword="true" />, even if it cannot be statically proven to always be <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to assume <see langword="true" />.</param>
		// Token: 0x06003419 RID: 13337 RVA: 0x000C7FC5 File Offset: 0x000C61C5
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Assume(bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, null, null, null);
			}
		}

		/// <summary>Instructs code analysis tools to assume that a condition is <see langword="true" />, even if it cannot be statically proven to always be <see langword="true" />, and displays a message if the assumption fails.</summary>
		/// <param name="condition">The conditional expression to assume <see langword="true" />.</param>
		/// <param name="userMessage">The message to post if the assumption fails.</param>
		// Token: 0x0600341A RID: 13338 RVA: 0x000C7FD3 File Offset: 0x000C61D3
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Assume(bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, userMessage, null, null);
			}
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, follows the escalation policy set for the analyzer.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x0600341B RID: 13339 RVA: 0x000C7FE1 File Offset: 0x000C61E1
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, null, null, null);
			}
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, follows the escalation policy set by the analyzer and displays the specified message.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">A message to display if the condition is not met.</param>
		// Token: 0x0600341C RID: 13340 RVA: 0x000C7FEF File Offset: 0x000C61EF
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Assert(bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, userMessage, null, null);
			}
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x0600341D RID: 13341 RVA: 0x000C7FFD File Offset: 0x000C61FD
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Requires(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and displays a message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is <see langword="false" />.</param>
		// Token: 0x0600341E RID: 13342 RVA: 0x000C800A File Offset: 0x000C620A
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Requires(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and throws an exception if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <typeparam name="TException">The exception to throw if the condition is <see langword="false" />.</typeparam>
		// Token: 0x0600341F RID: 13343 RVA: 0x000C8017 File Offset: 0x000C6217
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Requires<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and throws an exception with the provided message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is <see langword="false" />.</param>
		/// <typeparam name="TException">The exception to throw if the condition is <see langword="false" />.</typeparam>
		// Token: 0x06003420 RID: 13344 RVA: 0x000C8024 File Offset: 0x000C6224
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Requires<TException>(bool condition, string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		/// <summary>Specifies a postcondition contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test. The expression may include <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" />, <see cref="M:System.Diagnostics.Contracts.Contract.ValueAtReturn``1(``0@)" />, and <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> values.</param>
		// Token: 0x06003421 RID: 13345 RVA: 0x000C8031 File Offset: 0x000C6231
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Ensures(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		/// <summary>Specifies a postcondition contract for a provided exit condition and a message to display if the condition is <see langword="false" />.</summary>
		/// <param name="condition">The conditional expression to test. The expression may include <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" /> and <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> values.</param>
		/// <param name="userMessage">The message to display if the expression is not <see langword="true" />.</param>
		// Token: 0x06003422 RID: 13346 RVA: 0x000C803E File Offset: 0x000C623E
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Ensures(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		/// <summary>Specifies a postcondition contract for the enclosing method or property, based on the provided exception and condition.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <typeparam name="TException">The type of exception that invokes the postcondition check.</typeparam>
		// Token: 0x06003423 RID: 13347 RVA: 0x000C804B File Offset: 0x000C624B
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		/// <summary>Specifies a postcondition contract and a message to display if the condition is <see langword="false" /> for the enclosing method or property, based on the provided exception and condition.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the expression is <see langword="false" />.</param>
		/// <typeparam name="TException">The type of exception that invokes the postcondition check.</typeparam>
		// Token: 0x06003424 RID: 13348 RVA: 0x000C8058 File Offset: 0x000C6258
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void EnsuresOnThrow<TException>(bool condition, string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		/// <summary>Represents the return value of a method or property.</summary>
		/// <typeparam name="T">Type of return value of the enclosing method or property.</typeparam>
		/// <returns>Return value of the enclosing method or property.</returns>
		// Token: 0x06003425 RID: 13349 RVA: 0x000C8068 File Offset: 0x000C6268
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static T Result<T>()
		{
			return default(T);
		}

		/// <summary>Represents the final (output) value of an <see langword="out" /> parameter when returning from a method.</summary>
		/// <param name="value">The <see langword="out" /> parameter.</param>
		/// <typeparam name="T">The type of the <see langword="out" /> parameter.</typeparam>
		/// <returns>The output value of the <see langword="out" /> parameter.</returns>
		// Token: 0x06003426 RID: 13350 RVA: 0x000C807E File Offset: 0x000C627E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static T ValueAtReturn<T>(out T value)
		{
			value = default(T);
			return value;
		}

		/// <summary>Represents values as they were at the start of a method or property.</summary>
		/// <param name="value">The value to represent (field or parameter).</param>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <returns>The value of the parameter or field at the start of a method or property.</returns>
		// Token: 0x06003427 RID: 13351 RVA: 0x000C8090 File Offset: 0x000C6290
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static T OldValue<T>(T value)
		{
			return default(T);
		}

		/// <summary>Specifies an invariant contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x06003428 RID: 13352 RVA: 0x000C80A6 File Offset: 0x000C62A6
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Invariant(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		/// <summary>Specifies an invariant contract for the enclosing method or property, and displays a message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is <see langword="false" />.</param>
		// Token: 0x06003429 RID: 13353 RVA: 0x000C80B3 File Offset: 0x000C62B3
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Invariant(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		/// <summary>Determines whether a particular condition is valid for all integers in a specified range.</summary>
		/// <param name="fromInclusive">The first integer to pass to <paramref name="predicate" />.</param>
		/// <param name="toExclusive">One more than the last integer to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of the integers in the specified range.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="predicate" /> returns <see langword="true" /> for all integers starting from <paramref name="fromInclusive" /> to <paramref name="toExclusive" /> - 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toExclusive" /> is less than <paramref name="fromInclusive" />.</exception>
		// Token: 0x0600342A RID: 13354 RVA: 0x000C80C0 File Offset: 0x000C62C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ToExclusiveLessThanFromExclusive"));
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (!predicate(i))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether all the elements in a collection exist within a function.</summary>
		/// <param name="collection">The collection from which elements of type T will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of all the elements in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if and only if <paramref name="predicate" /> returns <see langword="true" /> for all elements of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x0600342B RID: 13355 RVA: 0x000C8108 File Offset: 0x000C6308
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (!predicate(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a specified test is true for any integer within a range of integers.</summary>
		/// <param name="fromInclusive">The first integer to pass to <paramref name="predicate" />.</param>
		/// <param name="toExclusive">One more than the last integer to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for any value of the integer in the specified range.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="predicate" /> returns <see langword="true" /> for any integer starting from <paramref name="fromInclusive" /> to <paramref name="toExclusive" /> - 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toExclusive" /> is less than <paramref name="fromInclusive" />.</exception>
		// Token: 0x0600342C RID: 13356 RVA: 0x000C8178 File Offset: 0x000C6378
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ToExclusiveLessThanFromExclusive"));
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (predicate(i))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether an element within a collection of elements exists within a function.</summary>
		/// <param name="collection">The collection from which elements of type T will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for an element in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if and only if <paramref name="predicate" /> returns <see langword="true" /> for any element of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x0600342D RID: 13357 RVA: 0x000C81C0 File Offset: 0x000C63C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Marks the end of the contract section when a method's contracts contain only preconditions in the <see langword="if" />-<see langword="then" />-<see langword="throw" /> form.</summary>
		// Token: 0x0600342E RID: 13358 RVA: 0x000C8230 File Offset: 0x000C6430
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void EndContractBlock()
		{
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000C8234 File Offset: 0x000C6434
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static void ReportFailure(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { failureKind }), "failureKind");
			}
			string text = ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
			if (text == null)
			{
				return;
			}
			ContractHelper.TriggerFailure(failureKind, text, userMessage, conditionText, innerException);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000C8288 File Offset: 0x000C6488
		[SecuritySafeCritical]
		private static void AssertMustUseRewriter(ContractFailureKind kind, string contractKind)
		{
			if (Contract._assertingMustUseRewriter)
			{
				System.Diagnostics.Assert.Fail("Asserting that we must use the rewriter went reentrant.", "Didn't rewrite this mscorlib?");
			}
			Contract._assertingMustUseRewriter = true;
			Assembly assembly = typeof(Contract).Assembly;
			StackTrace stackTrace = new StackTrace();
			Assembly assembly2 = null;
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				Assembly assembly3 = stackTrace.GetFrame(i).GetMethod().DeclaringType.Assembly;
				if (assembly3 != assembly)
				{
					assembly2 = assembly3;
					break;
				}
			}
			if (assembly2 == null)
			{
				assembly2 = assembly;
			}
			string name = assembly2.GetName().Name;
			ContractHelper.TriggerFailure(kind, Environment.GetResourceString("MustUseCCRewrite", new object[] { contractKind, name }), null, null, null);
			Contract._assertingMustUseRewriter = false;
		}

		/// <summary>Occurs when a contract fails.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06003431 RID: 13361 RVA: 0x000C8344 File Offset: 0x000C6544
		// (remove) Token: 0x06003432 RID: 13362 RVA: 0x000C834C File Offset: 0x000C654C
		[__DynamicallyInvokable]
		public static event EventHandler<ContractFailedEventArgs> ContractFailed
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			add
			{
				ContractHelper.InternalContractFailed += value;
			}
			[SecurityCritical]
			[__DynamicallyInvokable]
			remove
			{
				ContractHelper.InternalContractFailed -= value;
			}
		}

		// Token: 0x04001717 RID: 5911
		[ThreadStatic]
		private static bool _assertingMustUseRewriter;
	}
}

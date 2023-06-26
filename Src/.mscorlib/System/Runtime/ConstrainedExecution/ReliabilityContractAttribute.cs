using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Defines a contract for reliability between the author of some code, and the developers who have a dependency on that code.</summary>
	// Token: 0x0200072A RID: 1834
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.ReliabilityContractAttribute" /> class with the specified <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> guarantee and <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> value.</summary>
		/// <param name="consistencyGuarantee">One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values.</param>
		/// <param name="cer">One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values.</param>
		// Token: 0x0600518B RID: 20875 RVA: 0x001208F0 File Offset: 0x0011EAF0
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this._consistency = consistencyGuarantee;
			this._cer = cer;
		}

		/// <summary>Gets the value of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> reliability contract.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x0600518C RID: 20876 RVA: 0x00120906 File Offset: 0x0011EB06
		public Consistency ConsistencyGuarantee
		{
			get
			{
				return this._consistency;
			}
		}

		/// <summary>Gets the value that determines the behavior of a method, type, or assembly when called under a Constrained Execution Region (CER).</summary>
		/// <returns>One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x0012090E File Offset: 0x0011EB0E
		public Cer Cer
		{
			get
			{
				return this._cer;
			}
		}

		// Token: 0x04002440 RID: 9280
		private Consistency _consistency;

		// Token: 0x04002441 RID: 9281
		private Cer _cer;
	}
}

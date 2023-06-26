using System;

namespace System.Threading
{
	/// <summary>The class that provides data change information to <see cref="T:System.Threading.AsyncLocal`1" /> instances that register for change notifications.</summary>
	/// <typeparam name="T">The type of the data.</typeparam>
	// Token: 0x020004E5 RID: 1253
	[__DynamicallyInvokable]
	public struct AsyncLocalValueChangedArgs<T>
	{
		/// <summary>Gets the data's previous value.</summary>
		/// <returns>The data's previous value.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000E3C6E File Offset: 0x000E1E6E
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x000E3C76 File Offset: 0x000E1E76
		[__DynamicallyInvokable]
		public T PreviousValue
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		/// <summary>Gets the data's current value.</summary>
		/// <returns>The data's current value.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x000E3C7F File Offset: 0x000E1E7F
		// (set) Token: 0x06003B98 RID: 15256 RVA: 0x000E3C87 File Offset: 0x000E1E87
		[__DynamicallyInvokable]
		public T CurrentValue
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		/// <summary>Returns a value that indicates whether the value changes because of a change of execution context.</summary>
		/// <returns>
		///   <see langword="true" /> if the value changed because of a change of execution context; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x000E3C90 File Offset: 0x000E1E90
		// (set) Token: 0x06003B9A RID: 15258 RVA: 0x000E3C98 File Offset: 0x000E1E98
		[__DynamicallyInvokable]
		public bool ThreadContextChanged
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		// Token: 0x06003B9B RID: 15259 RVA: 0x000E3CA1 File Offset: 0x000E1EA1
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}
	}
}

using System;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Suppresses reporting of a specific static analysis tool rule violation, allowing multiple suppressions on a single code artifact.</summary>
	// Token: 0x02000418 RID: 1048
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	[__DynamicallyInvokable]
	public sealed class SuppressMessageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> class, specifying the category of the static analysis tool and the identifier for an analysis rule.</summary>
		/// <param name="category">The category for the attribute.</param>
		/// <param name="checkId">The identifier of the analysis tool rule the attribute applies to.</param>
		// Token: 0x06003446 RID: 13382 RVA: 0x000C84BD File Offset: 0x000C66BD
		[__DynamicallyInvokable]
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.category = category;
			this.checkId = checkId;
		}

		/// <summary>Gets the category identifying the classification of the attribute.</summary>
		/// <returns>The category identifying the attribute.</returns>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000C84D3 File Offset: 0x000C66D3
		[__DynamicallyInvokable]
		public string Category
		{
			[__DynamicallyInvokable]
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the identifier of the static analysis tool rule to be suppressed.</summary>
		/// <returns>The identifier of the static analysis tool rule to be suppressed.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000C84DB File Offset: 0x000C66DB
		[__DynamicallyInvokable]
		public string CheckId
		{
			[__DynamicallyInvokable]
			get
			{
				return this.checkId;
			}
		}

		/// <summary>Gets or sets the scope of the code that is relevant for the attribute.</summary>
		/// <returns>The scope of the code that is relevant for the attribute.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000C84E3 File Offset: 0x000C66E3
		// (set) Token: 0x0600344A RID: 13386 RVA: 0x000C84EB File Offset: 0x000C66EB
		[__DynamicallyInvokable]
		public string Scope
		{
			[__DynamicallyInvokable]
			get
			{
				return this.scope;
			}
			[__DynamicallyInvokable]
			set
			{
				this.scope = value;
			}
		}

		/// <summary>Gets or sets a fully qualified path that represents the target of the attribute.</summary>
		/// <returns>A fully qualified path that represents the target of the attribute.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000C84F4 File Offset: 0x000C66F4
		// (set) Token: 0x0600344C RID: 13388 RVA: 0x000C84FC File Offset: 0x000C66FC
		[__DynamicallyInvokable]
		public string Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.target;
			}
			[__DynamicallyInvokable]
			set
			{
				this.target = value;
			}
		}

		/// <summary>Gets or sets an optional argument expanding on exclusion criteria.</summary>
		/// <returns>A string containing the expanded exclusion criteria.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000C8505 File Offset: 0x000C6705
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x000C850D File Offset: 0x000C670D
		[__DynamicallyInvokable]
		public string MessageId
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageId;
			}
			[__DynamicallyInvokable]
			set
			{
				this.messageId = value;
			}
		}

		/// <summary>Gets or sets the justification for suppressing the code analysis message.</summary>
		/// <returns>The justification for suppressing the message.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000C8516 File Offset: 0x000C6716
		// (set) Token: 0x06003450 RID: 13392 RVA: 0x000C851E File Offset: 0x000C671E
		[__DynamicallyInvokable]
		public string Justification
		{
			[__DynamicallyInvokable]
			get
			{
				return this.justification;
			}
			[__DynamicallyInvokable]
			set
			{
				this.justification = value;
			}
		}

		// Token: 0x04001729 RID: 5929
		private string category;

		// Token: 0x0400172A RID: 5930
		private string justification;

		// Token: 0x0400172B RID: 5931
		private string checkId;

		// Token: 0x0400172C RID: 5932
		private string scope;

		// Token: 0x0400172D RID: 5933
		private string target;

		// Token: 0x0400172E RID: 5934
		private string messageId;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	/// <summary>The exception that is thrown when the execution time of a regular expression pattern-matching method exceeds its time-out interval.</summary>
	// Token: 0x0200069F RID: 1695
	[global::__DynamicallyInvokable]
	[Serializable]
	public class RegexMatchTimeoutException : TimeoutException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with information about the regular expression pattern, the input text, and the time-out interval.</summary>
		/// <param name="regexInput">The input text processed by the regular expression engine when the time-out occurred.</param>
		/// <param name="regexPattern">The pattern used by the regular expression engine when the time-out occurred.</param>
		/// <param name="matchTimeout">The time-out interval.</param>
		// Token: 0x06003F16 RID: 16150 RVA: 0x00107171 File Offset: 0x00105371
		[global::__DynamicallyInvokable]
		public RegexMatchTimeoutException(string regexInput, string regexPattern, TimeSpan matchTimeout)
			: base(SR.GetString("RegexMatchTimeoutException_Occurred"))
		{
			this.Init(regexInput, regexPattern, matchTimeout);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a system-supplied message.</summary>
		// Token: 0x06003F17 RID: 16151 RVA: 0x00107199 File Offset: 0x00105399
		[global::__DynamicallyInvokable]
		public RegexMatchTimeoutException()
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x06003F18 RID: 16152 RVA: 0x001071B4 File Offset: 0x001053B4
		[global::__DynamicallyInvokable]
		public RegexMatchTimeoutException(string message)
			: base(message)
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception.</param>
		// Token: 0x06003F19 RID: 16153 RVA: 0x001071D0 File Offset: 0x001053D0
		[global::__DynamicallyInvokable]
		public RegexMatchTimeoutException(string message, Exception inner)
			: base(message, inner)
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		// Token: 0x06003F1A RID: 16154 RVA: 0x001071F0 File Offset: 0x001053F0
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected RegexMatchTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			string @string = info.GetString("regexInput");
			string string2 = info.GetString("regexPattern");
			TimeSpan timeSpan = TimeSpan.FromTicks(info.GetInt64("timeoutTicks"));
			this.Init(@string, string2, timeSpan);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> object.</summary>
		/// <param name="si">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06003F1B RID: 16155 RVA: 0x00107244 File Offset: 0x00105444
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
			si.AddValue("regexInput", this.regexInput);
			si.AddValue("regexPattern", this.regexPattern);
			si.AddValue("timeoutTicks", this.matchTimeout.Ticks);
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x00107291 File Offset: 0x00105491
		private void Init()
		{
			this.Init("", "", TimeSpan.FromTicks(-1L));
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x001072AA File Offset: 0x001054AA
		private void Init(string input, string pattern, TimeSpan timeout)
		{
			this.regexInput = input;
			this.regexPattern = pattern;
			this.matchTimeout = timeout;
		}

		/// <summary>Gets the regular expression pattern that was used in the matching operation when the time-out occurred.</summary>
		/// <returns>The regular expression pattern.</returns>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x001072C1 File Offset: 0x001054C1
		[global::__DynamicallyInvokable]
		public string Pattern
		{
			[global::__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.regexPattern;
			}
		}

		/// <summary>Gets the input text that the regular expression engine was processing when the time-out occurred.</summary>
		/// <returns>The regular expression input text.</returns>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x001072C9 File Offset: 0x001054C9
		[global::__DynamicallyInvokable]
		public string Input
		{
			[global::__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.regexInput;
			}
		}

		/// <summary>Gets the time-out interval for a regular expression match.</summary>
		/// <returns>The time-out interval.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x001072D1 File Offset: 0x001054D1
		[global::__DynamicallyInvokable]
		public TimeSpan MatchTimeout
		{
			[global::__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.matchTimeout;
			}
		}

		// Token: 0x04002DEF RID: 11759
		private string regexInput;

		// Token: 0x04002DF0 RID: 11760
		private string regexPattern;

		// Token: 0x04002DF1 RID: 11761
		private TimeSpan matchTimeout = TimeSpan.FromTicks(-1L);
	}
}

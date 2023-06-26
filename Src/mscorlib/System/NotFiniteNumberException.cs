using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when a floating-point value is positive infinity, negative infinity, or Not-a-Number (NaN).</summary>
	// Token: 0x02000114 RID: 276
	[ComVisible(true)]
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class.</summary>
		// Token: 0x06001075 RID: 4213 RVA: 0x000314E0 File Offset: 0x0002F6E0
		public NotFiniteNumberException()
			: base(Environment.GetResourceString("Arg_NotFiniteNumberException"))
		{
			this._offendingNumber = 0.0;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with the invalid number.</summary>
		/// <param name="offendingNumber">The value of the argument that caused the exception.</param>
		// Token: 0x06001076 RID: 4214 RVA: 0x0003150C File Offset: 0x0002F70C
		public NotFiniteNumberException(double offendingNumber)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06001077 RID: 4215 RVA: 0x00031526 File Offset: 0x0002F726
		public NotFiniteNumberException(string message)
			: base(message)
		{
			this._offendingNumber = 0.0;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and the invalid number.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="offendingNumber">The value of the argument that caused the exception.</param>
		// Token: 0x06001078 RID: 4216 RVA: 0x00031549 File Offset: 0x0002F749
		public NotFiniteNumberException(string message, double offendingNumber)
			: base(message)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001079 RID: 4217 RVA: 0x00031564 File Offset: 0x0002F764
		public NotFiniteNumberException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message, the invalid number, and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="offendingNumber">The value of the argument that caused the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600107A RID: 4218 RVA: 0x00031579 File Offset: 0x0002F779
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException)
			: base(message, innerException)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600107B RID: 4219 RVA: 0x00031595 File Offset: 0x0002F795
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._offendingNumber = (double)info.GetInt32("OffendingNumber");
		}

		/// <summary>Gets the invalid number that is a positive infinity, a negative infinity, or Not-a-Number (NaN).</summary>
		/// <returns>The invalid number.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000315B1 File Offset: 0x0002F7B1
		public double OffendingNumber
		{
			get
			{
				return this._offendingNumber;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid number and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is <see langword="null" />.</exception>
		// Token: 0x0600107D RID: 4221 RVA: 0x000315B9 File Offset: 0x0002F7B9
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this._offendingNumber, typeof(int));
		}

		// Token: 0x040005C6 RID: 1478
		private double _offendingNumber;
	}
}

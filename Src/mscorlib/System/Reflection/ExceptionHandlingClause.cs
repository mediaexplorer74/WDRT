using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a clause in a structured exception-handling block.</summary>
	// Token: 0x02000610 RID: 1552
	[ComVisible(true)]
	public class ExceptionHandlingClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ExceptionHandlingClause" /> class.</summary>
		// Token: 0x06004813 RID: 18451 RVA: 0x001075B7 File Offset: 0x001057B7
		protected ExceptionHandlingClause()
		{
		}

		/// <summary>Gets a value indicating whether this exception-handling clause is a finally clause, a type-filtered clause, or a user-filtered clause.</summary>
		/// <returns>An <see cref="T:System.Reflection.ExceptionHandlingClauseOptions" /> value that indicates what kind of action this clause performs.</returns>
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x001075BF File Offset: 0x001057BF
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		/// <summary>The offset within the method, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x001075C7 File Offset: 0x001057C7
		public virtual int TryOffset
		{
			get
			{
				return this.m_tryOffset;
			}
		}

		/// <summary>The total length, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>The total length, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x001075CF File Offset: 0x001057CF
		public virtual int TryLength
		{
			get
			{
				return this.m_tryLength;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method body, in bytes, of this exception-handling clause.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x001075D7 File Offset: 0x001057D7
		public virtual int HandlerOffset
		{
			get
			{
				return this.m_handlerOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the body of this exception-handling clause.</summary>
		/// <returns>An integer that represents the length, in bytes, of the MSIL that forms the body of this exception-handling clause.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004818 RID: 18456 RVA: 0x001075DF File Offset: 0x001057DF
		public virtual int HandlerLength
		{
			get
			{
				return this.m_handlerLength;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of the user-supplied filter code.</summary>
		/// <returns>The offset within the method body, in bytes, of the user-supplied filter code. The value of this property has no meaning if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property has any value other than <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot get the offset because the exception handling clause is not a filter.</exception>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004819 RID: 18457 RVA: 0x001075E7 File Offset: 0x001057E7
		public virtual int FilterOffset
		{
			get
			{
				if (this.m_flags != ExceptionHandlingClauseOptions.Filter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotFilter"));
				}
				return this.m_filterOffset;
			}
		}

		/// <summary>Gets the type of exception handled by this clause.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents that type of exception handled by this clause, or <see langword="null" /> if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property is <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> or <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Finally" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Invalid use of property for the object's current state.</exception>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x00107608 File Offset: 0x00105808
		public virtual Type CatchType
		{
			get
			{
				if (this.m_flags != ExceptionHandlingClauseOptions.Clause)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotClause"));
				}
				Type type = null;
				if (!MetadataToken.IsNullToken(this.m_catchMetadataToken))
				{
					Type declaringType = this.m_methodBody.m_methodBase.DeclaringType;
					Module module = ((declaringType == null) ? this.m_methodBody.m_methodBase.Module : declaringType.Module);
					type = module.ResolveType(this.m_catchMetadataToken, (declaringType == null) ? null : declaringType.GetGenericArguments(), (this.m_methodBody.m_methodBase is MethodInfo) ? this.m_methodBody.m_methodBase.GetGenericArguments() : null);
				}
				return type;
			}
		}

		/// <summary>A string representation of the exception-handling clause.</summary>
		/// <returns>A string that lists appropriate property values for the filter clause type.</returns>
		// Token: 0x0600481B RID: 18459 RVA: 0x001076B4 File Offset: 0x001058B4
		public override string ToString()
		{
			if (this.Flags == ExceptionHandlingClauseOptions.Clause)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, CatchType={5}", new object[] { this.Flags, this.TryOffset, this.TryLength, this.HandlerOffset, this.HandlerLength, this.CatchType });
			}
			if (this.Flags == ExceptionHandlingClauseOptions.Filter)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, FilterOffset={5}", new object[] { this.Flags, this.TryOffset, this.TryLength, this.HandlerOffset, this.HandlerLength, this.FilterOffset });
			}
			return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[] { this.Flags, this.TryOffset, this.TryLength, this.HandlerOffset, this.HandlerLength });
		}

		// Token: 0x04001DD6 RID: 7638
		private MethodBody m_methodBody;

		// Token: 0x04001DD7 RID: 7639
		private ExceptionHandlingClauseOptions m_flags;

		// Token: 0x04001DD8 RID: 7640
		private int m_tryOffset;

		// Token: 0x04001DD9 RID: 7641
		private int m_tryLength;

		// Token: 0x04001DDA RID: 7642
		private int m_handlerOffset;

		// Token: 0x04001DDB RID: 7643
		private int m_handlerLength;

		// Token: 0x04001DDC RID: 7644
		private int m_catchMetadataToken;

		// Token: 0x04001DDD RID: 7645
		private int m_filterOffset;
	}
}

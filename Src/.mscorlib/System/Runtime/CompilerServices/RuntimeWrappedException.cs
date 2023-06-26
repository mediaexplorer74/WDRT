using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Wraps an exception that does not derive from the <see cref="T:System.Exception" /> class. This class cannot be inherited.</summary>
	// Token: 0x020008E2 RID: 2274
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x06005DFB RID: 24059 RVA: 0x0014B1CE File Offset: 0x001493CE
		private RuntimeWrappedException(object thrownObject)
			: base(Environment.GetResourceString("RuntimeWrappedException"))
		{
			base.SetErrorCode(-2146233026);
			this.m_wrappedException = thrownObject;
		}

		/// <summary>Gets the object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>The object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</returns>
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06005DFC RID: 24060 RVA: 0x0014B1F2 File Offset: 0x001493F2
		public object WrappedException
		{
			get
			{
				return this.m_wrappedException;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005DFD RID: 24061 RVA: 0x0014B1FA File Offset: 0x001493FA
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this.m_wrappedException, typeof(object));
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0014B22D File Offset: 0x0014942D
		internal RuntimeWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x04002A45 RID: 10821
		private object m_wrappedException;
	}
}

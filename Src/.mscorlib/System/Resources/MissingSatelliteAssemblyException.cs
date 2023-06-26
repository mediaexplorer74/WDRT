using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown when the satellite assembly for the resources of the default culture is missing.</summary>
	// Token: 0x02000390 RID: 912
	[ComVisible(true)]
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with default properties.</summary>
		// Token: 0x06002D1F RID: 11551 RVA: 0x000AB98B File Offset: 0x000A9B8B
		public MissingSatelliteAssemblyException()
			: base(Environment.GetResourceString("MissingSatelliteAssembly_Default"))
		{
			base.SetErrorCode(-2146233034);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002D20 RID: 11552 RVA: 0x000AB9A8 File Offset: 0x000A9BA8
		public MissingSatelliteAssemblyException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233034);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and the name of a neutral culture.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="cultureName">The name of the neutral culture.</param>
		// Token: 0x06002D21 RID: 11553 RVA: 0x000AB9BC File Offset: 0x000A9BBC
		public MissingSatelliteAssemblyException(string message, string cultureName)
			: base(message)
		{
			base.SetErrorCode(-2146233034);
			this._cultureName = cultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002D22 RID: 11554 RVA: 0x000AB9D7 File Offset: 0x000A9BD7
		public MissingSatelliteAssemblyException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233034);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x06002D23 RID: 11555 RVA: 0x000AB9EC File Offset: 0x000A9BEC
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the name of the default culture.</summary>
		/// <returns>The name of the default culture.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000AB9F6 File Offset: 0x000A9BF6
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x04001235 RID: 4661
		private string _cultureName;
	}
}

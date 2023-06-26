using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Logs tracing messages when the .NET Framework serialization infrastructure is compiled.</summary>
	// Token: 0x02000761 RID: 1889
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalST
	{
		// Token: 0x06005323 RID: 21283 RVA: 0x0012536F File Offset: 0x0012356F
		private InternalST()
		{
		}

		/// <summary>Prints SOAP trace messages.</summary>
		/// <param name="messages">An array of trace messages to print.</param>
		// Token: 0x06005324 RID: 21284 RVA: 0x00125377 File Offset: 0x00123577
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		/// <summary>Checks if SOAP tracing is enabled.</summary>
		/// <returns>
		///   <see langword="true" />, if tracing is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005325 RID: 21285 RVA: 0x00125379 File Offset: 0x00123579
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		/// <summary>Processes the specified array of messages.</summary>
		/// <param name="messages">An array of messages to process.</param>
		// Token: 0x06005326 RID: 21286 RVA: 0x00125388 File Offset: 0x00123588
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			int num = 0;
			object obj = messages[0];
			messages[num] = ((obj != null) ? obj.ToString() : null) + " ";
		}

		/// <summary>Asserts the specified message.</summary>
		/// <param name="condition">A Boolean value to use when asserting.</param>
		/// <param name="message">The message to use when asserting.</param>
		// Token: 0x06005327 RID: 21287 RVA: 0x001253D6 File Offset: 0x001235D6
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		/// <summary>Sets the value of a field.</summary>
		/// <param name="fi">A <see cref="T:System.Reflection.FieldInfo" /> containing data about the target field.</param>
		/// <param name="target">The field to change.</param>
		/// <param name="value">The value to set.</param>
		// Token: 0x06005328 RID: 21288 RVA: 0x001253D8 File Offset: 0x001235D8
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		/// <summary>Loads a specified assembly to debug.</summary>
		/// <param name="assemblyString">The name of the assembly to load.</param>
		/// <returns>The <see cref="T:System.Reflection.Assembly" /> to debug.</returns>
		// Token: 0x06005329 RID: 21289 RVA: 0x00125412 File Offset: 0x00123612
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}

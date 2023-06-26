using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x020002FD RID: 765
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00012C8C File Offset: 0x0001208C
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string text = "NestedException";
			this.NestedException = (Exception)info.GetValue(text, typeFromHandle);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000132E8 File Offset: 0x000126E8
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException)
			: base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00012B88 File Offset: 0x00011F88
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00012B9C File Offset: 0x00011F9C
		public Exception NestedException
		{
			get
			{
				return this.<backing_store>NestedException;
			}
			set
			{
				this.<backing_store>NestedException = value;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00012BB0 File Offset: 0x00011FB0
		public override string ToString()
		{
			string text;
			if (this.InnerException != null)
			{
				text = this.InnerException.ToString();
			}
			else
			{
				text = string.Empty;
			}
			string text2;
			if (this.NestedException != null)
			{
				text2 = this.NestedException.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			object[] array = new object[4];
			Type type = this.GetType();
			array[0] = type;
			string text3;
			if (this.Message != null)
			{
				text3 = this.Message;
			}
			else
			{
				text3 = string.Empty;
			}
			array[1] = text3;
			string text4;
			if (text != null)
			{
				text4 = text;
			}
			else
			{
				text4 = string.Empty;
			}
			array[2] = text4;
			string text5;
			if (text2 != null)
			{
				text5 = text2;
			}
			else
			{
				text5 = string.Empty;
			}
			array[3] = text5;
			return string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", array);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00012C58 File Offset: 0x00012058
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			info.AddValue("NestedException", nestedException, typeFromHandle);
		}

		// Token: 0x04000286 RID: 646
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x04000287 RID: 647
		private Exception <backing_store>NestedException;
	}
}

using System;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The exception that is thrown when a configuration system error has occurred.</summary>
	// Token: 0x02000081 RID: 129
	[Serializable]
	public class ConfigurationException : SystemException
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00021120 File Offset: 0x0001F320
		private void Init(string filename, int line)
		{
			base.HResult = -2146232062;
			this._filename = filename;
			this._line = line;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		// Token: 0x0600050D RID: 1293 RVA: 0x0002113B File Offset: 0x0001F33B
		protected ConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Init(info.GetString("filename"), info.GetInt32("line"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		// Token: 0x0600050E RID: 1294 RVA: 0x00021161 File Offset: 0x0001F361
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException()
			: this(null, null, null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		// Token: 0x0600050F RID: 1295 RVA: 0x0002116D File Offset: 0x0001F36D
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message)
			: this(message, null, null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		// Token: 0x06000510 RID: 1296 RVA: 0x00021179 File Offset: 0x0001F379
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner)
			: this(message, inner, null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x06000511 RID: 1297 RVA: 0x00021185 File Offset: 0x0001F385
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, XmlNode node)
			: this(message, null, ConfigurationException.GetUnsafeXmlNodeFilename(node), ConfigurationException.GetXmlNodeLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x06000512 RID: 1298 RVA: 0x0002119B File Offset: 0x0001F39B
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, XmlNode node)
			: this(message, inner, ConfigurationException.GetUnsafeXmlNodeFilename(node), ConfigurationException.GetXmlNodeLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> was thrown.</param>
		// Token: 0x06000513 RID: 1299 RVA: 0x000211B1 File Offset: 0x0001F3B1
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, string filename, int line)
			: this(message, null, filename, line)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> was thrown.</param>
		// Token: 0x06000514 RID: 1300 RVA: 0x000211BD File Offset: 0x0001F3BD
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			this.Init(filename, line);
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000515 RID: 1301 RVA: 0x000211D0 File Offset: 0x0001F3D0
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this._filename);
			info.AddValue("line", this._line);
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000211FC File Offset: 0x0001F3FC
		public override string Message
		{
			get
			{
				string filename = this.Filename;
				if (!string.IsNullOrEmpty(filename))
				{
					if (this.Line != 0)
					{
						return string.Concat(new string[]
						{
							this.BareMessage,
							" (",
							filename,
							" line ",
							this.Line.ToString(CultureInfo.InvariantCulture),
							")"
						});
					}
					return this.BareMessage + " (" + filename + ")";
				}
				else
				{
					if (this.Line != 0)
					{
						return this.BareMessage + " (line " + this.Line.ToString("G", CultureInfo.InvariantCulture) + ")";
					}
					return this.BareMessage;
				}
			}
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000212BA File Offset: 0x0001F4BA
		public virtual string BareMessage
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Gets the path to the configuration file that caused this configuration exception to be thrown.</summary>
		/// <returns>The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000212C2 File Offset: 0x0001F4C2
		public virtual string Filename
		{
			get
			{
				return ConfigurationException.SafeFilename(this._filename);
			}
		}

		/// <summary>Gets the line number within the configuration file at which this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000212CF File Offset: 0x0001F4CF
		public virtual int Line
		{
			get
			{
				return this._line;
			}
		}

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <returns>A <see langword="string" /> representing the node file name.</returns>
		// Token: 0x0600051A RID: 1306 RVA: 0x000212D7 File Offset: 0x0001F4D7
		[Obsolete("This class is obsolete, use System.Configuration!System.Configuration.ConfigurationErrorsException.GetFilename instead")]
		public static string GetXmlNodeFilename(XmlNode node)
		{
			return ConfigurationException.SafeFilename(ConfigurationException.GetUnsafeXmlNodeFilename(node));
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <returns>An <see langword="int" /> representing the node line number.</returns>
		// Token: 0x0600051B RID: 1307 RVA: 0x000212E4 File Offset: 0x0001F4E4
		[Obsolete("This class is obsolete, use System.Configuration!System.Configuration.ConfigurationErrorsException.GetLinenumber instead")]
		public static int GetXmlNodeLineNumber(XmlNode node)
		{
			IConfigErrorInfo configErrorInfo = node as IConfigErrorInfo;
			if (configErrorInfo != null)
			{
				return configErrorInfo.LineNumber;
			}
			return 0;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00021304 File Offset: 0x0001F504
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string FullPathWithAssert(string filename)
		{
			string text = null;
			try
			{
				text = Path.GetFullPath(filename);
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00021330 File Offset: 0x0001F530
		internal static string SafeFilename(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return filename;
			}
			if (filename.StartsWith("http:", StringComparison.OrdinalIgnoreCase))
			{
				return filename;
			}
			try
			{
				if (!Path.IsPathRooted(filename))
				{
					return filename;
				}
			}
			catch
			{
				return null;
			}
			try
			{
				string fullPath = Path.GetFullPath(filename);
			}
			catch (SecurityException)
			{
				try
				{
					string text = ConfigurationException.FullPathWithAssert(filename);
					filename = Path.GetFileName(text);
				}
				catch
				{
					filename = null;
				}
			}
			catch
			{
				filename = null;
			}
			return filename;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000213CC File Offset: 0x0001F5CC
		private static string GetUnsafeXmlNodeFilename(XmlNode node)
		{
			IConfigErrorInfo configErrorInfo = node as IConfigErrorInfo;
			if (configErrorInfo != null)
			{
				return configErrorInfo.Filename;
			}
			return string.Empty;
		}

		// Token: 0x04000C14 RID: 3092
		private const string HTTP_PREFIX = "http:";

		// Token: 0x04000C15 RID: 3093
		private string _filename;

		// Token: 0x04000C16 RID: 3094
		private int _line;
	}
}

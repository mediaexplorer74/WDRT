using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml;

namespace System.Resources
{
	/// <summary>Enumerates XML resource (.resx) files and streams, and reads the sequential resource name and value pairs.</summary>
	// Token: 0x020000EF RID: 239
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ResXResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000A56D File Offset: 0x0000876D
		private ResXResourceReader(ITypeResolutionService typeResolver)
		{
			this.typeResolver = typeResolver;
			this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A587 File Offset: 0x00008787
		private ResXResourceReader(AssemblyName[] assemblyNames)
		{
			this.assemblyNames = assemblyNames;
			this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified resource file.</summary>
		/// <param name="fileName">The path of the resource file to read.</param>
		// Token: 0x0600036B RID: 875 RVA: 0x0000A5A1 File Offset: 0x000087A1
		public ResXResourceReader(string fileName)
			: this(fileName, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a file name and a type resolution service.</summary>
		/// <param name="fileName">The name of an XML resource file that contains resources.</param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x0600036C RID: 876 RVA: 0x0000A5AC File Offset: 0x000087AC
		public ResXResourceReader(string fileName, ITypeResolutionService typeResolver)
			: this(fileName, typeResolver, null)
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A5B7 File Offset: 0x000087B7
		internal ResXResourceReader(string fileName, ITypeResolutionService typeResolver, IAliasResolver aliasResolver)
		{
			this.fileName = fileName;
			this.typeResolver = typeResolver;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="reader">A text input stream that contains resources.</param>
		// Token: 0x0600036E RID: 878 RVA: 0x0000A5E7 File Offset: 0x000087E7
		public ResXResourceReader(TextReader reader)
			: this(reader, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a text stream reader and a type resolution service.</summary>
		/// <param name="reader">A text stream reader that contains resources.</param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x0600036F RID: 879 RVA: 0x0000A5F2 File Offset: 0x000087F2
		public ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver)
			: this(reader, typeResolver, null)
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A5FD File Offset: 0x000087FD
		internal ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver, IAliasResolver aliasResolver)
		{
			this.reader = reader;
			this.typeResolver = typeResolver;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified stream.</summary>
		/// <param name="stream">An input stream that contains resources.</param>
		// Token: 0x06000371 RID: 881 RVA: 0x0000A62D File Offset: 0x0000882D
		public ResXResourceReader(Stream stream)
			: this(stream, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using an input stream and a type resolution service.</summary>
		/// <param name="stream">An input stream that contains resources.</param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x06000372 RID: 882 RVA: 0x0000A638 File Offset: 0x00008838
		public ResXResourceReader(Stream stream, ITypeResolutionService typeResolver)
			: this(stream, typeResolver, null)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A643 File Offset: 0x00008843
		internal ResXResourceReader(Stream stream, ITypeResolutionService typeResolver, IAliasResolver aliasResolver)
		{
			this.stream = stream;
			this.typeResolver = typeResolver;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a stream and an array of assembly names.</summary>
		/// <param name="stream">An input stream that contains resources.</param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type.</param>
		// Token: 0x06000374 RID: 884 RVA: 0x0000A673 File Offset: 0x00008873
		public ResXResourceReader(Stream stream, AssemblyName[] assemblyNames)
			: this(stream, assemblyNames, null)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A67E File Offset: 0x0000887E
		internal ResXResourceReader(Stream stream, AssemblyName[] assemblyNames, IAliasResolver aliasResolver)
		{
			this.stream = stream;
			this.assemblyNames = assemblyNames;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a <see cref="T:System.IO.TextReader" /> object and an array of assembly names.</summary>
		/// <param name="reader">An object used to read resources from a stream of text.</param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type.</param>
		// Token: 0x06000376 RID: 886 RVA: 0x0000A6AE File Offset: 0x000088AE
		public ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames)
			: this(reader, assemblyNames, null)
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A6B9 File Offset: 0x000088B9
		internal ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames, IAliasResolver aliasResolver)
		{
			this.reader = reader;
			this.assemblyNames = assemblyNames;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using an XML resource file name and an array of assembly names.</summary>
		/// <param name="fileName">The name of an XML resource file that contains resources.</param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type.</param>
		// Token: 0x06000378 RID: 888 RVA: 0x0000A6E9 File Offset: 0x000088E9
		public ResXResourceReader(string fileName, AssemblyName[] assemblyNames)
			: this(fileName, assemblyNames, null)
		{
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A6F4 File Offset: 0x000088F4
		internal ResXResourceReader(string fileName, AssemblyName[] assemblyNames, IAliasResolver aliasResolver)
		{
			this.fileName = fileName;
			this.assemblyNames = assemblyNames;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		/// <summary>This member overrides the <see cref="M:System.Object.Finalize" /> method.</summary>
		// Token: 0x0600037A RID: 890 RVA: 0x0000A724 File Offset: 0x00008924
		~ResXResourceReader()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the base path for the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
		/// <returns>A path that, if prepended to the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object, yields an absolute path to a resource file.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, a value cannot be specified because the XML resource file has already been accessed and is in use.</exception>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000A754 File Offset: 0x00008954
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000A75C File Offset: 0x0000895C
		public string BasePath
		{
			get
			{
				return this.basePath;
			}
			set
			{
				if (this.isReaderDirty)
				{
					throw new InvalidOperationException(SR.GetString("InvalidResXBasePathOperation"));
				}
				this.basePath = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Resources.ResXDataNode" /> objects are returned when reading the current XML resource file or stream.</summary>
		/// <returns>
		///   <see langword="true" /> if resource data nodes are retrieved; <see langword="false" /> if resource data nodes are ignored.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the enumerator for the resource file or stream is already open.</exception>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000A77D File Offset: 0x0000897D
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000A785 File Offset: 0x00008985
		public bool UseResXDataNodes
		{
			get
			{
				return this.useResXDataNodes;
			}
			set
			{
				if (this.isReaderDirty)
				{
					throw new InvalidOperationException(SR.GetString("InvalidResXBasePathOperation"));
				}
				this.useResXDataNodes = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Resources.ResXResourceReader" />.</summary>
		// Token: 0x0600037F RID: 895 RVA: 0x0000A7A6 File Offset: 0x000089A6
		public void Close()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceReader" /> and optionally releases the managed resources. For a description of this member, see the <see cref="M:System.IDisposable.Dispose" /> method.</summary>
		// Token: 0x06000380 RID: 896 RVA: 0x0000A7AE File Offset: 0x000089AE
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000381 RID: 897 RVA: 0x0000A7C0 File Offset: 0x000089C0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.fileName != null && this.stream != null)
				{
					this.stream.Close();
					this.stream = null;
				}
				if (this.reader != null)
				{
					this.reader.Close();
					this.reader = null;
				}
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A80C File Offset: 0x00008A0C
		private void SetupNameTable(XmlReader reader)
		{
			reader.NameTable.Add("type");
			reader.NameTable.Add("name");
			reader.NameTable.Add("data");
			reader.NameTable.Add("metadata");
			reader.NameTable.Add("mimetype");
			reader.NameTable.Add("value");
			reader.NameTable.Add("resheader");
			reader.NameTable.Add("version");
			reader.NameTable.Add("resmimetype");
			reader.NameTable.Add("reader");
			reader.NameTable.Add("writer");
			reader.NameTable.Add(ResXResourceWriter.BinSerializedObjectMimeType);
			reader.NameTable.Add(ResXResourceWriter.SoapSerializedObjectMimeType);
			reader.NameTable.Add("assembly");
			reader.NameTable.Add("alias");
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A918 File Offset: 0x00008B18
		private void EnsureResData()
		{
			if (this.resData == null)
			{
				this.resData = new ListDictionary();
				this.resMetadata = new ListDictionary();
				XmlTextReader xmlTextReader = null;
				try
				{
					if (this.fileContents != null)
					{
						xmlTextReader = new XmlTextReader(new StringReader(this.fileContents));
					}
					else if (this.reader != null)
					{
						xmlTextReader = new XmlTextReader(this.reader);
					}
					else if (this.fileName != null || this.stream != null)
					{
						if (this.stream == null)
						{
							this.stream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
						}
						xmlTextReader = new XmlTextReader(this.stream);
					}
					this.SetupNameTable(xmlTextReader);
					xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
					this.ParseXml(xmlTextReader);
				}
				finally
				{
					if (this.fileName != null && this.stream != null)
					{
						this.stream.Close();
						this.stream = null;
					}
				}
			}
		}

		/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file.</summary>
		/// <param name="fileContents">A string containing XML resource-formatted information.</param>
		/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
		// Token: 0x06000384 RID: 900 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public static ResXResourceReader FromFileContents(string fileContents)
		{
			return ResXResourceReader.FromFileContents(fileContents, null);
		}

		/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file, and to use an <see cref="T:System.ComponentModel.Design.ITypeResolutionService" /> object to resolve type names specified in a resource.</summary>
		/// <param name="fileContents">A string containing XML resource-formatted information.</param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
		// Token: 0x06000385 RID: 901 RVA: 0x0000AA08 File Offset: 0x00008C08
		public static ResXResourceReader FromFileContents(string fileContents, ITypeResolutionService typeResolver)
		{
			return new ResXResourceReader(typeResolver)
			{
				fileContents = fileContents
			};
		}

		/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file, and to use an array of <see cref="T:System.Reflection.AssemblyName" /> objects to resolve type names specified in a resource.</summary>
		/// <param name="fileContents">A string whose contents are in the form of an XML resource file.</param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type.</param>
		/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
		// Token: 0x06000386 RID: 902 RVA: 0x0000AA24 File Offset: 0x00008C24
		public static ResXResourceReader FromFileContents(string fileContents, AssemblyName[] assemblyNames)
		{
			return new ResXResourceReader(assemblyNames)
			{
				fileContents = fileContents
			};
		}

		/// <summary>Returns an enumerator for the current <see cref="T:System.Resources.ResXResourceReader" /> object. For a description of this member, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
		/// <returns>An enumerator that can iterate through the name/value pairs in the XML resource (.resx) stream or string associated with the current <see cref="T:System.Resources.ResXResourceReader" /> object.</returns>
		// Token: 0x06000387 RID: 903 RVA: 0x0000AA40 File Offset: 0x00008C40
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for the current <see cref="T:System.Resources.ResXResourceReader" /> object.</summary>
		/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		// Token: 0x06000388 RID: 904 RVA: 0x0000AA48 File Offset: 0x00008C48
		public IDictionaryEnumerator GetEnumerator()
		{
			this.isReaderDirty = true;
			this.EnsureResData();
			return this.resData.GetEnumerator();
		}

		/// <summary>Provides a dictionary enumerator that can retrieve the design-time properties from the current XML resource file or stream.</summary>
		/// <returns>An enumerator for the metadata in a resource.</returns>
		// Token: 0x06000389 RID: 905 RVA: 0x0000AA62 File Offset: 0x00008C62
		public IDictionaryEnumerator GetMetadataEnumerator()
		{
			this.EnsureResData();
			return this.resMetadata.GetEnumerator();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000AA78 File Offset: 0x00008C78
		private Point GetPosition(XmlReader reader)
		{
			Point point = new Point(0, 0);
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				point.Y = xmlLineInfo.LineNumber;
				point.X = xmlLineInfo.LinePosition;
			}
			return point;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		private void ParseXml(XmlTextReader reader)
		{
			bool flag = false;
			try
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						string localName = reader.LocalName;
						if (reader.LocalName.Equals("assembly"))
						{
							this.ParseAssemblyNode(reader, false);
						}
						else if (reader.LocalName.Equals("data"))
						{
							this.ParseDataNode(reader, false);
						}
						else if (reader.LocalName.Equals("resheader"))
						{
							this.ParseResHeaderNode(reader);
						}
						else if (reader.LocalName.Equals("metadata"))
						{
							this.ParseDataNode(reader, true);
						}
					}
				}
				flag = true;
			}
			catch (SerializationException ex)
			{
				Point position = this.GetPosition(reader);
				string @string = SR.GetString("SerializationException", new object[]
				{
					reader["type"],
					position.Y,
					position.X,
					ex.Message
				});
				XmlException ex2 = new XmlException(@string, ex, position.Y, position.X);
				SerializationException ex3 = new SerializationException(@string, ex2);
				throw ex3;
			}
			catch (TargetInvocationException ex4)
			{
				Point position2 = this.GetPosition(reader);
				string string2 = SR.GetString("InvocationException", new object[]
				{
					reader["type"],
					position2.Y,
					position2.X,
					ex4.InnerException.Message
				});
				XmlException ex5 = new XmlException(string2, ex4.InnerException, position2.Y, position2.X);
				TargetInvocationException ex6 = new TargetInvocationException(string2, ex5);
				throw ex6;
			}
			catch (XmlException ex7)
			{
				throw new ArgumentException(SR.GetString("InvalidResXFile", new object[] { ex7.Message }), ex7);
			}
			catch (Exception ex8)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex8))
				{
					throw;
				}
				Point position3 = this.GetPosition(reader);
				XmlException ex9 = new XmlException(ex8.Message, ex8, position3.Y, position3.X);
				throw new ArgumentException(SR.GetString("InvalidResXFile", new object[] { ex9.Message }), ex9);
			}
			finally
			{
				if (!flag)
				{
					this.resData = null;
					this.resMetadata = null;
				}
			}
			bool flag2 = false;
			if (object.Equals(this.resHeaderMimeType, ResXResourceWriter.ResMimeType))
			{
				Type typeFromHandle = typeof(ResXResourceReader);
				Type typeFromHandle2 = typeof(ResXResourceWriter);
				string text = this.resHeaderReaderType;
				string text2 = this.resHeaderWriterType;
				if (text != null && text.IndexOf(',') != -1)
				{
					text = text.Split(new char[] { ',' })[0].Trim();
				}
				if (text2 != null && text2.IndexOf(',') != -1)
				{
					text2 = text2.Split(new char[] { ',' })[0].Trim();
				}
				if (text != null && text2 != null && text.Equals(typeFromHandle.FullName) && text2.Equals(typeFromHandle2.FullName))
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				this.resData = null;
				this.resMetadata = null;
				throw new ArgumentException(SR.GetString("InvalidResXFileReaderWriterTypes"));
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000AE38 File Offset: 0x00009038
		private void ParseResHeaderNode(XmlReader reader)
		{
			string text = reader["name"];
			if (text != null)
			{
				reader.ReadStartElement();
				if (object.Equals(text, "version"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderVersion = reader.ReadElementString();
						return;
					}
					this.resHeaderVersion = reader.Value.Trim();
					return;
				}
				else if (object.Equals(text, "resmimetype"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderMimeType = reader.ReadElementString();
						return;
					}
					this.resHeaderMimeType = reader.Value.Trim();
					return;
				}
				else if (object.Equals(text, "reader"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderReaderType = reader.ReadElementString();
						return;
					}
					this.resHeaderReaderType = reader.Value.Trim();
					return;
				}
				else if (object.Equals(text, "writer"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderWriterType = reader.ReadElementString();
						return;
					}
					this.resHeaderWriterType = reader.Value.Trim();
					return;
				}
				else
				{
					string text2 = text.ToLower(CultureInfo.InvariantCulture);
					if (!(text2 == "version"))
					{
						if (!(text2 == "resmimetype"))
						{
							if (!(text2 == "reader"))
							{
								if (!(text2 == "writer"))
								{
									return;
								}
								if (reader.NodeType == XmlNodeType.Element)
								{
									this.resHeaderWriterType = reader.ReadElementString();
									return;
								}
								this.resHeaderWriterType = reader.Value.Trim();
							}
							else
							{
								if (reader.NodeType == XmlNodeType.Element)
								{
									this.resHeaderReaderType = reader.ReadElementString();
									return;
								}
								this.resHeaderReaderType = reader.Value.Trim();
								return;
							}
						}
						else
						{
							if (reader.NodeType == XmlNodeType.Element)
							{
								this.resHeaderMimeType = reader.ReadElementString();
								return;
							}
							this.resHeaderMimeType = reader.Value.Trim();
							return;
						}
					}
					else
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							this.resHeaderVersion = reader.ReadElementString();
							return;
						}
						this.resHeaderVersion = reader.Value.Trim();
						return;
					}
				}
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B014 File Offset: 0x00009214
		private void ParseAssemblyNode(XmlReader reader, bool isMetaData)
		{
			string text = reader["alias"];
			string text2 = reader["name"];
			AssemblyName assemblyName = new AssemblyName(text2);
			if (string.IsNullOrEmpty(text))
			{
				text = assemblyName.Name;
			}
			this.aliasResolver.PushAlias(text, assemblyName);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B05C File Offset: 0x0000925C
		private void ParseDataNode(XmlTextReader reader, bool isMetaData)
		{
			DataNodeInfo dataNodeInfo = new DataNodeInfo();
			dataNodeInfo.Name = reader["name"];
			string text = reader["type"];
			string text2 = null;
			AssemblyName assemblyName = null;
			if (!string.IsNullOrEmpty(text))
			{
				text2 = this.GetAliasFromTypeName(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				assemblyName = this.aliasResolver.ResolveAlias(text2);
			}
			if (assemblyName != null)
			{
				dataNodeInfo.TypeName = this.GetTypeFromTypeName(text) + ", " + assemblyName.FullName;
			}
			else
			{
				dataNodeInfo.TypeName = reader["type"];
			}
			dataNodeInfo.MimeType = reader["mimetype"];
			bool flag = false;
			dataNodeInfo.ReaderPosition = this.GetPosition(reader);
			while (!flag && reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && (reader.LocalName.Equals("data") || reader.LocalName.Equals("metadata")))
				{
					flag = true;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					if (reader.Name.Equals("value"))
					{
						WhitespaceHandling whitespaceHandling = reader.WhitespaceHandling;
						try
						{
							reader.WhitespaceHandling = WhitespaceHandling.Significant;
							dataNodeInfo.ValueData = reader.ReadString();
							continue;
						}
						finally
						{
							reader.WhitespaceHandling = whitespaceHandling;
						}
					}
					if (reader.Name.Equals("comment"))
					{
						dataNodeInfo.Comment = reader.ReadString();
					}
				}
				else
				{
					dataNodeInfo.ValueData = reader.Value.Trim();
				}
			}
			if (dataNodeInfo.Name == null)
			{
				throw new ArgumentException(SR.GetString("InvalidResXResourceNoName", new object[] { dataNodeInfo.ValueData }));
			}
			ResXDataNode resXDataNode = new ResXDataNode(dataNodeInfo, this.BasePath);
			if (this.UseResXDataNodes)
			{
				this.resData[dataNodeInfo.Name] = resXDataNode;
				return;
			}
			IDictionary dictionary = (isMetaData ? this.resMetadata : this.resData);
			if (this.assemblyNames == null)
			{
				dictionary[dataNodeInfo.Name] = resXDataNode.GetValue(this.typeResolver);
				return;
			}
			dictionary[dataNodeInfo.Name] = resXDataNode.GetValue(this.assemblyNames);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B274 File Offset: 0x00009474
		private string GetAliasFromTypeName(string typeName)
		{
			int num = typeName.IndexOf(",");
			return typeName.Substring(num + 2);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B298 File Offset: 0x00009498
		private string GetTypeFromTypeName(string typeName)
		{
			int num = typeName.IndexOf(",");
			return typeName.Substring(0, num);
		}

		// Token: 0x040003C7 RID: 967
		private string fileName;

		// Token: 0x040003C8 RID: 968
		private TextReader reader;

		// Token: 0x040003C9 RID: 969
		private Stream stream;

		// Token: 0x040003CA RID: 970
		private string fileContents;

		// Token: 0x040003CB RID: 971
		private AssemblyName[] assemblyNames;

		// Token: 0x040003CC RID: 972
		private string basePath;

		// Token: 0x040003CD RID: 973
		private bool isReaderDirty;

		// Token: 0x040003CE RID: 974
		private ITypeResolutionService typeResolver;

		// Token: 0x040003CF RID: 975
		private IAliasResolver aliasResolver;

		// Token: 0x040003D0 RID: 976
		private ListDictionary resData;

		// Token: 0x040003D1 RID: 977
		private ListDictionary resMetadata;

		// Token: 0x040003D2 RID: 978
		private string resHeaderVersion;

		// Token: 0x040003D3 RID: 979
		private string resHeaderMimeType;

		// Token: 0x040003D4 RID: 980
		private string resHeaderReaderType;

		// Token: 0x040003D5 RID: 981
		private string resHeaderWriterType;

		// Token: 0x040003D6 RID: 982
		private bool useResXDataNodes;

		// Token: 0x02000542 RID: 1346
		private sealed class ReaderAliasResolver : IAliasResolver
		{
			// Token: 0x06005557 RID: 21847 RVA: 0x00165F1C File Offset: 0x0016411C
			internal ReaderAliasResolver()
			{
				this.cachedAliases = new Hashtable();
			}

			// Token: 0x06005558 RID: 21848 RVA: 0x00165F30 File Offset: 0x00164130
			public AssemblyName ResolveAlias(string alias)
			{
				AssemblyName assemblyName = null;
				if (this.cachedAliases != null)
				{
					assemblyName = (AssemblyName)this.cachedAliases[alias];
				}
				return assemblyName;
			}

			// Token: 0x06005559 RID: 21849 RVA: 0x00165F5A File Offset: 0x0016415A
			public void PushAlias(string alias, AssemblyName name)
			{
				if (this.cachedAliases != null && !string.IsNullOrEmpty(alias))
				{
					this.cachedAliases[alias] = name;
				}
			}

			// Token: 0x040037FD RID: 14333
			private Hashtable cachedAliases;
		}
	}
}

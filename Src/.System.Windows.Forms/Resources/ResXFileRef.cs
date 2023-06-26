using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Resources
{
	/// <summary>Represents a link to an external resource.</summary>
	// Token: 0x020000ED RID: 237
	[TypeConverter(typeof(ResXFileRef.Converter))]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[Serializable]
	public class ResXFileRef
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResXFileRef" /> class that references the specified file.</summary>
		/// <param name="fileName">The file to reference.</param>
		/// <param name="typeName">The type of the resource that is referenced.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> or <paramref name="typeName" /> is <see langword="null" />.</exception>
		// Token: 0x0600035D RID: 861 RVA: 0x0000A359 File Offset: 0x00008559
		public ResXFileRef(string fileName, string typeName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.fileName = fileName;
			this.typeName = typeName;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A38B File Offset: 0x0000858B
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.textFileEncoding = null;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000070A6 File Offset: 0x000052A6
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXFileRef" /> class that references the specified file.</summary>
		/// <param name="fileName">The file to reference.</param>
		/// <param name="typeName">The type name of the resource that is referenced.</param>
		/// <param name="textFileEncoding">The encoding used in the referenced file.</param>
		// Token: 0x06000360 RID: 864 RVA: 0x0000A394 File Offset: 0x00008594
		public ResXFileRef(string fileName, string typeName, Encoding textFileEncoding)
			: this(fileName, typeName)
		{
			this.textFileEncoding = textFileEncoding;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A3A5 File Offset: 0x000085A5
		internal ResXFileRef Clone()
		{
			return new ResXFileRef(this.fileName, this.typeName, this.textFileEncoding);
		}

		/// <summary>Gets the file name specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
		/// <returns>The name of the referenced file.</returns>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000A3BE File Offset: 0x000085BE
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		/// <summary>Gets the type name specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
		/// <returns>The type name of the resource that is referenced.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A3C6 File Offset: 0x000085C6
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets the encoding specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
		/// <returns>The encoding used in the referenced file.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000A3CE File Offset: 0x000085CE
		public Encoding TextFileEncoding
		{
			get
			{
				return this.textFileEncoding;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A3D8 File Offset: 0x000085D8
		private static string PathDifference(string path1, string path2, bool compareCase)
		{
			int num = -1;
			int i = 0;
			while (i < path1.Length && i < path2.Length && (path1[i] == path2[i] || (!compareCase && char.ToLower(path1[i], CultureInfo.InvariantCulture) == char.ToLower(path2[i], CultureInfo.InvariantCulture))))
			{
				if (path1[i] == Path.DirectorySeparatorChar)
				{
					num = i;
				}
				i++;
			}
			if (i == 0)
			{
				return path2;
			}
			if (i == path1.Length && i == path2.Length)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (i < path1.Length)
			{
				if (path1[i] == Path.DirectorySeparatorChar)
				{
					stringBuilder.Append(".." + Path.DirectorySeparatorChar.ToString());
				}
				i++;
			}
			return stringBuilder.ToString() + path2.Substring(num + 1);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A4BA File Offset: 0x000086BA
		internal void MakeFilePathRelative(string basePath)
		{
			if (basePath == null || basePath.Length == 0)
			{
				return;
			}
			this.fileName = ResXFileRef.PathDifference(basePath, this.fileName, false);
		}

		/// <summary>Gets the text representation of the current <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
		/// <returns>A string that consists of the concatenated text representations of the parameters specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</returns>
		// Token: 0x06000367 RID: 871 RVA: 0x0000A4DC File Offset: 0x000086DC
		public override string ToString()
		{
			string text = "";
			if (this.fileName.IndexOf(";") != -1 || this.fileName.IndexOf("\"") != -1)
			{
				text = text + "\"" + this.fileName + "\";";
			}
			else
			{
				text = text + this.fileName + ";";
			}
			text += this.typeName;
			if (this.textFileEncoding != null)
			{
				text = text + ";" + this.textFileEncoding.WebName;
			}
			return text;
		}

		// Token: 0x040003C4 RID: 964
		private string fileName;

		// Token: 0x040003C5 RID: 965
		private string typeName;

		// Token: 0x040003C6 RID: 966
		[OptionalField(VersionAdded = 2)]
		private Encoding textFileEncoding;

		/// <summary>Provides a type converter to convert data for a <see cref="T:System.Resources.ResXFileRef" /> to and from a string.</summary>
		// Token: 0x02000541 RID: 1345
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class Converter : TypeConverter
		{
			/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
			/// <returns>
			///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
			// Token: 0x06005551 RID: 21841 RVA: 0x00165C5A File Offset: 0x00163E5A
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string);
			}

			/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
			/// <returns>
			///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
			// Token: 0x06005552 RID: 21842 RVA: 0x00165C5A File Offset: 0x00163E5A
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string);
			}

			/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
			/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			// Token: 0x06005553 RID: 21843 RVA: 0x00165C74 File Offset: 0x00163E74
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				object obj = null;
				if (destinationType == typeof(string))
				{
					obj = ((ResXFileRef)value).ToString();
				}
				return obj;
			}

			// Token: 0x06005554 RID: 21844 RVA: 0x00165CA4 File Offset: 0x00163EA4
			internal static string[] ParseResxFileRefString(string stringValue)
			{
				string[] array = null;
				if (stringValue != null)
				{
					stringValue = stringValue.Trim();
					string text;
					string text2;
					if (stringValue.StartsWith("\""))
					{
						int num = stringValue.LastIndexOf("\"");
						if (num - 1 < 0)
						{
							throw new ArgumentException("value");
						}
						text = stringValue.Substring(1, num - 1);
						if (num + 2 > stringValue.Length)
						{
							throw new ArgumentException("value");
						}
						text2 = stringValue.Substring(num + 2);
					}
					else
					{
						int num2 = stringValue.IndexOf(";");
						if (num2 == -1)
						{
							throw new ArgumentException("value");
						}
						text = stringValue.Substring(0, num2);
						if (num2 + 1 > stringValue.Length)
						{
							throw new ArgumentException("value");
						}
						text2 = stringValue.Substring(num2 + 1);
					}
					string[] array2 = text2.Split(new char[] { ';' });
					if (array2.Length > 1)
					{
						array = new string[]
						{
							text,
							array2[0],
							array2[1]
						};
					}
					else if (array2.Length != 0)
					{
						array = new string[]
						{
							text,
							array2[0]
						};
					}
					else
					{
						array = new string[] { text };
					}
				}
				return array;
			}

			/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			// Token: 0x06005555 RID: 21845 RVA: 0x00165DBC File Offset: 0x00163FBC
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				object obj = null;
				string text = value as string;
				if (text != null)
				{
					string[] array = ResXFileRef.Converter.ParseResxFileRefString(text);
					string text2 = array[0];
					Type type = Type.GetType(array[1], true);
					if (type.Equals(typeof(string)))
					{
						Encoding encoding = Encoding.Default;
						if (array.Length > 2)
						{
							encoding = Encoding.GetEncoding(array[2]);
						}
						using (StreamReader streamReader = new StreamReader(text2, encoding))
						{
							return streamReader.ReadToEnd();
						}
					}
					byte[] array2 = null;
					using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						array2 = new byte[fileStream.Length];
						fileStream.Read(array2, 0, (int)fileStream.Length);
					}
					if (type.Equals(typeof(byte[])))
					{
						obj = array2;
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream(array2);
						if (type.Equals(typeof(MemoryStream)))
						{
							return memoryStream;
						}
						if (type.Equals(typeof(Bitmap)) && text2.EndsWith(".ico"))
						{
							Icon icon = new Icon(memoryStream);
							obj = icon.ToBitmap();
						}
						else
						{
							obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[] { memoryStream }, null);
						}
					}
				}
				return obj;
			}
		}
	}
}

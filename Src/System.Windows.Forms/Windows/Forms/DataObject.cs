using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Implements a basic data transfer mechanism.</summary>
	// Token: 0x02000222 RID: 546
	[ClassInterface(ClassInterfaceType.None)]
	public class DataObject : IDataObject, IDataObject
	{
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000A9635 File Offset: 0x000A7835
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x000A963D File Offset: 0x000A783D
		internal bool RestrictedFormats { get; set; }

		// Token: 0x0600237D RID: 9085 RVA: 0x000A9646 File Offset: 0x000A7846
		internal DataObject(IDataObject data)
		{
			this.innerData = data;
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000A9655 File Offset: 0x000A7855
		internal DataObject(IDataObject data)
		{
			if (data is DataObject)
			{
				this.innerData = data as IDataObject;
				return;
			}
			this.innerData = new DataObject.OleConverter(data);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class.</summary>
		// Token: 0x0600237F RID: 9087 RVA: 0x000A967E File Offset: 0x000A787E
		public DataObject()
		{
			this.innerData = new DataObject.DataStore();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object to it.</summary>
		/// <param name="data">The data to store.</param>
		// Token: 0x06002380 RID: 9088 RVA: 0x000A9694 File Offset: 0x000A7894
		public DataObject(object data)
		{
			if (data is IDataObject && !Marshal.IsComObject(data))
			{
				this.innerData = (IDataObject)data;
				return;
			}
			if (data is IDataObject)
			{
				this.innerData = new DataObject.OleConverter((IDataObject)data);
				return;
			}
			this.innerData = new DataObject.DataStore();
			this.SetData(data);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object in the specified format.</summary>
		/// <param name="format">The format of the specified data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="data">The data to store.</param>
		// Token: 0x06002381 RID: 9089 RVA: 0x000A96F0 File Offset: 0x000A78F0
		public DataObject(string format, object data)
			: this()
		{
			this.SetData(format, data);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000A9700 File Offset: 0x000A7900
		private IntPtr GetCompatibleBitmap(Bitmap bm)
		{
			IntPtr hbitmap = bm.GetHbitmap();
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			IntPtr intPtr = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, dc));
			IntPtr intPtr2 = SafeNativeMethods.SelectObject(new HandleRef(null, intPtr), new HandleRef(bm, hbitmap));
			IntPtr intPtr3 = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, dc));
			IntPtr intPtr4 = SafeNativeMethods.CreateCompatibleBitmap(new HandleRef(null, dc), bm.Size.Width, bm.Size.Height);
			IntPtr intPtr5 = SafeNativeMethods.SelectObject(new HandleRef(null, intPtr3), new HandleRef(null, intPtr4));
			SafeNativeMethods.BitBlt(new HandleRef(null, intPtr3), 0, 0, bm.Size.Width, bm.Size.Height, new HandleRef(null, intPtr), 0, 0, 13369376);
			SafeNativeMethods.SelectObject(new HandleRef(null, intPtr), new HandleRef(null, intPtr2));
			SafeNativeMethods.SelectObject(new HandleRef(null, intPtr3), new HandleRef(null, intPtr5));
			UnsafeNativeMethods.DeleteCompatibleDC(new HandleRef(null, intPtr));
			UnsafeNativeMethods.DeleteCompatibleDC(new HandleRef(null, intPtr3));
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			SafeNativeMethods.DeleteObject(new HandleRef(bm, hbitmap));
			return intPtr4;
		}

		/// <summary>Returns the data associated with the specified data format, using an automated conversion parameter to determine whether to convert the data to the format.</summary>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> to the convert data to the specified format; otherwise, <see langword="false" />.</param>
		/// <returns>The data associated with the specified format, or <see langword="null" />.</returns>
		// Token: 0x06002383 RID: 9091 RVA: 0x000A9833 File Offset: 0x000A7A33
		public virtual object GetData(string format, bool autoConvert)
		{
			return this.innerData.GetData(format, autoConvert);
		}

		/// <summary>Returns the data associated with the specified data format.</summary>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <returns>The data associated with the specified format, or <see langword="null" />.</returns>
		// Token: 0x06002384 RID: 9092 RVA: 0x000A9842 File Offset: 0x000A7A42
		public virtual object GetData(string format)
		{
			return this.GetData(format, true);
		}

		/// <summary>Returns the data associated with the specified class type format.</summary>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format of the data to retrieve.</param>
		/// <returns>The data associated with the specified format, or <see langword="null" />.</returns>
		// Token: 0x06002385 RID: 9093 RVA: 0x000A984C File Offset: 0x000A7A4C
		public virtual object GetData(Type format)
		{
			if (format == null)
			{
				return null;
			}
			return this.GetData(format.FullName);
		}

		/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format to check for.</param>
		/// <returns>
		///   <see langword="true" /> if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002386 RID: 9094 RVA: 0x000A9868 File Offset: 0x000A7A68
		public virtual bool GetDataPresent(Type format)
		{
			return !(format == null) && this.GetDataPresent(format.FullName);
		}

		/// <summary>Determines whether this <see cref="T:System.Windows.Forms.DataObject" /> contains data in the specified format or, optionally, contains data that can be converted to the specified format.</summary>
		/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> to determine whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> can be converted to the specified format; <see langword="false" /> to check whether the data is in the specified format.</param>
		/// <returns>
		///   <see langword="true" /> if the data is in, or can be converted to, the specified format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002387 RID: 9095 RVA: 0x000A9890 File Offset: 0x000A7A90
		public virtual bool GetDataPresent(string format, bool autoConvert)
		{
			return this.innerData.GetDataPresent(format, autoConvert);
		}

		/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
		/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <returns>
		///   <see langword="true" /> if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002388 RID: 9096 RVA: 0x000A98AC File Offset: 0x000A7AAC
		public virtual bool GetDataPresent(string format)
		{
			return this.GetDataPresent(format, true);
		}

		/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to, using an automatic conversion parameter to determine whether to retrieve only native data formats or all formats that the data can be converted to.</summary>
		/// <param name="autoConvert">
		///   <see langword="true" /> to retrieve all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to; <see langword="false" /> to retrieve only native data formats.</param>
		/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
		// Token: 0x06002389 RID: 9097 RVA: 0x000A98C3 File Offset: 0x000A7AC3
		public virtual string[] GetFormats(bool autoConvert)
		{
			return this.innerData.GetFormats(autoConvert);
		}

		/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
		// Token: 0x0600238A RID: 9098 RVA: 0x000A98D1 File Offset: 0x000A7AD1
		public virtual string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		/// <summary>Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
		/// <returns>
		///   <see langword="true" /> if the data object contains audio data; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600238B RID: 9099 RVA: 0x000A98DA File Offset: 0x000A7ADA
		public virtual bool ContainsAudio()
		{
			return this.GetDataPresent(DataFormats.WaveAudio, false);
		}

		/// <summary>Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</summary>
		/// <returns>
		///   <see langword="true" /> if the data object contains a file drop list; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600238C RID: 9100 RVA: 0x000A98E8 File Offset: 0x000A7AE8
		public virtual bool ContainsFileDropList()
		{
			return this.GetDataPresent(DataFormats.FileDrop, true);
		}

		/// <summary>Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</summary>
		/// <returns>
		///   <see langword="true" /> if the data object contains image data; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600238D RID: 9101 RVA: 0x000A98F6 File Offset: 0x000A7AF6
		public virtual bool ContainsImage()
		{
			return this.GetDataPresent(DataFormats.Bitmap, true);
		}

		/// <summary>Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
		/// <returns>
		///   <see langword="true" /> if the data object contains text data; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600238E RID: 9102 RVA: 0x000A9904 File Offset: 0x000A7B04
		public virtual bool ContainsText()
		{
			return this.ContainsText(TextDataFormat.UnicodeText);
		}

		/// <summary>Indicates whether the data object contains text data in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the data object contains text data in the specified format; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		// Token: 0x0600238F RID: 9103 RVA: 0x000A990D File Offset: 0x000A7B0D
		public virtual bool ContainsText(TextDataFormat format)
		{
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			return this.GetDataPresent(DataObject.ConvertToDataFormats(format), false);
		}

		/// <summary>Retrieves an audio stream from the data object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing audio data or <see langword="null" /> if the data object does not contain any data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</returns>
		// Token: 0x06002390 RID: 9104 RVA: 0x000A9942 File Offset: 0x000A7B42
		public virtual Stream GetAudioStream()
		{
			return this.GetData(DataFormats.WaveAudio, false) as Stream;
		}

		/// <summary>Retrieves a collection of file names from the data object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing file names or <see langword="null" /> if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</returns>
		// Token: 0x06002391 RID: 9105 RVA: 0x000A9958 File Offset: 0x000A7B58
		public virtual StringCollection GetFileDropList()
		{
			StringCollection stringCollection = new StringCollection();
			string[] array = this.GetData(DataFormats.FileDrop, true) as string[];
			if (array != null)
			{
				stringCollection.AddRange(array);
			}
			return stringCollection;
		}

		/// <summary>Retrieves an image from the data object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the image data in the data object or <see langword="null" /> if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</returns>
		// Token: 0x06002392 RID: 9106 RVA: 0x000A9988 File Offset: 0x000A7B88
		public virtual Image GetImage()
		{
			return this.GetData(DataFormats.Bitmap, true) as Image;
		}

		/// <summary>Retrieves text data from the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
		/// <returns>The text data in the data object or <see cref="F:System.String.Empty" /> if the data object does not contain data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</returns>
		// Token: 0x06002393 RID: 9107 RVA: 0x000A999B File Offset: 0x000A7B9B
		public virtual string GetText()
		{
			return this.GetText(TextDataFormat.UnicodeText);
		}

		/// <summary>Retrieves text data from the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <returns>The text data in the data object or <see cref="F:System.String.Empty" /> if the data object does not contain data in the specified format.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		// Token: 0x06002394 RID: 9108 RVA: 0x000A99A4 File Offset: 0x000A7BA4
		public virtual string GetText(TextDataFormat format)
		{
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			string text = this.GetData(DataObject.ConvertToDataFormats(format), false) as string;
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		/// <summary>Adds a <see cref="T:System.Byte" /> array to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format after converting it to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="audioBytes">A <see cref="T:System.Byte" /> array containing the audio data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioBytes" /> is <see langword="null" />.</exception>
		// Token: 0x06002395 RID: 9109 RVA: 0x000A99F4 File Offset: 0x000A7BF4
		public virtual void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			this.SetAudio(new MemoryStream(audioBytes));
		}

		/// <summary>Adds a <see cref="T:System.IO.Stream" /> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
		/// <param name="audioStream">A <see cref="T:System.IO.Stream" /> containing the audio data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioStream" /> is <see langword="null" />.</exception>
		// Token: 0x06002396 RID: 9110 RVA: 0x000A9A10 File Offset: 0x000A7C10
		public virtual void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			this.SetData(DataFormats.WaveAudio, false, audioStream);
		}

		/// <summary>Adds a collection of file names to the data object in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format.</summary>
		/// <param name="filePaths">A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file names.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filePaths" /> is <see langword="null" />.</exception>
		// Token: 0x06002397 RID: 9111 RVA: 0x000A9A30 File Offset: 0x000A7C30
		public virtual void SetFileDropList(StringCollection filePaths)
		{
			if (filePaths == null)
			{
				throw new ArgumentNullException("filePaths");
			}
			string[] array = new string[filePaths.Count];
			filePaths.CopyTo(array, 0);
			this.SetData(DataFormats.FileDrop, true, array);
		}

		/// <summary>Adds an <see cref="T:System.Drawing.Image" /> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the data object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x06002398 RID: 9112 RVA: 0x000A9A6C File Offset: 0x000A7C6C
		public virtual void SetImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.SetData(DataFormats.Bitmap, true, image);
		}

		/// <summary>Adds text data to the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
		/// <param name="textData">The text to add to the data object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textData" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06002399 RID: 9113 RVA: 0x000A9A89 File Offset: 0x000A7C89
		public virtual void SetText(string textData)
		{
			this.SetText(textData, TextDataFormat.UnicodeText);
		}

		/// <summary>Adds text data to the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <param name="textData">The text to add to the data object.</param>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textData" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		// Token: 0x0600239A RID: 9114 RVA: 0x000A9A94 File Offset: 0x000A7C94
		public virtual void SetText(string textData, TextDataFormat format)
		{
			if (string.IsNullOrEmpty(textData))
			{
				throw new ArgumentNullException("textData");
			}
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			this.SetData(DataObject.ConvertToDataFormats(format), false, textData);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000A9AE8 File Offset: 0x000A7CE8
		private static string ConvertToDataFormats(TextDataFormat format)
		{
			switch (format)
			{
			case TextDataFormat.UnicodeText:
				return DataFormats.UnicodeText;
			case TextDataFormat.Rtf:
				return DataFormats.Rtf;
			case TextDataFormat.Html:
				return DataFormats.Html;
			case TextDataFormat.CommaSeparatedValue:
				return DataFormats.CommaSeparatedValue;
			default:
				return DataFormats.UnicodeText;
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000A9B24 File Offset: 0x000A7D24
		private static string[] GetDistinctStrings(string[] formats)
		{
			ArrayList arrayList = new ArrayList();
			foreach (string text in formats)
			{
				if (!arrayList.Contains(text))
				{
					arrayList.Add(text);
				}
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000A9B70 File Offset: 0x000A7D70
		private static string[] GetMappedFormats(string format)
		{
			if (format == null)
			{
				return null;
			}
			if (format.Equals(DataFormats.Text) || format.Equals(DataFormats.UnicodeText) || format.Equals(DataFormats.StringFormat))
			{
				return new string[]
				{
					DataFormats.StringFormat,
					DataFormats.UnicodeText,
					DataFormats.Text
				};
			}
			if (format.Equals(DataFormats.FileDrop) || format.Equals(DataObject.CF_DEPRECATED_FILENAME) || format.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
			{
				return new string[]
				{
					DataFormats.FileDrop,
					DataObject.CF_DEPRECATED_FILENAMEW,
					DataObject.CF_DEPRECATED_FILENAME
				};
			}
			if (format.Equals(DataFormats.Bitmap) || format.Equals(typeof(Bitmap).FullName))
			{
				return new string[]
				{
					typeof(Bitmap).FullName,
					DataFormats.Bitmap
				};
			}
			return new string[] { format };
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000A9C60 File Offset: 0x000A7E60
		private bool GetTymedUseable(TYMED tymed)
		{
			for (int i = 0; i < DataObject.ALLOWED_TYMEDS.Length; i++)
			{
				if ((tymed & DataObject.ALLOWED_TYMEDS[i]) != TYMED.TYMED_NULL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000A9C90 File Offset: 0x000A7E90
		private void GetDataIntoOleStructs(ref FORMATETC formatetc, ref STGMEDIUM medium)
		{
			if (this.GetTymedUseable(formatetc.tymed) && this.GetTymedUseable(medium.tymed))
			{
				string name = DataFormats.GetFormat((int)formatetc.cfFormat).Name;
				if (!this.GetDataPresent(name))
				{
					Marshal.ThrowExceptionForHR(-2147221404);
					return;
				}
				object data = this.GetData(name);
				if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
				{
					int num = this.SaveDataToHandle(data, name, ref medium);
					if (NativeMethods.Failed(num))
					{
						Marshal.ThrowExceptionForHR(num);
						return;
					}
				}
				else
				{
					if ((formatetc.tymed & TYMED.TYMED_GDI) == TYMED.TYMED_NULL)
					{
						Marshal.ThrowExceptionForHR(-2147221399);
						return;
					}
					if (name.Equals(DataFormats.Bitmap) && data is Bitmap)
					{
						Bitmap bitmap = (Bitmap)data;
						if (bitmap != null)
						{
							medium.unionmember = this.GetCompatibleBitmap(bitmap);
							return;
						}
					}
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(-2147221399);
			}
		}

		/// <summary>Creates a connection between a data object and an advisory sink. This method is called by an object that supports an advisory sink and enables the advisory sink to be notified of changes in the object's data.</summary>
		/// <param name="pFormatetc">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, target device, aspect, and medium that will be used for future notifications.</param>
		/// <param name="advf">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> values that specifies a group of flags for controlling the advisory connection.</param>
		/// <param name="pAdvSink">A pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface on the advisory sink that will receive the change notification.</param>
		/// <param name="pdwConnection">When this method returns, contains a pointer to a DWORD token that identifies this connection. You can use this token later to delete the advisory connection by passing it to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(System.Int32)" />. If this value is zero, the connection was not established. This parameter is passed uninitialized.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The advisory connection was created.  
		///
		///   E_NOTIMPL  
		///
		///   This method is not implemented on the data object.  
		///
		///   DV_E_LINDEX  
		///
		///   There is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.  
		///
		///   DV_E_FORMATETC  
		///
		///   There is an invalid value for the <paramref name="pFormatetc" /> parameter.  
		///
		///   OLE_E_ADVISENOTSUPPORTED  
		///
		///   The data object does not support change notification.</returns>
		// Token: 0x060023A0 RID: 9120 RVA: 0x000A9D5C File Offset: 0x000A7F5C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink pAdvSink, out int pdwConnection)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.DAdvise(ref pFormatetc, advf, pAdvSink, out pdwConnection);
			}
			pdwConnection = 0;
			return -2147467263;
		}

		/// <summary>Destroys a notification connection that had been previously established.</summary>
		/// <param name="dwConnection">A DWORD token that specifies the connection to remove. Use the value returned by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> when the connection was originally established.</param>
		// Token: 0x060023A1 RID: 9121 RVA: 0x000A9D8F File Offset: 0x000A7F8F
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.DUnadvise(int dwConnection)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.DUnadvise(dwConnection);
				return;
			}
			Marshal.ThrowExceptionForHR(-2147467263);
		}

		/// <summary>Creates an object that can be used to enumerate the current advisory connections.</summary>
		/// <param name="enumAdvise">When this method returns, contains an <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumSTATDATA" /> that receives the interface pointer to the new enumerator object. If the implementation sets <paramref name="enumAdvise" /> to <see langword="null" />, there are no connections to advisory sinks at this time. This parameter is passed uninitialized.</param>
		/// <returns>This method supports the standard return value E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The enumerator object is successfully instantiated or there are no connections.  
		///
		///   OLE_E_ADVISENOTSUPPORTED  
		///
		///   This object does not support advisory notifications.</returns>
		// Token: 0x060023A2 RID: 9122 RVA: 0x000A9DBF File Offset: 0x000A7FBF
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.EnumDAdvise(out IEnumSTATDATA enumAdvise)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.EnumDAdvise(out enumAdvise);
			}
			enumAdvise = null;
			return -2147221501;
		}

		/// <summary>Creates an object for enumerating the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures for a data object. These structures are used in calls to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />.</summary>
		/// <param name="dwDirection">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.DATADIR" /> values that specifies the direction of the data.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The enumerator object was successfully created.  
		///
		///   E_NOTIMPL  
		///
		///   The direction specified by the <paramref name="direction" /> parameter is not supported.  
		///
		///   OLE_S_USEREG  
		///
		///   Requests that OLE enumerate the formats from the registry.</returns>
		// Token: 0x060023A3 RID: 9123 RVA: 0x000A9DF0 File Offset: 0x000A7FF0
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		IEnumFORMATETC IDataObject.EnumFormatEtc(DATADIR dwDirection)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.EnumFormatEtc(dwDirection);
			}
			if (dwDirection == DATADIR.DATADIR_GET)
			{
				return new DataObject.FormatEnumerator(this);
			}
			throw new ExternalException(SR.GetString("ExternalException"), -2147467263);
		}

		/// <summary>Provides a standard <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that is logically equivalent to a more complex structure. Use this method to determine whether two different <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures would return the same data, removing the need for duplicate rendering.</summary>
		/// <param name="pformatetcIn">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device that the caller would like to use to retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored.</param>
		/// <param name="pformatetcOut">When this method returns, contains a pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that contains the most general information possible for a specific rendering, making it canonically equivalent to <c>formatetIn</c>. The caller must allocate this structure and the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.FORMATETC@)" /> method must fill in the data. To retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, the caller uses the supplied value of <c>formatOut</c>, unless the value supplied is <see langword="null" />. This value is <see langword="null" /> if the method returns <see langword="DATA_S_SAMEFORMATETC" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored. This parameter is passed uninitialized.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The returned <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure is different from the one that was passed.  
		///
		///   DATA_S_SAMEFORMATETC  
		///
		///   The <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures are the same and <see langword="null" /> is returned in the <paramref name="formatOut" /> parameter.  
		///
		///   DV_E_LINDEX  
		///
		///   There is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.  
		///
		///   DV_E_FORMATETC  
		///
		///   There is an invalid value for the <paramref name="pFormatetc" /> parameter.  
		///
		///   OLE_E_NOTRUNNING  
		///
		///   The application is not running.</returns>
		// Token: 0x060023A4 RID: 9124 RVA: 0x000A9E40 File Offset: 0x000A8040
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.GetCanonicalFormatEtc(ref FORMATETC pformatetcIn, out FORMATETC pformatetcOut)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.GetCanonicalFormatEtc(ref pformatetcIn, out pformatetcOut);
			}
			pformatetcOut = default(FORMATETC);
			return 262448;
		}

		/// <summary>Obtains data from a source data object. The <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method, which is called by a data consumer, renders the data described in the specified <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure and transfers it through the specified <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure. The caller then assumes responsibility for releasing the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. It is possible to specify more than one medium by using the Boolean OR operator, allowing the method to choose the best medium among those specified.</param>
		/// <param name="medium">When this method returns, contains a pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that indicates the storage medium containing the returned data through its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed" /> member, and the responsibility for releasing the medium through the value of its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member. If <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> is <see langword="null" />, the receiver of the medium is responsible for releasing it; otherwise, <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> points to the <see langword="IUnknown" /> interface on the appropriate object so its <see langword="Release" /> method can be called. The medium must be allocated and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory to perform this operation.</exception>
		// Token: 0x060023A5 RID: 9125 RVA: 0x000A9E74 File Offset: 0x000A8074
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.GetData(ref FORMATETC formatetc, out STGMEDIUM medium)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.GetData(ref formatetc, out medium);
				return;
			}
			medium = default(STGMEDIUM);
			if (this.GetTymedUseable(formatetc.tymed))
			{
				if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
				{
					medium.tymed = TYMED.TYMED_HGLOBAL;
					medium.unionmember = UnsafeNativeMethods.GlobalAlloc(8258, 1);
					if (medium.unionmember == IntPtr.Zero)
					{
						throw new OutOfMemoryException();
					}
					try
					{
						((IDataObject)this).GetDataHere(ref formatetc, ref medium);
						return;
					}
					catch
					{
						UnsafeNativeMethods.GlobalFree(new HandleRef(medium, medium.unionmember));
						medium.unionmember = IntPtr.Zero;
						throw;
					}
				}
				medium.tymed = formatetc.tymed;
				((IDataObject)this).GetDataHere(ref formatetc, ref medium);
				return;
			}
			Marshal.ThrowExceptionForHR(-2147221399);
		}

		/// <summary>Obtains data from a source data object. This method, which is called by a data consumer, differs from the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method in that the caller must allocate and free the specified storage medium.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. Only one medium can be specified in <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" />, and only the following <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> values are valid: <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTORAGE" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL" />, or <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_FILE" />.</param>
		/// <param name="medium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passed by reference, that defines the storage medium containing the data being transferred. The medium must be allocated by the caller and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The caller must also free the medium. The implementation of this method must always supply a value of <see langword="null" /> for the <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member of the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that this parameter points to.</param>
		// Token: 0x060023A6 RID: 9126 RVA: 0x000A9F5C File Offset: 0x000A815C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.GetDataHere(ref FORMATETC formatetc, ref STGMEDIUM medium)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.GetDataHere(ref formatetc, ref medium);
				return;
			}
			this.GetDataIntoOleStructs(ref formatetc, ref medium);
		}

		/// <summary>Determines whether the data object is capable of rendering the data described in the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure. Objects attempting a paste or drop operation can call this method before calling <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> to get an indication of whether the operation may be successful.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use for the query.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   A subsequent call to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> would probably be successful.  
		///
		///   DV_E_LINDEX  
		///
		///   An invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.  
		///
		///   DV_E_FORMATETC  
		///
		///   An invalid value for the <paramref name="pFormatetc" /> parameter.  
		///
		///   DV_E_TYMED  
		///
		///   An invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.tymed" /> value.  
		///
		///   DV_E_DVASPECT  
		///
		///   An invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.dwAspect" /> value.  
		///
		///   OLE_E_NOTRUNNING  
		///
		///   The application is not running.</returns>
		// Token: 0x060023A7 RID: 9127 RVA: 0x000A9F8C File Offset: 0x000A818C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.QueryGetData(ref FORMATETC formatetc)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.QueryGetData(ref formatetc);
			}
			if (formatetc.dwAspect != DVASPECT.DVASPECT_CONTENT)
			{
				return -2147221397;
			}
			if (!this.GetTymedUseable(formatetc.tymed))
			{
				return -2147221399;
			}
			if (formatetc.cfFormat == 0)
			{
				return 1;
			}
			if (!this.GetDataPresent(DataFormats.GetFormat((int)formatetc.cfFormat).Name))
			{
				return -2147221404;
			}
			return 0;
		}

		/// <summary>Transfers data to the object that implements this method. This method is called by an object that contains a data source.</summary>
		/// <param name="pFormatetcIn">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format used by the data object when interpreting the data contained in the storage medium.</param>
		/// <param name="pmedium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure, passed by reference, that defines the storage medium in which the data is being passed.</param>
		/// <param name="fRelease">
		///   <see langword="true" /> to specify that the data object called, which implements <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />, owns the storage medium after the call returns. This means that the data object must free the medium after it has been used by calling the <see langword="ReleaseStgMedium" /> function. <see langword="false" /> to specify that the caller retains ownership of the storage medium, and the data object called uses the storage medium for the duration of the call only.</param>
		/// <exception cref="T:System.NotImplementedException">This method does not support the type of the underlying data object.</exception>
		// Token: 0x060023A8 RID: 9128 RVA: 0x000AA009 File Offset: 0x000A8209
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.SetData(ref FORMATETC pFormatetcIn, ref STGMEDIUM pmedium, bool fRelease)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.SetData(ref pFormatetcIn, ref pmedium, fRelease);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000AA038 File Offset: 0x000A8238
		private int SaveDataToHandle(object data, string format, ref STGMEDIUM medium)
		{
			int num = -2147467259;
			if (data is Stream)
			{
				num = this.SaveStreamToHandle(ref medium.unionmember, (Stream)data);
			}
			else if (format.Equals(DataFormats.Text) || format.Equals(DataFormats.Rtf) || format.Equals(DataFormats.OemText))
			{
				num = this.SaveStringToHandle(medium.unionmember, data.ToString(), false);
			}
			else if (format.Equals(DataFormats.Html))
			{
				if (WindowsFormsUtils.TargetsAtLeast_v4_5)
				{
					num = this.SaveHtmlToHandle(medium.unionmember, data.ToString());
				}
				else
				{
					num = this.SaveStringToHandle(medium.unionmember, data.ToString(), false);
				}
			}
			else if (format.Equals(DataFormats.UnicodeText))
			{
				num = this.SaveStringToHandle(medium.unionmember, data.ToString(), true);
			}
			else if (format.Equals(DataFormats.FileDrop))
			{
				num = this.SaveFileListToHandle(medium.unionmember, (string[])data);
			}
			else if (format.Equals(DataObject.CF_DEPRECATED_FILENAME))
			{
				string[] array = (string[])data;
				num = this.SaveStringToHandle(medium.unionmember, array[0], false);
			}
			else if (format.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
			{
				string[] array2 = (string[])data;
				num = this.SaveStringToHandle(medium.unionmember, array2[0], true);
			}
			else if (format.Equals(DataFormats.Dib) && data is Image)
			{
				num = -2147221399;
			}
			else if (format.Equals(DataFormats.Serializable) || data is ISerializable || (data != null && data.GetType().IsSerializable))
			{
				num = this.SaveObjectToHandle(ref medium.unionmember, data);
			}
			return num;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000AA1DC File Offset: 0x000A83DC
		private int SaveObjectToHandle(ref IntPtr handle, object data)
		{
			Stream stream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(DataObject.serializedObjectID);
			DataObject.SaveObjectToHandleSerializer(stream, data);
			return this.SaveStreamToHandle(ref handle, stream);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000AA210 File Offset: 0x000A8410
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static void SaveObjectToHandleSerializer(Stream stream, object data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, data);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000AA22C File Offset: 0x000A842C
		private int SaveStreamToHandle(ref IntPtr handle, Stream stream)
		{
			if (handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.GlobalFree(new HandleRef(null, handle));
			}
			int num = (int)stream.Length;
			handle = UnsafeNativeMethods.GlobalAlloc(8194, num);
			if (handle == IntPtr.Zero)
			{
				return -2147024882;
			}
			IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
			if (intPtr == IntPtr.Zero)
			{
				return -2147024882;
			}
			try
			{
				byte[] array = new byte[num];
				stream.Position = 0L;
				stream.Read(array, 0, num);
				Marshal.Copy(array, 0, intPtr, num);
			}
			finally
			{
				UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
			}
			return 0;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000AA2E4 File Offset: 0x000A84E4
		private int SaveFileListToHandle(IntPtr handle, string[] files)
		{
			if (files == null)
			{
				return 0;
			}
			if (files.Length < 1)
			{
				return 0;
			}
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			bool flag = Marshal.SystemDefaultCharSize != 1;
			IntPtr intPtr = IntPtr.Zero;
			int num = 20;
			int num2 = num;
			if (flag)
			{
				for (int i = 0; i < files.Length; i++)
				{
					num2 += (files[i].Length + 1) * 2;
				}
				num2 += 2;
			}
			else
			{
				for (int j = 0; j < files.Length; j++)
				{
					num2 += NativeMethods.Util.GetPInvokeStringLength(files[j]) + 1;
				}
				num2++;
			}
			IntPtr intPtr2 = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), num2, 8194);
			if (intPtr2 == IntPtr.Zero)
			{
				return -2147024882;
			}
			IntPtr intPtr3 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr2));
			if (intPtr3 == IntPtr.Zero)
			{
				return -2147024882;
			}
			intPtr = intPtr3;
			int[] array = new int[5];
			array[0] = num;
			int[] array2 = array;
			if (flag)
			{
				array2[4] = -1;
			}
			Marshal.Copy(array2, 0, intPtr, array2.Length);
			intPtr = (IntPtr)((long)intPtr + (long)num);
			for (int k = 0; k < files.Length; k++)
			{
				if (flag)
				{
					UnsafeNativeMethods.CopyMemoryW(intPtr, files[k], files[k].Length * 2);
					intPtr = (IntPtr)((long)intPtr + (long)(files[k].Length * 2));
					Marshal.Copy(new byte[2], 0, intPtr, 2);
					intPtr = (IntPtr)((long)intPtr + 2L);
				}
				else
				{
					int pinvokeStringLength = NativeMethods.Util.GetPInvokeStringLength(files[k]);
					UnsafeNativeMethods.CopyMemoryA(intPtr, files[k], pinvokeStringLength);
					intPtr = (IntPtr)((long)intPtr + (long)pinvokeStringLength);
					Marshal.Copy(new byte[1], 0, intPtr, 1);
					intPtr = (IntPtr)((long)intPtr + 1L);
				}
			}
			if (flag)
			{
				Marshal.Copy(new char[1], 0, intPtr, 1);
				intPtr = (IntPtr)((long)intPtr + 2L);
			}
			else
			{
				Marshal.Copy(new byte[1], 0, intPtr, 1);
				intPtr = (IntPtr)((long)intPtr + 1L);
			}
			UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr2));
			return 0;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000AA4F0 File Offset: 0x000A86F0
		private int SaveStringToHandle(IntPtr handle, string str, bool unicode)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			IntPtr intPtr = IntPtr.Zero;
			if (unicode)
			{
				int num = str.Length * 2 + 2;
				intPtr = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), num, 8258);
				if (intPtr == IntPtr.Zero)
				{
					return -2147024882;
				}
				IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
				if (intPtr2 == IntPtr.Zero)
				{
					return -2147024882;
				}
				char[] array = str.ToCharArray(0, str.Length);
				UnsafeNativeMethods.CopyMemoryW(intPtr2, array, array.Length * 2);
			}
			else
			{
				int num2 = UnsafeNativeMethods.WideCharToMultiByte(0, 0, str, str.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
				byte[] array2 = new byte[num2];
				UnsafeNativeMethods.WideCharToMultiByte(0, 0, str, str.Length, array2, array2.Length, IntPtr.Zero, IntPtr.Zero);
				intPtr = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), num2 + 1, 8258);
				if (intPtr == IntPtr.Zero)
				{
					return -2147024882;
				}
				IntPtr intPtr3 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
				if (intPtr3 == IntPtr.Zero)
				{
					return -2147024882;
				}
				UnsafeNativeMethods.CopyMemory(intPtr3, array2, num2);
				Marshal.Copy(new byte[1], 0, (IntPtr)((long)intPtr3 + (long)num2), 1);
			}
			if (intPtr != IntPtr.Zero)
			{
				UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
			}
			return 0;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000AA65C File Offset: 0x000A885C
		private int SaveHtmlToHandle(IntPtr handle, string str)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			IntPtr intPtr = IntPtr.Zero;
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] bytes = utf8Encoding.GetBytes(str);
			intPtr = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), bytes.Length + 1, 8258);
			if (intPtr == IntPtr.Zero)
			{
				return -2147024882;
			}
			IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
			if (intPtr2 == IntPtr.Zero)
			{
				return -2147024882;
			}
			try
			{
				UnsafeNativeMethods.CopyMemory(intPtr2, bytes, bytes.Length);
				Marshal.Copy(new byte[1], 0, (IntPtr)((long)intPtr2 + (long)bytes.Length), 1);
			}
			finally
			{
				UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
			}
			return 0;
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format and indicating whether the data can be converted to another format.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> to allow the data to be converted to another format; otherwise, <see langword="false" />.</param>
		/// <param name="data">The data to store.</param>
		// Token: 0x060023B0 RID: 9136 RVA: 0x000AA724 File Offset: 0x000A8924
		public virtual void SetData(string format, bool autoConvert, object data)
		{
			this.innerData.SetData(format, autoConvert, data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="data">The data to store.</param>
		// Token: 0x060023B1 RID: 9137 RVA: 0x000AA734 File Offset: 0x000A8934
		public virtual void SetData(string format, object data)
		{
			this.innerData.SetData(format, data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified type as the format.</summary>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format associated with the data.</param>
		/// <param name="data">The data to store.</param>
		// Token: 0x060023B2 RID: 9138 RVA: 0x000AA743 File Offset: 0x000A8943
		public virtual void SetData(Type format, object data)
		{
			this.innerData.SetData(format, data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the object type as the data format.</summary>
		/// <param name="data">The data to store.</param>
		// Token: 0x060023B3 RID: 9139 RVA: 0x000AA752 File Offset: 0x000A8952
		public virtual void SetData(object data)
		{
			this.innerData.SetData(data);
		}

		// Token: 0x04000E97 RID: 3735
		private static readonly string CF_DEPRECATED_FILENAME = "FileName";

		// Token: 0x04000E98 RID: 3736
		private static readonly string CF_DEPRECATED_FILENAMEW = "FileNameW";

		// Token: 0x04000E99 RID: 3737
		private const int DV_E_FORMATETC = -2147221404;

		// Token: 0x04000E9A RID: 3738
		private const int DV_E_LINDEX = -2147221400;

		// Token: 0x04000E9B RID: 3739
		private const int DV_E_TYMED = -2147221399;

		// Token: 0x04000E9C RID: 3740
		private const int DV_E_DVASPECT = -2147221397;

		// Token: 0x04000E9D RID: 3741
		private const int OLE_E_NOTRUNNING = -2147221499;

		// Token: 0x04000E9E RID: 3742
		private const int OLE_E_ADVISENOTSUPPORTED = -2147221501;

		// Token: 0x04000E9F RID: 3743
		private const int DATA_S_SAMEFORMATETC = 262448;

		// Token: 0x04000EA0 RID: 3744
		private static readonly TYMED[] ALLOWED_TYMEDS = new TYMED[]
		{
			TYMED.TYMED_HGLOBAL,
			TYMED.TYMED_ISTREAM,
			TYMED.TYMED_ENHMF,
			TYMED.TYMED_MFPICT,
			TYMED.TYMED_GDI
		};

		// Token: 0x04000EA1 RID: 3745
		private IDataObject innerData;

		// Token: 0x04000EA3 RID: 3747
		private static readonly byte[] serializedObjectID = new Guid("FD9EA796-3B13-4370-A679-56106BB288FB").ToByteArray();

		// Token: 0x0200067D RID: 1661
		private class FormatEnumerator : IEnumFORMATETC
		{
			// Token: 0x060066BC RID: 26300 RVA: 0x0017FE7D File Offset: 0x0017E07D
			public FormatEnumerator(IDataObject parent)
				: this(parent, parent.GetFormats())
			{
			}

			// Token: 0x060066BD RID: 26301 RVA: 0x0017FE8C File Offset: 0x0017E08C
			public FormatEnumerator(IDataObject parent, FORMATETC[] formats)
			{
				this.formats = new ArrayList();
				base..ctor();
				this.formats.Clear();
				this.parent = parent;
				this.current = 0;
				if (formats != null)
				{
					DataObject dataObject = parent as DataObject;
					if (dataObject != null && dataObject.RestrictedFormats && !Clipboard.IsFormatValid(formats))
					{
						throw new SecurityException(SR.GetString("ClipboardSecurityException"));
					}
					foreach (FORMATETC formatetc in formats)
					{
						FORMATETC formatetc2 = default(FORMATETC);
						formatetc2.cfFormat = formatetc.cfFormat;
						formatetc2.dwAspect = formatetc.dwAspect;
						formatetc2.ptd = formatetc.ptd;
						formatetc2.lindex = formatetc.lindex;
						formatetc2.tymed = formatetc.tymed;
						this.formats.Add(formatetc2);
					}
				}
			}

			// Token: 0x060066BE RID: 26302 RVA: 0x0017FF64 File Offset: 0x0017E164
			public FormatEnumerator(IDataObject parent, string[] formats)
			{
				this.formats = new ArrayList();
				base..ctor();
				this.parent = parent;
				this.formats.Clear();
				string bitmap = DataFormats.Bitmap;
				string enhancedMetafile = DataFormats.EnhancedMetafile;
				string text = DataFormats.Text;
				string unicodeText = DataFormats.UnicodeText;
				string stringFormat = DataFormats.StringFormat;
				string stringFormat2 = DataFormats.StringFormat;
				if (formats != null)
				{
					DataObject dataObject = parent as DataObject;
					if (dataObject != null && dataObject.RestrictedFormats && !Clipboard.IsFormatValid(formats))
					{
						throw new SecurityException(SR.GetString("ClipboardSecurityException"));
					}
					foreach (string text2 in formats)
					{
						FORMATETC formatetc = default(FORMATETC);
						formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(text2).Id);
						formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
						formatetc.ptd = IntPtr.Zero;
						formatetc.lindex = -1;
						if (text2.Equals(bitmap))
						{
							formatetc.tymed = TYMED.TYMED_GDI;
						}
						else if (text2.Equals(enhancedMetafile))
						{
							formatetc.tymed = TYMED.TYMED_ENHMF;
						}
						else if (text2.Equals(text) || text2.Equals(unicodeText) || text2.Equals(stringFormat) || text2.Equals(stringFormat2) || text2.Equals(DataFormats.Rtf) || text2.Equals(DataFormats.CommaSeparatedValue) || text2.Equals(DataFormats.FileDrop) || text2.Equals(DataObject.CF_DEPRECATED_FILENAME) || text2.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
						{
							formatetc.tymed = TYMED.TYMED_HGLOBAL;
						}
						else
						{
							formatetc.tymed = TYMED.TYMED_HGLOBAL;
						}
						if (formatetc.tymed != TYMED.TYMED_NULL)
						{
							this.formats.Add(formatetc);
						}
					}
				}
			}

			// Token: 0x060066BF RID: 26303 RVA: 0x00180114 File Offset: 0x0017E314
			public int Next(int celt, FORMATETC[] rgelt, int[] pceltFetched)
			{
				if (this.current < this.formats.Count && celt > 0)
				{
					FORMATETC formatetc = (FORMATETC)this.formats[this.current];
					rgelt[0].cfFormat = formatetc.cfFormat;
					rgelt[0].tymed = formatetc.tymed;
					rgelt[0].dwAspect = DVASPECT.DVASPECT_CONTENT;
					rgelt[0].ptd = IntPtr.Zero;
					rgelt[0].lindex = -1;
					if (pceltFetched != null)
					{
						pceltFetched[0] = 1;
					}
					this.current++;
					return 0;
				}
				if (pceltFetched != null)
				{
					pceltFetched[0] = 0;
				}
				return 1;
			}

			// Token: 0x060066C0 RID: 26304 RVA: 0x001801C2 File Offset: 0x0017E3C2
			public int Skip(int celt)
			{
				if (this.current + celt >= this.formats.Count)
				{
					return 1;
				}
				this.current += celt;
				return 0;
			}

			// Token: 0x060066C1 RID: 26305 RVA: 0x001801EA File Offset: 0x0017E3EA
			public int Reset()
			{
				this.current = 0;
				return 0;
			}

			// Token: 0x060066C2 RID: 26306 RVA: 0x001801F4 File Offset: 0x0017E3F4
			public void Clone(out IEnumFORMATETC ppenum)
			{
				FORMATETC[] array = new FORMATETC[this.formats.Count];
				this.formats.CopyTo(array, 0);
				ppenum = new DataObject.FormatEnumerator(this.parent, array);
			}

			// Token: 0x04003A79 RID: 14969
			internal IDataObject parent;

			// Token: 0x04003A7A RID: 14970
			internal ArrayList formats;

			// Token: 0x04003A7B RID: 14971
			internal int current;
		}

		// Token: 0x0200067E RID: 1662
		private class OleConverter : IDataObject
		{
			// Token: 0x060066C3 RID: 26307 RVA: 0x0018022D File Offset: 0x0017E42D
			public OleConverter(IDataObject data)
			{
				this.innerData = data;
			}

			// Token: 0x1700166A RID: 5738
			// (get) Token: 0x060066C4 RID: 26308 RVA: 0x0018023C File Offset: 0x0017E43C
			public IDataObject OleDataObject
			{
				get
				{
					return this.innerData;
				}
			}

			// Token: 0x060066C5 RID: 26309 RVA: 0x00180244 File Offset: 0x0017E444
			private object GetDataFromOleIStream(string format)
			{
				FORMATETC formatetc = default(FORMATETC);
				STGMEDIUM stgmedium = default(STGMEDIUM);
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = TYMED.TYMED_ISTREAM;
				stgmedium.tymed = TYMED.TYMED_ISTREAM;
				if (this.QueryGetDataUnsafe(ref formatetc) != 0)
				{
					return null;
				}
				try
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						this.innerData.GetData(ref formatetc, out stgmedium);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				catch
				{
					return null;
				}
				if (stgmedium.unionmember != IntPtr.Zero)
				{
					UnsafeNativeMethods.IStream stream = (UnsafeNativeMethods.IStream)Marshal.GetObjectForIUnknown(stgmedium.unionmember);
					Marshal.Release(stgmedium.unionmember);
					NativeMethods.STATSTG statstg = new NativeMethods.STATSTG();
					stream.Stat(statstg, 0);
					int num = (int)statstg.cbSize;
					IntPtr intPtr = UnsafeNativeMethods.GlobalAlloc(8258, num);
					IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(this.innerData, intPtr));
					stream.Read(intPtr2, num);
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(this.innerData, intPtr));
					return this.GetDataFromHGLOBLAL(format, intPtr);
				}
				return null;
			}

			// Token: 0x060066C6 RID: 26310 RVA: 0x00180384 File Offset: 0x0017E584
			private object GetDataFromHGLOBLAL(string format, IntPtr hglobal)
			{
				object obj = null;
				if (hglobal != IntPtr.Zero)
				{
					if (format.Equals(DataFormats.Text) || format.Equals(DataFormats.Rtf) || format.Equals(DataFormats.OemText))
					{
						obj = this.ReadStringFromHandle(hglobal, false);
					}
					else if (format.Equals(DataFormats.Html))
					{
						if (WindowsFormsUtils.TargetsAtLeast_v4_5)
						{
							obj = this.ReadHtmlFromHandle(hglobal);
						}
						else
						{
							obj = this.ReadStringFromHandle(hglobal, false);
						}
					}
					else if (format.Equals(DataFormats.UnicodeText))
					{
						obj = this.ReadStringFromHandle(hglobal, true);
					}
					else if (format.Equals(DataFormats.FileDrop))
					{
						obj = this.ReadFileListFromHandle(hglobal);
					}
					else if (format.Equals(DataObject.CF_DEPRECATED_FILENAME))
					{
						obj = new string[] { this.ReadStringFromHandle(hglobal, false) };
					}
					else if (format.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
					{
						obj = new string[] { this.ReadStringFromHandle(hglobal, true) };
					}
					else if (!LocalAppContextSwitches.EnableLegacyDangerousClipboardDeserializationMode)
					{
						bool flag = format.Equals(DataFormats.StringFormat) || format.Equals(typeof(Bitmap).FullName) || format.Equals(DataFormats.CommaSeparatedValue) || format.Equals(DataFormats.Dib) || format.Equals(DataFormats.Dif) || format.Equals(DataFormats.Locale) || format.Equals(DataFormats.PenData) || format.Equals(DataFormats.Riff) || format.Equals(DataFormats.SymbolicLink) || format.Equals(DataFormats.Tiff) || format.Equals(DataFormats.WaveAudio) || format.Equals(DataFormats.Bitmap) || format.Equals(DataFormats.EnhancedMetafile) || format.Equals(DataFormats.Palette) || format.Equals(DataFormats.MetafilePict);
						obj = this.ReadObjectFromHandle(hglobal, flag);
					}
					else
					{
						obj = this.ReadObjectFromHandle(hglobal, false);
					}
					UnsafeNativeMethods.GlobalFree(new HandleRef(null, hglobal));
				}
				return obj;
			}

			// Token: 0x060066C7 RID: 26311 RVA: 0x00180590 File Offset: 0x0017E790
			private object GetDataFromOleHGLOBAL(string format, out bool done)
			{
				done = false;
				FORMATETC formatetc = default(FORMATETC);
				STGMEDIUM stgmedium = default(STGMEDIUM);
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = TYMED.TYMED_HGLOBAL;
				stgmedium.tymed = TYMED.TYMED_HGLOBAL;
				object obj = null;
				if (this.QueryGetDataUnsafe(ref formatetc) == 0)
				{
					try
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							this.innerData.GetData(ref formatetc, out stgmedium);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						if (stgmedium.unionmember != IntPtr.Zero)
						{
							obj = this.GetDataFromHGLOBLAL(format, stgmedium.unionmember);
						}
					}
					catch (DataObject.OleConverter.RestrictedTypeDeserializationException)
					{
						done = true;
					}
					catch
					{
					}
				}
				return obj;
			}

			// Token: 0x060066C8 RID: 26312 RVA: 0x00180668 File Offset: 0x0017E868
			private object GetDataFromOleOther(string format)
			{
				FORMATETC formatetc = default(FORMATETC);
				STGMEDIUM stgmedium = default(STGMEDIUM);
				TYMED tymed = TYMED.TYMED_NULL;
				if (format.Equals(DataFormats.Bitmap))
				{
					tymed = TYMED.TYMED_GDI;
				}
				else if (format.Equals(DataFormats.EnhancedMetafile))
				{
					tymed = TYMED.TYMED_ENHMF;
				}
				if (tymed == TYMED.TYMED_NULL)
				{
					return null;
				}
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = tymed;
				stgmedium.tymed = tymed;
				object obj = null;
				if (this.QueryGetDataUnsafe(ref formatetc) == 0)
				{
					try
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							this.innerData.GetData(ref formatetc, out stgmedium);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					catch
					{
					}
				}
				if (stgmedium.unionmember != IntPtr.Zero && format.Equals(DataFormats.Bitmap))
				{
					System.Internal.HandleCollector.Add(stgmedium.unionmember, NativeMethods.CommonHandles.GDI);
					Image image = null;
					IntSecurity.ObjectFromWin32Handle.Assert();
					try
					{
						image = Image.FromHbitmap(stgmedium.unionmember);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (image != null)
					{
						Image image2 = image;
						image = (Image)image.Clone();
						SafeNativeMethods.DeleteObject(new HandleRef(null, stgmedium.unionmember));
						image2.Dispose();
					}
					obj = image;
				}
				return obj;
			}

			// Token: 0x060066C9 RID: 26313 RVA: 0x001807C4 File Offset: 0x0017E9C4
			private object GetDataFromBoundOleDataObject(string format, out bool done)
			{
				object obj = null;
				done = false;
				try
				{
					obj = this.GetDataFromOleOther(format);
					if (obj == null)
					{
						obj = this.GetDataFromOleHGLOBAL(format, out done);
					}
					if (obj == null && !done)
					{
						obj = this.GetDataFromOleIStream(format);
					}
				}
				catch (Exception ex)
				{
				}
				return obj;
			}

			// Token: 0x060066CA RID: 26314 RVA: 0x00180810 File Offset: 0x0017EA10
			private Stream ReadByteStreamFromHandle(IntPtr handle, out bool isSerializedObject)
			{
				IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
				if (intPtr == IntPtr.Zero)
				{
					throw new ExternalException(SR.GetString("ExternalException"), -2147024882);
				}
				Stream stream;
				try
				{
					int num = UnsafeNativeMethods.GlobalSize(new HandleRef(null, handle));
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					int num2 = 0;
					if (num > DataObject.serializedObjectID.Length)
					{
						isSerializedObject = true;
						for (int i = 0; i < DataObject.serializedObjectID.Length; i++)
						{
							if (DataObject.serializedObjectID[i] != array[i])
							{
								isSerializedObject = false;
								break;
							}
						}
						if (isSerializedObject)
						{
							num2 = DataObject.serializedObjectID.Length;
						}
					}
					else
					{
						isSerializedObject = false;
					}
					stream = new MemoryStream(array, num2, array.Length - num2);
				}
				finally
				{
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
				}
				return stream;
			}

			// Token: 0x060066CB RID: 26315 RVA: 0x001808E4 File Offset: 0x0017EAE4
			private object ReadObjectFromHandle(IntPtr handle, bool restrictDeserialization)
			{
				bool flag;
				Stream stream = this.ReadByteStreamFromHandle(handle, out flag);
				object obj;
				if (flag)
				{
					obj = DataObject.OleConverter.ReadObjectFromHandleDeserializer(stream, restrictDeserialization);
				}
				else
				{
					obj = stream;
				}
				return obj;
			}

			// Token: 0x060066CC RID: 26316 RVA: 0x00180910 File Offset: 0x0017EB10
			private static object ReadObjectFromHandleDeserializer(Stream stream, bool restrictDeserialization)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (restrictDeserialization)
				{
					binaryFormatter.Binder = new DataObject.OleConverter.RestrictiveBinder();
				}
				binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				return binaryFormatter.Deserialize(stream);
			}

			// Token: 0x060066CD RID: 26317 RVA: 0x00180940 File Offset: 0x0017EB40
			private string[] ReadFileListFromHandle(IntPtr hdrop)
			{
				string[] array = null;
				StringBuilder stringBuilder = new StringBuilder(260);
				int num = UnsafeNativeMethods.DragQueryFile(new HandleRef(null, hdrop), -1, null, 0);
				if (num > 0)
				{
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						int num2 = UnsafeNativeMethods.DragQueryFileLongPath(new HandleRef(null, hdrop), i, stringBuilder);
						if (num2 != 0)
						{
							string text = stringBuilder.ToString(0, num2);
							string fullPath = Path.GetFullPath(text);
							new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPath).Demand();
							array[i] = text;
						}
					}
				}
				return array;
			}

			// Token: 0x060066CE RID: 26318 RVA: 0x001809BC File Offset: 0x0017EBBC
			private unsafe string ReadStringFromHandle(IntPtr handle, bool unicode)
			{
				string text = null;
				IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
				try
				{
					if (unicode)
					{
						text = new string((char*)(void*)intPtr);
					}
					else
					{
						text = new string((sbyte*)(void*)intPtr);
					}
				}
				finally
				{
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
				}
				return text;
			}

			// Token: 0x060066CF RID: 26319 RVA: 0x00180A18 File Offset: 0x0017EC18
			private string ReadHtmlFromHandle(IntPtr handle)
			{
				string text = null;
				IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
				try
				{
					int num = UnsafeNativeMethods.GlobalSize(new HandleRef(null, handle));
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					text = Encoding.UTF8.GetString(array);
				}
				finally
				{
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
				}
				return text;
			}

			// Token: 0x060066D0 RID: 26320 RVA: 0x00180A80 File Offset: 0x0017EC80
			public virtual object GetData(string format, bool autoConvert)
			{
				bool flag = false;
				object obj = this.GetDataFromBoundOleDataObject(format, out flag);
				object obj2 = obj;
				if (!flag && autoConvert && (obj == null || obj is MemoryStream))
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						int num = 0;
						while (!flag && num < mappedFormats.Length)
						{
							if (!format.Equals(mappedFormats[num]))
							{
								obj = this.GetDataFromBoundOleDataObject(mappedFormats[num], out flag);
								if (!flag && obj != null && !(obj is MemoryStream))
								{
									obj2 = null;
									break;
								}
							}
							num++;
						}
					}
				}
				if (obj2 != null)
				{
					return obj2;
				}
				return obj;
			}

			// Token: 0x060066D1 RID: 26321 RVA: 0x00180AFE File Offset: 0x0017ECFE
			public virtual object GetData(string format)
			{
				return this.GetData(format, true);
			}

			// Token: 0x060066D2 RID: 26322 RVA: 0x00180B08 File Offset: 0x0017ED08
			public virtual object GetData(Type format)
			{
				return this.GetData(format.FullName);
			}

			// Token: 0x060066D3 RID: 26323 RVA: 0x000070A6 File Offset: 0x000052A6
			public virtual void SetData(string format, bool autoConvert, object data)
			{
			}

			// Token: 0x060066D4 RID: 26324 RVA: 0x00180B16 File Offset: 0x0017ED16
			public virtual void SetData(string format, object data)
			{
				this.SetData(format, true, data);
			}

			// Token: 0x060066D5 RID: 26325 RVA: 0x00180B21 File Offset: 0x0017ED21
			public virtual void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x060066D6 RID: 26326 RVA: 0x00180B30 File Offset: 0x0017ED30
			public virtual void SetData(object data)
			{
				if (data is ISerializable)
				{
					this.SetData(DataFormats.Serializable, data);
					return;
				}
				this.SetData(data.GetType(), data);
			}

			// Token: 0x060066D7 RID: 26327 RVA: 0x00180B54 File Offset: 0x0017ED54
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			private int QueryGetDataUnsafe(ref FORMATETC formatetc)
			{
				return this.innerData.QueryGetData(ref formatetc);
			}

			// Token: 0x060066D8 RID: 26328 RVA: 0x00180B54 File Offset: 0x0017ED54
			private int QueryGetDataInner(ref FORMATETC formatetc)
			{
				return this.innerData.QueryGetData(ref formatetc);
			}

			// Token: 0x060066D9 RID: 26329 RVA: 0x00180B62 File Offset: 0x0017ED62
			public virtual bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName);
			}

			// Token: 0x060066DA RID: 26330 RVA: 0x00180B70 File Offset: 0x0017ED70
			private bool GetDataPresentInner(string format)
			{
				FORMATETC formatetc = default(FORMATETC);
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				for (int i = 0; i < DataObject.ALLOWED_TYMEDS.Length; i++)
				{
					formatetc.tymed |= DataObject.ALLOWED_TYMEDS[i];
				}
				int num = this.QueryGetDataUnsafe(ref formatetc);
				return num == 0;
			}

			// Token: 0x060066DB RID: 26331 RVA: 0x00180BDC File Offset: 0x0017EDDC
			public virtual bool GetDataPresent(string format, bool autoConvert)
			{
				IntSecurity.ClipboardRead.Demand();
				bool flag = false;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					flag = this.GetDataPresentInner(format);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (!flag && autoConvert)
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!format.Equals(mappedFormats[i]))
							{
								IntSecurity.UnmanagedCode.Assert();
								try
								{
									flag = this.GetDataPresentInner(mappedFormats[i]);
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
								if (flag)
								{
									break;
								}
							}
						}
					}
				}
				return flag;
			}

			// Token: 0x060066DC RID: 26332 RVA: 0x00180C78 File Offset: 0x0017EE78
			public virtual bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, true);
			}

			// Token: 0x060066DD RID: 26333 RVA: 0x00180C84 File Offset: 0x0017EE84
			public virtual string[] GetFormats(bool autoConvert)
			{
				IEnumFORMATETC enumFORMATETC = null;
				ArrayList arrayList = new ArrayList();
				try
				{
					enumFORMATETC = this.innerData.EnumFormatEtc(DATADIR.DATADIR_GET);
				}
				catch
				{
				}
				if (enumFORMATETC != null)
				{
					enumFORMATETC.Reset();
					FORMATETC[] array = new FORMATETC[1];
					int[] array2 = new int[] { 1 };
					while (array2[0] > 0)
					{
						array2[0] = 0;
						try
						{
							enumFORMATETC.Next(1, array, array2);
						}
						catch
						{
						}
						if (array2[0] > 0)
						{
							string name = DataFormats.GetFormat((int)array[0].cfFormat).Name;
							if (autoConvert)
							{
								string[] mappedFormats = DataObject.GetMappedFormats(name);
								for (int i = 0; i < mappedFormats.Length; i++)
								{
									arrayList.Add(mappedFormats[i]);
								}
							}
							else
							{
								arrayList.Add(name);
							}
						}
					}
				}
				string[] array3 = new string[arrayList.Count];
				arrayList.CopyTo(array3, 0);
				return DataObject.GetDistinctStrings(array3);
			}

			// Token: 0x060066DE RID: 26334 RVA: 0x00180D74 File Offset: 0x0017EF74
			public virtual string[] GetFormats()
			{
				return this.GetFormats(true);
			}

			// Token: 0x04003A7C RID: 14972
			internal IDataObject innerData;

			// Token: 0x020008B8 RID: 2232
			private class RestrictiveBinder : SerializationBinder
			{
				// Token: 0x06007299 RID: 29337 RVA: 0x001A2C14 File Offset: 0x001A0E14
				static RestrictiveBinder()
				{
					AssemblyName assemblyName = new AssemblyName(typeof(Bitmap).Assembly.FullName);
					if (assemblyName != null)
					{
						DataObject.OleConverter.RestrictiveBinder.s_allowedAssemblyName = assemblyName.Name;
						DataObject.OleConverter.RestrictiveBinder.s_allowedToken = assemblyName.GetPublicKeyToken();
					}
				}

				// Token: 0x0600729A RID: 29338 RVA: 0x001A2C68 File Offset: 0x001A0E68
				public override Type BindToType(string assemblyName, string typeName)
				{
					if (string.CompareOrdinal(typeName, DataObject.OleConverter.RestrictiveBinder.s_allowedTypeName) == 0)
					{
						AssemblyName assemblyName2 = null;
						try
						{
							assemblyName2 = new AssemblyName(assemblyName);
						}
						catch
						{
						}
						if (assemblyName2 != null && string.CompareOrdinal(assemblyName2.Name, DataObject.OleConverter.RestrictiveBinder.s_allowedAssemblyName) == 0)
						{
							byte[] publicKeyToken = assemblyName2.GetPublicKeyToken();
							if (publicKeyToken != null && DataObject.OleConverter.RestrictiveBinder.s_allowedToken != null && publicKeyToken.Length == DataObject.OleConverter.RestrictiveBinder.s_allowedToken.Length)
							{
								bool flag = false;
								for (int i = 0; i < DataObject.OleConverter.RestrictiveBinder.s_allowedToken.Length; i++)
								{
									if (DataObject.OleConverter.RestrictiveBinder.s_allowedToken[i] != publicKeyToken[i])
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									return null;
								}
							}
						}
					}
					throw new DataObject.OleConverter.RestrictedTypeDeserializationException();
				}

				// Token: 0x0400452B RID: 17707
				private static string s_allowedTypeName = typeof(Bitmap).FullName;

				// Token: 0x0400452C RID: 17708
				private static string s_allowedAssemblyName;

				// Token: 0x0400452D RID: 17709
				private static byte[] s_allowedToken;
			}

			// Token: 0x020008B9 RID: 2233
			private class RestrictedTypeDeserializationException : Exception
			{
			}
		}

		// Token: 0x0200067F RID: 1663
		private class DataStore : IDataObject
		{
			// Token: 0x060066E0 RID: 26336 RVA: 0x00180D98 File Offset: 0x0017EF98
			public virtual object GetData(string format, bool autoConvert)
			{
				DataObject.DataStore.DataStoreEntry dataStoreEntry = (DataObject.DataStore.DataStoreEntry)this.data[format];
				object obj = null;
				if (dataStoreEntry != null)
				{
					obj = dataStoreEntry.data;
				}
				object obj2 = obj;
				if (autoConvert && (dataStoreEntry == null || dataStoreEntry.autoConvert) && (obj == null || obj is MemoryStream))
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!format.Equals(mappedFormats[i]))
							{
								DataObject.DataStore.DataStoreEntry dataStoreEntry2 = (DataObject.DataStore.DataStoreEntry)this.data[mappedFormats[i]];
								if (dataStoreEntry2 != null)
								{
									obj = dataStoreEntry2.data;
								}
								if (obj != null && !(obj is MemoryStream))
								{
									obj2 = null;
									break;
								}
							}
						}
					}
				}
				if (obj2 != null)
				{
					return obj2;
				}
				return obj;
			}

			// Token: 0x060066E1 RID: 26337 RVA: 0x00180E3D File Offset: 0x0017F03D
			public virtual object GetData(string format)
			{
				return this.GetData(format, true);
			}

			// Token: 0x060066E2 RID: 26338 RVA: 0x00180E47 File Offset: 0x0017F047
			public virtual object GetData(Type format)
			{
				return this.GetData(format.FullName);
			}

			// Token: 0x060066E3 RID: 26339 RVA: 0x00180E58 File Offset: 0x0017F058
			public virtual void SetData(string format, bool autoConvert, object data)
			{
				if (data is Bitmap && format.Equals(DataFormats.Dib))
				{
					if (!autoConvert)
					{
						throw new NotSupportedException(SR.GetString("DataObjectDibNotSupported"));
					}
					format = DataFormats.Bitmap;
				}
				this.data[format] = new DataObject.DataStore.DataStoreEntry(data, autoConvert);
			}

			// Token: 0x060066E4 RID: 26340 RVA: 0x00180EA9 File Offset: 0x0017F0A9
			public virtual void SetData(string format, object data)
			{
				this.SetData(format, true, data);
			}

			// Token: 0x060066E5 RID: 26341 RVA: 0x00180EB4 File Offset: 0x0017F0B4
			public virtual void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x060066E6 RID: 26342 RVA: 0x00180EC3 File Offset: 0x0017F0C3
			public virtual void SetData(object data)
			{
				if (data is ISerializable && !this.data.ContainsKey(DataFormats.Serializable))
				{
					this.SetData(DataFormats.Serializable, data);
				}
				this.SetData(data.GetType(), data);
			}

			// Token: 0x060066E7 RID: 26343 RVA: 0x00180EF8 File Offset: 0x0017F0F8
			public virtual bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName);
			}

			// Token: 0x060066E8 RID: 26344 RVA: 0x00180F08 File Offset: 0x0017F108
			public virtual bool GetDataPresent(string format, bool autoConvert)
			{
				if (!autoConvert)
				{
					return this.data.ContainsKey(format);
				}
				string[] formats = this.GetFormats(autoConvert);
				for (int i = 0; i < formats.Length; i++)
				{
					if (format.Equals(formats[i]))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060066E9 RID: 26345 RVA: 0x00180F49 File Offset: 0x0017F149
			public virtual bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, true);
			}

			// Token: 0x060066EA RID: 26346 RVA: 0x00180F54 File Offset: 0x0017F154
			public virtual string[] GetFormats(bool autoConvert)
			{
				string[] array = new string[this.data.Keys.Count];
				this.data.Keys.CopyTo(array, 0);
				if (autoConvert)
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < array.Length; i++)
					{
						if (((DataObject.DataStore.DataStoreEntry)this.data[array[i]]).autoConvert)
						{
							string[] mappedFormats = DataObject.GetMappedFormats(array[i]);
							for (int j = 0; j < mappedFormats.Length; j++)
							{
								arrayList.Add(mappedFormats[j]);
							}
						}
						else
						{
							arrayList.Add(array[i]);
						}
					}
					string[] array2 = new string[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					array = DataObject.GetDistinctStrings(array2);
				}
				return array;
			}

			// Token: 0x060066EB RID: 26347 RVA: 0x0018100B File Offset: 0x0017F20B
			public virtual string[] GetFormats()
			{
				return this.GetFormats(true);
			}

			// Token: 0x04003A7D RID: 14973
			private Hashtable data = new Hashtable(BackCompatibleStringComparer.Default);

			// Token: 0x020008BA RID: 2234
			private class DataStoreEntry
			{
				// Token: 0x0600729D RID: 29341 RVA: 0x001A2D08 File Offset: 0x001A0F08
				public DataStoreEntry(object data, bool autoConvert)
				{
					this.data = data;
					this.autoConvert = autoConvert;
				}

				// Token: 0x0400452E RID: 17710
				public object data;

				// Token: 0x0400452F RID: 17711
				public bool autoConvert;
			}
		}
	}
}

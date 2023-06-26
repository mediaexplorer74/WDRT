using System;

namespace System.Net
{
	/// <summary>Container class for <see cref="T:System.Net.WebRequestMethods.Ftp" />, <see cref="T:System.Net.WebRequestMethods.File" />, and <see cref="T:System.Net.WebRequestMethods.Http" /> classes. This class cannot be inherited</summary>
	// Token: 0x020000EB RID: 235
	public static class WebRequestMethods
	{
		/// <summary>Represents the types of FTP protocol methods that can be used with an FTP request. This class cannot be inherited.</summary>
		// Token: 0x020006F7 RID: 1783
		public static class Ftp
		{
			/// <summary>Represents the FTP RETR protocol method that is used to download a file from an FTP server.</summary>
			// Token: 0x04003089 RID: 12425
			public const string DownloadFile = "RETR";

			/// <summary>Represents the FTP NLIST protocol method that gets a short listing of the files on an FTP server.</summary>
			// Token: 0x0400308A RID: 12426
			public const string ListDirectory = "NLST";

			/// <summary>Represents the FTP STOR protocol method that uploads a file to an FTP server.</summary>
			// Token: 0x0400308B RID: 12427
			public const string UploadFile = "STOR";

			/// <summary>Represents the FTP DELE protocol method that is used to delete a file on an FTP server.</summary>
			// Token: 0x0400308C RID: 12428
			public const string DeleteFile = "DELE";

			/// <summary>Represents the FTP APPE protocol method that is used to append a file to an existing file on an FTP server.</summary>
			// Token: 0x0400308D RID: 12429
			public const string AppendFile = "APPE";

			/// <summary>Represents the FTP SIZE protocol method that is used to retrieve the size of a file on an FTP server.</summary>
			// Token: 0x0400308E RID: 12430
			public const string GetFileSize = "SIZE";

			/// <summary>Represents the FTP STOU protocol that uploads a file with a unique name to an FTP server.</summary>
			// Token: 0x0400308F RID: 12431
			public const string UploadFileWithUniqueName = "STOU";

			/// <summary>Represents the FTP MKD protocol method creates a directory on an FTP server.</summary>
			// Token: 0x04003090 RID: 12432
			public const string MakeDirectory = "MKD";

			/// <summary>Represents the FTP RMD protocol method that removes a directory.</summary>
			// Token: 0x04003091 RID: 12433
			public const string RemoveDirectory = "RMD";

			/// <summary>Represents the FTP LIST protocol method that gets a detailed listing of the files on an FTP server.</summary>
			// Token: 0x04003092 RID: 12434
			public const string ListDirectoryDetails = "LIST";

			/// <summary>Represents the FTP MDTM protocol method that is used to retrieve the date-time stamp from a file on an FTP server.</summary>
			// Token: 0x04003093 RID: 12435
			public const string GetDateTimestamp = "MDTM";

			/// <summary>Represents the FTP PWD protocol method that prints the name of the current working directory.</summary>
			// Token: 0x04003094 RID: 12436
			public const string PrintWorkingDirectory = "PWD";

			/// <summary>Represents the FTP RENAME protocol method that renames a directory.</summary>
			// Token: 0x04003095 RID: 12437
			public const string Rename = "RENAME";
		}

		/// <summary>Represents the types of HTTP protocol methods that can be used with an HTTP request.</summary>
		// Token: 0x020006F8 RID: 1784
		public static class Http
		{
			/// <summary>Represents an HTTP GET protocol method.</summary>
			// Token: 0x04003096 RID: 12438
			public const string Get = "GET";

			/// <summary>Represents the HTTP CONNECT protocol method that is used with a proxy that can dynamically switch to tunneling, as in the case of SSL tunneling.</summary>
			// Token: 0x04003097 RID: 12439
			public const string Connect = "CONNECT";

			/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
			// Token: 0x04003098 RID: 12440
			public const string Head = "HEAD";

			/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
			// Token: 0x04003099 RID: 12441
			public const string Put = "PUT";

			/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
			// Token: 0x0400309A RID: 12442
			public const string Post = "POST";

			/// <summary>Represents an HTTP MKCOL request that creates a new collection (such as a collection of pages) at the location specified by the request-Uniform Resource Identifier (URI).</summary>
			// Token: 0x0400309B RID: 12443
			public const string MkCol = "MKCOL";
		}

		/// <summary>Represents the types of file protocol methods that can be used with a FILE request. This class cannot be inherited.</summary>
		// Token: 0x020006F9 RID: 1785
		public static class File
		{
			/// <summary>Represents the FILE GET protocol method that is used to retrieve a file from a specified location.</summary>
			// Token: 0x0400309C RID: 12444
			public const string DownloadFile = "GET";

			/// <summary>Represents the FILE PUT protocol method that is used to copy a file to a specified location.</summary>
			// Token: 0x0400309D RID: 12445
			public const string UploadFile = "PUT";
		}
	}
}

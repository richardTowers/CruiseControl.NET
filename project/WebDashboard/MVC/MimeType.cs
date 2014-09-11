namespace ThoughtWorks.CruiseControl.WebDashboard.MVC
{
    using System.Collections.Generic;

    internal class MimeType
	{
		public static readonly MimeType Jpg = new MimeType("image/jpeg", "jpg", "jpe");
		public static readonly MimeType Png = new MimeType("image/png", "png");
		public static readonly MimeType Xml = new MimeType("text/xml", "xml");
        public static readonly MimeType Json = new MimeType("application/json", "json");
        public static readonly MimeType Html = new MimeType("text/html", "html", "htm");
        public static readonly MimeType Xhtml = new MimeType("application/xhtml+xml", "xhtml");
        public static readonly MimeType EventStream = new MimeType("text/event-stream");

		private List<string> mimeExtension;
		private string mimeType;

		public MimeType(string mimeType, params string[] extensions)
		{
			mimeExtension = new List<string>();
			mimeExtension.AddRange(extensions);
			this.mimeType = mimeType;
		}

		public bool HasExtension(string extension)
		{
			return mimeExtension.Contains(extension);
		}

		public string ContentType
		{
			get { return mimeType; }
		}
	}
}
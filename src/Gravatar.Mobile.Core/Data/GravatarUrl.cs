using System;
using System.Xml.Serialization;

namespace GravatarMobile.Core.Data
{
	/// <summary>
	/// Gravatar URL.
	/// </summary>
	public class GravatarUrl
	{
		[XmlElement(ElementName="title")]
		public string Title {get; set;}

		[XmlElement(ElementName="value")]
		public string Value {get; set;}
	}
}


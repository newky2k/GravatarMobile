using System;
using System.Xml.Serialization;

namespace GravatarMobile
{
	/// <summary>
	/// Gravatar background settings
	/// </summary>
	public class GravatarBackground
	{
		[XmlElement(ElementName="color")]
		public string Color {get; set;}

		[XmlElement(ElementName="url")]
		public string Url {get; set;}
	}
}


using System;
using System.Xml.Serialization;

namespace GravatarMobile.Core.Data
{
	public enum ImageType 
	{
		[XmlEnum("standard")]
		Standard, 
		[XmlEnum("thumbnail")]
		Thumbnail,
	};

	public class GravatarImage
	{
		[XmlElement(ElementName="value")]
		public string Value {get; set;}

		[XmlElement(ElementName="type")]
		public ImageType Type {get; set;}

	}
}


using System;
using System.Xml.Serialization;

namespace GravatarMobile.Core.Data
{
	public class GravatarAccount
	{
		[XmlElement(ElementName="domain")]
		public string Domain {get; set;}

		[XmlElement(ElementName="userid")]
		public string UserId {get; set;}

		[XmlElement(ElementName="display")]
		public string Display {get; set;}

		[XmlElement(ElementName="url")]
		public string Url {get; set;}

		[XmlElement(ElementName="verified")]
		public bool Verified {get; set;}

		[XmlElement(ElementName="shortname")]
		public string ShortName {get; set;}

	}
}


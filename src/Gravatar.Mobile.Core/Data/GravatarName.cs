using System;
using System.Xml.Serialization;

namespace GravatarMobile.Core.Data
{
	/// <summary>
	/// Gravatar name data
	/// </summary>
	public class GravatarName
	{
		[XmlElement(ElementName="givenName")]
		public string GivenName {get; set;}

		[XmlElement(ElementName="familyName")]
		public string FamilyName {get; set;}

		[XmlElement(ElementName="formatted")]
		public string Formatted {get; set;}

	}
}


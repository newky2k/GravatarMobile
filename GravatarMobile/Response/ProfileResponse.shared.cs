using System;
using System.Xml.Serialization;

namespace GravatarMobile
{
	[XmlRoot(ElementName="response")]
	public class ProfileResponse
	{
		[XmlElement(ElementName="entry")]
		public GravatarProfile Profile {get; set;}

		public ProfileResponse()
		{
		}
	}
}


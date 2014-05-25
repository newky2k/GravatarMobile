using System;
using System.Xml.Serialization;

namespace GravatarMobile.Core.Data
{
	/// <summary>
	/// Gravatar email contact
	/// </summary>
	public class GravatarEmail
	{
		[XmlElement(ElementName="value")]
		public string Value {get; set;}

		[XmlElement(ElementName="primary")]
		public bool IsPrimary {get; set;}
	}
}


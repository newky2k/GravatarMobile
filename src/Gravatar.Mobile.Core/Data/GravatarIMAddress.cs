using System;
using System.Xml.Serialization;

namespace GravatarMobile.Core.Data
{
	public enum IMType 
	{
		None,
		[XmlEnum("icq")]
		ICQ, 
		[XmlEnum("aim")]
		AIM,
		[XmlEnum("yahoo")]
		Yahoo,
		[XmlEnum("gtalk")]
		GTalk,
		[XmlEnum("skype")]
		Skype,
	};

	/// <summary>
	/// Gravatar IM address.
	/// </summary>
	public class GravatarIMAddress
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		[XmlElement(ElementName="type")]
		public IMType Type  {get; set;}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		[XmlElement(ElementName="value")]
		public string Value {get; set;}
	}
}


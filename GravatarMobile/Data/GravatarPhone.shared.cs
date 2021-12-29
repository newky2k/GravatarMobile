using System;
using System.Xml.Serialization;

namespace GravatarMobile
{
	public enum PhoneType 
	{
		None,
		[XmlEnum("home")]
		Home, 
		[XmlEnum("work")]
		Work,
		[XmlEnum("mobile")]
		Mobile,
	};

	/// <summary>
	/// Gravatar phone number
	/// </summary>
	public class GravatarPhone
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		[XmlElement(ElementName="type")]
		public PhoneType Type  {get; set;}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		[XmlElement(ElementName="value")]
		public string Value {get; set;}
	}
}


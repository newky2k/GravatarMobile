using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using GravatarMobile.Core.Data;

namespace GravatarMobile.Core
{
	/// <summary>
	/// Gravatar profile class
	/// </summary>
	public class GravatarProfile
	{
		#region Properties
		/// <summary>
		/// Gets or sets the Id
		/// </summary>
		/// <value>The I.</value>
		[XmlElement(ElementName="id")]
		public String ID {get; set;}

		/// <summary>
		/// Gets or sets a value indicating whether this instance hash.
		/// </summary>
		/// <value><c>true</c> if this instance hash; otherwise, <c>false</c>.</value>
		[XmlElement(ElementName="hash")]
		public String Hash {get; set;}

		/// <summary>
		/// Gets or sets the profile URL.
		/// </summary>
		/// <value>The profile URL.</value>
		[XmlElement(ElementName="profileUrl")]
		public String ProfileUrl {get; set;}

		/// <summary>
		/// Gets or sets the preferred username.
		/// </summary>
		/// <value>The preferred username.</value>
		[XmlElement(ElementName="preferredUsername")]
		public String PreferredUsername {get; set;}

		/// <summary>
		/// Gets or sets the thumbnail URL.
		/// </summary>
		/// <value>The thumbnail URL.</value>
		[XmlElement(ElementName="thumbnailUrl")]
		public String ThumbnailUrl {get; set;}

		/// <summary>
		/// Gets or sets the photos.
		/// </summary>
		/// <value>The photos.</value>
		[XmlElement(ElementName="photos")]
		public List<GravatarImage> Photos {get; set;}

		/// <summary>
		/// Gets or sets the name information
		/// </summary>
		/// <value>The name.</value>
		[XmlElement(ElementName="name")]
		public GravatarName Name {get; set;}

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>The display name.</value>
		[XmlElement(ElementName="displayName")]
		public String DisplayName {get; set;}

		/// <summary>
		/// Gets or sets the about me.
		/// </summary>
		/// <value>The about me.</value>
		[XmlElement(ElementName="aboutMe")]
		public String AboutMe {get; set;}

		/// <summary>
		/// Gets or sets the current location.
		/// </summary>
		/// <value>The current location.</value>
		[XmlElement(ElementName="currentLocation")]
		public String CurrentLocation {get; set;}

		/// <summary>
		/// Gets or sets the urls.
		/// </summary>
		/// <value>The urls.</value>
		[XmlElement(ElementName="urls")]
		public List<GravatarUrl> Urls {get; set;}

		/// <summary>
		/// Gets or sets the background.
		/// </summary>
		/// <value>The background.</value>
		[XmlElement(ElementName="profileBackground")]
		public GravatarBackground Background {get; set;}

		/// <summary>
		/// Gets or sets the email addresses.
		/// </summary>
		/// <value>The email addresses.</value>
		[XmlElement(ElementName="emails")]
		public List<GravatarEmail> EmailAddresses {get; set;}

		/// <summary>
		/// Gets or sets the Instant Messaging addresses.
		/// </summary>
		/// <value>The IM addresses.</value>
		[XmlElement(ElementName="ims")]
		public List<GravatarIMAddress> IMAddresses {get; set;}

		/// <summary>
		/// Gets or sets the phone numbers.
		/// </summary>
		/// <value>The phone numbers.</value>
		[XmlElement(ElementName="phoneNumbers")]
		public List<GravatarPhone> PhoneNumbers {get; set;}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarMobile.Core.GravatarProfile"/> class.
		/// </summary>
		public GravatarProfile()
		{
			
		}
		#endregion

	}
}


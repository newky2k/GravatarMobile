using System;
using GravatarMobile.Core.Security;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace GravatarMobile.Core
{
    /// <summary>
    /// Gravatar object class
    /// </summary>
    public class Gravatar
    {
        #region fields

        private String mEmail;
        private Byte[] mResult;
		private GravatarProfile mProfile;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public String Email
        {
            get
            {
                if (String.IsNullOrWhiteSpace(mEmail))
                    throw new Exception("Email has not been set");

                return mEmail;
            }

        }

        /// <summary>
        /// Gets a value indicating Gravatar Hash value
        /// </summary>
        /// <value><c>true</c> if this instance hash; otherwise, <c>false</c>.</value>
        public String Hash
        {
            get
            {

                var gravEmail = Email.ToLower().Trim();

                return MD5.GetHashString(gravEmail).ToLower();

            }
        }

        /// <summary>
        /// Image
        /// </summary>
        /// <value>The image.</value>
        public Byte[] Image
        {
            get
            {
                if (mResult == null)
                {
                    throw new Exception("Image not set try LoadImage");
                }

                return mResult;
            }
        }

        /// <summary>
        /// Gets the image as a stream
        /// </summary>
        /// <value>The image.</value>
        public MemoryStream ImageAsStream
        {
            get
            {
                return new MemoryStream(Image);
            }
        }

		/// <summary>
		/// Gets the profile.
		/// </summary>
		/// <value>The profile.</value>
		public GravatarProfile Profile
		{
			get
			{
				if (mProfile == null)
				{
					throw new Exception("Profile not set try LoadProfile");
				}
				return mProfile;
			}
		}
        #endregion

        #region Methods
		/// <summary>
		/// Setup this instance.
		/// </summary>
		private async void Setup()
		{
			await LoadImageAsync();
			await LoadProfileAsync();
		}

        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <returns>The image.</returns>
        public async Task LoadImageAsync()
        {
            mResult = await GravatarControl.GetImage(Hash, 80);
        }

        /// <summary>
        /// Loads the image and specify thee
        /// </summary>
        /// <returns>The image.</returns>
        /// <param name="Size">Size.</param>
        public async Task LoadImageAsync(int Size)
        {
            mResult = await GravatarControl.GetImage(Hash, Size);
        }

		/// <summary>
		/// Loads the profile.
		/// </summary>
		/// <returns>The profile.</returns>
		public async Task LoadProfileAsync()
		{
			mProfile = await GravatarControl.GetProfile(Hash);
		}
        #endregion

        #region Constuctor

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.Core.Gravatar"/> class.
        /// </summary>
        /// <param name="Email">Email.</param>
		public Gravatar(String Email)
        {
			if (String.IsNullOrWhiteSpace(Email))
				throw new Exception("Email has not been provided");
            mEmail = Email;

			Setup();
        }

        #endregion
    }
}


using System;
using GravatarMobile.Core.Security;

namespace GravatarMobile.Core
{
    /// <summary>
    /// Gravatar object class
    /// </summary>
    public class Gravatar
    {
        #region fields

        private String mEmail;

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

        #endregion

        #region Constuctor

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.Core.Gravatar"/> class.
        /// </summary>
        /// <param name="Email">Email.</param>
        public Gravatar(String Email)
        {
            mEmail = Email;
        }

        #endregion
    }
}


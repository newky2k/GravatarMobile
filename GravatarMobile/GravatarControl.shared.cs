using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Xml.Serialization;
using System.Text;

namespace GravatarMobile
{
    /// <summary>
    /// Central class for Gravatar functions
    /// </summary>
    public static class GravatarControl
    {
        #region Constants

        private const string kAvatarUrl = "https://www.gravatar.com/avatar/";
        private const string kProfileUrl = "https://en.gravatar.com/";

        private static readonly HttpClient sClient = new HttpClient();

        #endregion

        #region Methods

        private static string BuildQueryString(int size)
        {
            return string.Format("?s={0}", size);
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>The image.</returns>
        /// <param name="Hash">Hash.</param>
        /// <param name="Size">Size.</param>
        public static async Task<Byte[]> GetImage(String Hash, int Size)
        {
            var url = string.Format("{0}{1}{2}", kAvatarUrl, Hash, BuildQueryString(Size));

            using (var stream = await sClient.GetStreamAsync(url).ConfigureAwait(false))
            using (var output = new MemoryStream())
            {
                await stream.CopyToAsync(output).ConfigureAwait(false);

                return output.ToArray();
            }
        }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <returns>The profile, or null if there is no profile for the hash.</returns>
        /// <param name="Hash">Hash.</param>
        public static async Task<GravatarProfile> GetProfile(String Hash)
        {
            var url = string.Format("{0}{1}.xml", kProfileUrl, Hash);

            using (var response = await sClient.GetAsync(url).ConfigureAwait(false))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(content))
                    return null;

                return ParseProfile(content);
            }
        }

        /// <summary>
        /// Parses the XML profile response returned by Gravatar.
        /// </summary>
        /// <param name="xml">The XML text.</param>
        /// <returns>The profile.</returns>
        internal static GravatarProfile ParseProfile(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(ProfileResponse));
                var output = (ProfileResponse)serializer.Deserialize(reader);

                return output.Profile;
            }
        }

        #endregion
    }
}


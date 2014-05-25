using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Xml.Serialization;
using GravatarMobile.Core.Response;

namespace GravatarMobile.Core
{
    /// <summary>
    /// Central class for Gravatar functions
    /// </summary>
    public static class GravatarControl
    {
        #region Constants

        private const string kAvatarUrl = @"http://www.gravatar.com/avatar/";
		private const string kProfileUrl = @"http://en.gravatar.com/";

        #endregion

        #region Methods

        private static string BuildQueryString(int size)
        {
            var qs = String.Empty;

            qs += String.Format("?s={0}", size.ToString());

            return qs;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>The image.</returns>
        /// <param name="Hash">Hash.</param>
        /// <param name="Size">Size.</param>
        public async static Task<Byte[]> GetImage(String Hash, int Size)
        {
            var hClient = new  HttpClient();
            var qs = BuildQueryString(Size);

            var aURl = String.Format("{0}{1}{2}", kAvatarUrl, Hash, qs);
            var result = await hClient.GetStreamAsync(aURl);

            var byteArray = new Byte[result.Length];
            result.Read(byteArray, 0, (int)result.Length);
		
            return byteArray;
       
        }

		/// <summary>
		/// Gets the profile.
		/// </summary>
		/// <returns>The profile.</returns>
		/// <param name="Hash">Hash.</param>
		public async static Task<GravatarProfile> GetProfile(String Hash)
		{
			var hClient = new  HttpClient();
			var aURl = String.Format("{0}{1}.xml", kProfileUrl, Hash);
			var result = await hClient.GetStreamAsync(aURl);

			if (result == null) 
				return null;

			var xml = new XmlSerializer(typeof(ProfileResponse));
			var outPut = (ProfileResponse)xml.Deserialize(result);

			var aProfile = outPut.Profile;

			return aProfile;
		}
        //        public async static Task<TData> DownloadJsonTheOldWay<TData>(string url)
        //        {
        //            return await Task.Run(() =>
        //            {
        //                var req = WebRequest.CreateHttp(url);
        //                req.Accept = "application/json";
        //                TData result = default(TData);
        //                var waiter = new ManualResetEvent(false);
        //                req.BeginGetResponse(cb =>
        //                {
        //                    var cbreq = cb.AsyncState as WebRequest;
        //                    var resp = cbreq.EndGetResponse(cb);
        //                    var serializer = new DataContractJsonSerializer(typeof(TData));
        //                    using (var strm = resp.GetResponseStream())
        //                    {
        //                        result = (TData)serializer.ReadObject(strm);
        //                    }
        //                    waiter.Set();
        //                }, req);
        //                waiter.WaitOne();
        //                return result;
        //            });
        //        }

        #endregion
    }
}


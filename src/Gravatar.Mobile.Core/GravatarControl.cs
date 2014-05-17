using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Json;

namespace GravatarMobile.Core
{
    /// <summary>
    /// Central class for Gravatar functions
    /// </summary>
    public static class GravatarControl
    {
        #region Constants

        private const string kAvatarUrl = @"http://www.gravatar.com/avatar/";

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
            var qs = BuildQueryString(Size);

            var aURl = String.Format("{0}{1}{2}", kAvatarUrl, Hash, qs);
            var item = await Task.Run<Byte[]>(() =>
            {
                var req = WebRequest.CreateHttp(aURl);
                //breq.Accept = "application/json";
                Byte[] result = default( Byte[]);
                var waiter = new ManualResetEvent(false);
                req.BeginGetResponse(cb =>
                {
                    var cbreq = cb.AsyncState as WebRequest;
                    var resp = cbreq.EndGetResponse(cb);
                    using (var strm = resp.GetResponseStream())
                    {
                        var byteArray = new Byte[strm.Length];
                        strm.Read(byteArray, 0, (int)strm.Length);

                        result = byteArray;
                    }
                    waiter.Set();
                }, req);
                waiter.WaitOne();

                req = null;

                return result;
            });
                
            return item;

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


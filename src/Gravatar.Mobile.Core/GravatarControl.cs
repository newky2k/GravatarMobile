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
    public class GravatarControl
    {
        #region Constants

        private const string kAvatarUrl = @"http://www.gravatar.com/avatar/";

        #endregion

        public GravatarControl()
        {

        }

        #region Methods

        public async static Task<Byte[]> GetImage(String Hash)
        {
            var aURl = String.Format("{0}{1}", kAvatarUrl, Hash);
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


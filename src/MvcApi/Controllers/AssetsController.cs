using log4net;
using MvcApi.Business.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web.Http;

namespace MvcApi.Controllers
{
    [RoutePrefix("assets")]
    public class AssetsController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        //GET ~/assets/c898fdea-4c05-411b-85d5-dcea10bf3387/content
        [Route("{assetUid}/content")]
        [HttpGet]
        public HttpResponseMessage GetAttachment(string assetUid)
        {
            logger.Info($"{nameof(GetAttachment)} called.");
            logger.Info($"assetUid: {assetUid}");
            Debug.WriteLine($"assetUid: {assetUid}");

            HttpResponseMessage result = null;
            IDataReader objRdr = null;
            SqlConnection objDB = null;
            MemoryStream memoryStream = null;
            StreamWriter streamWriter = null;
            byte[] bytesInUni = null;

            string strFileName = string.Empty;
            string strRet = string.Empty;
            string strCon = string.Empty;
            string strSP = string.Empty;
            try
            {
                strCon = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                strSP = "GetAttachByAssetUid";
                objDB = new SqlConnection(strCon);

                objDB.Open();
                var cmd = objDB.CreateCommand();
                cmd.CommandText = strSP;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@assetUid", assetUid));

                objRdr = cmd.ExecuteReader();
                while (objRdr.Read())
                {
                    if (objRdr["vcFileName"] != DBNull.Value)
                    {
                        strFileName = objRdr["vcFileName"].ToString();
                    }
                    if (objRdr["imAsset"] != DBNull.Value)
                    {
                        bytesInUni = objRdr["imAsset"] as byte[];
                        if (bytesInUni != null && bytesInUni.Length > 0)
                        {
                            strRet = Encoding.UTF8.GetString(bytesInUni);
                        }
                    }
                }

                //memoryStream = new MemoryStream();
                //streamWriter = new StreamWriter(memoryStream);

                //streamWriter.WriteLine(strRet);

                //streamWriter.Flush();
                //memoryStream.Seek(0, SeekOrigin.Begin);

                //result = new HttpResponseMessage(HttpStatusCode.OK)
                //{
                //    Content = new ByteArrayContent(memoryStream.ToArray())
                //};

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytesInUni)
                };

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = strFileName
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{strSP}: {ex}");
            }
            finally
            {
                if (objRdr != null)
                {
                    objRdr.Close();
                    objRdr.Dispose();
                }

                if (objDB != null)
                {
                    objDB.Close();
                    objDB.Dispose();
                }

                if (streamWriter != null)
                {
                    streamWriter.Dispose();
                }

                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }

            return result;
        }

        //GET ~/assets/17181/content?UserLoginLog=d68716b5-793e-4ba6-88aa-fdc383f04d35
        //    =>
        //GET ~/assets/17181/content/d68716b5-793e-4ba6-88aa-fdc383f04d35
        [Route("{iAssetId:long}/content/{userLoginLog}")]
        [HttpGet]
        public HttpResponseMessage GetAttachment(long iAssetId, string userLoginLog)
        {
            logger.Info($"{nameof(GetAttachment)} called.");
            logger.Info($"{nameof(iAssetId)}: {iAssetId}, {nameof(userLoginLog)}: {userLoginLog}");
            Debug.WriteLine($"{nameof(iAssetId)}: {iAssetId}, {nameof(userLoginLog)}: {userLoginLog}");

            HttpResponseMessage result = null;
            IDataReader objRdr = null;
            SqlConnection objDB = null;
            MemoryStream memoryStream = null;
            StreamWriter streamWriter = null;
            byte[] bytesInUni = null;

            string strFileName = string.Empty;
            string strRet = string.Empty;
            string strCon = string.Empty;
            string strSP = string.Empty;
            try
            {
                strCon = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                strSP = "GetAttachmentByAssetId";
                objDB = new SqlConnection(strCon);

                objDB.Open();
                var cmd = objDB.CreateCommand();
                cmd.CommandText = strSP;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@iAssetID", iAssetId));
                cmd.Parameters.Add(new SqlParameter("@userLoginLog", userLoginLog));

                objRdr = cmd.ExecuteReader();
                while (objRdr.Read())
                {
                    if (objRdr["vcFileName"] != DBNull.Value)
                    {
                        strFileName = objRdr["vcFileName"].ToString();
                    }
                    if (objRdr["imAsset"] != DBNull.Value)
                    {
                        bytesInUni = objRdr["imAsset"] as byte[];
                        if (bytesInUni != null && bytesInUni.Length > 0)
                        {
                            strRet = Encoding.UTF8.GetString(bytesInUni);
                        }
                    }
                }

                //memoryStream = new MemoryStream();
                //streamWriter = new StreamWriter(memoryStream);

                //streamWriter.WriteLine(strRet);

                //streamWriter.Flush();
                //memoryStream.Seek(0, SeekOrigin.Begin);

                //result = new HttpResponseMessage(HttpStatusCode.OK)
                //{
                //    Content = new ByteArrayContent(memoryStream.ToArray())
                //};

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytesInUni)
                };

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = strFileName
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{strSP}: {ex}");
            }
            finally
            {
                if (objRdr != null)
                {
                    objRdr.Close();
                    objRdr.Dispose();
                }

                if (objDB != null)
                {
                    objDB.Close();
                    objDB.Dispose();
                }

                if (streamWriter != null)
                {
                    streamWriter.Dispose();
                }

                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }

            return result;
        }

        ////GET ~/assets/c898fdea-4c05-411b-85d5-dcea10bf3387/asset
        //[Route("{assetUid}/asset")]
        //[HttpGet]
        //public HttpResponseMessage GetAsset(string assetUid)
        //{
        //    HttpResponseMessage result = null;
        //    byte[] fileContent = null;
        //    string fileName = string.Empty;

        //    (fileName, fileContent) = _assetService.GetAttachment(assetUid);

        //    result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(fileContent)
        //    };

        //    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = fileName
        //    };

        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //    return result;
        //}

        ////GET ~/assets/GetAsset/?intAssetID=17194
        ////GET ~/assets/GetAsset/17194
        //[Route("GetAsset/{intAssetID}")]
        //[HttpGet]
        //public IHttpActionResult GetAssetUidByAssetId(long intAssetID)
        //{
        //    logger.Info($"{nameof(GetAttachment)} called.");
        //    logger.Info($"intAssetID: {intAssetID}");
        //    Debug.WriteLine($"intAssetID: {intAssetID}");

        //    string strRet = string.Empty;
        //    string strCon = string.Empty;
        //    string strSP = string.Empty;
        //    IDataReader objRdr = null;
        //    SqlConnection objDB = null;

        //    try
        //    {
        //        strCon = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        //        strSP = "GetAssetUidByAssetID";
        //        objDB = new SqlConnection(strCon);

        //        objDB.Open();
        //        var cmd = objDB.CreateCommand();
        //        cmd.CommandText = strSP;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.Add(new SqlParameter("@intAssetID", intAssetID));

        //        objRdr = cmd.ExecuteReader();
        //        while (objRdr.Read())
        //        {
        //            if (objRdr["AssetUid"] != DBNull.Value)
        //            {
        //                strRet = objRdr["AssetUid"].ToString();
        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"{strSP}: {ex}");
        //    }
        //    finally
        //    {
        //        if (objRdr != null)
        //        {
        //            objRdr.Close();
        //            objRdr.Dispose();
        //        }

        //        if (objDB != null)
        //        {
        //            objDB.Close();
        //            objDB.Dispose();
        //        }
        //    }

        //    return Ok(strRet);
        //}

        ////POST ~/assets/c898fdea-4c05-411b-85d5-dcea10bf3387/save
        //[Route("{assetUid}/save")]
        //[HttpPost]
        //public IHttpActionResult SaveAttachment(string assetUid)
        //{
        //    bool blnRet = false;
        //    string query = "UPDATE [dbo].[attachment] SET [imAsset] = @imAsset WHERE [AssetUid] = @assetUid";

        //    try
        //    {
        //        byte[] bytesInUni = Encoding.UTF8.GetBytes(assetUid);
        //        if (bytesInUni != null && bytesInUni.Length > 0)
        //        {
        //            string strCon = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        //            using (SqlConnection conn = new SqlConnection(strCon))
        //            {
        //                conn.Open();

        //                using (SqlCommand cmd = new SqlCommand(query))
        //                {
        //                    cmd.Connection = conn;
        //                    cmd.Parameters.AddWithValue("@assetUid", assetUid);
        //                    cmd.Parameters.AddWithValue("@imAsset", bytesInUni);
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        blnRet = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        blnRet = false;
        //        //throw;
        //    }
        //    return Ok(blnRet);
        //}
    }
}
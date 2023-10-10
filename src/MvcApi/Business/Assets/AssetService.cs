using MvcApi.Business.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MvcApi.Business.Assets
{
    public class AssetService : IAssetService
    {
        public string GetAssetUidByAssetId(long intAssetID)
        {
            throw new NotImplementedException();
        }

        public (string, byte[]) GetAttachment(string assetUid)
        {
            IDataReader objRdr = null;
            SqlConnection objDB = null;
            byte[] bytesInUni = null;

            string strFileName = string.Empty;
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
                    }
                }
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
            }

            return (strFileName, bytesInUni);
        }

        public bool SaveAttachment(string assetUid)
        {
            throw new NotImplementedException();
        }
    }
}
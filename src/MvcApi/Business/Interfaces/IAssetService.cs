namespace MvcApi.Business.Interfaces
{
    public interface IAssetService
    {
        string GetAssetUidByAssetId(long intAssetID);

        bool SaveAttachment(string assetUid);

        (string, byte[]) GetAttachment(string assetUid);
    }
}
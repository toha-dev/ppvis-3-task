namespace Core.Storage.Contracts
{
    public interface IPharmacyStorageProvider
    {
        bool IsInStock(string medicamentId);
    }
}

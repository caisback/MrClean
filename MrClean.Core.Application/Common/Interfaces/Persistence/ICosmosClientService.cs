namespace MrClean.Core.Application.Common.Interfaces.Persistence
{
    public interface ICosmosClientService
    {
        Task<T> CreateItemAsync<T>(string collectionName, T item, string partitionKeyPath, string partitionKeyValue);
        Task<T> DeleteItemAsync<T>(string collectionName, string id, string partitionKeyPath, string partitionKeyValue);
        Task<T> GetItemAsync<T>(string collectionName, string id, string partitionKeyPath = null!, string partitionKeyValue = null!);
        Task<List<T>> GetItemsAsync<T>(string collectionName, string partitionKeyPath = null!, string partitionKeyValue = null!, string sqlQueryText = null!, params KeyValuePair<string, object>[] queryParams);
        Task<T> UpdateItemAsync<T>(string collectionName, T item, string id, string partitionKeyPath = null!, string partitionKeyValue = null!);
    }
}

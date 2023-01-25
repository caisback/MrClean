using Microsoft.Azure.Cosmos;
using MrClean.Core.Application.Common.Interfaces.Persistence;

namespace MrClean.Infrastructure.Persistence
{
    public class CosmosClientService : ICosmosClientService, IDisposable
    {
        //private const int AutoscaleMaxThroughput = 4000;
        private readonly CosmosClient _cosmosClient;

        private const string DatabaseName = "fixturesdb";

        public CosmosClientService(string connectionString)
        {
            _cosmosClient = new CosmosClient(connectionString);
        }

        public async Task<T> DeleteItemAsync<T>(string collectionName,
            string id,
            string partitionKeyPath,
            string partitionKeyValue)
        {
            var container = await GetContainerAsync(DatabaseName, collectionName, partitionKeyPath);
            return await container.DeleteItemAsync<T>(id, new PartitionKey(partitionKeyValue));
        }

        public async Task<T> UpdateItemAsync<T>(string collectionName,
            T item, string id,
            string partitionKeyPath = null!,
            string partitionKeyValue = null!)
        {
            var container = await GetContainerAsync(DatabaseName, collectionName, partitionKeyPath ?? "/id");
            return await container.ReplaceItemAsync(item, id, new PartitionKey(partitionKeyValue ?? id));
        }

        public async Task<T> GetItemAsync<T>(string collectionName,
            string id,
            string partitionKeyPath = null!,
            string partitionKeyValue = null!)
        {
            var container = await GetContainerAsync(DatabaseName, collectionName, partitionKeyPath ?? "/id");
            return await container.ReadItemAsync<T>(id, new PartitionKey(partitionKeyValue ?? id));
        }

        public async Task<T> CreateItemAsync<T>(string collectionName,
            T item,
            string partitionKeyPath,
            string partitionKeyValue)
        {
            var container = await GetContainerAsync(DatabaseName, collectionName, partitionKeyPath);
            return await container.CreateItemAsync(item, new PartitionKey(partitionKeyValue));
        }

        public async Task<List<T>> GetItemsAsync<T>(string collectionName,
            string partitionKeyPath = null!,
            string partitionKeyValue = null!,
            string sqlQueryText = null!,
            params KeyValuePair<string, object>[] queryParams)
        {
            var container = await GetContainerAsync(DatabaseName, collectionName, partitionKeyPath);

            QueryDefinition query = new QueryDefinition(sqlQueryText);
            if (queryParams != null && queryParams.Any())
            {
                queryParams.ToList().ForEach(p =>
                {
                    query = query.WithParameter(p.Key, p.Value);
                });
            }

            var results = new List<T>();
            using (FeedIterator<T> resultSetIterator = container.GetItemQueryIterator<T>(
                query,
                requestOptions: string.IsNullOrEmpty(partitionKeyValue) ? null : new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(partitionKeyValue)
                }))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<T> response = await resultSetIterator.ReadNextAsync();
                    results.AddRange(response);
                    if (response.Diagnostics != null)
                    {
                        //log.LogInformation($"\nQueryWithSqlParameters Diagnostics: {response.Diagnostics.ToString()}");
                    }
                }
            }

            return results;
        }

        private async Task<Container> GetContainerAsync(string databaseName, string collectionName, string partitionKeyPath = null!)
        {
            var dbResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName, ThroughputProperties.CreateAutoscaleThroughput(4000));
            ContainerProperties containerProperties = new ContainerProperties()
            {
                Id = collectionName,
                PartitionKeyPath = partitionKeyPath ?? "/id"
            };
            var containerResponse = await dbResponse.Database.CreateContainerIfNotExistsAsync(containerProperties);

            return containerResponse.Container;
        }

        public void Dispose()
        {
            if (_cosmosClient != null)
                _cosmosClient.Dispose();
        }
    }
}


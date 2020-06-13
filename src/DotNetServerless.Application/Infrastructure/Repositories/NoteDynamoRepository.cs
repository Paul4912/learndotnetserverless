using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Configs;

namespace DotNetServerless.Application.Infrastructure.Repositories
{
    public class NoteDynamoRepository : INoteRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBOperationConfig _configuration;

        public NoteDynamoRepository(DynamoDbConfiguration configuration,
          IAwsClientFactory<AmazonDynamoDBClient> clientFactory)
        {
            _client = clientFactory.GetAwsClient();
            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = "dev-notes"/*configuration.TableName*/,
                SkipVersionCheck = true
            };
        }

        public async Task Save(Note item, CancellationToken cancellationToken)
        {
            using (var context = new DynamoDBContext(_client))
            {
                await context.SaveAsync(item, _configuration, cancellationToken);
            }
        }

        public async Task<IEnumerable<T>> GetById<T>(string id, CancellationToken cancellationToken)
        {
            var resultList = new List<T>();
            using (var context = new DynamoDBContext(_client))
            {
                var scanCondition = new ScanCondition(nameof(Note.noteId), ScanOperator.Equal, id);
                var search = context.ScanAsync<T>(new[] { scanCondition }, _configuration);

                while (!search.IsDone)
                {
                    var entities = await search.GetNextSetAsync(cancellationToken);
                    resultList.AddRange(entities);
                }
            }

            return resultList;
        }

        public async Task<IEnumerable<T>> ListByUserId<T>(string id, CancellationToken cancellationToken)
        {
            var resultList = new List<T>();
            using (var context = new DynamoDBContext(_client))
            {
                var scanCondition = new ScanCondition(nameof(Note.userId), ScanOperator.Equal, id);
                var search = context.ScanAsync<T>(new[] { scanCondition }, _configuration);

                while (!search.IsDone)
                {
                    var entities = await search.GetNextSetAsync(cancellationToken);
                    resultList.AddRange(entities);
                }
            }

            return resultList;
        }
    }
}

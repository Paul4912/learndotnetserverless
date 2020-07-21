using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Configs;
using DotNetServerless.Application.Requests;
using DotNetServerless.Application.Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
                OverrideTableName = $"{Environment.GetEnvironmentVariable("stage")}-notes",
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

        public async Task<DeleteNoteResponse> DeleteNote<T>(DeleteNoteRequest request, CancellationToken cancellationToken)
        {
            //TODO: Check if item exists if not return back error msg.
            AttributeValue userId = new AttributeValue
            {
                S = request.userId
            };
            AttributeValue noteId = new AttributeValue
            {
                S = request.noteId.ToString()
            };

            var key = new Dictionary<string, AttributeValue>
            {
                { "userId", userId },
                { "noteId", noteId },
            };
            
            await _client.DeleteItemAsync(_configuration.OverrideTableName, key, cancellationToken);

            return new DeleteNoteResponse { Status = true };
        }
    }
}

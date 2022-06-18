using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLService.Enteties;
using IndividualTask.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GraphQLService.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDistributedCache _cache;

        public GraphQLController(ISchema schema,
            IDocumentExecuter executer, IDistributedCache cache)
        {
            _schema = schema;
            _executer = executer;
            _cache = cache;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]
            GraphQLQueryDTO query)
        {
            var md5 = MD5.Create();   // stops
            var key = md5.ComputeHash(query.ObjectToByteArray()).BytesToString(false);
            var result = new ExecutionResult();

            var bytes = await _cache.GetAsync(key);
            if (bytes == null)
            {
                result = await _executer.ExecuteAsync(_ =>
                {
                    _.Schema = _schema;
                    _.Query = query.Query;
                    _.Inputs = query.Variables?.ToInputs();

                });


                if (result.Errors?.Count > 0)
                {
                    return BadRequest();
                }

                await _cache.SetAsync(key, result.Data.ObjectToByteArray());
            }
            else
            {
                
                return Ok(bytes.ByteArrayToObject<object>());
            }

            return Ok(result.Data);
        }
    }
}

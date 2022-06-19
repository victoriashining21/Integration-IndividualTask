using GraphQL.Types;
using IndividualTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.Enteties
{
    public class NewsType : ObjectGraphType<News>
    {
        public NewsType()
        {
            Name = "News";
            Field(_ => _.Id);
            Field(_ => _.Author);
            Field(_ => _.Title);
            Field(_=>_.ShortDescription);
            Field(_=>_.Url);
            Field(_=>_.Date);
            Field(_ =>_.Likes);
            Field(_ =>_.Dislike);
        }
    }
}

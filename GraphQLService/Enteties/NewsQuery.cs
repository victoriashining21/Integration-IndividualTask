using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumerService.Contracts;
using GraphQL;
using GraphQL.Types;
using IndividualTask.Models;

namespace GraphQLService.Enteties
{
    public class NewsQuery : ObjectGraphType
    {
        public NewsQuery(IRepository<News> newsRepository)
        {
            Field<ListGraphType<NewsType>>(
                "news",
                resolve: context => newsRepository.GetAll()
            );

            Field<NewsType>(
                "newsById",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => newsRepository.GetById(context.GetArgument<int>("id"))
            );
        }
    }
}

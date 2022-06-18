using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;


namespace GraphQLService.Enteties
{
    public class GraphQLDemoSchema : Schema, ISchema
    {
        public GraphQLDemoSchema(IDependencyResolver
            resolver) : base(resolver)
        {
            Query = resolver.Resolve<NewsQuery>();
        }
    }
}

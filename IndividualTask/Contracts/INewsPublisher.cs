using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndividualTask.Models;

namespace IndividualTask.Contracts
{
    public interface INewsPublisher
    {
        void Publish(News news);
    }
}

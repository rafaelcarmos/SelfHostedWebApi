using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Default.Queries
{
    public class DefaultGetQuery : IRequest<IEnumerable<string>>
    {
        public class DefaultQueryHandler : IRequestHandler<DefaultGetQuery, IEnumerable<string>>
        {
            public Task<IEnumerable<string>> Handle(DefaultGetQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new List<string> { "Cheguei aqui via MediatR" } as IEnumerable<string>);
            }
        }
    }
}

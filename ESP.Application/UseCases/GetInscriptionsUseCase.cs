using ESP.Domain.Interfaces.Repositories;
using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.UseCases
{
    public class GetInscriptionsUseCase
    {
        private readonly IInscriptionRepository _repository;

        public GetInscriptionsUseCase(IInscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Registration>> ExecuteAsync()
            => await _repository.GetAllAsync();

        public async Task<IEnumerable<Registration>> ExecuteByRaceAsync(int idRace)
            => await _repository.GetByRaceAsync(idRace);

    }
}

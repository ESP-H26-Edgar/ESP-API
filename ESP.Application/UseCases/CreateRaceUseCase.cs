using ESP.Application.DTOS;
using ESP.Application.Services;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;
using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.UseCases
{
    public class CreateRaceUseCase : ICreateRaceUseCase
    {
        private readonly IRaceRepository _raceRepository;
        private readonly ImageService _imageService;

        public CreateRaceUseCase(IRaceRepository raceRepository, ImageService imageService)
        {
            _raceRepository = raceRepository;
            _imageService = imageService;
        }

        public async Task<RaceDto> Execute(CreateRaceRequest request)
        {
            var imagePath = await _imageService.SaveImage(request.Image);

            if (string.IsNullOrWhiteSpace(request.RaceName))
                throw new ArgumentException("Race name is required");

            if (request.Kilometer <= 0)
                throw new ArgumentException("Kilometer must be greater than 0");

            if (request.NumberPlace <= 0)
                throw new ArgumentException("NumberPlace must be greater than 0");

            if (request.Price < 0)
                throw new ArgumentException("Price cannot be negative");

            var race = new Race
            {
                RaceName = request.RaceName,
                IdRaceType = request.IdRaceType,
                Kilometer = request.Kilometer,
                Location = request.Location,
                Date = request.Date,
                Description = request.Description,
                NumberPlace = request.NumberPlace,
                Image = imagePath,
                Price = request.Price
            };

            var createdRace = await _raceRepository.AddAsync(race);
            return new RaceDto(createdRace);
        }
    }
}

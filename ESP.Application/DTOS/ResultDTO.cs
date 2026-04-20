using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.DTOS
{
    public class ResultDto
    {
        public int IdResult { get; set; }
        public int IdUser { get; set; }
        public int IdRace { get; set; }
        public int Place { get; set; }

        public ResultDto(Result result)
        {
            IdResult = result.IdResult;
            IdUser = result.IdUser;
            IdRace = result.IdRace;
            Place = result.Place;
        }
    }
}

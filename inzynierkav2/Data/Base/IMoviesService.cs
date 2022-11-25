using inzynierka.Models;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Data.Base
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);

        Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues();

        Task AddNewMovieAsync(NewMovieVM data);
    }
}

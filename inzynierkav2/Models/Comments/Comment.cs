using pracainz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Models.Comments
{
    public class Comment
    {
        
        public int Id { get; set; }
        public string Comments { get; set; }
        public DateTime PublishedDate { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }
    }
}

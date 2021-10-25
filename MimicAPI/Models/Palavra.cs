using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Models
{
    public class Palavra
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Punctuation { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

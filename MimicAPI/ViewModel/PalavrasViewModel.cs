using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.ViewModel
{
    public class PalavrasViewModel
    {
        [Required]
        public string Name { get; set; }

        public bool Active { get; set; }

        public int Punctuation { get; set; }

    }
}

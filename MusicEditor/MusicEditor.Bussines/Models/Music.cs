using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Models
{
    public class Music
    {
        [Key]
        public string Category { get; set; }
        [Key]
        public int Number { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }

        public bool State { get; set; }
    }
}

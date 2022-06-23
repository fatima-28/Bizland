using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bizland.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Image { get; set; }
       
        public string Name { get; set; }

        public string Job { get; set; }
        public bool IsDeleted { get; set; }
    }
}

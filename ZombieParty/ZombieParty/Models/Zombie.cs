using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZombieParty.Models
{
    public class Zombie
    {
        public int id { get; set; }
        [StringLength(20,MinimumLength = 5)]
        public string Name { get; set; }

        [Display(Name = "Zombie Type")]
        [ForeignKey("ZombieType")]
        public int ZombieTypeId { get; set; }
        public ZombieType? ZombieType { get; set; }
        [Range(1, 10, ErrorMessage = "Le champ {0} doit etre entre {1} et {2}")]
        public int Point { get; set; }
        [StringLength(255, MinimumLength = 0)]
        public string? Description{ get; set; }
    }
}

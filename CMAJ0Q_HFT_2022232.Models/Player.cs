using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Models
{
    [Table("Player")]
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [ForeignKey(nameof(Team))]
        public int? TeamId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        [JsonIgnore]
        [NotMapped]
        public string AllData => $"[{PlayerId} -> {Name} - {Age} - {Nationality} - {Position}]";
        [JsonIgnore]
        [NotMapped]
        public virtual Team Team { get; set; }

        public Player GetCopy(Player value)
        {
            return new Player()
            {
                Name = value.Name,
                Age = value.Age,
                Nationality = value.Nationality,
                Position = value.Position,
                Team = value.Team,
                PlayerId = value.PlayerId,
                TeamId = value.TeamId
            };
        }

    }
}

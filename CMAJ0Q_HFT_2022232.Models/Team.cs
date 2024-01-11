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
    [Table("Team")]
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        [ForeignKey(nameof(Championship))]
        public int? ChampionshipId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Player
        {
            get { return this.Players.Count; }
        }
        [JsonIgnore]
        [NotMapped]
        public string AllData => $"[{TeamId}] -> {Name} - {Nickname}";
        [JsonIgnore]
        [NotMapped]
        public virtual Championship Championship { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            this.Players = new HashSet<Player>();
        }
        public Team GetCopy(Team value)
        {
            return new Team()
            {
                TeamId = value.TeamId,
                Name = value.Name,
                Nickname = value.Nickname,
                ChampionshipId = value.ChampionshipId,
                Championship = value.Championship,
                Players = value.Players
            };
        }

    }
}

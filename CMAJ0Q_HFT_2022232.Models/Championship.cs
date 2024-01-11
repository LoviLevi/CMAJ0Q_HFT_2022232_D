using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CMAJ0Q_HFT_2022232.Models
{
    [Table("Championship")]
    public class Championship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChampionshipId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public string AllData => $"{Name} - {Location}";
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Team> Teams { get; set; }
        public Championship()
        {
            this.Teams = new HashSet<Team>();
        }
        public Championship GetCopy(Championship value)
        {
            return new Championship()
            {
                ChampionshipId = value.ChampionshipId,
                Name = value.Name,
                Location = value.Location,
                Teams = value.Teams
            };
        }

    }
}

using CMAJ0Q_HFT_2022232.Logic.Interfaces;
using CMAJ0Q_HFT_2022232.Models;
using CMAJ0Q_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Logic
{
    public class TeamLogic : ITeamLogic
    {
        IRepository<Team> teamRepository;
        IRepository<Player> playerRepository;

        public TeamLogic(IRepository<Team> teamRepository, IRepository<Player> playerRepository)
        {
            this.teamRepository = teamRepository;
            this.playerRepository = playerRepository;
        }

        public void AddNewTeam(Team team)
        {
            teamRepository.Create(team);
        }

        public IEnumerable<IEnumerable<string>> AllNationalitiesPerTeam()
        {
            return teamRepository.GetAll().Select(x => x.Players.Select(y => y.Nationality).ToList()).ToList();
        }

        public IEnumerable<KeyValuePair<string, double>> AVGAgeOfTeamsPlayers()
        {
            return (from x in playerRepository.GetAll()
                    group x by x.Team.Name into g
                    select new KeyValuePair<string, double>
                    (
                        g.Key, g.Average(x => x.Age)
                    )).ToList();
        }

        public void DeleteTeam(int id)
        {
            teamRepository.Delete(id);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return teamRepository.GetAll();
        }

        public Team GetTeamById(int id)
        {
            if (teamRepository.GetAll().Any(x => x.TeamId.Equals(id)))
            {
                return teamRepository.Get(id);
            }
            else
            {
                throw new IndexOutOfRangeException("{ERROR} ID was too big!");
            }
        }

        public void UpdateTeam(Team team)
        {
            teamRepository.Update(team);
        }
    }
}

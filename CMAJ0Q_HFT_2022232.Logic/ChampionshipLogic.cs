using CMAJ0Q_HFT_2022232.Logic.Interfaces;
using CMAJ0Q_HFT_2022232.Models;
using CMAJ0Q_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMAJ0Q_HFT_2022232.Logic
{
    public class ChampionshipLogic : IChampionshipLogic
    {
        IRepository<Championship> championshipRepo;
        IRepository<Team> teamRepo;
        IRepository<Player> playerRepo;

        public ChampionshipLogic(IRepository<Championship> championshipRepo, IRepository<Team> teamRepo, IRepository<Player> playerRepo)
        {
            this.championshipRepo = championshipRepo;
            this.teamRepo = teamRepo;
            this.playerRepo = playerRepo;
        }
        public void AddNewChampionship(Championship championship)
        {
            championshipRepo.Create(championship);
        }

        public void DeleteChampionship(int id)
        {
            championshipRepo.Delete(id);
        }

        public IEnumerable<Championship> GetAllChampionships()
        {
            return championshipRepo.GetAll();
        }

        public Championship GetChampionshipById(int id)
        {
            if (championshipRepo.GetAll().Any(x => x.ChampionshipId.Equals(id)))
            {
                return championshipRepo.Get(id);
            }
            else
            {
                throw new IndexOutOfRangeException("{ERROR} The item does not exist! " + $"(ID: {id})");
            }
        }

        public IEnumerable<KeyValuePair<string, int>> PlayersPerChampionship()
        {
            var sub = from x in playerRepo.GetAll()
                      group x by x.TeamId into g
                      select new
                      {
                          TEAM_ID = g.Key,
                          PLAYER_NO = g.Count()
                      };

            var query = (from x in teamRepo.GetAll()
                         join z in sub on x.TeamId equals z.TEAM_ID
                         let joinedItem = new { x.TeamId, x.Name, z.PLAYER_NO }
                         join y in championshipRepo.GetAll() on x.ChampionshipId equals y.ChampionshipId
                         let yes = new { y.Name, z.PLAYER_NO }
                         group yes by yes.Name into g
                         select new KeyValuePair<string, int>
                         (
                            g.Key, g.Sum(a => a.PLAYER_NO)
                         )).ToList();


            return query;
        }

        public IEnumerable<KeyValuePair<string, int>> TeamNicknameCount()
        {
            return (from x in teamRepo.GetAll()
                    group x by x.Nickname into g
                    select new KeyValuePair<string, int>
                    (
                        g.Key, g.Count()
                    )).ToList();
        }

        public IEnumerable<KeyValuePair<string, int>> TeamNicknameCountInSpecificChampionship(int championshipId)
        {
            var nickCount = (from x in teamRepo.GetAll()
                             where x.ChampionshipId.Equals(championshipId)
                             group x by x.Nickname into g
                             select new KeyValuePair<string, int>
                             (
                                 g.Key, g.Count()
                             )).ToList();

            return nickCount;
        }

        public void UpdateChampionship(Championship championship)
        {
            championshipRepo.Update(championship);
        }
    }
}

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
    public class PlayerLogic : IPlayerLogic
    {
        IRepository<Player> playertRepository;

        public PlayerLogic(IRepository<Player> playertRepository)
        {
            this.playertRepository = playertRepository;
        }

        public void AddNewPlayer(Player player)
        {
            playertRepository.Create(player);
        }

        public void DeletePlayer(int id)
        {
            playertRepository.Delete(id);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return playertRepository.GetAll();
        }

        public Player GetPlayerById(int id)
        {
            if (playertRepository.GetAll().Any(x => x.PlayerId.Equals(id)))
            {
                return playertRepository.Get(id);
            }
            else
            {
                throw new IndexOutOfRangeException("{ERROR} ID was too big!");
            }
        }

        public bool IsNationalityPresent(string nationality)
        {
            var isPresent = playertRepository.GetAll().Any(x => x.Nationality.Contains(nationality));
            if (isPresent)
            {
                return isPresent;
            }
            else
            {
                //throw new NationalityIsNotPresentException(nationality);
                return false;
            }
        }

        public void UpdatePlayer(Player player)
        {
            playertRepository.Update(player);
        }
    }
}

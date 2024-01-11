using CMAJ0Q_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Logic.Interfaces
{
    public interface IPlayerLogic
    {
        Player GetPlayerById(int id);
        IEnumerable<Player> GetAllPlayers();
        void AddNewPlayer(Player patient);
        void UpdatePlayer(Player patient);
        void DeletePlayer(int id);

        bool IsNationalityPresent(string disease);
    }
}

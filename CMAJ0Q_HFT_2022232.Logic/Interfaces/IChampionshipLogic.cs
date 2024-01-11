using CMAJ0Q_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Logic.Interfaces
{
    public interface IChampionshipLogic
    {

        Championship GetChampionshipById(int id);
        IEnumerable<Championship> GetAllChampionships();
        void AddNewChampionship(Championship championship);
        void UpdateChampionship(Championship championship);
        void DeleteChampionship(int id);



        IEnumerable<KeyValuePair<string, int>> TeamNicknameCount();
        IEnumerable<KeyValuePair<string, int>> TeamNicknameCountInSpecificChampionship(int hospitalID);
        IEnumerable<KeyValuePair<string, int>> PlayersPerChampionship();
    }
}

using CMAJ0Q_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Logic.Interfaces
{
    public interface ITeamLogic
    {
        Team GetTeamById(int id);
        IEnumerable<Team> GetAllTeams();
        void AddNewTeam(Team team);
        void UpdateTeam(Team team);
        void DeleteTeam(int id);



        IEnumerable<KeyValuePair<string, double>> AVGAgeOfTeamsPlayers();
        IEnumerable<IEnumerable<string>> AllNationalitiesPerTeam();
    }
}

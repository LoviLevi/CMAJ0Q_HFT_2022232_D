using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CMAJ0Q_HFT_2022232.Models;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace CMAJ0Q_HFT_2022232.WpfClient
{

    public class MainWindowViewModel : ObservableRecipient
    {
        #region Errormessage
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        #endregion

        public RestCollection<Championship> Championships { get; set; }
        public RestCollection<Team> Teams { get; set; }
        public RestCollection<Player> Players { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        #region Commands
        public ICommand ManageChampionshipsCommand { get; set; }
        public ICommand ManageTeamsCommand { get; set; }
        public ICommand ManagePlayersCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public ICommand CreateChampionshipCommand { get; set; }
        public ICommand DeleteChampionshipCommand { get; set; }
        public ICommand UpdateChampionshipCommand { get; set; }

        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }

        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }


        #endregion





        #region Properties
        private bool showMenu;

        public bool ShowMenu
        {
            get { return showMenu; }
            set
            {
                SetProperty(ref showMenu, value);
            }
        }

        private bool showChampionships;

        public bool ShowChampionships
        {
            get { return showChampionships; }
            set
            {
                SetProperty(ref showChampionships, value);
                (ManageChampionshipsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        private bool showTeams;

        public bool ShowTeams
        {
            get { return showTeams; }
            set
            {
                SetProperty(ref showTeams, value);
                (ManageTeamsCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private bool showPlayers;

        public bool ShowPlayers
        {
            get { return showPlayers; }
            set
            {
                SetProperty(ref showPlayers, value);
                (ManagePlayersCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        private Championship selectedChampionship;

        public Championship SelectedChampionship
        {
            get { return selectedChampionship; }
            set
            {
                if (value != null)
                {
                    selectedChampionship = new Championship()
                    {
                        Name = value.Name,
                        Location = value.Location,
                        ChampionshipId = value.ChampionshipId
                    };
                    OnPropertyChanged();
                    (DeleteChampionshipCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateChampionshipCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Team selectedTeam;

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        Name = value.Name,
                        Nickname = value.Nickname,
                        TeamId = value.TeamId
                    };
                    OnPropertyChanged();
                    (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        Name = value.Name,
                        Age = value.Age,
                        Nationality = value.Nationality,
                        Position = value.Position,
                        PlayerId = value.PlayerId
                    };
                    OnPropertyChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        #endregion








        private string _nationality;
        public string Nationality
        {
            get => _nationality;
            set
            {
                _nationality = value;
                OnPropertyChanged(nameof(Nationality));
            }
        }

        private bool _isNationalityPresent;
        public bool IsNationalityPresent
        {
            get => _isNationalityPresent;
            set
            {
                _isNationalityPresent = value;
                OnPropertyChanged(nameof(IsNationalityPresent));
            }
        }

        public ICommand CheckNationalityCommand { get; }

        private void CheckNationality()
        {
            IsNationalityPresent = Players.Any(player => player.Nationality.Equals(Nationality, StringComparison.OrdinalIgnoreCase));
        }
        //--------------------------------------------------------------------------------------------------------------------------------

        private HttpClient _httpClient = new HttpClient();
        private string _apiBaseUrl = "http://localhost:18344/";

        private ObservableCollection<KeyValuePair<string, double>> _avgAgeOfTeamsPlayers;
        public ObservableCollection<KeyValuePair<string, double>> AvgAgeOfTeamsPlayers
        {
            get => _avgAgeOfTeamsPlayers;
            set
            {
                _avgAgeOfTeamsPlayers = value;
                OnPropertyChanged(nameof(AvgAgeOfTeamsPlayers));
            }
        }

        public ICommand LoadAvgAgeOfTeamsPlayersCommand { get; }

        private async Task LoadAvgAgeOfTeamsPlayers()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}stat/avgageofteamsplayers");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, double>>>(jsonString);
                AvgAgeOfTeamsPlayers.Clear();
                foreach (var item in data)
                {
                    AvgAgeOfTeamsPlayers.Add(item);
                }
            }
            else
            {
                // Kezelje a hiba eseteket
            }
        }
        //----------------------------------------------------------------------------------------------
        private ObservableCollection<KeyValuePair<string, int>> _teamNicknameCounts;
        public ObservableCollection<KeyValuePair<string, int>> TeamNicknameCounts
        {
            get => _teamNicknameCounts;
            set
            {
                _teamNicknameCounts = value;
                OnPropertyChanged(nameof(TeamNicknameCounts));
            }
        }
        public ICommand LoadTeamNicknameCountsCommand { get; }

        private async Task LoadTeamNicknameCounts()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}stat/teamnicknamecount");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, int>>>(jsonString);
                TeamNicknameCounts.Clear();
                foreach (var item in data)
                {
                    TeamNicknameCounts.Add(item);
                }
            }
            else
            {
                // Kezelje a hiba eseteket
            }
        }
        //----------------------------------------------------------------------------------------------
        private ObservableCollection<KeyValuePair<string, int>> _playersPerChampionship;
        public ObservableCollection<KeyValuePair<string, int>> PlayersPerChampionship
        {
            get => _playersPerChampionship;
            set
            {
                _playersPerChampionship = value;
                OnPropertyChanged(nameof(PlayersPerChampionship));
            }
        }
        public ICommand LoadPlayersPerChampionshipCommand { get; }

        private async Task LoadPlayersPerChampionship()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}stat/playersperchampionship");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, int>>>(jsonString);
                PlayersPerChampionship.Clear();
                foreach (var item in data)
                {
                    PlayersPerChampionship.Add(item);
                }
            }
            else
            {
                // Kezelje a hiba eseteket
            }
        }
        //----------------------------------------------------------------------------------------------

        private int _selectedChampionshipId;
        public int SelectedChampionshipId
        {
            get => _selectedChampionshipId;
            set
            {
                _selectedChampionshipId = value;
                OnPropertyChanged(nameof(SelectedChampionshipId));
            }
        }

        private ObservableCollection<KeyValuePair<string, int>> _teamNicknames;
        public ObservableCollection<KeyValuePair<string, int>> TeamNicknames
        {
            get => _teamNicknames;
            set
            {
                _teamNicknames = value;
                OnPropertyChanged(nameof(TeamNicknames));
            }
        }

        public ICommand LoadTeamNicknamesCommand { get; }

        private async Task LoadTeamNicknames()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}stat/teamnicknamecountinspecificchampionship/{SelectedChampionshipId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, int>>>(jsonString);
                TeamNicknames.Clear();
                foreach (var item in data)
                {
                    TeamNicknames.Add(item);
                }
            }
            else
            {
                // Kezelje a hiba eseteket
            }
        }

























































        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Championships = new RestCollection<Championship>("http://localhost:18344/", "championship");
                CreateChampionshipCommand = new RelayCommand(() =>
                {
                    Championships.Add(new Championship()
                    {
                        Name = SelectedChampionship.Name,
                        Location = SelectedChampionship.Location
                    });
                });

                Teams = new RestCollection<Team>("http://localhost:18344/", "team");
                CreateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Add(new Team()
                    {
                        Name = SelectedTeam.Name,
                        Nickname = SelectedTeam.Nickname
                    });
                });

                Players = new RestCollection<Player>("http://localhost:18344/", "player");
                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                        Name = SelectedPlayer.Name,
                        Nationality = SelectedPlayer.Nationality,
                        Age = SelectedPlayer.Age,
                        Position = SelectedPlayer.Position
                    });
                });
                //----------------------------------------------------------------------------------------------
                UpdateChampionshipCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Championships.Update(SelectedChampionship);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                UpdateTeamCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Teams.Update(SelectedTeam);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });


                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Players.Update(SelectedPlayer);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                //----------------------------------------------------------------------------------------------
                DeleteChampionshipCommand = new RelayCommand(() =>
                {
                    Championships.Delete(SelectedChampionship.ChampionshipId);
                },
                () =>
                {
                    return SelectedChampionship != null;
                });

                DeleteTeamCommand = new RelayCommand(() =>
                {
                    Teams.Delete(SelectedTeam.TeamId);
                },
                () =>
                {
                    return SelectedTeam != null;
                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId);
                },
                () =>
                {
                    return SelectedPlayer != null;
                });
                //----------------------------------------------------------------------------------------------
                SelectedChampionship = new Championship();
                SelectedTeam = new Team();
                SelectedPlayer = new Player();

                //----------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------
                CheckNationalityCommand = new RelayCommand(CheckNationality);

                //----------------------------------------------------------------------------------------------
                LoadAvgAgeOfTeamsPlayersCommand = new RelayCommand(async () => await LoadAvgAgeOfTeamsPlayers());
                AvgAgeOfTeamsPlayers = new ObservableCollection<KeyValuePair<string, double>>();
                //----------------------------------------------------------------------------------------------
                LoadTeamNicknameCountsCommand = new RelayCommand(async () => await LoadTeamNicknameCounts());
                TeamNicknameCounts = new ObservableCollection<KeyValuePair<string, int>>();
                //----------------------------------------------------------------------------------------------
                LoadPlayersPerChampionshipCommand = new RelayCommand(async () => await LoadPlayersPerChampionship());
                PlayersPerChampionship = new ObservableCollection<KeyValuePair<string, int>>();
                //----------------------------------------------------------------------------------------------
                TeamNicknames = new ObservableCollection<KeyValuePair<string, int>>();
                LoadTeamNicknamesCommand = new RelayCommand(async () => await LoadTeamNicknames());

            }

        }
    }
}

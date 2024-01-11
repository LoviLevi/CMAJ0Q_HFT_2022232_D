using CMAJ0Q_HFT_2022232.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CMAJ0Q_HFT_2022232.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            #region THREADSLEEP AND LOADING
            Console.Write("L");
            Thread.Sleep(1000);
            Console.Write("O");
            Thread.Sleep(1500);
            Console.Write("A");
            Thread.Sleep(500);
            Console.Write("D");
            Thread.Sleep(500);
            Console.Write("I");
            Thread.Sleep(250);
            Console.Write("N");
            Thread.Sleep(250);
            Console.Write("G");
            Thread.Sleep(750);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(100);
            #endregion


            RestService rs = new RestService("http://localhost:18344");

            #region GetALLMenu
            var subGetAll = new ConsoleMenu(args, level: 1)
           .Add("Get all championships", () => GetAllInstance(rs, "championship"))
           .Add("Get all teams", () => GetAllInstance(rs, "team"))
           .Add("Get all players", () => GetAllInstance(rs, "player"))
           .Add("Back", ConsoleMenu.Close)
           .Configure(config =>
           {
               config.Selector = "~>";
               config.EnableFilter = true;
               config.Title = "Get all data\n";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
           });
            #endregion

            #region GetOneMenu
            var subGetOne = new ConsoleMenu(args, level: 1)
        .Add("Get one championships", () => GetOneInstance(rs, "championship"))
        .Add("Get one teams", () => GetOneInstance(rs, "team"))
        .Add("Get one players", () => GetOneInstance(rs, "player"))
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "~>";
            config.EnableFilter = true;
            config.Title = "Get all data\n";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
        });
            #endregion

            #region UPDATE
            var subUpdate = new ConsoleMenu(args, level: 1)
         .Add("Update a championships", () => Update(rs, "championship"))
         .Add("Update a teams", () => Update(rs, "team"))
         .Add("Update a players", () => Update(rs, "player"))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Update\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });

            #endregion

            #region DELETE
            var subDelete = new ConsoleMenu(args, level: 1)
         .Add("Delete a championships", () => Delete(rs, "championship"))
         .Add("Delete a teams", () => Delete(rs, "team"))
         .Add("Delete a players", () => Delete(rs, "player"))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Delete\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion

            #region CREATE
            var subCreate = new ConsoleMenu(args, level: 1)
         .Add("Create a championships", () => Create(rs, "championship"))
         .Add("Create a teams", () => Create(rs, "team"))
         .Add("Create a players", () => Create(rs, "player"))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Create\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion

            #region STATS
            var subStats = new ConsoleMenu(args, level: 1)
         .Add("Avarage age of players per team", () => AVGAgeOfTeamsPlayers(rs))
         .Add("Count of teams nickname", () => TeamNicknameCount(rs))
         //.Add("Specific nationality present at team", () => NationalityPerTeam(rs))
         .Add("All player per championship", () => PlayersPerChampionship(rs))
         .Add("Count of teams nickname in specific championship", () => TeamNicknameCountInSpecificChampionship(rs))
         .Add("Is the specific nationality is present", () => IsNationalityPresent(rs))
         //.Add("All nationalities per team", () => AllNationalityPerTeam(rs))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Stats\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion


            #region MainMenu
            var menu = new ConsoleMenu(args, level: 0)
           .Add("Get all data", subGetAll.Show)
           .Add("Get one instance", subGetOne.Show)
           .Add("Create", subCreate.Show)
           .Add("Update", subUpdate.Show)
           .Add("Delete", subDelete.Show)
           .Add("Stats", subStats.Show)
           .Add("Exit", () => Environment.Exit(0))
           .Configure(config =>
           {
               config.Selector = "~>";
               config.EnableFilter = true;
               config.Title = "CHAMPIONSHIP DATABASE";
               config.EnableWriteTitle = false;
               config.EnableBreadcrumb = true;
           });
            #endregion

            menu.Show();
        }

        static void AllNationalityPerTeam(RestService rs)
        {
            Console.Clear();


            var allnationalies = rs.GetSingle<IEnumerable<IEnumerable<string>>>($"stat/allnationalitiesperteam");
            var teamNames = rs.Get<Team>("team").Select(x => x.Name).ToList();

            int y = 0;
            foreach (var item in allnationalies)
            {
                Console.WriteLine($"TeamName: {teamNames[y++]}");
                Console.WriteLine("Diseases:");
                foreach (var nat in item)
                {
                    Console.WriteLine($"-{nat}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void IsNationalityPresent(RestService rs)
        {
            Console.Clear();

            Console.Write("Type in the name of a nationality: ");
            string nationality = Console.ReadLine();
            Console.Clear();

            var stat = rs.GetSingle<bool>($"stat/IsNationalityPresent/{nationality}");
            if (stat)
            {
                Console.WriteLine($"{nationality} is present!");
            }
            else
            {
                Console.WriteLine($"{nationality} is not present!");
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void TeamNicknameCountInSpecificChampionship(RestService rs)
        {
            Console.Clear();

            rs.Get<Championship>($"championship").ForEach(x => Console.WriteLine($"[{x.ChampionshipId}] - {x.Name}"));


            Console.Write("Select an id: "); Console.WriteLine();
            int championshipID = int.Parse(Console.ReadLine());
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>($"stat/teamnicknamecountinspecificchampionship/{championshipID}").ToList();
            Console.WriteLine("Nickname -> Count of teams");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void PlayersPerChampionship(RestService rs)
        {
            Console.Clear();


            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>($"stat/playersperchampionship").ToList();
            Console.WriteLine("Championship name: -> Count of players");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }


        static void TeamNicknameCount(RestService rs)
        {
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>("stat/teamnicknamecount").ToList();
            Console.WriteLine("Nickname -> Count");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void AVGAgeOfTeamsPlayers(RestService rs)
        {
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, double>>>("stat/avgageofteamsplayers").ToList();
            Console.WriteLine("Team name -> AVG Age of players");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void Create(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("CREATE");

            if (model == "championship")
            {
                Console.WriteLine("Must fill in the following options:");
                Console.Write("Championship Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Championship Location: ");
                string location = Console.ReadLine(); Console.WriteLine();



                rs.Post<Championship>(new Championship() { Name = name, Location = location }, model);
            }
            else if (model == "team")
            {
                Console.WriteLine("Must fill in the following options:");
                Console.Write("Team Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Team nickname: ");
                string nick = Console.ReadLine(); Console.WriteLine();


                rs.Get<Championship>($"{model}").ForEach(x => Console.WriteLine($"[{x.ChampionshipId}] - {x.Name}"));
                Console.WriteLine("Which championship do you want to add the new team? (select id)");
                int id = int.Parse(Console.ReadLine());



                rs.Post<Team>(new Team() { Name = name, Nickname = nick }, model);
            }
            else
            {
                Console.WriteLine("Must fill in the following options:");
                Console.Write("Player Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Player Age: ");
                string age = Console.ReadLine(); Console.WriteLine();

                Console.Write("Player position: ");
                string position = Console.ReadLine(); Console.WriteLine();

                Console.Write("Player nationality: ");
                string nationality = Console.ReadLine(); Console.WriteLine();

                rs.Get<Team>($"{model}").ForEach(x => Console.WriteLine($"[{x.TeamId}] - {x.Name}"));
                Console.WriteLine("Which team do you want to add the new player? (select id)");
                int id = int.Parse(Console.ReadLine());




                rs.Post<Player>(new Player() { Name = name, TeamId = id, Age = int.Parse(age), Position = position, Nationality = nationality }, model);
            }

            Console.Clear();
            Console.WriteLine("Item created!");
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void Delete(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("DELETE");
            if (model == "championship")
            {
                rs.Get<Championship>($"{model}").ForEach(x => Console.WriteLine($"[{x.ChampionshipId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                rs.Delete(id, model);

            }
            else if (model == "team")
            {
                rs.Get<Team>($"{model}").ForEach(x => Console.WriteLine($"[{x.TeamId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                rs.Delete(id, model);
            }
            else
            {
                rs.Get<Player>($"{model}").ForEach(x => Console.WriteLine($"[{x.PlayerId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                rs.Delete(id, model);
            }
            Console.Clear();
            Console.WriteLine("Item deleted!");
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void Update(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("UPDATE");

            if (model == "championship")
            {
                rs.Get<Championship>($"{model}").ForEach(x => Console.WriteLine($"[{x.ChampionshipId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();


                Console.WriteLine("Must fill in the following options:");
                Console.Write("Championship Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Championship Location: ");
                string location = Console.ReadLine(); Console.WriteLine();
                Console.Clear();


                rs.Put<Championship>(new Championship() { ChampionshipId = id, Name = name, Location = location }, model);
            }
            else if (model == "team")
            {
                rs.Get<Team>($"{model}").ForEach(x => Console.WriteLine($"[{x.TeamId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();


                Console.WriteLine("Must fill in the following options:");
                Console.Write("Team Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Team Nickname: ");
                string nick = Console.ReadLine(); Console.WriteLine();
                Console.Clear();


                rs.Put<Team>(new Team() { TeamId = id, Name = name, Nickname = nick }, model);
            }
            else
            {
                rs.Get<Player>($"{model}").ForEach(x => Console.WriteLine($"[{x.PlayerId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();


                Console.WriteLine("Must fill in the following options:");
                Console.Write("Player Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Player Age: ");
                string age = Console.ReadLine(); Console.WriteLine();

                Console.Write("Player position: ");
                string position = Console.ReadLine(); Console.WriteLine();

                Console.Write("Player nationality: ");
                string nationality = Console.ReadLine(); Console.WriteLine();
                Console.Clear();


                rs.Put<Player>(new Player() { PlayerId = id, Name = name, Age = int.Parse(age), Position = position, Nationality = nationality }, model);
            }

            Console.Clear();
            Console.WriteLine("Item updated!");
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void GetOneInstance(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("GET ONE");

            if (model == "championship")
            {
                rs.Get<Championship>($"{model}").ForEach(x => Console.WriteLine($"[{x.ChampionshipId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                var champ = rs.GetSingle<Championship>($"{model}/{id}");
                Console.WriteLine($"{champ.AllData} - Championships");

            }
            else if (model == "team")
            {
                rs.Get<Team>($"{model}").ForEach(x => Console.WriteLine($"[{x.TeamId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                var team = rs.GetSingle<Team>($"{model}/{id}");
                Console.WriteLine($"{team.AllData} - Players: {team.Player}");
            }
            else
            {
                rs.Get<Player>($"{model}").ForEach(x => Console.WriteLine($"[{x.PlayerId}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                var player = rs.GetSingle<Player>($"{model}/{id}");
                Console.WriteLine($"{player.AllData}");
            }
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void GetAllInstance(RestService rs, string model)
        {

            Console.Clear();
            Console.WriteLine("GET ALL");

            if (model == "championship")
            {
                rs.Get<Championship>($"{model}").ForEach(x => Console.WriteLine(x.AllData));
            }
            else if (model == "team")
            {
                rs.Get<Team>($"{model}").ForEach(x => Console.WriteLine(x.AllData));
            }
            else
            {
                rs.Get<Player>($"{model}").ForEach(x => Console.WriteLine(x.AllData));
            }


            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }


    }
}

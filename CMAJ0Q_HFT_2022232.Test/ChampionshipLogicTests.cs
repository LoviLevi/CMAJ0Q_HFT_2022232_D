using CMAJ0Q_HFT_2022232.Logic;
using CMAJ0Q_HFT_2022232.Models;
using CMAJ0Q_HFT_2022232.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMAJ0Q_HFT_2022232.Test
{
    public class ChampionshipLogicTests
    {
        private ChampionshipLogic ChampionshipLogic { get; set; }
        private TeamLogic TeamLogic { get; set; }
        private PlayerLogic PlayerLogic { get; set; }

        [SetUp]
        public void Setup()
        {
            Mock<IRepository<Championship>> mockedChampRepo = new Mock<IRepository<Championship>>();
            Mock<IRepository<Team>> mockedTeamRepo = new Mock<IRepository<Team>>();
            Mock<IRepository<Player>> mockedPlayerRepo = new Mock<IRepository<Player>>();


            mockedChampRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new Championship()
                {
                    ChampionshipId = 1,
                    Name = "Premier League",
                    Location = "England"
                });

            mockedChampRepo.Setup(x => x.GetAll()).Returns(this.FakeChampionshipObjects);
            mockedTeamRepo.Setup(x => x.GetAll()).Returns(this.FakeTeamObjects);
            mockedPlayerRepo.Setup(x => x.GetAll()).Returns(this.FakePlayerObjects);

            this.ChampionshipLogic = new ChampionshipLogic(mockedChampRepo.Object, mockedTeamRepo.Object, mockedPlayerRepo.Object);
            this.TeamLogic = new TeamLogic(mockedTeamRepo.Object, mockedPlayerRepo.Object);
            this.PlayerLogic = new PlayerLogic(mockedPlayerRepo.Object);


        }
        [Test]
        public void GetOneChampionship_ReturnsCorrectInstance()
        {
            var champItem = ChampionshipLogic.GetChampionshipById(1);

            Assert.That(champItem.Name, Is.EqualTo("Premier League"));
        }

        [Test]
        public void GetAllChampionship_ReturnExactNumberOfInstances()
        {
            Assert.That(ChampionshipLogic.GetAllChampionships().Count(), Is.EqualTo(2));
        }

        [Test]
        public void TeamNicknamesCount_ReturnsCorrectNumbers()
        {
            Assert.That(ChampionshipLogic.TeamNicknameCount().ToList().Count, Is.EqualTo(5));
        }

        [Test]
        public void PlayersPerTeam_ReturnsCorrectNumbers()
        {
            Assert.That(ChampionshipLogic.PlayersPerChampionship().ToList()[0].Value, Is.EqualTo(12));
        }

        [Test]
        public void AllNationalityPerTeam_ReturnsCorrectTeamsCount()
        {
            Assert.That(() => TeamLogic.AllNationalitiesPerTeam().Count(), Is.EqualTo(6));
        }
        [TestCase(1, 5)]
        [TestCase(2, 0)]
        public void TeamNicknameCountInSpecificChampionship_ReturnsCorrectNumbers(int id, int expected)
        {
            Assert.That(() => ChampionshipLogic.TeamNicknameCountInSpecificChampionship(id).ToList().Count, Is.EqualTo(expected));
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void GetChampionshipById_ThrowsException_IfIdIsTooBig(int id)
        {
            Assert.That(() => ChampionshipLogic.GetChampionshipById(id), Throws.TypeOf<IndexOutOfRangeException>());
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void GetTeamById_DoesNotThrowException_IfIdIsInRange(int id)
        {
            Assert.That(() => TeamLogic.GetTeamById(id), Throws.Nothing);
        }
        [TestCase("Hungary")]
        [TestCase("Slovak")]
        [TestCase("Croatia")]
        [TestCase("Romania")]
        public void IsNationalityPresent_ThrowsException_IfNationalityIsNotPresentAmongPayers(string nationality)
        {
            Assert.That(() => PlayerLogic.IsNationalityPresent(nationality), Throws.TypeOf<NationalityIsNotPresentException>());
        }
        [TestCase("England")]
        [TestCase("Brazil")]
        [TestCase("France")]
        [TestCase("Spain")]
        public void IsDiseasePresent_ThrowsNothing_IfDiseaseIsPresent(string disease)
        {
            Assert.That(() => PlayerLogic.IsNationalityPresent(disease), Throws.Nothing);
        }
        private IQueryable<Championship> FakeChampionshipObjects()
        {
            Championship c0 = new Championship() { ChampionshipId = 1, Name = "Premier League", Location = "England" };

            Team t0 = new Team() { TeamId = 1, Name = "Manchester United", Nickname = "The Red Devils" };
            Team t1 = new Team() { TeamId = 2, Name = "Liverpool", Nickname = "The Reds" };
            Team t2 = new Team() { TeamId = 3, Name = "Arsenal", Nickname = "The Gunners" };

            Player p0 = new Player() { PlayerId = 1, Name = "Marcus Rashford", Age = 25, Position = "Winger", Nationality = "England" };
            Player p1 = new Player() { PlayerId = 2, Name = "Mohamed Salah", Age = 30, Position = "Winger", Nationality = "Egypt" };
            Player p2 = new Player() { PlayerId = 3, Name = "Bukayo Saka", Age = 21, Position = "Midfield", Nationality = "England" };
            Player p3 = new Player() { PlayerId = 4, Name = "Bruno Fernandes", Age = 28, Position = "Midfield", Nationality = "Portugal" };
            Player p4 = new Player() { PlayerId = 5, Name = "Alisson", Age = 30, Position = "Goalkeeper", Nationality = "Brazil" };
            Player p5 = new Player() { PlayerId = 6, Name = "Gabriel Jesus", Age = 25, Position = "Attacker", Nationality = "Brazil" };


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Championship c1 = new Championship() { ChampionshipId = 1, Name = "LaLiga", Location = "Spain" };

            Team t3 = new Team() { TeamId = 4, Name = "FC Barcelona", Nickname = "Barça" };
            Team t4 = new Team() { TeamId = 5, Name = "Real Madrid", Nickname = "Los Blancos" };
            Team t5 = new Team() { TeamId = 6, Name = "Atlético Madrid", Nickname = "The Reds" };

            Player p6 = new Player() { PlayerId = 6, Name = "Robert Lewandowski", Age = 34, Position = "Attacker", Nationality = "Poland" };
            Player p7 = new Player() { PlayerId = 7, Name = "Karim Benzema", Age = 34, Position = "Attacker", Nationality = "France" };
            Player p8 = new Player() { PlayerId = 8, Name = "Jan Oblak", Age = 29, Position = "Goalkeeper", Nationality = "Slovenia" };
            Player p9 = new Player() { PlayerId = 9, Name = "Sergio Busquets", Age = 34, Position = "Midfield", Nationality = "Spain" };
            Player p10 = new Player() { PlayerId = 10, Name = "Marco Asensio", Age = 26, Position = "Winger", Nationality = "Spain" };
            Player p11 = new Player() { PlayerId = 11, Name = "Koke", Age = 30, Position = "Midfield", Nationality = "Spain" };

            t0.ChampionshipId = c0.ChampionshipId;
            t1.ChampionshipId = c0.ChampionshipId;
            t2.ChampionshipId = c0.ChampionshipId;

            p0.TeamId = t0.TeamId;
            p1.TeamId = t1.TeamId;
            p2.TeamId = t2.TeamId;
            p3.TeamId = t0.TeamId;
            p4.TeamId = t1.TeamId;
            p5.TeamId = t2.TeamId;



            //---------------------------------
            t3.ChampionshipId = c1.ChampionshipId;
            t4.ChampionshipId = c1.ChampionshipId;
            t5.ChampionshipId = c1.ChampionshipId;

            p6.TeamId = t3.TeamId;
            p7.TeamId = t4.TeamId;
            p8.TeamId = t5.TeamId;
            p9.TeamId = t3.TeamId;
            p10.TeamId = t4.TeamId;
            p11.TeamId = t5.TeamId;

            List<Championship> items = new List<Championship>();
            items.Add(c0);
            items.Add(c1);
            return items.AsQueryable();
        }
        private IQueryable<Team> FakeTeamObjects()
        {
            Championship c0 = new Championship() { ChampionshipId = 1, Name = "Premier League", Location = "England" };

            Team t0 = new Team() { TeamId = 1, Name = "Manchester United", Nickname = "The Red Devils" };
            Team t1 = new Team() { TeamId = 2, Name = "Liverpool", Nickname = "The Reds" };
            Team t2 = new Team() { TeamId = 3, Name = "Arsenal", Nickname = "The Gunners" };

            Player p0 = new Player() { PlayerId = 1, Name = "Marcus Rashford", Age = 25, Position = "Winger", Nationality = "England" };
            Player p1 = new Player() { PlayerId = 2, Name = "Mohamed Salah", Age = 30, Position = "Winger", Nationality = "Egypt" };
            Player p2 = new Player() { PlayerId = 3, Name = "Bukayo Saka", Age = 21, Position = "Midfield", Nationality = "England" };
            Player p3 = new Player() { PlayerId = 4, Name = "Bruno Fernandes", Age = 28, Position = "Midfield", Nationality = "Portugal" };
            Player p4 = new Player() { PlayerId = 5, Name = "Alisson", Age = 30, Position = "Goalkeeper", Nationality = "Brazil" };
            Player p5 = new Player() { PlayerId = 6, Name = "Gabriel Jesus", Age = 25, Position = "Attacker", Nationality = "Brazil" };


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Championship c1 = new Championship() { ChampionshipId = 1, Name = "LaLiga", Location = "Spain" };

            Team t3 = new Team() { TeamId = 4, Name = "FC Barcelona", Nickname = "Barça" };
            Team t4 = new Team() { TeamId = 5, Name = "Real Madrid", Nickname = "Los Blancos" };
            Team t5 = new Team() { TeamId = 6, Name = "Atlético Madrid", Nickname = "The Reds" };

            Player p6 = new Player() { PlayerId = 6, Name = "Robert Lewandowski", Age = 34, Position = "Attacker", Nationality = "Poland" };
            Player p7 = new Player() { PlayerId = 7, Name = "Karim Benzema", Age = 34, Position = "Attacker", Nationality = "France" };
            Player p8 = new Player() { PlayerId = 8, Name = "Jan Oblak", Age = 29, Position = "Goalkeeper", Nationality = "Slovenia" };
            Player p9 = new Player() { PlayerId = 9, Name = "Sergio Busquets", Age = 34, Position = "Midfield", Nationality = "Spain" };
            Player p10 = new Player() { PlayerId = 10, Name = "Marco Asensio", Age = 26, Position = "Winger", Nationality = "Spain" };
            Player p11 = new Player() { PlayerId = 11, Name = "Koke", Age = 30, Position = "Midfield", Nationality = "Spain" };

            t0.ChampionshipId = c0.ChampionshipId;
            t1.ChampionshipId = c0.ChampionshipId;
            t2.ChampionshipId = c0.ChampionshipId;

            p0.TeamId = t0.TeamId;
            p1.TeamId = t1.TeamId;
            p2.TeamId = t2.TeamId;
            p3.TeamId = t0.TeamId;
            p4.TeamId = t1.TeamId;
            p5.TeamId = t2.TeamId;



            //---------------------------------
            t3.ChampionshipId = c1.ChampionshipId;
            t4.ChampionshipId = c1.ChampionshipId;
            t5.ChampionshipId = c1.ChampionshipId;

            p6.TeamId = t3.TeamId;
            p7.TeamId = t4.TeamId;
            p8.TeamId = t5.TeamId;
            p9.TeamId = t3.TeamId;
            p10.TeamId = t4.TeamId;
            p11.TeamId = t5.TeamId;

            List<Team> items = new List<Team>();
            items.Add(t0);
            items.Add(t1);
            items.Add(t2);
            items.Add(t3);
            items.Add(t4);
            items.Add(t5);
            return items.AsQueryable();
        }
        private IQueryable<Player> FakePlayerObjects()
        {
            Championship c0 = new Championship() { ChampionshipId = 1, Name = "Premier League", Location = "England" };

            Team t0 = new Team() { TeamId = 1, Name = "Manchester United", Nickname = "The Red Devils" };
            Team t1 = new Team() { TeamId = 2, Name = "Liverpool", Nickname = "The Reds" };
            Team t2 = new Team() { TeamId = 3, Name = "Arsenal", Nickname = "The Gunners" };

            Player p0 = new Player() { PlayerId = 1, Name = "Marcus Rashford", Age = 25, Position = "Winger", Nationality = "England" };
            Player p1 = new Player() { PlayerId = 2, Name = "Mohamed Salah", Age = 30, Position = "Winger", Nationality = "Egypt" };
            Player p2 = new Player() { PlayerId = 3, Name = "Bukayo Saka", Age = 21, Position = "Midfield", Nationality = "England" };
            Player p3 = new Player() { PlayerId = 4, Name = "Bruno Fernandes", Age = 28, Position = "Midfield", Nationality = "Portugal" };
            Player p4 = new Player() { PlayerId = 5, Name = "Alisson", Age = 30, Position = "Goalkeeper", Nationality = "Brazil" };
            Player p5 = new Player() { PlayerId = 6, Name = "Gabriel Jesus", Age = 25, Position = "Attacker", Nationality = "Brazil" };


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Championship c1 = new Championship() { ChampionshipId = 1, Name = "LaLiga", Location = "Spain" };

            Team t3 = new Team() { TeamId = 4, Name = "FC Barcelona", Nickname = "Barça" };
            Team t4 = new Team() { TeamId = 5, Name = "Real Madrid", Nickname = "Los Blancos" };
            Team t5 = new Team() { TeamId = 6, Name = "Atlético Madrid", Nickname = "The Reds" };

            Player p6 = new Player() { PlayerId = 6, Name = "Robert Lewandowski", Age = 34, Position = "Attacker", Nationality = "Poland" };
            Player p7 = new Player() { PlayerId = 7, Name = "Karim Benzema", Age = 34, Position = "Attacker", Nationality = "France" };
            Player p8 = new Player() { PlayerId = 8, Name = "Jan Oblak", Age = 29, Position = "Goalkeeper", Nationality = "Slovenia" };
            Player p9 = new Player() { PlayerId = 9, Name = "Sergio Busquets", Age = 34, Position = "Midfield", Nationality = "Spain" };
            Player p10 = new Player() { PlayerId = 10, Name = "Marco Asensio", Age = 26, Position = "Winger", Nationality = "Spain" };
            Player p11 = new Player() { PlayerId = 11, Name = "Koke", Age = 30, Position = "Midfield", Nationality = "Spain" };

            t0.ChampionshipId = c0.ChampionshipId;
            t1.ChampionshipId = c0.ChampionshipId;
            t2.ChampionshipId = c0.ChampionshipId;

            p0.TeamId = t0.TeamId;
            p1.TeamId = t1.TeamId;
            p2.TeamId = t2.TeamId;
            p3.TeamId = t0.TeamId;
            p4.TeamId = t1.TeamId;
            p5.TeamId = t2.TeamId;



            //---------------------------------
            t3.ChampionshipId = c1.ChampionshipId;
            t4.ChampionshipId = c1.ChampionshipId;
            t5.ChampionshipId = c1.ChampionshipId;

            p6.TeamId = t3.TeamId;
            p7.TeamId = t4.TeamId;
            p8.TeamId = t5.TeamId;
            p9.TeamId = t3.TeamId;
            p10.TeamId = t4.TeamId;
            p11.TeamId = t5.TeamId;

            List<Player> items = new List<Player>();
            items.Add(p0);
            items.Add(p1);
            items.Add(p2);
            items.Add(p3);
            items.Add(p4);
            items.Add(p5);
            items.Add(p6);
            items.Add(p7);
            items.Add(p8);
            items.Add(p9);
            items.Add(p10);
            items.Add(p11);

            return items.AsQueryable();
        }

    }
}

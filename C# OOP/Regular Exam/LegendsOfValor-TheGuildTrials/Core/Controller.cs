using LegendsOfValor_TheGuildTrials.Core.Contracts;
using LegendsOfValor_TheGuildTrials.Models;
using LegendsOfValor_TheGuildTrials.Models.Contracts;
using LegendsOfValor_TheGuildTrials.Repositories;
using LegendsOfValor_TheGuildTrials.Repositories.Contratcs;
using LegendsOfValor_TheGuildTrials.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Core
{
    public class Controller : IController
    {
        IRepository<IHero> heroRepository;
        IRepository<IGuild> guildRepository;

        public Controller()
        {
            heroRepository = new HeroRepository();
            guildRepository = new GuildRepository();
        }

        public string AddHero(string heroTypeName, string heroName, string runeMark)
        {
            if (heroTypeName != nameof(Warrior) && heroTypeName != nameof(Sorcerer) && heroTypeName != nameof(Spellblade))
            {
                 return string.Format(OutputMessages.InvalidHeroType, heroTypeName);
            }

            IHero hero = heroRepository.GetModel(runeMark);

            if (hero != null)
            {
                return string.Format(OutputMessages.HeroAlreadyExists, runeMark);
            }

            switch (heroTypeName)
            {
                case nameof(Warrior):
                    hero = new Warrior(heroName, runeMark);
                    break;

                case nameof(Sorcerer):
                    hero = new Sorcerer(heroName, runeMark);
                    break;

                default:
                    hero = new Spellblade(heroName, runeMark);
                    break;
            }

            heroRepository.AddModel(hero);

            return string.Format(OutputMessages.HeroAdded, heroTypeName, heroName, runeMark);
        }

        public string CreateGuild(string guildName)
        {
            IGuild guild = guildRepository.GetModel(guildName);

            if (guild != null)
            {
                return string.Format(OutputMessages.GuildAlreadyExists, guildName);
            }

            guild = new Guild(guildName);

            guildRepository.AddModel(guild);

            return string.Format(OutputMessages.GuildCreated, guildName);
        }

        public string RecruitHero(string runeMark, string guildName)
        {
            IHero hero = heroRepository.GetModel(runeMark);

            if (hero == null)
            {
                return string.Format(OutputMessages.HeroNotFound, runeMark);
            }

            IGuild guild = guildRepository.GetModel(guildName);

            if (guild == null)
            {
                return string.Format(OutputMessages.GuildNotFound, guildName);
            }

            if (hero.GuildName != null)
            {
                return string.Format(OutputMessages.HeroAlreadyInGuild, hero.Name);
            }

            if (guild.IsFallen)
            {
                return string.Format(OutputMessages.GuildIsFallen, guildName);
            }

            if (guild.Wealth < 500)
            {
                return string.Format(OutputMessages.GuildCannotAffordRecruitment, guildName);
            }

            string heroType = hero.GetType().Name;

            if ((heroType == nameof(Warrior) && guild.Name == "SorcererGuild") ||
                (heroType == nameof(Sorcerer) && guild.Name == "WarriorGuild") ||
                (heroType == nameof(Spellblade) && guild.Name == "ShadowGuild"))
            {
                return string.Format(OutputMessages.HeroTypeNotCompatible, heroType, guildName);
            }

            hero.JoinGuild(guild);
            guild.RecruitHero(hero);

            return string.Format(OutputMessages.HeroRecruited, hero.Name, guildName);
        }

        public string TrainingDay(string guildName)
        {
            IGuild guild = guildRepository.GetModel(guildName);

            if (guild == null)
            {
                return string.Format(OutputMessages.GuildNotFound, guildName);
            }

            if (guild.IsFallen)
            {
                return string.Format(OutputMessages.GuildTrainingDayIsFallen,  guildName);
            }

            int totalTrainingCost = guild.Legion.Count * 200;

            if (guild.Wealth < totalTrainingCost)
            {
                return string.Format(OutputMessages.TrainingDayFailed, guildName);
            }

            List<IHero> heroesToTrain = heroRepository
                .GetAll()
                .Where(h => guild.Legion.Contains(h.RuneMark))
                .ToList();

            guild.TrainLegion(heroesToTrain);

            return string.Format(OutputMessages.TrainingDayStarted, guildName, heroesToTrain.Count, totalTrainingCost);
        }

        public string StartWar(string attackerGuildName, string defenderGuildName)
        {
            IGuild attackerGuild = guildRepository.GetModel(attackerGuildName);
            IGuild defenderGuild = guildRepository.GetModel(defenderGuildName);

            if (attackerGuild is null || defenderGuild is null)
            {
                return OutputMessages.OneOfTheGuildsDoesNotExist;
            }

            if (attackerGuild.IsFallen || defenderGuild.IsFallen)
            {
                return OutputMessages.OneOfTheGuildsIsFallen;
            }

            List<IHero> attackingHeroes = heroRepository
                .GetAll()
                .Where(h => attackerGuild.Legion.Contains(h.RuneMark))
                .ToList();

            List<IHero> defendingHeroes = heroRepository
                .GetAll()
                .Where(h => defenderGuild.Legion.Contains(h.RuneMark))
                .ToList();

            int attackingHeroesTotalPower = attackingHeroes.Sum(h => h.Power)
                                        + attackingHeroes.Sum(h => h.Mana)
                                        + attackingHeroes.Sum(h => h.Stamina);

            int defendingHeroesTotalPower = defendingHeroes.Sum(h => h.Power)
                                        + defendingHeroes.Sum(h => h.Mana)
                                        + defendingHeroes.Sum(h => h.Stamina);

            if (attackingHeroesTotalPower > defendingHeroesTotalPower)
            {
                int amountOfGold = defenderGuild.Wealth;
                attackerGuild.WinWar(amountOfGold);
                defenderGuild.LoseWar();

                return string.Format(OutputMessages.WarWon, attackerGuildName, defenderGuildName, amountOfGold);
            }
            else
            {
                int amountOfGold = attackerGuild.Wealth;
                defenderGuild.WinWar(amountOfGold);
                attackerGuild.LoseWar();

                return string.Format(OutputMessages.WarLost, defenderGuildName, amountOfGold, attackerGuildName);
            }
        }

        public string ValorState()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Valor State:");

            foreach (IGuild guild in guildRepository.GetAll().OrderByDescending(g => g.Wealth))
            {
                sb.AppendLine($"{guild.Name} (Wealth: {guild.Wealth})");

                List<IHero> heroes = heroRepository
                .GetAll()
                .Where(h => guild.Legion.Contains(h.RuneMark))
                .ToList();

                foreach (IHero hero in heroes.OrderBy(h => h.Name))
                {
                    sb.AppendLine($"-{hero.ToString()}");
                    sb.AppendLine($"--{hero.Essence()}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}

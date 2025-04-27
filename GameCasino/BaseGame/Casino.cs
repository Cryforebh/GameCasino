using GameCasino.BaseGame.Games;
using GameCasino.ProfileSystem;
using GameCasino.SaveLoad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame
{
    public class Casino : IGame
    {
        private readonly ISaveLoadService<string> _saveService;
        private PlayerProfile _profile;
        private const int MaxBankValue = 1_000_000;

        public Casino(ISaveLoadService<string> saveService)
        {
            _saveService = saveService;
            LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                _profile = _saveService.LoadData<PlayerProfile>("SaveGameCasino_Kirill_Yagodko");
                if (_profile == null)
                {
                    CreateNewProfile();
                }

            }
            catch (FileNotFoundException)
            {
                CreateNewProfile();
            }
        }

        private void CreateNewProfile()
        {
            _profile = new PlayerProfile();
            Console.Write("Введите имя игрока: ");
            _profile.Name = Console.ReadLine();
            SaveProfile();
        }

        public void SaveProfile()
        {
            _saveService.SaveData(_profile, "SaveGameCasino_Kirill_Yagodko");
        }

        public void StartGame()
        {
            Console.WriteLine($"Добро пожаловать в казино, {_profile.Name}!");
            try
            {
                if (_profile.Balance <= 0)
                {
                    // “No money? Kicked!”
                    Console.WriteLine("Нет денег? Выходи!");
                    return;
                }

                Console.WriteLine("Выберите игру:\n1. Блэкджек\n2. Кости");
                int choice = GetValidChoice(1, 2);

                int bet = GetValidBet();
                CasinoGameBase game = CreateGame(choice, bet);

                SetupGameHandlers(game);
                game.PlayGame();

                CheckBankLimit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                SaveProfile();
            }
        }

        private int GetValidChoice(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine($"Некорректный ввод. Введите число от {min} до {max}.");
            }
            return choice;
        }

        private int GetValidBet()
        {
            int bet;
            do
            {
                Console.Write($"Введите ставку (макс: {_profile.Balance}): ");
            } while (!int.TryParse(Console.ReadLine(), out bet) || bet <= 0 || bet > _profile.Balance);

            return bet;
        }

        private CasinoGameBase CreateGame(int choice, int bet)
        {
            switch (choice)
            {
                case 1:
                    return new BlackjackGame(52, bet);
                case 2:
                    return new DiceGame(5, 1, 6, bet);
                default:
                    throw new InvalidOperationException();
            }
        }

        private void SetupGameHandlers(CasinoGameBase game)
        {
            game.OnWin += (s, e) =>
            {
                _profile.Balance += e.BetAmount;
                Console.WriteLine($"{e.GameDetails} | Выигрыш: +{e.BetAmount}");
            };

            game.OnLose += (s, e) =>
            {
                _profile.Balance -= e.BetAmount;
                Console.WriteLine($"{e.GameDetails} | Проигрыш: -{e.BetAmount}");
            };

            game.OnDraw += (s, e) =>
            {
                Console.WriteLine($"{e.GameDetails} | Ничья, ставка возвращена");
            };
        }

        private void CheckBankLimit()
        {
            if (_profile.Balance > MaxBankValue)
            {
                int oldBalance = _profile.Balance;
                _profile.Balance = MaxBankValue / 2;
                // “You wasted half of your bank money in casino’s bar”
                Console.WriteLine($"Вы потратили половину своих банковских денег в баре казино! {oldBalance} → {_profile.Balance}");
            }
            else
            {
                Console.WriteLine($"Баланс: {_profile.Balance}");
            }
        }

    }
}

using GameCasino.BaseGame;
using GameCasino.SaveLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Инициализация системы сохранения
                var saveService = new FileSystemSaveLoadService("D:\\CasinoKirillYagodko");

                // Создание экземпляра казино
                var casino = new Casino(saveService);

                // Основной игровой цикл
                while (true)
                {
                    Console.Clear();
                    casino.StartGame();

                    Console.WriteLine("\nНажмите Q для выхода или любую клавишу для продолжения");
                    if (Console.ReadKey().Key == ConsoleKey.Q) break;
                }

                // Автосохранение при выходе
                casino.SaveProfile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}

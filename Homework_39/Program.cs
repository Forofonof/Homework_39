using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        const string AddingPlayer = "1";
        const string ShowAllPlayers = "2";
        const string BanPlayer = "3";
        const string UnBanPlayer = "4";
        const string DeletingPlayer = "5";
        const string Exit = "6";

        bool isWork = true;

        Datebase datebase = new Datebase();

        while (isWork)
        {
            Console.WriteLine($"{AddingPlayer} - Добавить игрока.\n{ShowAllPlayers} - Показать всех игроков.\n{BanPlayer} - Забанить игрока.");
            Console.WriteLine($"{UnBanPlayer} - Разбанить игрока.\n{DeletingPlayer} - Удалить игрока.\n{Exit} - Закрыть программу.");
            string userInput = Console.ReadLine();
            Console.Clear();

            switch (userInput)
            {
                case AddingPlayer:
                    datebase.AddPlayer();
                    break;
                case ShowAllPlayers:
                    datebase.ShowPlayersDatabase();
                    break;
                case BanPlayer:
                    datebase.BanPlayer();
                    break;
                case UnBanPlayer:
                    datebase.UnBanPlayer();
                    break;
                case DeletingPlayer:
                    datebase.DeletePlayer();
                    break;
                case Exit:
                    isWork = false;
                    break;
                default:
                    Console.WriteLine("\nОшибка! Нет такой команды!\n");
                    break;
            }
        }
    }
}

class Player
{
    public string Nickname { get; private set; }
    
    public int Level { get; private set; }

    public bool IsBanned { get; private set; }

    public string Banned { get; private set; }

    public Player(string nickname, int level, bool isBanned)
    {
        Nickname = nickname;
        Level = level;
        IsBanned = isBanned;
    }

    public void ShowPlayerInfo()
    {
        DetermineBanStatus();
        Console.WriteLine($"Ник игрока: {Nickname}. Уровень: {Level}. Блокировка игрока: {Banned}");
    }

    public void DetermineBanStatus()
    {
        if (IsBanned == true)
        {
            Banned = "Есть";
        }
        else
        {
            Banned = "Нет";
        }
    }

    public void Ban()
    {
        IsBanned = true;
    }

    public void UnBan()
    {
        IsBanned = false;
    }
}

class Datebase
{
    private const string Yes = "y";
    private const string No = "n";

    private Dictionary<int, Player> _players = new Dictionary<int, Player>();
    private int _playerIndex = 0;

    public void Database()
    {
        _playerIndex = 0;
    }

    public void AddPlayer()
    {
        Console.WriteLine("Введите nickname игрока: ");
        string nickname = Console.ReadLine();

        Console.WriteLine("Введите уровень игрока: ");
        bool isNumber = int.TryParse(Console.ReadLine(), out int level);

        if (isNumber == false)
        {
            Console.WriteLine("Введено неверное значение.");
            return;
        }

        Console.WriteLine($"Имеет ли игрок блокировку?\n{Yes} / {No}");
        string userInput = Console.ReadLine();
        bool isBanned;

        if (userInput.ToLower() == Yes)
        {
            isBanned = true;
        }
        else if (userInput.ToLower() == No)
        {
            isBanned = false;
        }
        else
        {
            Console.WriteLine("Введено неверное значение.");
            return;
        }

        _players.Add(_playerIndex, new Player(nickname, level, isBanned));
        _playerIndex++;

        Console.Clear();
        Console.WriteLine("Успешно! Игрок добавлен.");
    }

    public void ShowPlayersDatabase()
    {
        if (_players.Count != 0)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowPlayerInfo();

                Console.WriteLine($"Индекс игрока: {i}");
            }
        }
        else
        {
            Console.WriteLine("В базе данных нет игроков!");
        }
    }

    public void BanPlayer()
    {
        Console.WriteLine("Введите индекс игрока:");
        ReadNumber(out bool isNumber, out int index);

        if (_players[index].IsBanned == false && isNumber == true)
        {
            _players[index].Ban();
            Console.WriteLine("Успешно! Игрок заблокирован.");
        }
        else if(_players[index].IsBanned == true)
        {
            Console.WriteLine("Игрок уже заблокирован!");
        }
    }

    public void UnBanPlayer()
    {
        Console.WriteLine("Введите индекс игрока:");
        ReadNumber(out bool isNumber, out int index);

        if (_players[index].IsBanned == true && isNumber == true)
        {
            _players[index].UnBan();
            Console.WriteLine("Успешно! Игрок разблокирован.");
        }
        else if (_players[index].IsBanned == false)
        {
            Console.WriteLine("Игрок уже разблокирован!");
        }
    }

    public void DeletePlayer()
    {
        Console.WriteLine("Введите индекс игрока:");
        ReadNumber(out bool isNumber, out int userInput);

        if (_players.ContainsKey(userInput) == true && isNumber == true)
        {
            _players.Remove(userInput);
            Console.WriteLine("Успешно! Игрок удален.");
        }

    }

    private void ReadNumber(out bool isNumber, out int index)
    {
        if (isNumber = int.TryParse(Console.ReadLine(), out index) && _players.ContainsKey(index) == true)
        {

        }
        else
        {
            Console.WriteLine("Ошибка ввода!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using neurowll;
class Program
{
    static void Main()
    {
        Console.WriteLine("Добро пожаловать, боец!");
        Console.Write("Назови себя: ");
        string playerName = Console.ReadLine();
        Player player = new Player(playerName, 100);

        player.HealthKit = new Aid("Средняя аптечка", 10);

        int totalPoints = 0;

        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy("Крип-мечник", new Random().Next(20, 100), new Weapon("Меч", 16, 100)),
            new Enemy("Медведемон-крушитель", new Random().Next(20, 100), new Weapon("Клинки-росомахи", 20, 100)),
            new Enemy("Глиняный голем", new Random().Next(20, 100), new Weapon("Камни", 6, 100)),
            new Enemy("Тролль-некромант", new Random().Next(20, 100), new Weapon("Посох", 14, 100)),
            new Enemy("Скиталец-шаман", new Random().Next(20, 100), new Weapon("Секира", 28, 100))
        };

        List<Weapon> playerWeapons = new List<Weapon>
{
    new Weapon("Буриза", 8, 100),
    new Weapon("Рапира", 30, 100),
    new Weapon("Пулемет", 17, 100),
    new Weapon("Дезолятор", 23, 100),
    new Weapon("Дагон", 12, 100)
};
        List<Weapon> weapons = new List<Weapon>
{
    new Weapon("Меч", 10, 100),
    new Weapon("Клинки-росомахи", 15, 100),
    new Weapon("Камни", 18, 100),
    new Weapon("Посох", 12, 100),
    new Weapon("Секира", 25, 100)
};
        while (player.CurrentHealth > 0)
        {
            Enemy enemy = enemies[new Random().Next(enemies.Count)];
            Weapon enemyWeapon = weapons[new Random().Next(weapons.Count)];
            player.CurrentWeapon = playerWeapons[new Random().Next(playerWeapons.Count)];
            enemy.EnemyWeapon = enemyWeapon;
            Console.WriteLine($"Вы встречаете врага {enemy.Name} ({enemy.CurrentHealth}hp) и замечаете у него {enemy.EnemyWeapon.Name} ({enemy.EnemyWeapon.Damage}).");
            while (player.CurrentHealth > 0 && enemy.CurrentHealth > 0)
            {
                Console.WriteLine($"Ваше здоровье: {player.CurrentHealth}, Здоровье врага: {enemy.CurrentHealth}");
                Console.WriteLine($"{player.Name}, у вас оружие: {player.CurrentWeapon.Name}");

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Атаковать");
                Console.WriteLine("2. Подождать");
                Console.WriteLine("3. Похиллиться");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        player.Attack(enemy);
                        Console.WriteLine($"Вы атаковали {enemy.Name}!");
                        break;
                    case "2":
                        Console.WriteLine($"Вы тихо ожидаете, когда {enemy.Name} атакует.");
                        break;
                    case "3":
                        player.Heal();
                        Console.WriteLine($"Вы похиллились. Ваше текущее здоровье: {player.CurrentHealth}");
                        break;
                    default:
                        Console.WriteLine("Нет такого варианта ответа. Выберите цифру 1, 2 или 3.");
                        break;
                }


                enemy.Attack(player);
                Console.WriteLine($"{enemy.Name} атакует вас! Ваше текущее здоровье: {player.CurrentHealth}");
            }

            if (player.CurrentHealth <= 0)
            {
                Console.WriteLine($"Игра окончена. {enemy.Name} победил! Ваши общие очки {totalPoints}");
                break;
            }
            else
            {
                Console.WriteLine($"Вы победили {enemy.Name} и получили очки!");


                player.CurrentHealth += 20;
                Console.WriteLine($"Вы похиллились на 20 хп. Ваше здоровье: {player.CurrentHealth}");
                totalPoints += 10;
                Console.WriteLine($"Ваши текущие очки: {totalPoints}");

                Console.WriteLine("Хотите продолжить игру? (Да/Нет)");
                string continueChoice = Console.ReadLine();
                if (continueChoice.ToLower() != "да")
                {
                    Console.WriteLine($"Игра завершена. Ваши общие очки: {totalPoints}");
                    break;
                }
            }
        }
    }
}
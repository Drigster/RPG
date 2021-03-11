//По большей части все 3 задания сделаны(Не совсем по заданию правда)
//Всё с комментариями, разобратся легко
//
//Gabriel Joe

using System;
using System.Numerics;
using System.Collections.Generic;

namespace Game2
{
    public class Character //Персонаж
    {
        public int Health = 10; //Жизни
        public int Armor = 0; //Броня
        public int Damage = 1; //Урон(не готово)

        public bool IsAlive = true;

        public Character(int health, int armor, int damage)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void ApplyDamage(int damage) //Отнять урон
        {
            if (Health <= (damage - Armor)) //Проверяем если (урон - броня) больше или равер жизням
            {
                Health = 0;
                IsAlive = false;
            }
            else
            {
                Health -= (damage - Armor); //Отнимаем (урон - броня) от жизней
            }
        }
    }

    public class Player : Character //Класс игрока
    {
        Random rnd = new Random();

        public int StepsFromLastEvent; //Шаги с последнего события
        public int StepsUntllEvent; //Шаги до следующего события
        public int Steps; //Всего шагов
        public int Coins; //Монеты
        public Vector2 Position = new Vector2(0, 0); //Координаты персонажа

        public Player(int health, int armor, int damage, int coins = 0) : base(health, armor, damage)
        {
            StepsUntllEvent = rnd.Next(3, 15); //Определяем шаги до следующего события
            Coins = coins;
        }

        public void Move(Vector2 direction)
        {
            Steps++; //Увеличиваем шаги
            StepsFromLastEvent++; //Увеличиваем шаги с прошлого события
            Position += direction; //Вычисляем позицию с учётом последнего движения
        }
    }

    public class Enemy : Character
    {
        Random rnd = new Random();

        public int Level; //Уровень врага
        public int Loot; //Добыча с врага(монеты)

        public Enemy() : base(1, 1, 1)
        {
            Level = rnd.Next(1, 100); //Уровень врага. Случайно от 1 до 100
            IsAlive = true; //Жив ли враг?

            if (Level < 60) //Если уровень врага меньше 60(1-59)
            {
                base.Health = rnd.Next(1, 5); //Случайные жизни врага
                base.Armor = rnd.Next(0, 2); //Случайное количство брони врага
                base.Damage = rnd.Next(1, 3); //Случайный урон врага
                Loot = rnd.Next(1, 5); //Случайное количество добычи с врага
            }
            else if (Level < 90) //Если уровень врага меньше 90(60-89)
            {
                base.Health = rnd.Next(4, 9); //Случайные жизни врага
                base.Armor = rnd.Next(2, 5); //Случайное количство брони врага
                base.Damage = rnd.Next(3, 6); //Случайный урон врага
                Loot = rnd.Next(5, 10); //Случайное количество добычи с врага
            }
            else if (Level <= 100) //Если уровень врага меньше 100(90-100)
            {
                base.Health = rnd.Next(10, 15); //Случайные жизни врага
                base.Armor = rnd.Next(4, 8); //Случайное количство брони врага
                base.Damage = rnd.Next(5, 8); //Случайный урон врага
                Loot = rnd.Next(10, 20); //Случайное количество добычи с врага
            }
        }

        public void Attack(Player player)
        {
            ApplyDamage(player.Damage); //Отнимаем урон у врага
            if (IsAlive) //Если враг остался жив
            {
                player.ApplyDamage(Damage); //Отнимаем урон у игрока
            }
        }
    }

    public class Shop //Магазин
    {
        Random rnd = new Random();

        public int WeaponPrice; //Цена оружия
        public int ArmorPrice; //Цена брони
        public int FoodPrice; //Цена еды
        public Shop()
        {
            WeaponPrice = rnd.Next(1, 10); //Случайная цена на оружие
            ArmorPrice = rnd.Next(5, 15); //Случайная цена на броню
            FoodPrice = rnd.Next(1, 3); //Случайная цена на еду
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo action;
            string message = "Вы начли игру";

            Player player = new Player(100, 0, 0); //Создаём персонажа. Жизни-Броня-Урон-Монеты
            do
            {
                List<string> path = new List<string>() //Картинка пути в интерфейсе
                {
                    "      | |      ",
                    "      | |      ",
                    "______| |______",
                    "______   ______",
                    "      | |      ",
                    "      | |      ",
                    "      | |      "
                };
                DrawInterface(path, player, 1, message); //Рисуем интерфейс. Картинка пути-Игрок-Игрок(так как второго нету)-Тип интерфейса(1-обычный)-Сообщение

                action = Console.ReadKey(true); //Скрываем отображение нажатых клавиш в интерфейсе
                if (action.KeyChar == 'w') //Нажата кнопка W
                {
                    Vector2 direction = new Vector2(1, 0); //Направление движения. Прямо-Направо. В нашем случаии вперёд
                    player.Move(direction); //'Двигаем' персонажа
                    message = "Вы пошли вперёд"; //Сообщение в интерфейсе
                }
                else if (action.KeyChar == 's') //Нажата кнопка S
                {
                    Vector2 direction = new Vector2(-1, 0); //Направление движения. Прямо-Направо. В нашем случаии назад
                    player.Move(direction); //'Двигаем' персонажа
                    message = "Вы пошли назад"; //Сообщение в интерфейсе
                }
                else if (action.KeyChar == 'a') //Нажата кнопка A
                {
                    Vector2 direction = new Vector2(0, -1); //Направление движения. Прямо-Направо. В нашем случаии налево
                    player.Move(direction); //'Двигаем' персонажа
                    message = "Вы пошли налево"; //Сообщение в интерфейсе
                }
                else if (action.KeyChar == 'd') //Нажата кнопка D
                {
                    Vector2 direction = new Vector2(0, 1); //Направление движения. Прямо-Направо. В нашем случаии направо
                    player.Move(direction); //'Двигаем' персонажа
                    message = "Вы пошли направо"; //Сообщение в интерфейсе
                }

                string eventMessage = TryGenerateEvent(player); //Пытаемся сгенерировать событие. Возвращяет сообщение
                if (eventMessage != null) //Если сообщение события не равно пустоте
                {
                    message = eventMessage; //Присваеваем сообщению сообщение события
                }

            } while (action.Key != ConsoleKey.Escape && player.IsAlive); //Выход при нажатии кнопки ESC

            Console.WriteLine($"Вы прошли: {player.Steps} шагов, ваши координаты: X = {player.Position.X} Y = {player.Position.Y}"); //Информация после выхода или проигрыша
        }

        public static string TryGenerateEvent(Player player) //Попытка создать интерфейс. Игрок
        {
            Random rnd = new Random();
            string message = null;

            if (player.StepsUntllEvent == player.StepsFromLastEvent) //Вычисляем равны ли шаги количеству шагов до события
            {
                if (rnd.Next(1, 3) == 1) //Магазин. Шанс 1 из 3
                {
                    Shop shop = new Shop();

                    List<string> shopImage = new List<string> //Картинка магазина в интерфейсе
                    {
                        "  ___________  ",
                        " /    $$$    \\ ", //Знак \ отменяет следующий символ в считывании текста
                        "/_____________\\",
                        "|             |",
                        "|_____________|",
                        "|===S=H=O=P===|",
                        "|_____________|"
                    };

                    DrawInterface(shopImage, player, 1, "Вы пришли в магазин. Чтобы войти нажмите клавишу E", shop); //Рисуем интерфейс магазина. Картинка магазина-Игрок-Сообщение-Магазин

                    ConsoleKeyInfo action = Console.ReadKey(true);
                    if (action.KeyChar == 'e') //Если нажимаем E
                    {
                        message = "Вы в магазине. Чтобы выйти нажмите клавишу X";
                        do
                        {
                            DrawInterface(shopImage, player, 2, message, shop); //Рисуем интерфейс. Картинка магазина-Игрок-Тип 2(Магазин)-Сообщение-Магазин
                            action = Console.ReadKey(true);

                            if (action.KeyChar == '1') //Нажата кнопка 1
                            {
                                if (player.Coins >= shop.WeaponPrice) //Если у вас хватает монет
                                {
                                    player.Coins -= shop.WeaponPrice; //Вычитаем цену
                                    player.Damage++;
                                    message = "Вы купили оружие"; //Сообщение о покупке
                                }
                                else
                                {
                                    message = "У вас не хватает монет";
                                }
                            }
                            else if (action.KeyChar == '2') //Нажата кнопка 2
                            {
                                if (player.Coins >= shop.ArmorPrice) //Если у вас хватает монет
                                {
                                    player.Coins -= shop.ArmorPrice; //Вычитаем цену
                                    player.Armor++;
                                    message = "Вы купили броню"; //Сообщение о покупке
                                }
                                else
                                {
                                    message = "У вас не хватает монет";
                                }
                            }
                            else if (action.KeyChar == '3') //Нажата кнопка 3
                            {
                                if (player.Coins >= shop.FoodPrice) //Если у вас хватает монет
                                {
                                    player.Coins -= shop.FoodPrice; //Вычитаем цену
                                    player.Health++;
                                    message = "Вы купили еду"; //Сообщение о покупке
                                }
                                else
                                {
                                    message = "У вас не хватает монет";
                                }
                            }

                        } while (action.Key != ConsoleKey.X); //Если нажимаем кнопку X цикл завершается
                        message = "Вы вышли из магазина";
                    }
                }
                else
                {
                    message = "Вы видите врага";
                    List<string> enemyImage = new List<string>() //Картинка врага в интерфейсе
                    {
                        "XXXXXXXXXXXXXXX",
                        "XXXXXXXXXXXXXXX",
                        "XXXXXXXXXXXXXXX",
                        "XXXXXXXXXXXXXXX",
                        "XXXXXXXXXXXXXXX",
                        "XXXXXXXXXXXXXXX",
                        "XXXXXXXXXXXXXXX"
                    };

                    DrawInterface(enemyImage, player, 1, message); //Рисуем интерфейс. Картинка врага-Игрок-Тип 1(Обычный)-Сообщение

                    ConsoleKeyInfo action = Console.ReadKey(true);

                    if (action.KeyChar == 'e') //Если нажисаем E
                    {
                        Enemy enemy = new Enemy(); //Создаём врага
                        while (true)
                        {
                            message = "Вы атаковали врага";
                            enemy.Attack(player); //Атакуем врага. Игрок
                            if (!enemy.IsAlive) //Если враг мёртв
                            {
                                player.Coins += enemy.Loot; //Добавляем игроку добычу
                                message = $"Вы победили(+{enemy.Loot} Coins)";
                                break; //Завершаем цикл
                            }
                            else if (!player.IsAlive)
                            { //Если враг мёртв
                                break; //Завершаем цикл
                            }
                            DrawInterface(enemyImage, player, 3, message, null, enemy); //Рисуем интерфейс. Картинка врага-Игрок-Тип 3(Враг)-Сообщениеё-НЕЧЕГО-враг
                            action = Console.ReadKey(true);
                        }
                    }
                    else
                    {
                        message = "Вы сбежали(-10 Health)";
                        player.ApplyDamage(10); //Применяем урон к игроку
                    }
                }

                player.StepsFromLastEvent = 0; //Сбрасываем счётчик магов с предедушего события
                player.StepsUntllEvent = rnd.Next(3, 15); //Назначем случайное число в шаги до следующего события
            }

            return message; //Возвращяем сообщение
        }

        public static void DrawInterface(List<string> image, Player player, int type, string message = null, Shop shop = null, Enemy enemy = null) //Рисуем интерфейс. Картинка-Игрок-Тип интерфейса(1-обычный, 2-магазин, 3-бой)-Сообщение-Магазин-Враг
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (type == 1)//Стандартный интерфейс
            {
                List<string> hint = new List<string>() //Нижняя панель(подсказка)
                {
                    "                                                               ",
                    {message},
                    "===============================================================",
                    "     W - Вперёд    S - Назад    A -  Налево    D - Направо     ",
                    "                       E - Использовать                        "
                };

                List<string> info = new List<string>() //Информация о персонаже
                {
                    "   +                               ",
                   $"   +   Жизни: {player.Health}      ",
                    "   +                               ",
                   $"   +   Броня: {player.Armor}       ",
                    "   +                               ",
                   $"   +   Урон: {player.Damage}       ",
                    "   +                               "
                };

                string indent = "             "; //Отступ перед картинкой(Чтобы красиво было)

                Console.Clear(); //очищяем консоль

                for (int i = 0; i < image.Count; i++) //Рисуем верхнюю часть
                {
                    string line = indent + image[i] + info[i]; //Соеденияем отступ, строку картинки(по индексу строки) и строку информации
                    Console.WriteLine(line, Console.ForegroundColor); //Рисуем верхнюю часть по строчно
                }

                for (int i = 0; i < hint.Count; i++) //Рисуем нижнюю часть
                {
                    Console.WriteLine(hint[i]); //Рисуем подсказку по строчно
                }
            }
            else if (type == 2)//Интерфейс магазина
            {
                List<string> shopHint = new List<string>() //Нижняя панель магазина(подсказка)
                {
                   "                                                               ",
                   {message},
                   "===============================================================",
                   "   1 - Купить оружие(+1 Damage)    2 - Купить броню(+1 Armor)  ",
                   "             3 - Купить еду(+1 Health)    X - Выйти            "
                };

                List<string> shopInfo = new List<string>() //Информация о персонаже
                {
                   $"   +   Ваши монеты: {player.Coins}      ",
                    "   +                                    ",
                   $"   +   Магазин:                         ",
                    "   +                                    ",
                   $"   +   Оружие: {shop.WeaponPrice}$      ",
                   $"   +   Броня: {shop.ArmorPrice}$        ",
                   $"   +   Еда: {shop.FoodPrice}$           "
                };

                string indent = "             "; //Отступ перед картинкой(Чтобы красиво было)

                Console.Clear(); //очищяем консоль

                for (int i = 0; i < image.Count; i++) //Рисуем верхнюю часть
                {
                    string line = indent + image[i] + shopInfo[i]; //Соеденияем отступ, строку картинки(по индексу строки) и строку информации
                    Console.WriteLine(line); //Рисуем верхнюю часть по строчно
                }

                for (int i = 0; i < shopHint.Count; i++) //Рисуем нижнюю часть
                {
                    Console.WriteLine(shopHint[i]); //Рисуем подсказку по строчно
                }
            }
            else if (type == 3)//Интерфейс магазина
            {
                List<string> shopHint = new List<string>() //Нижняя панель магазина(подсказка)
                {
                   "                                                               ",
                   {message},
                   "===============================================================",
                   "            E - Атаковать    Any key - Убежать(-10)            ",
                   "                                                               "
                };

                List<string> shopInfo = new List<string>() //Информация о персонаже
                {
                    "   +                               ",
                   $"   +   Жизни: {player.Health}      ",
                    "   +                               ",
                   $"   +   Броня: {player.Armor}       ",
                    "   +                               ",
                   $"   +   Урон: {player.Damage}       ",
                    "   +                               "
                };

                string indent = "             "; //Отступ перед картинкой(Чтобы красиво было)

                Console.Clear(); //очищяем консоль

                for (int i = 0; i < image.Count; i++) //Рисуем верхнюю часть
                {
                    string line = indent + image[i] + shopInfo[i]; //Соеденияем отступ, строку картинки(по индексу строки) и строку информации
                    Console.WriteLine(line); //Рисуем верхнюю часть по строчно
                }

                for (int i = 0; i < shopHint.Count; i++) //Рисуем нижнюю часть
                {
                    Console.WriteLine(shopHint[i]); //Рисуем подсказку по строчно
                }
            }
        }
    }
}

//По большей части все 3 задания сделаны(Не совсем по заданию правда)
//Всё с комментариями, разобратся легко
//Жизни отнимаются в соучайном количестве во время события, в это же время добовляются монеты.
//Это должны были заменить враги


using System;
using System.Numerics;
using System.Collections.Generic;

namespace Game2
{
    public class Character //Персонаж
    {
        public int Health = 10; //Жизни
        public int Armor = 0; //Броня
        public int Atack = 1; //Урон(не готово)

        public Character(int health, int armor, int atack)
        {
            Health = health;
            Armor = armor;
            Atack = atack;
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

        public Player(int health, int armor, int atack, int coins) : base(health, armor, atack)
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

    public class Enemy : Character //Не готово :((((
    {
        public Enemy(int health, int armor, int atack) : base(health, armor, atack)
        {

        }
    }

    public class Shop
    {
        Random rnd = new Random();

        public int WeaponPrice; //Цена оружия
        public int ArmorPrice; //Цена брони
        public int FoodPrice; //Цена еды
        public Shop()
        {
            WeaponPrice = rnd.Next(1, 10);
            ArmorPrice = rnd.Next(5, 15);
            FoodPrice = rnd.Next(1, 3);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo action;
            string message = "Вы начли игру";

            Player player = new Player(100, 0, 2, 0); //Создаём персонажа. Жизни-Броня-Урон
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

                TryGenerateEvent(player); //Пытаемся сгенерировать событие

            } while (action.Key != ConsoleKey.Escape); //Выход при нажатии кнопки ESC

            Console.WriteLine($"Вы прошли: {player.Steps} шагов, ваши координаты: X = {player.Position.X} Y = {player.Position.Y}"); //Информация после выхода или проигрыша
        }

        public static void TryGenerateEvent(Player player) //Попытка создать интерфейс. Игрок
        {
            Random rnd = new Random();

            if (player.StepsUntllEvent == player.StepsFromLastEvent) //Вычисляем равны ли шаги количеству шагов до события
            {
                if(rnd.Next(1, 3) == 1) //Магазин. Шанс 1 из 3
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
                    if(action.KeyChar == 'e')
                    {
                        string message = "Вы в магазине. Чтобы выйти нажмите клавишу X";
                        do
                        {
                            DrawInterface(shopImage, player, 2, message, shop);
                            action = Console.ReadKey(true);

                            if (action.KeyChar == '1') //Нажата кнопка 1
                            {
                                if (player.Coins >= shop.WeaponPrice) //Если у вас хватает монет
                                {
                                    player.Coins -= shop.WeaponPrice; //Вычитаем цену
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
                                    message = "Вы купили еду"; //Сообщение о покупке
                                }
                                else
                                {
                                    message = "У вас не хватает монет";
                                }
                            }

                        } while (action.Key != ConsoleKey.X);
                    }
                }
                else
                {
                    //Тут должен был быть враг но я ушёл спать
                    player.Coins += rnd.Next(1, 5); //Случайно прибавляем монеты
                    player.Health -= rnd.Next(1, 10) - player.Armor; //Случайно убавляем жизни(может пойти в +)
                }

                player.StepsFromLastEvent = 0; //Сбрасываем счётчик магов с предедушего события
                player.StepsUntllEvent = rnd.Next(3, 15); //Назначем случайное число в шаги до следующего события
            }
        }

        public static void DrawInterface(List<string> image, Player player, int type, string message = null, Shop shop=null, Enemy enemy=null) //Рисуем интерфейс. Картинка-Игрок-Тип интерфейса(1-обычный, 2-магазин, 3-бой)-Сообщение-Магазин-Враг
        {
            if(type == 1)//Стандартный интерфейс
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
                   $"   +   Урон: {player.Atack}(не готово)        ",
                    "   +                               "
                };

                string indent = "             "; //Отступ перед картинкой(Чтобы красиво было)

                Console.Clear(); //очищяем консоль

                for (int i = 0; i < image.Count; i++) //Рисуем верхнюю часть
                {
                    string line = indent + image[i] + info[i]; //Соеденияем отступ, строку картинки(по индексу строки) и строку информации
                    Console.WriteLine(line); //Рисуем верхнюю часть по строчно
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
        }
    }
}

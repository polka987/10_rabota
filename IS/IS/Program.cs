using Newtonsoft.Json;
using IS;
using System.Collections.Generic;

class Program
{
    public static List<user> users = json.Deserialize<List<user>>("save.json") ?? new List<user>();

    static void Main()
    {
        if (users.Count == 0)
            users.Add(new user("admin", "admin"));
        string us = Auth();
        int ra = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Роль: {us}");
            Console.WriteLine($"\nКоманды:\nF1 - Добавить пользователя | F2 - Удалить\nF3 - Обновить | F4 - Поиск | Escape - Сохранить\n");
            Console.WriteLine("Все пользователи:");
            foreach (var a in users)
                Console.WriteLine("  " + a.Login);
            Console.SetCursorPosition(0, 7 + ra);
            Console.WriteLine("->");
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.DownArrow)
                if (ra < users.Count - 1) ra++;
            if (key == ConsoleKey.UpArrow)
                if (ra > 0) ra--;
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                user tmp = users[ra];
                Console.WriteLine($"Логин: {tmp.Login}, пароль: {tmp.Password}");
                Console.ReadKey(true);
            }
            if (key == ConsoleKey.F1) Add();
            if (key == ConsoleKey.F2)
            {
                users.RemoveAt(ra);
                ra = 0;
            }
            if (key == ConsoleKey.F3) Update(ra);
            if (key == ConsoleKey.F4) Search();
            if (key == ConsoleKey.Escape) json.Serialize("save.json", users);
            if(key == ConsoleKey.Backspace) Main();
        }
    }
    static bool inuser(user user)
    {
        foreach (var a in users)
            if (a.Login == user.Login && a.Password == user.Password) return true;
        return false;
    }
    static string Auth()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите логин");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            string password = string.Empty;
            ConsoleKeyInfo info;
            while (true)
            {
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                    break;
                password += info.KeyChar;
                Console.Write('*');
            }
            Console.WriteLine();
            if (inuser(new user(login, password))) return login;
            Console.WriteLine("Логин или пароль неверный");
        }
    }

    static void Add()
    {
        Console.Clear();
        Console.WriteLine("Введите логин");
        string tmp_login = Console.ReadLine();
        Console.WriteLine("Введите пароль");
        string tmp_password = Console.ReadLine();
        if (!inuser(new user(tmp_login, tmp_password)))
            users.Add(new user(tmp_login, tmp_password));
        else Console.WriteLine("\nТакой пользователь уже есть");
    }

    static void Update(int pol)
    {
        Console.Clear();
        Console.WriteLine("Новый логин");
        string tmp_login = Console.ReadLine();
        Console.WriteLine("Новый пароль");
        string tmp_password = Console.ReadLine();
        if (!inuser(new user(tmp_login, tmp_password)))
            users[pol] = new user(tmp_login, tmp_password);
        else Console.WriteLine("\nТакой пользователь уже есть");
    }

    static void Search()
    {
        int y = 0;
        bool search = true;
        while (search)
        {
            Console.Clear();
            Console.WriteLine("Искать по");
            Console.WriteLine("  Логину");
            Console.WriteLine("  Паролю");
            Console.SetCursorPosition(0, y + 1);
            Console.WriteLine("->");
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow)
                if (y > 0) y--;
            if (key == ConsoleKey.DownArrow)
                if (y < 1) y++;
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Введите значение для поиска");
                string val = Console.ReadLine();
                Console.WriteLine("Результат поиска:");
                if (y == 0)
                    foreach (var a in users)
                        if (a.Login.Contains(val)) Console.WriteLine(a.Login);
                if (y == 1)
                    foreach (var a in users)
                        if (a.Password.Contains(val)) Console.WriteLine(a.Login);
                Console.ReadKey();
                search = false;
            }
            if (key == ConsoleKey.Escape)
                search = false;
        }
    }
}
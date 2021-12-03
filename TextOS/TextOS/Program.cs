using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextOS
{
    class Program
    {
        public static List<System> system;
        public static List<LoginMenu> loginMenu;
        public static List<Users> users = new List<Users>();
        public static string uAdmin = "admin";
        public static string uPassw = "admin";
        public static List<OSmenu> OS;
        static char[] board = { '0', '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void Main(string[] args)
        {
            SystemMenu();

            Console.ReadKey();
        }

        // System Menu + controls logic
        static void SystemMenu()
        {
            system = new List<System>()
            {
                new System("Windows", () => WriteTemporaryMessage("Windows už máš -,-")),
                new System("Mac", () => WriteTemporaryMessage("Mac?!")),
                new System("TextOS", () => LoginMenu()),
                new System("Exit", () => Environment.Exit(0)),
            };

            int index = 0;

            WriteSystem(system, system[index]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();

                if(keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if(index + 1 < system.Count)
                    {
                        index++;
                        WriteSystem(system, system[index]);
                    }
                }
                if(keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if(index - 1 >= 0)
                    {
                        index--;
                        WriteSystem(system, system[index]);
                    }
                }
                if(keyInfo.Key == ConsoleKey.Enter)
                {
                    system[index].Selected.Invoke();
                    index = 0;
                }
            } while(keyInfo.Key != ConsoleKey.Enter);
        }

        // Output text
        static void WriteTemporaryMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (message.Length / 2)) + "}", message));
        }

        // Menu checks
        static void WriteSystem(List<System> systems, System selectedSystem)
        {
            Console.Clear();

            foreach(System system in systems)
            {
                if(system == selectedSystem)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(system.Name);
            }
        }
        static void WriteLoginMenu(List<LoginMenu> loginMenuTypes, LoginMenu selectedLoginMenu)
        {
            Console.Clear();

            foreach (LoginMenu loginMenu in loginMenuTypes)
            {
                if (loginMenu == selectedLoginMenu)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(loginMenu.Name);
            }
        }
        static void WriteOS(List<OSmenu> OS, OSmenu selectedOS)
        {
            Console.Clear();

            foreach (OSmenu OSmenu in OS)
            {
                if (OSmenu == selectedOS)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(OSmenu.Name);
            }
        }

        // Login/Register
        static void LoginMenu()
        {
            loginMenu = new List<LoginMenu>()
            {
                new LoginMenu("Přihlásit se", () => Login(users)),
                new LoginMenu("Zaregistrovat se", () => Register(users)),
                new LoginMenu("Zpět", () => SystemMenu()),
            };

            int index = 0;

            WriteLoginMenu(loginMenu, loginMenu[index]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < loginMenu.Count)
                    {
                        index++;
                        WriteLoginMenu(loginMenu, loginMenu[index]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteLoginMenu(loginMenu, loginMenu[index]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    loginMenu[index].Selected.Invoke();
                    index = 0;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
        }
        static void Login(List<Users> users)
        {
            char input;
            Console.Clear();
            string username, password;
            Console.Write("Přihlašovací jméno: ");
            username = Console.ReadLine();
            Console.Write("Heslo: ");
            password = Console.ReadLine();

            bool pass = false;

            foreach(var a in users)
            {
                if(a.Username == username && a.Password == password)
                {
                    pass = true;
                }
            }

            if(username == uAdmin && password == uPassw)
            {
                pass = true;
            }

            if (pass)
            {
                Console.WriteLine("\n---------------------------------------------");
                Console.WriteLine("[1] Install OS\t\t[2] Already installed");
                Console.WriteLine("---------------------------------------------");
                input = Console.ReadLine()[0];
                if(input == '1')
                {
                    InsallOS();
                }
                if(input == '2')
                {
                    TextOSMenu();
                }
            }
            else
            {
                Console.WriteLine("Wrong username or password");
                Console.ReadKey();
                SystemMenu();
            }
        }
        static void Register(List<Users> users)
        {
            Console.Clear();
            string username, password;
            Console.Write("Jméno: ");
            username = Console.ReadLine();
            Console.Write("Heslo(min. 5 znaků): ");
            do
            {
                password = Console.ReadLine();
                if(password.ToString().Length < 5)
                {
                    Console.WriteLine("Heslo by mělo mít minimálně 5 znaků!");
                    Console.Write("Heslo");
                }
            } while (password.ToString().Length < 5);

            Users user = new Users(username, password);
            users.Add(user);
            SystemMenu();
        }

        // Installation
        static void InsallOS()
        {
            string boot;

            Console.Clear();

            boot:

            Console.Write("boot> ");
            boot = Console.ReadLine();

            switch (boot)
            {
                case "TextOS text":
                    FakeLoader();
                    break;
                case "Linux text":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tohle není linux");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    goto boot;
                case "help":
                    Console.WriteLine("Napiš: 'TextOS text' ke startu instalačky");
                    Console.WriteLine("Napiš 'Linux text' aby si viděl, že tohle není linux");
                    Console.WriteLine("Napiš: 'cls' abys vyčistil konzoli");
                    goto boot;
                case "cls":
                    Console.Clear();
                    goto boot;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Příkaz neexistuje");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto boot;
            }
        }
        static void FakeLoader()
        {
            int index = 0;
            
        again:

            for(int i = 10; i <= 100; i += 10)
            {
                if(index == 0)
                {
                    if(i == 100)
                    {
                        Console.Write($"\nBITMAP Loading({i}%)[");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("]");
                    }
                    else
                    {
                        Console.Write($"\nBITMAP Loading({i}%)[");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("]");

                    }
                    Thread.Sleep(20);
                }
                else
                {
                    if (i == 100)
                    {
                        Console.Write($"\nOS Settings Loading({i}%)[");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("]");
                    }
                    else
                    {
                        Console.Write($"\nOS Settings Loading({i}%)[");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("]");
                    }
                    Thread.Sleep(20);
                }
            }

            if(index == 0)
            {
                index = 1;
                goto again;
            }

            Console.WriteLine("\nStiskni libovolné tlačítko na klávesnici");
            Console.ReadKey();
            InstallMenu();
        }
        static void InstallMenu()
        {
            char input;

            refresh:
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("[1] Jazyky\t\t[2] Časový pásmo");
            Console.WriteLine("[3] Z čeho instalace\t[4] Verze");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Vyber co chceš 'r' pro obnoveni 'b' pro instalaci 'cislo z menu' pro vstup: ");
            input = Console.ReadLine()[0];

            switch (input)
            {
                case '1':
                    Language();
                    break;
                case '2':
                    Time();
                    break;
                case '3':
                    InstallingWay();
                    break;
                case '4':
                    Version();
                    break;
                case 'r':
                    goto refresh;
                case 'b':
                    FullInstall();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Příkaz neexistuje");
                    Console.ForegroundColor = ConsoleColor.White;
                    InstallMenu();
                    break;
            }
        }
        static void Language()
        {
            char input;
            Console.WriteLine("[1] Čeština\t\t [-]víc nevedeme");
            Console.Write("Vyber jazyk: ");
            input = Console.ReadLine()[0];
            if(input == '1')
            {
                Console.WriteLine("Nastavený jazyk: Čeština");
            }
            else
            {
                Console.WriteLine("Žadný jazyk nebyl nastaven, automaticky se nastaví čeština");
            }

            InstallMenu();
        }
        static void Time()
        {
            char input;
            Console.WriteLine("[1] Praha\t\t [-] Víc jazků zatím není možné nastavit");
            Console.Write("Vyber časový pásmo: ");
            input = Console.ReadLine()[0];
            if (input == '1')
            {
                Console.WriteLine("Nastavený časový pásmo: Praha");
            }
            else
            {
                Console.WriteLine("Žadný časový pásmo nebylo nastaveno, automaticky se nastaví Praha");
            }

            InstallMenu();
        }
        static void InstallingWay()
        {
            char input;
            Console.WriteLine("[1] HDD");
            Console.WriteLine("[2] Network");
            input = Console.ReadLine()[0];
            if(input == '1')
            {
                Console.WriteLine("Instalace proběhne z HDD");
            }
            else if(input == '2')
            {
                Console.WriteLine("Instalace proběhne ze sítě");
            }
            else
            {
                Console.WriteLine("Nic nebylo vybráno, projdeme pc a najdeme nejlepší cestu pro instalaci OS");
                for(int i = 1; i <= 3; i++)
                {
                    Thread.Sleep(120);
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("Searching.");
                            break;
                        case 2:
                            Console.WriteLine("Searching..");
                            break;
                        case 3:
                            Console.WriteLine("Searching...");
                            break;
                    }
                }

                Console.WriteLine("Vybráno HDD!");
            }

            InstallMenu();
        }
        static void Version()
        {
            char input;
            Console.WriteLine("[1] Verze 0.1\t\t[2] Verze 0.2");
            Console.WriteLine("[3] StreamVerze 0.1\t\t[4] LiteVerze 0.1");
            Console.Write("Vyber verzi: ");
            input = Console.ReadLine()[0];

            switch (input)
            {
                case '1':
                    Console.WriteLine("Verze 0.1");
                    break;
                case '2':
                    Console.WriteLine("Verze 0.2");
                    break;
                case '3':
                    Console.WriteLine("StreamVerze 0.1");
                    break;
                case '4':
                    Console.WriteLine("LiteVerze 0.1");
                    break;
                default:
                    Console.WriteLine("Žádná verze nebyla vybrána, vybere se automaticky Verze 0.1");
                    break;
            }

            InstallMenu();
        }
        static void FullInstall()
        {
            for (int i = 10; i <= 300; i += 10)
            {
                Thread.Sleep(200);
                if(i == 300)
                {
                    Console.Write($"\nInstalace TextOS({i}/300 packets)[");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("X");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("]");
                }
                else
                {
                    Console.Write($"\nInstalace TextOS({i}/300 packets)[");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("]");
                }
            }

            Console.WriteLine("\nStiskni libovolnou klávesu pro vstup do OS");
            Console.ReadKey();
            TextOSMenu();
        }

        // Full OS
        static void TextOSMenu()
        {
            Console.Clear();

            OS = new List<OSmenu>()
            {
                new OSmenu("TicTacToe", () => TicTacToe()),
                new OSmenu("GUI(beta)", () => GUI()),
            };

            int index = 0;

            WriteOS(OS, OS[index]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < OS.Count)
                    {
                        index++;
                        WriteOS(OS, OS[index]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteOS(OS, OS[index]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    OS[index].Selected.Invoke();
                    index = 0;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
        }
        static void GUI()
        {
            Demo gui = new Demo();
        }
        static void TicTacToe()
        {
            Console.Clear();
            Board(board);
            Play(board);
        }
        static void Board(char[] board)
        {
            Console.WriteLine("| {0} | {1} | {2} |", board[1], board[2], board[3]);
            Console.WriteLine("-------------");
            Console.WriteLine("| {0} | {1} | {2} |", board[4], board[5], board[6]);
            Console.WriteLine("-------------");
            Console.WriteLine("| {0} | {1} | {2} |", board[7], board[8], board[9]);
        }
        static void Play(char[] board)
        {
            int round = 1;
            int inp;
            bool winner = false;

            while (round <= 9 && !winner)
            {
                Console.WriteLine("Input (0 to quit): ");
                inp = int.Parse(Console.ReadLine());
                if (round % 2 != 0)
                {
                    if(inp == 0)
                    {
                        TextOSMenu();
                    }
                    else if (inp < 1 || inp > 9 || board[inp] != '-')
                    {
                        Console.Clear();
                        Board(board);
                        Console.WriteLine("Wrong input!");
                    }
                    else
                    {
                        Console.Clear();
                        board[inp] = 'O';
                        Board(board);
                        round++;
                    }
                }
                else
                {
                    if (inp == 0)
                    {
                        TextOSMenu();
                    }
                    else if (inp < 1 || inp > 9 || board[inp] != '-')
                    {
                        Console.Clear();
                        Board(board);
                        Console.WriteLine("Wrong input!");
                    }
                    else
                    {
                        Console.Clear();
                        board[inp] = 'X';
                        Board(board);
                        round++;
                    }
                }

                winner = CheckWin(board);
                if (winner)
                {
                    Console.ReadKey();
                    TextOSMenu();
                }
            }
        }
        static bool CheckWin(char[] board)
        {
            //O wins
            if (board[1] == board[2] && board[2] == board[3] && board[1] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[4] == board[5] && board[5] == board[6] && board[4] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[7] == board[8] && board[8] == board[9] && board[7] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[1] == board[4] && board[4] == board[7] && board[1] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[2] == board[5] && board[5] == board[8] && board[2] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[3] == board[6] && board[6] == board[9] && board[3] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[1] == board[5] && board[5] == board[9] && board[1] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }
            else if (board[3] == board[5] && board[5] == board[7] && board[3] == 'O')
            {
                Console.Clear();
                Console.WriteLine("O wins");
                return true;
            }

            //X wins
            if (board[1] == board[2] && board[2] == board[3] && board[1] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[4] == board[5] && board[5] == board[6] && board[4] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[7] == board[8] && board[8] == board[9] && board[7] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[1] == board[4] && board[4] == board[7] && board[1] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[2] == board[5] && board[5] == board[8] && board[2] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[3] == board[6] && board[6] == board[9] && board[3] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[1] == board[5] && board[5] == board[9] && board[1] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }
            else if (board[3] == board[5] && board[5] == board[7] && board[3] == 'X')
            {
                Console.Clear();
                Console.WriteLine("X wins");
                return true;
            }

            return false;
        }
    }
}

using System;

namespace SaperUnix
{
    
    
    
        struct Pole
        {
            public int value;
            public bool hide;
        }

        class Program
        {
            static void MinesSet(Pole[,] field)
            {
                int mines = 0;
                do
                {
                    Random random = new Random();
                    int x = random.Next(0, 10);
                    int y = random.Next(0, 10);
                    if (field[x, y].value < 9)
                    {
                        field[x, y].value = 9;

                        if (x < 9 && y < 9) field[x + 1, y + 1].value++;

                        if (x > 0 && y > 0) field[x - 1, y - 1].value++;
                        if (x < 9 && y > 0) field[x + 1, y - 1].value++;

                        if (x > 0 && y < 9) field[x - 1, y + 1].value++;

                        if (x < 9) field[x + 1, y].value++;
                        if (y < 9) field[x, y + 1].value++;

                        if (x > 0) field[x - 1, y].value++;
                        if (y > 0) field[x, y - 1].value++;

                        mines++;
                    }
                } while (mines < 10);
            }

            static void ZeroSet(Pole[,] field)
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        field[i, j].value = 0;
                        field[i, j].hide = false;
                    }
            }

            static void Check9(Pole[,] field)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (field[i, j].value > 9)
                            field[i, j].value = 9;
                    }
                }
            }

            static void Show(Pole[,] field)
            {
            Console.Write("  ");
                for (int i = 0; i < 10; i++)
                Console.Write(i+" ");
                Console.Write("\n");

                for (int i = 0; i < 10; i++)
                {
                Console.Write(i + " ");
                    for (int j = 0; j < 10; j++)
                    {
                        if (field[i, j].hide)
                            Console.Write(field[i, j].value+" ");
                        else
                            Console.Write("# ");
                    }
                    Console.Write("\n");
                }
            }

            static void change(Pole[,] field, int x, int y)
            {
                field[x, y].hide = true;
            }

            static void CheckWin(Pole[,] field)
            {
                int count = 0, hidden = 0;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (!field[i, j].hide)
                            hidden++;
                        if (!field[i, j].hide && field[i, j].value == 9)
                            count++;
                    }
                }

                if (count == 10 && hidden == count)
                    Console.WriteLine("Gratulacje wygrana!!");
                else
                    Console.WriteLine("Przegrana!!");


            }

            static bool CheckPlay(Pole[,] field, int w, int q)
            {
                if (field[w, q].value != 9)
                    return true;
                else
                {
                    Console.WriteLine("Przegrales, oto rozklad bomb");
                    return false;
                }

            }
            static int InputX(string message)
            {
                int x = 0;
                do
                {
                    try
                    {  
                            Console.Write(message);
                            x = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Bledne dane jeszcze raz");
                        return 5000;                     
                    }

                    if (x<0)
                    {
                    Console.WriteLine("błedne dane wartosc powinna byc wieksza lub rowna od 0");
                        return 5000;
                    }
                } while (x > 9);

                return x;
            }
            static bool Moves(Pole[,] field)
            {
                int x = 0, y = 0;
                do
                {
                    Console.Write("Koniec czy jeszcze nie? [k/n]\t");
                    String end = Console.ReadLine();

                    if (end.Equals("k"))
                    {
                        CheckWin(field);
                        return false;
                    }

                    Console.WriteLine("------------------------------------");
                    do
                    {
                        x = InputX("x : ");
                    } while (x == 5000);

                do
                {
                    y = InputX("y : ");
                } while (x == 5000);

                change(field, x, y);
                    Console.Clear();
                    Show(field);

                } while (CheckPlay(field, x, y));

                return CheckPlay(field, x, y);
            }
            static void Saper(Pole[,] field)
            {
                bool game = true;
                do
                {
                    ZeroSet(field);
                    MinesSet(field);
                    Check9(field);
                    Show(field);
                    Moves(field);



                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Console.Write(field[i, j].value);
                            Console.Write("\t");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("Gramy jeszcze raz czy koniec? [g/k]");
                    string Again = Console.ReadLine();
                    if (Again.Equals("k"))
                        game = false;

                } while (game);

            }

            static void Main(string[] args)
            {
                Pole[,] field = new Pole[10, 10];

                Saper(field);
                Console.ReadKey();
            }
        }
    

}

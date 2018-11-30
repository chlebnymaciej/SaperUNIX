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
            static void MinesSet(Pole[,] field, int xBound, int yBound)
            {
                int mines = 0;
                do
                {
                    Random random = new Random();
                    int x = random.Next(0, xBound);
                    int y = random.Next(0, yBound);
                    if (field[x, y].value < 9)
                    {
                        field[x, y].value = 9;

                        if (x < (xBound-1) && y < (yBound-1)) field[x + 1, y + 1].value++;

                        if (x > 0 && y > 0) field[x - 1, y - 1].value++;
                        if (x < (xBound-1) && y > 0) field[x + 1, y - 1].value++;

                        if (x > 0 && y < (yBound-1)) field[x - 1, y + 1].value++;

                        if (x < (xBound-1)) field[x + 1, y].value++;
                        if (y < (yBound-1)) field[x, y + 1].value++;

                        if (x > 0) field[x - 1, y].value++;
                        if (y > 0) field[x, y - 1].value++;

                        mines++;
                    }
                } while (mines < 10);
            }

            static void ZeroSet(Pole[,] field, int xBound, int yBound)
            {
                for (int i = 0; i < xBound; i++)
                    for (int j = 0; j < yBound; j++)
                    {
                        field[i, j].value = 0;
                        field[i, j].hide = false;
                    }
            }

            static void Check9(Pole[,] field, int xBound, int yBound)
            {
                for (int i = 0; i < xBound; i++)
                {
                    for (int j = 0; j < yBound; j++)
                    {
                        if (field[i, j].value > 9)
                            field[i, j].value = 9;
                    }
                }
            }

            static void Show(Pole[,] field, int xBound, int yBound)
            {
            Console.Write("   ");
                for (int i = 0; i < yBound; i++)
                if(i<10)Console.Write(i+"  ");
                else Console.Write(i + " ");
            Console.Write("\n");

                for (int i = 0; i < xBound; i++)
                {
                    if(i<10) Console.Write(i + "  ");
                    else Console.Write(i + " ");

                    for (int j = 0; j < yBound; j++)
                        {
                            if (field[i, j].hide)
                                Console.Write(field[i, j].value+"  ");
                            else
                                Console.Write("#  ");
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
            static int InputX(string message, int X)
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
                } while (x > X);

                return x;
            }
            static bool Moves(Pole[,] field, int xBound, int yBound)
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
                        x = InputX("x : ", xBound);
                    } while (x == 5000);

                do
                {
                    y = InputX("y : ", yBound);
                } while (x == 5000);

                change(field, x, y);
                    Console.Clear();
                    Show(field, xBound, yBound);

                } while (CheckPlay(field, x, y));

                return CheckPlay(field, x, y);
            }
            static void Saper(/*Pole[,] field,*/ int xBound, int yBound)
            {
                
                bool game = true;
                
                    InitGame(ref xBound, ref yBound);
                    Pole[,] field = new Pole[xBound, yBound];
                    do
                    {
                        ZeroSet(field, xBound,yBound);
                        MinesSet(field, xBound, yBound);
                        Check9(field, xBound, yBound);
                        Show(field, xBound, yBound);
                        Moves(field, xBound, yBound);
                        
                        for (int i = 0; i < xBound; i++)
                        {
                            for (int j = 0; j < yBound; j++)
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
            static void setBounds(ref int x, ref int y)
            {
                Console.WriteLine("Jakie wymiary chcesz ustawic musza byc mniejsz od 50");

                do
                {
                    x = InputX("x : ", 50);
                } while (x == 5000);

                do
                {
                    y = InputX("y : ", 50);
                } while (x == 5000);
            }

            static void InitGame(ref int x, ref int y)
            {
                Console.WriteLine("czy chcesz zmienic wymiary planszy ? [y/n]");
                string Bounds = Console.ReadLine();

                if (Bounds.Equals("y"))
                    setBounds(ref x,ref y);

            }

            static void Main(string[] args)
            {
                int x = 10;
                int y = 10;
                //InitGame(ref x,ref y);
                //Pole[,] field = new Pole[x, y];

                Saper(/*field,*/ x, y);
                Console.ReadKey();
            }
        }
    

}

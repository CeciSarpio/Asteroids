using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asteroids.Logic
{
   public class Logic
    {
        public static void Play()
        {
            SetConsole.Default();
            var field = new char[Console.WindowHeight, Console.WindowWidth];
            var fieldHeight = Console.WindowHeight;
            var fieldWidth = Console.WindowWidth;
            GenerateField(field, fieldHeight, fieldWidth);
            AddShip(field,fieldHeight,fieldWidth);
            PrintField(field,fieldHeight,fieldWidth);
            GenerateAsteroids(field, fieldHeight, fieldWidth);
            //MoveShip(field, fieldHeight, fieldWidth);

            SynchronisingMovements(field, fieldHeight, fieldWidth);



        }

        private static void SynchronisingMovements(char[,]field, int fieldHeight, int fieldWidth)
        {
           var asteroidsList= GenerateAsteroids(field, fieldHeight, fieldWidth);
            var oldPosition = fieldWidth / 2;
            var startTime = DateTime.Now;
            

            while (true)
            {

                var currtime = DateTime.Now;
                TimeSpan diff = currtime - startTime;
               
                
                if (diff.TotalMilliseconds>=1000)
                {
                    DropAsteroids(asteroidsList, field);
                    startTime=DateTime.Now;
                    Console.Clear();

                    PrintField(field, fieldHeight, fieldWidth);

                }

                if (Console.KeyAvailable)
                {
                    
                
                var move = Console.ReadKey().Key.ToString();
                    switch (move)
                    {
                        case "LeftArrow":
                            MoveLeft(field, fieldHeight, ref oldPosition);
                            break;
                        case "RightArrow":
                            MoveRight(field, fieldHeight, ref oldPosition);
                            break;
                    }


                    Console.Clear();

                    PrintField(field, fieldHeight, fieldWidth);
                   

                }


            }

        }

        private static List<Asteroid> GenerateAsteroids(char[,] field,int fieldHeight,int fieldWidth)
        {
            var asteroidsPositions = new Queue<int>();
            var randompositions = new Random();
            int positions = randompositions.Next(0, fieldWidth-1);
            for (int i = 0; i < 2; i++)
            {
                positions = randompositions.Next(0, fieldWidth-1);
                asteroidsPositions.Enqueue(positions);
            }
            var asteroids = new List<Asteroid>();
            while (asteroidsPositions.Count != 0)
            {
                var asteroidStartPosition = asteroidsPositions.Dequeue();
                var asteroid = new Asteroid() { row = 0, col = asteroidStartPosition };
                asteroids.Add(asteroid);

            }
            return asteroids;

        }

        private static void DropAsteroids(List<Asteroid> asteroids,char[,] field)
        {
           
            foreach (var a in asteroids)
            {
                a.row++;
                field[a.row - 1, a.col - 1] = ' ';
                if (a.row < Console.WindowHeight - 1)
                {

                    field[a.row, a.col - 1] = '*';
                }
            }
        }

        //private static void MoveShip(char[,] field, int fieldHeight, int fieldWidth)
        //{
        //    int oldPosition = fieldWidth / 2;


        //    while (true)
        //    {
        //        PrintField(field, fieldHeight, fieldWidth);
        //        var move = Console.ReadKey().Key.ToString();
        //        switch (move)
        //        {
        //            case "LeftArrow":
        //                MoveLeft(field, fieldHeight,ref  oldPosition); break;
        //            case "RightArrow": MoveRight(field, fieldHeight,ref oldPosition);
        //                break;

        //        }
            

        //    Console.Clear();
        //    }
        

        //}

        private static void MoveRight(char[,] field, int fieldHeight, ref int oldPosition)
        {
            if (oldPosition < Console.WindowWidth - 1)
            {
                field[fieldHeight - 1, oldPosition + 1] = '$';
                field[fieldHeight - 1, oldPosition] = ' ';
                oldPosition++;
            }
        }

        private static void MoveLeft(char[,] field, int fieldHeight, ref int oldPosition)
        {
            if (oldPosition >0)
            {
                field[fieldHeight - 1, oldPosition - 1] = '$';
                field[fieldHeight - 1, oldPosition] = ' ';
                oldPosition--;
                
            }
           
        }

        private static void PrintField(char[,] field, int fieldHeight, int fieldWidth)
        {
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    Console.Write(field[i,j]);
                }
                Console.WriteLine();
            }
        }

        private static void AddShip(char[,] field, int fieldHeight, int fieldWidth)
        {
            field[fieldHeight-1, fieldWidth / 2] = '$';
        }

        private static void GenerateField(char[,] field, int fieldHeight, int fieldWidth)
        {
           
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    field[i, j] = ' ';

                }
            }
          
        }
    }

  }


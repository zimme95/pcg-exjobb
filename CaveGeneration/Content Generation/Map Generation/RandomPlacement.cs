﻿using CaveGeneration.Content_Generation.Map_Cleanup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveGeneration.Content_Generation.Map_Generation
{
    class RandomPlacement : MapGenerator
    {
        public int randomFillPercent;

        public RandomPlacement(int width, int height, int randomFillPercent) : base(width, height)
        {
            this.randomFillPercent = randomFillPercent;
        }

        public override void Start(string seed)
        {
            if (seed.Equals(""))
                UseRandomSeed = true;

            Seed = seed;
            GenerateMap();
        }

        private void GenerateMap()
        {
            map = new int[Width, Height];
            RandomFillMap();

            CellularAutomata ca = new CellularAutomata(Width, Height, map);

            for (int i = 0; i < 3; i++)
            {
                ca.SmoothMap();
            }
        }

        private void FillMap()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    map[x, y] = 1;
                }
            }
        }

        private void RandomFillMap()
        {
            if (UseRandomSeed)
            {
                Seed = DateTime.Now.ToString();
            }

            Random pseudoRandom = new Random(Seed.GetHashCode());

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                    }
                }
            }
        }

    }
}
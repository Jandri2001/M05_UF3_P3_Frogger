using System;
using System.Collections.Generic;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                FroggerGame game = new FroggerGame();
                Utils.GAME_STATE result = game.StartGame();

                Console.Clear();
                if (result == Utils.GAME_STATE.WIN)
                {
                    Console.WriteLine("¡Has ganado!");
                }
                else if (result == Utils.GAME_STATE.LOOSE)
                {
                    Console.WriteLine("Has perdido...");
                }

                Console.WriteLine("¿Quieres jugar otra vez? (s/n)");
                char input = Console.ReadKey().KeyChar;
                if (input != 's' && input != 'S')
                {
                    break;
                }
            }
        }
    }

    public class FroggerGame
    {
        private Player player;
        private List<Lane> lanes;

        public FroggerGame()
        {
            player = new Player();
            lanes = new List<Lane>();

            for (int i = 0; i < Utils.MAP_HEIGHT; i++)
            {
                if (i % 2 == 0) // Car lanes
                {
                    lanes.Add(new Lane(i, true, ConsoleColor.Black, true, false, 0.2f, Utils.charCars, new List<ConsoleColor>(Utils.colorsCars)));
                }
                else if (i % 2 != 0) // Log lanes
                {
                    lanes.Add(new Lane(i, false, ConsoleColor.Blue, false, true, 0.2f, Utils.charLogs, new List<ConsoleColor>(Utils.colorsLogs)));
                }
            }
        }

        public Utils.GAME_STATE StartGame()
        {
            while (true)
            {
                Draw();
                Utils.GAME_STATE gameState = Update(Utils.Input());

                TimeManager.NextFrame();

                if (gameState == Utils.GAME_STATE.WIN || gameState == Utils.GAME_STATE.LOOSE)
                {
                    return gameState;
                }
            }
        }

        public void Draw()
        {
            Console.Clear();
            foreach (var lane in lanes)
            {
                lane.Draw();
            }
            player.Draw(lanes);
        }

        public Utils.GAME_STATE Update(Vector2Int input)
        {
            foreach (var lane in lanes)
            {
                lane.Update();
            }
            return player.Update(input, lanes);
        }
    }
}
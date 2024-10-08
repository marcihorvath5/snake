﻿using System.Reflection.Metadata;

namespace Snake.Models
{
    public static class ArenaSettings
    {
        /// <summary>
        /// A játéjkban egyszerre látható ételek száma
        /// </summary>
        public static int MealsCountForStart { get; } = 3;

        /// <summary>
        /// A kígyó hoszza a játék kezdetén
        /// </summary>
        public static int SnakeCountForStart { get; } = 4;

        /// <summary>
        /// A játék maximum x koordinátája
        /// </summary>
        public static int maxX { get; } = 20;

        /// <summary>
        /// A játék maximum y koordinátája
        /// </summary>
        public static int maxY { get; } = 20;

        /// <summary>
        /// Megmondja hogy a kígyó fejében ülünk vagy kívültről irányítjuk a játékot?
        /// </summary>
        public static bool IsSittingInTheHeadOfSnake { get; } = false;
    }
}
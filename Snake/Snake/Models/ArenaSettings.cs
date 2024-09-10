namespace Snake.Models
{
    public class ArenaSettings
    {
        /// <summary>
        /// A játéjkban egyszerre látható ételek száma
        /// </summary>
        public static int MealsCountForStart { get; } = 8;

        /// <summary>
        /// A játék maximum x koordinátája
        /// </summary>
        public static int maxX { get; } = 20;

        /// <summary>
        /// A játék maximum y koordinátája
        /// </summary>
        public static int maxY { get; } = 20;
    }
}
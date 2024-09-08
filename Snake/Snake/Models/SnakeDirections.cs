namespace Snake.Models
{
    /// <summary>
    /// A kígyó fejének lehetséges irányai
    /// </summary>
    public enum SnakeDirections
    {
        /// <summary>
        /// alapértelmezett irány ha nem adunk meg semmit
        /// </summary>
        None,

        /// <summary>
        /// Balra megy a kígyó
        /// </summary>
        Left,
        /// <summary>
        /// Jobbra megy a kígyó
        /// </summary>
        Right,
        /// <summary>
        /// Fel megy a kígyó
        /// </summary>
        Up,
        /// <summary>
        /// Le megy a kígyó
        /// </summary>
        Down
    }
}
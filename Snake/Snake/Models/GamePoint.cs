namespace Snake.Models
{
    /// <summary>
    /// A játétáblán egy (étel vagy kígyó) pontot jelképező egység
    /// </summary>
    public class GamePoint
    {

        public GamePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }


    }
}
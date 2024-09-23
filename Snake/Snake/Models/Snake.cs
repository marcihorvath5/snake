using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// A kígyót leíró osztály
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Mutatja hogy a kígyó feje éppen merre áll
        /// </summary>
        public SnakeDirections Directon { get; set; }

        /// <summary>
        /// A kígyó pontjait tartalmazó lista
        /// 
        /// ha lehet, ne használjunk null értéket mindig inicializáljuk ha mást nem akkor üres listával
        /// </summary>
        public List<GamePoint> Gamepoints { get; set; } = new List<GamePoint>();

        /// <summary>
        /// A kígyó hossza csak lekérdezhető így nincs settere
        /// és saját kóddal a gettert implementáltuk
        /// </summary>
        public int Length 
        {
            get 
            {
                return Gamepoints.Count;    
            }  
             
                // GamePoints?.Count: ha a gamepoints értéke null, akkor a végeredmény null, ha nem akkor veszi a Count propertyjét
                // a ?? b: ha a értéke null, akkor b a visszatérés ha nem akkor a aa visszatérés
                //return Gamepoints?.Count ?? 0;             
        }

        /// <summary>
        /// Megevett ételek száma
        /// </summary>
        public int EatenMealsCount { get; private set; } = 0;

        /// <summary>
        /// Pontszám
        /// </summary>
        public int Points { get; private set; } = 0;

        /// <summary>
        /// Vissza adja a kígyó fejét
        /// </summary>
        public GamePoint Head 
        { 
            get
            {
                return Gamepoints[0];
            }
        }

        public GamePoint Neck 
        { 
            get
            {
                return Gamepoints[1];
            }
        }

        /// <summary>
        /// a kígyó megevett egy ételt
        /// </summary>
        /// <param name="meal">a megevett étel</param>
        public void Eat(Meal meal)
        {
            ++EatenMealsCount;
            Points += meal.Points;
        }

        /// Property tulajdonságai (fieldhez képest):
        /// külön lehet szabályozni hogy írható és olvasható-e?
        /// get: olvasható
        /// set: írható
        /// Az írhatóság és olvashatóság kívülről történő láthatósága is szabályozható
        /// private get: csak osztályon belül használható
        /// private set: csak osztályon belül használható
        /// 
        /// Működése implemenálható: saját kóddal adhatom meg hogy mit tegyen
    }
}

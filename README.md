
﻿# Snake projekt

## Rövid leírás
A Snake játék lényege, hogy van egy játéktér, ami téglalap alakú, erről nem lehet lemenni. Van rajta egy kígyó
aminek a fejében ülünk és a feje irányításával mozgatjuk a kígyót. A cél: a játéktéren felbukkanó ételeket megegenni.
minden evés alkalmával a kígyónk hossza megnő, és minél tovább játszunk, annál hosszabb a kígyónk. Így a feladat 
egyre nehezebb: elkerülni a saját kígyónkat, és nem beleütközni a falba.

```
+----------------------------------------------------------------------------------+
|                                                                                  |
|                                                                                  |
|                                                                                  |
|          a                                                                       |
|                                                            s                     |
|                                                                                  |
|                                                                                  |
|                                                                                  |
|                                                                                  |
|                                                                                  |
|            +--------->                       a                                   |
|            |                                                                     |
|            |                                                                     |
|            +                                                                     |
|                                                                                  |
|                                                                a                 |
|                                                                                  |
|                              v                                                   |
|                                                                                  |
|                                                                                  |
|                                                                                  |
+----------------------------------------------------------------------------------+
```
## A játékunk kinézete

```
                                                                X  <--------+ Kilépés
+-----------------------------------------+-------------------+
|                                         |                   |
|                                         | Pontszám: 3120    |
|                                         |                   |
|                                         | Megevett étel: 10 |
|                                         |                   |
|                                         | Kígyó hossza: 5   |
|                                         |                   |
|                                         | Játékidő: 03:56   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |                   |
|                                         |  +-------+------+ |
|                                         |  | Start | Stop | |
|                                         |  +-------+------+ |
+-----------------------------------------+-------------------+
```

## Játéktér kialakítása XAML-ben
egy 20x20-as hálózatot húzunk ki, ami valójában 22x22, ennek az az értelme, hogy az első és az utolsó sor (és oszlop) valójában a fal, ha mozgatjuk a kígyót, akkor nem kell azzal törődnünk, hogy mozoghat-e, mert egyet még tudunk lépni a látható tábláról: ekkor a falba mozogtunk, de ehhez nem kell esetszétválasztás.

A megjelenítéskor a [FontAwesome ikonkészletet](http://fontawesome.io/icons/) használjuk, ehhez a megfelelő nuget csomagot telepítjük ([FontAwesome.WPF](https://www.nuget.org/packages/FontAwesome.WPF/))

A telepítés három dolgot végez el:
- beírja magát a packages.config könyvtárba
- letölti a csomagot a Solution mappa Packages könyvtárjába
- létrehoz egy hivatkozást a projektünkbe ami a megfelelő dll-t a packages mappából meghivatkozza.

a középső 20x20-as részt feltöltjük ikonokkal

ahhoz, hogy az ikonokat az XAML-ben használni tudjuk, kell a névtér hivatkozás a definícióba. XAML névtérhovatkozás kell: ami valójában XML névtér hivatkozás: xmlns, vagyis xml name space.

Ezt legegyszerűbb lelopni a nuget csomagnak a doksijából ([a githubról](https://github.com/charri/Font-Awesome-WPF/blob/master/README-WPF.md))


## Tennivalók (2017.11.14)
- Hibajavítás, 
  - az utolsó sorba és oszlopba nem teszünk semmit, 
  - illetve, az utolsó sorba és az utolsó oszlopba nem tudom a kígyót kormányozni
  - egyre több étel kerül ki a táblára
    - Egy hibabejelentés kötelező részei
      - (lehetőleg a legrövidebb) lépéssorozat, amivel a hibát reprodukálni lehet
      - a hibajelenség leírása
      - mi lenne az elvárt működése a programnak
    - Ebben az esetben
      - egyelőre nem sikerült reprodukálni a hibát, azonban, ha a kígyó új feje helyére vagy a régi farka 
        helyére generálódik az új étel, akkor ott lehet probléma
      - következő tipp: minél hosszabb a kígyó, annál tovább tart a feldolgozás, illetve, a 100 milisecundum
        lehet, hogy nem elég, így többször is meghívódik a megjelenítő függvény úgy, hogy az előző nem ért véget
      - nem sikerült a megoldás, így az első lépés még mindig a reprodukálni képes lépéssorozat meghatározása
      - elképzelhető, hogy a hiba okát most megszüntettük: nem töröltük az ételt az ételek listájából csak eltüntettük

- az ételeket és a kígyót "objektumosítani"
  - pontok és megevett ételek számítása
  - pontszámítás az étel pontok alapján

## Objektum leszármaztatása

```
Osztály leszármaztatásának jelölése
+----------+                        +-------------+
|   Meal   |                        |  GamePoint  |
|----------|                        |-------------|
|          |                        |             |
| Points   | +--------------------> | X           |
|          |                        | Y           |
|          |                        |             |
|          |                        |             |
+----------+                        +-------------+

+-----------------+
|   Meal          |
|-----------------|
|                 |
|  Points         |
|                 | +--------------+
|                 | | GamePoint    |
|                 | |--------------|
|  X +--------------> X            |
|  Y +--------------> Y            |
+-----------------+ +--------------+
```
Amikor leszármaztatott osztályból példányosítunk (például var akarmi = new Meal(); ) akkor MINDIG példányosodik az ősosztályból is egy példány (base). Így az új tulajdonságokat (Meal.Points) a leszármaztatott osztály implementálja, az eredeti tulajdonságokat (GamePoint.X és Y) pedig ez az "árnyék" ősosztálypéldány.

HA az ősosztálynak van saját nem triviális (paraméter nélküli) konstruktora

pl.:

```csharp
public GamePoint(int x, int y)
{
    X = x;
    Y = y;
}
```

AKKOR a leszármaztatott osztálnak ezt a konstruktort meg kell hívnia valahogy:

```csharp
public Meal(int x, int y) //paraméterek átvétele az ősosztály konstruktorához
        : base(x, y)  //ősosztály konstruktor meghívása
{ }
```

## Extra alkalom todo
- [X] Beülni a kígyó fejébe
- [X] Static magyarázat
- [X] Game Over
- [ ] Canvas kezelés (grafikus megoldás)


```

                                                  +--------------------+
                                                  |Osztálypéldány      |
                                                  |--------------------|
                                                  | X, Y               |
+--------------------------+                      |                    |
|  Osztálydefiníció        |+-------------------->| Függvény()         |
|--------------------------|                      |                    |
|  (Tervrajz)              |                      |                    |
|                          |                      +--------------------+
|                          |
|                          |
|  X, Y  (Terv) <-----------+ Példányszintű változó (Instance)
|                          |                      +--------------------+
|  Függvény (Terv) <--------+ és függvény         |Osztálypéldány      |
|                          |                      |--------------------|
|                          |+-------------------->| X, Y               |
|  Szín (osztályszintű)    |                      |                    |
|         STATIC           |                      | Függvény()         |
+--------------------------+                      |                    |
                                                  |                    |
                                                  +--------------------+
```

```
                                      +---------------------------+
                                      |  CanvasArena              |
                                      |---------------------------|
                                      |                           |
                                      |  +--------------------+   |
+----------+                          |  | Children           |   |
|Meals     |                          |  |--------------------|   |
|----------|                          |  |                    |   |
|          |                          |  | Snake-0            |   |
|          |                          |  | Snake-1            |   |
|          |                          |  | Snake-2            |   |
|          |                          |  | ...                |   |
|          |                          |  | Snake-7            |   |
+----------+                          |  | Meal-0             |   |
                                      |  | Meal-1             |   |
                                      |  | Meal-2             |   |
+----------+                          |  | ...                |   |
|Snake     |                          |  | Meal-8             |   |
|----------|                          |  | Snake-head1        |   |
|          |                          |  | Snake-head2        |   |
|          |                          |  |                    |   |
|          |                          |  +--------------------+   |
|          |                          |                           |
|          |                          +---------------------------+
|          |
+----------+
```

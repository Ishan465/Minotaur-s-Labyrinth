namespace A6MinotaurLabyrinth
{
    /// <summary>
    /// LabyrinthCreator class that contains methods to create labyrinth
    /// </summary>
    public static class LabyrinthCreator
	{
		static readonly (int rows, int cols) smallCoords = (4, 4); //small
		static readonly (int rows, int cols) medCoords = (6, 6); // medium
		static readonly (int rows, int cols) largeCoords = (8, 8); // large room

        // Creates a small 4x4 game.
        /// <summary>
        /// CreateSmallGame method that creates small game
        /// </summary>
        /// <returns> creategame method with parameter map and start</returns>
        public static LabyrinthGame CreateSmallGame()
		{
			(Map map, Location start) = InitializeMap(Size.Small);
			return CreateGame(map, start);
		}

        // Creates a medium 6x6 game.
        /// <summary>
        /// CreateSmallGame method that creates medium game
        /// </summary>
        /// <returns> creategame method with parameter map and start</returns>
        public static LabyrinthGame CreateMediumGame()
		{
			(Map map, Location start) = InitializeMap(Size.Medium);
			return CreateGame(map, start);
		}

        // Creates a large 8x8 game.
        /// <summary>
        /// CreateSmallGame method that creates small large
        /// </summary>
        /// <returns> creategame method with parameter map and start</returns>
        public static LabyrinthGame CreateLargeGame()
		{
			(Map map, Location start) = InitializeMap(Size.Large);
			return CreateGame(map, start);
		}

        /// <summary>
        /// Helper function that initializes the map size and all the map locations
        /// </summary>
        /// <param name="mapSize"></param>
        /// <returns>Returns the initialized map and the entrance location (start) so we can  set the player starting location accordingly.</returns>
        private static (Map, Location) InitializeMap(Size mapSize)
		{
			Map map = mapSize switch
			{
				Size.Small => new Map(smallCoords.rows, smallCoords.cols),
				Size.Medium => new Map(medCoords.rows, medCoords.cols),
				Size.Large => new Map(largeCoords.rows, largeCoords.cols),
			};
			Location start = RandomizeMap(map);
			return (map, start);
		}


        /// <summary>
        /// Creates a map with randomly placed and non-overlapping features. The Entrance will be located in a random position along the edge of the map
        /// The Sword will be placed randomly in the map but it will not be adjacent to the Entrance
        /// Traps & Monsters will be placed randomly
        /// </summary>
        /// <param name="map"></param>
        /// <returns>start location</returns>
        private static Location RandomizeMap(Map map)
		{
            //TODO: (A6) create an instance of the LabryinthCreatorRng class to get random locations

            // created an instance of LabryinthCreatorRng class with parameter map to get random locations
            LabryinthCreatorRng labryinth = new LabryinthCreatorRng(map);

			Location start = labryinth.RandomStartLocation();	//TODO: (A6) Randomize the start location of the entrance
			map.SetRoomTypeAtLocation(start, RoomType.Entrance);

			Location swordStart = labryinth.RandomSwordLocation();	//TODO: (A6) Randomize the location of the sword
			map.SetRoomTypeAtLocation(swordStart, RoomType.Sword);

            //TODO: (A7) Randomize the location of the pit rooms
            //add pit room(s) to the map - consider scaling the number of obstacle rooms to the game size.

            if (map.Rows == 4)
            {
                Location pit1 = labryinth.RandomPitRoomLocation(); 
                map.SetRoomTypeAtLocation(pit1, RoomType.Pit);
            }
            else if (map.Rows == 6) 
            {
                Location pit1 = labryinth.RandomPitRoomLocation();
                map.SetRoomTypeAtLocation(pit1, RoomType.Pit);
                Location pit2 = labryinth.RandomPitRoomLocation();
                map.SetRoomTypeAtLocation(pit2, RoomType.Pit);

                Location oneUp1 = labryinth.RandomOneUpLocation();
                map.SetRoomTypeAtLocation(oneUp1, RoomType.OneUp);
            }
            else if (map.Rows == 8)
            {
                Location pit1 = labryinth.RandomPitRoomLocation();
                map.SetRoomTypeAtLocation(pit1, RoomType.Pit);
                Location pit2 = labryinth.RandomPitRoomLocation();
                map.SetRoomTypeAtLocation(pit2, RoomType.Pit);
                Location pit3 = labryinth.RandomPitRoomLocation();
                map.SetRoomTypeAtLocation(pit3, RoomType.Pit);

                Location oneUp1 = labryinth.RandomOneUpLocation();
                map.SetRoomTypeAtLocation(oneUp1, RoomType.OneUp);
                Location oneUp2 = labryinth.RandomOneUpLocation();
                map.SetRoomTypeAtLocation(oneUp2, RoomType.OneUp);
            }
            return start;

		}
		/// <summary>
		/// inititilizes player
		/// </summary>
		/// <param name="start"></param>
		/// <returns> returns a player object with parameter start</returns>
		private static Player InitializePlayer(Location start)
		{
			return new Player(start);
		}

		/// <summary>
		/// initilizes monster
		/// </summary>
		/// <param name="map"></param>
		/// <returns> returns an array of monsters created</returns>
		private static Monster[] InitializeMonsters(Map map)
		{
            //TODO: (A8) Initalize an array of monsters and fill it with one Minotaur instance (and other monster instances).

            //Consider scaling the number of your monster type to the size of the game. However, there should only ever be one minotaur in the labyrinth.
            //Set every new monster's loaction to be random (hint: use the LabryinthCreatorRng class)

            // Ensure monster locations do not overlap existing locations on the map

            LabryinthCreatorRng labryinth = new LabryinthCreatorRng(map);

            Minotaur minotaur = new Minotaur(labryinth.RandomMonsterLocation());
            Bowser bowser = new Bowser(labryinth.RandomMonsterLocation());
            Monster[] monsters = { minotaur, bowser };

            if (minotaur.Location != bowser.Location)
                return monsters;
            else 
                return InitializeMonsters(map);
		}
		/// <summary>
		/// create game method that creates game according to conditions of player and monster array
		/// </summary>
		/// <param name="map"></param>
		/// <param name="start"></param>
		/// <returns>returns a labyrinth game</returns>
		private static LabyrinthGame CreateGame(Map map, Location start)
		{
			Player player = InitializePlayer(start);
			Monster[] monsters = InitializeMonsters(map);
			return new LabyrinthGame(map, player, monsters);
		}
		/// <summary>
		/// enum that contains the sizes of map
		/// </summary>
		private enum Size { Small, Medium, Large };
	}

    //TODO: (A6) Complete this new class that will be used to get random room locations in the labyrinth that are suited for the entrance and the sword
    /// <summary>
    /// LabryinthCreatorRng class that helps for creation of labyrinth
    /// </summary>
    public class LabryinthCreatorRng
	{
		//TODO: (A6) data fields
        private Location Location { get; set; }
		private Map Map { get; }
		private int Mapsize { get; } // mapsize is either rows or colums as both are same

        //TODO: (A6) constuctor
		/// <summary>
		/// default constructor that seps value of map and mapsize
		/// </summary>
		/// <param name="map"></param>
        public LabryinthCreatorRng(Map map) 
		{
			Map = map;
			Mapsize = map.Rows;
		}

        //TODO: (A6) method that returns a random location suited to the entrance
        //the entrance should always be located on the edge of the 2D grid.
        /// <summary>
        /// RandomStartLocation method to generate random start location
        /// </summary>
        /// <returns>returns random location for entrance</returns>
        public Location RandomStartLocation()
        {
            Random locationrow = new Random(); //randomrow variable
            Random locationcol = new Random(); //randomcol variable

            int row = locationrow.Next(Mapsize); // range is from 0 to mapsizw
			int col = locationcol.Next(Mapsize);
			Location = new Location(row, col); // location is rows and cols

			if ((row == 0) || (col == 0) || (row == Mapsize) || (col == Mapsize)) // if located on edge ie. the conditions mentioned
				return Location; // return location
			else
				return RandomStartLocation(); // recursion
        }

        //TODO: (A6) method that returns a random location suited to the sword
        //sword location should not be adjacent or overlapping the start location
        /// <summary>
        /// RandomSwordLocation method to generate random sword location
        /// </summary>
        /// <returns>returns random location for sword</returns>
        public Location RandomSwordLocation() // same logic as start location
        {
            Random locationrow = new Random();
            Random locationcol = new Random();

            int row = locationrow.Next(Mapsize);
            int col = locationcol.Next(Mapsize);
            Location = new Location(row, col);

			if (!Map.HasNeighborWithType(Location, RoomType.Entrance)) // used has neighbourwithtype method to detect entrance
                return Location;
			else
				return RandomSwordLocation(); // if there is neighbour then recursion to get new location
        }
        
        //TODO: (A7) Create a method that returns a random location suited to the to a Pit room
        //pit room locatoins should only be added to locations that are empty (RoomType.Normal)
        /// <summary>
        /// RandomPitLocation method to generate random pit room location
        /// </summary>
        /// <returns>returns random location for pit room</returns>
        public Location RandomPitRoomLocation() // same logic as start location
        {
            Random locationrow = new Random();
            Random locationcol = new Random();

            int row = locationrow.Next(Mapsize);
            int col = locationcol.Next(Mapsize);
            Location = new Location(row, col);

            if (Map.GetRoomTypeAtLocation(Location) == RoomType.Normal) // if normal
                return Location;
            else
                return RandomPitRoomLocation(); // if there is neighbour then recursion to get new location
        }

        //any additional methods (if needed)
        /// <summary>
        /// RandomPitLocation method to generate random oneup room location
        /// </summary>
        /// <returns>returns random location for oneup room</returns>
        public Location RandomOneUpLocation() // same logic as pitroom location
        {
            Random locationrow = new Random();
            Random locationcol = new Random();

            int row = locationrow.Next(Mapsize);
            int col = locationcol.Next(Mapsize);
            Location = new Location(row, col);

            if (Map.GetRoomTypeAtLocation(Location) == RoomType.Normal) // if normal
                return Location;
            else
                return RandomOneUpLocation(); // if there is neighbour then recursion to get new location
        }

        //TODO: (A8) Create a method that returns a random location where it is suitable to add the minotaur monster
        //a minotaur should only be added to a location that is empty (RoomType.Normal)

        /// <summary>
        /// RandomPitLocation method to generate random monster location
        /// </summary>
        /// <returns>returns random location for monster</returns>
        public Location RandomMonsterLocation() // same logic as pitroom location
        {
            Random locationrow = new Random();
            Random locationcol = new Random();

            int row = locationrow.Next(Mapsize);
            int col = locationcol.Next(Mapsize);
            Location = new Location(row, col);

            if (Map.GetRoomTypeAtLocation(Location) == RoomType.Normal) // if normal
                return Location;
            else
                return RandomMonsterLocation(); // if there is neighbour then recursion to get new location
        }

    }
}

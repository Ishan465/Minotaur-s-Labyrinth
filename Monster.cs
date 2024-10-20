namespace A6MinotaurLabyrinth
{
	/// <summary>
	/// Represents one of the several monster types in the game.
	/// </summary>
	public abstract class Monster
	{
		// The monster's current location.
		public Location Location { get; set; }

		// Whether the monster is alive or not.
		public bool IsAlive { get; set; } = true;

		// Creates a monster at the given location.
		public Monster(Location start) => Location = start;

		// Called when the monster and the player are both in the same room. Gives
		// the monster a chance to do its thing.
		public abstract void Activate(LabyrinthGame game);
	}

    /// <summary>
    /// class Minotaur that inherits from monster
    /// Minotaur class that inherits from the Monster class. Make sure to implement any abstract methods.
    /// When activated, the minotaur charges and knocks the player two spaces east (+2 columns) and one space north (-1 row) and the minotaur moves two spaces west (-2 columns) and one space south (+1 row). 
    /// Ensure both player and minotaur stay within the boundaries of the map. 
    /// If the player has found the sword, the minotaur will hide it in a random room.
    /// </summary>
    class Minotaur : Monster
	{
        public Minotaur(Location start) : base(start) { }

        /// <summary>
        /// Activate method that automatically activates when monster and player are in same room
        /// </summary>
        /// <param name="game"></param>
		public override void Activate(LabyrinthGame game)
		{
            LabryinthCreatorRng labryinth = new LabryinthCreatorRng(game.Map);
            if (game.PlayerHasSword == true) // if player has sword
            {
                Console.WriteLine("You have encountered Minotaur. Because you are carriying sword he is afraid of you and hidden to other room "); // minoter hides because of sword
                Location = labryinth.RandomMonsterLocation(); // random location
            }
            else
            {
                Console.WriteLine("You have encountered Minotaur. He rushed towards you and you both moved to different locations due to clash"); // else both clash according to 
                game.Player.Location = GetPlayerLocation(game); // player location according to instructions
                Location = GetMonsterLocation(game); // monster location acording to instructions
            }
		}

        /// <summary>
        /// get player location
        /// </summary>
        /// <param name="game"></param>
        /// <returns> player location according to instruction </returns>
		public Location GetPlayerLocation(LabyrinthGame game)
		{
			Location location = game.Player.Location; // player location
			int row = location.Row;
			int col = location.Column;

			int newRow = row - 1;
            int newCol = col + 2;

            Location testLocation = new Location(newRow, newCol); // new test location

            if (!(game.Map.IsOnMap(testLocation))) // if test location is not on map
            {
                if (newRow < 0 && newCol >= game.Map.Columns) // if row and colums both are outside then return previous
				{
					Location newLocation = new Location(row, col);
					return newLocation;
				}
                else if ((newRow >= 0 && newRow < game.Map.Rows) && newCol >= game.Map.Columns) // if row is inside and column is outside then return new row and old column
				{
                    Location newLocation = new Location(newRow, col);
                    return newLocation;
                }
                else
                {
                    Location newLocation = new Location(row, newCol); // if row is outside and column is inside then return old row and new column
                    return newLocation;
                }
            }
            else
            {
				return testLocation; // if inside then return test location
            }
        }
        /// <summary>
        /// get monster location
        /// </summary>
        /// <param name="game"></param>
        /// <returns> monster location </returns>
        public Location GetMonsterLocation(LabyrinthGame game) // same logic as get player location but numbers are different
        {
            int row = Location.Row;
            int col = Location.Column;

            int newRow = row + 1;
            int newCol = col - 2;

            Location testLocation = new Location(newRow, newCol);

            if (!(game.Map.IsOnMap(testLocation)))
            {
                if (newRow >= game.Map.Rows && newCol < 0)
                {
                    Location newLocation = new Location(row, col);
                    return newLocation;
                }
                else if ((newRow >= 0 && newRow < game.Map.Rows) && newCol < 0)
                {
                    Location newLocation = new Location(newRow, col);
                    return newLocation;
                }
                else
                {
                    Location newLocation = new Location(row, newCol);
                    return newLocation;
                }
            }
            else
            {
                return testLocation;
            }
        }
    }
    class Bowser : Monster // Bowser monster
    {
        public Bowser(Location start) : base(start) { }

        public override void Activate(LabyrinthGame game)
        {
            LabryinthCreatorRng labryinth = new LabryinthCreatorRng(game.Map);
            if (game.PlayerHasSword) // if player has sword
            {
                Console.WriteLine("You have encountered bowser you have 50% chance of winning because you have sword. Press enter to continue"); // he has 50 50 chance to survive while encountering bowser
                Console.ReadLine();
                Random random = new Random();
                int num = random.Next(0, 2);

                if (num == 0)
                {
                    Console.WriteLine("You won against bowser. The battle lasted and he moved to a different location"); // if won then bowser moves to other location
                    Location = labryinth.RandomMonsterLocation(); // bowser hides to random location
                }
                else
                {
                    game.Player.ReduceLives(); // if loses then player lives is reduced
                    Console.WriteLine("You lost against Bowser. He has moved to random location"); // and bowser hides in random room
                    Location = labryinth.RandomMonsterLocation();
                }
            }
            else // if player does not have sword then he will die 100% or reduce lives if player has
            {
                Console.WriteLine("You have encountered bowser you have 0% chance of winning because you do not have sword.");
                game.Player.ReduceLives();
                Console.WriteLine("You lost against Bowser.");
                if (game.Player.IsAlive)
                    Console.Write("You lost a life. Bowser has now spawned to a different location");
                Location = labryinth.RandomMonsterLocation();
            }
        }
    }
}

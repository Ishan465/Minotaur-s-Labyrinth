namespace A6MinotaurLabyrinth
{
	// Represents the player in the game.
	public class Player
	{
		// Creates a new player that starts at the given location.
		public Player(Location start) => Location = start;

		// The player's current location.
		public Location Location { get; set; }

		// Indicates whether the player is alive or not.
		public bool IsAlive { get; private set; } = true;

		// Indicates whether the player currently has the catacomb map.
		public bool HasMap { get; set; } = true;

		public int PlayerLives { get; set; } // added a new field for player lives

		// Indicates whether the player currently has the sword.
		public bool HasSword { get; set; } = false;

		// Explains why a player died.
		public string CauseOfDeath { get; private set; }

		public Player() // default is no ectra life
		{
			PlayerLives = 0;
        }
		/// <summary>
		/// increase life method to increase life by 1
		/// </summary>
		public void IncreaseLife()
		{
			PlayerLives += 1;
		}
		/// <summary>
		/// reduce life method to reduce lives. if 0 then die else reduce by 1
		/// </summary>
		public void ReduceLives()
		{
			if (PlayerLives == 0)
				Kill("You ran out of lives");
			else 
			{
                Console.WriteLine($"You used a life out of {PlayerLives} extra lives. If it falls to 0 and u are in danger you will die.");
                PlayerLives--;
            }
		}

		/// <summary>
		/// kill method to kill the player
		/// </summary>
		/// <param name="cause"></param>
		public void Kill(string cause)
		{
			IsAlive = false;
			CauseOfDeath = cause;
		}
	}
}

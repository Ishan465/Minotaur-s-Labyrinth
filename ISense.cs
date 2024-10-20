using System;

namespace A6MinotaurLabyrinth
{
	// Represents something that the player can sense as they wander the labyrinth.
	public interface ISense
	{
		// Returns whether the player should be able to sense the thing in question.
		bool CanSense(LabyrinthGame game);

		// Displays the sensed information. (Assumes `CanSense` was called first and returned `true`.)
		void DisplaySense(LabyrinthGame game);
	}

	// Represents the player's ability to sense the light in the entrance room.
	public class LightInEntranceSense : ISense
	{
		// Returns `true` if the player is in the entrance room.
		public bool CanSense(LabyrinthGame game) => game.Map.GetRoomTypeAtLocation(game.Player.Location) == RoomType.Entrance;

		// Displays the appropriate message if the player can see the light from outside the labyrinth
		public void DisplaySense(LabyrinthGame game) => ConsoleHelper.WriteLine("You see light in this room coming from outside the labyrinth. This is the entrance.", ConsoleColor.Yellow);
	}

	// Represents the player's ability to sense the magic sword.
	public class SwordSense : ISense
	{
		// Returns `true` if the player is in the sword room.
		public bool CanSense(LabyrinthGame game) => game.Map.GetRoomTypeAtLocation(game.Player.Location) == RoomType.Sword;

		// Displays the appropriate message depending on whether the sword is picked up or not.
		public void DisplaySense(LabyrinthGame game)
		{
			if (game.PlayerHasSword) ConsoleHelper.WriteLine("This is the sword room but you've already picked up the sword!", ConsoleColor.DarkCyan);
			else ConsoleHelper.WriteLine("You can sense that the sword is nearby!", ConsoleColor.DarkCyan);
		}
	}

    //TODO: (A7) Create a new class called PitSense that implements the ISense interface  
    // The player is able to sense a pit room when they are close (neighboring rooms)
    // Use other implementations as an example. 
    public class PitSense : ISense
    {
        // Returns `true` if the player is in the pit room.
        public bool CanSense(LabyrinthGame game) => game.Map.HasNeighborWithType(game.Player.Location, RoomType.Pit);

        // Displays the appropriate message on whether the pit is near or not.
        public void DisplaySense(LabyrinthGame game) => ConsoleHelper.WriteLine($"You can sense pit room nearby. Be careful(you have {game.Player.PlayerLives} extra lives) ", ConsoleColor.Yellow);
    
    }

    public class LifeSense : ISense
    {
        // Returns `true` if the player is in the one up room.
        public bool CanSense(LabyrinthGame game) => game.Map.HasNeighborWithType(game.Player.Location, RoomType.OneUp);

        // Displays the appropriate message depending on whether the one up is near or not.
        public void DisplaySense(LabyrinthGame game) => ConsoleHelper.WriteLine("You can sense see oneup room nearby.", ConsoleColor.Green);
    }

    //TODO: (A8) Create a new class called MinotaurSense that implements the ISense interface  
    // The player is able to sense if a minotaur is in one of the neighboring rooms
    // Use other implementations as an example. 

    public class MinotaurSense : ISense
    {
        // Returns `true` if the player minotour as neighbour
        public bool CanSense(LabyrinthGame game)
        {
            Location playerLocation = game.Player.Location;
            Monster[] monsters = game.Monsters;
            Location minotaurLocation = monsters[0].Location;

            return game.Map.HasNeighbourWithMonster(playerLocation, minotaurLocation); ;
        }

        // Displays the appropriate message depending on whether the one up is near or not.
        public void DisplaySense(LabyrinthGame game) => ConsoleHelper.WriteLine("You can sense minotaur is nearby.", ConsoleColor.Magenta);
       
    }

    public class BowserSense : ISense
    {
        // Returns `true` if the player minotour as neighbour
        public bool CanSense(LabyrinthGame game)
        {
            Location playerLocation = game.Player.Location;
            Monster[] monsters = game.Monsters;
            Location bowserLocation = monsters[1].Location;

            return game.Map.HasNeighbourWithMonster(playerLocation, bowserLocation); ;
        }

        // Displays the appropriate message depending on whether the one up is near or not.
        public void DisplaySense(LabyrinthGame game)
        {
            Location playerLocation = game.Player.Location;
            Monster[] monsters = game.Monsters;
            Location bowserLocation = monsters[1].Location;

            if (bowserLocation == playerLocation)
            {
                Console.WriteLine("You encountered Bowser and it hit you");
            }
            else 
            { 
                ConsoleHelper.WriteLine("You can sense bowser is nearby.", ConsoleColor.Blue); 
            }
        }
    }
}

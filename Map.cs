namespace A6MinotaurLabyrinth
{
	// Represents the map and what each room is made out of.
	public class Map
	{
		// Stores which room type each room in the world is. The default is `Normal` because that is the first
		// member in the enumeration list.
		private readonly RoomType[,] _rooms;

		// The total number of rows in this specific game world.
		public int Rows { get; }

		// The total number of columns in this specific game world.
		public int Columns { get; }

		// Creates a new map with a specific size.
		public Map(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			_rooms = new RoomType[rows, columns];
		}

		// Returns what type a room at a specific location is.
		public RoomType GetRoomTypeAtLocation(Location location) => IsOnMap(location) ? _rooms[location.Row, location.Column] : RoomType.OffTheMap;

		// Determines if a neighboring room is of the given type.
		public bool HasNeighborWithType(Location location, RoomType roomType)
		{
			if (GetRoomTypeAtLocation(new Location(location.Row - 1, location.Column - 1)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row - 1, location.Column)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row - 1, location.Column + 1)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row, location.Column - 1)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row, location.Column)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row, location.Column + 1)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row + 1, location.Column - 1)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row + 1, location.Column)) == roomType) return true;
			if (GetRoomTypeAtLocation(new Location(location.Row + 1, location.Column + 1)) == roomType) return true;
			return false;
		}

		// added a new has neighbour with monster method which checks whether monster is in neighbouting room or not
		public bool HasNeighbourWithMonster(Location playerLocation, Location monsterLocation) // similar to has neighbour with roomtype method
		{
			int playerRow = playerLocation.Row;
			int playerCol = playerLocation.Column;
			int monsterRow = monsterLocation.Row;
			int monsterCol = monsterLocation.Column;

			if (playerRow == monsterRow - 1 && playerCol == monsterCol - 1 ) return true;
            if (playerRow == monsterRow - 1 && playerCol == monsterCol) return true;
            if (playerRow == monsterRow - 1 && playerCol == monsterCol + 1) return true;
            if (playerRow == monsterRow && playerCol == monsterCol - 1) return true;
            if (playerRow == monsterRow && playerCol == monsterCol) return true;
            if (playerRow == monsterRow && playerCol == monsterCol + 1) return true;
            if (playerRow == monsterRow + 1 && playerCol == monsterCol - 1) return true;
            if (playerRow == monsterRow + 1 && playerCol == monsterCol) return true;
            if (playerRow == monsterRow + 1 && playerCol == monsterCol + 1) return true;
			return false;
        }

		// Indicates whether a specific location is actually on the map or not.
		public bool IsOnMap(Location location) =>
			location.Row >= 0 &&
			location.Row < _rooms.GetLength(0) &&
			location.Column >= 0 &&
			location.Column < _rooms.GetLength(1);

		// Changes the type of room at a specific spot in the world to a new type.
		public void SetRoomTypeAtLocation(Location location, RoomType type) => _rooms[location.Row, location.Column] = type;

		public void Display(Location? playerLocation)
		{
			for (int i = 0; i < Rows; ++i)
			{
				for (int j = 0; j < Columns; ++j) 
				{
					var room = _rooms[i, j];
					if (playerLocation != null)
					{
						if (playerLocation.Row == i && playerLocation.Column == j)
							ConsoleHelper.Write($"[ ]", ConsoleColor.Yellow);
						else
							ConsoleHelper.Write($"[ ]", ConsoleColor.Gray);
					}
					else
						ConsoleHelper.Write($"[{room.ToString()[0]}]", ConsoleColor.Gray);
				}
				Console.WriteLine();
			}

		}
	}

	// Represents a location in the 2D game world, based on its row and column.
	public record Location(int Row, int Column);
	// Represents one of the four directions of movement.
	public enum Direction { North, South, West, East }
	// Represents one of the different types of rooms in the game.
	public enum RoomType { Normal, Entrance, Sword, Pit, OneUp, OffTheMap }	//TODO: (A7) add a extra RoomType enum option for Pit rooms 
	// alse added oneup type room
}

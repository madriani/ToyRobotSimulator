namespace ToyRobotSimulatorCore.Table {
	///<summary>Represents a generic table, that can check whether a given position is available on top of itself.</summary>
	public interface ITable {
		///<summary>Checks whether the provided position is available on the table.</summary>
		///<param name="position">The position to be checked.</param>
		///<returns>Returns true if the position is available on the table. Otherwise, false.</returns>
		bool IsPositionAvailableOnTable(Point.Point position);
	}
}
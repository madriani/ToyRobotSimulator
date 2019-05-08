namespace ToyRobotSimulatorCore.Robot.RobotStatus {
	///<summary>Represents a read-only snapshot of the status of the robot, including its position and direction.</summary>
	public interface IReadOnlyStatus {
		///<value>Represents the position of the robot.</value>
		Point.Point Position { get; }

		///<value>Represents the direction of the robot.</value>		
		Direction.Direction Direction { get; }
	}
}
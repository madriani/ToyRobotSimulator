using ToyRobotSimulatorCore.Robot.RobotStatus;
using ToyRobotSimulatorCore.Table;

namespace ToyRobotSimulatorCore.Robot {
	///<summary>
	///The main ToyRobotSimulatorCore.Robot interface.
	///Contains the methods to interact with the robot.
	///</summary>
	public interface IRobot {
		///<summary>Places the robot on the provided <paramref name="table"/> on the position specified by the <paramref name="desiredPosition"/> parameter and with the direction specified by the <paramref name="desiredDirection"/> parameter.</summary>
		///<param name ="table">The table where to place the robot.</param>
		///<param name="desiredPosition">The desired position.</param>
		///<param name="desiredDirection">The desired direction.</param>
		///<exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		///<exception cref="System.InvalidOperationException">Thrown if <paramref name="desiredPosition"/> is not available on the table.</exception>
		///<exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="desiredDirection"/> does not contain a valid value.</exception>
		void Place(ITable table, Point.Point desiredPosition, Direction.Direction desiredDirection);

		///<summary>Moves the robot one unit forward in the direction it is currently facing.</summary>
		///<exception cref="System.InvalidOperationException">Thrown if the robot is not currently placed on the table or if the position in front of the robot is not available.</exception>
		void Move();

		///<summary>Rotates the robot 90 degrees counterclockwise without changing its position.</summary>
		///<exception cref="System.InvalidOperationException">Thrown if the robot is not currently placed on the table.</exception>
		void RotateToTheLeft();

		///<summary>Rotates the robot 90 degrees clockwise without changing its position.</summary>
		///<exception cref="System.InvalidOperationException">Thrown if the robot is not currently placed on the table.</exception>
		void RotateToTheRight();

		///<summary>Returns a read-only shapshot of the current position and direction of the robot.</summary>
		///<exception cref="System.InvalidOperationException">Thrown if the robot is not currently placed on the table.</exception>
		IReadOnlyStatus GetStatus();
	}
}
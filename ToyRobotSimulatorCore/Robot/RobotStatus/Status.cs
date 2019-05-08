using System;
using ToyRobotSimulatorCore.Table;

namespace ToyRobotSimulatorCore.Robot.RobotStatus {
	///<summary>Represents the status of the robot, including its position and direction on the table.</summary>
	class Status : IReadOnlyStatus {
		///<value>Represents the table the robot is placed on.</value>
		public ITable Table { get; set; }

		///<value>Represents the position of the robot.</value>
		public Point.Point Position { get; set; }

		///<value>Represents the direction of the robot.</value>		
		public Direction.Direction Direction { get; set; }

		///<summary>Initializes a new instance of the ToyRobotSimulatorCore.Robot.RobotStatus.Status class with the provided table, position and direction.</summary>
		///<param name ="table">The table the robot is placed on.</param>
		///<param name="position">The position of the robot.</param>
		///<param name="direction">The direction of the robot.</param>
		///<exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public Status(ITable table, Point.Point position, Direction.Direction direction) {
			if (table == null) {
				throw new ArgumentNullException(nameof(table));
			}
			this.Table = table;
			this.Position = position;
			this.Direction = direction;
		}
	}
}
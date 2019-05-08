using System;
using ToyRobotSimulatorCore.Robot.Direction;
using ToyRobotSimulatorCore.Robot.RobotStatus;
using ToyRobotSimulatorCore.Table;

namespace ToyRobotSimulatorCore.Robot {
	class ToyRobot : IRobot {
		protected const string NotPlacedOnTableText = "The robot is not currently placed on the table";

		protected const string NotAvailablePositionText = "Desired robot position is not available";

		protected Status Status { get; set; }

		public void Place(ITable table, Point.Point desiredPosition, Direction.Direction desiredDirection) {
			if (table == null) {
				throw new ArgumentNullException(nameof(table));
			}
			if (!Enum.IsDefined(typeof(Direction.Direction), desiredDirection)) {
				throw new ArgumentOutOfRangeException(nameof(desiredDirection));
			}

			//Check desiredPosition validity
			if (table.IsPositionAvailableOnTable(desiredPosition)) {
				//Create new Status instance (if needed) and set properties
				if (Status == null) {
					Status = new Status(table, desiredPosition, desiredDirection);
				} else {
					Status.Table = table;
					Status.Position = desiredPosition;
					Status.Direction = desiredDirection;
				}
			} else {
				throw new InvalidOperationException(NotAvailablePositionText);
			}
		}

		public void Move() {
			//Check if robot is on the table
			if (Status != null) {
				//Compute desired position
				var newRelativePosition = Status.Direction.GetNewRelativePosition();
				var desiredPosition = Status.Position + newRelativePosition;

				//Check if desired position is valid
				if (Status.Table.IsPositionAvailableOnTable(desiredPosition)) {
					//Update current robot position
					Status.Position = desiredPosition;
				} else {
					throw new InvalidOperationException(NotAvailablePositionText);
				}
			} else {
				throw new InvalidOperationException(NotPlacedOnTableText);
			}
		}

		protected void Rotate(bool toTheLeft) {
			//Check if robot is on the table
			if (Status != null) {
				//Rotate robot
				Status.Direction = toTheLeft ? Status.Direction.GetLeftRotatedDirection() : Status.Direction.GetRightRotatedDirection();
			} else {
				throw new InvalidOperationException(NotPlacedOnTableText);
			}
		}

		public void RotateToTheLeft() {
			Rotate(true);
		}

		public void RotateToTheRight() {
			Rotate(false);
		}

		public IReadOnlyStatus GetStatus() {
			//Check if robot is on the table
			if (Status != null) {
				return Status;
			} else {
				throw new InvalidOperationException(NotPlacedOnTableText);
			}
		}
	}
}
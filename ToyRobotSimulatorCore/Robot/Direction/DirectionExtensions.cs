using System;

namespace ToyRobotSimulatorCore.Robot.Direction {
	///<summary>Contains helper methods for the Direction enum.</summary>
	static class DirectionExtensions {
		///<summary>Rotates the provided direction counterclockwise by 90 degrees and returns the result.</summary>
		///<param name="direction">The direction to be rotated.</param>
		///<returns>Returns the provided direction rotated counterclockwise by 90 degrees.</returns>
		///<exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="direction"/> does not contain a valid value.</exception>
		public static Direction GetLeftRotatedDirection(this Direction direction) {
			switch (direction) {
				case Direction.North:
					return Direction.West;
				case Direction.East:
					return Direction.North;
				case Direction.South:
					return Direction.East;
				case Direction.West:
					return Direction.South;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction));
			}
		}

		///<summary>Rotates the provided direction clockwise by 90 degrees and returns the result.</summary>
		///<param name="direction">The direction to be rotated.</param>
		///<returns>Returns the provided direction rotated clockwise by 90 degrees.</returns>
		///<exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="direction"/> does not contain a valid value.</exception>
		public static Direction GetRightRotatedDirection(this Direction direction) {
			switch (direction) {
				case Direction.North:
					return Direction.East;
				case Direction.East:
					return Direction.South;
				case Direction.South:
					return Direction.West;
				case Direction.West:
					return Direction.North;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction));
			}
		}

		///<summary>Computes the new position of the robot, relative to the current one, if it moves one step forward in the provided direction, and returns it.</summary>
		///<param name="direction">The direction of the robot.</param>
		///<returns>Returns the new position of the robot, relative to the current one, if it moves one step forward in the provided direction.</returns>
		///<exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="direction"/> does not contain a valid value.</exception>
		public static Point.Point GetNewRelativePosition(this Direction direction) {
			switch (direction) {
				case Direction.North:
					return new Point.Point(0, 1);
				case Direction.South:
					return new Point.Point(0, -1);
				case Direction.East:
					return new Point.Point(1, 0);
				case Direction.West:
					return new Point.Point(-1, 0);
				default:
					throw new ArgumentOutOfRangeException(nameof(direction));
			}
		}

		///<summary>Converts a string to the corresponding value of the Direction enum, if possible.</summary>
		///<param name="string">The string to convert.</param>
		///<returns>The value of the Direction enum corresponding to the provided string.</returns>
		///<exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="string"/> is not convertible to a valid Direction.</exception>
		public static Direction ConvertStringToDirection(string @string) {
			if (@string == null) {
				throw new ArgumentNullException(nameof(@string));
			}
			switch (@string.ToUpperInvariant()) {
				case "NORTH":
					return Direction.North;
				case "EAST":
					return Direction.East;
				case "SOUTH":
					return Direction.South;
				case "WEST":
					return Direction.West;
				default:
					throw new ArgumentOutOfRangeException(nameof(@string));
			}
		}
	}
}
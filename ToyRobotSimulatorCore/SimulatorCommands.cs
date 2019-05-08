using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotSimulatorCore.Robot.Direction;

namespace ToyRobotSimulatorCore {
	///<summary>Contains methods and constants describing the commands supported by the simulator.</summary>
	public static class SimulatorCommands {
		///<value>Places the robot on the table.</value>
		public const string Place = "PLACE";

		///<value>Moves the robot one unit forward in the direction it is currently facing.</value>
		public const string Move = "MOVE";

		///<value>Rotates the robot 90 degrees counterclockwise without changing its position.</value>
		public const string Left = "LEFT";

		///<value>Rotates the robot 90 degrees clockwise without changing its position.</value>
		public const string Right = "RIGHT";

		///<value>Announces position and direction of the robot.</value>
		public const string Report = "REPORT";

		///<summary>Returns the list of all the command names supported by the simulator.</summary>
		public static IEnumerable<string> GetCommandNames() {
			return new string[]{
				Place, Move, Left, Right, Report
			};
		}

		///<summary>Returns the list of all the commands supported by the simulator, including required parameters.</summary>
		public static IEnumerable<string> GetCommandsWithParameters() {
			var placeWithParameters = new StringBuilder($"{Place} X,Y,{{");
			var directions = Enum.GetNames(typeof(Direction));
			int i;
			for (i = 0; i < directions.Length - 1; i++) {
				placeWithParameters.Append($"{directions[i].ToString().ToUpperInvariant()}|");
			}
			placeWithParameters.Append($"{directions[i].ToString().ToUpperInvariant()}}}");

			return new string[]{
				placeWithParameters.ToString(), Move, Left, Right, Report
			};
		}
	}
}
using System;
using System.IO;
using ToyRobotSimulatorCore.Robot;
using ToyRobotSimulatorCore.Robot.Direction;
using ToyRobotSimulatorCore.Table;

namespace ToyRobotSimulatorCore {
	public class Simulator : ISimulator {
		protected ITable Table { get; }
		protected IRobot Robot { get; }
		protected TextWriter MainTextWriter { get; }
		protected TextWriter LogTextWriter { get; }

		///<summary>Initializes a new instance of the ToyRobotSimulatorCore.Simulator class with the provided table size and writing output and logs on the provided TextWriter instances.</summary>
		///<param name ="horizontalTableSize">The horizontal size of the table to use.</param>
		///<param name ="verticalTableSize">The vertical size of the table to use.</param>
		///<param name ="mainTextWriter">A TextWriter to use for the main Simulator output.</param>
		///<param name ="logTextWriter">A TextWriter to use for secondary Simulator logs.</param>
		///<exception cref="System.ArgumentNullException">Thrown if <paramref name="mainTextWriter"/> is null.</exception>
		public Simulator(int horizontalTableSize, int verticalTableSize, TextWriter mainTextWriter, TextWriter logTextWriter = null) {
			if (mainTextWriter == null) {
				throw new ArgumentNullException(nameof(mainTextWriter));
			}
			this.Table = new BasicTable(horizontalTableSize, verticalTableSize);
			this.Robot = new ToyRobot();
			this.MainTextWriter = mainTextWriter;
			this.LogTextWriter = logTextWriter;
		}

		public void ExecuteCommand(string command) {
			if (command == null) {
				ThrowArgumentNullExceptionOnNullArgument(nameof(command), nameof(ExecuteCommand));
			}

			//Ignore not relevant spaces and case
			command = command.Trim().ToUpperInvariant();

			if (command.StartsWith(SimulatorCommands.Place)) {
				var parts = command.Split(' ');
				if (parts.Length == 2 && parts[0] == SimulatorCommands.Place) {
					var parameters = parts[1].Split(',');
					if (parameters.Length != 3) {
						ThrowArgumentExceptionOnInvalidCommand(command, nameof(ExecuteCommand), null);
					} else {
						try {
							int x = int.Parse(parameters[0].Trim());
							int y = int.Parse(parameters[1].Trim());
							var position = new Point.Point(x, y);
							var direction = DirectionExtensions.ConvertStringToDirection(parameters[2].Trim());

							Robot.Place(Table, position, direction);
						} catch (InvalidOperationException ioe) {
							// Handle exceptions from Robot.Place
							LogNotAllowedCommand(command, ioe);
						} catch (Exception e) when (e is FormatException || e is OverflowException || e is ArgumentOutOfRangeException) {
							// Handle exceptions from parameters parsing
							ThrowArgumentExceptionOnInvalidCommand(command, nameof(ExecuteCommand), e);
						}
					}
				} else {
					ThrowArgumentExceptionOnInvalidCommand(command, nameof(ExecuteCommand), null);
				}
			} else {
				try {
					switch (command) {
						case SimulatorCommands.Move:
							Robot.Move();
							break;
						case SimulatorCommands.Left:
							Robot.RotateToTheLeft();
							break;
						case SimulatorCommands.Right:
							Robot.RotateToTheRight();
							break;
						case SimulatorCommands.Report:
							var robotStatus = Robot.GetStatus();
							MainTextWriter.WriteLine($"{robotStatus.Position.x},{robotStatus.Position.y},{robotStatus.Direction.ToString().ToUpperInvariant()}");
							break;
						default:
							ThrowArgumentExceptionOnInvalidCommand(command, nameof(ExecuteCommand), null);
							break;
					}
				} catch (InvalidOperationException ioe) {
					LogNotAllowedCommand(command, ioe);
				}
			}
		}

		protected void LogNotAllowedCommand(string command, InvalidOperationException ioe) {
			LogTextWriter?.WriteLine($"LOG: \"{command}\" command is not allowed at this moment (\"{ioe.Message}\")");
		}

		protected void ThrowArgumentExceptionOnInvalidCommand(string command, string methodName, Exception cause) {
			var message = $"Invalid {nameof(command)} \"{command}\" passed to {methodName}";
			LogTextWriter?.WriteLine($"LOG: {message}");
			if (cause == null) {
				throw new ArgumentException(message);
			} else {
				throw new ArgumentException(message, cause);
			}
		}

		protected void ThrowArgumentNullExceptionOnNullArgument(string parameterName, string methodName) {
			var message = $"null passed as \"{parameterName}\" to {methodName}";
			LogTextWriter?.WriteLine($"LOG: {message}");
			throw new ArgumentNullException(parameterName, message);
		}
	}
}
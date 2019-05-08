using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ToyRobotSimulatorCore {
	public class SimulatorTest {
		const string DefaultNewLine = "\r\n";
		const int HorizontalTableSize = 5;
		const int VerticalTableSize = 5;

		//Helper methods
		static ISimulator CreateDefaultSimulator(out StringWriter outWriter, out StringWriter logWriter) {
			outWriter = new StringWriter();
			logWriter = new StringWriter();
			return new Simulator(HorizontalTableSize, VerticalTableSize, outWriter, logWriter);
		}

		static void ExecuteMultipleCommands(ISimulator simulator, string[] commands) {
			foreach (var command in commands) {
				simulator.ExecuteCommand(command);
			}
		}

		static string ReplaceNewLine(string input) {
			return input.Replace(DefaultNewLine, Environment.NewLine);
		}

		static void RunTestForValidCommands(string[] commands, string expected) {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			ExecuteMultipleCommands(simulator, commands);

			expected = ReplaceNewLine(expected);
			Assert.Equal(expected, outWriter.ToString());
			Assert.Equal("", logWriter.ToString());
		}

		//Tests on provided examples
		[Theory]
		[Trait("Category", "Valid Commands")]
		[InlineData(new string[] { "PLACE 0,0,NORTH", "MOVE", "REPORT" }, "0,1,NORTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,NORTH", "LEFT", "REPORT" }, "0,0,WEST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE", "REPORT" }, "3,3,NORTH" + DefaultNewLine)]
		public void ExecuteCommand_ProvidedExamples_ReportsCorrectPositionAndDirectionAndLogIsEmpty(string[] commands, string expected) {
			RunTestForValidCommands(commands, expected);
		}

		//Tests on valid commands
		[Theory]
		[Trait("Category", "Valid Commands")]
		[InlineData(new string[] { "PLACE 0,0,NORTH", "REPORT" }, "0,0,NORTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,EAST", "REPORT" }, "0,0,EAST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,SOUTH", "REPORT" }, "0,0,SOUTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,WEST", "REPORT" }, "0,0,WEST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 3,2,SOUTH", "REPORT" }, "3,2,SOUTH" + DefaultNewLine)] //checks with x!=y
		[InlineData(new string[] { "PLACE 4,4,NORTH", "REPORT" }, "4,4,NORTH" + DefaultNewLine)] //checks boundary
		[InlineData(new string[] { "PLACE 0,0,NORTH", "PLACE 2,1,SOUTH", "REPORT" }, "2,1,SOUTH" + DefaultNewLine)] //checks multiple calls to "PLACE"
		public void ExecuteCommand_ValidPlaceCommand_ReportsCorrectPositionAndDirectionAndLogIsEmpty(string[] commands, string expected) {
			RunTestForValidCommands(commands, expected);
		}

		[Theory]
		[Trait("Category", "Valid Commands")]
		[InlineData(new string[] { "PLACE 1,1,NORTH", "MOVE", "REPORT" }, "1,2,NORTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 1,1,EAST", "MOVE", "REPORT" }, "2,1,EAST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 1,1,SOUTH", "MOVE", "REPORT" }, "1,0,SOUTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 1,1,WEST", "MOVE", "REPORT" }, "0,1,WEST" + DefaultNewLine)]
		public void ExecuteCommand_ValidMoveCommand_ReportsCorrectPositionAndDirectionAndLogIsEmpty(string[] commands, string expected) {
			RunTestForValidCommands(commands, expected);
		}

		[Theory]
		[Trait("Category", "Valid Commands")]
		[InlineData(new string[] { "PLACE 0,0,NORTH", "LEFT", "REPORT" }, "0,0,WEST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,EAST", "LEFT", "REPORT" }, "0,0,NORTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,SOUTH", "LEFT", "REPORT" }, "0,0,EAST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,WEST", "LEFT", "REPORT" }, "0,0,SOUTH" + DefaultNewLine)]
		public void ExecuteCommand_ValidLeftCommand_ReportsCorrectPositionAndDirectionAndLogIsEmpty(string[] commands, string expected) {
			RunTestForValidCommands(commands, expected);
		}

		[Theory]
		[Trait("Category", "Valid Commands")]
		[InlineData(new string[] { "PLACE 0,0,NORTH", "RIGHT", "REPORT" }, "0,0,EAST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,EAST", "RIGHT", "REPORT" }, "0,0,SOUTH" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,SOUTH", "RIGHT", "REPORT" }, "0,0,WEST" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,0,WEST", "RIGHT", "REPORT" }, "0,0,NORTH" + DefaultNewLine)]
		public void ExecuteCommand_ValidRightCommand_ReportsCorrectPositionAndDirectionAndLogIsEmpty(string[] commands, string expected) {
			RunTestForValidCommands(commands, expected);
		}

		//Tests on not allowed commands
		[Theory]
		[Trait("Category", "Not Allowed Commands")]
		[InlineData(new string[] { "MOVE" },
			"LOG: \"MOVE\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "LEFT" },
			"LOG: \"LEFT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "RIGHT" },
			"LOG: \"RIGHT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "REPORT" },
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		public void ExecuteCommand_MoveLeftRightReportBeforePlace_NoOutputAndLogContainsCorrectMessage(string[] commands, string expectedLog) {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			ExecuteMultipleCommands(simulator, commands);

			expectedLog = ReplaceNewLine(expectedLog);
			Assert.Equal("", outWriter.ToString());
			Assert.Equal(expectedLog, logWriter.ToString());
		}

		[Theory]
		[Trait("Category", "Not Allowed Commands")]
		[InlineData(new string[] { "PLACE -1,0,NORTH", "REPORT" },
			"LOG: \"PLACE -1,0,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine +
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,-1,NORTH", "REPORT" },
			"LOG: \"PLACE 0,-1,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine +
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE -1,-1,NORTH", "REPORT" },
			"LOG: \"PLACE -1,-1,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine +
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 5,0,NORTH", "REPORT" },
			"LOG: \"PLACE 5,0,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine +
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 0,5,NORTH", "REPORT" },
			"LOG: \"PLACE 0,5,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine +
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		[InlineData(new string[] { "PLACE 5,5,NORTH", "REPORT" },
			"LOG: \"PLACE 5,5,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine +
			"LOG: \"REPORT\" command is not allowed at this moment (\"The robot is not currently placed on the table\")" + DefaultNewLine)]
		public void ExecuteCommand_SingleNotAllowedPlaceCommand_RobotNotPlacedAndLogContainsCorrectMessage(string[] commands, string expectedLog) {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			ExecuteMultipleCommands(simulator, commands);

			expectedLog = ReplaceNewLine(expectedLog);
			Assert.Equal("", outWriter.ToString());
			Assert.Equal(expectedLog, logWriter.ToString());
		}

		[Theory]
		[Trait("Category", "Not Allowed Commands")]
		[InlineData(new string[] { "PLACE 0,0,NORTH", "PLACE -1,0,NORTH", "REPORT" },
			"0,0,NORTH" + DefaultNewLine,
			"LOG: \"PLACE -1,0,NORTH\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine)]
		public void ExecuteCommand_MultiplePlaceCommandsIncludingNotAllowed_ReportsCorrectPositionAndDirectionAndLogContainsCorrectMessage(string[] commands, string expectedOut, string expectedLog) {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			ExecuteMultipleCommands(simulator, commands);

			expectedOut = ReplaceNewLine(expectedOut);
			expectedLog = ReplaceNewLine(expectedLog);
			Assert.Equal(expectedOut, outWriter.ToString());
			Assert.Equal(expectedLog, logWriter.ToString());
		}

		[Theory]
		[Trait("Category", "Not Allowed Commands")]
		[InlineData(new string[] {
			"PLACE 2,4,NORTH", "MOVE", "REPORT" },
			"2,4,NORTH" + DefaultNewLine,
			"LOG: \"MOVE\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine)]
		[InlineData(new string[] {
			"PLACE 4,2,EAST", "MOVE", "REPORT" },
			"4,2,EAST" + DefaultNewLine,
			"LOG: \"MOVE\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine)]
		[InlineData(new string[] {
			"PLACE 2,0,SOUTH", "MOVE", "REPORT" },
			"2,0,SOUTH" + DefaultNewLine,
			"LOG: \"MOVE\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine)]
		[InlineData(new string[] {
			"PLACE 0,2,WEST", "MOVE", "REPORT" },
			"0,2,WEST" + DefaultNewLine,
			"LOG: \"MOVE\" command is not allowed at this moment (\"Desired robot position is not available\")" + DefaultNewLine)]
		public void ExecuteCommand_MoveAfterPlaceNearBoundary_ReportsCorrectPositionAndDirectionAndLogContainsCorrectMessage(string[] commands, string expectedOut, string expectedLog) {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			ExecuteMultipleCommands(simulator, commands);

			expectedOut = ReplaceNewLine(expectedOut);
			expectedLog = ReplaceNewLine(expectedLog);
			Assert.Equal(expectedOut, outWriter.ToString());
			Assert.Equal(expectedLog, logWriter.ToString());
		}

		//Exceptional situations (e.g. null/invalid commands)
		[Fact]
		[Trait("Category", "Exceptional Situations")]
		public void Constructor_NullMainTextWriter_ThrowsArgumentNullException() {
			Assert.Throws<ArgumentNullException>(() => new Simulator(HorizontalTableSize, VerticalTableSize, null));
		}

		[Fact]
		[Trait("Category", "Exceptional Situations")]
		public void ExecuteCommand_NullCommand_ThrowsArgumentNullException() {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			var ex = Assert.Throws<ArgumentNullException>(() => simulator.ExecuteCommand(null));

			var expectedMessage = $"null passed as \"command\" to ExecuteCommand{Environment.NewLine}";
			Assert.Equal($"{expectedMessage}Parameter name: command", ex.Message);
			Assert.Equal("", outWriter.ToString());
			Assert.Equal($"LOG: {expectedMessage}", logWriter.ToString());
		}

		[Fact]
		[Trait("Category", "Exceptional Situations")]
		public void ExecuteCommand_InvalidCommand_ThrowsArgumentException() {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);
			var command = "HELLO";

			var ex = Assert.Throws<ArgumentException>(() => simulator.ExecuteCommand(command));

			var expectedMessage = $"Invalid command \"{command.ToUpperInvariant()}\" passed to ExecuteCommand";
			Assert.Equal(expectedMessage, ex.Message);
			Assert.Equal("", outWriter.ToString());
			Assert.Equal($"LOG: {expectedMessage}{Environment.NewLine}", logWriter.ToString());
		}

		[Theory]
		[Trait("Category", "Exceptional Situations")]
		[InlineData("PLACE")]
		[InlineData("PLACEMENT")]
		[InlineData("PLACEMENT 0,0,NORTH")]
		[InlineData("PLACE 0,0,NORTHHELLO")]
		[InlineData("PLACE 0,0,NORTH HELLO")]
		[InlineData("PLACE ,0,NORTH")]
		[InlineData("PLACE 0,,NORTH")]
		[InlineData("PLACE 0,0,")]
		[InlineData("PLACE 0,0")]
		[InlineData("PLACE NORTH")]
		[InlineData("PLACE a,0,NORTH")]
		[InlineData("PLACE 0,a,NORTH")]
		[InlineData("PLACE 0,0,5")]
		[InlineData("PLACE 0,0,hello")]
		public void ExecuteCommand_InvalidPlaceCommand_ThrowsArgumentException(string command) {
			var simulator = CreateDefaultSimulator(out var outWriter, out var logWriter);

			var ex = Assert.Throws<ArgumentException>(() => simulator.ExecuteCommand(command));

			var expectedMessage = $"Invalid command \"{command.ToUpperInvariant()}\" passed to ExecuteCommand";
			Assert.Equal(expectedMessage, ex.Message);
			Assert.Equal("", outWriter.ToString());
			Assert.Equal($"LOG: {expectedMessage}{Environment.NewLine}", logWriter.ToString());
		}
	}
}

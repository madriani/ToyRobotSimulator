using System;
using ToyRobotSimulatorCore;
namespace ToyRobotSimulatorRunner {
	class Program {
		static void Main(string[] args) {
			const string closeProgramCommand = "*EXIT";
			const int tableSize = 5;
			Console.WriteLine("Welcome to Toy Robot Simulator!");
			Console.WriteLine($"The table is {tableSize}x{tableSize}.");
			Console.WriteLine("The following commands are available:");
			foreach (var command in SimulatorCommands.GetCommandsWithParameters()) {
				Console.WriteLine($"- {command}");
			}
			Console.WriteLine($"- {closeProgramCommand}");
			Console.WriteLine();

			ISimulator simulator = new ToyRobotSimulatorCore.Simulator(tableSize, tableSize, Console.Out, Console.Error);

			while (true) {
				var command = Console.ReadLine();
				if (command.ToUpperInvariant() == closeProgramCommand) {
					break;
				} else {
					try {
						simulator.ExecuteCommand(command);
					} catch (ArgumentException e) {
						Console.Error.WriteLine(e.Message);
					}
				}
			}
		}
	}
}

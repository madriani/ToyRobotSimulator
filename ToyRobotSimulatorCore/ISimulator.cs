namespace ToyRobotSimulatorCore {
	///<summary>
	///The main ToyRobotSimulatorCore interface.
	///Contains the methods to interact with the simulator.
	///</summary>
	public interface ISimulator {
		///<summary>Executes the provided command.</summary>
		///<param name="command">The command to execute.</param>
		///<exception cref="System.ArgumentException">Thrown if <paramref name="command"/> is not a valid command.</exception>
		///<exception cref="System.ArgumentNullException">Thrown if <paramref name="command"/> is null.</exception>
		void ExecuteCommand(string command);
	}
}
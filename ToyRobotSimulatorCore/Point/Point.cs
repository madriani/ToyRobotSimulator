namespace ToyRobotSimulatorCore.Point {
	///<summary>Represents a point in the bidimensional space considered by the simulator.</summary>
	public struct Point {
		///<value>Represents the horizontal position of the point.</value>
		public readonly int x;

		///<value>Represents the vertical position of the point.</value>
		public readonly int y;

		///<summary>Initializes a new instance of the ToyRobotSimulatorCore.Point.Point struct with the provided x and y position.</summary>
		///<param name="x">The horizontal position of the point.</param>
		///<param name="y">The vertical position of the point.</param>
		public Point(int x, int y) {
			this.x = x;
			this.y = y;
		}

		///<summary>Given two Point instances <paramref name="a"/> and <paramref name="b"/>, returns a new Point, whose <c>x = a.x + b.x</c> and <c>y = a.y + b.y</c>.</summary>
		///<returns>Returns a new Point, whose <c>x = a.x + b.x</c> and <c>y = a.y + b.y</c>.</returns>
		public static Point operator +(Point a, Point b) {
			return new Point(a.x + b.x, a.y + b.y);
		}
	}
}
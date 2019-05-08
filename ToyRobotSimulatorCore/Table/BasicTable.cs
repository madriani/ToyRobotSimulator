using System;

namespace ToyRobotSimulatorCore.Table {
	class BasicTable : ITable {
		///<value>The horizontal size of the table.</value>
		public int HorizontalSize { get; }

		///<value>The vertical size of the table.</value>
		public int VerticalSize { get; }

		///<summary>Initializes a new instance of the ToyRobotSimulatorCore.Table.Table class with the provided horizontal and vertical sizes.</summary>
		///<param name ="horizontalSize">The horizontal size of the table.</param>
		///<param name ="verticalSize">The vertical size of the table.</param>
		///<exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="horizontalSize"/> or <paramref name="verticalSize"/> are smaller than 1.</exception>
		public BasicTable(int horizontalSize, int verticalSize) {
			if (horizontalSize < 1 || verticalSize < 1) {
				throw new ArgumentOutOfRangeException($"{nameof(horizontalSize)} and {nameof(verticalSize)} of a BasicTable have to be greater than or equal to 1");
			}
			this.HorizontalSize = horizontalSize;
			this.VerticalSize = verticalSize;
		}

		public bool IsPositionAvailableOnTable(Point.Point position) {
			return ((position.x >= 0 && position.x < HorizontalSize) && (position.y >= 0 && position.y < VerticalSize));
		}
	}
}
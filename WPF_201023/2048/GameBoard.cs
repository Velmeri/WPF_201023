using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WPF_201023._2048
{
	public enum Directions
	{
		UP, DOWN, LEFT, RIGHT
	}

	public class GameBoard
	{
		private Tile[,] board;
		readonly private Random random;

		public GameBoard()
		{
			board = new Tile[4, 4];
			random = new Random();
			InitializeBoard();
		}

		private void InitializeBoard()
		{
			for (int x = 0; x < 4; x++) {
				for (int y = 0; y < 4; y++) {
					board[x, y] = new Tile(0);
				}
			}
			AddNewTile();
			AddNewTile();
		}

		public Tile[,] GetTable()
		{
			return board;
		}

		#region Process

		public void AddNewTile()
		{
			int x, y;
			do {
				x = random.Next(0, 4);
				y = random.Next(0, 4);
			} while (board[x, y] > 0);

			switch (random.Next(0, 2)) {
				case 0:
					board[x, y].Value = 2;
					break; 

				case 1:
					board[x, y].Value = 4;
					break;

				default: 
					break;
			}
		}

		#region Move

		public void Move(Directions direction)
		{
			switch (direction) {
				case Directions.UP: {
						MoveUp();
						break;
					}

				case Directions.DOWN:
					{
						MoveDown();
						break;
					}

				case Directions.LEFT: {
						MoveLeft();
						break;
					}

					case Directions.RIGHT: {
						MoveRight();
						break;
					}

				default: break;
			}
			AddNewTile();
			PrintBoard();
		}

		private void MoveUp()
		{
			for (int col = 0; col < 4; col++) {
				for (int row = 0; row < 4; row++) {
					int nextRow = FindNextRow(row, col);
					if (nextRow == -1) break;

					if (board[row, col] == 0) {
						(board[row, col], board[nextRow, col]) = (board[nextRow, col], board[row, col]);
						row--;
					} else if (board[row, col] == board[nextRow, col]) {
						board[row, col].Merge(board[nextRow, col]);
						board[nextRow, col].Value = 0;
					}
				}
			}
		}

		private void MoveDown()
		{
			for (int col = 0; col < 4; col++) {
				for (int row = 3; row >= 0; row--) {
					int nextRow = FindPreviousRow(row, col);
					if (nextRow == -1) break;

					if (board[row, col] == 0) {
						(board[row, col], board[nextRow, col]) = (board[nextRow, col], board[row, col]);
					} else if (board[row, col] == board[nextRow, col]) {
						board[row, col].Merge(board[nextRow, col]);
						board[nextRow, col].Value = 0;
					}
				}
			}
		}


		private void MoveLeft()
		{
			for (int row = 0; row < 4; row++) {
				for (int col = 0; col < 4; col++) {
					int nextCol = FindNextCol(row, col);
					if (nextCol == -1) break;

					if (board[row, col] == 0) {
						(board[row, col], board[row, nextCol]) = (board[row, nextCol], board[row, col]);
					} else if (board[row, col] == board[row, nextCol]) {
						board[row, col].Merge(board[row, nextCol]);
						board[row, nextCol].Value = 0;
					}
				}
			}
		}


		private void MoveRight()
		{
			for (int row = 0; row < 4; row++) {
				for (int col = 3; col >= 0; col--) {
					int nextCol = FindPreviousCol(row, col);
					if (nextCol == -1) break;

					if (board[row, col] == 0) {
						(board[row, col], board[row, nextCol]) = (board[row, nextCol], board[row, col]);
					} else if (board[row, col] == board[row, nextCol]) {
						board[row, col].Merge(board[row, nextCol]);
						board[row, nextCol].Value = 0;
					}
				}
			}
		}


		private int FindNextRow(int currentRow, int col)
		{
			for (int row = currentRow + 1; row < 4; row++) {
				if (board[row, col] != 0) {
					return row;
				}
			}
			return -1;
		}

		private int FindPreviousRow(int currentRow, int col)
		{
			for (int row = currentRow - 1; row >= 0; row--) {
				if (board[row, col] != 0) {
					return row;
				}
			}
			return -1;
		}

		private int FindPreviousCol(int row, int currentCol)
		{
			for (int col = currentCol - 1; col >= 0; col--) {
				if (board[row, col] != 0) {
					return col;
				}
			}
			return -1;
		}

		private int FindNextCol(int row, int currentCol)
		{
			for (int col = currentCol + 1; col < 4; col++) {
				if (board[row, col] != 0) {
					return col;
				}
			}
			return -1;
		}


		#endregion

		#endregion

		#region Conditions

		public bool IsGameOver()
		{
			foreach (var tile in board)
				if (tile == 0) return false;
			return true;
		}

		public bool HasWon()
		{
			foreach (var tile in board) 
				if (tile == 2048) return true;
			return false;
		}

		#endregion

		#region Debug
		public void PrintBoard()
		{
			for (int row = 0; row < board.GetLength(0); row++) {
				string rowOutput = "";
				for (int col = 0; col < board.GetLength(1); col++) {
					rowOutput += board[row, col] + "\t";
				}

				Debug.WriteLine(rowOutput);
			}
			Debug.WriteLine(new string('-', 20));
		}

		#endregion

		#region Operators

		public static implicit operator Tile[,] (GameBoard obj)
		{
			return obj.board;
		}

		public Tile this[int x, int y]
		{
			get { return board[x, y]; }
			set { board[x, y] = value; }
		}

		#endregion
	}

}
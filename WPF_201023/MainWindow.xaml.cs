using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_201023._2048;

namespace WPF_201023
{
	public partial class MainWindow : Window
	{
		GameBoard board;

		bool isGameWon;
		bool isGameLose;

		public MainWindow()
		{
			InitializeComponent();

			board = new GameBoard();
			isGameLose = false;
			isGameWon = false;

			DrawGameBoard();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if(!isGameWon && !isGameLose) {
				switch (e.Key) {
					case Key.W:
					case Key.Up:
						board.Move(Directions.UP);
						DrawGameBoard();
						UpdateStatus();
						break;
					case Key.A:
					case Key.Left:
						board.Move(Directions.LEFT);
						DrawGameBoard();
						UpdateStatus();
						break;
					case Key.S:
					case Key.Down:
						board.Move(Directions.DOWN);
						DrawGameBoard();
						UpdateStatus();
						break;
					case Key.D:
					case Key.Right:
						board.Move(Directions.RIGHT);
						DrawGameBoard();
						UpdateStatus();
						break;
				}
			}
		}

		private void DrawGameBoard()
		{
			int size = 100; // Размер каждого квадрата
			int margin = 10; // Отступ между квадратами

			GameCanvas.Children.Clear(); // Очищаем предыдущие элементы

			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 4; j++) {
					// Создаем квадрат
					Rectangle rect = new Rectangle
					{
						Width = size,
						Height = size,
						Fill = GetBrushFromValue(board[i, j])
					};

					Canvas.SetLeft(rect, j * (size + margin));
					Canvas.SetTop(rect, i * (size + margin));
					GameCanvas.Children.Add(rect);

					// Создаем текстовый блок
					TextBlock text = new TextBlock
					{
						Text = board[i, j].ToString(),
						Foreground = Brushes.White, // Цвет текста
						FontWeight = FontWeights.Bold,
						FontSize = 24,
						Width = size,
						Height = size,
						TextAlignment = TextAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center
					};

					Canvas.SetLeft(text, j * (size + margin));
					Canvas.SetTop(text, i * (size + margin));
					GameCanvas.Children.Add(text);
				}
			}
		}

		private Brush GetBrushFromValue(int value)
		{
			// Здесь вы можете определить цвета в зависимости от значений в массиве
			switch (value) {
				case 0: return Brushes.Black; // Фон пустой ячейки
				case 2: return Brushes.Blue;
				case 4: return Brushes.Green;
				case 8: return Brushes.Red;
				case 16: return Brushes.Cyan;
				case 32: return Brushes.Magenta;
				case 64: return Brushes.Yellow;
				case 128: return Brushes.Orange;
				case 256: return Brushes.Purple;
				case 512: return Brushes.Pink;
				case 1024: return Brushes.Lime;
				case 2048: return Brushes.Silver;
				// и так далее для других значений
				default: return Brushes.Gray;
			}
		}

		private void UpdateStatus()
		{
			if (board.HasWon()) {
				isGameWon = true;
				MessageBox.Show("Ты победил!!!");
			}
			if (board.IsGameOver()) {
				isGameLose = true;
				MessageBox.Show("Ты проиграл((");
			}
		}
	}
}

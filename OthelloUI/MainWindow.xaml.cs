using OthelloApplication.UseCases;
using OthelloLogic;
using System.Windows;
using System.Windows.Controls;

namespace OthelloUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];

        private readonly AddOrMoveBoardPieceUseCase _addOrMoveBoardPieceUseCase;

        private GameState _gameState;
        private Position selectedPos = null;

        public MainWindow(GameState gameState, AddOrMoveBoardPieceUseCase addOrMoveBoardPieceUseCase)
        {
            InitializeComponent();
            InitializeBoard();

            _gameState = gameState;

            DrawBoard(gameState.Board);

            _addOrMoveBoardPieceUseCase = addOrMoveBoardPieceUseCase;
            _addOrMoveBoardPieceUseCase.MovimentProcessed += OnMovimentProcessed;
        }
        
        public void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);
                }
            }
        }

        public void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r, c].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            return new Position(row, col);
        }

        public void OnFromPositionSelected(Position pos)
        {
            selectedPos = pos;
        }

        public async Task OnToPositionSelected(Position pos)
        {
            if (selectedPos == pos)
            {
                if(_gameState.Board.IsEmpty(pos))
                {
                    selectedPos = null;
                }
                else
                {
                    selectedPos = null;
                    // chama useCase de toggle
                }
            }
            else
            {
                var move = new Move(selectedPos, pos);
                await _addOrMoveBoardPieceUseCase.HandleMovimentAsync(move);

                selectedPos = null;
            }
        }

        private void OnMovimentProcessed(object sender, MovimentProcessedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.IsSucesses)
                {
                    DrawBoard(_gameState.Board);
                }
                else
                {
                    Console.WriteLine(e.ErrorMessage);
                    // mostrar mensagem de que o movimento foi proibido usando a mensagem de erro
                }
            });
        }

        private void OnToggleProcessed(object sender)
        {
            Dispatcher.Invoke(() =>
            {
                DrawBoard(_gameState.Board);
            });
        }
    }
}
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

        private GameState gameState;
        private Position selectedPos = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Player.White, Board.Initial());

            DrawBoard(gameState.Board);
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

        public void OnToPositionSelected(Position pos)
        {
            if (selectedPos == pos)
            {
                if(gameState.Board.IsEmpty(pos))
                {
                    selectedPos = null;
                }
                else
                {
                    selectedPos = null;
                    HandleToggle(pos);
                }
            }
            else
            {
                if (gameState.CanMovePiece(pos))
                {
                    var move = new Move(selectedPos, pos);
                    HandleMove(move);
                    selectedPos = null;
                }
            }
        }

        public void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
        }

        public void HandleToggle(Position pos)
        {
            gameState.Board[pos].TogglePieceSide();
            DrawBoard(gameState.Board);
        }
    }
}
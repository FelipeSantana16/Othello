using OthelloApplication.UseCases.AddBoardPiece;
using OthelloApplication.UseCases.Chat;
using OthelloApplication.UseCases.MoveBoardPiece;
using OthelloApplication.UseCases.ShiftTurn;
using OthelloApplication.UseCases.TogglePieceSide;
using OthelloLogic;
using OthelloLogic.Messages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OthelloUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];

        private readonly AddBoardPieceUseCase _addBoardPieceUseCase;
        private readonly MoveBoardPieceUseCase _moveBoardPieceUseCase;
        private readonly TogglePieceSideUseCase _togglePieceSideUseCase;
        private readonly ShiftTurnUseCase _shiftTurnUseCase;
        private readonly ChatUseCase _chatUseCase;

        private GameState _gameState;
        private Position selectedPos = null;

        public bool _addModeOn;

        public MainWindow(
            GameState gameState,
            AddBoardPieceUseCase addBoardPieceUseCase,
            MoveBoardPieceUseCase moveBoardPieceUseCase,
            TogglePieceSideUseCase togglePieceSideUseCase,
            ShiftTurnUseCase shiftTurnUseCase,
            ChatUseCase chatUseCase
            )
        {
            InitializeComponent();
            InitializeBoard();

            _gameState = gameState;
            _addModeOn = false;

            DrawCurrentTurn();
            DrawBoard(gameState.Board);

            _addBoardPieceUseCase = addBoardPieceUseCase;
            _addBoardPieceUseCase.AddProcessed += OnAddProcessed;

            _moveBoardPieceUseCase = moveBoardPieceUseCase;
            _moveBoardPieceUseCase.MovimentProcessed += OnMovimentProcessed;

            _togglePieceSideUseCase = togglePieceSideUseCase;
            _togglePieceSideUseCase.ToggleProcessed += OnToggleProcessed;

            _shiftTurnUseCase = shiftTurnUseCase;
            _shiftTurnUseCase.ShiftTurnProcessed += OnShiftTurnProcessed;

            _chatUseCase = chatUseCase;
            _chatUseCase.MessageReceived += OnMessageReceived;

            // desistencia
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

        private async void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (_addModeOn)
            {
                var input = new AddBoardPieceUseCaseInput()
                {
                    Player = Player.White,
                    Position = pos
                };

                await _addBoardPieceUseCase.Handle(input, default);
                _addModeOn = false;
                return;
            }

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private void AddPieceButton_Click(object sender, RoutedEventArgs e)
        {
            _addModeOn = true;
        }

        private async void FinishTurn_Click(object sender, RoutedEventArgs e)
        {
            var input = new ShiftTurnUseCaseInput() { Player = Player.White };
            await _shiftTurnUseCase.Handle(input, default);
            DrawCurrentTurn();
        }

        private void DrawCurrentTurn()
        {
            if (_gameState.CurrentPlayer == Player.White)
            {
                TurnIndicatorImage.Source = new BitmapImage(new Uri("Assets/pieceWhite.png", UriKind.Relative));
            }
            else
            {
                TurnIndicatorImage.Source = new BitmapImage(new Uri("Assets/pieceBlack.png", UriKind.Relative));
            }
        }

        private void SurrenderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Você desistiu do jogo!", "Desistência", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string message = ChatInput.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {                
                var input = new ChatUseCaseInput()
                {
                    Player = Player.White,
                    Message = message
                };

                await _chatUseCase.Handle(input, default);
                ChatInput.Clear();
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
                    var input = new TogglePieceSideUseCaseInput()
                    {
                        Player = Player.White,
                        Position = pos
                    };

                    await _togglePieceSideUseCase.Handle(input, default);
                }
            }
            else
            {
                var move = new Move(selectedPos, pos);
                
                var input = new MoveBoardPieceUseCaseInput()
                {
                    Player = Player.White,
                    Move = move
                };

                await _moveBoardPieceUseCase.Handle(input, default);

                selectedPos = null;
            }
        }

        private void OnAddProcessed(object sender, AddProcessedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.IsSuccess)
                {
                    DrawBoard(_gameState.Board);
                }
                else
                {
                    MessageBox.Show($"Adição não realizada. Justificativa: {e.ErrorMessage}", "Adição", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }

        private void OnMovimentProcessed(object sender, MovimentProcessedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.IsSuccess)
                {
                    DrawBoard(_gameState.Board);
                }
                else
                {
                    MessageBox.Show($"Movimento não realizado. Justificativa: {e.ErrorMessage}", "Movimentação", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }

        private void OnToggleProcessed(object sender, ToggleProcessedEventArgs e)
        {
            if (e.IsSuccess)
            {
                DrawBoard(_gameState.Board);
            }
            else
            {
                MessageBox.Show($"Inversão de cor não realizada. Justificativa: {e.ErrorMessage}", "Inversão", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnShiftTurnProcessed(object sender, ShiftTurnEventArgs e)
        {
            if (e.IsSuccess)
            {
                DrawBoard(_gameState.Board);
            }
            else
            {
                MessageBox.Show($"Não foi possível finalizar turno. Justificativa: {e.ErrorMessage}", "Finalizar turno", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var messageBlock = new TextBlock
            {
                Text = $"{e.Player.ToString()}: {e.Message}",
                Foreground = Brushes.White,
                Margin = new Thickness(0, 2, 0, 2)
            };

            ChatMessages.Children.Add(messageBlock);
        }
    }
}
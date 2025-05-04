using MediatR;
using OthelloApplication.UseCases.AddBoardPiece;
using OthelloApplication.UseCases.CaptureBoardPiece;
using OthelloApplication.UseCases.Chat;
using OthelloApplication.UseCases.MoveBoardPiece;
using OthelloApplication.UseCases.ShiftTurn;
using OthelloApplication.UseCases.TogglePieceSide;
using OthelloLogic;
using OthelloLogic.Interfaces;
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
        private readonly Image[,] pieceImages = new Image[5, 5];

        private readonly IMediator _mediator;

        private readonly IDomainEventDispatcher _domainEventDispatcher;

        private GameState _gameState;
        private Position selectedPos = null;

        public bool _addModeOn;

        public MainWindow(
            GameState gameState,
            IMediator mediator,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            InitializeComponent();
            InitializeBoard();

            _gameState = gameState;
            _addModeOn = false;

            DrawCurrentTurn();
            DrawBoard(gameState.Board);

            _mediator = mediator;

            _domainEventDispatcher = domainEventDispatcher;

            _domainEventDispatcher.AddProcessed += OnAddProcessed;
            _domainEventDispatcher.MovimentProcessed += OnMovimentProcessed;
            _domainEventDispatcher.ToggleProcessed += OnToggleProcessed;
            _domainEventDispatcher.ShiftTurnProcessed += OnShiftTurnProcessed;
            _domainEventDispatcher.MessageReceived += OnMessageReceived;
            _domainEventDispatcher.SurrenderProcessed += OnSurrenderReceived;
            _domainEventDispatcher.CaptureProcessed += OnCaptureReceived;
        }

        public void InitializeBoard()
        {
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
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
                    Player = _gameState.LocalPlayer,
                    Position = pos
                };

                await _mediator.Send(input, new CancellationToken());
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
            var input = new ShiftTurnUseCaseInput() { Player = _gameState.LocalPlayer };
            await _mediator.Send(input, new CancellationToken());
            DrawCurrentTurn();
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
                    Player = _gameState.LocalPlayer,
                    Message = message
                };

                await _mediator.Send(input, new CancellationToken());
                ChatInput.Clear();
            }
        }

        public void DrawBoard(Board board)
        {
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r, c].Source = Images.GetImage(piece);
                }
            }
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

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 5;
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
                if (_gameState.Board.IsEmpty(pos))
                {
                    selectedPos = null;
                }
                else
                {
                    selectedPos = null;
                    var input = new CaptureBoardPieceUseCaseInput()
                    {
                        Player = _gameState.LocalPlayer,
                        Position = pos
                    };

                    await _mediator.Send(input, new CancellationToken());
                }
            }
            else
            {
                var move = new Move(selectedPos, pos);

                var input = new MoveBoardPieceUseCaseInput()
                {
                    Player = _gameState.LocalPlayer,
                    Move = move
                };

                await _mediator.Send(input, new CancellationToken());

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
                DrawCurrentTurn();
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
                Foreground = e.Player == Player.White ? Brushes.White : Brushes.Black,
                Margin = new Thickness(0, 2, 0, 2)
            };

            ChatMessages.Children.Add(messageBlock);
        }

        private void OnSurrenderReceived(object sender, SurrenderEventArgs e)
        {
            MessageBox.Show($"Winner: {e.Player.Opponent()}!");
        }

        private void OnCaptureReceived(object sender, CaptureProcessedEvent e)
        {
            if (e.IsSuccess)
            {
                DrawBoard(_gameState.Board);
            }
            else
            {
                MessageBox.Show($"Captura não realizada. Justificativa: {e.ErrorMessage}", "Captura", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
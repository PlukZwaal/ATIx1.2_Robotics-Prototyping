using Avans.StatisticalRobot;
using System;
using System.Threading;
using System.Threading.Tasks;

public class RobotController
{
    private bool _isRunning;
    private CancellationTokenSource _cancellationTokenSource;
    private Task _robotTask;
    private readonly MotorController _motorController;
    private readonly SensorManager _sensorManager;

    public RobotController(SensorManager sensorManager, MotorController motorController)
    {
        _isRunning = false;
        _sensorManager = sensorManager;
        _motorController = motorController;
    }

    public void StartRobot()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _isRunning = true;
        _robotTask = MoveRobotAsync(_cancellationTokenSource.Token);
    }

    public void StopRobot()
    {
        _isRunning = false;
        _cancellationTokenSource?.Cancel();
        _motorController.StopMotors();
    }

    public void ForwardRobot() => _motorController.MoveForward();

    public void BackwardRobot() => _motorController.MoveBackward();

    public void LeftRobot() => _motorController.TurnLeft();

    public void RightRobot() => _motorController.TurnRight();

    private async Task MoveRobotAsync(CancellationToken cancellationToken)
    {
        while (_isRunning && !cancellationToken.IsCancellationRequested)
        {
            int distance = _sensorManager.GetDistance();

            if (distance < 10)
            {
                _motorController.Reverse();
                await Task.Delay(1000);
                _motorController.TurnRight();
            }
            else if (distance < 30)
            {
                _motorController.TurnRight();
            }
            else
            {
                _motorController.MoveForward();
            }

            await Task.Delay(500);
        }
    }
}
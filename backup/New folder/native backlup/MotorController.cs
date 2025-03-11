using Avans.StatisticalRobot;

public class MotorController
{
    public void MoveForward() => Robot.Motors(100, 100);

    public void MoveBackward() => Robot.Motors(-100, -100);

    public void TurnLeft()
    {
        Robot.Motors(-50, 0);
        Robot.Wait(1000);
    }

    public void TurnRight()
    {
        Robot.Motors(0, -50);
        Robot.Wait(1000);
    }

    public void Reverse() => Robot.Motors(-75, -75);

    public void StopMotors() => Robot.Motors(0, 0);
}

using Avans.StatisticalRobot;

public class MotorController
{
    private short leftMotorSpeed = 0;
    private short rightMotorSpeed = 0;

    public void MoveForward()
    {
        leftMotorSpeed = 100;
        rightMotorSpeed = 100;
        Robot.Motors(leftMotorSpeed, rightMotorSpeed);
    }

    public void MoveBackward()
    {
        leftMotorSpeed = -100;
        rightMotorSpeed = -100;
        Robot.Motors(leftMotorSpeed, rightMotorSpeed);
    }

    public void TurnLeft()
    {
        leftMotorSpeed = -50;
        rightMotorSpeed = 0;
        Robot.Motors(leftMotorSpeed, rightMotorSpeed);
        Robot.Wait(1000);
    }

    public void TurnRight()
    {
        leftMotorSpeed = 0;
        rightMotorSpeed = -50;
        Robot.Motors(leftMotorSpeed, rightMotorSpeed);
        Robot.Wait(1000);
    }

    public void Reverse()
    {
        leftMotorSpeed = -75;
        rightMotorSpeed = -75;
        Robot.Motors(leftMotorSpeed, rightMotorSpeed);
    }

    public void StopMotors()
    {
        leftMotorSpeed = 0;
        rightMotorSpeed = 0;
        Robot.Motors(leftMotorSpeed, rightMotorSpeed);
    }

    public (int leftSpeed, int rightSpeed) GetMotorSpeed()
    {
        return (leftMotorSpeed, rightMotorSpeed);
    }
}
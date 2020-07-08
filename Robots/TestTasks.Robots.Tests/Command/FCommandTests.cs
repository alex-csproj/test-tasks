using FakeItEasy;
using FluentAssertions;
using System;
using TestTasks.Robots.Command;
using TestTasks.Robots.Contracts;
using Xunit;

namespace TestTasks.Robots.Tests.Command
{
    public class FCommandTests
    {
        [Fact]
        public void Ctor_IfRobotIsNotNull_DoesNotThrow()
        {
            // Arrange
            IRobot robot = A.Dummy<IRobot>();

            // Act
            Action act = () => new FCommand(robot);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Ctor_IfRobotIsNull_Throws()
        {
            // Arrange
            IRobot robot = null;

            // Act
            Action act = () => new FCommand(robot);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'robot')");
        }

        [Theory]
        [InlineData(Orientation.N, 0, 1)]
        [InlineData(Orientation.E, 1, 0)]
        [InlineData(Orientation.S, 0, -1)]
        [InlineData(Orientation.W, -1, 0)]
        public void Execute_MovesRobotOneStepForward(Orientation orientation, int x, int y)
        {
            // Arrange
            IRobot robot = A.Fake<IRobot>();
            robot.LastPosition = Position.Origin;
            robot.Orientation = orientation;

            FCommand sut = new FCommand(robot);

            // Act
            sut.Execute();

            // Assert
            robot.Orientation.Should().Be(orientation);
            robot.LastPosition.X.Should().Be(x);
            robot.LastPosition.Y.Should().Be(y);
        }
    }
}
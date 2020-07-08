using FakeItEasy;
using FluentAssertions;
using System;
using TestTasks.Robots.Command;
using TestTasks.Robots.Contract;
using Xunit;

namespace TestTasks.Robots.Tests.Command
{
    public class RCommandTests
    {
        [Fact]
        public void Ctor_IfRobotIsNotNull_DoesNotThrow()
        {
            // Arrange
            IRobot robot = A.Dummy<IRobot>();

            // Act
            Action act = () => new RCommand(robot);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Ctor_IfRobotIsNull_Throws()
        {
            // Arrange
            IRobot robot = null;

            // Act
            Action act = () => new RCommand(robot);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'robot')");
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.E)]
        [InlineData(Orientation.E, Orientation.S)]
        [InlineData(Orientation.S, Orientation.W)]
        [InlineData(Orientation.W, Orientation.N)]
        public void Execute_RotatesRobotToRight(Orientation start, Orientation end)
        {
            // Arrange
            Position origin = Position.Origin;

            IRobot robot = A.Fake<IRobot>();
            robot.LastPosition = origin;
            robot.Orientation = start;

            RCommand sut = new RCommand(robot);

            // Act
            sut.Execute();

            // Assert
            robot.Orientation.Should().Be(end);
            robot.LastPosition.Should().Be(origin);
        }
    }
}
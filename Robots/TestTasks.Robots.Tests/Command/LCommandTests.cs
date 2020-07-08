using FakeItEasy;
using FluentAssertions;
using System;
using TestTasks.Robots.Command;
using TestTasks.Robots.Contract;
using Xunit;

namespace TestTasks.Robots.Tests.Command
{
    public class LCommandTests
    {
        [Fact]
        public void Ctor_IfRobotIsNotNull_DoesNotThrow()
        {
            // Arrange
            IRobot robot = A.Dummy<IRobot>();

            // Act
            Action act = () => new LCommand(robot);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Ctor_IfRobotIsNull_Throws()
        {
            // Arrange
            IRobot robot = null;

            // Act
            Action act = () => new LCommand(robot);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'robot')");
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.W)]
        [InlineData(Orientation.W, Orientation.S)]
        [InlineData(Orientation.S, Orientation.E)]
        [InlineData(Orientation.E, Orientation.N)]
        public void Execute_RotatesRobotToLeft(Orientation start, Orientation end)
        {
            // Arrange
            Position origin = Position.Origin;

            IRobot robot = A.Fake<IRobot>();
            robot.LastPosition = origin;
            robot.Orientation = start;

            LCommand sut = new LCommand(robot);

            // Act
            sut.Execute();

            // Assert
            robot.Orientation.Should().Be(end);
            robot.LastPosition.Should().Be(origin);
        }
    }
}
using FluentAssertions;
using System;
using TestTasks.Robots.Contract;
using Xunit;

namespace TestTasks.Robots.Tests
{
    public class SimpleRobotTests
    {
        private static readonly Orientation defaultOrientation = Orientation.N;
        private static readonly Position defaultPosition = Position.Origin;
        private static readonly Position innerPosition = new Position(2, 2);
        private static readonly Position outerPosition = new Position(3, 3);

        private readonly IArea defaultArea = new RectangularArea(new Position(-2, -2), new Position(2, 2));

        private SimpleRobot CreateSut() =>
            new SimpleRobot() { Area = defaultArea, LastPosition = defaultPosition, Orientation = defaultOrientation };

        [Fact]
        public void SetLastPosition_WithArea_DoesNotThrow()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            Action act = () => sut.LastPosition = defaultPosition;

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void SetLastPosition_WithoutArea_Throws()
        {
            // Arrange
            SimpleRobot sut = CreateSut();
            sut.Area = null;

            // Act
            Action act = () => sut.LastPosition = defaultPosition;

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage($"Can not set position without area.");
        }

        [Fact]
        public void Status_ByDefault_IsNormal()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Assert
            sut.Status.Should().Be(Status.Normal);
        }

        [Fact]
        public void SetLastPosition_IfDoesNotMoveOverEdge_ChangesLastPosition()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            Position start = sut.LastPosition;
            sut.LastPosition = innerPosition;

            // Assert
            sut.LastPosition.Should().Be(innerPosition);
            sut.LastPosition.Should().NotBe(start);
        }

        [Fact]
        public void SetLastPosition_IfMovesOverEdge_DoesNotChangeLastPosition()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            Position start = sut.LastPosition;
            sut.LastPosition = outerPosition;

            // Assert
            sut.LastPosition.Should().Be(start);
            sut.LastPosition.Should().NotBe(outerPosition);
        }

        [Fact]
        public void SetLastPosition_IfMovesOverEdge_SetsStatusToLost()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            sut.LastPosition = outerPosition;

            // Assert
            sut.Status.Should().Be(Status.Lost);
        }

        [Fact]
        public void SetLastPosition_IfDoesNotMoveOverEdge_DoesNotAddScentPosition()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            sut.LastPosition = innerPosition;

            // Assert
            sut.Area.ScentPosition.Should().BeEmpty();
        }

        [Fact]
        public void SetLastPosition_IfMovesOverEdge_AddsScentPosition()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            sut.LastPosition = outerPosition;

            // Assert
            sut.Area.ScentPosition.Should().Equal(outerPosition);
        }

        [Fact]
        public void SetLastPosition_IfLost_DoesNotChangeLastPosition()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            Position start = sut.LastPosition;
            sut.LastPosition = outerPosition;
            sut.LastPosition = innerPosition;

            // Assert
            sut.LastPosition.Should().Be(start);
            sut.LastPosition.Should().NotBe(innerPosition);
        }

        [Fact]
        public void SetLastPosition_IfMovesToScentPosition_IgnoresMove()
        {
            // Arrange
            SimpleRobot robot = CreateSut();
            robot.LastPosition = outerPosition;

            SimpleRobot sut = CreateSut();

            // Act
            Position start = sut.LastPosition;
            sut.LastPosition = outerPosition;
            sut.LastPosition = innerPosition;

            // Assert
            sut.LastPosition.Should().Be(innerPosition);
            sut.LastPosition.Should().NotBe(start);
        }

        [Fact]
        public void _SetLastPosition_IfLost_DoesNotChangeLastPosition()
        {
            // Arrange
            SimpleRobot sut = CreateSut();

            // Act
            Orientation start = sut.Orientation;
            sut.LastPosition = outerPosition;
            sut.Orientation = Orientation.E;

            // Assert
            sut.Orientation.Should().Be(start);
            sut.Orientation.Should().NotBe(Orientation.E);
        }
    }
}
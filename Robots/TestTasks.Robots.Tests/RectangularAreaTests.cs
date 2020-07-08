using FluentAssertions;
using System;
using Xunit;

namespace TestTasks.Robots.Tests
{
    public class RectangularAreaTests
    {
        private static RectangularArea CreateSut() =>
            new RectangularArea(new Position(-2, -2), new Position(2, 2));

        [Fact]
        public void Ctor_IfLowerLeftAndUpperRightNotNull_DoesNotThrow()
        {
            // Arrange
            Position lowerLeft = Position.Origin;
            Position upperRight = Position.Origin;

            // Act
            Action act = () => new RectangularArea(lowerLeft, upperRight);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Ctor_IfLowerLeftNull_Throws()
        {
            // Arrange
            Position lowerLeft = null;
            Position upperRight = Position.Origin;

            // Act
            Action act = () => new RectangularArea(lowerLeft, upperRight);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'lowerLeft')");
        }

        [Fact]
        public void Ctor_IfUpperRightNull_Throws()
        {
            // Arrange
            Position lowerLeft = Position.Origin;
            Position upperRight = null;

            // Act
            Action act = () => new RectangularArea(lowerLeft, upperRight);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'upperRight')");
        }

        [Fact]
        public void Contains_IfPositionNotNull_DoesNotThrow()
        {
            // Arrange
            Position position = Position.Origin;
            RectangularArea sut = CreateSut();

            // Act
            Action act = () => sut.Contains(position);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Contains_IfPositionNull_Throws()
        {
            // Arrange
            Position position = null;
            RectangularArea sut = CreateSut();

            // Act
            Action act = () => sut.Contains(position);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'position')");
        }

        [Fact]
        public void Contains_IfContainsPosition_ReturnsTrue()
        {
            // Arrange
            Position position = new Position(1, 1);
            RectangularArea sut = CreateSut();

            // Act
            bool contains = sut.Contains(position);

            // Assert
            contains.Should().BeTrue();
        }

        [Fact]
        public void Contains_IfDoesNotContainPosition_ReturnsFalse()
        {
            // Arrange
            Position position = new Position(-10, 1);
            RectangularArea sut = CreateSut();

            // Act
            bool contains = sut.Contains(position);

            // Assert
            contains.Should().BeFalse();
        }
    }
}
using FluentAssertions;
using System;
using TestTasks.Robots.Contracts;
using TestTasks.Robots.Parse;
using Xunit;

namespace TestTasks.Robots.Tests.Parse
{
    public class SimpleStateParserTests
    {
        private static SimpleStateParser CreateSut() =>
            new SimpleStateParser();

        [Fact]
        public void Parse_IfStateNotNull_DoesNotThrow()
        {
            // Arrange
            string state = string.Empty;
            SimpleStateParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(state);

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void Parse_IfStateNull_Throws()
        {
            // Arrange
            string state = null;
            SimpleStateParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(state);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'state')");
        }

        [Fact]
        public void Parse_IfSupportedFormat_DoesNotThrow()
        {
            // Arrange
            string state = "1 2 E";
            SimpleStateParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(state);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1 2")]
        [InlineData("1 2 3 4")]
        [InlineData("A _ _")]
        [InlineData("1 A _")]
        [InlineData("1 2 A")]
        [InlineData("1 2 e")]
        public void Parse_IfNotSupportedFormat_Throws(string state)
        {
            // Arrange
            SimpleStateParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(state);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"Unsupported format. (Parameter 'state')");
        }

        [Fact]
        public void Parse_Returns()
        {
            // Arrange
            string state = "1 2 E";
            SimpleStateParser sut = CreateSut();

            // Act
            (Position Position, Orientation Orientation) result = sut.Parse(state);

            // Assert
            result.Orientation.Should().Be(Orientation.E);

            Position expectedPosition = new Position(1, 2);
            result.Position.Should().Be(expectedPosition);
        }
    }
}
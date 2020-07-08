using FluentAssertions;
using System;
using TestTasks.Robots.Contract;
using TestTasks.Robots.Parse;
using Xunit;

namespace TestTasks.Robots.Tests.Parse
{
    public class RectangularAreaParserTests
    {
        private static RectangularAreaParser CreateSut() =>
            new RectangularAreaParser();

        [Fact]
        public void Parse_IfAreaNotNull_DoesNotThrow()
        {
            // Arrange
            string area = string.Empty;
            RectangularAreaParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(area);

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void Parse_IfAreaNull_Throws()
        {
            // Arrange
            string area = null;
            RectangularAreaParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(area);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'area')");
        }

        [Fact]
        public void Parse_IfSupportedFormat_DoesNotThrow()
        {
            // Arrange
            string area = "1 2";
            RectangularAreaParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(area);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1 2 3")]
        [InlineData("A 1")]
        [InlineData("1 A")]
        public void Parse_IfNotSupportedFormat_Throws(string area)
        {
            // Arrange
            RectangularAreaParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(area);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"Unsupported format. (Parameter 'area')");
        }

        [Fact]
        public void Parse_ReturnsArea()
        {
            // Arrange
            string area = "1 2";
            RectangularAreaParser sut = CreateSut();

            // Act
            IArea result = sut.Parse(area);

            // Assert
            RectangularArea expectedResult = new RectangularArea(Position.Origin, new Position(1, 2));
            result.Should().Be(expectedResult);
        }
    }
}
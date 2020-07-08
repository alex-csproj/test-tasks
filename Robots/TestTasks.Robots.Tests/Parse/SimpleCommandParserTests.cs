using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.Robots.Command;
using TestTasks.Robots.Contract;
using TestTasks.Robots.Parse;
using Xunit;

namespace TestTasks.Robots.Tests.Parse
{
    public class SimpleCommandParserTests
    {
        private static SimpleCommandParser CreateSut() =>
            new SimpleCommandParser(A.Dummy<IRobot>());

        [Fact]
        public void Ctor_IfRobotNotNull_DoesNotThrow()
        {
            // Arrange
            IRobot robot = A.Fake<IRobot>();

            // Act
            Action act = () => new SimpleCommandParser(robot);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Ctor_IfRobotNull_Throws()
        {
            // Arrange
            IRobot robot = null;

            // Act
            Action act = () => new SimpleCommandParser(robot);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'robot')");
        }

        [Fact]
        public void Parse_IfCommandNotNull_DoesNotThrow()
        {
            // Arrange
            string commands = string.Empty;
            SimpleCommandParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(commands);

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void Parse_IfCommandNull_Throws()
        {
            // Arrange
            string commands = null;
            SimpleCommandParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(commands);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter 'commands')");
        }

        [Fact]
        public void Parse_IfSupportedFormat_DoesNotThrow()
        {
            // Arrange
            string commands = "RFRFRFRF";
            SimpleCommandParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(commands);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData("R F")]
        [InlineData("AR")]
        [InlineData("RA")]
        public void Parse_IfNotSupportedFormat_Throw(string commands)
        {
            // Arrange
            SimpleCommandParser sut = CreateSut();

            // Act
            Action act = () => sut.Parse(commands);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"Unsupported format. (Parameter 'commands')");
        }

        [Fact]
        public void Parse_ReturnsCommands()
        {
            // Arrange
            string commands = "RFLF";
            SimpleCommandParser sut = CreateSut();

            // Act
            IReadOnlyCollection<IRobotCommand> resultCommands = sut.Parse(commands);

            // Assert
            Type[] expectedTypes = new Type[] { typeof(RCommand), typeof(FCommand), typeof(LCommand), typeof(FCommand) };
            Type[] actualTypes = resultCommands.Select(command => command.GetType()).ToArray();
            actualTypes.Should().Equal(expectedTypes);
        }
    }
}
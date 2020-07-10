using FluentAssertions;
using System;
using TestTasks.T9.Contract;
using Xunit;

namespace TestTasks.T9.Tests
{
    public class SimpleT9KeypadTests
    {
        [Fact]
        public void GetButton_IfButtonDoesNotExist_Throws()
        {
            // Arrange
            SimpleT9Keypad sut = new SimpleT9Keypad();

            // Act
            Action act = () => sut.GetButton('*');

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Button with given symbol '*' does not exist.");
        }

        [Fact]
        public void GetButton_IfButtonExist_DoesNotThrow()
        {
            // Arrange
            SimpleT9Keypad sut = new SimpleT9Keypad();

            // Act
            Action act = () => sut.GetButton('e');

            // Assert
            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void GetButton_ReturnsExpectedButton()
        {
            // Arrange
            SimpleT9Keypad sut = new SimpleT9Keypad();

            // Act
            IT9Button button = sut.GetButton('e');

            // Assert
            button.Label.Should().Be('3');
        }

        [Fact]
        public void TryGetButton_IfButtonDoesNotExist_ReturnsFalse()
        {
            // Arrange
            SimpleT9Keypad sut = new SimpleT9Keypad();

            // Act
            bool exists = sut.TryGetButton('*', out IT9Button button);

            // Assert
            exists.Should().BeFalse();
            button.Should().BeNull();
        }

        [Fact]
        public void TryGetButton_IfButtonExist_ReturnsTrue()
        {
            // Arrange
            SimpleT9Keypad sut = new SimpleT9Keypad();

            // Act
            bool exists = sut.TryGetButton('e', out IT9Button button);

            // Assert
            exists.Should().BeTrue();
            button.Label.Should().Be('3');
        }
    }
}
using FakeItEasy;
using FluentAssertions;
using System;
using TestTasks.T9.Contract;
using Xunit;

namespace TestTasks.T9.Tests
{
    public class SimpleTextToT9ConverterTests
    {
        private IT9Keypad CreateT9Keypad()
        {
            IT9Button button1 = A.Fake<IT9Button>();
            A.CallTo(() => button1.Label).Returns('1');
            A.CallTo(() => button1.Symbols).Returns(new[] { 'a', 'b' });
            A.CallTo(() => button1.GetPressCount('a')).Returns(1);
            A.CallTo(() => button1.GetPressCount('b')).Returns(2);

            IT9Button button2 = A.Fake<IT9Button>();
            A.CallTo(() => button2.Label).Returns('2');
            A.CallTo(() => button2.Symbols).Returns(new[] { 'c', 'd' });
            A.CallTo(() => button2.GetPressCount('c')).Returns(1);
            A.CallTo(() => button2.GetPressCount('d')).Returns(2);

            IT9Keypad keypad = A.Fake<IT9Keypad>();
            A.CallTo(() => keypad.GetButton('a')).Returns(button1);
            A.CallTo(() => keypad.GetButton('b')).Returns(button1);
            A.CallTo(() => keypad.GetButton('c')).Returns(button2);
            A.CallTo(() => keypad.GetButton('d')).Returns(button2);

            return keypad;
        }

        [Fact]
        public void Ctor_IfKeypadNull_Throws()
        {
            // Act
            Action act = () => new SimpleTextToT9Converter(null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'keypad')");
        }

        [Fact]
        public void Ctor_IfKeypadNotNull_DoesNotThrow()
        {
            // Act
            Action act = () => new SimpleTextToT9Converter(A.Dummy<IT9Keypad>());

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }
        [Fact]
        public void Convert_IfTextNull_Throws()
        {
            // Arrange
            SimpleTextToT9Converter sut = new SimpleTextToT9Converter(A.Dummy<IT9Keypad>());

            // Act
            Action act = () => sut.Convert(null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'text')");
        }

        [Fact]
        public void Convert_IfTextNotNull_DoesNotThrow()
        {
            // Arrange
            SimpleTextToT9Converter sut = new SimpleTextToT9Converter(A.Dummy<IT9Keypad>());

            // Act
            Action act = () => sut.Convert(string.Empty);

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void Convert_IfKeypadGetButtonThrows_Throws()
        {
            // Arrange
            Exception exception = new Exception();
            IT9Keypad keypad = A.Fake<IT9Keypad>();
            A.CallTo(() => keypad.GetButton(A<char>.Ignored)).Throws(() => exception);

            SimpleTextToT9Converter sut = new SimpleTextToT9Converter(keypad);

            // Act
            Action act = () => sut.Convert("*");

            // Assert
            act.Should().Throw<Exception>().Where(e => ReferenceEquals(e, exception));
        }

        [Theory]
        [InlineData("ab", "1 11")]
        [InlineData("bc", "112")]
        public void Convert_Converts(string text, string expectedT9)
        {
            // Arrange
            IT9Keypad keypad = CreateT9Keypad();
            SimpleTextToT9Converter sut = new SimpleTextToT9Converter(keypad);

            // Act
            string t9 = sut.Convert(text);

            // Assert
            t9.Should().Be(expectedT9);
        }
    }
}
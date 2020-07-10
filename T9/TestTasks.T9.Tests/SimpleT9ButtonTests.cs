using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using TestTasks.T9.Contract;
using Xunit;

namespace TestTasks.T9.Tests
{
    public class SimpleT9ButtonTests
    {
        [Fact]
        public void Ctor_IfSymbolsNull_Throws()
        {
            // Act
            Action act = () => new SimpleT9Button(null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'button')");
        }

        [Fact]
        public void Ctor_IfSymbolsNotNull_DoesNotThrow()
        {
            // Act
            Action act = () => new SimpleT9Button(A.Dummy<IButton>());

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void Label_ReturnsUnderlyingButtonLabel()
        {
            // Arrange
            IButton button = A.Fake<IButton>();
            SimpleT9Button sut = new SimpleT9Button(button);

            // Act
            char label = sut.Label;

            // Assert
            A.CallTo(() => button.Label).MustHaveHappenedOnceExactly();
            label.Should().Be(button.Label);
        }

        [Fact]
        public void Symbols_ReturnsUnderlyingButtonSymbols()
        {
            // Arrange
            IButton button = A.Fake<IButton>();
            SimpleT9Button sut = new SimpleT9Button(button);

            // Act
            IReadOnlyList<char> symbols = sut.Symbols;

            // Assert
            A.CallTo(() => button.Symbols).MustHaveHappenedTwiceExactly(); // first time in the constructor
            symbols.Should().BeSameAs(button.Symbols);
        }

        [Theory]
        [InlineData('a', 1)]
        [InlineData('b', 2)]
        [InlineData('c', 3)]
        [InlineData('d', -1)]
        public void GetPressCount_ReturnsPressCount(char symbol, int expectedPressCount)
        {
            // Arrange
            IButton button = A.Fake<IButton>();
            A.CallTo(() => button.Symbols).Returns(new[] { 'a', 'b', 'c' });
            SimpleT9Button sut = new SimpleT9Button(button);

            // Act
            int pressCount = sut.GetPressCount(symbol);

            // Assert
            pressCount.Should().Be(expectedPressCount);
        }
    }
}
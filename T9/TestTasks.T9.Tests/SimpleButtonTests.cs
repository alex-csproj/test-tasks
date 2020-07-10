using FluentAssertions;
using System;
using Xunit;

namespace TestTasks.T9.Tests
{
    public class SimpleButtonTests
    {
        [Fact]
        public void Ctor_IfSymbolsNull_Throws()
        {
            // Act
            Action act = () => new SimpleButton('*', null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'symbols')");
        }

        [Fact]
        public void Ctor_IfSymbolsNotNull_DoesNotThrow()
        {
            // Act
            Action act = () => new SimpleButton('*', new char[0]);

            // Assert
            act.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_IfSymbolsContainDuplicates_Throws()
        {
            // Act
            Action act = () => new SimpleButton('*', "aa");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Duplicates are not allowed in 'symbols'.");
        }

        [Fact]
        public void Ctor_IfSymbolsDoNotContainDuplicates_DoesNotThrow()
        {
            // Act
            Action act = () => new SimpleButton('*', "ab");

            // Assert
            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Ctor_SetsProperties()
        {
            // Act
            SimpleButton sut = new SimpleButton('*', "ab");

            // Assert
            sut.Label.Should().Be('*');
            sut.Symbols.Should().Equal(new[] { 'a', 'b' });
        }
    }
}
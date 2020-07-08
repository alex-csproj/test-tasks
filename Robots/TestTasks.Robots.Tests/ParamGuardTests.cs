using FluentAssertions;
using System;
using Xunit;

namespace TestTasks.Robots.Tests
{
    public class ParamGuardTests
    {
        [Fact]
        public void NotNull_IfNotNull_DoesNotThrow()
        {
            // Arrange
            object param = new object();
            string paramName = nameof(param);

            // Act
            Action act = () => ParamGuard.NotNull(param, paramName);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void NotNull_IfNull_Throws()
        {
            // Arrange
            object param = null;
            string paramName = nameof(param);

            // Act
            Action act = () => ParamGuard.NotNull(param, paramName);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Parameter can not be null. (Parameter '{paramName}')");
        }
    }
}
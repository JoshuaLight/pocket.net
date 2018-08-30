﻿using System;
using Xunit;

namespace Pocket.Common.Tests.Guards
{
    public class GuardEquatableExtensionsTest
    {
        #region EnsureNotEqual

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(int.MaxValue, int.MaxValue)]
        [InlineData(int.MinValue, int.MinValue)]
        public void EnsureNotEqual_ShouldThrowGuardException_IfValuesAreEqual(int a, int b) =>
            Assert.Throws<ArgumentException>(() => a.EnsureNotEqual(b));

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(int.MaxValue, int.MinValue)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void EnsureNotEqual_ShouldNotThrowGuardException_IfValuesAreDifferent(int a, int b) =>
            a.EnsureNotEqual(b);

        #endregion
        
        #region EnsureEqual

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(int.MaxValue, int.MinValue)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void EnsureEqual_ShouldThrowGuardException_IfValuesAreDifferent(int a, int b) =>
            Assert.Throws<ArgumentException>(() => a.EnsureEqual(b));

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(int.MaxValue, int.MaxValue)]
        [InlineData(int.MinValue, int.MinValue)]
        public void EnsureEqual_ShouldNotThrowGuardException_IfValuesAreDifferent(int a, int b) =>
            a.EnsureEqual(b);

        #endregion
    }
}
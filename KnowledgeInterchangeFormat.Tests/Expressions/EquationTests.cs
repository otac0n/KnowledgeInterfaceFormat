// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace KnowledgeInterchangeFormat.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnowledgeInterchangeFormat.Expressions;
    using Xunit;

    public class EquationTests
    {
        public static IEnumerable<object[]> ValidTermPairs =>
            from s1 in ValidTerms
            from s2 in ValidTerms
            select s1.Concat(s2).ToArray();

        public static IEnumerable<object[]> ValidTerms => new List<object[]>
        {
            new object[] { new CharacterReference('a') },
            new object[] { new CharacterString("Bb") },
            new object[] { new CharacterBlock(new string('C', 1024)) },
        };

        [Theory]
        [MemberData(nameof(ValidTerms))]
        public void Constructor_WhenGivenANullLeftTerm_ThrowsArgumentNullException(Term right)
        {
            var exception = (ArgumentNullException)Record.Exception(() => new Equation(null, right));
            Assert.Equal("left", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(ValidTerms))]
        public void Constructor_WhenGivenANullRightTerm_ThrowsArgumentNullException(Term left)
        {
            var exception = (ArgumentNullException)Record.Exception(() => new Equation(left, null));
            Assert.Equal("right", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(ValidTermPairs))]
        public void Constructor_WhenGivenValidArguments_CreatesAnObjectWithTheSpecifiedProperties(Term left, Term right)
        {
            var subject = new Equation(left, right);

            Assert.Equal(left, subject.Left);
            Assert.Equal(right, subject.Right);
        }

        [Theory]
        [MemberData(nameof(ValidTermPairs))]
        public void ToString_Always_ReturnsExpectedOutput(Term left, Term right)
        {
            var expected = $"(= {left} {right})";
            var subject = new Equation(left, right);

            Assert.Equal(expected, subject.ToString());
        }
    }
}

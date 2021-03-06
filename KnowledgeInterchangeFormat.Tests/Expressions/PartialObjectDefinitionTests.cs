// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace KnowledgeInterchangeFormat.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnowledgeInterchangeFormat.Expressions;
    using Xunit;

    public class PartialObjectDefinitionTests
    {
        public static IEnumerable<object[]> ValidSentences => new List<object[]>
        {
            new object[] { new ConstantSentence(new Constant("A")) },
            new object[] { new ConstantSentence(new Constant("Bb")) },
            new object[] { new ConstantSentence(new Constant(new string('C', 1024))) },
        };

        public static IEnumerable<object[]> ValidConstants => new List<object[]>
        {
            new object[] { new Constant("A") },
            new object[] { new Constant("Bb") },
            new object[] { new Constant(new string('C', 1024)) },
        };

        public static IEnumerable<object[]> ValidStrings => new List<object[]>
        {
            new object[] { null },
            new object[] { new CharacterString("OK Description") },
        };

        public static IEnumerable<object[]> ValidVariables => new List<object[]>
        {
            new object[] { new IndividualVariable("?OK") },
            new object[] { new IndividualVariable("?A") },
            new object[] { new IndividualVariable("?Bb") },
        };

        public static IEnumerable<object[]> ValidArguments =>
            from s1 in ValidConstants
            from s2 in ValidStrings
            from s3 in ValidVariables
            from s4 in ValidSentences
            select s1.Concat(s2).Concat(s3).Concat(s4).ToArray();

        public static IEnumerable<object[]> ValidArgumentsNoConstant =>
            from s1 in ValidStrings
            from s2 in ValidVariables
            from s3 in ValidSentences
            select s1.Concat(s2).Concat(s3).ToArray();

        public static IEnumerable<object[]> ValidArgumentsNoVariable =>
            from s1 in ValidConstants
            from s2 in ValidStrings
            from s3 in ValidSentences
            select s1.Concat(s2).Concat(s3).ToArray();

        public static IEnumerable<object[]> ValidArgumentsNoSentence =>
            from s1 in ValidConstants
            from s2 in ValidStrings
            from s3 in ValidVariables
            select s1.Concat(s2).Concat(s3).ToArray();

        [Theory]
        [MemberData(nameof(ValidArgumentsNoConstant))]
        public void Constructor_WhenGivenANullConstant_ThrowsArgumentNullException(CharacterString description, IndividualVariable variable, Sentence sentence)
        {
            var exception = (ArgumentNullException)Record.Exception(() => new PartialObjectDefinition(null, description, variable, sentence));
            Assert.Equal("constant", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(ValidArgumentsNoSentence))]
        public void Constructor_WhenGivenANullSentence_ThrowsArgumentNullException(Constant constant, CharacterString description, IndividualVariable variable)
        {
            var exception = (ArgumentNullException)Record.Exception(() => new PartialObjectDefinition(constant, description, variable, null));
            Assert.Equal("sentence", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(ValidArgumentsNoVariable))]
        public void Constructor_WhenGivenANullVariable_ThrowsArgumentNullException(Constant constant, CharacterString description, Sentence sentence)
        {
            var exception = (ArgumentNullException)Record.Exception(() => new PartialObjectDefinition(constant, description, null, sentence));
            Assert.Equal("variable", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(ValidArguments))]
        public void Constructor_WhenGivenValidArguments_CreatesAnObjectWithTheSpecifiedProperties(Constant constant, CharacterString description, IndividualVariable variable, Sentence sentence)
        {
            var subject = new PartialObjectDefinition(constant, description, variable, sentence);

            Assert.Equal(constant, subject.Constant);
            Assert.Equal(description, subject.Description);
            Assert.Equal(variable, subject.Variable);
            Assert.Equal(sentence, subject.Sentence);
        }

        [Theory]
        [MemberData(nameof(ValidArguments))]
        public void ToString_Always_ReturnsExpectedOutput(Constant constant, CharacterString description, IndividualVariable variable, Sentence sentence)
        {
            var expected = $"(defobject {constant}{(description == null ? "" : $" {description}")} :-> {variable} :=> {sentence})";
            var subject = new PartialObjectDefinition(constant, description, variable, sentence);

            Assert.Equal(expected, subject.ToString());
        }
    }
}

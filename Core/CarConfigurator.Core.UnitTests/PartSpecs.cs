using CarConfigurator.Core.Model;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CarConfigurator.Core.UnitTests
{
    [TestFixture]
    public class PartSpecs
    {
        private Part ExamplePart;
        private Part AnotherPart;
        private CarModel ExampleCarModel = new CarModel(BodyType.Coupe, new DateTime(2017, 11, 10));

        [SetUp]
        public void SetUp()
        {
            ExamplePart = new Part();
            AnotherPart = new Part();
        }

        [Test]
        public void AddingConflictingPartOnceShouldSucceed()
        {
            // when
            ExamplePart.AddConflictingPart(AnotherPart);

            // then
            ExamplePart.ConflictingParts.Should().Contain(AnotherPart);
            AnotherPart.ConflictingParts.Should().Contain(ExamplePart);
        }

        [Test]
        public void AddingConflictingPartTwiceShouldFail()
        {
            // given
            ExamplePart.AddConflictingPart(AnotherPart);

            // when
            Action addTwice = () =>
            {
                ExamplePart.AddConflictingPart(AnotherPart);
            };

            // then
            addTwice.ShouldThrow<ArgumentException>();
            ExamplePart.ConflictingParts.Should().Contain(AnotherPart);
        }

        [Test]
        public void AddingNullConflictingPartShouldFail()
        {
            // when
            Action addNull = () =>
            {
                ExamplePart.AddConflictingPart(null);
            };

            // then
            addNull.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void RemovingConflictingPartOnceShouldSucceed()
        {
            // given
            ExamplePart.AddConflictingPart(AnotherPart);

            // when
            ExamplePart.RemoveConflictingPart(AnotherPart);

            // then
            ExamplePart.ConflictingParts.Should().NotContain(AnotherPart);
        }

        [Test]
        public void RemovingConflictingPartTwiceShouldFail()
        {
            // given
            ExamplePart.AddConflictingPart(AnotherPart);

            // when
            ExamplePart.RemoveConflictingPart(AnotherPart);
            Action removeTwice = () =>
            {
                ExamplePart.RemoveConflictingPart(AnotherPart);
            };

            // then
            removeTwice.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void RemovingNotAttachedConflictingPartShouldFail()
        {
            // when
            Action removeNotAttached = () =>
            {
                ExamplePart.RemoveConflictingPart(AnotherPart);
            };

            // then
            removeNotAttached.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void RemovingNullConflictingPartShouldFail()
        {
            // when
            Action removeNull = () =>
            {
                ExamplePart.RemoveConflictingPart(null);
            };

            // then
            removeNull.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AddingCarModelOnceShouldSucceed()
        {
            // when
            ExamplePart.AddSuitableModel(ExampleCarModel);

            // then
            ExamplePart.AvailableInModels.Should().Contain(ExampleCarModel);
        }

        [Test]
        public void AddingCarModelTwiceShouldFail()
        {
            // given
            ExamplePart.AddSuitableModel(ExampleCarModel);

            // when
            Action addTwice = () =>
            {
                ExamplePart.AddSuitableModel(ExampleCarModel);
            };

            // then
            addTwice.ShouldThrow<ArgumentException>();
            ExamplePart.AvailableInModels.Should().Contain(ExampleCarModel);
        }

        [Test]
        public void AddingNullCarModelShouldFail()
        {
            // when
            Action addNull = () =>
            {
                ExamplePart.AddSuitableModel(null);
            };

            // then
            addNull.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void RemovingCarModelOnceShouldSucceed()
        {
            // given
            ExamplePart.AddSuitableModel(ExampleCarModel);

            // when
            ExamplePart.RemoveSuitableModel(ExampleCarModel);

            // then
            ExamplePart.AvailableInModels.Should().NotContain(ExampleCarModel);
        }

        [Test]
        public void RemovingCarModelTwiceShouldFail()
        {
            // given
            ExamplePart.AddSuitableModel(ExampleCarModel);

            // when
            ExamplePart.RemoveSuitableModel(ExampleCarModel);
            Action removeTwice = () =>
            {
                ExamplePart.RemoveSuitableModel(ExampleCarModel);
            };

            // then
            removeTwice.ShouldThrow<ArgumentException>();
            ExamplePart.AvailableInModels.Should().NotContain(ExampleCarModel);
        }

        [Test]
        public void RemovingNullCarModelShouldFail()
        {
            // when
            Action removeNull = () =>
            {
                ExamplePart.RemoveSuitableModel(null);
            };

            // then
            removeNull.ShouldThrow<ArgumentException>();
        }
    }
}
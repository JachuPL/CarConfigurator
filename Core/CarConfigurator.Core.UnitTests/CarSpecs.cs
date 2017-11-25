using CarConfigurator.Core.Model;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CarConfigurator.Core.UnitTests
{
    [TestFixture]
    public class CarSpecs
    {
        private Part ExamplePart;
        private Part ConflictingPart;
        private CarModel ExampleCarModel;
        private Car ExampleCar;

        [SetUp]
        public void SetUp()
        {
            ExamplePart = new Part();
            ConflictingPart = new Part();
            ExampleCarModel = new CarModel(BodyType.Coupe, new DateTime(2017, 11, 10));
            ExampleCar = new Car(ExampleCarModel);

            ExamplePart.AddConflictingPart(ConflictingPart);
            ExamplePart.AddSuitableModel(ExampleCarModel);
            ConflictingPart.AddSuitableModel(ExampleCarModel);
        }

        [Test]
        public void AddingNullPartShouldFail()
        {
            // when
            Action addNull = () =>
            {
                ExampleCar.AddPart(null);
            };

            // then
            addNull.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AddingPartTwiceShouldFail()
        {
            // given
            ExampleCar.AddPart(ExamplePart);

            // when
            Action addTwice = () =>
            {
                ExampleCar.AddPart(ExamplePart);
            };

            // then
            addTwice.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AddingPartNotAvailableForCarShouldFail()
        {
            // given
            Part PartNotAvailableForCar = new Part();

            // when
            Action addPart = () =>
            {
                ExampleCar.AddPart(PartNotAvailableForCar);
            };

            // then
            addPart.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AddingPartWithConflictsShouldFail()
        {
            // given
            ExampleCar.AddPart(ExamplePart);

            // when
            Action addConflictingPart = () =>
            {
                ExampleCar.AddPart(ConflictingPart);
            };

            // then
            addConflictingPart.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AddingPartShouldSucceed()
        {
            // when
            ExampleCar.AddPart(ExamplePart);

            // then
            ExampleCar.Parts.Should().Contain(ExamplePart);
        }

        [Test]
        public void RemovingNullPartShouldFail()
        {
            // when
            Action removeNull = () =>
            {
                ExampleCar.RemovePart(null);
            };

            // then
            removeNull.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void RemovingNotAttachedPartShouldFail()
        {
            // when
            Action removeNotAttached = () =>
            {
                ExampleCar.RemovePart(ExamplePart);
            };

            // then
            removeNotAttached.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void RemovingPartShouldSucceed()
        {
            // given
            ExampleCar.AddPart(ExamplePart);

            // when
            ExampleCar.RemovePart(ExamplePart);

            // then
            ExampleCar.Parts.Should().NotContain(ExamplePart);
        }

        [Test]
        public void ChangingCarModelToNullShouldFail()
        {
            // when
            Action changeToNull = () =>
            {
                ExampleCar.ChangeModel(null);
            };

            // then
            changeToNull.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ChangingCarModelToCurrentShouldFail()
        {
            // when
            Action changeToCurrent = () =>
            {
                ExampleCar.ChangeModel(ExampleCar.Model);
            };

            // then
            changeToCurrent.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ChangingCarModelToOtherShouldRemoveAllConflictingParts()
        {
            // given
            Part Conflicting = new Part();
            Part NotConflicting = new Part();
            CarModel AnotherModel = new CarModel(BodyType.Kombi, new DateTime(2017, 11, 10));

            Conflicting.AddSuitableModel(ExampleCar.Model);
            NotConflicting.AddSuitableModel(ExampleCar.Model);
            NotConflicting.AddSuitableModel(AnotherModel);
            ExampleCar.AddPart(Conflicting);
            ExampleCar.AddPart(NotConflicting);

            // when
            ExampleCar.ChangeModel(AnotherModel);

            // then
            ExampleCar.Parts.Should().NotContain(Conflicting);
            ExampleCar.Parts.Should().Contain(NotConflicting);
        }
    }
}
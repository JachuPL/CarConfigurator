using System;

namespace CarConfigurator.Core.Model
{
    public sealed class Configuration
    {
        private Car _car;
        private Guid _id;

        public Car Car => _car;
        public Guid Id => _id;

        private Configuration()
        {
            _id = Guid.NewGuid();
        }

        public Configuration(Car car) : this()
        {
            _car = car;
        }
    }
}
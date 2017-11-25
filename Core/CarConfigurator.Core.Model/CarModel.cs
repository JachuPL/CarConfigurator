using System;

namespace CarConfigurator.Core.Model
{
    public class CarModel
    {
        private BodyType _bodyType;
        private DateTime _productionDate;

        public CarModel(BodyType body, DateTime productionDate)
        {
            _bodyType = body;
            _productionDate = productionDate;
        }

        public BodyType BodyType => _bodyType;
        public DateTime ProductionDate => _productionDate;
    }
}
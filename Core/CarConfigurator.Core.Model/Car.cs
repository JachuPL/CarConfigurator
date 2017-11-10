using CarConfigurator.Core.Abstractions.ActionPossibility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarConfigurator.Core.Model
{
    public class Car
    {
        private CarModel _model;
        private List<Part> _parts;

        public CarModel Model => _model;
        public IReadOnlyList<Part> Parts => _parts;

        public Car(CarModel modelName)
        {
            _model = modelName;
            _parts = new List<Part>();
        }

        private IActionPossible CanAddPart(Part p)
        {
            if (p is null)
                return new ActionImpossible("Cannot add null part");

            if (_parts.Contains(p))
                return new ActionImpossible("Part already added");

            if (!p.AvailableInModels.Contains(Model))
                return new ActionImpossible("Selected part cannot be mounted in chosen car");

            if (_parts.Any(x => p.ConflictingParts.Contains(x)))
                return new ActionImpossible("You have chosen parts that are conflicting with this one.");

            return new ActionPossible();
        }

        private IActionPossible CanRemovePart(Part p)
        {
            if (p is null)
                return new ActionImpossible("Cannot remove null part");

            if (!_parts.Contains(p))
                return new ActionImpossible("Part has not beed not added");

            return new ActionPossible();
        }

        public void AddPart(Part p)
        {
            IActionPossible canAdd = CanAddPart(p);
            if (!canAdd.IsPossible)
                throw new ArgumentException(canAdd.Reason);

            _parts.Add(p);
        }

        public void RemovePart(Part p)
        {
            IActionPossible canRemove = CanRemovePart(p);
            if (!canRemove.IsPossible)
                throw new ArgumentException(canRemove.Reason);

            _parts.Remove(p);
        }

        private IActionPossible CanChangeModel(CarModel model)
        {
            if (model is null)
                return new ActionImpossible("You have to choose another model instead");

            if (model == _model)
                return new ActionImpossible("This is the current car model");

            return new ActionPossible();
        }

        public void ChangeModel(CarModel model)
        {
            IActionPossible canChange = CanChangeModel(model);
            if (!canChange.IsPossible)
                throw new ArgumentException(canChange.Reason);

            _model = model;

            _parts.RemoveAll(x => !x.AvailableInModels.Contains(_model));
        }
    }
}
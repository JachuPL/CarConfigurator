using CarConfigurator.Core.Abstractions.ActionPossibility;
using System;
using System.Collections.Generic;

namespace CarConfigurator.Core.Model
{
    public class Part
    {
        private List<Part> _conflictingParts;
        private List<CarModel> _availableInModels;
        private Guid _id;

        public IReadOnlyList<Part> ConflictingParts => _conflictingParts;
        public IReadOnlyList<CarModel> AvailableInModels => _availableInModels;

        public Part() : this(Guid.NewGuid())
        {
        }

        public Part(Guid id)
        {
            _id = id;
            _conflictingParts = new List<Part>();
            _availableInModels = new List<CarModel>();
        }

        private bool HasConflict(Part p) => _conflictingParts.Contains(p);

        private IActionPossible CanAddConflictingPart(Part p)
        {
            if (p is null)
                return new ActionImpossible("Conflicting part to add is null");

            if (this == p)
                return new ActionImpossible("Cannot add self as conflicting part");

            if (HasConflict(p))
                return new ActionImpossible("These parts already conflict with each other.");

            return new ActionPossible();
        }

        private IActionPossible CanRemoveConflictingPart(Part p)
        {
            if (p is null)
                return new ActionImpossible("Conflicting part to remove is null");

            if (this == p)
                return new ActionImpossible("Cannot remove self as conflicting part");

            if (!HasConflict(p))
                return new ActionImpossible("These parts don't have conflict with each other.");

            return new ActionPossible();
        }

        public void AddConflictingPart(Part p)
        {
            IActionPossible canAdd = CanAddConflictingPart(p);
            if (!canAdd.IsPossible)
                throw new ArgumentException(canAdd.Reason);

            p._conflictingParts.Add(this);
            _conflictingParts.Add(p);
        }

        public void RemoveConflictingPart(Part p)
        {
            IActionPossible canRemove = CanRemoveConflictingPart(p);
            if (!canRemove.IsPossible)
                throw new ArgumentException(canRemove.Reason);

            p._conflictingParts.Remove(this);
            _conflictingParts.Remove(p);
        }

        private IActionPossible CanAddSuitableModel(CarModel model)
        {
            if (model is null)
                return new ActionImpossible("Model being added is null");

            if (_availableInModels.Contains(model))
                return new ActionImpossible("Model already added");

            return new ActionPossible();
        }

        private IActionPossible CanRemoveSuitableModel(CarModel model)
        {
            if (model is null)
                return new ActionImpossible("Model being removed is null");

            if (!_availableInModels.Contains(model))
                return new ActionImpossible("Model is not on list");

            return new ActionPossible();
        }

        public void AddSuitableModel(CarModel model)
        {
            IActionPossible canAdd = CanAddSuitableModel(model);
            if (!canAdd.IsPossible)
                throw new ArgumentException(canAdd.Reason);

            _availableInModels.Add(model);
        }

        public void RemoveSuitableModel(CarModel model)
        {
            IActionPossible canRemove = CanRemoveSuitableModel(model);
            if (!canRemove.IsPossible)
                throw new ArgumentException(canRemove.Reason);

            _availableInModels.Remove(model);
        }
    }
}
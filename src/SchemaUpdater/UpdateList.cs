using System;
using System.Collections.Generic;

namespace SchemaUpdater
{
    public class UpdateList
    {
        private List<IUpdate> _updates = new List<IUpdate>();

        public IEnumerable<IUpdate> Updates
        {
            get { return _updates; }
        }

        public void Add(IUpdate update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            CheckIfUpdateIsAlreadyAdded(update);

            update.Lock();
            _updates.Add(update);
        }

        private void CheckIfUpdateIsAlreadyAdded(IUpdate update)
        {
            if (_updates.Contains(update))
            {
                throw new InvalidOperationException("The given update is already added.");
            }
        }
    }
}
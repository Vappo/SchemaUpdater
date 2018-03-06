using System;
using System.Collections.Generic;
using SchemaUpdater.Updates.AddColumn;

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

        public void AddColumn(string tableName, Database database)
        {
            AddColumnUpdate addColumnUpdate = AddColumnUpdateFactory.CreateAddColumnUpdate(tableName, database);
            Add(addColumnUpdate);
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
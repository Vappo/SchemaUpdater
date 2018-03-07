using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchemaUpdater
{
    public class PrimaryKey
    {
        public PrimaryKey(params string[] primaryKeyColumns)
        {
            if (primaryKeyColumns == null)
            {
                throw new ArgumentNullException(nameof(primaryKeyColumns));
            }

            if (!primaryKeyColumns.Any())
            {
                throw new ArgumentException("No columns given in constructor.", nameof(primaryKeyColumns));
            }

            Columns = new ReadOnlyCollection<string>(primaryKeyColumns.ToList());
        }

        public IReadOnlyList<string> Columns { get; }

        public string Name { get; set; }

        public bool Clustered { get; set; }
    }
}
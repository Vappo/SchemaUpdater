using System;

namespace SchemaUpdater
{
    public class DatabaseVersion
    {
        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public int Revision { get; set; }

        public DateTime CreationDate { get; set; }

        public static bool operator <(DatabaseVersion left, DatabaseVersion right)
        {
            if (left.MajorVersion < right.MajorVersion)
            {
                return true;
            }

            if (left.MajorVersion > right.MajorVersion)
            {
                return false;
            }

            if (left.MinorVersion < right.MinorVersion)
            {
                return true;
            }

            if (left.MinorVersion > right.MinorVersion)
            {
                return false;
            }

            return left.Revision < right.Revision;
        }

        public static bool operator >(DatabaseVersion left, DatabaseVersion right)
        {
            if (left.MajorVersion < right.MajorVersion)
            {
                return false;
            }

            if (left.MajorVersion > right.MajorVersion)
            {
                return true;
            }

            if (left.MinorVersion < right.MinorVersion)
            {
                return false;
            }
            if (left.MinorVersion > right.MinorVersion)
            {
                return true;
            }

            return left.Revision > right.Revision;
        }

        public override string ToString()
        {
            return MajorVersion + "." + MinorVersion + "." + Revision;
        }
    }
}
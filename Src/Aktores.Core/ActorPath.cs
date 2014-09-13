namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorPath
    {
        public static bool IsName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return true;

            if (path.Contains("/"))
                return false;

            return true;
        }

        public static bool IsRelative(string path)
        {
            if (IsName(path))
                return true;

            if (path.StartsWith("."))
                return true;

            return false;
        }
    }
}

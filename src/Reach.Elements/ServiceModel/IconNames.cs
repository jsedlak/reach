using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reach.Elements.ServiceModel
{
    public static class IconNames
    {
        public const string ArrowRight = "arrow_right";
        public const string ArrowLeft = "arrow_left";
        public const string ArrowUp = "arrow_up";
        public const string ArrowDown = "arrow_down";

        public const string SquareEmpty = "square_empty";
        public const string SquareCheck = "square_check";

        public const string GenerateId = "id_generate";

        public static IEnumerable<string> GetIconNames()
        {
            return typeof(IconNames)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Select(m => (string)m.GetValue(null));
        }
    }
}

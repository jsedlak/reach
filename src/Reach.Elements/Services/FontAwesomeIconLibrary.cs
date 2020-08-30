using Microsoft.AspNetCore.Components;
using Reach.Elements.ServiceModel;
using System.Collections.Generic;

namespace Reach.Elements.Services
{
    public class FontAwesomeIconLibrary : IIconLibrary
    {
        public static readonly Dictionary<string, string> IconMap = new Dictionary<string, string>()
        {
            { IconNames.SquareCheck, "fal fa-check-square" },
            { IconNames.SquareEmpty, "fal fa-square" },
            { IconNames.ArrowLeft, "fal fa-caret-left" },
            { IconNames.ArrowRight, "fal fa-caret-right" },
            { IconNames.ArrowUp, "fal fa-caret-up" },
            { IconNames.ArrowDown, "fal fa-caret-down" },
            { IconNames.GenerateId, "fal fa-brackets-curly" }
        };

        public virtual MarkupString GetIcon(string iconName)
        {
            var result = IconMap[iconName];

            if(string.IsNullOrWhiteSpace(result))
            {
                result = $"fal fa-{result}";
            }

            return new MarkupString($"<i class=\"{result}\"></i>");
        }
    }
}

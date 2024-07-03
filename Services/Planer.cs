using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planer.DataModel;

namespace Planer.Services
{
    public class PlanerService
    {
        public IEnumerable<PlanerItem> GetItems() => new[]
        {
            new PlanerItem { Description = "Koncert Travis Scott 02.07.2024" },
            new PlanerItem { Description = "Ocena z wykładu programowania (oby było chociaż 3) 06.07.2024" },
           
        };
    }
}

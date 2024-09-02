using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Planer.DataModel;

namespace Planer.Services
{
    public class PlanerService
    {
        private readonly string _filePath = "planerItems.json";

        public IEnumerable<PlanerItem> GetItems()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<PlanerItem>>(json) ?? new List<PlanerItem>();
            }

            return new List<PlanerItem>
            {
                new PlanerItem { Description = "Koncert Travis Scott 02.07.2024" },
                new PlanerItem { Description = "Ocena z wykładu programowania (oby było chociaż 3) 06.07.2024" },
            };
        }

        public void SaveItems(IEnumerable<PlanerItem> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}

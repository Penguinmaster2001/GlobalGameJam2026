
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;



namespace Interactions;



public class DialogManager
{
    public async Task<List<Interaction>> LoadDialogs(string path)
    {
        var interactions = new List<Interaction>();

        await foreach (var interaction in Task.WhenEach(Directory.EnumerateFiles(path).Select(f =>
        {
            using var stream = new FileStream(path, FileMode.Open);
            return JsonSerializer.DeserializeAsync<Interaction>(stream).AsTask();
        })))
        {
            if (await interaction is Interaction validInteraction)
            {
                interactions.Add(validInteraction);
            }
        }

        return interactions;
    }
}

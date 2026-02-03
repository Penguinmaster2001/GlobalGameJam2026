
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Godot;



namespace Interactions;



public class NpcTextureDatabase
{
    private readonly Dictionary<string, Texture2D> _textures;



    public NpcTextureDatabase(Dictionary<string, Texture2D> textures)
    {
        _textures = textures;
    }




    public bool Query(string npc, [NotNullWhen(true)] out Texture2D? texture)
        => _textures.TryGetValue(npc.ToLower(), out texture);




    public Dictionary<string, Texture2D> Query(List<string> npcNames)
    {
        GD.Print(string.Join(',', npcNames));

        return npcNames.Select(name => (name, exists: _textures.TryGetValue(name.ToLower(), out var texture), texture))
            .Where(t => t.exists).ToDictionary(t => t.name.ToLower(), t => t.texture!);

    }
}

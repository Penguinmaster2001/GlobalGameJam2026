
using System.Collections.Generic;



namespace Interactions;



public class Requirements
{
    public int MinSuspicion { get; set; }
    public int MaxSuspicion { get; set; }
    public List<int> PreviousInteractions { get; }
    public List<string> CurrentObjective { get; }
    public List<string> CompletedObjectives { get; }
    public List<int> Mask { get; }



    public Requirements(int minSuspicion, int maxSuspicion, List<int> previousInteractions,
        List<string> currentObjective, List<string> completedObjectives, List<int> mask)
    {
        MinSuspicion = minSuspicion;
        MaxSuspicion = maxSuspicion;
        PreviousInteractions = previousInteractions;
        CurrentObjective = currentObjective;
        CompletedObjectives = completedObjectives;
        Mask = mask;
    }



    public static Requirements None => new(int.MinValue, int.MaxValue, [], [], [], []);
}

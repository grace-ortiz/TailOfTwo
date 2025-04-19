using UnityEngine;
using System.Collections.Generic;
/*
public class MushroomGrowPuzzleManager : MonoBehaviour
{
    public List<Growable> growables;
    private List<string> growOrder = new List<string>();
    private readonly string[] correctOrder = { "Left", "Right", "Middle" };
    private bool puzzleCompleted = false;

    public void AttemptGrow(Growable g)
    {
        if (puzzleCompleted || growOrder.Contains(g.growableID))
            return;

        growOrder.Add(g.growableID);

        if (growOrder.Count == 3)
        {
            bool correct =
                growOrder[0] == correctOrder[0] &&
                growOrder[1] == correctOrder[1] &&
                growOrder[2] == correctOrder[2];

            foreach (Growable mushroom in growables)
            {
                if (correct)
                {
                    if (mushroom.growableID == "Left") mushroom.GrowToStage(2);
                    else if (mushroom.growableID == "Middle") mushroom.GrowToStage(1);
                    else if (mushroom.growableID == "Right") mushroom.GrowToStage(0);
                }
                else
                {
                    mushroom.GrowToStage(1); // default height for failed order
                }
            }

            puzzleCompleted = true;
        }
    }

    public void ResetPuzzle()
    {
        puzzleCompleted = false;
        growOrder.Clear();

        foreach (Growable g in growables)
        {
            g.ResetInteraction();
        }
    }
}
*/
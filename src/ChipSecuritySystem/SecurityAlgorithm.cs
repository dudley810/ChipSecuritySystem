namespace ChipSecuritySystem;

public class SecurityAlgorithm
{
    private Color startColor = Color.Blue;
    private Color endColor = Color.Green;

    public List<ColorChip> FindCode(List<ColorChip> mainBag)
    {
        List<ColorChip> workingBag = new();
        ColorChip? workingChip = (from s in mainBag where s.StartColor == startColor && s.EndColor != endColor select s).FirstOrDefault();
        if (workingChip == null)
        {
            throw new Exception("Unable to find the starting chip. no " + startColor.ToString());
        }

        mainBag.Remove(workingChip);
        workingBag.Add(workingChip);

        bool firstTime = true;
        ColorChip? foundChip = null;
        while (foundChip != null || firstTime)
        {
            firstTime = false;
            foundChip = (from c in mainBag
                         where c.StartColor == workingChip.EndColor
                            && c.EndColor != endColor && c.EndColor
                            != workingChip.EndColor
                         select c).FirstOrDefault();

            if (foundChip != null)
            {
                mainBag.Remove(foundChip);
                workingBag.Add(foundChip);
                workingChip = foundChip;
            }
            else
            {
                var endChip = (from e in mainBag
                               where e.StartColor == workingChip.EndColor
                                  && e.EndColor == endColor
                               select e).FirstOrDefault();
                if (endChip == null)
                {
                    throw new Exception("Unable to find the ending chip. no " + endColor.ToString());
                }

                mainBag.Remove(endChip);
                workingBag.Add(endChip);
            }
        }
        return workingBag;
    }
}
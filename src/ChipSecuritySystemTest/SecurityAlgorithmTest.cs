using ChipSecuritySystem;

namespace ChipSecuritySystemTest;

public class SecurityAlgorithmTest
{
    [Fact]
    public void GoodTest()
    {
        SecurityAlgorithm securityAlgorithm = new SecurityAlgorithm();
        List<ColorChip> bag = new()
        {
            new ColorChip(Color.Blue, Color.Yellow),
            new ColorChip(Color.Red, Color.Green),
            new ColorChip(Color.Yellow, Color.Red),
            new ColorChip(Color.Orange, Color.Purple)
        };
        List<ColorChip>? answer = securityAlgorithm.FindCode(bag);
        Assert.NotNull(answer);
        if (answer != null)
        {
            Assert.Equal("Blue, Yellow", answer[0].ToString());
            Assert.Equal("Yellow, Red", answer[1].ToString());
            Assert.Equal("Red, Green", answer[2].ToString());
        }
    }

    [Fact]
    public void FullTest()
    {
        List<ColorChip> mainBag = new();
        SecurityAlgorithm securityAlgorithm = new SecurityAlgorithm();
        List<Color> bag = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();

        //Fill the bag up with everything
        foreach (var firstColor in bag)
        {
            foreach (var secondColor in bag)
            {
                if (firstColor != secondColor)
                {
                    mainBag.Add(new ColorChip(firstColor, secondColor));
                }
            }
        }

        List<ColorChip>? answer = securityAlgorithm.FindCode(mainBag);
        Assert.NotNull(answer);
        Assert.Equal(15, answer.Count);
    }

    [Fact]
    public void StartColorException()
    {
        SecurityAlgorithm securityAlgorithm = new SecurityAlgorithm();
        List<ColorChip> bag = new()
        {
            new ColorChip(Color.Red, Color.Green),
            new ColorChip(Color.Yellow, Color.Red),
            new ColorChip(Color.Orange, Color.Purple)
        };

        var ex = Assert.Throws<Exception>(() => securityAlgorithm.FindCode(bag));
        Assert.Equal("Unable to find the starting chip. no Blue", ex.Message);
    }

    [Fact]
    public void EndColorException()
    {
        SecurityAlgorithm securityAlgorithm = new SecurityAlgorithm();
        List<ColorChip> bag = new()
        {
            new ColorChip(Color.Blue, Color.Yellow),
            new ColorChip(Color.Yellow, Color.Red),
            new ColorChip(Color.Orange, Color.Purple)
        };

        var ex = Assert.Throws<Exception>(() => securityAlgorithm.FindCode(bag));
        Assert.Equal("Unable to find the ending chip. no Green", ex.Message);
    }
}
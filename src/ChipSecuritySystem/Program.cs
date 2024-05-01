using ChipSecuritySystem;

List<ColorChip> mainBag = new();
List<Color> colors = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();

//Fill the bag up with everything
foreach (var firstColor in colors)
{
    foreach (var secondColor in colors)
    {
        if (firstColor != secondColor)
        {
            mainBag.Add(new ColorChip(firstColor, secondColor));
        }
    }
}

try
{
    List<ColorChip> workingBag = new SecurityAlgorithm().FindCode(mainBag);
    Console.WriteLine("The bag solution with the most chips is:");
    foreach (var item in workingBag)
    {
        Console.WriteLine($"{item}");
    }
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(Constants.ErrorMessage + " " + ex.Message);
    Console.ReadLine();
}
namespace ProjectsApplication.Models;

public class MostUsedIndicatorsResponseViewModel
{
    public IEnumerable<MostUsedIndicatorEntryResponseViewModel> Indicators { get; set; }
}

public class MostUsedIndicatorEntryResponseViewModel
{
    public string Name { get; set; }
    public int Used { get; set; }
}
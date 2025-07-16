namespace ProjectsApplication.Models;

public class ProjectCreateRequestModel
{
    public string Name { get; set; }
    public IEnumerable<ChartCreateRequestModel>? Charts { get; set; } = new List<ChartCreateRequestModel>();
}
public class ChartCreateRequestModel
{
    public string Symbol { get; set; }
    public string Timeframe { get; set; }
    public IEnumerable<IndicatorCreateRequestModel>? Indicators { get; set; } = new List<IndicatorCreateRequestModel>();
}
public class IndicatorCreateRequestModel
{
    public string Name { get; set; }
    public string Parameters { get; set; }
}
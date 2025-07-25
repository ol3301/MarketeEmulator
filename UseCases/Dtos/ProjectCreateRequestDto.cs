namespace UseCases.Dtos;

public class ProjectCreateRequestDto
{
    public string Name { get; set; }
    public IEnumerable<ChartCreateRequestDto>? Charts { get; set; } = new List<ChartCreateRequestDto>();
}
public class ChartCreateRequestDto
{
    public string Symbol { get; set; }
    public string Timeframe { get; set; }
    public IEnumerable<IndicatorCreateRequestDto>? Indicators { get; set; } = new List<IndicatorCreateRequestDto>();
}
public class IndicatorCreateRequestDto
{
    public string Name { get; set; }
    public string Parameters { get; set; }
}
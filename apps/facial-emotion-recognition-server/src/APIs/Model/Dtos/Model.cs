namespace FacialEmotionRecognition.APIs.Dtos;

public class Model
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? Name { get; set; }

    public string? TrainedOnDataset { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Version { get; set; }
}

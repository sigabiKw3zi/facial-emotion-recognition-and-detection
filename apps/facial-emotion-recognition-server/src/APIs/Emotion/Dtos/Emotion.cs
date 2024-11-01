namespace FacialEmotionRecognition.APIs.Dtos;

public class Emotion
{
    public double? Confidence { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? DetectedInImage { get; set; }

    public string Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}

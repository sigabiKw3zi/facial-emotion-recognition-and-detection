using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ModelFindManyArgs : FindManyInput<Model, ModelWhereInput> { }

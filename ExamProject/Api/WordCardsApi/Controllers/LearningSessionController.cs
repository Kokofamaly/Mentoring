using Microsoft.AspNetCore.Mvc;
using WordCardsApi.Services;

namespace WordCardsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LearningSessionController : ControllerBase
{
    private readonly LearningSessionService _learningSessionService;
    public LearningSessionController(LearningSessionService learningSessionService)
    {
        _learningSessionService = learningSessionService;
    }
}
using Crito.Contexts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crito.Services;

public class SurfaceController : Controller 
{
    private readonly SubcriberService _subcribe;

    public SurfaceController(SubcriberService subcribe)
    {
        _subcribe = subcribe;
    }

    public async Task Run()
    {
        var subcriber = new NewsLetterForm()
        {
            Email = "leah@domain.com"
        };

        await _subcribe.AddSubscriberAsync(subcriber);

    }
}

using Crito.Contexts;
using Crito.Contexts.Models;

namespace Crito.Services;

public class SubcriberService
{
    private readonly DataContext _context;

    public SubcriberService(DataContext context)
    {
        _context = context;
    }

    public async Task AddSubscriberAsync(NewsLetterForm form)
    {
        _context.Subcribers.Add(new NewsLetterEntity
        {
            Email = form.Email
        });
        await _context.SaveChangesAsync();
    }

}

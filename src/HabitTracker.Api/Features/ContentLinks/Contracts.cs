namespace HabitTracker.Api.Features.ContentLinks;

internal record ContentLinkResponse(int Id, string Title, string? Description, string Url)
{
    public static ContentLinkResponse FromEntity(ContentLink link) =>
        new(link.Id, link.Title, link.Description, link.Url);
}

internal record GetContentLinksForGroupRequest(string Group);

namespace HabitTracker.Web.Features;

public partial class ContentLinksSidebar
{
    public record ContentLinkResponse(int Id, string Title, string? Description, string Url);

    private ContentLinkResponse[] _links { get; set; } = [];

    protected override async Task OnInitializedAsync() =>
        _links = await _sessionStorage.GetOrCreateAsync(
            Constants.ContentLinks.SessionKey,
            async () =>
            {
                var result = await _http.TryGetFromJsonAsync<ContentLinkResponse[]>(
                    Constants.ContentLinks.ServiceUrl, [], _log);
                return result.IsSuccess ? result.GetValue() : null;
            }) ?? [];
}

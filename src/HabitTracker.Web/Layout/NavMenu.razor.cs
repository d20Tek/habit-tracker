using Microsoft.AspNetCore.Components.Routing;

namespace HabitTracker.Web.Layout;

public partial class NavMenu
{
    private bool collapseNavMenu = true;
    private bool isReportsExpanded = false;

    private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;

    private void ToggleReports() => isReportsExpanded = !isReportsExpanded;

    protected override void OnInitialized()
    {
        Navigation.LocationChanged += OnLocationChanged;
        UpdateReportsExpanded();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        UpdateReportsExpanded();
        InvokeAsync(StateHasChanged);
    }

    private void UpdateReportsExpanded()
    {
        var currentUri = Navigation.Uri.ToLower();
        var isReportLink = currentUri.Contains("/report/");
        if (isReportLink is true) isReportsExpanded = true;
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= OnLocationChanged;
    }
}

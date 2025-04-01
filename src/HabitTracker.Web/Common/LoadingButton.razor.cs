namespace HabitTracker.Web.Common;

public partial class LoadingButton
{
    [Parameter]
    public string IconClass { get; set; } = "bi bi-play";

    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public int SpinnerDelay { get; set; } = 100;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _isLoading = false;

    private async Task OnClickAsync()
    {
        // prevent multiple clicks while operation is running.
        if (_isLoading || OnClick.HasDelegate == false) return;

        try
        {
            var delayTask = Task.Delay(SpinnerDelay);
            Task clickTask = OnClick.InvokeAsync();

            // wait for the first task to complete.
            await Task.WhenAny(delayTask, clickTask);

            // if the OnClick event callback hasn't completed yet, then wait for it.
            if (!clickTask.IsCompleted)
            {
                _isLoading = true;
                StateHasChanged();

                await clickTask;
            }
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }
}

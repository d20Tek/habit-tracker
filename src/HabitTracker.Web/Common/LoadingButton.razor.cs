using Microsoft.AspNetCore.Components.Forms;

namespace HabitTracker.Web.Common;

public partial class LoadingButton
{
    [Parameter]
    public string IconClass { get; set; } = "bi bi-play";

    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public EventCallback? OnClick { get; set; } // For normal buttons
    
    [Parameter]
    public EventCallback? OnValidSubmit { get; set; } // For form submission
    
    [CascadingParameter]
    public EditContext? CascadedEditContext { get; set; }

    [Parameter]
    public int SpinnerDelay { get; set; } = 100;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _isLoading = false;

    private async Task OnClickAsync()
    {
        // prevent multiple clicks while operation is running.
        if (_isLoading) return;
        if (OnClick is null && OnValidSubmit is null) return;

        try
        {
            var delayTask = Task.Delay(SpinnerDelay);
            Task clickTask = GetClickTask();

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

    private Task GetClickTask()
    {
        Task clickTask = Task.CompletedTask;

        if (OnClick is not null)
        {
            clickTask = OnClick.Value.InvokeAsync();
        }
        else if (OnValidSubmit is not null && CascadedEditContext is not null)
        {
            clickTask = OnValidSubmit.Value.InvokeAsync(CascadedEditContext);
        }

        return clickTask;
    }
}

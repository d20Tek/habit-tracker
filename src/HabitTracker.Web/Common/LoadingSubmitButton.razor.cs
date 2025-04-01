using Microsoft.AspNetCore.Components.Forms;

namespace HabitTracker.Web.Common;

public partial class LoadingSubmitButton
{
    [Parameter]
    public string IconClass { get; set; } = string.Empty;
    
    [Parameter]
    public string Label { get; set; } = string.Empty;
    
    [Parameter]
    public EventCallback<EditContext> OnValidSubmit { get; set; }
    
    [Parameter]
    public int SpinnerDelay { get; set; } = 100;

    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _isLoading = false;

    private async Task HandleSubmitAsync()
    {
        if (_isLoading || OnValidSubmit.HasDelegate == false || CascadedEditContext is null) return;

        try
        {
            var delayTask = Task.Delay(SpinnerDelay);
            Task submitTask = OnValidSubmit.InvokeAsync(CascadedEditContext);

            await Task.WhenAny(delayTask, submitTask);

            if (!submitTask.IsCompleted)
            {
                _isLoading = true;
                StateHasChanged();

                await submitTask;
            }
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }
}

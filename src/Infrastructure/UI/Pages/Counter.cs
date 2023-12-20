using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class CounterBase : ComponentBase
{
    protected int currentCount = 0;

    protected void IncrementCount()
    {
        currentCount++;
    }
}

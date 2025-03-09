namespace HabitTracker.Api.Features.Categories;

internal record CategoryResponse(int Id, string Name, string UserId);

internal record CreateCategoryRequest(string Name, string UserId);

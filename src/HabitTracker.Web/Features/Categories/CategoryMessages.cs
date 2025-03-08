namespace HabitTracker.Web.Features.Categories;

internal record CategoryResponse(int CategoryId, string Name, string UserId);

internal record CreateCategoryRequest(string Name);
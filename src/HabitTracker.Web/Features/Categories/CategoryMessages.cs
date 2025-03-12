namespace HabitTracker.Web.Features.Categories;

internal record CategoryResponse(int Id, string Name, string UserId);

internal record CreateCategoryRequest(string Name);

internal record UpdateCategoryRequest(int Id, string Name);

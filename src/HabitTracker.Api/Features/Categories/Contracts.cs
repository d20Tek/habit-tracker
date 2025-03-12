using HabitTracker.Api.Domain;
using System.Text.Json.Serialization;

namespace HabitTracker.Api.Features.Categories;

internal record CategoryResponse(int Id, string Name, string UserId)
{
    public static CategoryResponse FromEntity(Category category) =>
        new(category.CategoryId, category.Name, category.UserId);
}

internal record CreateCategoryRequest(string Name)
{
    [JsonIgnore]
    public string UserId { get; private set; } = string.Empty;

    public CreateCategoryRequest AppendUserId(string userId) => this with { UserId = userId };

    public Category ToEntity() => Category.Create(Name, UserId);
}

internal record UpdateCategoryRequest(int Id, string Name)
{
    [JsonIgnore]
    public string UserId { get; private set; } = string.Empty;

    public UpdateCategoryRequest AppendUserId(string userId) => this with { UserId = userId };
}

internal record DeleteCategoryRequest(int Id, string UserId);

internal record GetCategoryByIdRequest(int Id, string UserId);

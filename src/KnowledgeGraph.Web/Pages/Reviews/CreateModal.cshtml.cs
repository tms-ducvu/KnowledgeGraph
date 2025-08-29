using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeGraph.Reviews;
using KnowledgeGraph.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnowledgeGraph.Web.Pages.Reviews;

public class CreateModalModel : KnowledgeGraphPageModel
{
    [BindProperty]
    public CreateReviewDto Review { get; set; } = new CreateReviewDto();

    public List<SelectListItem> EntityList { get; set; }
    public List<SelectListItem> PlatformList { get; set; }
    public List<SelectListItem> RatingList { get; set; }

    private readonly IReviewService _reviewService;
    private readonly IEntityService _entityService;

    public CreateModalModel(IReviewService reviewService, IEntityService entityService)
    {
        _reviewService = reviewService;
        _entityService = entityService;
        
        // Initialize lists to avoid null reference
        EntityList = new List<SelectListItem>();
        PlatformList = new List<SelectListItem>();
        RatingList = new List<SelectListItem>();
    }

    public async Task OnGetAsync()
    {
        try
        {
            Review = new CreateReviewDto();
            
            // Set default values
            Review.ReviewReviewDate = DateTime.Now;
            Review.ReviewRating = 5;
            
            // Populate PlatformList
            PlatformList = new List<SelectListItem>
            {
                new SelectListItem("Chọn nền tảng", ""),
                new SelectListItem("Facebook", "00000000-0000-0000-0000-000000000001"),
                new SelectListItem("Google", "00000000-0000-0000-0000-000000000002"),
                new SelectListItem("TripAdvisor", "00000000-0000-0000-0000-000000000003"),
                new SelectListItem("Yelp", "00000000-0000-0000-0000-000000000004"),
                new SelectListItem("Other", "00000000-0000-0000-0000-000000000005")
            };
            
            // Populate RatingList
            RatingList = new List<SelectListItem>
            {
                new SelectListItem("Chọn đánh giá", ""),
                new SelectListItem("1 ⭐", "1"),
                new SelectListItem("2 ⭐⭐", "2"),
                new SelectListItem("3 ⭐⭐⭐", "3"),
                new SelectListItem("4 ⭐⭐⭐⭐", "4"),
                new SelectListItem("5 ⭐⭐⭐⭐⭐", "5")
            };
            
            // Load EntityList from _entityService
            var entities = await _entityService.GetListAsync(new FilterEntityDto
            {
                MaxResultCount = 1000,
                SkipCount = 0
            });
            
            EntityList = entities.Items.Select(e => new SelectListItem(e.EntityName, e.Id.ToString())).ToList();
            EntityList.Insert(0, new SelectListItem("Chọn doanh nghiệp", ""));
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _reviewService.CreateAsync(Review);
        if (result.success == true)
        {
            return NoContent();
        }
        else
        {
            // Handle error case
            ModelState.AddModelError("", result.message ?? "An error occurred while creating the review.");
            return Page();
        }
    }
}

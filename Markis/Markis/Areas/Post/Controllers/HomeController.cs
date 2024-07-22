using AutoMapper;
using Markis.Application.DTOs;
using Markis.Application.Services.Comments;
using Markis.Application.Services.Posts;
using Markis.Infrastructure.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[Area("Post")]
public class HomeController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;
    private readonly IHubContext<CommentHub> _hubContext;

    public HomeController(IPostService postService,
        IMapper mapper,
        UserManager<IdentityUser> userManager,
        ICommentService commentService,
        IHubContext<CommentHub> hubContext)
    {
        _postService = postService;
        _mapper = mapper;
        _userManager = userManager;
        _commentService = commentService;
        _hubContext = hubContext;
    }

    public async Task<IActionResult> Index(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        post.Comments = await _commentService.GetCommentsByPostIdAsync(id);

        return View(post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        var currentUserId = await _userManager.GetUserAsync(User);
        var userProfile = await _postService.GetUserProfileByIdentityUserIdAsync(currentUserId.Id);

        await _postService.DeletePostAsync(id);
        return RedirectToAction("Index", "Home", new { area = "Author", id = userProfile.IdentityUserId });
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDto addComment)
    {
        if (string.IsNullOrWhiteSpace(addComment.Text))
        {
            ModelState.AddModelError(string.Empty, "Comment text is required.");
            return Json(new { success = false, message = "Comment text is required." });
        }

        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        var commentDto = new CommentDto
        {
            UserName = user.UserName,
            Text = addComment.Text,
            ReleaseDate = DateTime.UtcNow,
            PostId = addComment.PostId,
            IdentityUserId = userId
        };

        await _commentService.AddCommentAsync(commentDto, addComment.PostId, userId);

        var commentHub = (IHubContext<CommentHub>)HttpContext.RequestServices.GetService(typeof(IHubContext<CommentHub>));
        await commentHub.Clients.All.SendAsync("ReceiveComment", user.UserName, addComment.Text, addComment.PostId);

        return Json(new { success = true });
    }

}

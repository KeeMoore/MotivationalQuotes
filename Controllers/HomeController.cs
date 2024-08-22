using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MotivationalQuotes.Models;
using Microsoft.AspNetCore.Identity;
using MotivationalQuotes.Context;
using MotivationalQuotes.Attributes;
using Microsoft.EntityFrameworkCore;

namespace MotivationalQuotes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }



    // New action method for the landing page
    [HttpGet("")]
    public IActionResult Landing()
    {
        return View("Landing");
    }

    [HttpGet("home")]
    public IActionResult Index(string? message)
    {
        ViewBag.Message = message;

        var homePageViewModel = new HomePageViewModel()
        {
            User = new User(),
            LoginUser = new LoginUser(),
        };

        return View("Index", homePageViewModel);
    }

    [HttpPost("register")]
    public IActionResult Register(User newUser)
    {
        if (!ModelState.IsValid)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                Console.WriteLine(message);
            }
            // form is invalid
            // show the form again with errors


            var homePageViewModel = new HomePageViewModel()
            {
                User = new User(),
                LoginUser = new LoginUser(),
            };
            return View("Index", homePageViewModel);
        }

        // form is valid
        // hash the user's password
        var hasher = new PasswordHasher<User>();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);

        // save the new user to the db
        _context.Users.Add(newUser);
        _context.SaveChanges();

        // login the user by storing their id in session
        HttpContext.Session.SetInt32("userId", newUser.UserId);

        // redirect user to dashboard
        return RedirectToAction("Dashboard");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUser loginUser)
    {
        if (!ModelState.IsValid)
        {
            // form is invalid
            // send them back to form to show errors
            var homePageViewModel = new HomePageViewModel()
            {
                User = new User(),
                LoginUser = new LoginUser(),
            };
            return View("Index", homePageViewModel);
        }

        // form is valid check if email exists
        var user = _context.Users.SingleOrDefault((user) => user.Email == loginUser.Email);

        if (user is null)
        {
            // email does not exist show a message
            return RedirectToAction("Index", new { message = "invalid-credentials" });
        }

        // check their password
        var hasher = new PasswordHasher<User>();

        PasswordVerificationResult result = hasher.VerifyHashedPassword(
            user,
            user.Password,
            loginUser.Password
        );

        if (result == 0)
        {
            // password is incorrect
            return RedirectToAction("Index", new { message = "invalid-credentials" });
        }

        // password is correct
        // login the user by storing their id in session
        HttpContext.Session.SetInt32("userId", user.UserId);

        // redirect user to dashboard
        return RedirectToAction("Dashboard");
    }

    [SessionCheck]
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {

        int? userId = HttpContext.Session.GetInt32("userId");
        if (userId is null)
        {
            return RedirectToAction("Index");
        }

        var user = _context.Users.FirstOrDefault((user) => user.UserId == userId);

        if (user is null)
        {
            return RedirectToAction("Index");
        }

        var quotes = _context.Quotes
        .Include(q => q.Likes)
        .Include(q => q.Comments)
            .ThenInclude(c => c.User)
        .ToList();



        var dashboardViewModel = new DashboardViewModel
        {
            User = user,
            Quotes = quotes
        };

        return View("Dashboard", dashboardViewModel);
    }

    [HttpGet("logout")]
    public RedirectToActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ViewQuote(int id)
    {
        var quote = _context.Quotes
            .Include(q => q.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefault(q => q.QuoteId == id);

        if (quote == null)
        {
            return NotFound();
        }

        return View(quote);
    }

    [HttpGet("create-quote")]
    public IActionResult CreateQuote()
    {
        return View();
    }

    [HttpPost("create-quote")]
    public IActionResult CreateQuote(Quote newQuote)
    {
        _logger.LogInformation("CreateQuote action called");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model state is invalid");
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                Console.WriteLine(message);
            }
            return View(newQuote);
        }

        int? userId = HttpContext.Session.GetInt32("userId");
        if (userId is null)
        {
            _logger.LogWarning("User ID is null");
            return RedirectToAction("Index");
        }

        newQuote.PostedByUserId = userId.Value;
        _context.Quotes.Add(newQuote);
        _context.SaveChanges();

        _logger.LogInformation("Quote added successfully");

        return RedirectToAction("Dashboard");
    }


    [HttpPost("home/like")]
    public IActionResult Like(int quoteId)
    {
        int? userId = HttpContext.Session.GetInt32("userId");
        if (userId is null)
        {
            return RedirectToAction("Index", "Home");
        }

        var quote = _context.Quotes
            .Include(q => q.Likes)
            .FirstOrDefault(q => q.QuoteId == quoteId);

        if (quote == null)
        {
            return RedirectToAction("Dashboard");
        }

        var like = new Like
        {
            QuoteId = quoteId,
            UserId = (int)userId
        };

        _context.Likes.Add(like);
        _context.SaveChanges();

        return RedirectToAction("Dashboard");
    }

    [HttpPost("home/unlike")]
    public IActionResult Unlike(int quoteId)
    {
        int? userId = HttpContext.Session.GetInt32("userId");
        if (userId is null)
        {
            return RedirectToAction("Index", "Home");
        }

        var like = _context.Likes
            .FirstOrDefault(l => l.QuoteId == quoteId && l.UserId == userId);

        if (like == null)
        {
            return RedirectToAction("Dashboard");
        }

        _context.Likes.Remove(like);
        _context.SaveChanges();

        return RedirectToAction("Dashboard");
    }



    [HttpPost]
    public IActionResult Comment(int quoteId, string text)
    {
        var quote = _context.Quotes.Include(q => q.Comments).FirstOrDefault(q => q.QuoteId == quoteId);
        if (quote == null) return NotFound();

        int? userId = HttpContext.Session.GetInt32("userId");
        if (userId == null) return Unauthorized();

        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null) return Unauthorized();

        quote.Comments.Add(new Comment { QuoteId = quoteId, UserId = user.UserId, Text = text });
        _context.SaveChanges();

        return RedirectToAction("Dashboard");
    }


}



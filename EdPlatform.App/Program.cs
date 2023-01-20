using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Services;
using EdPlatform.Business.Services;
using EdPlatform.Data;
using EdPlatform.Data.EF;
using EdPlatform.Data.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsql<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"), null, o => o.EnableSensitiveDataLogging());

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CourseEditPolicy", policy =>
        policy.Requirements.Add(new EditCourseRequirement()
    ));
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IAuthorizationHandler, CourseAuthorizationHandler>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IModuleService, ModuleService>();
builder.Services.AddTransient<ILessonService, LessonService>();
builder.Services.AddTransient<ICourseUserService, CourseUserService>();
builder.Services.AddTransient<IImageService, S3ImageService>();
builder.Services.AddTransient<ICodeExerciseService, CodeExerciseService>();
builder.Services.AddTransient<IIOCaseService, IOCaseService>();
builder.Services.AddTransient<IAttemptService, AttemptService>();
builder.Services.AddTransient<IExerciseService, ExerciseService>();
builder.Services.AddTransient<IFillExerciseService, FillExerciseService>();
builder.Services.AddTransient<ICheckFillExerciseAnswerService, CheckFillExerciseAnswerService>();
builder.Services.AddTransient<IQuizService, QuizService>();
builder.Services.AddTransient<ICaseService, CaseService>();
builder.Services.AddTransient<ICheckQuizAnswerService, CheckQuizAnswerService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddHttpClient<ICodeExecutingService, CodeExecutingService>();

builder.Services.AddTransient<ICustomAuthorizationViewService, CustomAuthorizationViewService>();
builder.Services.AddTransient<ICompletedLessonsViewService, CompletedLessonsViewService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.UseCookiePolicy(cookiePolicyOptions);

app.Run();
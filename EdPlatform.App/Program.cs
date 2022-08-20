using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddHttpClient<ICodeExecutingService, CodeExecutingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
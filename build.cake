var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./**/bin");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    foreach(var proj in GetFiles("**/*.csproj"))
    {
        DotNetBuild(proj.FullPath, new DotNetBuildSettings
        {
            Configuration = configuration,
        });
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest("./Marqi.Test/Marqi.Test.csproj", new DotNetTestSettings
    {
        Configuration = configuration,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
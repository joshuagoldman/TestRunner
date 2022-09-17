using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestRunner.Client;
using TestRunner.Library;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddGetTestCaseFactory();
builder.Services.AddGetTestCasesFactory();

try
{
    builder.Services.AddTestCaseClass<GetLocationsTests>();
}
catch (Exception e)
{

    Console.Write(e.ToString());
}


await builder.Build().RunAsync();

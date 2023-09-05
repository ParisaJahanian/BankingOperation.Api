using BankingOperationsApi.Data;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLogging(builder.Configuration, builder.Environment);
builder.Services.AddDbContext<FaraboomDbContext>(options =>
   options.UseOracle(builder.Configuration["ConnectionStrings:FaraboomConnection"]));
builder.Services.AddHttpClient<ISatnaTransferClient, SatnaTransferClient>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFaraboomServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(StartupBase));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.UseAuthorization();
app.MapControllers();
app.Run();

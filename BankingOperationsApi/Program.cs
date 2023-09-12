using BankingOperationsApi.Data;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLogging(builder.Configuration, builder.Environment);
builder.Services.AddDbContext<FaraboomDbContext>(options =>
   options.UseOracle(builder.Configuration["ConnectionStrings:FaraboomConnection"]));
builder.Services.AddHttpClient<ISatnaTransferClient, SatnaTransferClient>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFaraboomServices(builder.Configuration);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json", $" پنل سرویس های فرابوم"));
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.UseAuthorization();
app.MapControllers();
app.Run();

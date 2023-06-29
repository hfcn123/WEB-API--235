using Microsoft.EntityFrameworkCore;
using WebApI172.Models;

var builder = WebApplication.CreateBuilder(args);


#region �������ݿ�

// Add services to the container.
var config = new ConfigurationBuilder()
           .AddInMemoryCollection() //�������ļ������ݼ��ص��ڴ���
           .SetBasePath(Directory.GetCurrentDirectory()) //ָ�������ļ����ڵ�Ŀ¼
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //ָ�����ص������ļ�  --���ص�..�ǵ�ʼ�ո���
           .Build(); //����ɶ���  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<StudentsContext>(optionsBuilder => optionsBuilder.UseMySql(config.GetConnectionString("ConnectionString"), new MySqlServerVersion(new Version(8, 0, 33)), mySqlOptions =>
{
    mySqlOptions.MigrationsAssembly("WebApI172");
}
), poolSize: 5);

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

# Identity.Net.Simple
Implementing JWT Authentication in .NET 8


## ASP.NET Core Identity & Token-based Authentication Sample

This repository contains a simple example of how to implement ASP.NET Core Identity and Token-based Authentication in an ASP.NET Core application. The sample focuses on the basics of user authentication and authorization, without any specific architecture in mind. This sample uses SQLite as the database for simplicity.

### Features:
- ASP.NET Core Identity for managing user authentication and authorization
- Token-based authentication using JWT
- SQLite as the database
- Easy integration for secure authentication in .NET Core applications

### How to Install and Set Up

Follow these steps to set up the project and get it running:

1. Clone the repository
 
   git clone [GitHub Repository Link]
   cd [Repository Folder]
   

2. Install the required packages
file, make sure that ASP.NET Core Identity, JWT Authentication, and SQLite dependencies are installed. You can add them using the following commands:
 
   dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
   

3. Configure Identity in Program.cs file
   configure Identity services and use SQLite as the database:
  
 
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

   builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();
   
   builder.Services.AddAuthentication(options =>
   {
       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   }).AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           // Add your Token configurations here
       };
   });
   
Update Databasese**
   Apply migrations to set up the Identity database tables using SQLite:
  
 
   dotnet ef migrations add InitialIdentitySetup
   dotnet ef database update
   
Run the applicationon**
   Finally, run the application using the following command:

 
   dotnet run
   

### Notes:
- This sample does not focus on any specific architecture to keep it simple and easy to follow.
- ASP.NET Core Identity makes it straightforward to manage user authentication and authorization. You can further customize it to fit your application needs.
- SQLite is used in this project to simplify the setup. For production, you can switch to another database like SQL Server.

Feel free to explore, and don’t hesitate to reach out with any questions or suggestions!

dotnet ef dbcontext scaffold "Server=TIG3R\SQLEXPRESS;Database=TODO_LIST;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o DataContext --context DataContext

dotnet add API/API.csproj reference Data/Data.csproj

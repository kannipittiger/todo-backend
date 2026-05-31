dotnet ef dbcontext scaffold "Server=localhost;Database=TODO_LIST;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o DataContext1 --context DataContext

dotnet add API/API.csproj reference Data/Data.csproj

# Етап побудови (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копіюємо всі файли проектів для відновлення залежностей
COPY ["DoDay/API Layer.csproj", "DoDay/"]
COPY ["Business Logic Layer/Business Logic Layer.csproj", "Business Logic Layer/"]
COPY ["Data Access Layer/Data Access Layer.csproj", "Data Access Layer/"]

# Відновлюємо залежності для всього рішення
RUN dotnet restore "DoDay/API Layer.csproj"

# Копіюємо ВЕСЬ вихідний код
COPY . .

# Будуємо проект
WORKDIR "/src/DoDay"
RUN dotnet build "API Layer.csproj" -c Release -o /app/build

# Етап публікації (важливо: робимо publish саме тут)
FROM build AS publish
RUN dotnet publish "API Layer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Фінальний етап (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
# Копіюємо все, що було опубліковано, включаючи всі DLL залежностей
COPY --from=publish /app/publish .

# ПЕРЕВІР: назва файлу має точно збігатися з вихідною DLL
ENTRYPOINT ["dotnet", "API Layer.dll"]
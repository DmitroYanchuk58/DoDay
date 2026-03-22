# Етап побудови (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["API_Layer/API_Layer.csproj", "API_Layer/"]
COPY ["Business_Logic_Layer/Business_Logic_Layer.csproj", "Business_Logic_Layer/"]
COPY ["Data_Access_Layer/Data_Access_Layer.csproj", "Data_Access_Layer/"]
RUN dotnet restore "API_Layer/API_Layer.csproj"
COPY . .
WORKDIR "/src/API_Layer"
RUN dotnet build "API_Layer.csproj" -c Release -o /app/build

# Етап публікації
FROM build AS publish
RUN dotnet publish "API_Layer.csproj" -c Release -o /app/publish

# Фінальний етап (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Layer.dll"]
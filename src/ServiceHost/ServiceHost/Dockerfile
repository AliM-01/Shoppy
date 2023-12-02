FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ServiceHost/ServiceHost/ServiceHost.csproj", "src/ServiceHost/ServiceHost/"]
COPY ["src/0_Framework/0_Framework.csproj", "src/0_Framework/"]

COPY ["src/Modules/AccountModule/Domain/AM.Domain/AM.Domain.csproj", "src/Modules/AccountModule/Domain/AM.Domain/"]
COPY ["src/Modules/AccountModule/Application/AM.Application/AM.Application.csproj", "src/Modules/AccountModule/Application/AM.Application/"]
COPY ["src/Modules/AccountModule/Infrastructure/AM.Infrastructure/AM.Infrastructure.csproj", "src/Modules/AccountModule/Infrastructure/AM.Infrastructure/"]

COPY ["src/Modules/BlogModule/Domain/BM.Domain/BM.Domain.csproj", "src/Modules/BlogModule/Domain/BM.Domain/"]
COPY ["src/Modules/BlogModule/Application/BM.Application/BM.Application.csproj", "src/Modules/BlogModule/Application/BM.Application/"]
COPY ["src/Modules/BlogModule/Infrastructure/BM.Infrastructure/BM.Infrastructure.csproj", "src/Modules/BlogModule/Infrastructure/BM.Infrastructure/"]

COPY ["src/Modules/CommentModule/Domain/CM.Domain/CM.Domain.csproj", "src/Modules/CommentModule/Domain/CM.Domain/"]
COPY ["src/Modules/CommentModule/Application/CM.Application/CM.Application.csproj", "src/Modules/CommentModule/Application/CM.Application/"]
COPY ["src/Modules/CommentModule/Infrastructure/CM.Infrastructure/CM.Infrastructure.csproj", "src/Modules/CommentModule/Infrastructure/CM.Infrastructure/"]

COPY ["src/Modules/DiscountModule/Domain/DM.Domain/DM.Domain.csproj", "src/Modules/DiscountModule/Domain/DM.Domain/"]
COPY ["src/Modules/DiscountModule/Application/DM.Application/DM.Application.csproj", "src/Modules/DiscountModule/Application/DM.Application/"]
COPY ["src/Modules/DiscountModule/Infrastructure/DM.Infrastructure/DM.Infrastructure.csproj", "src/Modules/DiscountModule/Infrastructure/DM.Infrastructure/"]

COPY ["src/Modules/InventoryModule/Domain/IM.Domain/IM.Domain.csproj", "src/Modules/InventoryModule/Domain/IM.Domain/"]
COPY ["src/Modules/InventoryModule/Application/IM.Application/IM.Application.csproj", "src/Modules/InventoryModule/Application/IM.Application/"]
COPY ["src/Modules/InventoryModule/Infrastructure/IM.Infrastructure/IM.Infrastructure.csproj", "src/Modules/InventoryModule/Infrastructure/IM.Infrastructure/"]

COPY ["src/Modules/OrderModule/Domain/OM.Domain/OM.Domain.csproj", "src/Modules/OrderModule/Domain/OM.Domain/"]
COPY ["src/Modules/OrderModule/Application/OM.Application/OM.Application.csproj", "src/Modules/OrderModule/Application/OM.Application/"]
COPY ["src/Modules/OrderModule/Infrastructure/OM.Infrastructure/OM.Infrastructure.csproj", "src/Modules/OrderModule/Infrastructure/OM.Infrastructure/"]

COPY ["src/Modules/ShopModule/Domain/SM.Domain/SM.Domain.csproj", "src/Modules/ShopModule/Domain/SM.Domain/"]
COPY ["src/Modules/ShopModule/Application/SM.Application/SM.Application.csproj", "src/Modules/ShopModule/Application/SM.Application/"]
COPY ["src/Modules/ShopModule/Infrastructure/SM.Infrastructure/SM.Infrastructure.csproj", "src/Modules/ShopModule/Infrastructure/SM.Infrastructure/"]

RUN dotnet restore "src/ServiceHost/ServiceHost/ServiceHost.csproj"
COPY . .
WORKDIR "/src/src/ServiceHost/ServiceHost"
RUN dotnet build "ServiceHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceHost.dll"]
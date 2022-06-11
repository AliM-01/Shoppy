#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApi/Shoppy.WebApi/Shoppy.WebApi.csproj", "src/WebApi/Shoppy.WebApi/"]
COPY ["src/Modules/DiscountModule/Application.Contracts/DM.Application.Contracts/DM.Application.Contracts.csproj", "src/Modules/DiscountModule/Application.Contracts/DM.Application.Contracts/"]
COPY ["src/Modules/DiscountModule/Domain/DM.Domain/DM.Domain.csproj", "src/Modules/DiscountModule/Domain/DM.Domain/"]
COPY ["src/Modules/ShopModule/Domain/SM.Domin/SM.Domain.csproj", "src/Modules/ShopModule/Domain/SM.Domin/"]
COPY ["src/0_Framework/0_Framework.csproj", "src/0_Framework/"]
COPY ["src/02_DI_Container/02_DI_Container.csproj", "src/02_DI_Container/"]
COPY ["src/Modules/DiscountModule/Infrastructure/DM.Infrastructure.Shared/DM.Infrastructure.Shared.csproj", "src/Modules/DiscountModule/Infrastructure/DM.Infrastructure.Shared/"]
COPY ["src/01_Shoppy.Query/01_Shoppy.Query.csproj", "src/01_Shoppy.Query/"]
COPY ["src/Modules/InventoryModule/Application.Contracts/IM.Application.Contracts/IM.Application.Contracts.csproj", "src/Modules/InventoryModule/Application.Contracts/IM.Application.Contracts/"]
COPY ["src/Modules/InventoryModule/Domain/IM.Domain/IM.Domain.csproj", "src/Modules/InventoryModule/Domain/IM.Domain/"]
COPY ["src/Modules/DiscountModule/Infrastructure/DM.Infrastructure.Persistence/DM.Infrastructure.Persistence.csproj", "src/Modules/DiscountModule/Infrastructure/DM.Infrastructure.Persistence/"]
COPY ["src/Modules/ShopModule/Infrastructure/SM.Infrastructure.Persistence/SM.Infrastructure.Persistence.csproj", "src/Modules/ShopModule/Infrastructure/SM.Infrastructure.Persistence/"]
COPY ["src/Modules/CommentModule/Infrastructure/CM.Infrastructure.Persistence/CM.Infrastructure.Persistence.csproj", "src/Modules/CommentModule/Infrastructure/CM.Infrastructure.Persistence/"]
COPY ["src/Modules/CommentModule/Domain/CM.Domain/CM.Domain.csproj", "src/Modules/CommentModule/Domain/CM.Domain/"]
COPY ["src/Modules/BlogModule/Infrastructure/BM.Infrastructure.Persistence/BM.Infrastructure.Persistence.csproj", "src/Modules/BlogModule/Infrastructure/BM.Infrastructure.Persistence/"]
COPY ["src/Modules/BlogModule/Domain/BM.Domain/BM.Domain.csproj", "src/Modules/BlogModule/Domain/BM.Domain/"]
COPY ["src/Modules/ShopModule/Application.Contracts/SM.Application.Contracts/SM.Application.Contracts.csproj", "src/Modules/ShopModule/Application.Contracts/SM.Application.Contracts/"]
COPY ["src/Modules/InventoryModule/Infrastructure/IM.Infrastructure.Persistence/IM.Infrastructure.Persistence.csproj", "src/Modules/InventoryModule/Infrastructure/IM.Infrastructure.Persistence/"]
COPY ["src/Modules/AccountModule/Infrastructure/AM.Infrastructure.Persistence/AM.Infrastructure.Persistence.csproj", "src/Modules/AccountModule/Infrastructure/AM.Infrastructure.Persistence/"]
COPY ["src/Modules/AccountModule/Domain/AM.Domain/AM.Domain.csproj", "src/Modules/AccountModule/Domain/AM.Domain/"]
COPY ["src/Modules/OrderModule/Infrastructure/OM.Infrastructure.Configuration/OM.Infrastructure.Configuration.csproj", "src/Modules/OrderModule/Infrastructure/OM.Infrastructure.Configuration/"]
COPY ["src/Modules/OrderModule/Application.Contracts/OM.Application.Contracts/OM.Application.Contracts.csproj", "src/Modules/OrderModule/Application.Contracts/OM.Application.Contracts/"]
COPY ["src/Modules/OrderModule/Infrastructure/OM.Infrastructure.Persistence/OM.Infrastructure.Persistence.csproj", "src/Modules/OrderModule/Infrastructure/OM.Infrastructure.Persistence/"]
COPY ["src/Modules/OrderModule/Domain/OM.Domain/OM.Domain.csproj", "src/Modules/OrderModule/Domain/OM.Domain/"]
COPY ["src/Modules/OrderModule/Application/OM.Application/OM.Application.csproj", "src/Modules/OrderModule/Application/OM.Application/"]
COPY ["src/Modules/AccountModule/Infrastructure/AM.Infrastructure.Shared/AM.Infrastructure.Shared.csproj", "src/Modules/AccountModule/Infrastructure/AM.Infrastructure.Shared/"]
COPY ["src/Modules/AccountModule/Application.Contracts/AM.Application.Contracts/AM.Application.Contracts.csproj", "src/Modules/AccountModule/Application.Contracts/AM.Application.Contracts/"]
COPY ["src/Modules/InventoryModule/Application/IM.Application/IM.Application.csproj", "src/Modules/InventoryModule/Application/IM.Application/"]
COPY ["src/Modules/InventoryModule/Infrastructure/Acl/IM.Infrastructure.ProductAcl/IM.Infrastructure.ProductAcl.csproj", "src/Modules/InventoryModule/Infrastructure/Acl/IM.Infrastructure.ProductAcl/"]
COPY ["src/Modules/ShopModule/Infrastructure/SM.Infrastructure.Shared/SM.Infrastructure.Shared.csproj", "src/Modules/ShopModule/Infrastructure/SM.Infrastructure.Shared/"]
COPY ["src/Modules/InventoryModule/Infrastructure/Acl/IM.Infrastructure.AccountAcl/IM.Infrastructure.AccountAcl.csproj", "src/Modules/InventoryModule/Infrastructure/Acl/IM.Infrastructure.AccountAcl/"]
COPY ["src/Modules/InventoryModule/Infrastructure/IM.Infrastructure.Shared/IM.Infrastructure.Shared.csproj", "src/Modules/InventoryModule/Infrastructure/IM.Infrastructure.Shared/"]
COPY ["src/Modules/CommentModule/Infrastructure/CM.Infrastructure.Configuration/CM.Infrastructure.Configuration.csproj", "src/Modules/CommentModule/Infrastructure/CM.Infrastructure.Configuration/"]
COPY ["src/Modules/CommentModule/Application.Contracts/CM.Application.Contracts/CM.Application.Contracts.csproj", "src/Modules/CommentModule/Application.Contracts/CM.Application.Contracts/"]
COPY ["src/Modules/CommentModule/Infrastructure/Acl/CM.Infrastructure.ArticleAcl/CM.Infrastructure.ArticleAcl.csproj", "src/Modules/CommentModule/Infrastructure/Acl/CM.Infrastructure.ArticleAcl/"]
COPY ["src/Modules/BlogModule/Infrastructure/BM.Infrastructure.Shared/BM.Infrastructure.Shared.csproj", "src/Modules/BlogModule/Infrastructure/BM.Infrastructure.Shared/"]
COPY ["src/Modules/BlogModule/Application.Contracts/BM.Application.Contracts/BM.Application.Contracts.csproj", "src/Modules/BlogModule/Application.Contracts/BM.Application.Contracts/"]
COPY ["src/Modules/CommentModule/Infrastructure/Acl/CM.Infrastructure.ProductAcl/CM.Infrastructure.ProductAcl.csproj", "src/Modules/CommentModule/Infrastructure/Acl/CM.Infrastructure.ProductAcl/"]
COPY ["src/Modules/DiscountModule/Infrastructure/DM.Infrastructure.Configuration/DM.Infrastructure.Configuration.csproj", "src/Modules/DiscountModule/Infrastructure/DM.Infrastructure.Configuration/"]
COPY ["src/Modules/DiscountModule/Application/DM.Application/DM.Application.csproj", "src/Modules/DiscountModule/Application/DM.Application/"]
COPY ["src/Modules/DiscountModule/Infrastructure/Acl/DM.Infrastructure.ProductAcl/DM.Infrastructure.ProductAcl.csproj", "src/Modules/DiscountModule/Infrastructure/Acl/DM.Infrastructure.ProductAcl/"]
COPY ["src/Modules/CommentModule/Application/CM.Application/CM.Application.csproj", "src/Modules/CommentModule/Application/CM.Application/"]
COPY ["src/03_Reports.Query/03_Reports.Query.csproj", "src/03_Reports.Query/"]
COPY ["src/Modules/InventoryModule/Infrastructure/IM.Infrastructure.Configuration/IM.Infrastructure.Configuration.csproj", "src/Modules/InventoryModule/Infrastructure/IM.Infrastructure.Configuration/"]
COPY ["src/Modules/AccountModule/Infrastructure/AM.Infrastructure.Configuration/AM.Infrastructure.Configuration.csproj", "src/Modules/AccountModule/Infrastructure/AM.Infrastructure.Configuration/"]
COPY ["src/Modules/AccountModule/Application/AM.Application/AM.Application.csproj", "src/Modules/AccountModule/Application/AM.Application/"]
COPY ["src/Modules/ShopModule/Infrastructure/SM.Infrastructure.Configuration/SM.Infrastructure.Configuration.csproj", "src/Modules/ShopModule/Infrastructure/SM.Infrastructure.Configuration/"]
COPY ["src/Modules/ShopModule/Application/SM.Application/SM.Application.csproj", "src/Modules/ShopModule/Application/SM.Application/"]
COPY ["src/Modules/CommentModule/Infrastructure/CM.Infrastructure.Shared/CM.Infrastructure.Shared.csproj", "src/Modules/CommentModule/Infrastructure/CM.Infrastructure.Shared/"]
COPY ["src/Modules/OrderModule/Infrastructure/OM.Infrastructure.Shared/OM.Infrastructure.Shared.csproj", "src/Modules/OrderModule/Infrastructure/OM.Infrastructure.Shared/"]
COPY ["src/Modules/BlogModule/Infrastructure/BM.Infrastructure.Configuration/BM.Infrastructure.Configuration.csproj", "src/Modules/BlogModule/Infrastructure/BM.Infrastructure.Configuration/"]
COPY ["src/Modules/BlogModule/Application/BM.Application/BM.Application.csproj", "src/Modules/BlogModule/Application/BM.Application/"]
RUN dotnet restore "src/WebApi/Shoppy.WebApi/Shoppy.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi/Shoppy.WebApi"
RUN dotnet build "Shoppy.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shoppy.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shoppy.WebApi.dll"]
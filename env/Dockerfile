FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10 AS builder

WORKDIR /app

COPY . ./

RUN dotnet publish ./src/DavisBot/DavisBot.csproj -c Release -o ./out

FROM scratch
FROM mcr.microsoft.com/dotnet/core/runtime:3.1

ENV LANG en_US.UTF-8
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false

WORKDIR /app

COPY --from=builder /app/out/ .

ENTRYPOINT ["dotnet", "DavisBot.dll"]
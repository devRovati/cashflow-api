############### Build Stage ###############

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder

WORKDIR /workspace

# ==== Copy files ====

COPY . ./

# ==== Build the app ====

RUN dotnet publish "src/CashFlowApi.WebApi/CashFlowApi.WebApi.csproj" -c Release -o ./publish

############### Run Stage ###############

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# ==== Create a non-root user to perform entrypoint ====
 
RUN addgroup --system --gid 1001 dotnetgroup && \
    adduser --system --uid 1001 cashflowuser

# ==== Create necessary directories ====

RUN mkdir bin

# ==== Copy built items from build image ====

COPY --from=builder /workspace/publish ./bin

# ==== Expose port ====

EXPOSE 8090

# ==== Use cashflowUser user ====

USER cashflowuser

# ==== Start api ====

ENTRYPOINT ["dotnet", "./bin/CashFlowApi.WebApi.dll"]

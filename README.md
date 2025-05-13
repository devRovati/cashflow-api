# CashFlowApi - Development Guide

## 1. Setting Up the Local Environment

### 1.1 Database (MySQL via Docker)

Run the following command to start a MySQL container locally.  
> Make sure to replace credentials with your own.

```bash
docker run --name mysql-local \
  -e MYSQL_ROOT_PASSWORD=rootPass \
  -e MYSQL_DATABASE=db_name \
  -e MYSQL_USER=user \
  -e MYSQL_PASSWORD=userPass \
  -p 3306:3306 \
  -d mysql:8.0.31
```

### 1.2 Local Settings
Create an appsettings.json file and put a Default ConnectionString:

```bash
"ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=db_name;user=user;password=userPass;"
}
```

## 2. Running the API
Navigate to the project folder and run:
```bash
dotnet run --project src/CashFlowApi.WebApi
```

The API should be available at https://localhost:7064 (or as defined in launchSettings.json).

#### 1.3 (optional) Aws initial setup (if it's necessary to validate the aws resources on local environment)
    Create IAM user
    Create IAM group and add the created user to the group
    Create the credentials file on %User_Profile%\.aws with the content:
        [default]
        aws_access_key_id=you-access-key
        aws_secret_access_key=your-secret
        region=projects-region

## 2. Automated tests
Run tests
```bash
dotnet test --collect:"XPlat Code Coverage"
```

Install reportgenerator globally (skip if you already have it)
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Run reportgenerator to gets the coverage stats on test/CoverageReport/index.hmtl
```bash
reportgenerator "-reports:test/**/TestResults/**/*.xml" "-targetdir:test/CoverageReport" -reporttypes:Html
```

## 3. Build and run the api on docker
Build the docker image
```bash
docker build -t cashflow-api .
```

Make sure you have a valid env_local file. Contact the project lead if you don't have it.

Run docker image
```bash
docker run --env-file env_local --name cashflow-api -p 8090:8090 cashflow-api
```
  <#
.SYNOPSIS
    Scafolding script for new platform project.
.DESCRIPTION
    The script will create a console or api project.
#>
 Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string]$Proj, 
    [switch]$Api = $False
)
$Version = "2.1"

function CheckSDKVersion {

    $Version = dotnet --version | Select-String -Pattern $Version
    
    if([string]::IsNullOrWhiteSpace($Version)){
        Write-Host "La version minima del SDK requerida es las $Version. Por favor instale la version correspondiente" -BackgroundColor White -ForegroundColor Red
        exit
    }
}

#CheckSDKVersion

$ProjectType = if($Api) { "Platform.ApiTemplate" } else { "Platform.ConsoleTemplate" }
$TemplateName = if($Api) { "platformapi" } else { "platformconsole" }
$Folder = if($Api) { "src\\api" } else { "src\\service" }

Write-Host "Verificando Templates...." -BackgroundColor Yellow -ForegroundColor Black
dotnet new --debug:reinit
Start-Sleep -Seconds 2
dotnet new -i "$ProjectType::*" --nuget-source http://nuget.test.andreani.com.ar/nuget/andreani

Write-Host "Inicializando git flow" -BackgroundColor Yellow -ForegroundColor Black
git flow init -d -f

Write-Host "Creando Proyecto desde template ..." -BackgroundColor Yellow -ForegroundColor Black
Start-Sleep -Seconds 2

dotnet new $TemplateName -n $Proj -o $Folder
dotnet new sln -n $Proj
dotnet sln $Proj.sln add .\\$Folder\\$Proj.csproj
dotnet restore

Write-Host "Proyecto $Proj creado exitosamente." -BackgroundColor Yellow -ForegroundColor Black





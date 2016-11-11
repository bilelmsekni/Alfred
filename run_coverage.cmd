
nuget install NUnit.Runners -Version 3.4.1 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
 
.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\tools\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe -targetargs:"/nologo /noshadow .\Alfred.Tests\bin\Release\Alfred.Tests.dll" -filter:"+[*]* -[*.Tests]*" -register:user
.\tools\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\results.xml
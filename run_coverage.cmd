
nuget install NUnit.Runners -Version 3.4.1 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.7.0 -OutputDirectory tools
 
.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\tools\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe -targetargs:".\Alfred.Domain.Tests\bin\Release\Alfred.Domain.Tests.dll .\Alfred.Dal.Tests\bin\Release\Alfred.Dal.Tests.dll .\Alfred.Dal.Implementation.Fake.Tests\bin\Release\Alfred.Dal.Implementation.Fake.Tests.dll .\Alfred.Shared.Tests\bin\Release\Alfred.Shared.Tests.dll" -filter:"+[Alfred*]* -[*.Tests]* -[*]*ObjectDiffPatch*" -register:user
.\tools\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover -i .\results.xml
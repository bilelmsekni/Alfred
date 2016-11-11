choco install "msbuild-sonarqube-runner" -y
MSBuild.SonarQube.Runner.exe begin /k:"skible:Alfred" /n:"Alfred" /v:"1.0.0" /d:sonar.host.url=https://sonarqube.com /d:sonar.login=[SonarQube_Token]
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" /t:Rebuild
MSBuild.SonarQube.Runner.exe end /d:sonar.login=[SonarQube_Token]
# Continuous integration tools

## Dotnet core

[JenkinsFile](Jenkinsfile.dotnetcore) Simple file for building and releasing this project. Is is built using pipreleie syntax. Cotent can be copied and pasted into Jenkins CI or referenced directly from Jenkins to GIT. Prerequisite for this file to work is that .Net core SDK 2.1+ is installed on Jenkins master/slave nodes. 

## Sonar qube
Integration with SonarQuba Quality check. There are two instances of this file.

Instance named [Jenkinsfile.sonarqube.simple](Jenkinsfile.sonarqube.simple) is using installed [SonnarScanner dot net tools](https://www.nuget.org/packages/dotnet-sonarscanner/) which will be utilised implicitly (with dotnet command `dotnet sonarscanner`)

Instance named [Jenkinsfile.sonarqube.jenkinstool](Jenkinsfile.sonarqube.jenkinstool) is used with [SonarQube Jenkins plugin](https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+Jenkins) installed with Jenkins.  
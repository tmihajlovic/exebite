# Continuous integration tools

## Dotnet core

[This JenkinsFile](Jenkinsfile.dotnetcore) is a simple file for building and releasing this project. Is is built using pipeline syntax. Content can be copied and pasted into Jenkins CI or referenced directly from Jenkins to GIT. Prerequisite for this file to work is that .Net core SDK 2.1+ is installed on Jenkins master/slave nodes. 

## Sonar qube
Integration with SonarQube Quality check. There are two instances of this file.

Instance named [Jenkinsfile.sonarqube.simple](Jenkinsfile.sonarqube.simple) is using installed [SonnarScanner dot net tools](https://www.nuget.org/packages/dotnet-sonarscanner/) which will be utilised implicitly (with dotnet command `dotnet sonarscanner`)

Instance named [Jenkinsfile.sonarqube.jenkinstool](Jenkinsfile.sonarqube.jenkinstool) is used with [SonarQube Jenkins plugin](https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+Jenkins) installed with Jenkins.  

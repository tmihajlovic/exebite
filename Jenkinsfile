pipeline {
 agent {
     label = 'Windows_server_standard_2016'
 }
 environment {
  dotnet = 'dotnet'
 }
 triggers {
        cron('H 08 * * 1-5')
 }
 stages {
  stage('Checkout') {
   steps {
    git url: 'https://github.com/execom-eu/exebite', branch: 'dev'
   }
  }
  stage('Clean') {
   steps {
    bat 'dotnet clean'
   }
  }
  stage('Build') {
   steps {
    bat 'dotnet build --configuration Release'
   }
  }
  stage('Test') {
   steps {
    bat 'dotnet test  /p:CollectCoverage=true'
   }
  }
 }
}
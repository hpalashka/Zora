pipeline {
  agent any
  stages {
    stage('Verify Branch') {
       steps {
         echo "$GIT_BRANCH"
       }
    }
    stage('Pull Changes') {
      steps {
        powershell(script: "git pull")
      }
    }
    stage('Run Unit Tests') {
      steps {
        powershell(script: """ 
          dotnet test
        """)
      }
    }
    stage('Docker Build') {
      steps {
        powershell(script: 'docker-compose build')   
        powershell(script: 'docker images -a')
      }
    }
    stage('Run Test Application') {
      steps {
        powershell(script: 'docker-compose up -d')    
      }
    }
    stage('Run Integration Tests') {
      steps {
        powershell(script: './Tests/ContainerTests.ps1') 
        powershell(script: './Tests/ContainerTests2.ps1') 
      }
    }
    stage('Stop Test Application') {
      steps {
        powershell(script: 'docker-compose down') 
      } 
      post {
	    success {
	      echo "Build successfull! You should deploy! :)"
	    }
	    failure {
	      echo "Build failed! You should receive an e-mail! :("
	    }
      }
    }
    stage('Push Images') {
      when { branch 'master' }
      steps {
        script {
          docker.withRegistry('https://index.docker.io/v1/','DockerHubCredentials') {

            def imageIdentityService = docker.image("hpalashka/zora-identity-service")
            imageIdentityService.push("1.0.${env.BUILD_ID}")

            def imageWebService = docker.image("hpalashka/zora-web-service")
            imageWebService.push("1.0.${env.BUILD_ID}")

            def imageStudentsService = docker.image("hpalashka/zora-student-service")
            imageStudentsService.push("1.0.${env.BUILD_ID}")

            def imagePaymentsService = docker.image("hpalashka/zora-payments-service")
            imagePaymentsService.push("1.0.${env.BUILD_ID}")

            def imageOutstandingService = docker.image("hpalashka/zora-outstandingpayments-service")
            imageOutstandingService.push("1.0.${env.BUILD_ID}")

            def imageStatisticsService = docker.image("hpalashka/zora-statistics-service")
            imageStatisticsService.push("1.0.${env.BUILD_ID}")

            def imageNotificationsService = docker.image("hpalashka/zora-notifications-service")
            imageNotificationsService.push("1.0.${env.BUILD_ID}")

            def imageWatchDogService = docker.image("hpalashka/zora-watchdog-service")
            imageWatchDogService.push("1.0.${env.BUILD_ID}")
            
            //image.push('latest')
          }
        }
      }
    } 
  }
}

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

    stage('Docker Build Dev') {
      when { branch 'dev' }
      steps {
        powershell(script: 'docker-compose build')   
        powershell(script: 'docker images -a')
      }
    }

    stage('Docker Build Production') {
      when { branch 'master' }
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

    /*to do uncomment*/
    /*stage('Run Integration Tests') {
      steps {
        powershell(script: './Tests/ContainerTests.ps1') 
      }
    }*/
 
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

    stage('Push Images for Dev') {
      when { branch 'dev' } 
      steps {
        script {
          docker.withRegistry('https://index.docker.io/v1/','DockerHubCredentials') {

            def imageIdentityService = docker.image("hpalashka/zora-identity-service")
            imageIdentityService.push('latest')

            def imageWebService = docker.image("hpalashka/zora-web-service")
            imageWebService.push('latest')

            def imageStudentsService = docker.image("hpalashka/zora-student-service")
            imageStudentsService.push('latest')

            def imagePaymentsService = docker.image("hpalashka/zora-payments-service")
            imagePaymentsService.push('latest')

            def imageOutstandingService = docker.image("hpalashka/zora-outstandingpayments-service")
            imageOutstandingService.push('latest')

            def imageStatisticsService = docker.image("hpalashka/zora-statistics-service")
            imageStatisticsService.push('latest')

            def imageNotificationsService = docker.image("hpalashka/zora-notifications-service")
            imageNotificationsService.push('latest')

            def imageWatchDogService = docker.image("hpalashka/zora-watchdog-service")
            imageWatchDogService.push('latest')
                   
          }
        }
      }
    }

    stage('Push Images for Production') {
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
          }
        }
      }
    } 

      stage('Deploy Dev') {
      when { branch 'dev' }
      steps {
        script {
          def USER_INPUT = input(
            message: 'Do you confirm deployment?',
            parameters: [
              [$class: 'ChoiceParameterDefinition',
              choices: ['no','yes'].join('\n'),
              name: 'input',
              description: 'Plese confirm that you want to publish this to production.']
            ])

            echo "The answer is: ${USER_INPUT}"

            if( "${USER_INPUT}" == "yes"){
              withKubeConfig([credentialsId: 'DevelopmentServer', serverUrl: 'https://35.223.10.211']) 
              {
                powershell(script: 'kubectl apply -f ./.k8s/.environment/development.yml')
                powershell(script: 'kubectl apply -f ./.k8s/databases')
                powershell(script: 'kubectl apply -f ./.k8s/event-bus')
                powershell(script: 'kubectl apply -f ./.k8s/web-services')
                powershell(script: 'kubectl apply -f ./.k8s/clients')
              }
            } else {
              echo "User refused deployment."
               /*to do add production tests*/
            }
        }
      }
    } 

    /*todo change connection data and env file*/
    stage('Deploy Production') {
      when { branch 'master' }
      steps {
        script {
          def USER_INPUT = input(
            message: 'Do you confirm deployment?',
            parameters: [
              [$class: 'ChoiceParameterDefinition',
              choices: ['no','yes'].join('\n'),
              name: 'input',
              description: 'Plese confirm that you want to publish this to production.']
            ])

            echo "The answer is: ${USER_INPUT}"

            if( "${USER_INPUT}" == "yes"){
              withKubeConfig([credentialsId: 'DevelopmentServer', serverUrl: 'https://35.223.10.211']) 
              {
                powershell(script: 'kubectl apply -f ./.k8s/.environment/development.yml')
                powershell(script: 'kubectl apply -f ./.k8s/databases')
                powershell(script: 'kubectl apply -f ./.k8s/event-bus')
                powershell(script: 'kubectl apply -f ./.k8s/web-services')
                powershell(script: 'kubectl apply -f ./.k8s/clients')
              }
            } else {
              echo "User refused deployment."
               /*to do add production tests*/
            }
        }
      }
    } 
  
    stage('Run Integration Tests') {
      when { branch 'dev' }
      steps {
        powershell(script: './Tests/DevelopmentContainerTests.ps1') 
      }
    }
  }
}


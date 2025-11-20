// pipeline {
//     agent any

//     environment {
//         REPO_URL        = 'https://github.com/Suhasreddy257/xr-dashbaoard-backend-dev.git'
//         GIT_CREDENTIALS = 'token'
//         SOLUTION_FILE   = 'AP.CHRP.XRDB.WebApi.sln'
//         WEBAPI_PROJECT  = 'AP.CHRP.XRDB.WebApi/AP.CHRP.XRDB.WebApi.csproj'
//         PUBLISH_DIR     = 'D:\\backend_codebuildpipeline'
//     }

//     stages {
//         stage('Checkout') {
//             steps {
//                 git branch: 'master',          // 👈 change this
//                     credentialsId: GIT_CREDENTIALS,
//                     url: REPO_URL
//             }
//         }

//         stage('Restore') {
//             steps {
//                 bat """
//                 dotnet restore "${SOLUTION_FILE}"
//                 """
//             }
//         }

//         stage('Build') {
//             steps {
//                 bat """
//                 dotnet build "${SOLUTION_FILE}" -c Release --no-restore
//                 """
//             }
//         }

//         stage('Test') {
//             steps {
//                 bat """
//                 dotnet test "${SOLUTION_FILE}" -c Release --no-build
//                 """
//             }
//         }

//         stage('Publish') {
//             steps {
//                 bat """
//                 if exist "${PUBLISH_DIR}" rmdir /S /Q "${PUBLISH_DIR}"
//                 mkdir "${PUBLISH_DIR}"
//                 dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${PUBLISH_DIR}"
//                 """
//             }
//         }
//     }

//     post {
//         success { echo '✅ Build, test, and publish completed successfully.' }
//         failure { echo '❌ Pipeline failed. Check the logs above.' }
//     }
// }
// the above pipeline is working up to build

// pipeline {
//     agent any   // Windows Jenkins agent

//     environment {
//         REPO_URL        = 'https://github.com/Suhasreddy257/xr-dashbaoard-backend-dev.git'
//         GIT_CREDENTIALS = 'token'   // Jenkins credential ID
//         SOLUTION_FILE   = 'AP.CHRP.XRDB.WebApi.sln'
//         WEBAPI_PROJECT  = 'AP.CHRP.XRDB.WebApi/AP.CHRP.XRDB.WebApi.csproj'

//         // Folder where published files should go
//         PUBLISH_DIR     = 'D:\\backend_codebuildpipeline'

//         // IIS config
//         IIS_SITE_NAME   = 'XRdashboard_Backend'
//         IIS_APPPOOL     = 'XRdashboard_Backend'  // change if your app pool has a different name
//     }

//     stages {
//         stage('Checkout') {
//             steps {
//                 echo 'Checking out code from GitHub (master branch)...'
//                 git branch: 'master',
//                     credentialsId: GIT_CREDENTIALS,
//                     url: REPO_URL
//             }
//         }

//         stage('Restore') {
//             steps {
//                 echo 'Running dotnet restore...'
//                 bat """
//                 dotnet restore "${SOLUTION_FILE}"
//                 """
//             }
//         }

//         stage('Build') {
//             steps {
//                 echo 'Building solution in Release mode...'
//                 bat """
//                 dotnet build "${SOLUTION_FILE}" -c Release --no-restore
//                 """
//             }
//         }

//         stage('Test') {
//             steps {
//                 echo 'Running tests...'
//                 bat """
//                 dotnet test "${SOLUTION_FILE}" -c Release --no-build
//                 """
//             }
//         }

//         stage('Publish') {
//             steps {
//                 echo "Publishing WebApi project to ${PUBLISH_DIR} ..."
//                 // No delete here to avoid 'Access is denied' when IIS locks files
//                 bat """
//                 dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${PUBLISH_DIR}"
//                 """
//             }
//         }

//         stage('Deploy to IIS') {
//             steps {
//                 echo "Updating IIS site '${IIS_SITE_NAME}' and restarting..."
//                 // Run PowerShell natively (cleaner than putting PS inside bat)
//                 powershell '''
//                   Import-Module WebAdministration

//                   $siteName = $env:IIS_SITE_NAME
//                   $appPool  = $env:IIS_APPPOOL
//                   $path     = $env:PUBLISH_DIR

//                   Write-Host "Setting IIS site path for $siteName to $path"
//                   Set-ItemProperty "IIS:\\Sites\\$siteName" -Name physicalPath -Value $path

//                   Write-Host "Restarting App Pool: $appPool"
//                   Restart-WebAppPool -Name $appPool

//                   Write-Host "Restarting Site: $siteName"
//                   Restart-WebItem "IIS:\\Sites\\$siteName"
//                 '''
//             }
//         }
//     }

//     post {
//         success {
//             echo '✅ Build, test, publish, and IIS deploy completed successfully.'
//         }
//         failure {
//             echo '❌ Pipeline failed. Check the logs above in each stage.'
//         }
//     }
// }
// this pipeline is working all up to deployment also 

pipeline {
    agent any   Windows Jenkins agent

    environment {
        REPO_URL        = 'https://github.com/Suhasreddy257/xr-dashbaoard-backend-dev.git'
        GIT_CREDENTIALS = 'token'   // Jenkins credential ID
        SOLUTION_FILE   = 'AP.CHRP.XRDB.WebApi.sln'
        WEBAPI_PROJECT  = 'AP.CHRP.XRDB.WebApi/AP.CHRP.XRDB.WebApi.csproj'

        // Folder where published files should go
        PUBLISH_DIR     = 'D:\\backend_codebuildpipeline'

        // IIS config
        IIS_SITE_NAME   = 'XRdashboard_Backend'
        IIS_APPPOOL     = 'XRdashboard_Backend'  // change if your app pool has a different name

        // Email notification (same email as frontend)
        PERSONAL_EMAIL  = 'reddydr257@gmail.com'
    }

    stages {
        stage('Checkout') {
            steps {
                echo 'Checking out code from GitHub (master branch)...'
                git branch: 'master',
                    credentialsId: GIT_CREDENTIALS,
                    url: REPO_URL
            }
        }

        stage('Restore') {
            steps {
                echo 'Running dotnet restore...'
                bat """
                dotnet restore "${SOLUTION_FILE}"
                """
            }
        }

        stage('Build') {
            steps {
                echo 'Building solution in Release mode...'
                bat """
                dotnet build "${SOLUTION_FILE}" -c Release --no-restore
                """
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                bat """
                dotnet test "${SOLUTION_FILE}" -c Release --no-build
                """
            }
        }

        stage('Publish') {
            steps {
                echo "Publishing WebApi project to ${PUBLISH_DIR} ..."
                bat """
                dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${PUBLISH_DIR}"
                """
            }
        }

        stage('Deploy to IIS') {
            steps {
                echo "Updating IIS site '${IIS_SITE_NAME}' and restarting..."
                powershell '''
                  Import-Module WebAdministration

                  $siteName = $env:IIS_SITE_NAME
                  $appPool  = $env:IIS_APPPOOL
                  $path     = $env:PUBLISH_DIR

                  Write-Host "Setting IIS site path for $siteName to $path"
                  Set-ItemProperty "IIS:\\Sites\\$siteName" -Name physicalPath -Value $path

                  Write-Host "Restarting App Pool: $appPool"
                  Restart-WebAppPool -Name $appPool

                  Write-Host "Restarting Site: $siteName"
                  Restart-WebItem "IIS:\\Sites\\$siteName"
                '''
            }
        }
    }

    post {
        success {
            echo '✅ Backend build, test, publish, and IIS deploy completed successfully — sending email...'

            mail to: "${env.PERSONAL_EMAIL}",
                 subject: "BACKEND SUCCESS: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
                 body: """Hello Suhas,

The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} completed SUCCESSFULLY.

Backend details:
- Solution : ${env.SOLUTION_FILE}
- Publish  : ${env.PUBLISH_DIR}
- IIS site : ${env.IIS_SITE_NAME}

Build URL : ${env.BUILD_URL}

Regards,
Jenkins (Backend pipeline)
"""
        }

        failure {
            echo '❌ Backend pipeline failed — sending email...'

            mail to: "${env.PERSONAL_EMAIL}",
                 subject: "BACKEND FAILURE: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
                 body: """Hello Suhas,

The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} has FAILED.

Backend details:
- Solution : ${env.SOLUTION_FILE}
- Publish  : ${env.PUBLISH_DIR}
- IIS site : ${env.IIS_SITE_NAME}

Build URL : ${env.BUILD_URL}

Please check the Jenkins console output for error details.

Regards,
Jenkins (Backend pipeline)
"""
        }

        always {
            echo 'Backend post actions finished.'
        }
    }
}



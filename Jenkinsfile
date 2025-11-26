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

// pipeline {
//     agent any  // Windows Jenkins agent

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

//         // Email notification (same email as frontend)
//         PERSONAL_EMAIL  = 'reddydr257@gmail.com'
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
//                 bat """
//                 dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${PUBLISH_DIR}"
//                 """
//             }
//         }

//         stage('Deploy to IIS') {
//             steps {
//                 echo "Updating IIS site '${IIS_SITE_NAME}' and restarting..."
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
//             echo '✅ Backend build, test, publish, and IIS deploy completed successfully — sending email...'

//             mail to: "${env.PERSONAL_EMAIL}",
//                  subject: "BACKEND SUCCESS: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
//                  body: """Hello Suhas,

// The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} completed SUCCESSFULLY.

// Backend details:
// - Solution : ${env.SOLUTION_FILE}
// - Publish  : ${env.PUBLISH_DIR}
// - IIS site : ${env.IIS_SITE_NAME}

// Build URL : ${env.BUILD_URL}

// Regards,
// Jenkins (Backend pipeline)
// """
//         }

//         failure {
//             echo '❌ Backend pipeline failed — sending email...'

//             mail to: "${env.PERSONAL_EMAIL}",
//                  subject: "BACKEND FAILURE: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
//                  body: """Hello Suhas,

// The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} has FAILED.

// Backend details:
// - Solution : ${env.SOLUTION_FILE}
// - Publish  : ${env.PUBLISH_DIR}
// - IIS site : ${env.IIS_SITE_NAME}

// Build URL : ${env.BUILD_URL}

// Please check the Jenkins console output for error details.

// Regards,
// Jenkins (Backend pipeline)
// """
//         }

//         always {
//             echo 'Backend post actions finished.'
//         }
//     }
// }
//This pipeline is working all and come to send email succes also

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
//         IIS_APPPOOL     = 'XRdashboard_Backend'

//         // Email notification
//         PERSONAL_EMAIL  = 'reddydr257@gmail.com'
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

//         stage('Approval Before Build & Deploy') {
//             steps {
//                 script {
//                     echo "Sending approval email before backend build & deploy..."

//                     // 1) Send approval email
//                     mail to: "${env.PERSONAL_EMAIL}",
//                          subject: "APPROVAL NEEDED: BACKEND build & deploy #${env.BUILD_NUMBER}",
//                          body: """Hello Suhas,

// A new BACKEND build & deploy has been triggered for:

// Job      : ${env.JOB_NAME}
// Build    : #${env.BUILD_NUMBER}
// Git repo : ${env.REPO_URL}

// The pipeline is waiting for your APPROVAL BEFORE proceeding to:
// - dotnet restore / build / test / publish
// - IIS deploy to site: ${env.IIS_SITE_NAME}
// - Physical path: ${env.PUBLISH_DIR}

// Please open the Jenkins build page below and click "Proceed" to approve,
// or "Abort" to stop the deployment:

// ${env.BUILD_URL}

// Regards,
// Jenkins (Backend pipeline)
// """

//                     // 2) Wait for manual approval in Jenkins UI (not via email)
//                     timeout(time: 2, unit: 'HOURS') {
//                         input message: 'Approve BACKEND build & deploy to IIS?',
//                               ok: 'Proceed'
//                     }

//                     echo "Approval received, continuing backend pipeline..."
//                 }
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
//                 bat """
//                 dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${PUBLISH_DIR}"
//                 """
//             }
//         }

//         stage('Deploy to IIS') {
//             steps {
//                 echo "Updating IIS site '${IIS_SITE_NAME}' and restarting..."
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
//             echo '✅ Backend build, test, publish, and IIS deploy completed successfully — sending email...'

//             mail to: "${env.PERSONAL_EMAIL}",
//                  subject: "BACKEND SUCCESS: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
//                  body: """Hello Suhas,

// The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} completed SUCCESSFULLY.

// Backend details:
// - Solution : ${env.SOLUTION_FILE}
// - Publish  : ${env.PUBLISH_DIR}
// - IIS site : ${env.IIS_SITE_NAME}

// Build URL : ${env.BUILD_URL}

// Regards,
// Jenkins (Backend pipeline)
// """
//         }

//         failure {
//             echo '❌ Backend pipeline failed — sending email...'

//             mail to: "${env.PERSONAL_EMAIL}",
//                  subject: "BACKEND FAILURE: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
//                  body: """Hello Suhas,

// The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} has FAILED.

// Backend details:
// - Solution : ${env.SOLUTION_FILE}
// - Publish  : ${env.PUBLISH_DIR}
// - IIS site : ${env.IIS_SITE_NAME}

// Build URL : ${env.BUILD_URL}

// Please check the Jenkins console output for error details.

// Regards,
// Jenkins (Backend pipeline)
// """
//         }

//         aborted {
//             echo '⚠️ Backend pipeline was ABORTED — sending email...'

//             mail to: "${env.PERSONAL_EMAIL}",
//                  subject: "BACKEND ABORTED: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
//                  body: """Hello Suhas,

// The BACKEND Jenkins job '${env.JOB_NAME}' build #${env.BUILD_NUMBER} was ABORTED.

// This often happens when:
// - The approval step was cancelled, or
// - Someone clicked "Abort" in the Jenkins UI.

// Build URL : ${env.BUILD_URL}

// Regards,
// Jenkins (Backend pipeline)
// """
//         }

//         always {
//             echo 'Backend post actions finished.'
//         }
//     }
// }
//in the above code is working up to emial approval also 


pipeline {
    agent any

    environment {
        GIT_CREDENTIALS = 'token'
        REPO_URL        = 'https://github.com/Suhasreddy257/xr-dashbaoard-backend-dev.git'

        SOLUTION_FILE   = 'AP.CHRP.XRDB.WebApi.sln'
        WEBAPI_PROJECT  = 'AP.CHRP.XRDB.WebApi/AP.CHRP.XRDB.WebApi.csproj'

        DEPLOY_BASE     = 'D:\\backend_codebuildpipeline'

        IIS_SITE_NAME   = 'XRdashboard_Backend'
        IIS_APPPOOL     = 'XRdashboard_Backend'

        PERSONAL_EMAIL  = 'reddydr257@gmail.com'
    }

    stages {
        stage('Prepare Timestamp Deploy Folder') {
            steps {
                script {
                    def ts = new Date().format("yyyy-MM-dd_HH-mm-ss")

                    env.DEPLOY_FOLDER_NAME = "backend_${ts}"
                    env.DEPLOY_TARGET = "${DEPLOY_BASE}\\${env.DEPLOY_FOLDER_NAME}"

                    echo "Timestamp folder: ${env.DEPLOY_FOLDER_NAME}"
                    echo "Full path      : ${env.DEPLOY_TARGET}"
                }
            }
        }

        stage('Checkout') {
            steps {
                echo 'Checking out backend code...'
                git branch: 'master',
                    credentialsId: GIT_CREDENTIALS,
                    url: REPO_URL
            }
        }
        stage('Restore') {
            steps {
                bat """
                dotnet restore "${SOLUTION_FILE}"
                """
            }
        }
        stage('Build') {
            steps {
                bat """
                dotnet build "${SOLUTION_FILE}" -c Release --no-restore
                """
            }
        }

        /* ------------------ 5. dotnet test ------------------ */
        stage('Test') {
            steps {
                bat """
                dotnet test "${SOLUTION_FILE}" -c Release --no-build
                """
            }
        }
        stage('Publish') {
            steps {
                echo "Publishing backend to: ${env.DEPLOY_TARGET}"

                bat """
                if not exist "${DEPLOY_TARGET}" (
                    mkdir "${DEPLOY_TARGET}"
                )

                dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${DEPLOY_TARGET}"
                """
            }
        }

        stage('Deploy to IIS') {
            steps {
                powershell '''
                    Import-Module WebAdministration

                    $siteName = $env:IIS_SITE_NAME
                    $appPool  = $env:IIS_APPPOOL
                    $path     = $env:DEPLOY_TARGET

                    Write-Host "Setting IIS physicalPath to: $path"
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

        /* Success email */
        success {
            mail to: "${env.PERSONAL_EMAIL}",
                 subject: "BACKEND SUCCESS: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
                 body: """Hello Suhas,

The BACKEND Jenkins pipeline completed SUCCESSFULLY.

Backend Build Info:
- Solution: ${env.SOLUTION_FILE}
- Published To: ${env.DEPLOY_TARGET}
- IIS Site: ${env.IIS_SITE_NAME}
- App Pool: ${env.IIS_APPPOOL}

Build URL:
${env.BUILD_URL}

All older builds are stored under:
${env.DEPLOY_BASE}

Regards,
Jenkins (Backend Pipeline)
"""
        }

        /* Failure email */
        failure {
            mail to: "${env.PERSONAL_EMAIL}",
                 subject: "BACKEND FAILURE: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
                 body: """Hello Suhas,

The BACKEND Jenkins build FAILED.

Please check logs:
${env.BUILD_URL}

Last attempted deploy folder:
${env.DEPLOY_TARGET}

Regards,
Jenkins (Backend Pipeline)
"""
        }

        always {
            echo "Backend pipeline finished (Post actions)."
        }
    }
}







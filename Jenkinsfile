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

pipeline {
    agent any

    environment {
        REPO_URL        = 'https://github.com/Suhasreddy257/xr-dashbaoard-backend-dev.git'
        GIT_CREDENTIALS = 'token'
        SOLUTION_FILE   = 'AP.CHRP.XRDB.WebApi.sln'
        WEBAPI_PROJECT  = 'AP.CHRP.XRDB.WebApi/AP.CHRP.XRDB.WebApi.csproj'
        PUBLISH_DIR     = 'D:\\backend_codebuildpipeline'
        IIS_SITE_NAME   = 'XRdashboard_Backend'
        // If your app pool name is different, change this:
        IIS_APPPOOL     = 'XRdashboard_Backend'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'master',  // we fixed this earlier
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

        stage('Test') {
            steps {
                bat """
                dotnet test "${SOLUTION_FILE}" -c Release --no-build
                """
            }
        }

        stage('Publish') {
            steps {
                bat """
                if exist "${PUBLISH_DIR}" rmdir /S /Q "${PUBLISH_DIR}"
                mkdir "${PUBLISH_DIR}"
                dotnet publish "${WEBAPI_PROJECT}" -c Release -o "${PUBLISH_DIR}"
                """
            }
        }

        stage('Deploy to IIS') {
            steps {
                // Use PowerShell to point the IIS site to the new folder and restart it
                bat """
                powershell -NoProfile -ExecutionPolicy Bypass -Command ^
                  "Import-Module WebAdministration; ^
                   Set-ItemProperty 'IIS:\\Sites\\${IIS_SITE_NAME}' -Name physicalPath -Value '${PUBLISH_DIR}'; ^
                   Restart-WebAppPool -Name '${IIS_APPPOOL}'; ^
                   Restart-WebItem 'IIS:\\Sites\\${IIS_SITE_NAME}'"
                """
            }
        }
    }

    post {
        success {
            echo '✅ Build, test, publish and IIS deploy completed successfully.'
        }
        failure {
            echo '❌ Pipeline failed. Check the logs above.'
        }
    }
}

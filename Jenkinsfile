pipeline {
    agent any

    environment {
        REPO_URL       = 'https://github.com/Suhasreddy257/xr-dashbaoard-backend-dev.git'
        GIT_CREDENTIALS = 'token'   // Jenkins credentials ID
        SOLUTION_FILE  = 'AP.CHRP.XRDB.WebApi.sln'
        WEBAPI_PROJECT = 'AP.CHRP.XRDB.WebApi/AP.CHRP.XRDB.WebApi.csproj'
        PUBLISH_DIR    = 'D:\\backend_codebuildpipeline'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main',
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
    }

    post {
        success {
            echo '✅ Build, test, and publish completed successfully.'
        }
        failure {
            echo '❌ Pipeline failed. Check the logs above.'
        }
    }
}

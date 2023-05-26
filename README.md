# Azure and Doccer
here i want to explore about azure deployement and how Doccer affects it all.

## Azure Deployement
- Create an Azure Account: Sign up for an Azure account if you don't have one already. You can visit the Azure portal at portal.azure.com.

- Create an API: Develop your API using your preferred programming language and framework (e.g., Node.js with Express, ASP.NET, Python with Flask/Django, etc.). Make sure your API is ready for deployment, and you have the necessary code and dependencies.

- Set up a SQL Server database: In the Azure portal, navigate to the SQL Databases service and create a new SQL Server database. Configure the necessary settings such as server name, credentials, and resource group.

- Configure firewall rules: By default, Azure SQL Server blocks all external connections. You'll need to configure the firewall rules to allow access to your SQL Server database. Go to the SQL Server's Firewall settings and add the appropriate IP addresses or IP ranges to allow access.

- Deploy the API to Azure: There are different ways to deploy an API on Azure, depending on your preferred deployment method and technology stack. Here are a few options:

- Azure App Service: Create an Azure App Service and publish your API as an App Service application. You can deploy directly from source control (e.g., GitHub, Azure DevOps), or you can deploy a Docker container if your API is containerized.

- Azure Functions: If your API follows a serverless architecture, you can deploy your API as Azure Functions. Each function can be triggered by an HTTP request.

- Azure Kubernetes Service (AKS): If you want to deploy your API using containers and Kubernetes, you can set up an AKS cluster and deploy your API as a containerized service.

- Connect the API to the SQL Server database: Update your API's configuration to use the connection string for the Azure SQL Server database you created. This connection string should include the server name, credentials, and database name.

- Test the deployment: Once the API is deployed, test its functionality by making requests to the API endpoints and ensuring it can connect to and retrieve data from the SQL Server database.

## Doccer 
Docker is an open-source platform that allows you to automate the deployment, scaling, and management of applications using containerization. It provides a way to package an application and its dependencies into a standardized unit called a container. These containers are lightweight and isolated, allowing them to run consistently across different environments.

### Here are some key concepts related to Docker:

- Containers: Containers are isolated and lightweight environments that package an application with its dependencies. They are portable and can run on any system that supports Docker. Containers provide a consistent and reproducible environment, ensuring that an application behaves the same way across different systems.

- Images: An image is a read-only template that contains all the instructions and dependencies needed to create a container. It serves as a blueprint for creating containers. Docker images are built using a set of instructions defined in a Dockerfile, which specifies the base image, application code, dependencies, and configuration.

- Docker Engine: Docker Engine is the runtime that runs and manages containers. It consists of the Docker daemon, which is responsible for building, running, and managing containers, and the Docker CLI (Command-Line Interface), which allows users to interact with Docker.

- Dockerfile: A Dockerfile is a text file that contains a set of instructions to build a Docker image. It specifies the base image, copies the application code, installs dependencies, configures the environment, and exposes ports, among other things. Dockerfiles provide a declarative way to define the desired state of a container.

- Docker Compose: Docker Compose is a tool that allows you to define and manage multi-container applications. It uses a YAML file to define the services, networks, and volumes required by the application. Docker Compose simplifies the process of running complex applications composed of multiple containers.

- Docker Registry: A Docker registry is a central repository where Docker images can be stored, shared, and downloaded. The default public registry is Docker Hub, which hosts a vast collection of pre-built images. You can also set up your private registry to store and distribute your own Docker images.

Docker provides a flexible and efficient way to package and deploy applications, making it easier to manage dependencies and ensure consistent behavior across different environments. It has gained significant popularity in the software development and deployment landscape due to its simplicity and portability.

### Videos watched

Docker is a virtualisation tech to make deployement easier. It uses containers to make this happen. It also makes developing easier. You dont have to download and intall all the services locally. The Docker container takes care of that. 

- It standardizes process of running any service on any local dev environment. 

- noconfig needed directly on the server OS. Less room for errors. 

ofcourse you'll need to install docker on the server. But thats only a one time thing and it makes the operations for the rest of the application easier. 

Here are the differences between docker and virtual machines.:
### docker
- Docker runs on the APPLICATION LAYER od the OS. and it uses the kernel of the host sinds it doesnt have its own kernel.  
- docker images are much smaller. (MB)
- docker containers start much faster, they take seconds to start.
- Docker is only compatible with linux. thats why docker desktop can be used on windows and mac.
### VM
- Virtual machines are compatible on any OS and can be any OS themselves.
- virtual machine images are much larger. (GB)
- virtual machines take minutes to start. 
- Virtual machines have an application layer and its own kernel. 
## CI (Continuous Integration)
Continuous Integration (CI) is a software development practice that involves regularly merging code changes from multiple developers into a central repository. The main goal of CI is to catch and address integration issues early in the development process. It promotes collaboration, early bug detection, and efficient development workflows.

Here's how CI typically works:

- Version Control: Developers work on separate branches of a version control system, such as Git. Each branch represents a specific feature or bug fix.

- Automated Build: When a developer completes their changes and pushes them to the central repository, a CI server or build system automatically triggers a build process. This process compiles the code, runs tests, and generates executable files or artifacts.

- Testing: The CI server runs various automated tests on the newly built code. This includes unit tests, integration tests, and sometimes even performance or security tests. These tests verify the correctness and quality of the code.

- Reporting: The CI server generates reports indicating the status and results of the build and tests. Developers can quickly identify any failures or issues that need to be addressed.

- Notifications: The CI system sends notifications to relevant team members, such as developers or project managers, to inform them about the build and test results. This helps in prompt issue resolution and facilitates communication among team members.

- Continuous Feedback: The CI process provides rapid feedback to developers, allowing them to detect and fix integration problems, conflicts, or bugs early in the development cycle. It encourages smaller, more frequent code changes instead of large, infrequent merges.

- Continuous Deployment (Optional): In addition to CI, some teams implement Continuous Deployment (CD), where successful builds and tests automatically trigger the deployment of the application to production or staging environments.

By following the CI approach, development teams can minimize integration issues, improve code quality, increase productivity, and enhance collaboration among team members. It allows for faster development cycles and reduces the likelihood of encountering significant problems during the final stages of a project.

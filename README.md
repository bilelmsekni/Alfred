# Alfred 
[![Build status](https://ci.appveyor.com/api/projects/status/ol7r703e1rmagn44?svg=true)](https://ci.appveyor.com/project/mseknibilel/alfred)
[![Coverage Status](https://coveralls.io/repos/github/mseknibilel/Alfred/badge.svg?branch=master)](https://coveralls.io/github/mseknibilel/Alfred?branch=master)
[![Source Browser](https://img.shields.io/badge/Browse-Source-green.svg)](http://sourcebrowser.io/Browse/mseknibilel/Alfred)
[![SonarQube Tech Debt](https://img.shields.io/sonar/http/sonar.qatools.ru/ru.yandex.qatools.allure:allure-core/tech_debt.svg)](https://sonarqube.com/dashboard/index/1191532)

Presentation
-----------------

Alfred is a community managing application. Its purpose is to facilitate the tasks of planning and monitoring the work of a group of people. It also comes with additional features inspired from the agile world such as displaying backlog, settings goals and reporting KPIs

Solution Details
-----------------

> This repo contains the backend part of Alfred. If you are looking for the frontend, you can find it here https://github.com/mseknibilel/Alfred.GUI

* Alfred API overview

![ScreenShot](http://i.imgur.com/HdCIVdb.png)

### Quick start
**Make sure you installed visual studio and added paket for Visual studio extension**

```bash
# clone the repo
git clone https://github.com/mseknibilel/Alfred.GUI.git

# Build solution (this will take a while as it needs to download dependencies)
Build > Build Solution
```

### Solution Structure
I used Domain Driven Design in my project. It seperates code to different layers and prevents it from leaking. On the other hand, it is time consuming because it needs writing a lot of code compared to other approaches. Here's how it looks:

![ScreenShot](http://i.imgur.com/CrLpzsm.png)

  1. **Alfred.WebApi**: WebApi project to expose Alfred services using REST
  2. **Alfred.IoC**: IoC configuration has its own project for the sake of clarity and Dll referencing optimization
  3. **Alfred.Dal.Implementation.Fake**: An implementation of Data Access Layer using in memory data provided by Bogus
  4. **Alfred.Dal**: The required abstraction of the Data Access Layer by the domain layer
  5. **Alfred.Domain**: The concrete implementation of Alfred's domain layer
  6. **Alfred**: The abstraction of Alfred's domain layer
  7. **Alfred.Shared**: Because no matter what, we always have common extension methods, enums, ... but NO LOGIC !
  
  - **Alfred.Logging**: Provides logging features
  - **Alfred.Configuration**: Provides configuration features
  
  Next steps
-----------------

  1. Add more features to support score
  2. Develop a proper DAL implementation
  3. .Net core migration


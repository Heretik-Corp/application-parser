# Relativity Application-Parser
[![Build status](https://ci.appveyor.com/api/projects/status/vti5t700o00f58j4?svg=true)](https://ci.appveyor.com/project/dvbarnes/application-parser)

```
Install-Package Heretik.ApplicationParser
```
After installing a ObjectTypes.tt will be generated that will output a cs file with all of the information in the application.xml file

If you would like to consume this package and not use the T4 template library you can run
```
Install-Package Heretik.ApplicationParser.Core
```

## Note:
All template files assume the application file will be in the $(SolutionDir)\application\application.xml if you would like to change the location you will have to edit each file separately.

While this is an open source project on the kCura GitHub account, support is only available through through the Relativity developer community. You are welcome to use the code and solution as you see fit within the confines of the license it is released under. However, if you are looking for support or modifications to the solution, we suggest reaching out to the Project Champion listed below.

# Project Champion 
![Heretik](https://kcura-media.s3.amazonaws.com/app/img/partner_logos/heretik_small.png "Heretik")

[Heretik](https://heretik.io/) is a major contributor to this project.  If you are interested in having modifications made to this project, please reach out to [Heretik](https://heretik.io/).

Also check out [Heretik's github page](https://github.com/Heretik-Corp) for more interesting projects

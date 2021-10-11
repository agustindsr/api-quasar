# api-quasar

## Tecnologías utilizadas
El proyecto está construído con el framework Asp Net Core 3.1.

## Arquitectura

- _Controller_: Todos las clases que reciben peticiones HTTP a los end-point defindos.
- _Services_: Lógica necesaria para calcular la posición final de la nave y el mensage recibido.
- _Services.Interfaces_: Todas las interfaces para los servicios.
- _Repository_: Capa de acceso a datos.
- _DataAccess.Interfaces_: Todas las interfaces del acceso a datos, en este se incluyen las interfaces de los repositorios.
- _Domain_: Tiene todo lo que representa al dominio de la aplicación, como las entidades necesarias y las excepciones asociadas al negocio.
- _Common_: Todo lo común a varios proyectos como Dtos utilizados tanto en la capa de controller como en la de servicio.
- _Test.Infrasture_: Contiene la infraestructura de los test, como un test server para levantar la aplicación.
- _Test.Integration_: Test de intergración de la aplicación.
- _Test.Unit_: Test unitarios de la aplicación.

## Azure
La solución está disponible en Azure
[https://meliquasarapi.azurewebsites.net/swagger/index.html](https://meliquasarapi.azurewebsites.net/swagger/index.html)

## Ejecución

### Docker

Ir la carpeta raíz del proyecto y correr los siguientes comandos en consola:
1. Build images:
`docker build -t melinetcore .`
2. Run container: 
`docker run -d -p 80:80 --name Quasar melinetcore`
3. Go to:
`localhost/swagger`
### Local
1. `cd Meli.Quasar.Api`
2. `dotnet run`
3. Go to:
`localhost:5001/swagger`
## API

- Documentación: [Swagger](https://meliquasarapi.azurewebsites.net/swagger/index.html)

- Postman: [Meli Quasar Collection](https://www.postman.com/agusdsr/workspace/meli/collection/6179752-d1e56b65-0302-43f0-8153-274eb897a3de?ctx=documentation)


### Validaciones
- Todas las distancias deben ser números positivos.
- Los nombres de los satellites tienen que ser válidos (kenobi, sato o skywalker).

# Inventario App

Este proyecto es un sistema sencillo de gestión de inventario desarrollado con ASP.NET Core para la API y Blazor WebAssembly para el cliente. La aplicación permite gestionar productos, incluyendo operaciones como crear, actualizar, eliminar y listar productos.

## Estructura del Proyecto

El proyecto está dividido en tres partes principales:

1. **Inventario.Api**: Contiene la API REST que maneja las operaciones relacionadas con los productos.
   - **Controllers**: Controladores que manejan las solicitudes HTTP.
   - **Models**: Modelos que representan las entidades del sistema.
   - **Services**: Servicios que contienen la lógica de negocio.
   - **Data**: Contexto de la base de datos.
   - **Properties**: Configuraciones de inicio.
   - **Program.cs**: Punto de entrada de la aplicación.
   - **Startup.cs**: Configuración de servicios y middleware.
   - **appsettings.json**: Configuraciones de la aplicación.

2. **Inventario.Client**: Contiene el cliente web desarrollado en Blazor WebAssembly.
   - **wwwroot**: Archivos estáticos como CSS y JavaScript.
   - **Pages**: Páginas de la aplicación Blazor.
   - **Services**: Servicios para realizar solicitudes a la API.
   - **Program.cs**: Punto de entrada de la aplicación Blazor.
   - **App.razor**: Estructura de la aplicación Blazor.

3. **Inventario.Tests**: Contiene pruebas unitarias y de integración para asegurar la calidad del código.
   - **Unit**: Pruebas unitarias para la lógica de negocio.
   - **Integration**: Pruebas de integración para la API.

## Instalación

Para ejecutar el proyecto, sigue estos pasos:

1. Clona el repositorio:
   ```
   git clone <url-del-repositorio>
   ```

2. Navega al directorio del proyecto:
   ```
   cd inventario-app
   ```

3. Restaura las dependencias:
   ```
   dotnet restore
   ```

4. Ejecuta la API:
   ```
   cd Inventario.Api
   dotnet run
   ```

5. En otra terminal, ejecuta el cliente:
   ```
   cd Inventario.Client
   dotnet run
   ```

## Uso

Una vez que la API y el cliente estén en ejecución, puedes acceder a la aplicación Blazor en tu navegador en `http://localhost:5000` (o el puerto que se muestre en la consola).

## Autenticación y Autorización

La API incluye mecanismos de autenticación y autorización para proteger los endpoints. Asegúrate de tener las credenciales necesarias para acceder a las operaciones restringidas.

## Documentación

La API está documentada utilizando Swagger. Puedes acceder a la documentación en `http://localhost:5000/swagger` una vez que la API esté en ejecución.

## Pruebas

El proyecto incluye pruebas unitarias y de integración. Para ejecutar las pruebas, navega al directorio de pruebas y ejecuta:

```
cd Inventario.Tests
dotnet test
```

## Contribuciones

Las contribuciones son bienvenidas. Si deseas contribuir, por favor abre un issue o envía un pull request.

## Licencia

Este proyecto está bajo la licencia MIT."# laboratorio__3" 
